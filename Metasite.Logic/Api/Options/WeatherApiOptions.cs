namespace Metasite.Logic.Api.Options
{
    public sealed class WeatherApiOptions
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string BaseUrl { get; set; }

        public string AuthorizationEndpoint { get; set; }

        public string CityEndpoint { get; set; }

        public string WeatherEndpoint { get; set; }

        public string AuthorizationUrl => BaseUrl + AuthorizationEndpoint;

        public string CityUrl => BaseUrl + CityEndpoint;

        public string WeatherUrl => BaseUrl + WeatherEndpoint;
    }
}
