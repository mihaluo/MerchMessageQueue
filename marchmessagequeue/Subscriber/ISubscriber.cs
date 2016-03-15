using System;
using MarchMessageQueue.Messages;

namespace MarchMessageQueue.Subscriber
{
    public interface ISubscriber
    {
        void Subscribe(Type consumeType);

    }
}