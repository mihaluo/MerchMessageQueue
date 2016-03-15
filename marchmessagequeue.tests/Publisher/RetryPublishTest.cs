using MarchMessageQueue.Messages;
using MarchMessageQueue.Publisher;
using Xunit;

namespace MarchMessageQueue.Tests.Publisher
{
    public class RetryPublishTest
    {
        [Fact]
        public void Publish()
        {
            IPublisher publisher = new RetryPublisher();
            publisher.Publish((MessageBase)new RetryMessage());
        }
    }
}