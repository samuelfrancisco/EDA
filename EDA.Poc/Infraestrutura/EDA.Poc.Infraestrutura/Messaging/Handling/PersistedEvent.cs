using System;
using System.Collections.Generic;
using EDA.Poc.Infraestrutura.Messaging.Interfaces;

namespace EDA.Poc.Infraestrutura.Messaging.Handling
{
    public abstract class PersistedEvent
    {
        protected PersistedEvent()
        {            
            Metadata = new Dictionary<string, string>();
        }

        protected PersistedEvent(Type eventHandlerType, IEvent @event)
            : this()
        {
            Id = @event.Id;
            EventHandlerTypeFullName = eventHandlerType.Name;
            EventTypeFullName = @event.GetType().FullName;            
            EventDate = @event.Date;
            Payload = @event;

            SetMetadata(eventHandlerType, @event);
        }

        public Guid Id { get; set; }
        public string EventHandlerTypeFullName { get; set; }
        public string EventTypeFullName { get; set; }        
        public DateTime EventDate { get; set; }
        public IEvent Payload { get; set; }
        public Dictionary<string, string> Metadata { get; set; }

        private void SetMetadata(Type eventHandlerType, IEvent @event)
        {
            var eventType = @event.GetType();

            Metadata.Add("EventHandlerTypeFullName", eventHandlerType.Name);
            Metadata.Add("EventHandlerTypeName", eventHandlerType.Name);
            Metadata.Add("EventHandlerAssemblyFullName", eventHandlerType.Assembly.FullName);
            Metadata.Add("EventHandlerNamespace", eventHandlerType.Namespace);
            Metadata.Add("EventTypeFullName", eventType.FullName);
            Metadata.Add("EventTypeName", eventType.Name);
            Metadata.Add("EventAssemblyFullName", eventType.Assembly.FullName);
            Metadata.Add("EventNamespace", eventType.Namespace);
        }
    }
}