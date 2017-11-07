using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using EDA.Poc.Infraestrutura.Messaging;
using EDA.Poc.Infraestrutura.Messaging.Interfaces;
using GreenPipes;
using MassTransit;
using System;
using System.Configuration;

namespace EDA.Poc.Web.Installers
{
    public class CommandBusInstaller : IWindsorInstaller
    {
        private readonly string _rabbitMqUri = ConfigurationManager.AppSettings["RabbitMQUri"];
        private readonly string _rabbitMQUserName = ConfigurationManager.AppSettings["RabbitMQUserName"];
        private readonly string _rabbitMQPassword = ConfigurationManager.AppSettings["RabbitMQPassword"];
        private readonly string _commandSchedulerQueueFullUri = ConfigurationManager.AppSettings["CommandSchedulerQueueFullUri"];

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            var commandBusControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
                                                                        {
                                                                            var host = cfg.Host(new Uri(_rabbitMqUri), h =>
                                                                                                                           {
                                                                                                                               h.Username(_rabbitMQUserName);
                                                                                                                               h.Password(_rabbitMQPassword);
                                                                                                                           });

                                                                            cfg.UseMessageScheduler(new Uri(_commandSchedulerQueueFullUri));

                                                                            cfg.UseRetry(x => x.Exponential(5, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(2)));
                                                                        });

            container.Register(Component.For<IBusControl>().Named("commandBusControl").Instance(commandBusControl));

            var commandBus = new CommandBus(container.Resolve<IBusControl>("commandBusControl"));
            container.Register(Component.For<ICommandBus>().Instance(commandBus));
        }
    }
}
