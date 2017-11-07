using System;
using System.Collections.Generic;
using System.Linq;
using EDA.Poc.Infraestrutura.ReadModel;
using EDA.Poc.Pagamentos.Events;

namespace EDA.Poc.Pagamentos.ReadModel.PaginaDeDetalhesDoConteudo.Eventos
{
    public class PaginaDeDetalhesDoConteudoConteudoReavaliado : ReadModelProcessedEvent
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
        public string NomeDoUsuario { get; set; }

        public PaginaDeDetalhesDoConteudoConteudoReavaliado()
        {

        }

        public PaginaDeDetalhesDoConteudoConteudoReavaliado(PaginaDeDetalhesDoConteudo paginaDeDetalhesDoConteudo, ConteudoReavaliado conteudoReavaliado)
            : base(paginaDeDetalhesDoConteudo, conteudoReavaliado)
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

        public AvaliacaoDTO ToAvaliacaoDTO()
        {
            return new AvaliacaoDTO
                       {
                           IdDoConteudo = IdDoConteudo,
                           IdDoUsuario = IdDoUsuario,
                           IdDoComandoOriginador = IdDoComandoOriginador,
                           Avaliacao = Avaliacao,
                           Conteudo = Conteudo,
                           DataDaAvaliacao = DataDaAvaliacao,
                           QuantidadeDeAvaliacoes = QuantidadeDeAvaliacoes,
                           MediaDasAvaliacoesDoConteudo = MediaDasAvaliacoesDoConteudo,
                           QuantidadeDeAvaliacoesPorValorDeAvaliacao = QuantidadeDeAvaliacoesPorValorDeAvaliacao,
                           NomeDoUsuario = NomeDoUsuario
                       };
        }
    }
}