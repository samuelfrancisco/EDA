using System;

namespace EDA.Poc.Infraestrutura.Messaging.Interfaces
{
    public interface IEvent
    {
        Guid Id { get; }
        DateTime Date { get; }
    }
}