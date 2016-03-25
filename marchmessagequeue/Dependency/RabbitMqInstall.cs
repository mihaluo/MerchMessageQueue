using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using MarchMessageQueue.Publisher;
using MarchMessageQueue.Subscriber;

namespace MarchMessageQueue.Dependency
{
    public class RabbitMqInstall : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<IPublisher, RabbitMqPublish>(),
                Component.For<IRetryPublisher,RabbitMqRetryPublisher>(),
                Component.For<ISubscriber, RabbitMqGeneralSubscriber>(),
                Component.For<IRetrSubscriber, RabbitMqReTrySubscriber>()
                );
        }
    }
}