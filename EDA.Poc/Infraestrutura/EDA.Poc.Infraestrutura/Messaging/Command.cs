using System;
using EDA.Poc.Infraestrutura.Messaging.Interfaces;

namespace EDA.Poc.Infraestrutura.Messaging
{
    public class Command : ICommand
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public Guid? OriginalCorrelationId { get; set; }
        public Guid? CorrelationId { get; set; }
        public TimeSpan Delay { get; set; }
        public TimeSpan TimeToLive { get; set; }

        public Command()
        {
            Id = Guid.NewGuid();
            Date = DateTime.Now;
        }
    }
}
