using System.Threading.Tasks;
using MarchMessageQueue.Messages;

namespace MarchMessageQueue.Publisher
{
    public class RetryPublisher : IPublisher
    {
        public void Publish<TMessage>(TMessage message) where TMessage : MessageBase
        {
            var rabbitBus = RabbitBusFactory.GetRabbitBus();

            var retryMessage = message as RetryMessage;
            if (retryMessage != null)
            {
                string queue = $"{typeof(RetryMessage).FullName}.{retryMessage.MessageType.Name}";
                rabbitBus.Send(queue, message);
                return;
            }

            rabbitBus.Publish(message);
        }

        public Task PublishAsnyc<TMessage>(TMessage message) where TMessage : MessageBase
        {
            var rabbitBus = RabbitBusFactory.GetRabbitBus();
            var retryMessage = message as RetryMessage;

            if (retryMessage != null)
            {
                string queue = $"{typeof(RetryMessage).FullName}.{nameof(retryMessage.MessageType)}";
                return rabbitBus.SendAsync(queue, message);

            }

            return rabbitBus.PublishAsync(message);
        }
    }
}