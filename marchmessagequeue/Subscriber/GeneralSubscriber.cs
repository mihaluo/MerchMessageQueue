using System;
using EasyNetQ;
using EasyNetQ.NonGeneric;
using MarchMessageQueue.Perform;
using MarchMessageQueue.Scheduling;

namespace MarchMessageQueue.Subscriber
{
    public class GeneralSubscriber : ISubscriber
    {
        public void Subscribe(Type consumeType)
        {
            var rabbitBus = RabbitHutch.CreateBus("host=192.168.1.37;port=5672;username=yanwei;password=123456");
            Type messageType = MapManager.GetMessageTypeByConsumeType(consumeType);

            rabbitBus.Subscribe(messageType, messageType.Name, messageObj =>
            {
                ConsumerPerformer consumerPerformer = new ConsumerPerformer();
                PerformContext performContext = new PerformContext(messageType, consumeType, messageObj);
                PerformedContext performedContext = consumerPerformer.Perform(performContext);

                RetrySchedule.Schedule(performedContext);
            });
        }
        
    }
}