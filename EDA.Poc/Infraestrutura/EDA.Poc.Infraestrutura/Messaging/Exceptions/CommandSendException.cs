using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using EDA.Poc.Infraestrutura.Messaging.Interfaces;

namespace EDA.Poc.Infraestrutura.Messaging.Exceptions
{
    [Serializable]
    public class CommandSendException : Exception
    {
        private readonly List<INotSentCommand> _notSentCommands;

        public CommandSendException()
        {
            _notSentCommands = new List<INotSentCommand>();
        }

        public CommandSendException(string message)
            : base(message)
        {
            _notSentCommands = new List<INotSentCommand>();
        }

        public CommandSendException(string message, Exception inner)
            : base(message, inner)
        {
            _notSentCommands = new List<INotSentCommand>();
        }

        protected CommandSendException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            _notSentCommands = new List<INotSentCommand>();
        }

        public IEnumerable<INotSentCommand> NotSentCommands
        {
            get { return _notSentCommands; }
        }

        public void AddNotSentCommand(INotSentCommand notSentCommand)
        {
            if (notSentCommand == null) return;

            _notSentCommands.Add(notSentCommand);
        }
    }
}