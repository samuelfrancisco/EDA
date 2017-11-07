using System;
using System.Linq;
using System.Collections.Generic;
using EDA.Poc.Infraestrutura.ReadModel;
using EDA.Poc.Pagamentos.ReadModel.PaginaDeDetalhesDoConteudo.Eventos;

namespace EDA.Poc.Pagamentos.ReadModel.PaginaDeDetalhesDoConteudo
{
    public class PaginaDeDetalhesDoConteudo : VersionedReadModel
    {
        public Guid IdDoConteudo { get; set; }

        public virtual ConsolidadoAvaliacao ConsolidadoAvaliacao { get; set; }

        public virtual Precos Precos { get; set; }

        public virtual List<Detalhes> Detalhes { get; set; }

        public virtual List<Preview> Previews { get; set; }

        public virtual List<CompetenciaParaSuaEquipe> CompetenciasParaSuaEquipe { get; set; }

        public virtual List<CompetenciaParaSuaEmpresa> CompetenciasParaSuaEmpresa { get; set; }

        public virtual List<CompetenciaParaOCliente> CompetenciasParaOCliente { get; set; }

        public virtual List<Semelhante> Semelhantes { get; set; }

        public virtual List<MaisDoFornecedor> MaisDoFornecedor { get; set; }

        public virtual List<Resenha> Resenhas { get; set; }

        public virtual List<AvaliacaoDTO> AvaliacoesRealizadas { get; set; }

        public PaginaDeDetalhesDoConteudo()
        {
            Detalhes = new List<Detalhes>();
            Previews = new List<Preview>();
            CompetenciasParaSuaEquipe = new List<CompetenciaParaSuaEquipe>();
            CompetenciasParaSuaEmpresa = new List<CompetenciaParaSuaEmpresa>();
            CompetenciasParaOCliente = new List<CompetenciaParaOCliente>();
            Semelhantes = new List<Semelhante>();
            MaisDoFornecedor = new List<MaisDoFornecedor>();
            Resenhas = new List<Resenha>();
            AvaliacoesRealizadas = new List<AvaliacaoDTO>();
        }

        public void Avaliar(PaginaDeDetalhesDoConteudoConteudoAvaliado evento)
        {
            Update(evento);
        }

        public void Reavaliar(PaginaDeDetalhesDoConteudoConteudoReavaliado evento)
        {
            Update(evento);
        }

        protected override void SetHandlers()
        {
            Handles<PaginaDeDetalhesDoConteudoConteudoAvaliado>(When);
            Handles<PaginaDeDetalhesDoConteudoConteudoReavaliado>(When);
        }

        protected override void SetReverseHandlers()
        {
            HandlesReverse<PaginaDeDetalhesDoConteudoConteudoAvaliado>(WhenReverse);
            HandlesReverse<PaginaDeDetalhesDoConteudoConteudoReavaliado>(WhenReverse);
        }

        private void When(PaginaDeDetalhesDoConteudoConteudoAvaliado @event)
        {
            ProcessarAvaliacao(@event.ToAvaliacaoDTO());
        }

        private void When(PaginaDeDetalhesDoConteudoConteudoReavaliado @event)
        {
            var resenha = Resenhas.SingleOrDefault(x => x.IdDoUsuario == @event.IdDoUsuario);

            if (resenha != null)
                Resenhas.Remove(resenha);

            ProcessarAvaliacao(@event.ToAvaliacaoDTO());
        }

        private void WhenReverse(PaginaDeDetalhesDoConteudoConteudoAvaliado @event)
        {
            var resenha = Resenhas.SingleOrDefault(x => x.IdDoUsuario == @event.IdDoUsuario);

            if (resenha != null)
                Resenhas.Remove(resenha);

            var avaliacao = AvaliacoesRealizadas.SingleOrDefault(x => x.IdDoComandoOriginador == @event.IdDoComandoOriginador);

            if (avaliacao != null)
                AvaliacoesRealizadas.Remove(avaliacao);
        }

        private void WhenReverse(PaginaDeDetalhesDoConteudoConteudoReavaliado @event)
        {
            var resenha = Resenhas.SingleOrDefault(x => x.IdDoUsuario == @event.IdDoUsuario);

            if (resenha != null)
                Resenhas.Remove(resenha);

            var avaliacao = AvaliacoesRealizadas.SingleOrDefault(x => x.IdDoComandoOriginador == @event.IdDoComandoOriginador);

            if (avaliacao != null)
                AvaliacoesRealizadas.Remove(avaliacao);

            var minhaAvaliacaoAnterior = AvaliacoesRealizadas.OrderByDescending(x => x.DataDaAvaliacao).FirstOrDefault(x => x.IdDoUsuario == @event.IdDoUsuario);

            if (minhaAvaliacaoAnterior != null)
            {
                Resenhas.Add(new Resenha
                                 {
                                     IdDoConteudo = minhaAvaliacaoAnterior.IdDoConteudo,
                                     IdDoUsuario = minhaAvaliacaoAnterior.IdDoUsuario,
                                     Avaliacao = minhaAvaliacaoAnterior.Avaliacao,
                                     Conteudo = minhaAvaliacaoAnterior.Conteudo,
                                     Data = minhaAvaliacaoAnterior.DataDaAvaliacao,
                                     NomeUsuario = minhaAvaliacaoAnterior.NomeDoUsuario
                                 });
            }
        }

        private void ProcessarAvaliacao(AvaliacaoDTO minhaAvaliacao)
        {
            AvaliacoesRealizadas.Add(minhaAvaliacao);

            Resenhas.Add(new Resenha
                             {
                                 IdDoConteudo = minhaAvaliacao.IdDoConteudo,
                                 IdDoUsuario = minhaAvaliacao.IdDoUsuario,
                                 Avaliacao = minhaAvaliacao.Avaliacao,
                                 Conteudo = minhaAvaliacao.Conteudo,
                                 Data = minhaAvaliacao.DataDaAvaliacao,
                                 NomeUsuario = minhaAvaliacao.NomeDoUsuario
                             });

            ConsolidadoAvaliacao.MediaDasAvaliacoesDoConteudo = minhaAvaliacao.MediaDasAvaliacoesDoConteudo;
            ConsolidadoAvaliacao.QuantidadeAvaliacoes = minhaAvaliacao.QuantidadeDeAvaliacoes;
            ConsolidadoAvaliacao.QuantidadeDeAvaliacoesPorValorDeAvaliacao = minhaAvaliacao.QuantidadeDeAvaliacoesPorValorDeAvaliacao;

            Detalhes.ForEach(x =>
                                 {
                                     x.Avaliacao = minhaAvaliacao.MediaDasAvaliacoesDoConteudo;
                                 });
        }
    }
}
