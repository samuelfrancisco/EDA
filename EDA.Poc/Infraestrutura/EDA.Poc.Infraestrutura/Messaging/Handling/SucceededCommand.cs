using System;
using System.Collections.Generic;
using System.Linq;
using EDA.Poc.Infraestrutura.Exceptions;
using EDA.Poc.Infraestrutura.Messaging.Interfaces;

namespace EDA.Poc.Infraestrutura.Messaging.Handling
{
    public class SucceededCommand : PersistedCommand
    {
        public List<ExceptionInfo> PreviousFails { get; set; }

        public SucceededCommand()
            : base()
        {
            PreviousFails = new List<ExceptionInfo>();
        }

        public SucceededCommand(Type commandHandlerType, ICommand command)
            : base(commandHandlerType, command)
        {
            PreviousFails = new List<ExceptionInfo>();
        }

        public SucceededCommand(Type commandHandlerType, ICommand command, IEnumerable<ExceptionInfo> previousFails)
            : base(commandHandlerType, command)
        {
            PreviousFails = previousFails.ToList();
        }
    }
}