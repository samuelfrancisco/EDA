using System.Threading.Tasks;
using EDA.Poc.Infraestrutura.EventSourcing.Interfaces;
using EDA.Poc.Infraestrutura.Messaging.Handling;
using EDA.Poc.Infraestrutura.Messaging.Handling.Interfaces;
using EDA.Poc.Pagamentos.Commands;
using EDA.Poc.Pagamentos.Domain;
using MassTransit;

namespace EDA.Poc.Pagamentos.CommandHandlers
{
    public class ConcluirOrdemDeCompraHandler : CommandHandler<ConcluirOrdemDeCompra>
    {
        private readonly IEventSourcedRepository<OrdemDeCompra> _repositorioDasOrdensDeCompra;

        public ConcluirOrdemDeCompraHandler(ICommandHandlerRepository commandHandlerRepository,
            IEventSourcedRepository<OrdemDeCompra> repositorioDasOrdensDeCompra)
            : base(commandHandlerRepository)
        {
            _repositorioDasOrdensDeCompra = repositorioDasOrdensDeCompra;
        }

        public override Task Consume(ConsumeContext<ConcluirOrdemDeCompra> context)
        {
            return ExecuteConsume(context.Message, async comando =>
                                                             {
                                                                 var ordemDeCompra = await _repositorioDasOrdensDeCompra.GetById(comando.IdDaOrdemDeCompra)
                                                                                               .ConfigureAwait(false);

                                                                 ordemDeCompra.Concluir(comando);

                                                                 await _repositorioDasOrdensDeCompra.Save(ordemDeCompra, comando.Id).ConfigureAwait(false);
                                                             });
        }
    }
}
