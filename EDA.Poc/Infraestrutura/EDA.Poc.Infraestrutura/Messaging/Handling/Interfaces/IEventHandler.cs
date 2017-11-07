using EDA.Poc.Infraestrutura.Messaging.Interfaces;
using MassTransit;

namespace EDA.Poc.Infraestrutura.Messaging.Handling.Interfaces
{
    public interface IEventHandler : IConsumer { }

    public interface IEventHandler<T> : IEventHandler, IConsumer<T> where T : class, IEvent
    {
    }
}
