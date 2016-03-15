using System.Threading.Tasks;
using MarchMessageQueue.Messages;

namespace MarchMessageQueue.Publisher
{
    public interface IPublisher
    {
        void Publish<TMessage>(TMessage message) where TMessage : MessageBase;

        Task PublishAsnyc<TMessage>(TMessage message) where TMessage : MessageBase;
    }
}