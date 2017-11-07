using EDA.Poc.Infraestrutura.ReadModel;
using EDA.Poc.Pagamentos.ReadModel.PaginaInicial.Eventos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EDA.Poc.Pagamentos.ReadModel.PaginaInicial
{
    public class PaginaInicial : VersionedReadModel
    { 
        public virtual List<Lancamento> Lancamentos { get; set; }

        public virtual List<DestaqueDaSemana> DestaquesDaSemana { get; set; }

        public virtual List<MaisPopular> MaisPopulares { get; set; }

        public virtual List<RecomendadoParaSuaEmpresa> RecomendadosParaSuaEmpresa { get; set; }

        public virtual List<RecomendadoParaOCliente> RecomendadosParaOCliente { get; set; }

        public virtual List<RecomendadoParaSuaEquipe> RecomendadosParaSuaEquipe { get; set; }

        public PaginaInicial()
        {
            Lancamentos = new List<Lancamento>();
            DestaquesDaSemana = new List<DestaqueDaSemana>();
            MaisPopulares = new List<MaisPopular>();
            RecomendadosParaOCliente = new List<RecomendadoParaOCliente>();
            RecomendadosParaSuaEmpresa = new List<RecomendadoParaSuaEmpresa>();
            RecomendadosParaSuaEquipe = new List<RecomendadoParaSuaEquipe>();
        }

        public void ConteudoAvaliado(PaginaInicialConteudoAvaliado conteudoAvaliado)
        {
            Update(conteudoAvaliado);
        }

        public void ConteudoReavaliado(PaginaInicialConteudoReavaliado conteudoReavaliado)
        {
            Update(conteudoReavaliado);
        }

        protected override void SetHandlers()
        {
            Handles<PaginaInicialConteudoAvaliado>(When);
            Handles<PaginaInicialConteudoReavaliado>(When);
        }        

        protected override void SetReverseHandlers()
        {
            HandlesReverse<PaginaInicialConteudoAvaliado>(WhenReverse);
            HandlesReverse<PaginaInicialConteudoReavaliado>(WhenReverse);
        }

        private void When(PaginaInicialConteudoAvaliado @event)
        {
            AtualizarAvaliacao(@event.IdDoConteudo, @event.MediaDasAvaliacoesDoConteudo);
        }

        private void When(PaginaInicialConteudoReavaliado @event)
        {
            AtualizarAvaliacao(@event.IdDoConteudo, @event.MediaDasAvaliacoesDoConteudo);
        }

        private void WhenReverse(PaginaInicialConteudoAvaliado @event)
        {
            
        }

        private void WhenReverse(PaginaInicialConteudoReavaliado @event)
        {
            
        }

        private void AtualizarAvaliacao(Guid idDoConteudo, decimal avaliacao)
        {
            foreach (var lancamento in Lancamentos.Where(x => x.IdDoConteudo == idDoConteudo))
            {
                lancamento.Avaliacao = avaliacao;
            }

            foreach (var maisPopular in MaisPopulares.Where(x => x.IdDoConteudo == idDoConteudo))
            {
                maisPopular.Avaliacao = avaliacao;
            }

            foreach (var recomendado in RecomendadosParaSuaEmpresa.Where(x => x.IdDoConteudo == idDoConteudo))
            {
                recomendado.Avaliacao = avaliacao;
            }

            foreach (var recomendado in RecomendadosParaOCliente.Where(x => x.IdDoConteudo == idDoConteudo))
            {
                recomendado.Avaliacao = avaliacao;
            }

            foreach (var recomendado in RecomendadosParaSuaEquipe.Where(x => x.IdDoConteudo == idDoConteudo))
            {
                recomendado.Avaliacao = avaliacao;
            }
        }
    }
}
