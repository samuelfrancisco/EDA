using System.Threading.Tasks;
using EDA.Poc.Infraestrutura.EventSourcing.Interfaces;
using EDA.Poc.Infraestrutura.Messaging.Handling;
using EDA.Poc.Infraestrutura.Messaging.Handling.Interfaces;
using EDA.Poc.Pagamentos.Commands;
using EDA.Poc.Pagamentos.Domain;
using MassTransit;

namespace EDA.Poc.Pagamentos.CommandHandlers
{
    public class GerarCobrancaDaCompraParaOClienteHandler : CommandHandler<GerarCobrancaDaCompraParaOCliente>
    {
        private readonly IEventSourcedRepository<Cliente> _repositorioDeClientes;

        public GerarCobrancaDaCompraParaOClienteHandler(ICommandHandlerRepository commandHandlerRepository, 
            IEventSourcedRepository<Cliente> repositorioDeClientes)
            : base(commandHandlerRepository)
        {
            _repositorioDeClientes = repositorioDeClientes;
        }

        public override Task Consume(ConsumeContext<GerarCobrancaDaCompraParaOCliente> context)
        {
            return ExecuteConsume(context.Message, async comando =>
                                                             {
                                                                 var cliente = await _repositorioDeClientes.GetById(comando.IdDoCliente).ConfigureAwait(false);

                                                                 cliente.GerarCombrancaDaCompra(comando);

                                                                 await _repositorioDeClientes.Save(cliente, comando.Id).ConfigureAwait(false);
                                                             });
        }
    }
}
