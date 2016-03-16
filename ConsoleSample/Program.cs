using System;
using Hangfire;
using MarchMessageQueue;
using MarchMessageQueue.Common;
using MarchMessageQueue.Consumer;
using MarchMessageQueue.Hangfire.Redis.Storage;
using MarchMessageQueue.Messages;
using MarchMessageQueue.Publisher;
using MarchMessageQueue.Scheduling;
using MarchMessageQueue.Subscriber;

namespace ConsoleSample
{
    class Program
    {
        static void Main(string[] args)
        {

            MarchMessageQueueServer.Start();
            return;
            var consumeType = typeof(SayHelloConsume);



            RedisStorage redisStorage = new RedisStorage("210.14.146.56:6379");
            GlobalConfiguration.Configuration.UseStorage(redisStorage);
           /* BackgroundJob.Enqueue(() => RetrySchedule.RePublish(new RetryMessage
            {
                MessageType = typeof(SayHelloMessage),
                Message = new SayHelloMessage
                {
                    Message = "hello world!"
                }
            }));*/
            
            IPublisher publisher = new RabbitMqPublish();
            publisher.Publish(new SayHelloMessage
            {
                Message = "hello world!"
            });

            ISubscriber subscriber = new GeneralSubscriber();
            subscriber.Subscribe(consumeType);

            ISubscriber retrySubscriber = new RetrySubscriber();
            retrySubscriber.Subscribe(consumeType);

            using (var backgroundJobServer = new BackgroundJobServer())
            {
                Console.ReadLine();
            }
        }        

        public void TestSend()
        {

        }

        public static void Test()
        {
            Console.WriteLine("hello world!");
        }


    }
}
