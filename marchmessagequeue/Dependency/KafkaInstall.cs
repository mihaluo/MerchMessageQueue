using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using MarchMessageQueue.Publisher;
using MarchMessageQueue.Subscriber;

namespace MarchMessageQueue.Dependency
{
    public class KafkaInstall : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
               Component.For<IPublisher, KafkaPublisher>(),
               Component.For<IRetryPublisher, KafkaRetryPublisher>(),
               Component.For<ISubscriber, KafkaGeneralSubscriber>(),
               Component.For<IRetrSubscriber, KafkaRetrySubscriber>()
               );
        }
    }
}