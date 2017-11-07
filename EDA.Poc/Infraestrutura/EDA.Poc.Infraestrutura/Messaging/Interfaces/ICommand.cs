using System;

namespace EDA.Poc.Infraestrutura.Messaging.Interfaces
{
    public interface ICommand
    {
        Guid Id { get; set; }
        DateTime Date { get; set; }
        Guid? OriginalCorrelationId { get; set; }
        Guid? CorrelationId { get; set; }
        TimeSpan Delay { get; set; }
        TimeSpan TimeToLive { get; set; }
    }
}
