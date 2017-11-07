using System;
using System.Collections.Generic;

namespace EDA.Poc.Pagamentos.ReadModel.PaginaDeDetalhesDoConteudo
{
    public class AvaliacaoDTO
    {
        public Guid IdDoConteudo { get; set; }
        
        public Guid IdDoUsuario { get; set; }
        
        public Guid IdDoComandoOriginador { get; set; }

        public int Avaliacao { get; set; }

        public string Conteudo { get; set; }
        
        public DateTime DataDaAvaliacao { get; set; }

        public int QuantidadeDeAvaliacoes { get; set; }

        public decimal MediaDasAvaliacoesDoConteudo { get; set; }

        public Dictionary<int, int> QuantidadeDeAvaliacoesPorValorDeAvaliacao { get; set; }

        public string NomeDoUsuario { get; set; }

        public string DataDaResenhaLabel { get { return DataDaAvaliacao.ToShortDateString(); } }
        
        public AvaliacaoDTO()
        {
            QuantidadeDeAvaliacoesPorValorDeAvaliacao = new Dictionary<int, int>();
        }
    }
}
