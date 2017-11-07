using System;
using System.Runtime.Serialization;

namespace EDA.Poc.Infraestrutura.ReadModel.Exceptions
{
    [Serializable]
    public class ReadModelConcurrencyException : Exception
    {
        public ReadModelConcurrencyException()
        {
        }

        public ReadModelConcurrencyException(string message)
            : base(message)
        {
        }

        public ReadModelConcurrencyException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected ReadModelConcurrencyException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}