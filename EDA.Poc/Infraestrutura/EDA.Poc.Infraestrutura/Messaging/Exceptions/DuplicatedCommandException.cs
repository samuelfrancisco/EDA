using System;
using System.Runtime.Serialization;

namespace EDA.Poc.Infraestrutura.Messaging.Exceptions
{
    [Serializable]
    public class DuplicatedCommandException : Exception
    {
        public DuplicatedCommandException()
            : base(string.Format("The command had alread been processed successfully"))
        {
        }

        public DuplicatedCommandException(string message) : base(message)
        {
        }

        public DuplicatedCommandException(string message, Exception inner) : base(message, inner)
        {
        }

        protected DuplicatedCommandException(
            SerializationInfo info,
            StreamingContext context) : base(info, context)
        {
        }
    }
}