using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using EDA.Poc.Infraestrutura.Messaging;
using EDA.Poc.Infraestrutura.Messaging.Interfaces;
using EDA.Poc.Pagamentos.EventHandlers;
using GreenPipes;
using MassTransit;
using System;
using System.Configuration;

namespace EDA.Poc.Worker.ConsoleApplication.Installers
{
    public class EventBusInstaller : IWindsorInstaller
    {
        private readonly string _rabbitMqUri = ConfigurationManager.AppSettings["RabbitMQUri"];
        private readonly string _rabbitMQUserName = ConfigurationManager.AppSettings["RabbitMQUserName"];
        private readonly string _rabbitMQPassword = ConfigurationManager.AppSettings["RabbitMQPassword"];

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            var eventBusControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
                                                                      {
                                                                          var host = cfg.Host(new Uri(_rabbitMqUri), h =>
                                                                                                                         {
                                                                                                                             h.Username(_rabbitMQUserName);
                                                                                                                             h.Password(_rabbitMQPassword);
                                                                                                                         });


                                                                          cfg.ReceiveEndpoint(host, "events_queue", ep =>
                                                                                                                        {
                                                                                                                            ep.Consumer(typeof(NovoClienteRegistradoHandler), container.Resolve);

                                                                                                                            ep.Consumer(typeof(NovoConteudoCadastradoHandler), container.Resolve);

                                                                                                                            ep.Consumer(typeof(ProcessoDeCompraHandler), container.Resolve);

                                                                                                                            ep.Consumer(typeof(ProcessoDeCobrancaRecorrenteHandler), container.Resolve);

                                                                                                                            ep.Consumer(typeof(Pagamentos.Denormalizers.PaginaInicialHandler), container.Resolve);

                                                                                                                            ep.Consumer(typeof(Pagamentos.Denormalizers.PaginaDeDetalhesDoConteudoHandler), container.Resolve);

                                                                                                                            ep.Consumer(typeof(Pagamentos.Denormalizers.PaginaDeLicencasDoClienteHandler), container.Resolve);

                                                                                                                            ep.Consumer(typeof(Pagamentos.Denormalizers.PaginaDeLicencasDaEmpresaHandler), container.Resolve);

                                                                                                                            ep.Consumer(typeof(Pagamentos.Denormalizers.PaginaDeLicencasDoUsuarioHandler), container.Resolve);

                                                                                                                            ep.Consumer(typeof(Pagamentos.Denormalizers.DetalhesDaLicencaHandler), container.Resolve);

                                                                                                                            ep.Consumer(typeof(Pagamentos.Denormalizers.LicencasHandler), container.Resolve);

                                                                                                                            ep.Consumer(typeof(IntegracaoLMS.NotificacaoDeCompraDeConteudo), container.Resolve);
                                                                                                                        });

                                                                          cfg.UseRetry(x => x.Exponential(5, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(2)));
                                                                      });

            container.Register(Component.For<IBusControl>().Named("eventBusControl").Instance(eventBusControl));

            var eventBus = new EventBus(container.Resolve<IBusControl>("eventBusControl"));
            container.Register(Component.For<IEventBus>().Instance(eventBus));
        }
    }
}