using Metasite.Logic.Weather.Models;
using System.Threading.Tasks;

namespace Metasite.Logic.Weather.Contracts
{
    internal interface IWeatherProvider
    {
        Task<WeatherResponse> GetWeatherByCityAsync(string city);
    }
}
