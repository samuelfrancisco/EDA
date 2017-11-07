using System;
using System.Collections.Generic;
using System.Linq;
using EDA.Poc.Infraestrutura.ReadModel;
using EDA.Poc.Pagamentos.Events;

namespace EDA.Poc.Pagamentos.ReadModel.PaginaInicial.Eventos
{
    public class PaginaInicialConteudoAvaliado : ReadModelProcessedEvent
    {
        public Guid IdDoConteudo { get; private set; }
        public Guid IdDoUsuario { get; private set; }
        public Guid IdDoComandoOriginador { get; private set; }
        public int Avaliacao { get; private set; }
        public string Conteudo { get; private set; }
        public DateTime DataDaAvaliacao { get; private set; }
        public int QuantidadeDeAvaliacoes { get; private set; }
        public decimal MediaDasAvaliacoesDoConteudo { get; private set; }
        public Dictionary<int, int> QuantidadeDeAvaliacoesPorValorDeAvaliacao { get; private set; }

        private PaginaInicialConteudoAvaliado()
        {

        }

        public PaginaInicialConteudoAvaliado(PaginaInicial paginaInicial, ConteudoAvaliado conteudoAvaliado)
            : base(paginaInicial, conteudoAvaliado)
        {
            if (!conteudoAvaliado.OriginalCorrelationId.HasValue) throw new NullReferenceException("conteudoAvaliado.OriginalCorrelationId");

            IdDoConteudo = conteudoAvaliado.IdDoConteudo;
            IdDoUsuario = conteudoAvaliado.IdDoUsuario;
            IdDoComandoOriginador = conteudoAvaliado.OriginalCorrelationId.Value;
            Avaliacao = conteudoAvaliado.Avaliacao;
            Conteudo = conteudoAvaliado.Conteudo;
            DataDaAvaliacao = conteudoAvaliado.Date;
            QuantidadeDeAvaliacoes = conteudoAvaliado.QuantidadeDeAvaliacoesPorValorDeAvaliacao.Sum(x => x.Value);
            MediaDasAvaliacoesDoConteudo = conteudoAvaliado.MediaDasAvaliacoesDoConteudo;
            QuantidadeDeAvaliacoesPorValorDeAvaliacao = new Dictionary<int, int>(conteudoAvaliado.QuantidadeDeAvaliacoesPorValorDeAvaliacao);
        }
    }
}
