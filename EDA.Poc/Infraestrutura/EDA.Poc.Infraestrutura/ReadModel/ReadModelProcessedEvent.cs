using System;
using System.Collections.Generic;
using EDA.Poc.Infraestrutura.EventSourcing.Interfaces;
using EDA.Poc.Infraestrutura.ReadModel.Interfaces;

namespace EDA.Poc.Infraestrutura.ReadModel
{
    public class ReadModelProcessedEvent : IReadModelProcessedEvent
    {
        public ReadModelProcessedEventIdentity Id
        {
            get
            {
                return new ReadModelProcessedEventIdentity(ReadModelIdentity, EventId);
            }
        }

        public Guid ReadModelIdentity { get; private set; }
        public Guid EventId { get; private set; }
        public DateTime EventUtcDate { get; set; }
        public string ReadModelTypeFullName { get; set; }
        public string EventTypeFullName { get; set; }
        public IVersionedEvent EventPayload { get; private set; }
        public Dictionary<string, string> Metadata { get; set; }

        protected ReadModelProcessedEvent()
        {
            Metadata = new Dictionary<string, string>();
        }

        protected ReadModelProcessedEvent(IVersionedReadModel readModel, IVersionedEvent @event)
            : this()
        {
            ReadModelIdentity = readModel.Identity;
            EventId = @event.Id;
            EventUtcDate = @event.Date.ToUniversalTime();
            ReadModelTypeFullName = readModel.GetType().FullName;
            EventTypeFullName = @event.GetType().FullName;
            EventPayload = @event;

            SetMetadata(readModel, @event);
        }

        private void SetMetadata(IVersionedReadModel readModel, IVersionedEvent @event)
        {
            var readModelType = readModel.GetType();
            var eventType = @event.GetType();

            Metadata.Add("ReadModelTypeFullName", readModelType.Name);
            Metadata.Add("ReadModelTypeName", readModelType.Name);
            Metadata.Add("ReadModelAssemblyFullName", readModelType.Assembly.FullName);
            Metadata.Add("ReadModelNamespace", readModelType.Namespace);
            Metadata.Add("EventTypeFullName", eventType.FullName);
            Metadata.Add("EventTypeName", eventType.Name);
            Metadata.Add("EventAssemblyFullName", eventType.Assembly.FullName);
            Metadata.Add("EventNamespace", eventType.Namespace);
        }
    }
}