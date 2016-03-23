using System;
using System.Diagnostics;
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
            Stopwatch stopwatch = Stopwatch.StartNew();
            IPublisher kafkaPublisher = new KafkaPublisher();
            for (int i = 0; i < 10000; i++)
            {
                TestMessage testMessage = new TestMessage
                {
                    Message = Guid.NewGuid().ToString()
                };
                kafkaPublisher.Publish(testMessage);
            }
            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedMilliseconds.ToString());
            Console.ReadKey();
            MarchMessageQueueServer.Start();
            Console.Read();
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

            ISubscriber subscriber = new RabbitMqGeneralSubscriber();
            subscriber.Subscribe(consumeType);

            ISubscriber retrySubscriber = new RabbitMqReTrySubscriber();
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
