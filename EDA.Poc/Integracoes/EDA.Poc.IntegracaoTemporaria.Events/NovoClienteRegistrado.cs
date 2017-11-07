using System;
using EDA.Poc.Infraestrutura.Messaging.Interfaces;

namespace EDA.Poc.IntegracaoTemporaria.Events
{
    public class NovoClienteRegistrado : IEvent
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public Guid IdDoCliente { get; set; }

        public NovoClienteRegistrado()
        {
            Id = Guid.NewGuid();
            Date = DateTime.Now;
        }
    }
}