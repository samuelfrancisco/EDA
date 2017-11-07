using System;
using System.Collections.Generic;
using System.Linq;
using EDA.Poc.Infraestrutura.ReadModel;
using EDA.Poc.Pagamentos.Events;
namespace EDA.Poc.Pagamentos.ReadModel.PaginaInicial.Eventos
{
    public class PaginaInicialConteudoReavaliado : ReadModelProcessedEvent
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

        private PaginaInicialConteudoReavaliado()
        {

        }

        public PaginaInicialConteudoReavaliado(PaginaInicial paginaInicial, ConteudoReavaliado conteudoReavaliado)
            : base(paginaInicial, conteudoReavaliado)
        {
            if (!conteudoReavaliado.OriginalCorrelationId.HasValue) throw new NullReferenceException("conteudoReavaliado.OriginalCorrelationId");

            IdDoConteudo = conteudoReavaliado.IdDoConteudo;
            IdDoUsuario = conteudoReavaliado.IdDoUsuario;
            IdDoComandoOriginador = conteudoReavaliado.OriginalCorrelationId.Value;
            Avaliacao = conteudoReavaliado.Avaliacao;
            Conteudo = conteudoReavaliado.Conteudo;
            DataDaAvaliacao = conteudoReavaliado.Date;
            QuantidadeDeAvaliacoes = conteudoReavaliado.QuantidadeDeAvaliacoesPorValorDeAvaliacao.Sum(x => x.Value);
            MediaDasAvaliacoesDoConteudo = conteudoReavaliado.MediaDasAvaliacoesDoConteudo;
            QuantidadeDeAvaliacoesPorValorDeAvaliacao = new Dictionary<int, int>(conteudoReavaliado.QuantidadeDeAvaliacoesPorValorDeAvaliacao);
        }
    }
}
