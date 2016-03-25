using System;

namespace MarchMessageQueue.Subscriber
{
    public interface IRetrSubscriber
    {
        void Subscribe(Type consumeType);
    }
}