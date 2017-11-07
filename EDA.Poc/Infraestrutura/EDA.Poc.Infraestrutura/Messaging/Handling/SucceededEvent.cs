using System;
using System.Collections.Generic;
using System.Linq;
using EDA.Poc.Infraestrutura.Exceptions;
using EDA.Poc.Infraestrutura.Messaging.Interfaces;

namespace EDA.Poc.Infraestrutura.Messaging.Handling
{
    public class SucceededEvent : PersistedEvent
    {
        public List<ExceptionInfo> PreviousFails { get; set; }

        public SucceededEvent()
            : base()
        {
            PreviousFails = new List<ExceptionInfo>();
        }

        public SucceededEvent(Type eventHandlerType, IEvent @event)
            : base(eventHandlerType, @event)
        {
            PreviousFails = new List<ExceptionInfo>();
        }

        public SucceededEvent(Type eventHandlerType, IEvent @event, IEnumerable<ExceptionInfo> previousFails)
            : base(eventHandlerType, @event)
        {
            PreviousFails = previousFails.ToList();
        }
    }
}
