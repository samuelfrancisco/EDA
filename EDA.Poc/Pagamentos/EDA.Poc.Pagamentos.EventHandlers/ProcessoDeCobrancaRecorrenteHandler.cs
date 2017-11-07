using EDA.Poc.Infraestrutura.Messaging.Handling;
using EDA.Poc.Infraestrutura.Messaging.Handling.Interfaces;
using EDA.Poc.Infraestrutura.Processes.Interfaces;
using EDA.Poc.Pagamentos.Domain;
using EDA.Poc.Pagamentos.Events;
using MassTransit;
using System.Threading.Tasks;

namespace EDA.Poc.Pagamentos.EventHandlers
{
    public class ProcessoDeCobrancaRecorrenteHandler : EventHandler,
        IEventHandler<CobrancaRecorrenteRegistrada>,
        IEventHandler<DebitoRecorrenteRegistradoNaFaturaDoCliente>
    {
        private readonly IEventSourcedProcessManagerRepository<ProcessoDeCobrancaRecorrente> _repositorioDosProcessosDeCobrancaRecorrente;

        public ProcessoDeCobrancaRecorrenteHandler(IEventHandlerRepository eventHandlerRepository,
            IEventSourcedProcessManagerRepository<ProcessoDeCobrancaRecorrente> repositorioDosProcessosDeCobrancaRecorrente)
            : base(eventHandlerRepository)
        {
            _repositorioDosProcessosDeCobrancaRecorrente = repositorioDosProcessosDeCobrancaRecorrente;
        }

        public Task Consume(ConsumeContext<CobrancaRecorrenteRegistrada> context)
        {
            return ExecuteConsume(context.Message, async evento =>
                                                             {
                                                                 var processo = await _repositorioDosProcessosDeCobrancaRecorrente
                                                                                          .GetById(evento.IdDoProcessoDeCobrancaRecorrente)
                                                                                          .ConfigureAwait(false)
                                                                                ?? new ProcessoDeCobrancaRecorrente(evento.IdDoProcessoDeCobrancaRecorrente);

                                                                 processo.NoRegistroDaCobrancaRecorrente(evento);

                                                                 await _repositorioDosProcessosDeCobrancaRecorrente.Save(processo, evento.Id).ConfigureAwait(false);
                                                             });
        }
        public Task Consume(ConsumeContext<DebitoRecorrenteRegistradoNaFaturaDoCliente> context)
        {
            return ExecuteConsume(context.Message, async evento =>
                                                             {
                                                                 var processo = await _repositorioDosProcessosDeCobrancaRecorrente
                                                                                          .GetById(evento.IdDoProcessoDeCobrancaRecorrente)
                                                                                          .ConfigureAwait(false);

                                                                 processo.NoRegistroDebitoRecorrenteNaFaturaDoCliente(evento);

                                                                 await _repositorioDosProcessosDeCobrancaRecorrente.Save(processo, evento.Id).ConfigureAwait(false);
                                                             });
        }
    }
}
