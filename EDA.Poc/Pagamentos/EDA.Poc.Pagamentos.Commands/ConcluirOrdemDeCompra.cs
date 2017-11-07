using System;
using EDA.Poc.Infraestrutura.Messaging;

namespace EDA.Poc.Pagamentos.Commands
{
    public class ConcluirOrdemDeCompra : Command
    {
        public Guid IdDaOrdemDeCompra { get; set; }
    }
}