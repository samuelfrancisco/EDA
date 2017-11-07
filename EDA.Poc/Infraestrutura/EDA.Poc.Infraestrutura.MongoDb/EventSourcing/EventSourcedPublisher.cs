using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using EDA.Poc.Infraestrutura.EventSourcing;
using EDA.Poc.Infraestrutura.EventSourcing.Interfaces;
using EDA.Poc.Infraestrutura.Messaging.Exceptions;
using EDA.Poc.Infraestrutura.Messaging.Interfaces;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;

namespace EDA.Poc.Infraestrutura.MongoDb.EventSourcing
{
    public class EventSourcedPublisher<T> : IEventSourcedPublisher<T> where T : EventSourced
    {
        private const string MongoConcurrencyExceptionCode = "E1100";
        private const string DatabaseName = "MarketPlaceUnpublishedEventStore";
        private readonly MongoClient _mongoClient;
        private readonly IEventBus _eventBus;
        private readonly string _unpublishedEventsCollectionName = string.Format("{0}_UnpublishedEvents", typeof(T).Name);
        private readonly string _publishingEventsCollectionName = string.Format("{0}_PublishingEvents", typeof(T).Name);

        public EventSourcedPublisher(MongoClient mongoClient, IEventBus eventBus)
        {
            _mongoClient = mongoClient;
            _eventBus = eventBus;
        }

        public Task SaveUnpublishedEvents(T aggregate)
        {
            return Task.Run(() =>
                                {
                                    var unpublishedEvents = aggregate.UncommittedEvents.ToList();

                                    var server = _mongoClient.GetServer();

                                    var database = server.GetDatabase(DatabaseName);

                                    var collection = database.GetCollection<VersionedEvent>(_unpublishedEventsCollectionName);

                                    collection.InsertBatch(unpublishedEvents);
                                });
        }

        public Task PublishUnpublishedEvents(T aggregate)
        {
            return Task.Run(() =>
                                {
                                    var server = _mongoClient.GetServer();

                                    var database = server.GetDatabase(DatabaseName);

                                    var collection = database.GetCollection<VersionedEvent>(_unpublishedEventsCollectionName);

                                    var publishingCollection = database.GetCollection<VersionedEvent>(_publishingEventsCollectionName, new MongoCollectionSettings {WriteConcern = WriteConcern.Acknowledged});

                                    var unpublishedEvents = collection.AsQueryable().OrderBy(x => x.SourceVersion).Where(x => x.SourceId == aggregate.Id).ToList();

                                    if (!unpublishedEvents.Any()) return;

                                    foreach (var @event in unpublishedEvents)
                                    {
                                        try
                                        {
                                            publishingCollection.Insert(@event, WriteConcern.Acknowledged);

                                            var type = @event.GetType();

                                            var method = _eventBus.GetType().GetMethod("PublishOne", BindingFlags.Public | BindingFlags.Instance);

                                            var genericMethod = method.MakeGenericMethod(type);

                                            genericMethod.Invoke(_eventBus, new object[] {@event});

                                            collection.Remove(Query<VersionedEvent>.EQ(x => x.Id, @event.Id));
                                        }
                                        catch (EventPublishException)
                                        {
                                            publishingCollection.Remove(Query<VersionedEvent>.EQ(x => x.Id, @event.Id));

                                            throw;
                                        }
                                        catch (MongoException ex)
                                        {
                                            if (ex.Message.Contains(MongoConcurrencyExceptionCode))
                                            {
                                                continue;
                                            }

                                            throw;
                                        }
                                    }
                                });
        }

        public Task RemoveUnpublishedEvents(T aggregate)
        {
            return Task.Run(() =>
                                {
                                    var unpublishedEvents = aggregate.UncommittedEvents.ToList();

                                    var server = _mongoClient.GetServer();

                                    var database = server.GetDatabase(DatabaseName);

                                    var collection = database.GetCollection<VersionedEvent>(_unpublishedEventsCollectionName);

                                    foreach (var @event in unpublishedEvents)
                                    {
                                        collection.Remove(Query<VersionedEvent>.EQ(x => x.Id, @event.Id));
                                    }
                                });
        }
    }
}