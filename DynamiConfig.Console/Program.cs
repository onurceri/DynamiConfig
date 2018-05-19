using DynamiConfig.Business.Services.Classes;
using DynamiConfig.Business.Services.Interfaces;
using DynamiConfig.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DynamiConfig.Console
{
    class Program
    {
        public Program(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration Configuration { get; set; }

        static void Main(string[] args)
        {
            Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            var services = new ServiceCollection();
            services.ConfigureDynamiConfig(Configuration);
            services.AddSingleton<IConfigurationReader>(p => new ConfigurationReader("SERVICE-A", 10000));
            var provider = services.BuildServiceProvider();
            provider.GetService<IConfigurationReader>();
        }
    }
}
