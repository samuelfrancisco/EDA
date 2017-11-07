using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EDA.Poc.Infraestrutura.Messaging.Handling;
using EDA.Poc.Infraestrutura.Messaging.Handling.Interfaces;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;

namespace EDA.Poc.Infraestrutura.MongoDb.Messaging.Handling
{
    public class EventHandlerRepository : IEventHandlerRepository
    {
        private readonly MongoClient _mongoClient;
        private const string DatabaseName = "MarketPlaceEventHandlerStore";

        public EventHandlerRepository(MongoClient mongoClient)
        {
            _mongoClient = mongoClient;
        }

        public Task<ReceivedEvent> GetReceivedEventById(Type eventHandlerType, Guid eventId)
        {
            return Task.Run(() =>
                                {
                                    var server = _mongoClient.GetServer();

                                    var database = server.GetDatabase(DatabaseName);

                                    var collection = database.GetCollection<ReceivedEvent>(string.Format("{0}_ReceivedEvents", eventHandlerType.Name));

                                    return collection.AsQueryable().FirstOrDefault(x => x.Id == eventId);
                                });
        }

        public Task<SucceededEvent> GetSucceededEventById(Type eventHandlerType, Guid eventId)
        {
            return Task.Run(() =>
                                {
                                    var server = _mongoClient.GetServer();

                                    var database = server.GetDatabase(DatabaseName);

                                    var collection = database.GetCollection<SucceededEvent>(string.Format("{0}_SucceededEvents", eventHandlerType.Name));

                                    return collection.AsQueryable().FirstOrDefault(x => x.Id == eventId);
                                });
        }

        public Task<FailedEvent> GetFailedEventById(Type eventHandlerType, Guid eventId)
        {
            return Task.Run(() =>
                                {
                                    var server = _mongoClient.GetServer();

                                    var database = server.GetDatabase(DatabaseName);

                                    var collection = database.GetCollection<FailedEvent>(string.Format("{0}_FailedEvents", eventHandlerType.Name));

                                    return collection.AsQueryable().FirstOrDefault(x => x.Id == eventId);
                                });
        }

        public Task<List<ReceivedEvent>> GetAllReceivedEventsWhere(Type eventHandlerType, Expression<Func<ReceivedEvent, bool>> expression)
        {
            return Task.Run(() =>
                                {
                                    var server = _mongoClient.GetServer();

                                    var database = server.GetDatabase(DatabaseName);

                                    var collection = database.GetCollection<ReceivedEvent>(string.Format("{0}_ReceivedEvents", eventHandlerType.Name));

                                    return collection.AsQueryable().Where(expression).ToList();
                                });
        }

        public Task<List<SucceededEvent>> GetAllSucceededEventsWhere(Type eventHandlerType, Expression<Func<SucceededEvent, bool>> expression)
        {
            return Task.Run(() =>
                                {
                                    var server = _mongoClient.GetServer();

                                    var database = server.GetDatabase(DatabaseName);

                                    var collection = database.GetCollection<SucceededEvent>(string.Format("{0}_SucceededEvents", eventHandlerType.Name));

                                    return collection.AsQueryable().Where(expression).ToList();
                                });
        }

        public Task<List<FailedEvent>> GetAllFailedEventsWhere(Type eventHandlerType, Expression<Func<FailedEvent, bool>> expression)
        {
            return Task.Run(() =>
                                {
                                    var server = _mongoClient.GetServer();

                                    var database = server.GetDatabase(DatabaseName);

                                    var collection = database.GetCollection<FailedEvent>(string.Format("{0}_FailedEvents", eventHandlerType.Name));

                                    return collection.AsQueryable().Where(expression).ToList();
                                });
        }

        public Task SaveReceivedEvent(Type eventHandlerType, ReceivedEvent receivedEvent)
        {
            return Task.Run(() =>
                                {
                                    var server = _mongoClient.GetServer();

                                    var database = server.GetDatabase(DatabaseName);

                                    var collection = database.GetCollection<ReceivedEvent>(string.Format("{0}_ReceivedEvents", eventHandlerType.Name));

                                    collection.Save(receivedEvent);
                                });
        }

        public Task SaveSucceededEvent(Type eventHandlerType, SucceededEvent succeededEvent)
        {
            return Task.Run(() =>
                                {
                                    var server = _mongoClient.GetServer();

                                    var database = server.GetDatabase(DatabaseName);

                                    var collection = database.GetCollection<SucceededEvent>(string.Format("{0}_SucceededEvents", eventHandlerType.Name));

                                    collection.Save(succeededEvent);
                                });
        }

        public Task SaveFailedEvent(Type eventHandlerType, FailedEvent failedEvent)
        {
            return Task.Run(() =>
                                {
                                    var server = _mongoClient.GetServer();

                                    var database = server.GetDatabase(DatabaseName);

                                    var collection = database.GetCollection<FailedEvent>(string.Format("{0}_FailedEvents", eventHandlerType.Name));

                                    collection.Save(failedEvent);
                                });
        }

        public Task RemoveFailedEvent(Type eventHandlerType, FailedEvent failedEvent)
        {
            return Task.Run(() =>
                                {
                                    var server = _mongoClient.GetServer();

                                    var database = server.GetDatabase(DatabaseName);

                                    var collection = database.GetCollection<FailedEvent>(string.Format("{0}_FailedEvents", eventHandlerType.Name));

                                    collection.Remove(Query<FailedEvent>.EQ(x => x.Id, failedEvent.Id));
                                });
        }
    }
}
