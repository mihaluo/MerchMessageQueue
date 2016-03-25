using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KafkaNet.Common;
using KafkaNet.Protocol;
using MarchMessageQueue.Common;
using MarchMessageQueue.Perform;
using MarchMessageQueue.Scheduling;

namespace MarchMessageQueue.Subscriber
{
    public class KafkaGeneralSubscriber : Kafka, ISubscriber
    {
        public KafkaGeneralSubscriber()
        {

        }

        public KafkaGeneralSubscriber(string hosts) : base(hosts)
        {

        }

        public async void Subscribe(Type consumeType)
        {
            await Task.Factory.StartNew(() =>
             {
                 Type messageType = MapManager.GetMessageTypeByConsumeType(consumeType);
                 string topic = GetGeneralTopic(messageType);
                 KafkaNet.Consumer consumer = GetConsumer(topic);
                 List<OffsetPosition> offsetPositions = consumer.GetOffsetPosition();
                 int count = offsetPositions.Count;
                 foreach (var offsetPosition in offsetPositions)
                 {
                     consumer.SetOffsetPosition(offsetPosition);
                 }
                 foreach (var message in consumer.Consume())
                 {
                     ConsumerPerformer consumerPerformer = new ConsumerPerformer();
                     var messageObj = message.Value.ToUtf8String().ToObject(messageType);
                     PerformContext performContext = new PerformContext(messageType, consumeType, messageObj);
                     PerformedContext performedContext = consumerPerformer.Perform(performContext);

                     RetrySchedule.Schedule(performedContext);
                 }
             });
        }
    }
}