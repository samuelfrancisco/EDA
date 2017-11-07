using System;
using System.Collections.Generic;

namespace EDA.Poc.Infraestrutura.EventSourcing.Interfaces
{
    public interface IEventSourced
    {
        Guid Id { get; }
        long Version { get; }        
        IEnumerable<IVersionedEvent> UncommittedEvents { get; }

        IMemento<IEventSourced> GetMemento();
    }
}
