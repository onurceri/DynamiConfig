using Autofac;
using DynamiConfig.Business.Services.Classes;
using DynamiConfig.Business.Services.Interfaces;

namespace DynamiConfig.Infrastructure
{
    public static class IoC
    {
        public static ContainerBuilder Builder;
        private static IContainer Container;

        static IoC()
        {
            if (Builder == null)
            {
                Builder = new ContainerBuilder();

                Builder.RegisterType<ConfigurationDataService>().As<IConfigurationDataService>();
                Builder.RegisterType<CacheService>().As<ICacheService>();
                Builder.RegisterType<ConfigManager>().As<IConfigManager>();
            }
        }

        public static IContainer CreateContainer()
        {
            Container = Builder.Build();
            return Container;
        }

        public static T Resolve<T>()
        {
            return Container.Resolve<T>();
        }
    }
}
