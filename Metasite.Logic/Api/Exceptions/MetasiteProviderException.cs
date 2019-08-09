using System;

namespace Metasite.Logic.Api.Exceptions
{
    [Serializable]
    public class MetasiteProviderException : Exception
    {
        public MetasiteProviderException() { }
        public MetasiteProviderException(string message) : base(message) { }
        public MetasiteProviderException(string message, Exception inner) : base(message, inner) { }
        protected MetasiteProviderException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
