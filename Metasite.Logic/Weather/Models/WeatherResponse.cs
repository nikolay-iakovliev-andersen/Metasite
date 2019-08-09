using Newtonsoft.Json;

namespace Metasite.Logic.Weather.Models
{
    internal sealed class WeatherResponse
    {
        public string City { get; set; }

        public decimal Temperature { get; set; }

        public int Precipitation { get; set; }

        [JsonProperty("weather")]
        public string State { get; set; }
    }
}
