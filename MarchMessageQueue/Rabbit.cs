using EasyNetQ;

namespace MarchMessageQueue
{
    public class Rabbit
    {
        private static IBus Bus;

        protected IBus GetRabbitBus()
        {
            return Bus ?? (Bus = RabbitHutch.CreateBus("host=192.168.1.37;port=5672;username=yanwei;password=123456"));
        }

    }
}