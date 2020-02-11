using System;

namespace Nethereum.eShop.ApplicationCore.Exceptions
{
    public class QuoteNotFoundException : Exception
    {
        public QuoteNotFoundException(int quoteId) : base($"No quote found with id {quoteId}")
        {
        }

        protected QuoteNotFoundException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
        {
        }

        public QuoteNotFoundException(string message) : base(message)
        {
        }

        public QuoteNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
