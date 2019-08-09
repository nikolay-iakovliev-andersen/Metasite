using Metasite.Logic.Api.Exceptions;
using Metasite.Logic.Api.Options;
using Metasite.Logic.Api.Providers;
using Metasite.Logic.Weather.Contracts;
using Metasite.Logic.Weather.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace Metasite.Logic.Weather.Providers
{
    sealed class WeatherProvider : MetasiteProvider, IWeatherProvider
    {
        public WeatherProvider(HttpClient httpClient, IOptions<WeatherApiOptions> options, ILoggerFactory loggerFactory) : base(httpClient, options, loggerFactory)
        {
        }

        public async Task<WeatherResponse> GetWeatherByCityAsync(string city)
        {
            if (string.IsNullOrEmpty(Token))
            {
                await UpdateTokenAsync();
            }

            do
            {
                string endpoint = Options.WeatherUrl + city;
                HttpRequestMessage requestMessage = CreateRequest(HttpMethod.Get, endpoint);

                HttpResponseMessage responseMessage;
                try
                {
                    responseMessage = await HttpClient.SendAsync(requestMessage);
                }
                catch (HttpRequestException exception)
                {
                    Logger.LogError(exception, $@"
                        Unable to get weather info from remote API.
                        URL: {endpoint}
                        Requested city: {city}.
                    ");
                    throw new MetasiteProviderException($"Unable to fetch weather from endpoint \"{endpoint}\". See inner exception", exception);
                }

                if (!responseMessage.IsSuccessStatusCode)
                {
                    if (responseMessage.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    {
                        await UpdateTokenAsync();
                        continue;
                    }

                    Logger.LogError($@"
                        Unable to get weather info from remote API.
                        URL: {endpoint}
                        Requested city: {city}.
                    ");
                    throw new MetasiteProviderException($"Unable to fetch weather from endpoint \"{endpoint}\". Response status code: {responseMessage.StatusCode}.");
                }

                string content = await responseMessage.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<WeatherResponse>(content);
            } while (true);
        }
    }
}
