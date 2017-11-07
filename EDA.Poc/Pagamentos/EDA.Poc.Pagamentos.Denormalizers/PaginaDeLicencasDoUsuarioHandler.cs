using System.Threading.Tasks;
using EDA.Poc.Infraestrutura.Messaging.Handling;
using EDA.Poc.Infraestrutura.Messaging.Handling.Interfaces;
using EDA.Poc.Infraestrutura.ReadModel.Interfaces;
using EDA.Poc.Pagamentos.Events;
using EDA.Poc.Pagamentos.ReadModel.PaginaDeLicencas;
using EDA.Poc.Pagamentos.ReadModel.PaginaDeLicencas.Eventos;
using MassTransit;
using EDA.Poc.Pagamentos.Consultas.PaginaDeLicencas;

namespace EDA.Poc.Pagamentos.Denormalizers
{
    public class PaginaDeLicencasDoUsuarioHandler : EventHandler,
        IEventHandler<DebitoRegistradoNaFaturaDoCliente>
    {
        private readonly IServicoPaginaDeLicencas _servicoPaginaDeLicencas;
        private readonly IVersionedReadModelRepository<PaginaDeLicencas> _repositorioDePaginasDeLicencas;

        public PaginaDeLicencasDoUsuarioHandler(IEventHandlerRepository eventHandlerRepository,
            IServicoPaginaDeLicencas servicoPaginaDeLicencas,
            IVersionedReadModelRepository<PaginaDeLicencas> repositorioDePaginasDeLicencas)
            : base(eventHandlerRepository)
        {
            _servicoPaginaDeLicencas = servicoPaginaDeLicencas;
            _repositorioDePaginasDeLicencas = repositorioDePaginasDeLicencas;
        }

        public Task Consume(ConsumeContext<DebitoRegistradoNaFaturaDoCliente> context)
        {
            return ExecuteConsume(context.Message,
                                  async evento =>
                                  {
                                      var paginaDeLicencasDoUsuario = await _servicoPaginaDeLicencas.ObterAPaginaDeLicencasDoUsuario(evento.IdDoUsuario)
                                                                                .ConfigureAwait(false);

                                      var paginaDeLicencasDoUsuarioParaEditar = await _repositorioDePaginasDeLicencas
                                                                                          .GetByIdentity(paginaDeLicencasDoUsuario.Identity, evento.Date)
                                                                                          .ConfigureAwait(false);

                                      paginaDeLicencasDoUsuarioParaEditar
                                          .RegistrarDebito(new PaginaDeLicencasDebitoRegistradoNaFaturaDoCliente(paginaDeLicencasDoUsuarioParaEditar, evento));

                                      await _repositorioDePaginasDeLicencas.Save(paginaDeLicencasDoUsuarioParaEditar).ConfigureAwait(false);
                                  });
        }
    }
}
