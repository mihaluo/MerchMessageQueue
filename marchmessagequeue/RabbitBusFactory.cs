using System;
using System.Collections.Generic;
using EasyNetQ;

namespace MarchMessageQueue
{
    public static class RabbitBusFactory
    {
        private static IBus Bus;

        public static IBus GetRabbitBus()
        {
            return Bus ?? (Bus = RabbitHutch.CreateBus("host=192.168.1.37;port=5672;username=yanwei;password=123456"));
        }
    }
}