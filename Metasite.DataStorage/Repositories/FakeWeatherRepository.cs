using Metasite.DataStorage.Contracts;
using Metasite.DataStorage.Entities;
using System.Threading.Tasks;

namespace Metasite.DataStorage.Repositories
{
    sealed class FakeWeatherRepository : IWeatherRepository
    {
        public Task AddAsync(WeatherEntity entity)
        {
            return Task.CompletedTask;
        }
    }
}
