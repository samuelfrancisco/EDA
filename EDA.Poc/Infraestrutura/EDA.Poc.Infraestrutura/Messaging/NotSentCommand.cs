using System;
using EDA.Poc.Infraestrutura.Exceptions;
using EDA.Poc.Infraestrutura.Messaging.Interfaces;

namespace EDA.Poc.Infraestrutura.Messaging
{
    public class NotSentCommand : INotSentCommand
    {
        public ICommand Command { get; private set; }
        public ExceptionInfo ExceptionInfo { get; private set; }

        public NotSentCommand(ICommand command, Exception exception)
        {
            Command = command;
            ExceptionInfo = new ExceptionInfo(exception);
        }
    }
}