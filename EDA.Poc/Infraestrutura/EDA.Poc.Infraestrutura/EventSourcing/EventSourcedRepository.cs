using EDA.Poc.Infraestrutura.EventSourcing.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EDA.Poc.Infraestrutura.EventSourcing
{
    public class EventSourcedRepository<T> : IEventSourcedRepository<T> where T : class, IEventSourced
    {
        private const string BucketId = "Aggregates";
        private readonly IEventStore _eventStore;
        private readonly IEventSourcedFactory<T> _aggregatefactory;

        public EventSourcedRepository(IEventStore eventStore,
            IEventSourcedFactory<T> aggregatefactory)
        {
            _eventStore = eventStore;
            _aggregatefactory = aggregatefactory;
        }

        public Task<T> GetById(Guid id)
        {
            return GetById(id, BucketId);
        }

        public async Task<T> GetById(Guid id, string bucketId)
        {
            var streamId = bucketId + "_" + typeof(T).FullName + "_" + id;

            var snapshot = await GetSnapshot(streamId).ConfigureAwait(false);

            T aggregate;

            aggregate = await GetAggregate(streamId, id, snapshot).ConfigureAwait(false);

            return aggregate;
        }

        public Task Save(T aggregate, Guid commitId)
        {
            return Save(aggregate, commitId, BucketId);
        }

        public async Task Save(T aggregate, Guid commitId, string bucketId)
        {
            var streamId = bucketId + "_" + typeof(T).FullName + "_" + aggregate.Id;

            await _eventStore.Save(streamId, commitId, aggregate);

            await _eventStore.SaveSnapshot(streamId, commitId, aggregate);
        }

        private async Task<IMemento<T>> GetSnapshot(string streamId)
        {
            var snapshot = await _eventStore.GetTheSnapshotFromStream(streamId).ConfigureAwait(false);

            return snapshot as IMemento<T>;
        }

        private async Task<T> GetAggregate(string streamId, Guid aggregateId, IMemento<T> snapshot)
        {
            var startVersion = snapshot == null ? 1 : snapshot.Version + 1;

            var versionedEvents = await _eventStore.GetEventsById(streamId, startVersion).ConfigureAwait(false);

            if (!versionedEvents.Any() && snapshot == null) return default(T);            

            var aggregate = _aggregatefactory.GetAggregate(aggregateId, snapshot, versionedEvents);

            return aggregate;
        }        
    }
}
