using System;
using System.Collections.Generic;
using EDA.Poc.Infraestrutura.Messaging.Interfaces;

namespace EDA.Poc.IntegracaoTemporaria.Events
{
    public class NovoConteudoCadastrado : IEvent
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public Guid IdDoConteudo { get; set; }
        public Decimal PrecoSemLimitesDeUsuarios { get; set; }
        public List<NovoConteudoCadastradoFaixaDePreco> FaixaDePrecos { get; set; }
        public List<NovoConteudoCadastradoPromocao> Promocoes { get; set; }

        public NovoConteudoCadastrado()
        {
            Id = Guid.NewGuid();
            Date = DateTime.Now;
        }
    }
}