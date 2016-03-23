using System;
using System.Threading.Tasks;
using KafkaNet;
using KafkaNet.Common;
using MarchMessageQueue.Common;
using MarchMessageQueue.Perform;
using MarchMessageQueue.Scheduling;

namespace MarchMessageQueue.Subscriber
{
    public class KafkaGeneralSubscriber : Kafka, ISubscriber
    {
        public void Subscribe(Type consumeType)
        {
            Type messageType = MapManager.GetMessageTypeByConsumeType(consumeType);
            string topic = GetGeneralTopic(messageType);
            KafkaNet.Consumer consumer = GetConsumer(topic);
            foreach (var message in consumer.Consume())
            {
                ConsumerPerformer consumerPerformer = new ConsumerPerformer();
                var messageObj = message.Value.ToUtf8String().ToObject(messageType);
                PerformContext performContext = new PerformContext(messageType, consumeType, messageObj);
                PerformedContext performedContext = consumerPerformer.Perform(performContext);

                RetrySchedule.Schedule(performedContext);
            }
        }
    }
}