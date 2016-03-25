using System;
using System.Threading.Tasks;
using KafkaNet.Common;
using KafkaNet.Protocol;
using MarchMessageQueue.Common;
using MarchMessageQueue.Messages;
using MarchMessageQueue.Perform;
using MarchMessageQueue.Scheduling;

namespace MarchMessageQueue.Subscriber
{
    public class KafkaRetrySubscriber : Kafka, IRetrSubscriber
    {
        public KafkaRetrySubscriber()
        {

        }

        public KafkaRetrySubscriber(string hosts) : base(hosts)
        {

        }

        public void Subscribe(Type consumeType)
        {
            Type messageType = MapManager.GetMessageTypeByConsumeType(consumeType);
            string retryTopic = GetRetryTopic(messageType);
            KafkaNet.Consumer consumer = GetConsumer(retryTopic);
            foreach (Message message in consumer.Consume())
            {
                RetryMessage retryMessage = message.Value.ToUtf8String().ToObject<RetryMessage>();
                object messageObj = retryMessage.Message.ToObject(messageType);

                PerformContext performContext = new PerformContext(retryMessage.MessageType, consumeType, messageObj);
                Console.WriteLine(retryMessage.RetryTimes);

                ConsumerPerformer consumerPerformer = new ConsumerPerformer();
                var performedContext = consumerPerformer.Perform(performContext);

                RetrySchedule.Schedule(performedContext, retryMessage);
            }
        }
    }
}