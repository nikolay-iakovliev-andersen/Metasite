using Metasite.Logic.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Metasite.Cli.DependencyInjection
{
    class Startup
    {
        private readonly IConfiguration configuration;

        public IServiceProvider ServiceProvider { get; }

        public Startup()
        {
            this.configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile($"appsettings.json", optional: false, reloadOnChange: false)
                .Build();

            var serviceCollection = new ServiceCollection();

            ConfigureServices(serviceCollection);

            this.ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddLogic(this.configuration);
        }
    }
}
