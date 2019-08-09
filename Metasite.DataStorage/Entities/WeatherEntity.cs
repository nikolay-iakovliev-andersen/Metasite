namespace Metasite.DataStorage.Entities
{
    public sealed class WeatherEntity
    {
        public int Id { get; set; }

        public string City { get; set; }

        public decimal Temperature { get; set; }

        public int Precipitation { get; set; }

        public string State { get; set; }
    }
}
