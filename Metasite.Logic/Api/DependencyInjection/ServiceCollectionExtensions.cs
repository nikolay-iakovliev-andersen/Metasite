using Metasite.Logic.Api.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Metasite.Logic.Api.DependencyInjection
{
    static class ServiceCollectionExtensions
    {
        public static void AddMetasiteApi(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<WeatherApiOptions>(configuration.GetSection("WeatherApi"));
        }
    }
}
