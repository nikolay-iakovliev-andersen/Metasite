using Metasite.Logic.Weather.Contracts;
using Metasite.Logic.Weather.Providers;
using Metasite.Logic.Weather.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

namespace Metasite.Logic.Weather.DependencyInjection
{
    static class ServiceCollectionExtensions
    {
        public static void AddWeatherModule(this IServiceCollection services)
        {
            services.AddSingleton<HttpClient>();
            services.AddSingleton<IWeatherProvider, WeatherProvider>();
            services.AddScoped<IWeatherService, WeatherService>();
        }
    }
}
