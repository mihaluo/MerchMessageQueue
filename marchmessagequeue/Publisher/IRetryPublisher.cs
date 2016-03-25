using System.Threading.Tasks;
using MarchMessageQueue.Messages;

namespace MarchMessageQueue.Publisher
{
    public interface IRetryPublisher
    {
        void Publish<TMessage>(TMessage message) where TMessage : MessageBase;

        Task PublishAsnyc<TMessage>(TMessage message) where TMessage : MessageBase;
    }
}