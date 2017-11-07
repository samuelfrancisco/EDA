using System.Threading.Tasks;
using EDA.Poc.Infraestrutura.EventSourcing.Interfaces;
using EDA.Poc.Infraestrutura.Messaging.Handling;
using EDA.Poc.Infraestrutura.Messaging.Handling.Interfaces;
using EDA.Poc.Pagamentos.Commands;
using EDA.Poc.Pagamentos.Domain;
using MassTransit;

namespace EDA.Poc.Pagamentos.CommandHandlers
{
    public class AvaliarConteudoHandler : CommandHandler<AvaliarConteudo>
    {
        private readonly IEventSourcedRepository<Conteudo> _repositorioDeConteudos;

        public AvaliarConteudoHandler(ICommandHandlerRepository commandHandlerRepository,
            IEventSourcedRepository<Conteudo> repositorioDeConteudos)
            : base(commandHandlerRepository)
        {
            _repositorioDeConteudos = repositorioDeConteudos;
        }

        public override Task Consume(ConsumeContext<AvaliarConteudo> context)
        {
            return ExecuteConsume(context.Message, async comando => 
                                                       {
                                                           var conteudo = await _repositorioDeConteudos.GetById(comando.IdDoConteudo).ConfigureAwait(false);

                                                           conteudo.Avaliar(comando);

                                                           await _repositorioDeConteudos.Save(conteudo, comando.Id).ConfigureAwait(false);
                                                       });

        }
    }
}
