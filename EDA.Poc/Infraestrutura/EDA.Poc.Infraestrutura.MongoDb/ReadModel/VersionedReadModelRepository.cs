using System;
using System.Linq;
using System.Threading.Tasks;
using EDA.Poc.Infraestrutura.ReadModel;
using EDA.Poc.Infraestrutura.ReadModel.Exceptions;
using EDA.Poc.Infraestrutura.ReadModel.Interfaces;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace EDA.Poc.Infraestrutura.MongoDb.ReadModel
{
    public class VersionedReadModelRepository<T> : IVersionedReadModelRepository<T> where T : VersionedReadModel
    {
        private const string MongoConcurrencyExceptionCode = "E1100";
        private const string Database = "MarketPlaceReadModelStore";
        private readonly string _readModelCollectionName = typeof(T).Name;
        private readonly string _eventStreamCollectionName = string.Format("{0}_EventStream", typeof(T).Name);
        private readonly MongoClient _mongoClient;
        private readonly MongoServer _mongoServer;
        private readonly MongoDatabase _mongoDatabase;
        private readonly MongoCollection<T> _readModelCollection;
        private readonly MongoCollection<ReadModelProcessedEvent> _eventStreamCollection;

        public VersionedReadModelRepository(MongoClient mongoClient)
        {
            _mongoClient = mongoClient;
            _mongoServer = _mongoClient.GetServer();
            _mongoDatabase = _mongoServer.GetDatabase(Database);
            _readModelCollection = _mongoDatabase.GetCollection<T>(_readModelCollectionName, new MongoCollectionSettings { WriteConcern = WriteConcern.Acknowledged });
            _eventStreamCollection = _mongoDatabase.GetCollection<ReadModelProcessedEvent>(_eventStreamCollectionName, new MongoCollectionSettings { WriteConcern = WriteConcern.Acknowledged });
        }

        public Task<T> GetByIdentity(Guid identity, DateTime eventDate)
        {
            return Task.Run(() =>
                                {
                                    var eventUtcDate = eventDate.ToUniversalTime();

                                    var readModel = _readModelCollection.AsQueryable()
                                        .OrderByDescending(x => x.Version)
                                        .FirstOrDefault(x => x.Identity == identity);

                                    var unprocessedEvents = _eventStreamCollection.AsQueryable()
                                        .Where(x => x.ReadModelIdentity == identity && x.EventUtcDate > eventUtcDate)
                                        .ToList();

                                    if (readModel != null)
                                        readModel.SetUnprocessedEvents(unprocessedEvents);

                                    return readModel;
                                });
        }

        public Task Save(T readModel)
        {
            return Task.Run(() =>
                                {
                                    try
                                    {
                                        if (!readModel.UncommittedEvents.Any()) return;

                                        readModel.Version++;

                                        _readModelCollection.Insert(readModel, WriteConcern.Acknowledged);

                                        foreach (var @event in readModel.UncommittedEvents)
                                        {
                                            try
                                            {
                                                _eventStreamCollection.Insert(@event, WriteConcern.Acknowledged);
                                            }
                                            catch (MongoException ex)
                                            {
                                                if (!ex.Message.Contains(MongoConcurrencyExceptionCode))
                                                {
                                                    throw;
                                                }
                                            }
                                        }
                                    }
                                    catch (MongoException ex)
                                    {
                                        if (ex.Message.Contains(MongoConcurrencyExceptionCode))
                                        {
                                            throw new ReadModelConcurrencyException(ex.Message, ex);
                                        }

                                        throw;
                                    }
                                });
        }
    }
}
