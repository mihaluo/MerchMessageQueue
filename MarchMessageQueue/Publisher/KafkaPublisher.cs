using System.Threading.Tasks;
using KafkaNet.Protocol;
using MarchMessageQueue.Common;
using MarchMessageQueue.Messages;

namespace MarchMessageQueue.Publisher
{
    public class KafkaPublisher : Kafka, IPublisher
    {
        public KafkaPublisher()
        {
            
        }

        public KafkaPublisher(string hosts) : base(hosts)
        {
            
        }
        
        public async void Publish<TMessage>(TMessage message) where TMessage : MessageBase
        {
            var producer = GetProducer();

            var topic = GetGeneralTopic(message.GetType());
            var messages = new[] {new Message(message.ToJson())};

            await producer.SendMessageAsync(topic, messages);
        }

        public Task PublishAsnyc<TMessage>(TMessage message) where TMessage : MessageBase
        {
            var producer = GetProducer();

            var topic = GetGeneralTopic(message.GetType());
            var messages = new[] {new Message(message.ToJson())};

            return producer.SendMessageAsync(topic, messages);
        }
    }
}