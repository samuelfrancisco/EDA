using System;

namespace EDA.Poc.Infraestrutura.EventSourcing.Interfaces
{
    public interface IMemento
    {
        Guid Id { get; set; }
        long Version { get; set; }
    }

    public interface IMemento<T> : IMemento where T : class, IEventSourced
    {
       
    }
}
