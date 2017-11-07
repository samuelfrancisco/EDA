using System.Linq;
using System.Threading.Tasks;
using EDA.Poc.Infraestrutura.Messaging.Handling;
using EDA.Poc.Infraestrutura.Messaging.Handling.Interfaces;
using EDA.Poc.Pagamentos.Consultas.PaginaDeDetalhesDoConteudo;
using EDA.Poc.Pagamentos.Consultas.WebAPI;
using EDA.Poc.Pagamentos.Events;
using EDA.Poc.Pagamentos.ReadModel.WebAPI;
using MassTransit;

namespace EDA.Poc.Pagamentos.Denormalizers
{
    public class LicencasHandler : EventHandler, IEventHandler<LicencaDeConteudoRegistradaParaCliente>
    {
        private readonly IServicoLicencasAdquiridas _servicoLicencasAdquiridas;
        private readonly IServicoPaginaDeDetalhesDoConteudo _servicoPaginaDeDetalhesDoConteudo;

        public LicencasHandler(IEventHandlerRepository eventHandlerRepository,
            IServicoLicencasAdquiridas servicoLicencasAdquiridas,
            IServicoPaginaDeDetalhesDoConteudo servicoPaginaDeDetalhesDoConteudo)
            : base(eventHandlerRepository)
        {
            _servicoLicencasAdquiridas = servicoLicencasAdquiridas;
            _servicoPaginaDeDetalhesDoConteudo = servicoPaginaDeDetalhesDoConteudo;
        }

        public Task Consume(ConsumeContext<LicencaDeConteudoRegistradaParaCliente> context)
        {
            return ExecuteConsume(context.Message,
                                  async evento =>
                                  {
                                      const string siglaDoIdioma = "pt-BR";

                                      var licenca = await _servicoLicencasAdquiridas.ObterLicencaPeloId(evento.IdDaLicenca)
                                                              .ConfigureAwait(false);

                                      if (licenca != null) return;

                                      var conteudo = await _servicoPaginaDeDetalhesDoConteudo
                                                               .ObterAPaginaDeDetalhesDoConteudo(evento.IdDoConteudo)
                                                               .ConfigureAwait(false);

                                      licenca = new Licenca
                                                    {
                                                        IdDoCliente = evento.IdDoCliente,
                                                        SiglaDoIdioma = siglaDoIdioma,
                                                        IdDaLicenca = evento.IdDaLicenca,
                                                        IdDoConteudo = evento.IdDoConteudo,
                                                        QuantidadeDeUsuarios = evento.QuantidadeDeUsuarios,
                                                        DataDeInicio = evento.DisponibilizarAPartirDe,
                                                        QuantidadeDeMeses = evento.QuantidadeDeMeses,
                                                        Titulo = conteudo.Detalhes.Single(x => x.SiglaDoIdioma == siglaDoIdioma).Titulo,
                                                        Objetivo = conteudo.Detalhes.Single(x => x.SiglaDoIdioma == siglaDoIdioma).Objetivo,
                                                        Descricao = conteudo.Detalhes.Single(x => x.SiglaDoIdioma == siglaDoIdioma).Descricao,
                                                        CargaHoraria = conteudo.Detalhes.Single(x => x.SiglaDoIdioma == siglaDoIdioma).CargaHoraria,
                                                        ImagemIlustrativa = conteudo.Detalhes.Single(x => x.SiglaDoIdioma == siglaDoIdioma).Imagem,
                                                        Banner = string.Empty,
                                                        Link = conteudo.Detalhes.Single(x => x.SiglaDoIdioma == siglaDoIdioma).Link,
                                                        Conteudo = conteudo.Detalhes.Single(x => x.SiglaDoIdioma == siglaDoIdioma).Conteudo,
                                                    };

                                      await _servicoLicencasAdquiridas.Adicionar(licenca).ConfigureAwait(false);
                                  });
        }
    }
}
