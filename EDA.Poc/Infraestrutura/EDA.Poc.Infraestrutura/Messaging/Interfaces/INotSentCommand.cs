using EDA.Poc.Infraestrutura.Exceptions;

namespace EDA.Poc.Infraestrutura.Messaging.Interfaces
{
    public interface INotSentCommand
    {
        ICommand Command { get; }
        ExceptionInfo ExceptionInfo { get; }
    }
}
