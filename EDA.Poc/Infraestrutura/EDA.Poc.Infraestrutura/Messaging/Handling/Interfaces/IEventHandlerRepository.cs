using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EDA.Poc.Infraestrutura.Messaging.Handling.Interfaces
{
    public interface IEventHandlerRepository
    {
        Task<ReceivedEvent> GetReceivedEventById(Type eventHandlerType, Guid eventId);
        Task<SucceededEvent> GetSucceededEventById(Type eventHandlerType, Guid eventId);
        Task<FailedEvent> GetFailedEventById(Type eventHandlerType, Guid eventId);
        Task<List<ReceivedEvent>> GetAllReceivedEventsWhere(Type eventHandlerType, Expression<Func<ReceivedEvent, bool>> expression);
        Task<List<SucceededEvent>> GetAllSucceededEventsWhere(Type eventHandlerType, Expression<Func<SucceededEvent, bool>> expression);
        Task<List<FailedEvent>> GetAllFailedEventsWhere(Type eventHandlerType, Expression<Func<FailedEvent, bool>> expression);
        Task SaveReceivedEvent(Type eventHandlerType, ReceivedEvent receivedEvent);
        Task SaveSucceededEvent(Type eventHandlerType, SucceededEvent succeededEvent);
        Task SaveFailedEvent(Type eventHandlerType, FailedEvent failedEvent);
        Task RemoveFailedEvent(Type eventHandlerType, FailedEvent failedEvent);
    }
}