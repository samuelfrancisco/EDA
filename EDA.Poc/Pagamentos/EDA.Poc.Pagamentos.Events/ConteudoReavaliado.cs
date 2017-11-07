using System;
using System.Collections.Generic;
using EDA.Poc.Infraestrutura.EventSourcing;

namespace EDA.Poc.Pagamentos.Events
{
    public class ConteudoReavaliado : VersionedEvent
    {
        public Guid IdDoCliente { get; set; }
        public Guid IdDoConteudo { get; set; }
        public Guid IdDoUsuario { get; set; }
        public int Avaliacao { get; set; }
        public string Conteudo { get; set; }
        public string NomeDoUsuario { get; set; }
        public decimal MediaDasAvaliacoesDoConteudo { get; set; }
        public Dictionary<int, int> QuantidadeDeAvaliacoesPorValorDeAvaliacao { get; set; }
    }
}