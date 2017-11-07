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
    public class PaginaDeLicencasDoClienteHandler : EventHandler,
        IEventHandler<DebitoRegistradoNaFaturaDoCliente>
    {
        private readonly IServicoPaginaDeLicencas _servicoPaginaDeLicencas;
        private readonly IVersionedReadModelRepository<PaginaDeLicencas> _repositorioDePaginasDeLicencas;

        public PaginaDeLicencasDoClienteHandler(IEventHandlerRepository eventHandlerRepository,
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
                                      var paginaDeLicencasDoCliente = await _servicoPaginaDeLicencas.ObterAPaginaDeLicencasDoCliente(evento.IdDoCliente)
                                                                                .ConfigureAwait(false);

                                      var paginaDeLicencasDoClienteParaEditar = await _repositorioDePaginasDeLicencas
                                                                                          .GetByIdentity(paginaDeLicencasDoCliente.Identity, evento.Date)
                                                                                          .ConfigureAwait(false);

                                      paginaDeLicencasDoClienteParaEditar
                                          .RegistrarDebito(new PaginaDeLicencasDebitoRegistradoNaFaturaDoCliente(paginaDeLicencasDoClienteParaEditar, evento));

                                      await _repositorioDePaginasDeLicencas.Save(paginaDeLicencasDoClienteParaEditar).ConfigureAwait(false);
                                  });
        }
    }
}
