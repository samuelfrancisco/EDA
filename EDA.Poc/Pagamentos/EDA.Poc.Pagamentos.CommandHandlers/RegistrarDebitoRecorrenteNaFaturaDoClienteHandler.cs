using EDA.Poc.Infraestrutura.EventSourcing.Interfaces;
using EDA.Poc.Infraestrutura.Messaging.Handling;
using EDA.Poc.Infraestrutura.Messaging.Handling.Interfaces;
using EDA.Poc.Pagamentos.Commands;
using EDA.Poc.Pagamentos.Domain;
using MassTransit;
using System.Threading.Tasks;

namespace EDA.Poc.Pagamentos.CommandHandlers
{
    public class RegistrarDebitoRecorrenteNaFaturaDoClienteHandler : CommandHandler<RegistrarDebitoRecorrenteNaFaturaDoCliente>
    {
        private readonly IEventSourcedRepository<Cliente> _repositorioDosClientes;

        public RegistrarDebitoRecorrenteNaFaturaDoClienteHandler(ICommandHandlerRepository commandHandlerRepository,
            IEventSourcedRepository<Cliente> repositorioDosClientes)
            : base(commandHandlerRepository)
        {
            _repositorioDosClientes = repositorioDosClientes;
        }

        public override Task Consume(ConsumeContext<RegistrarDebitoRecorrenteNaFaturaDoCliente> context)
        {
            return ExecuteConsume(context.Message, async (comando) =>
                                                             {
                                                                 var cliente = await _repositorioDosClientes.GetById(comando.IdDoCliente).ConfigureAwait(false);

                                                                 cliente.RegistrarDebitoRecorrenteNaFatura(comando);

                                                                 await _repositorioDosClientes.Save(cliente, comando.Id).ConfigureAwait(false);
                                                             });
        }
    }
}
