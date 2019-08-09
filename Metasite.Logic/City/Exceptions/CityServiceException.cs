using System;

namespace Metasite.Logic.City.Exceptions
{
    [Serializable]
    public class CityServiceException : Exception
    {
        public CityServiceException() { }
        public CityServiceException(string message) : base(message) { }
        public CityServiceException(string message, Exception inner) : base(message, inner) { }
        protected CityServiceException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
