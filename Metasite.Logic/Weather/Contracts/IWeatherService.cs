using Metasite.Logic.Weather.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Metasite.Logic.Weather.Contracts
{
    public interface IWeatherService : IDisposable
    {
        Task<WeatherDto> GetAsync(string city);

        Task SaveAsync(WeatherDto weather);

        Task SaveAsync(IEnumerable<WeatherDto> weatherItems);
    }
}
