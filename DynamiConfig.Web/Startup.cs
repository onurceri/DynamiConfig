using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using DynamiConfig.Business.Services.Classes;
using DynamiConfig.Business.Services.Interfaces;
using DynamiConfig.Extensions;
using DynamiConfig.Web.Business;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DynamiConfig.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public IContainer ApplicationContainer { get; private set; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.ConfigureDynamiConfig(Configuration);
            services.AddSingleton<IConfigurationReader>(p => new ConfigurationReader("SERVICE-A", 10000));
            var builder = new ContainerBuilder();
            builder.Populate(services);
            builder.RegisterType<ConfigurationDataService>().As<IConfigurationDataService>();
            builder.RegisterType<ConfigService>().As<IConfigService>();
            ApplicationContainer = builder.Build();
            var serviceProvider = new AutofacServiceProvider(ApplicationContainer);
            serviceProvider.GetService<IConfigurationReader>();
            return serviceProvider;
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
