using System.Collections.Generic;
using EDA.Poc.Infraestrutura.EventSourcing.Interfaces;
using EDA.Poc.Infraestrutura.Messaging.Interfaces;

namespace EDA.Poc.Infraestrutura.Processes.Interfaces
{
    public interface IEventSourcedProcessManager : IEventSourced
    {
        IEnumerable<dynamic> UnsentCommands { get; }
    }
}