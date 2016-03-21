using System;
using System.Collections;
using System.Collections.Generic;
using Hangfire;
using MarchMessageQueue.Common;
using MarchMessageQueue.Consumer;
using MarchMessageQueue.Hangfire.Redis.Storage;
using MarchMessageQueue.Subscriber;

namespace MarchMessageQueue
{
    public static class MarchMessageQueueServer
    {
        public static void Start()
        {
            RedisStorage redisStorage = new RedisStorage("192.168.1.37:6379");
            GlobalConfiguration.Configuration.UseStorage(redisStorage);
            var backgroundJobServer = new BackgroundJobServer();

            Type[] consumeTypes = TypeHelper.GetTypesByInterfaceType(typeof(IConsume<>));
            ISubscriber generalSubscriber = new GeneralSubscriber();
            ISubscriber retrySubscriber = new RetrySubscriber();
            
            foreach (var consumeType in consumeTypes)
            {
                for (int i = 0; i < 5; i++)
                {
                    generalSubscriber.Subscribe(consumeType);
                    retrySubscriber.Subscribe(consumeType);
                }
              
            }

        }
    }
}