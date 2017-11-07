using System;
using System.Collections.Generic;
using EDA.Poc.Infraestrutura.Messaging.Interfaces;

namespace EDA.Poc.Infraestrutura.Messaging.Handling
{
    public class ReceivedCommand : PersistedCommand
    {
        public List<DateTime> ReceiveDates { get; set; }

        public ReceivedCommand()
            : base()
        {
            ReceiveDates = new List<DateTime>
                                {
                                    DateTime.Now
                                };
        }

        public ReceivedCommand(Type commandHandlerType, ICommand command)
            : base(commandHandlerType, command)
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