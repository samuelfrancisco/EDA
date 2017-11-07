using System;
using System.Collections.Generic;
using EDA.Poc.Infraestrutura.Exceptions;
using EDA.Poc.Infraestrutura.Messaging.Interfaces;

namespace EDA.Poc.Infraestrutura.Messaging.Handling
{
    public class FailedCommand : PersistedCommand
    {
        public List<ExceptionInfo> Fails { get; set; }

        public FailedCommand()
            : base()
        {
            Fails = new List<ExceptionInfo>();
        }

        public FailedCommand(Type commandHandlerType, ICommand command, Exception exception)
            : base(commandHandlerType, command)
        {
            Fails = new List<ExceptionInfo>
                        {
                            new ExceptionInfo(exception)
                        };
        }

        public void AddNewFail(Exception exception)
        {
            Fails.Add(new ExceptionInfo(exception));
        }
    }
}