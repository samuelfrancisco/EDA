using System;
using EDA.Poc.Infraestrutura.EventSourcing.Interfaces;

namespace EDA.Poc.Infraestrutura.EventSourcing
{
    public abstract class VersionedEvent : IVersionedEvent
    {
        public Guid Id { get; set; }
        public Guid SourceId { get; set; }
        public DateTime Date { get; set; }
        public Guid? OriginalCorrelationId { get; set; }
        public Guid? CorrelationId { get; set; }
        public long SourceVersion { get; set; }

        protected VersionedEvent()
        {
            Id = Guid.NewGuid();
            Date = DateTime.Now;
        }
    }
}
