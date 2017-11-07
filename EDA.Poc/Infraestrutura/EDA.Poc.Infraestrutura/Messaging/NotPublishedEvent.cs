using System;
using EDA.Poc.Infraestrutura.Exceptions;
using EDA.Poc.Infraestrutura.Messaging.Interfaces;

namespace EDA.Poc.Infraestrutura.Messaging
{
    public class NotPublishedEvent : INotPublishedEvent
    {
        public IEvent Event { get; private set; }
        public ExceptionInfo ExceptionInfo { get; private set; }

        public NotPublishedEvent(IEvent @event, Exception exception)
        {
            Event = @event;
            ExceptionInfo = new ExceptionInfo(exception);
        }
    }
}