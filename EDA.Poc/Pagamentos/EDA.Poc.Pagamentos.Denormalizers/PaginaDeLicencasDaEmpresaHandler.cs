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
    public class PaginaDeLicencasDaEmpresaHandler : EventHandler,
        IEventHandler<DebitoRegistradoNaFaturaDoCliente>
    {
        private readonly IServicoPaginaDeLicencas _servicoPaginaDeLicencas;
        private readonly IVersionedReadModelRepository<PaginaDeLicencas> _repositorioDePaginasDeLicencas;

        public PaginaDeLicencasDaEmpresaHandler(IEventHandlerRepository eventHandlerRepository,
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
                                      //TODO substituir por uma consulta
                                      var idDaEmpresa = System.Guid.Parse("01a2a426-5c95-4c0e-9e22-eb353fc7fa8a");

                                      var paginaDeLicencasDaEmpresa = await _servicoPaginaDeLicencas
                                                                                .ObterAPaginaDeLicencasDaEmpresa(idDaEmpresa)
                                                                                .ConfigureAwait(false);

                                      var paginaDeLicencasDaEmpresaParaEditar = await _repositorioDePaginasDeLicencas
                                                                                          .GetByIdentity(paginaDeLicencasDaEmpresa.Identity, evento.Date)
                                                                                          .ConfigureAwait(false);

                                      paginaDeLicencasDaEmpresaParaEditar
                                          .RegistrarDebito(new PaginaDeLicencasDebitoRegistradoNaFaturaDoCliente(paginaDeLicencasDaEmpresaParaEditar, evento));

                                      await _repositorioDePaginasDeLicencas.Save(paginaDeLicencasDaEmpresaParaEditar).ConfigureAwait(false);
                                  });
        }
    }
}
