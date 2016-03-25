using System.Threading.Tasks;
using EasyNetQ;
using MarchMessageQueue.Messages;

namespace MarchMessageQueue.Publisher
{
    public class RabbitMqPublish : Rabbit, IPublisher
    {
        public async void Publish<TMessage>(TMessage message) where TMessage : MessageBase
        {
            var rabbitBus = GetRabbitBus();
           await rabbitBus.PublishAsync(message);
        }

        public Task PublishAsnyc<TMessage>(TMessage message) where TMessage : MessageBase
        {
            var rabbitBus = GetRabbitBus();
            return rabbitBus.PublishAsync(message);
        }
    }
}