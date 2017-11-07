using System.Linq;
using System.Threading.Tasks;
using EDA.Poc.Infraestrutura.EventSourcing.Interfaces;
using EDA.Poc.Infraestrutura.Messaging.Handling;
using EDA.Poc.Infraestrutura.Messaging.Handling.Interfaces;
using EDA.Poc.IntegracaoTemporaria.Events;
using EDA.Poc.Pagamentos.Domain;
using MassTransit;

namespace EDA.Poc.Pagamentos.EventHandlers
{
    public class NovoConteudoCadastradoHandler : EventHandler, IEventHandler<NovoConteudoCadastrado>
    {
        private readonly IEventSourcedRepository<Conteudo> _repositorioDosConteudos;

        public NovoConteudoCadastradoHandler(IEventHandlerRepository eventHandlerRepository, IEventSourcedRepository<Conteudo> repositorioDosConteudos)
            : base(eventHandlerRepository)
        {
            _repositorioDosConteudos = repositorioDosConteudos;
        }

        public Task Consume(ConsumeContext<NovoConteudoCadastrado> context)
        {
            return ExecuteConsume(context.Message, async evento =>
                                                             {
                                                                 var conteudo = await _repositorioDosConteudos.GetById(evento.IdDoConteudo).ConfigureAwait(false);

                                                                 if (conteudo != null) return;

                                                                 var faixasDePrecos = evento.FaixaDePrecos
                                                                     .Select(x => new ConteudoFaixaDePreco
                                                                                      {
                                                                                          Preco = x.Preco,
                                                                                          QuantidadeInicialDeLicencas = x.QuantidadeInicialDeLicencas,
                                                                                          QuantidadeFinalDeLicencas = x.QuantidadeFinalDeLicencas
                                                                                      }).ToList();

                                                                 var promocoes = evento.Promocoes
                                                                     .Select(x => new ConteudoPromocao
                                                                                      {
                                                                                          IdDaPromocao = x.IdDaPromocao,
                                                                                          TipoDeDesconto = x.TipoDeDesconto,
                                                                                          ValorDoDesconto = x.ValorDoDesconto,
                                                                                          Inicio = x.Inicio,
                                                                                          Fim = x.Fim
                                                                                      }).ToList();

                                                                 conteudo = new Conteudo(evento.IdDoConteudo, evento.PrecoSemLimitesDeUsuarios,
                                                                                         faixasDePrecos, promocoes);

                                                                 await _repositorioDosConteudos.Save(conteudo, evento.Id).ConfigureAwait(false);
                                                             });
        }
    }
}
