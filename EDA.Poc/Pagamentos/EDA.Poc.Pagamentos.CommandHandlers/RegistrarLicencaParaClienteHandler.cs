using System.Threading.Tasks;
using EDA.Poc.Infraestrutura.EventSourcing.Interfaces;
using EDA.Poc.Infraestrutura.Messaging.Handling;
using EDA.Poc.Infraestrutura.Messaging.Handling.Interfaces;
using EDA.Poc.Pagamentos.Commands;
using EDA.Poc.Pagamentos.Domain;
using MassTransit;

namespace EDA.Poc.Pagamentos.CommandHandlers
{
    public class RegistrarLicencaParaClienteHandler : CommandHandler<RegistrarLicencaParaCliente>
    {
        private readonly IEventSourcedRepository<Cliente> _repositorioDosClientes;

        public RegistrarLicencaParaClienteHandler(ICommandHandlerRepository commandHandlerRepository,
            IEventSourcedRepository<Cliente> repositorioDosClientes)
            : base(commandHandlerRepository)
        {
            _repositorioDosClientes = repositorioDosClientes;
        }

        public override Task Consume(ConsumeContext<RegistrarLicencaParaCliente> context)
        {
            return ExecuteConsume(context.Message, async comando =>
                                                             {
                                                                 var cliente = await _repositorioDosClientes.GetById(comando.IdDoCliente).ConfigureAwait(false);

                                                                 cliente.RegistrarLicencaDeConteudo(comando);

                                                                 await _repositorioDosClientes.Save(cliente, comando.Id).ConfigureAwait(false);
                                                             });
        }
    }
}
