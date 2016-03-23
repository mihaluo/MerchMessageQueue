using System;
using System.Threading.Tasks;
using MarchMessageQueue.Messages;

namespace MarchMessageQueue.Subscriber
{
    public interface ISubscriber
    {
        void Subscribe(Type consumeType);

    }
}