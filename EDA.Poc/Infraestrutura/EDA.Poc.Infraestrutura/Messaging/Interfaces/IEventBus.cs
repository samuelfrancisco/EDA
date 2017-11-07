using System.Collections.Generic;
using System.Threading.Tasks;

namespace EDA.Poc.Infraestrutura.Messaging.Interfaces
{
    public interface IEventBus
    {
        Task PublishOne<T>(T @event) where T : class, IEvent;
        Task PublishMany<T>(IEnumerable<T> events) where T : class, IEvent;
    }
}
