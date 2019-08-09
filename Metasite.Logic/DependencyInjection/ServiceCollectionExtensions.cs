using Metasite.DataStorage.DependencyInjection;
using Metasite.Logic.Api.DependencyInjection;
using Metasite.Logic.City.DependencyInjection;
using Metasite.Logic.Weather.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Metasite.Logic.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static void AddLogic(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDataStorage();

            services.AddWeatherModule();
            services.AddCityModule();
            services.AddMetasiteApi(configuration);

            services.AddLogging();
        }
    }
}
