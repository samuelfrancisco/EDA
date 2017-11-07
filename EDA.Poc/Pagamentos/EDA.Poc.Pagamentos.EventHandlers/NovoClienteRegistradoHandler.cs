using System.Threading.Tasks;
using EDA.Poc.Infraestrutura.EventSourcing.Interfaces;
using EDA.Poc.Infraestrutura.Messaging.Handling;
using EDA.Poc.Infraestrutura.Messaging.Handling.Interfaces;
using EDA.Poc.IntegracaoTemporaria.Events;
using EDA.Poc.Pagamentos.Domain;
using MassTransit;

namespace EDA.Poc.Pagamentos.EventHandlers
{
    public class NovoClienteRegistradoHandler : EventHandler, IEventHandler<NovoClienteRegistrado>
    {
        private readonly IEventSourcedRepository<Cliente> _repositorioDosClientes;

        public NovoClienteRegistradoHandler(IEventHandlerRepository eventHandlerRepository, IEventSourcedRepository<Cliente> repositorioDosClientes)
            : base(eventHandlerRepository)
        {
            _repositorioDosClientes = repositorioDosClientes;
        }

        public Task Consume(ConsumeContext<NovoClienteRegistrado> context)
        {
            return ExecuteConsume(context.Message, async evento =>
                                                             {
                                                                 var cliente = await _repositorioDosClientes.GetById(evento.IdDoCliente).ConfigureAwait(false);

                                                                 if (cliente != null) return;

                                                                 cliente = new Cliente(evento.IdDoCliente);

                                                                 await _repositorioDosClientes.Save(cliente, evento.Id).ConfigureAwait(false);
                                                             });
        }
    }
}
