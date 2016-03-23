using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KafkaNet;
using KafkaNet.Protocol;
using MarchMessageQueue.Common;
using MarchMessageQueue.Messages;

namespace MarchMessageQueue.Publisher
{
    public class KafkaRetryPublisher : Kafka, IPublisher
    {
        public void Publish<TMessage>(TMessage message) where TMessage : MessageBase
        {
            var producer = GetProducer();

            var messages = new[] { new Message(message.ToJson()) };
            string topic;
            var retryMessage = message as RetryMessage;
            if (retryMessage != null)
            {
                
                topic = GetRetryTopic(retryMessage.MessageType);
                var sendMessageAsync = producer.SendMessageAsync(topic, messages);
                return;
            }
            
            var messageType = message.GetType();
            topic = GetGeneralTopic(messageType);

            var messageAsync = producer.SendMessageAsync(topic, messages);
        }

        public Task PublishAsnyc<TMessage>(TMessage message) where TMessage : MessageBase
        {
            var producer = GetProducer();

            var messages = new[] { new Message(message.ToJson()) };
            string topic;
            var retryMessage = message as RetryMessage;
            if (retryMessage != null)
            {
                topic = GetRetryTopic(retryMessage.MessageType);
                return producer.SendMessageAsync(topic, messages);

            }
            
            var messageType = message.GetType();
            topic = GetGeneralTopic(messageType);

            return producer.SendMessageAsync(topic, messages);
        }
    }
}