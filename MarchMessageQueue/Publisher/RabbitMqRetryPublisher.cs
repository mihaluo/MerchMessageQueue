using System;
using System.Threading.Tasks;
using MarchMessageQueue.Messages;

namespace MarchMessageQueue.Publisher
{
    public class RabbitMqRetryPublisher : Rabbit, IRetryPublisher
    {
        public void Publish<TMessage>(TMessage message) where TMessage : MessageBase
        {
            var rabbitBus = GetRabbitBus();

            var retryMessage = message as RetryMessage;
            if (retryMessage != null)
            {
                string queue = GetQueueName(retryMessage.MessageType);
                rabbitBus.Send(queue, message);
                return;
            }

            rabbitBus.Publish(message);
        }

        public Task PublishAsnyc<TMessage>(TMessage message) where TMessage : MessageBase
        {
            var rabbitBus = GetRabbitBus();

            var retryMessage = message as RetryMessage;
            if (retryMessage != null)
            {
                string queue = GetQueueName(retryMessage.MessageType);
                return rabbitBus.SendAsync(queue, message);
            }

            return rabbitBus.PublishAsync(message);
        }


        private string GetQueueName(Type messageType)
        {
            return $"{typeof(RetryMessage).FullName}.{nameof(messageType)}";
        }
    }
}