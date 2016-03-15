using EasyNetQ;

namespace MarchMessageQueue
{
    public static class RabbitBusFactory
    {
        public static IBus GetRabbitBus()
        {
            return RabbitHutch.CreateBus("host=dianzipiao.cn;username=vpiao;password=123456");
        }
    }
}