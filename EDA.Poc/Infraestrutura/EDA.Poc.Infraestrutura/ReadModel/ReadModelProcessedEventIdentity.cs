using System;

namespace EDA.Poc.Infraestrutura.ReadModel
{
    public class ReadModelProcessedEventIdentity
    {
        public Guid ReadModelIdentity { get; set; }
        public Guid EventId { get; set; }

        private ReadModelProcessedEventIdentity()
        {
            
        }

        public ReadModelProcessedEventIdentity(Guid readModelIdentity, Guid eventId)
        {
            ReadModelIdentity = readModelIdentity;
            EventId = eventId;
        }
    }
}