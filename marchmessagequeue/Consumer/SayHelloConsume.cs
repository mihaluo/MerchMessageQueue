using System;
using MarchMessageQueue.Messages;

namespace MarchMessageQueue.Consumer
{
    public class SayHelloConsume : IConsume<SayHelloMessage>
    {
        public bool Consume(SayHelloMessage message)
        {
            Console.WriteLine(message.Message);
            Console.WriteLine();
            return true;
        }
    }
}