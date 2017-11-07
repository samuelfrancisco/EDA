using System;
using EDA.Poc.Infraestrutura.EventSourcing;

namespace EDA.Poc.Pagamentos.Events
{
    public class NovoClienteRegistradoNaLoja : VersionedEvent
    {
        public Guid IdDoCliente { get; set; }
    }
}