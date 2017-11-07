using System.Threading.Tasks;
using EDA.Poc.Infraestrutura.EventSourcing.Interfaces;
using EDA.Poc.Infraestrutura.Messaging.Handling;
using EDA.Poc.Infraestrutura.Messaging.Handling.Interfaces;
using EDA.Poc.Pagamentos.Commands;
using EDA.Poc.Pagamentos.Domain;
using MassTransit;

namespace EDA.Poc.Pagamentos.CommandHandlers
{
    public class ComprarConteudoHandler : CommandHandler<ComprarConteudo>
    {
        private readonly IEventSourcedRepository<Conteudo> _repositorioDeConteudos;
        private readonly IEventSourcedRepository<OrdemDeCompra> _repositorioDeOrdensDeCompra;

        public ComprarConteudoHandler(ICommandHandlerRepository commandHandlerRepository,
            IEventSourcedRepository<Conteudo> repositorioDeConteudos,
            IEventSourcedRepository<OrdemDeCompra> repositorioDeOrdensDeCompra)
            : base(commandHandlerRepository)
        {
            _repositorioDeConteudos = repositorioDeConteudos;
            _repositorioDeOrdensDeCompra = repositorioDeOrdensDeCompra;
        }

        public override Task Consume(ConsumeContext<ComprarConteudo> context)
        {
            return ExecuteConsume(context.Message, async comando =>
                                                             {
                                                                 var conteudo = await _repositorioDeConteudos.GetById(comando.IdDoConteudo).ConfigureAwait(false);

                                                                 var ordemDeCompra = conteudo.GerarOrdemDeCompra(comando);

                                                                 await _repositorioDeOrdensDeCompra.Save(ordemDeCompra, comando.Id).ConfigureAwait(false);
                                                             });

        }
    }
}
