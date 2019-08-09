using CommandLine;
using Metasite.Cli.DependencyInjection;
using Metasite.Cli.Extensions;
using Metasite.Cli.Options;
using Metasite.Logic.City.Contracts;
using Metasite.Logic.Weather.Contracts;
using Metasite.Logic.Weather.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Metasite.Cli
{
    class Program
    {
        private static IServiceProvider serviceProvider;

        static void Main(string[] args)
        {
            Startup startup = new Startup();

            serviceProvider = startup.ServiceProvider;

            ParserResult<object> result = Parser.Default.ParseArguments<WeatherOptions, CityOptions>(args)
                .WithParsed<WeatherOptions>(RunWeather)
                .WithParsed<CityOptions>(RunCities);

            Console.ReadKey(true);
        }

        static void RunWeather(WeatherOptions options)
        {
            IEnumerable<string> cities = options.Cities;
            if (cities == null || !cities.Any())
            {
                Console.WriteLine("No cities provided");
            }
            else
            {
                cities = cities.Select(city => city.Trim());
                FetchWeather(cities).GetAwaiter().GetResult();
            }
        }

        static void RunCities(CityOptions options)
        {
            FetchCities().GetAwaiter().GetResult();
        }

        static async Task FetchWeather(IEnumerable<string> cities)
        {
            var service = serviceProvider.GetRequiredService<IWeatherService>();

            CancellationTokenSource source = new CancellationTokenSource();

            Console.WriteLine("Press Enter to stop");
            Console.WriteLine();

            _ = Task.Run(async () =>
            {
                while (true)
                {
                    IEnumerable<WeatherDto> weatherItems = await cities
                        .Select(service.GetAsync)
                        .WhenAll();

                    _ = Task.Run(() => service.SaveAsync(weatherItems));
                    
                    foreach (WeatherDto weather in weatherItems)
                    {
                        DisplayWeatherInfo(weather);
                    }

                    await Task.Delay(TimeSpan.FromSeconds(30));
                }
            }, source.Token);

            ConsoleKeyInfo keyInfo = new ConsoleKeyInfo();
            while (keyInfo.Key != ConsoleKey.Enter)
            {
                keyInfo = Console.ReadKey();
            }

            source.Cancel();
        }

        static async Task FetchCities()
        {
            var service = serviceProvider.GetRequiredService<ICityService>();

            IEnumerable<string> cities = await service.GetCitiesAsync();

            Console.WriteLine("Cities available:");
            foreach (string city in cities)
            {
                Console.WriteLine($"- {city}");
            }
        }

        static void DisplayWeatherInfo(WeatherDto weather)
        {
            Console.WriteLine($"City: {weather.City}");
            Console.WriteLine($"Weather: {weather.State}");
            Console.WriteLine($"Temparature: {weather.Temperature}");
            Console.WriteLine($"Precipitation: {weather.Precipitation}%");
            Console.WriteLine();
        }
    }
}
