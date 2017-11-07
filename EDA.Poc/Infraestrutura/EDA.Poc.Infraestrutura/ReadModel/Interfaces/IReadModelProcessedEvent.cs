using System;
using System.Collections.Generic;
using EDA.Poc.Infraestrutura.EventSourcing.Interfaces;

namespace EDA.Poc.Infraestrutura.ReadModel.Interfaces
{
    public interface IReadModelProcessedEvent
    {
        ReadModelProcessedEventIdentity Id { get; }
        Guid ReadModelIdentity { get; }
        Guid EventId { get; }
        DateTime EventUtcDate { get; }
        string ReadModelTypeFullName { get; }
        string EventTypeFullName { get; }
        IVersionedEvent EventPayload { get; }
        Dictionary<string, string> Metadata { get; }
    }
}