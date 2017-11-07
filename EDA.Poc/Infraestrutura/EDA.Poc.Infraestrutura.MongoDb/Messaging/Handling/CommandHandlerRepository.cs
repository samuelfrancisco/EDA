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
    public class CommandHandlerRepository : ICommandHandlerRepository
    {
        private readonly MongoClient _mongoClient;
        private const string DatabaseName = "MarketPlaceCommandHandlerStore";

        public CommandHandlerRepository(MongoClient mongoClient)
        {
            _mongoClient = mongoClient;
        }

        public Task<ReceivedCommand> GetReceivedCommandById(Type commandHandlerType, Guid commandId)
        {
            return Task.Run(() =>
                                {
                                    var server = _mongoClient.GetServer();

                                    var database = server.GetDatabase(DatabaseName);

                                    var collection = database.GetCollection<ReceivedCommand>(string.Format("{0}_ReceivedCommands", commandHandlerType.Name));

                                    return collection.AsQueryable().FirstOrDefault(x => x.Id == commandId);
                                });
        }

        public Task<SucceededCommand> GetSucceededCommandById(Type commandHandlerType, Guid commandId)
        {
            return Task.Run(() =>
                                {
                                    var server = _mongoClient.GetServer();

                                    var database = server.GetDatabase(DatabaseName);

                                    var collection = database.GetCollection<SucceededCommand>(string.Format("{0}_SucceededCommands", commandHandlerType.Name));

                                    return collection.AsQueryable().FirstOrDefault(x => x.Id == commandId);
                                });
        }

        public Task<FailedCommand> GetFailedCommandById(Type commandHandlerType, Guid commandId)
        {
            return Task.Run(() =>
                                {
                                    var server = _mongoClient.GetServer();

                                    var database = server.GetDatabase(DatabaseName);

                                    var collection = database.GetCollection<FailedCommand>(string.Format("{0}_FailedCommands", commandHandlerType.Name));

                                    return collection.AsQueryable().FirstOrDefault(x => x.Id == commandId);
                                });
        }

        public Task<List<ReceivedCommand>> GetAllReceivedCommandsWhere(Type commandHandlerType, Expression<Func<ReceivedCommand, bool>> expression)
        {
            return Task.Run(() =>
                                {
                                    var server = _mongoClient.GetServer();

                                    var database = server.GetDatabase(DatabaseName);

                                    var collection = database.GetCollection<ReceivedCommand>(string.Format("{0}_ReceivedCommands", commandHandlerType.Name));

                                    return collection.AsQueryable().Where(expression).ToList();
                                });
        }

        public Task<List<SucceededCommand>> GetAllSucceededCommandsWhere(Type commandHandlerType, Expression<Func<SucceededCommand, bool>> expression)
        {
            return Task.Run(() =>
                                {
                                    var server = _mongoClient.GetServer();

                                    var database = server.GetDatabase(DatabaseName);

                                    var collection = database.GetCollection<SucceededCommand>(string.Format("{0}_SucceededCommands", commandHandlerType.Name));

                                    return collection.AsQueryable().Where(expression).ToList();
                                });
        }

        public Task<List<FailedCommand>> GetAllFailedCommandsWhere(Type commandHandlerType, Expression<Func<FailedCommand, bool>> expression)
        {
            return Task.Run(() =>
                                {
                                    var server = _mongoClient.GetServer();

                                    var database = server.GetDatabase(DatabaseName);

                                    var collection = database.GetCollection<FailedCommand>(string.Format("{0}_FailedCommands", commandHandlerType.Name));

                                    return collection.AsQueryable().Where(expression).ToList();
                                });
        }

        public Task SaveReceivedCommand(Type commandHandlerType, ReceivedCommand receivedCommand)
        {
            return Task.Run(() =>
                                {
                                    var server = _mongoClient.GetServer();

                                    var database = server.GetDatabase(DatabaseName);

                                    var collection = database.GetCollection<ReceivedCommand>(string.Format("{0}_ReceivedCommands", commandHandlerType.Name));

                                    collection.Save(receivedCommand);
                                });
        }

        public Task SaveSucceededCommand(Type commandHandlerType, SucceededCommand succeededCommand)
        {
            return Task.Run(() =>
                                {
                                    var server = _mongoClient.GetServer();

                                    var database = server.GetDatabase(DatabaseName);

                                    var collection = database.GetCollection<SucceededCommand>(string.Format("{0}_SucceededCommands", commandHandlerType.Name));

                                    collection.Save(succeededCommand);
                                });
        }

        public Task SaveFailedCommand(Type commandHandlerType, FailedCommand failedCommand)
        {
            return Task.Run(() =>
                                {
                                    var server = _mongoClient.GetServer();

                                    var database = server.GetDatabase(DatabaseName);

                                    var collection = database.GetCollection<FailedCommand>(string.Format("{0}_FailedCommands", commandHandlerType.Name));

                                    collection.Save(failedCommand);
                                });
        }

        public Task RemoveFailedCommand(Type commandHandlerType, FailedCommand failedCommand)
        {
            return Task.Run(() =>
                                {
                                    var server = _mongoClient.GetServer();

                                    var database = server.GetDatabase(DatabaseName);

                                    var collection = database.GetCollection<FailedCommand>(string.Format("{0}_FailedCommands", commandHandlerType.Name));

                                    collection.Remove(Query<FailedCommand>.EQ(x => x.Id, failedCommand.Id));
                                });
        }
    }
}
