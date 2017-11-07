using System;
using System.Collections.Generic;
using EDA.Poc.Infraestrutura.EventSourcing.Interfaces;

namespace EDA.Poc.Pagamentos.Domain
{
    public class ConteudoMemento : IMemento<Conteudo>, IMemento<IEventSourced>
    {
        public Guid Id { get; set; }
        public long Version { get; set; }        
        public decimal PrecoSemLimiteDeLicencas { get; set; }
        public List<ConteudoFaixaDePreco> FaixasDePreco { get; set; }
        public List<ConteudoPromocao> Promocoes { get; set; }
        public List<ConteudoAvaliacao> Avaliacoes { get; set; }
        public Dictionary<int, int> QuantidadeDeAvaliacoesPorValorDeAvaliacao { get; set; }
    }
}