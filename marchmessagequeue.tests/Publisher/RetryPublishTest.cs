using MarchMessageQueue.Messages;
using MarchMessageQueue.Publisher;
using Xunit;

namespace MarchMessageQueue.Tests.Publisher
{
    public class RetryPublishTest
    {
        [Fact]
        public void RetryMessagePublish()
        {
            IPublisher publisher = new RetryPublisher();
            publisher.Publish(new RetryMessage());
        }

        [Fact]
        public void SayHelloMessagePublish()
        {
            IPublisher publisher = new RabbitMqPublish();
            publisher.Publish(new SayHelloMessage());
        }
    }
}