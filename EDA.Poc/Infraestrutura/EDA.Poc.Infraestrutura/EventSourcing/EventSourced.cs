using System;
using System.Collections.Generic;
using EDA.Poc.Infraestrutura.EventSourcing.Interfaces;

namespace EDA.Poc.Infraestrutura.EventSourcing
{
    public abstract class EventSourced : IEventSourced
    {
        private readonly Dictionary<Type, Action<IVersionedEvent>> _handlers = new Dictionary<Type, Action<IVersionedEvent>>();
        private readonly List<IVersionedEvent> _uncommittedEvents = new List<IVersionedEvent>();

        public Guid Id { get; protected set; }
        public long Version { get; protected set; }        
        public IEnumerable<IVersionedEvent> UncommittedEvents { get { return _uncommittedEvents; } }

        protected EventSourced()
        {
            Version = 0;            

            SetHandlers();
        }

        protected EventSourced(Guid id)
            : this()
        {
            Id = id;
        }        

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;

            var aggregateRoot = obj as EventSourced;

            if (aggregateRoot == null) return false;

            return Id == aggregateRoot.Id && GetType() == obj.GetType();
        }

        public static bool operator ==(EventSourced a1, EventSourced a2)
        {
            var obj1 = a1 as object;

            var obj2 = a2 as object;

            if (obj1 == null && obj2 == null) return true;

            if (obj1 == null || obj2 == null) return false;

            return a1.Equals(a2);
        }

        public static bool operator !=(EventSourced a1, EventSourced a2)
        {
            return !(a1 == a2);
        }

        public abstract IMemento<IEventSourced> GetMemento();

        protected abstract void SetHandlers();

        protected void Handles<TEvent>(Action<TEvent> handler)
           where TEvent : IVersionedEvent
        {
            _handlers.Add(typeof(TEvent), @event => handler((TEvent)@event));
        }

        protected void LoadFromHistory(IEnumerable<IVersionedEvent> pastEvents)
        {
            foreach (var e in pastEvents)
            {
                Handle(e);
            }
        }

        protected void Update(VersionedEvent @event)
        {
            @event.SourceId = Id;
            @event.SourceVersion = Version + 1;

            Handle(@event);

            _uncommittedEvents.Add(@event);
        }

        private void Handle(IVersionedEvent @event)
        {
            _handlers[@event.GetType()].Invoke(@event);

            Version = @event.SourceVersion;
        }
    }
}
