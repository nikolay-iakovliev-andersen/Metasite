using Metasite.Logic.Api.Exceptions;
using Metasite.Logic.Api.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Metasite.Logic.Api.Providers
{
    abstract class MetasiteProvider
    {
        protected HttpClient HttpClient { get; }
        protected WeatherApiOptions Options { get; }
        protected ILogger<MetasiteProvider> Logger { get; }

        public string Token { get; private set; }

        protected MetasiteProvider(HttpClient httpClient, IOptions<WeatherApiOptions> options, ILoggerFactory loggerFactory)
        {
            HttpClient = httpClient;
            Logger = loggerFactory.CreateLogger<MetasiteProvider>();
            Options = options.Value;
        }

        protected HttpRequestMessage CreateRequest(HttpMethod method, string url)
        {
            HttpRequestMessage requestMessage = new HttpRequestMessage(method, url);

            requestMessage.Headers.Add("Authorization", $"bearer {this.Token}");

            return requestMessage;
        }

        protected async Task UpdateTokenAsync()
        {
            string endpoint = Options.AuthorizationUrl;
            string content = JsonConvert.SerializeObject(new TokenRequest
            {
                Username = Options.Username,
                Password = Options.Password,
            });
            HttpContent httpContent = new StringContent(content, Encoding.UTF8, "application/json");

            HttpResponseMessage responseMessage;
            try
            {
                responseMessage = await HttpClient.PostAsync(endpoint, httpContent);
                responseMessage.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException exception)
            {
                Logger.LogError(exception, $@"
                    Unable to get authentication token from remote API.
                    URL: {endpoint}
                    Credentials: {content}.
                ");
                throw new MetasiteProviderException("Unable to fetch token. See inner exception", exception);
            }

            content = await responseMessage.Content.ReadAsStringAsync();
            TokenResponse response = JsonConvert.DeserializeObject<TokenResponse>(content);

            Token = response.Token;
        }

        class TokenRequest
        {
            [JsonProperty("username")]
            public string Username { get; set; }

            [JsonProperty("password")]
            public string Password { get; set; }
        }

        class TokenResponse
        {
            [JsonProperty("bearer")]
            public string Token { get; set; }
        }
    }
}
