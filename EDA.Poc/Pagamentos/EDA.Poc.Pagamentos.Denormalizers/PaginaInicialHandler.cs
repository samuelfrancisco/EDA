using System.Threading.Tasks;
using MassTransit;
using EDA.Poc.Infraestrutura.Messaging.Handling;
using EDA.Poc.Infraestrutura.Messaging.Handling.Interfaces;
using EDA.Poc.Pagamentos.Events;
using EDA.Poc.Pagamentos.ReadModel.PaginaInicial;
using EDA.Poc.Infraestrutura.ReadModel.Interfaces;
using EDA.Poc.Pagamentos.Consultas.PaginaInicial;
using EDA.Poc.Pagamentos.ReadModel.PaginaInicial.Eventos;

namespace EDA.Poc.Pagamentos.Denormalizers
{
    public class PaginaInicialHandler : EventHandler,
        IEventHandler<ConteudoAvaliado>,
        IEventHandler<ConteudoReavaliado>
    {
        private readonly IVersionedReadModelRepository<PaginaInicial> _repositorioDasPaginasIniciais;
        private readonly IServicoPaginaInicial _servicoPaginaInicial;

        public PaginaInicialHandler(IEventHandlerRepository eventHandlerRepository,
            IVersionedReadModelRepository<PaginaInicial> repositorioDasPaginasIniciais,
            IServicoPaginaInicial servicoPaginaInicial)
            : base(eventHandlerRepository)
        {
            _repositorioDasPaginasIniciais = repositorioDasPaginasIniciais;
            _servicoPaginaInicial = servicoPaginaInicial;
        }

        public Task Consume(ConsumeContext<ConteudoAvaliado> context)
        {
            return ExecuteConsume(context.Message,
                                  async evento =>
                                  {
                                      var paginaInicial = await _servicoPaginaInicial.ObterAPaginaInicial().ConfigureAwait(false);

                                      var paginaInicialParaAlterar = await _repositorioDasPaginasIniciais
                                                                               .GetByIdentity(paginaInicial.Identity, evento.Date).ConfigureAwait(false);

                                      paginaInicialParaAlterar
                                          .ConteudoAvaliado(new PaginaInicialConteudoAvaliado(paginaInicialParaAlterar, evento));

                                      await _repositorioDasPaginasIniciais.Save(paginaInicialParaAlterar).ConfigureAwait(false);

                                  });
        }

        public Task Consume(ConsumeContext<ConteudoReavaliado> context)
        {
            return ExecuteConsume(context.Message,
                                  async evento =>
                                  {
                                      var paginaInicial = await _servicoPaginaInicial.ObterAPaginaInicial().ConfigureAwait(false);

                                      var paginaInicialParaAlterar = await _repositorioDasPaginasIniciais
                                                                               .GetByIdentity(paginaInicial.Identity, evento.Date).ConfigureAwait(false);

                                      paginaInicialParaAlterar
                                          .ConteudoReavaliado(new PaginaInicialConteudoReavaliado(paginaInicialParaAlterar, evento));

                                      await _repositorioDasPaginasIniciais.Save(paginaInicialParaAlterar).ConfigureAwait(false);

                                  });
        }
    }
}
