using System;
using System.Collections;
using System.Collections.Generic;
using MarchMessageQueue.Messages;
using MarchMessageQueue.Publisher;
using Xunit;

namespace MarchMessageQueue.Tests.Publisher
{
    public class KafkaPublisherTst : Kafka
    {
        [Fact]
        public void GeneralPublish()
        {
            IPublisher publisher = new KafkaPublisher();
            for (int i = 0; i < 1000; i++)
            {
                SayHelloMessage sayHelloMessage = new SayHelloMessage
                {
                    Message = Guid.NewGuid().ToString()
                };
                publisher.Publish(sayHelloMessage);
            }
           
        }
    }
}