using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KafkaNet;
using KafkaNet.Model;
using KafkaNet.Protocol;
using MarchMessageQueue.Common;
using MarchMessageQueue.Messages;

namespace MarchMessageQueue.Publisher
{
    public class KafkaPublisher : Kafka, IPublisher
    {
        public void Publish<TMessage>(TMessage message) where TMessage : MessageBase
        {
            var producer = GetProducer();

            string topic = GetGeneralTopic(message.GetType());
            var messages = new[] { new Message(message.ToJson()) };
            
            producer.SendMessageAsync(topic, messages);
        }

        public Task PublishAsnyc<TMessage>(TMessage message) where TMessage : MessageBase
        {
            var producer = GetProducer();
            string topic = GetGeneralTopic(message.GetType());
            var messages = new[] { new Message(message.ToJson()) };

            return producer.SendMessageAsync(topic, messages);
        }


    }
}