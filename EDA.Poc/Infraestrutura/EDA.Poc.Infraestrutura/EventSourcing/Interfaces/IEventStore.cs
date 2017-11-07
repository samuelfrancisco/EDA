using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EDA.Poc.Infraestrutura.EventSourcing.Interfaces
{
    public interface IEventStore
    {
        Task<IEnumerable<IVersionedEvent>> GetEventsById(string streamId);
        Task<IEnumerable<IVersionedEvent>> GetEventsById(string streamId, long startVersion);
        Task<IEnumerable<IVersionedEvent>> GetEventsById(string streamId, long startVersion, long endVersion);
        Task<IMemento> GetTheSnapshotFromStream(string streamId);
        Task Save(string streamId, Guid commitId, IEventSourced aggregate);
        Task SaveSnapshot(string streamId, Guid commitId, IEventSourced aggregate);
    }
}
