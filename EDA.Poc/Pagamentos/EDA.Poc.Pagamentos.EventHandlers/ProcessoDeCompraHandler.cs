using System.Threading.Tasks;
using EDA.Poc.Infraestrutura.Messaging.Handling;
using EDA.Poc.Infraestrutura.Messaging.Handling.Interfaces;
using EDA.Poc.Infraestrutura.Processes.Interfaces;
using EDA.Poc.Pagamentos.Domain;
using EDA.Poc.Pagamentos.Events;
using MassTransit;

namespace EDA.Poc.Pagamentos.EventHandlers
{
    public class ProcessoDeCompraHandler : EventHandler,
        IEventHandler<OrdemDeCompraCriada>,
        IEventHandler<CobrancaDaCompraGeradaParaOCliente>,
        IEventHandler<LicencaDeConteudoRegistradaParaCliente>
    {
        private readonly IEventSourcedProcessManagerRepository<ProcessoDeCompra> _repositorioDosProcessosDeCompra;

        public ProcessoDeCompraHandler(IEventHandlerRepository eventHandlerRepository,
            IEventSourcedProcessManagerRepository<ProcessoDeCompra> repositorioDosProcessosDeCompra)
            : base(eventHandlerRepository)
        {
            _repositorioDosProcessosDeCompra = repositorioDosProcessosDeCompra;
        }

        public Task Consume(ConsumeContext<OrdemDeCompraCriada> context)
        {
            return ExecuteConsume(context.Message, async evento =>
                                                             {
                                                                 var processo = await _repositorioDosProcessosDeCompra
                                                                                          .GetById(evento.IdDoProcessoDeCompra)
                                                                                          .ConfigureAwait(false)
                                                                                ?? new ProcessoDeCompra(evento.IdDoProcessoDeCompra);

                                                                 processo.NaCriacaoDaOrdemDeCompra(evento);

                                                                 await _repositorioDosProcessosDeCompra.Save(processo, evento.Id).ConfigureAwait(false);
                                                             });
        }

        public Task Consume(ConsumeContext<CobrancaDaCompraGeradaParaOCliente> context)
        {
            return ExecuteConsume(context.Message, async evento =>
                                                             {
                                                                 var processo = await _repositorioDosProcessosDeCompra
                                                                                          .GetById(evento.IdDoProcessoDeCompra)
                                                                                          .ConfigureAwait(false);

                                                                 processo.NaGeracaoDaCobrancaDaCompra(evento);

                                                                 await _repositorioDosProcessosDeCompra.Save(processo, evento.Id).ConfigureAwait(false);
                                                             });
        }

        public Task Consume(ConsumeContext<LicencaDeConteudoRegistradaParaCliente> context)
        {
            return ExecuteConsume(context.Message, async evento =>
                                                             {
                                                                 var processo = await _repositorioDosProcessosDeCompra
                                                                                          .GetById(evento.IdDoProcessoDeCompra)
                                                                                          .ConfigureAwait(false);

                                                                 processo.NoRegistroDaLicenca(evento);

                                                                 await _repositorioDosProcessosDeCompra.Save(processo, evento.Id).ConfigureAwait(false);
                                                             });
        }
    }
}
