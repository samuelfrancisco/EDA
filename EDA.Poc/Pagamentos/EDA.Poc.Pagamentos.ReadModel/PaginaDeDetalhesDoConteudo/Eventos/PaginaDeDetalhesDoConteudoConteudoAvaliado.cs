using System;
using System.Collections.Generic;
using System.Linq;
using EDA.Poc.Infraestrutura.ReadModel;
using EDA.Poc.Pagamentos.Events;

namespace EDA.Poc.Pagamentos.ReadModel.PaginaDeDetalhesDoConteudo.Eventos
{
    public class PaginaDeDetalhesDoConteudoConteudoAvaliado : ReadModelProcessedEvent
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

        public PaginaDeDetalhesDoConteudoConteudoAvaliado()
        {
            
        }

        public PaginaDeDetalhesDoConteudoConteudoAvaliado(PaginaDeDetalhesDoConteudo paginaDeDetalhesDoConteudo, ConteudoAvaliado conteudoAvaliado)
            : base(paginaDeDetalhesDoConteudo, conteudoAvaliado)
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