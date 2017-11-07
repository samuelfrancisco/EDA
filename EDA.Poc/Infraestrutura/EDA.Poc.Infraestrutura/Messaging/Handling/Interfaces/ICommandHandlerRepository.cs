using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EDA.Poc.Infraestrutura.Messaging.Handling.Interfaces
{
    public interface ICommandHandlerRepository
    {
        Task<ReceivedCommand> GetReceivedCommandById(Type commandHandlerType, Guid commandId);
        Task<SucceededCommand> GetSucceededCommandById(Type commandHandlerType, Guid commandId);
        Task<FailedCommand> GetFailedCommandById(Type commandHandlerType, Guid commandId);
        Task<List<ReceivedCommand>> GetAllReceivedCommandsWhere(Type commandHandlerType, Expression<Func<ReceivedCommand, bool>> expression);
        Task<List<SucceededCommand>> GetAllSucceededCommandsWhere(Type commandHandlerType, Expression<Func<SucceededCommand, bool>> expression);
        Task<List<FailedCommand>> GetAllFailedCommandsWhere(Type commandHandlerType, Expression<Func<FailedCommand, bool>> expression);
        Task SaveReceivedCommand(Type commandHandlerType, ReceivedCommand receivedCommand);
        Task SaveSucceededCommand(Type commandHandlerType, SucceededCommand succeededCommand);
        Task SaveFailedCommand(Type commandHandlerType, FailedCommand failedCommand);
        Task RemoveFailedCommand(Type commandHandlerType, FailedCommand failedCommand);
    }
}