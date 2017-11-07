using System;
using System.Collections.Generic;

namespace EDA.Poc.Infraestrutura.EventSourcing.Interfaces
{
    public interface IEventSourcedFactory<T> where T : class, IEventSourced
    {
        T GetAggregate(Guid aggregateId, IMemento<T> memento, IEnumerable<IVersionedEvent> events);
    }
}
