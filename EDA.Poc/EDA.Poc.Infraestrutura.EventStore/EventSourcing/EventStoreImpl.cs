using EDA.Poc.Infraestrutura.EventSourcing.Interfaces;
using EventStore.ClientAPI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDA.Poc.Infraestrutura.EventStore.EventSourcing
{
    public class EventStoreImpl : IEventStore
    {
        private readonly IEventStoreConnection _eventStoreConnection;

        private readonly JsonSerializerSettings _serializerSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.None
        };

        private const int ReadPageSize = 100;
        private const int WritePageSize = 100;

        public EventStoreImpl(IEventStoreConnection eventStoreConnection)
        {
            _eventStoreConnection = eventStoreConnection;
        }

        public Task<IEnumerable<IVersionedEvent>> GetEventsById(string streamId)
        {
            return GetEventsById(streamId, 1, int.MaxValue);
        }

        public Task<IEnumerable<IVersionedEvent>> GetEventsById(string streamId, long startVersion)
        {
            return GetEventsById(streamId, startVersion, int.MaxValue);
        }

        public async Task<IEnumerable<IVersionedEvent>> GetEventsById(string streamId, long startVersion, long endVersion)
        {
            if (endVersion <= 0)
                throw new InvalidOperationException("Cannot get version <= 0");

            long sliceStart = startVersion;

            var events = new List<IVersionedEvent>();

            StreamEventsSlice currentSlice;

            do
            {
                var sliceCount = sliceStart + ReadPageSize <= endVersion
                                    ? ReadPageSize
                                    : endVersion - sliceStart + 1;

                currentSlice = await _eventStoreConnection.ReadStreamEventsForwardAsync(streamId, sliceStart, (int)sliceCount, false).ConfigureAwait(false);

                if (currentSlice.Status == SliceReadStatus.StreamNotFound)
                    throw new InvalidOperationException(string.Format("Stream {0} not found.", streamId));

                if (currentSlice.Status == SliceReadStatus.StreamDeleted)
                    throw new InvalidOperationException(string.Format("Stream {0} deleted.", streamId));

                sliceStart = currentSlice.NextEventNumber;

                events.AddRange(currentSlice.Events.Select(x => DeserializeEvent(x.OriginalEvent)).Cast<IVersionedEvent>());
                
            } while (endVersion >= currentSlice.NextEventNumber && !currentSlice.IsEndOfStream);            

            return events;
        }

        public async Task<IMemento> GetTheSnapshotFromStream(string streamId)
        {
            var currentSlice = await _eventStoreConnection.ReadStreamEventsForwardAsync(streamId, 0, 1, false).ConfigureAwait(false);

            if (!currentSlice.Events.Any())
                return null;

            var @event = currentSlice.Events.First();

            return (IMemento)DeserializeEvent(@event.OriginalEvent);
        }

        public async Task Save(string streamId, Guid commitId, IEventSourced aggregate)
        {
            var commitHeaders = new Dictionary<string, object>
            {
                { "CommitId", commitId },
                { "AggregateAssemblyName", aggregate.GetType().Assembly.FullName },
                { "AggregateNamespace", aggregate.GetType().Namespace },
                { "AggregateFullName", aggregate.GetType().FullName },
                { "AggregateTypeName", aggregate.GetType().Name },
                { "AggregateClrTypeName", aggregate.GetType().AssemblyQualifiedName },
                { "AggregateVersion", aggregate.Version }
            };

            var newEvents = aggregate.UncommittedEvents.ToList();

            var originalVersion = aggregate.Version - newEvents.Count;

            var expectedVersion = originalVersion == 0 ? ExpectedVersion.NoStream : originalVersion;

            var eventsToSave = newEvents.Select(e => ToEventData(e.Id, e, commitHeaders)).ToList();

            if (eventsToSave.Count < WritePageSize)
            {
                await _eventStoreConnection.AppendToStreamAsync(streamId, expectedVersion, eventsToSave).ConfigureAwait(false);
            }
            else
            {
                var transaction = await _eventStoreConnection.StartTransactionAsync(streamId, expectedVersion).ConfigureAwait(false);

                var position = 0;

                while (position < eventsToSave.Count)
                {
                    var pageEvents = eventsToSave.Skip(position).Take(WritePageSize);

                    await transaction.WriteAsync(pageEvents);

                    position += WritePageSize;
                }

                await transaction.CommitAsync();
            }
        }

        public async Task SaveSnapshot(string streamId, Guid commitId, IEventSourced aggregate)
        {
            var snapshotStreamId = streamId + "_Snapshot";

            var snapshot = aggregate.GetMemento();

            var newEvents = aggregate.UncommittedEvents.ToList();

            var originalVersion = aggregate.Version - newEvents.Count;

            var expectedVersion = originalVersion == 0 ? ExpectedVersion.NoStream : originalVersion;

            var commitHeaders = new Dictionary<string, object>
            {
                { "CommitId", commitId },
                { "AggregateAssemblyName", aggregate.GetType().Assembly.FullName },
                { "AggregateNamespace", aggregate.GetType().Namespace },
                { "AggregateFullName", aggregate.GetType().FullName },
                { "AggregateTypeName", aggregate.GetType().Name },
                { "AggregateClrTypeName", aggregate.GetType().AssemblyQualifiedName },
                { "AggregateVersion", aggregate.Version }
            };

            var eventsToSave = ToEventData(Guid.NewGuid(), snapshot, commitHeaders);

            await _eventStoreConnection.AppendToStreamAsync(snapshotStreamId, expectedVersion, eventsToSave).ConfigureAwait(false);
        }

        private EventData ToEventData(Guid eventId, object @event, IDictionary<string, object> headers)
        {
            var data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(@event, _serializerSettings));

            var eventHeaders = new Dictionary<string, object>(headers)
            {
                { "EventId", eventId },
                { "EventAssemblyName", @event.GetType().Assembly.FullName },
                { "EventNamespace", @event.GetType().Namespace },
                { "EventFullName", @event.GetType().FullName },
                { "EventTypeName", @event.GetType().Name },
                { "EventClrTypeName", @event.GetType().AssemblyQualifiedName },
                { "AggregateVersion", ((IVersionedEvent)@event).SourceVersion }
            };

            var metadata = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(eventHeaders, _serializerSettings));

            var typeName = @event.GetType().Name;

            return new EventData(eventId, typeName, true, data, metadata);
        }

        private object DeserializeEvent(RecordedEvent originalEvent)
        {
            var eventClrTypeName = JObject.Parse(Encoding.UTF8.GetString(originalEvent.Metadata)).Property("EventClrTypeName").Value;

            return (IVersionedEvent)JsonConvert.DeserializeObject(Encoding.UTF8.GetString(originalEvent.Data), Type.GetType((string)eventClrTypeName));
        }        
    }
}
