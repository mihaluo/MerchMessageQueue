using System.Threading.Tasks;
using Hangfire;
using MarchMessageQueue.Common;
using MarchMessageQueue.Consumer;
using MarchMessageQueue.Dependency;
using MarchMessageQueue.Subscriber;

namespace MarchMessageQueue
{
    public static class MarchMessageQueueServer
    {
        public static void Start()
        {
            Task.Factory.StartNew(() =>
            {
                var redisStorage = IocManager.Instance.Resolve<JobStorage>();
                GlobalConfiguration.Configuration.UseStorage(redisStorage);
                var backgroundJobServer = new BackgroundJobServer();

                var consumeTypes = TypeHelper.GetTypesByInterfaceType(typeof (IConsume<>));
                var generalSubscriber = IocManager.Instance.Resolve<ISubscriber>();
                var retrySubscriber = IocManager.Instance.Resolve<IRetrSubscriber>();

                foreach (var consumeType in consumeTypes)
                {
                    //for (int i = 0; i < 5; i++)
                    {
                        Task.Run(() => generalSubscriber.Subscribe(consumeType));
                        Task.Run(() => retrySubscriber.Subscribe(consumeType));
                        //generalSubscriber.Subscribe(consumeType);
                        //retrySubscriber.Subscribe(consumeType);
                    }
                }
            }, TaskCreationOptions.LongRunning);
        }
    }
}