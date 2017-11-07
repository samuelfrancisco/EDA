using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using EDA.Poc.Infraestrutura.EventSourcing.Interfaces;
using EDA.Poc.Infraestrutura.EventStore.EventSourcing;
using EventStore.ClientAPI;
using System.Net;

namespace EDA.Poc.Infraestrutura.EventStore.Installers
{
    public class EventStoreInstaller : IWindsorInstaller
    {
        private readonly IPEndPoint IntegrationTestTcpEndPoint = new IPEndPoint(IPAddress.Loopback, 1113);

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            var eventStoreConnection = EventStoreConnection.Create(IntegrationTestTcpEndPoint);

            eventStoreConnection.ConnectAsync().Wait();

            container.Register(Component.For<IEventStoreConnection>().Named("eventStoreConnection").Instance(eventStoreConnection));

            container.Register(Component.For(typeof(IEventStore))
                                   .ImplementedBy(typeof(EventStoreImpl))
                                   .DependsOn(Dependency.OnComponent(typeof(IEventStoreConnection), "eventStoreConnection"))
                                   .LifeStyle.Transient);
        }
    }
}
