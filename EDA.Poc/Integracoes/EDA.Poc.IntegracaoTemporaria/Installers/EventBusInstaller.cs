using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using EDA.Poc.Infraestrutura.Messaging;
using EDA.Poc.Infraestrutura.Messaging.Interfaces;
using GreenPipes;
using MassTransit;
using System;
using System.Configuration;

namespace EDA.Poc.IntegracaoTemporaria.Installers
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

                                                                          cfg.UseRetry(x => x.Exponential(5, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(2)));
                                                                      });

            container.Register(Component.For<IBusControl>().Named("eventBusControl").Instance(eventBusControl));

            var eventBus = new EventBus(container.Resolve<IBusControl>("eventBusControl"));
            container.Register(Component.For<IEventBus>().Instance(eventBus));
        }
    }
}