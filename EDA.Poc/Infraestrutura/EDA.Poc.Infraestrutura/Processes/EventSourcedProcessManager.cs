using System;
using System.Collections.Generic;
using EDA.Poc.Infraestrutura.EventSourcing;
using EDA.Poc.Infraestrutura.Messaging.Interfaces;
using EDA.Poc.Infraestrutura.Processes.Interfaces;

namespace EDA.Poc.Infraestrutura.Processes
{
    public abstract class EventSourcedProcessManager : EventSourced, IEventSourcedProcessManager
    {
        private readonly List<dynamic> _unsentCommands = new List<dynamic>();

        public IEnumerable<dynamic> UnsentCommands { get { return _unsentCommands; } }

        protected EventSourcedProcessManager()
        {

        }

        protected EventSourcedProcessManager(Guid id)
            : base(id)
        {

        }

        protected void AddCommand(ICommand command)
        {
            _unsentCommands.Add(command);
        }
    }
}
