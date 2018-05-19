using DynamiConfig.Common;
using DynamiConfig.Data;
using DynamiConfig.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DynamiConfig.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection ConfigureDynamiConfig(this IServiceCollection services, IConfiguration configuration)
        {
            RedisContext.SetConnectionString(configuration[ConfigurationConstants.RedisConnectionString]);
            new MongoContext(configuration[ConfigurationConstants.MongoDBConnectionString]);
            IoC.CreateContainer();
            return services;
        }
    }
}
