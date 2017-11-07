using System;
using System.Collections.Generic;
using EDA.Poc.Infraestrutura.Messaging.Interfaces;

namespace EDA.Poc.Infraestrutura.Messaging.Handling
{
    public class ReceivedEvent : PersistedEvent
    {
        public List<DateTime> ReceiveDates { get; set; }

        public ReceivedEvent()
            : base()
        {
            ReceiveDates = new List<DateTime>
                                {
                                    DateTime.Now
                                };
        }

        public ReceivedEvent(Type eventHandlerType, IEvent @event)
            : base(eventHandlerType, @event)
        {
            ReceiveDates = new List<DateTime>
                                {
                                    DateTime.Now
                                };
        }

        public void AddNewReceiveDate()
        {
            ReceiveDates.Add(DateTime.Now);
        }
    }
}