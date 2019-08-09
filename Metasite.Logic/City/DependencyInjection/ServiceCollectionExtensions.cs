using Metasite.Logic.City.Contracts;
using Metasite.Logic.City.Providers;
using Metasite.Logic.City.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Metasite.Logic.City.DependencyInjection
{
    static class ServiceCollectionExtensions
    {
        public static void AddCityModule(this IServiceCollection services)
        {
            services.AddScoped<ICityService, CityService>();
            services.AddSingleton<ICityProvider, CityProvider>();
        }
    }
}
