using System;
using System.Collections.Generic;
using System.Linq;

namespace EDA.Poc.Pagamentos.ReadModel.PaginaDeDetalhesDoConteudo
{
    public class ConsolidadoAvaliacao
    {
        public Guid IdDoConteudo { get; set; }

        public int QuantidadeAvaliacoes { get; set; }

        public decimal MediaDasAvaliacoesDoConteudo { get; set; }

        private Dictionary<int, int> _quantidadeDeAvaliacoesPorValorDeAvaliacao;        
        
        public Dictionary<int, int> QuantidadeDeAvaliacoesPorValorDeAvaliacao 
        { 
            get 
            {
                return _quantidadeDeAvaliacoesPorValorDeAvaliacao.OrderByDescending(x => x.Key).ToDictionary(x => x.Key, x => x.Value);
            }
            set
            {
                _quantidadeDeAvaliacoesPorValorDeAvaliacao = value;
            }
        }

        public ConsolidadoAvaliacao()
        {
            QuantidadeDeAvaliacoesPorValorDeAvaliacao = new Dictionary<int, int>();
        }
    }
}
