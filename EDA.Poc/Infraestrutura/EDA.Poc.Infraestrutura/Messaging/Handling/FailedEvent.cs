using System;
using System.Collections.Generic;
using EDA.Poc.Infraestrutura.Exceptions;
using EDA.Poc.Infraestrutura.Messaging.Interfaces;

namespace EDA.Poc.Infraestrutura.Messaging.Handling
{
    public class FailedEvent : PersistedEvent
    {
        public List<ExceptionInfo> Fails { get; set; }

        public FailedEvent()
            : base()
        {
            Fails = new List<ExceptionInfo>();
        }

        public FailedEvent(Type eventHandlerType, IEvent @event, Exception exception)
            : base(eventHandlerType, @event)
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