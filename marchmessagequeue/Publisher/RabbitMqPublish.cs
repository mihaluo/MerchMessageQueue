using System.Threading.Tasks;
using EasyNetQ;
using MarchMessageQueue.Messages;

namespace MarchMessageQueue.Publisher
{
    public class RabbitMqPublish : IPublisher
    {
        public void Publish<TMessage>(TMessage message) where TMessage : MessageBase
        {
            var rabbitBus = RabbitBusFactory.GetRabbitBus();
            rabbitBus.Publish(message);
        }

        public Task PublishAsnyc<TMessage>(TMessage message) where TMessage : MessageBase
        {
            throw new System.NotImplementedException();
        }
    }
}