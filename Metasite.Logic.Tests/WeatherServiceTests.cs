using Metasite.DataStorage.Contracts;
using Metasite.Logic.Weather.Contracts;
using Metasite.Logic.Weather.Models;
using Metasite.Logic.Weather.Services;
using Microsoft.Extensions.Logging;
using NSubstitute;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Metasite.Logic.Tests
{
    public class WeatherServiceTests
    {
        private readonly IWeatherProvider provider = Substitute.For<IWeatherProvider>();
        private readonly IUnitOfWork unitOfWork = Substitute.For<IUnitOfWork>();
        private readonly ILoggerFactory loggerFactory = Substitute.For<ILoggerFactory>();

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public async Task Should_Throw_When_CityIsNullOrEmpty(string city)
        {
            WeatherService service = new WeatherService(provider, unitOfWork, loggerFactory);

            Func<Task> testCode = () => service.GetAsync(city);

            await Assert.ThrowsAsync<ArgumentException>(testCode);
        }

        [Fact]
        public async Task Should_ReturnSameWeatherAsProviderReturns()
        {
            WeatherResponse response = new WeatherResponse
            {
                City = "test_city",
                Precipitation = 100,
                State = "test_state",
                Temperature = 77,
            };
            provider.GetWeatherByCityAsync(Arg.Any<string>()).ReturnsForAnyArgs(response);
            WeatherService service = new WeatherService(provider, unitOfWork, loggerFactory);

            WeatherDto weather = await service.GetAsync(response.City);

            Assert.Equal(response.City, weather.City);
            Assert.Equal(response.Precipitation, weather.Precipitation);
            Assert.Equal(response.State, weather.State);
            Assert.Equal(response.Temperature, weather.Temperature);
        }
    }
}
