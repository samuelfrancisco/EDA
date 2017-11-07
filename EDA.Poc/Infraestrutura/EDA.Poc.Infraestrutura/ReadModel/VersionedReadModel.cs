using System;
using System.Collections.Generic;
using System.Linq;
using EDA.Poc.Infraestrutura.ReadModel.Interfaces;

namespace EDA.Poc.Infraestrutura.ReadModel
{
    public abstract class VersionedReadModel : IVersionedReadModel
    {
        private readonly Dictionary<Type, Action<IReadModelProcessedEvent>> _handlers = new Dictionary<Type, Action<IReadModelProcessedEvent>>();
        private readonly Dictionary<Type, Action<IReadModelProcessedEvent>> _reverseHandlers = new Dictionary<Type, Action<IReadModelProcessedEvent>>();
        private readonly List<IReadModelProcessedEvent> _uncommittedEvents = new List<IReadModelProcessedEvent>();
        private readonly List<IReadModelProcessedEvent> _unprocessedEvents = new List<IReadModelProcessedEvent>();

        public VersionedReadModelIdentity Id
        {
            get
            {
                return new VersionedReadModelIdentity(Identity, Version);
            }
        }

        public Guid Identity { get; set; }
        public long Version { get; set; }
        public IEnumerable<IReadModelProcessedEvent> UncommittedEvents { get { return _uncommittedEvents.OrderBy(x => x.EventUtcDate).ToList(); } }

        protected VersionedReadModel()
        {
            Identity = Guid.NewGuid();
            Version = 0;

            SetHandlers();
            SetReverseHandlers();
        }

        public void SetUnprocessedEvents(IEnumerable<IReadModelProcessedEvent> unprocessedEvents)
        {
            _unprocessedEvents.AddRange(unprocessedEvents);
        }

        protected abstract void SetHandlers();

        protected abstract void SetReverseHandlers();

        protected void Handles<TEvent>(Action<TEvent> handler)
           where TEvent : IReadModelProcessedEvent
        {
            _handlers.Add(typeof(TEvent), @event => handler((TEvent)@event));
        }

        protected void HandlesReverse<TEvent>(Action<TEvent> handler)
           where TEvent : IReadModelProcessedEvent
        {
            _reverseHandlers.Add(typeof(TEvent), @event => handler((TEvent)@event));
        }

        protected void Update(IReadModelProcessedEvent @event)
        {
            var versionedEventEqualityComparer = new ReadModelProcessedEventEventEqualityComparer();

            if (_unprocessedEvents.All(x => !versionedEventEqualityComparer.Equals(x, @event)))
            {
                ReverseEvents();

                _uncommittedEvents.Add(@event);

                _unprocessedEvents.Add(@event);

                ProcessesEvents();
            }
        }

        private void ReverseEvents()
        {
            var orderedUnprocessedEvents = _unprocessedEvents.OrderByDescending(x => x.EventUtcDate).Distinct(new ReadModelProcessedEventEventEqualityComparer()).ToList();

            foreach (var @event in orderedUnprocessedEvents)
            {
                _reverseHandlers[@event.GetType()].Invoke(@event);
            }
        }

        protected void ProcessesEvents()
        {
            var orderedUnprocessedEvents = _unprocessedEvents.OrderBy(x => x.EventUtcDate).Distinct(new ReadModelProcessedEventEventEqualityComparer()).ToList();

            foreach (var @event in orderedUnprocessedEvents)
            {
                _handlers[@event.GetType()].Invoke(@event);
            }
        }
    }
}