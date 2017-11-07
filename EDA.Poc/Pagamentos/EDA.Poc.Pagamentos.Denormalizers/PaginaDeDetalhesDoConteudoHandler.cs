using System.Threading.Tasks;
using EDA.Poc.Infraestrutura.Messaging.Handling;
using EDA.Poc.Infraestrutura.Messaging.Handling.Interfaces;
using EDA.Poc.Infraestrutura.ReadModel.Interfaces;
using EDA.Poc.Pagamentos.Consultas.PaginaDeDetalhesDoConteudo;
using EDA.Poc.Pagamentos.Events;
using EDA.Poc.Pagamentos.ReadModel.PaginaDeDetalhesDoConteudo;
using EDA.Poc.Pagamentos.ReadModel.PaginaDeDetalhesDoConteudo.Eventos;
using MassTransit;
using EDA.Poc.Infraestrutura.ReadModel.Exceptions;
using EDA.Poc.Pagamentos.Consultas.Usuarios;

namespace EDA.Poc.Pagamentos.Denormalizers
{
    public class PaginaDeDetalhesDoConteudoHandler : EventHandler,
        IEventHandler<ConteudoAvaliado>,
        IEventHandler<ConteudoReavaliado>
    {
        private readonly IVersionedReadModelRepository<PaginaDeDetalhesDoConteudo> _repositorioDasPaginasDeDetalhes;
        private readonly IServicoPaginaDeDetalhesDoConteudo _servicoPaginaDeDetalhesDoConteudo;
        private readonly IServicoUsuario _servicoUsuario;

        public PaginaDeDetalhesDoConteudoHandler(IEventHandlerRepository eventHandlerRepository,
           IVersionedReadModelRepository<PaginaDeDetalhesDoConteudo> repositorioDasPaginasDeDetalhes,
           IServicoPaginaDeDetalhesDoConteudo servicoPaginaDeDetalhesDoConteudo,
           IServicoUsuario servicoUsuario)
            : base(eventHandlerRepository)
        {
            _repositorioDasPaginasDeDetalhes = repositorioDasPaginasDeDetalhes;
            _servicoPaginaDeDetalhesDoConteudo = servicoPaginaDeDetalhesDoConteudo;
            _servicoUsuario = servicoUsuario;
        }

        public Task Consume(ConsumeContext<ConteudoAvaliado> context)
        {
            return ExecuteConsume(context.Message,
                                  async evento =>
                                  {
                                      var retry = true;

                                      var paginaDeDetalhes = await _servicoPaginaDeDetalhesDoConteudo
                                                                       .ObterAPaginaDeDetalhesDoConteudo(evento.IdDoConteudo)
                                                                       .ConfigureAwait(false);

                                      var usuario = await _servicoUsuario.ObterUsuarioPorId(evento.IdDoUsuario).ConfigureAwait(false);

                                      while (retry)
                                      {
                                          try
                                          {
                                              var paginaDeDetalhesParaEditar = await _repositorioDasPaginasDeDetalhes
                                                                                         .GetByIdentity(paginaDeDetalhes.Identity, evento.Date).ConfigureAwait(false);

                                              paginaDeDetalhesParaEditar
                                                  .Avaliar(new PaginaDeDetalhesDoConteudoConteudoAvaliado(paginaDeDetalhesParaEditar, evento)
                                                               {
                                                                   NomeDoUsuario = usuario.Nome
                                                               });

                                              await _repositorioDasPaginasDeDetalhes.Save(paginaDeDetalhesParaEditar).ConfigureAwait(false);

                                              retry = false;
                                          }
                                          catch (ReadModelConcurrencyException)
                                          {


                                          }
                                      }
                                  });
        }

        public Task Consume(ConsumeContext<ConteudoReavaliado> context)
        {
            return ExecuteConsume(context.Message,
                                  async evento =>
                                  {
                                      var retry = true;

                                      var paginaDeDetalhes = await _servicoPaginaDeDetalhesDoConteudo
                                                                       .ObterAPaginaDeDetalhesDoConteudo(evento.IdDoConteudo)
                                                                       .ConfigureAwait(false);

                                      var usuario = await _servicoUsuario.ObterUsuarioPorId(evento.IdDoUsuario).ConfigureAwait(false);

                                      while (retry)
                                      {
                                          try
                                          {
                                              var paginaDeDetalhesParaEditar = await _repositorioDasPaginasDeDetalhes
                                                                                         .GetByIdentity(paginaDeDetalhes.Identity, evento.Date).ConfigureAwait(false);

                                              paginaDeDetalhesParaEditar
                                                  .Reavaliar(new PaginaDeDetalhesDoConteudoConteudoReavaliado(paginaDeDetalhesParaEditar, evento)
                                                                 {
                                                                     NomeDoUsuario = usuario.Nome
                                                                 });

                                              await _repositorioDasPaginasDeDetalhes.Save(paginaDeDetalhesParaEditar).ConfigureAwait(false);

                                              retry = false;
                                          }
                                          catch (ReadModelConcurrencyException)
                                          {


                                          }
                                      }
                                  });
        }
    }
}
