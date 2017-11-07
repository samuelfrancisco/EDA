using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using EDA.Poc.Infraestrutura.Messaging.Interfaces;

namespace EDA.Poc.Infraestrutura.Messaging.Exceptions
{
    [Serializable]
    public class EventPublishException : Exception
    {
        private readonly List<INotPublishedEvent> _notPublishedEvents;

        public EventPublishException()
        {
            _notPublishedEvents = new List<INotPublishedEvent>();
        }

        public EventPublishException(string message)
            : base(message)
        {
            _notPublishedEvents = new List<INotPublishedEvent>();
        }

        public EventPublishException(string message, Exception inner)
            : base(message, inner)
        {
            _notPublishedEvents = new List<INotPublishedEvent>();
        }

        protected EventPublishException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            _notPublishedEvents = new List<INotPublishedEvent>();
        }

        public IEnumerable<INotPublishedEvent> NotPublishedEvents
        {
            get { return _notPublishedEvents; }
        }

        public void AddNotPublishedEvent(INotPublishedEvent notPublishedEvent)
        {
            if(notPublishedEvent == null) return;

            _notPublishedEvents.Add(notPublishedEvent);
        }
    }
}