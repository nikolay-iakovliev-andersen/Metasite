using Metasite.DataStorage.Contracts;
using Metasite.DataStorage.Entities;
using Metasite.Logic.Api.Exceptions;
using Metasite.Logic.Weather.Contracts;
using Metasite.Logic.Weather.Exceptions;
using Metasite.Logic.Weather.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Metasite.Logic.Weather.Services
{
    internal sealed class WeatherService : IWeatherService
    {
        private readonly IWeatherProvider weatherProvider;
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<WeatherService> logger;

        public WeatherService(IWeatherProvider weatherProvider, IUnitOfWork unitOfWork, ILoggerFactory loggerFactory)
        {
            this.weatherProvider = weatherProvider;
            this.unitOfWork = unitOfWork;
            this.logger = loggerFactory.CreateLogger<WeatherService>();
        }

        public async Task<WeatherDto> GetAsync(string city)
        {
            if (string.IsNullOrEmpty(city))
            {
                throw new ArgumentException("City cannot be null or empty", nameof(city));
            }

            WeatherResponse weather;
            try
            {
                weather = await this.weatherProvider.GetWeatherByCityAsync(city);
                this.logger.LogInformation($"Successfully got weather by city \"{city}\".");
            }
            catch (MetasiteProviderException exception)
            {
                this.logger.LogError(exception, $"Unable to get weather by city \"{city}\".");
                throw new WeatherServiceException($"Could not fetch weather information for city \"{city}\"", exception);
            }

            return new WeatherDto
            {
                City = weather.City,
                Precipitation = weather.Precipitation,
                Temperature = weather.Temperature,
                State = weather.State,
            };
        }

        public async Task SaveAsync(IEnumerable<WeatherDto> weatherItems)
        {
            foreach (WeatherDto weather in weatherItems)
            {
                await this.unitOfWork.Weather.AddAsync(new WeatherEntity
                {
                    City = weather.City,
                    Precipitation = weather.Precipitation,
                    Temperature = weather.Temperature,
                    State = weather.State,
                });
            }
            await this.unitOfWork.CommitAsync();

            this.logger.LogInformation($"Successfully saved weather items: \"{JsonConvert.SerializeObject(weatherItems)}\".");
        }

        public async Task SaveAsync(WeatherDto weather)
        {
            await this.unitOfWork.Weather.AddAsync(new WeatherEntity
            {
                City = weather.City,
                Precipitation = weather.Precipitation,
                Temperature = weather.Temperature,
                State = weather.State,
            });
            await this.unitOfWork.CommitAsync();

            this.logger.LogInformation($"Successfully saved weather item: \"{JsonConvert.SerializeObject(weather)}\".");
        }

        public void Dispose() => this.unitOfWork.Dispose();
    }
}
