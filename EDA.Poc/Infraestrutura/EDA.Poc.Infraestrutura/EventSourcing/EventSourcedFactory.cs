using System;
using System.Collections.Generic;
using System.Reflection;
using EDA.Poc.Infraestrutura.EventSourcing.Interfaces;

namespace EDA.Poc.Infraestrutura.EventSourcing
{
    public class EventSourcedFactory<T> : IEventSourcedFactory<T> where T : class, IEventSourced
    {
        private readonly Func<Guid, IMemento<T>, IEnumerable<IVersionedEvent>, T> _factoryComMemento;

        private readonly Func<Guid, IEnumerable<IVersionedEvent>, T> _factorySemMemento;

        public EventSourcedFactory()
        {
            var construtorComMemento = typeof(T).GetConstructor(BindingFlags.Public | BindingFlags.Instance, null, new[] { typeof(Guid), typeof(IMemento<T>), typeof(IEnumerable<IVersionedEvent>) }, null);

            if (construtorComMemento == null)
                throw new InvalidCastException("Type T must have a constructor with the following signature: .ctor(Guid, IMemento<T>, IEnumerable<IVersionedEvent>)");

            _factoryComMemento = (aggregateId, memento, events) => (T)construtorComMemento.Invoke(new object[] { aggregateId, memento, events });

            var construtorSemMemento = typeof(T).GetConstructor(BindingFlags.Public | BindingFlags.Instance, null, new[] { typeof(Guid), typeof(IEnumerable<IVersionedEvent>) }, null);

            if (construtorSemMemento == null)
                throw new InvalidCastException("Type T must have a constructor with the following signature: .ctor(Guid, IEnumerable<IVersionedEvent>)");

            _factorySemMemento = (aggregateId, events) => (T)construtorSemMemento.Invoke(new object[] { aggregateId, events });
        }

        public T GetAggregate(Guid aggregateId, IMemento<T> memento, IEnumerable<IVersionedEvent> events)
        {
            var aggregate = memento != null
                                ? _factoryComMemento(aggregateId, memento, events)
                                : _factorySemMemento(aggregateId, events);

            return aggregate;
        }
    }
}