using EDA.Poc.Infraestrutura.Messaging.Handling.Interfaces;
using EDA.Poc.Pagamentos.Consultas.PaginaDeDetalhesDoConteudo;
using EDA.Poc.Pagamentos.Events;
using EDA.Poc.Pagamentos.ReadModel.WebAPI;
using MassTransit;
using System;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace EDA.Poc.IntegracaoLMS
{
    public class NotificacaoDeCompraDeConteudo : Infraestrutura.Messaging.Handling.EventHandler,
        IEventHandler<LicencaDeConteudoRegistradaParaCliente>
    {

        private readonly IServicoPaginaDeDetalhesDoConteudo _servicoPaginaDeDetalhesDoConteudo;

        public NotificacaoDeCompraDeConteudo(IEventHandlerRepository eventHandlerRepository,
            IServicoPaginaDeDetalhesDoConteudo servicoPaginaDeDetalhesDoConteudo)
            : base(eventHandlerRepository)
        {
            _servicoPaginaDeDetalhesDoConteudo = servicoPaginaDeDetalhesDoConteudo;
        }

        public Task Consume(ConsumeContext<LicencaDeConteudoRegistradaParaCliente> context)
        {
            return ExecuteConsume(context.Message,
                                  async evento =>
                                  {
                                      const string siglaDoIdioma = "pt-BR";

                                      var conteudo = await _servicoPaginaDeDetalhesDoConteudo
                                          .ObterAPaginaDeDetalhesDoConteudo(evento.IdDoConteudo)
                                          .ConfigureAwait(false);

                                      var licenca = new Licenca
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

                                      using (var httpClient = new HttpClient())
                                      {
                                          try
                                          {
                                              httpClient.BaseAddress = new Uri(ConfigurationManager.AppSettings["UrlNotificacaoLMS"]);
                                              httpClient.DefaultRequestHeaders.Accept.Clear();
                                              httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                                              var response = await httpClient
                                                                       .PostAsJsonAsync("lms/admin/cursoonline/NotificacaoCompraConteudoMarketPlace", licenca);

                                              //Todo Logar
                                              if (!response.IsSuccessStatusCode) return;

                                              var content = await response.Content.ReadAsStringAsync();

                                              //Todo Logar
                                              if (content.ToLower() == "false") return;
                                          }
                                          catch (Exception)
                                          {
                                              //throw new Exception("Notificação não ocorreu com sucesso");
                                          }
                                      }
                                  });
        }
    }
}
