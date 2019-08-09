namespace Metasite.Logic.Weather.Models
{
    public sealed class WeatherDto
    {
        public string City { get; set; }

        public decimal Temperature { get; set; }

        public int Precipitation { get; set; }
        
        public string State { get; set; }
    }
}
