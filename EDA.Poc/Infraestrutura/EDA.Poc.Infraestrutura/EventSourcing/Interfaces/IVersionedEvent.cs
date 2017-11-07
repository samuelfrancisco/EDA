using System;
using EDA.Poc.Infraestrutura.Messaging.Interfaces;

namespace EDA.Poc.Infraestrutura.EventSourcing.Interfaces
{
    public interface IVersionedEvent : IEvent
    {
        Guid SourceId { get; set; }
        long SourceVersion { get; set; }
        Guid? OriginalCorrelationId { get; set; }
        Guid? CorrelationId { get; set; }
    }
}