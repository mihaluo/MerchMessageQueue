using System;
using System.Threading.Tasks;
using EasyNetQ.NonGeneric;
using MarchMessageQueue.Perform;
using MarchMessageQueue.Scheduling;

namespace MarchMessageQueue.Subscriber
{
    public class RabbitMqGeneralSubscriber : Rabbit, ISubscriber
    {
        public void Subscribe(Type consumeType)
        {
            var rabbitBus = GetRabbitBus();
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