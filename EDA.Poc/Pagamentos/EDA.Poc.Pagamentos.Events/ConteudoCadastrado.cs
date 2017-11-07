using System;
using System.Collections.Generic;
using EDA.Poc.Infraestrutura.EventSourcing;

namespace EDA.Poc.Pagamentos.Events
{
    public class ConteudoCadastrado : VersionedEvent
    {
        public Guid IdDoConteudo { get; set; }
        public Decimal PrecoSemLimitesDeUsuarios { get; set; }
        public List<ConteudoCadastradoFaixaDePreco> FaixaDePrecos { get; set; }
        public List<ConteudoCadastradoPromocao> Promocoes { get; set; }
    }
}