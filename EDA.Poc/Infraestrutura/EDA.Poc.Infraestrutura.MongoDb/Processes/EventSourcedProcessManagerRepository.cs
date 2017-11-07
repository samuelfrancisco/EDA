using System;
using System.Linq;
using System.Threading.Tasks;
using EDA.Poc.Infraestrutura.EventSourcing.Interfaces;
using EDA.Poc.Infraestrutura.Messaging.Exceptions;
using EDA.Poc.Infraestrutura.Messaging.Interfaces;
using EDA.Poc.Infraestrutura.Processes;
using EDA.Poc.Infraestrutura.Processes.Interfaces;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace EDA.Poc.Infraestrutura.MongoDb.Processes
{
    public class EventSourcedProcessManagerRepository<T> : IEventSourcedProcessManagerRepository<T> where T : EventSourcedProcessManager
    {
        private const string BucketId = "ProcessManagers";
        private const string DatabaseName = "MarketPlaceProccessManagerCommandStore";
        private readonly ICommandBus _commandBus;
        private readonly IEventSourcedRepository<T> _eventSourcedRepository;
        private readonly MongoClient _mongoClient;
        private readonly string _unsentCommandsCollectionName = string.Format("{0}_UnsentCommands", typeof(T).Name);

        public EventSourcedProcessManagerRepository(
            ICommandBus commandBus,
            IEventSourcedRepository<T> eventSourcedRepository,
            MongoClient mongoClient)
        {
            _commandBus = commandBus;
            _eventSourcedRepository = eventSourcedRepository;
            _mongoClient = mongoClient;
        }

        public Task<T> GetById(Guid id)
        {
            return Task.Run(async () =>
                                      {
                                          var processManager = await _eventSourcedRepository.GetById(id, BucketId).ConfigureAwait(false);

                                          return processManager;
                                      });
        }

        public Task Save(T processManager, Guid commitId)
        {
            return Task.Run(async () =>
                                      {
                                          await _eventSourcedRepository.Save(processManager, commitId, BucketId).ConfigureAwait(false);

                                          await SaveUnsentCommands(processManager).ConfigureAwait(false);

                                          try
                                          {
                                              await SendUnsentCommands(processManager).ConfigureAwait(false);
                                          }
                                          catch (CommandSendException)
                                          {

                                          }
                                      });
        }

        private Task SaveUnsentCommands(T processManager)
        {
            return Task.Run(() =>
                                {
                                    var server = _mongoClient.GetServer();

                                    var database = server.GetDatabase(DatabaseName);

                                    var collection = database.GetCollection<ProcessManagerUnsentCommands>(_unsentCommandsCollectionName);

                                    var processManagerUnsentCommands = collection.AsQueryable().FirstOrDefault(x => x.ProcessManagerId == processManager.Id)
                                                                       ?? new ProcessManagerUnsentCommands
                                                                              {
                                                                                  ProcessManagerId = processManager.Id
                                                                              };

                                    var unsentCommands = processManager.UnsentCommands.ToList();

                                    processManagerUnsentCommands.UnsentCommands.AddRange(unsentCommands);

                                    collection.Save(processManagerUnsentCommands);
                                });
        }

        private Task SendUnsentCommands(T processManager)
        {
            return Task.Run(async () =>
                                      {
                                          var server = _mongoClient.GetServer();

                                          var database = server.GetDatabase(DatabaseName);

                                          var collection = database.GetCollection<ProcessManagerUnsentCommands>(_unsentCommandsCollectionName);

                                          var processManagerUnsentCommands = collection.AsQueryable().First(x => x.ProcessManagerId == processManager.Id);

                                          var unsentCommands = processManagerUnsentCommands.UnsentCommands.ToList();

                                          if (!unsentCommands.Any()) return;

                                          foreach (dynamic unsentCommand in unsentCommands)
                                          {
                                              try
                                              {
                                                  await _commandBus.SendOne(unsentCommand).ConfigureAwait(false);
                                              }
                                              catch (Exception)
                                              {
                                                  collection.Save(processManagerUnsentCommands);

                                                  throw;
                                              }

                                              processManagerUnsentCommands.UnsentCommands.Remove(unsentCommand);
                                          }

                                          collection.Save(processManagerUnsentCommands);
                                      });
        }
    }
}