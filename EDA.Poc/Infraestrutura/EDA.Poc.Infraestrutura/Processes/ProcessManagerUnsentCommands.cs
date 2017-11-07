using System;
using System.Collections.Generic;

namespace EDA.Poc.Infraestrutura.Processes
{
    public class ProcessManagerUnsentCommands
    {
        public Guid Id { get; set; }
        public Guid ProcessManagerId { get; set; }
        public List<dynamic> UnsentCommands { get; set; }

        public ProcessManagerUnsentCommands()
        {
            Id = Guid.NewGuid();
            UnsentCommands = new List<dynamic>();
        }
    }
}
