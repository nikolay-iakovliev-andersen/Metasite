using Metasite.DataStorage.Contracts;
using Metasite.DataStorage.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Metasite.DataStorage.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDataStorage(this IServiceCollection services)
        {
            services.AddSingleton<IUnitOfWork, FakeUnitOfWork>();
            services.AddSingleton<IWeatherRepository, FakeWeatherRepository>();
        }
    }
}
