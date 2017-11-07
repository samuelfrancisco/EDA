using System;
using System.Collections.Generic;
using EDA.Poc.Infraestrutura.Messaging.Interfaces;

namespace EDA.Poc.Infraestrutura.Messaging.Handling
{
    public abstract class PersistedCommand
    {
        protected PersistedCommand()
        {
            Metadata = new Dictionary<string, string>();
        }

        protected PersistedCommand(Type commandHandlerType, ICommand command)
            : this()
        {

            Id = command.Id;
            CommandHandlerTypeFullName = commandHandlerType.Name;
            CommandTypeFullName = command.GetType().FullName;            
            CommandDate = command.Date;
            Payload = command;

            SetMetadata(commandHandlerType, command);
        }

        public Guid Id { get; set; }
        public string CommandHandlerTypeFullName { get; set; }
        public string CommandTypeFullName { get; set; }
        public DateTime CommandDate { get; set; }
        public ICommand Payload { get; set; }
        public Dictionary<string, string> Metadata { get; set; }

        private void SetMetadata(Type commandHandlerType, ICommand command)
        {
            var commandType = command.GetType();

            Metadata.Add("CommandHandlerTypeFullName", commandHandlerType.Name);
            Metadata.Add("CommandHandlerTypeName", commandHandlerType.Name);
            Metadata.Add("CommandHandlerAssemblyFullName", commandHandlerType.Assembly.FullName);
            Metadata.Add("CommandHandlerNamespace", commandHandlerType.Namespace);
            Metadata.Add("CommandTypeFullName", commandType.FullName);
            Metadata.Add("CommandTypeName", commandType.Name);
            Metadata.Add("CommandAssemblyFullName", commandType.Assembly.FullName);
            Metadata.Add("CommandNamespace", commandType.Namespace);
        }
    }
}