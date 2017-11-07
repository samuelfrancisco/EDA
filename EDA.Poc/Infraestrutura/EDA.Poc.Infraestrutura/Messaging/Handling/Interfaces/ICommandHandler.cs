using EDA.Poc.Infraestrutura.Messaging.Interfaces;
using MassTransit;

namespace EDA.Poc.Infraestrutura.Messaging.Handling.Interfaces
{
    public interface ICommandHandler : IConsumer { }

    public interface ICommandHandler<T> : ICommandHandler, IConsumer<T> where T : class, ICommand
    {
    }
}
