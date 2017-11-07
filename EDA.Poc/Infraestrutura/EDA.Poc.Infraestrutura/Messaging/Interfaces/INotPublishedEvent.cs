using EDA.Poc.Infraestrutura.Exceptions;

namespace EDA.Poc.Infraestrutura.Messaging.Interfaces
{
    public interface INotPublishedEvent
    {
        IEvent Event { get; }
        ExceptionInfo ExceptionInfo { get; }
    }
}