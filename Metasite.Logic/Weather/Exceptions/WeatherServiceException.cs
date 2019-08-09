using System;

namespace Metasite.Logic.Weather.Exceptions
{
    [Serializable]
    public class WeatherServiceException : Exception
    {
        public WeatherServiceException() { }
        public WeatherServiceException(string message) : base(message) { }
        public WeatherServiceException(string message, Exception inner) : base(message, inner) { }
        protected WeatherServiceException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
