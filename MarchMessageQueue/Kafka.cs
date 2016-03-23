using System;
using KafkaNet;
using KafkaNet.Model;
using MarchMessageQueue.Messages;

namespace MarchMessageQueue
{
    public class Kafka
    {
        private Producer _producer;
        private KafkaNet.Consumer _consumer;
        private BrokerRouter _brokerRouter;

        protected Producer GetProducer()
        {
            var brokerRouter = GetBrokerRouter();
            return _producer ?? (_producer = new Producer(brokerRouter));
        }

        protected KafkaNet.Consumer GetConsumer(string topic)
        {
            var brokerRouter = GetBrokerRouter();
            ConsumerOptions consumerOptions = new ConsumerOptions(topic, brokerRouter);
            
            return _consumer ?? (_consumer = new KafkaNet.Consumer(consumerOptions));
        }

        protected string GetGeneralTopic(Type messageType)
        {
            return messageType.FullName;
        }

        protected string GetRetryTopic(Type messageType)
        {
            return $"{typeof(RetryMessage).FullName}.{messageType.Name}";
        }


        private BrokerRouter GetBrokerRouter()
        {
            KafkaOptions kafkaOptions = new KafkaOptions(new Uri("http://192.168.1.37:9092"));

            return _brokerRouter ?? (_brokerRouter = new BrokerRouter(kafkaOptions));
        }
    }
}