using System;
using EasyNetQ;
using MarchMessageQueue.Messages;
using MarchMessageQueue.Perform;
using MarchMessageQueue.Scheduling;
using Newtonsoft.Json;

namespace MarchMessageQueue.Subscriber
{
    public class RetrySubscriber : ISubscriber
    {
        public void Subscribe(Type consumeType)
        {
            var rabbitBus = RabbitHutch.CreateBus("host=192.168.1.37;port=5672;username=yanwei;password=123456");

            Type messageType = MapManager.GetMessageTypeByConsumeType(consumeType);
            string queue = $"{typeof(RetryMessage).FullName}.{messageType.Name}";

            rabbitBus.Receive<RetryMessage>(queue, message => Consume(message));

        }


        private void Consume(RetryMessage retryMessage)
        {
            Type consumeType = MapManager.GetConsumeTypeByMessageType(retryMessage.MessageType);
            object messageObj = JsonConvert.DeserializeObject(retryMessage.Message, retryMessage.MessageType);
            PerformContext performContext = new PerformContext(retryMessage.MessageType, consumeType, messageObj);
            Console.WriteLine(retryMessage.RetryTimes);
            ConsumerPerformer consumerPerformer = new ConsumerPerformer();
            var performedContext = consumerPerformer.Perform(performContext);

            RetrySchedule.Schedule(performedContext, retryMessage);
        }


    }
}