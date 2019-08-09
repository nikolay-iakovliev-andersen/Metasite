using Metasite.DataStorage.Entities;
using System.Threading.Tasks;

namespace Metasite.DataStorage.Contracts
{
    public interface IWeatherRepository
    {
        Task AddAsync(WeatherEntity entity);
    }
}
