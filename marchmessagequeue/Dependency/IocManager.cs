using System;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Castle.Windsor.Installer;

namespace MarchMessageQueue.Dependency
{
    public class IocManager
    {
        public static IocManager Instance { get; set; }

        private IWindsorContainer IocContainer { get; set; }

        private const string ConfigFile = "settings.xml";


        static IocManager()
        {
            Instance = new IocManager();
        }

        public IocManager()
        {
            IocContainer = new WindsorContainer();
            IocContainer.Install(Configuration.FromXmlFile(ConfigFile));
        }


        public T Resolve<T>()
        {
            return IocContainer.Resolve<T>();
        }

        public void C()
        {
            
        }
    }
}