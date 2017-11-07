using System.Configuration;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using EDA.Poc.Infraestrutura.EventSourcing.Interfaces;
using EDA.Poc.Infraestrutura.Messaging.Handling.Interfaces;
using EDA.Poc.Infraestrutura.MongoDb.EventSourcing;
using EDA.Poc.Infraestrutura.MongoDb.Messaging.Handling;
using EDA.Poc.Infraestrutura.MongoDb.Processes;
using EDA.Poc.Infraestrutura.MongoDb.ReadModel;
using EDA.Poc.Infraestrutura.Processes.Interfaces;
using EDA.Poc.Infraestrutura.ReadModel.Interfaces;
using MongoDB.Driver;

namespace EDA.Poc.Infraestrutura.MongoDb.Installers
{
    public class RepositoryInstaller : IWindsorInstaller
    {
        private readonly string _mongoUnpublishedEventStore = ConfigurationManager.ConnectionStrings["MongoUnpublishedEventStore"].ConnectionString;
        private readonly string _mongoProccessManagerCommandStore = ConfigurationManager.ConnectionStrings["MongoProccessManagerCommandStore"].ConnectionString;
        private readonly string _mongoCommandHandlerStore = ConfigurationManager.ConnectionStrings["MongoCommandHandlerStore"].ConnectionString;
        private readonly string _mongoEventHandlerStore = ConfigurationManager.ConnectionStrings["MongoEventHandlerStore"].ConnectionString;
        private readonly string _mongoReadModelStore = ConfigurationManager.ConnectionStrings["MongoReadModelStore"].ConnectionString;
        private readonly string _mongoEventGlobalVersionStore = ConfigurationManager.ConnectionStrings["MongoEventGlobalVersionStore"].ConnectionString;

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            var unpublishedEventMongoClient = new MongoClient(_mongoUnpublishedEventStore);

            container.Register(Component.For<MongoClient>().Named("unpublishedEventMongoClient").Instance(unpublishedEventMongoClient));

            container.Register(Component.For(typeof(IEventSourcedPublisher<>))
                                   .ImplementedBy(typeof(EventSourcedPublisher<>))
                                   .DependsOn(Dependency.OnComponent(typeof(MongoClient), "unpublishedEventMongoClient"))
                                   .LifeStyle.Transient);

            var commandHandlerMongoClient = new MongoClient(_mongoCommandHandlerStore);

            container.Register(Component.For<MongoClient>().Named("commandHandlerMongoClient").Instance(commandHandlerMongoClient));

            container.Register(Component.For<ICommandHandlerRepository>()
                                   .ImplementedBy<CommandHandlerRepository>()
                                   .DependsOn(Dependency.OnComponent(typeof(MongoClient), "commandHandlerMongoClient"))
                                   .LifeStyle.Transient);

            var eventHandlerMongoClient = new MongoClient(_mongoEventHandlerStore);

            container.Register(Component.For<MongoClient>().Named("eventHandlerMongoClient").Instance(eventHandlerMongoClient));

            container.Register(Component.For<IEventHandlerRepository>()
                                   .ImplementedBy<EventHandlerRepository>()
                                   .DependsOn(Dependency.OnComponent(typeof(MongoClient), "eventHandlerMongoClient"))
                                   .LifeStyle.Transient);

            var proccessManagerCommandMongoClient = new MongoClient(_mongoProccessManagerCommandStore);

            container.Register(Component.For<MongoClient>().Named("proccessManagerCommandMongoClient").Instance(proccessManagerCommandMongoClient));

            container.Register(Component.For(typeof(IEventSourcedProcessManagerRepository<>))
                                   .ImplementedBy(typeof(EventSourcedProcessManagerRepository<>))
                                   .DependsOn(Dependency.OnComponent(typeof(MongoClient), "proccessManagerCommandMongoClient"))
                                   .LifeStyle.Transient);

            var readModelMongoClient = new MongoClient(_mongoReadModelStore);

            container.Register(Component.For<MongoClient>().Named("readModelMongoClient").Instance(readModelMongoClient));

            container.Register(Component.For(typeof(IVersionedReadModelRepository<>))
                                   .ImplementedBy(typeof(VersionedReadModelRepository<>))
                                   .DependsOn(Dependency.OnComponent(typeof(MongoClient), "readModelMongoClient"))
                                   .LifeStyle.Transient);

            var eventGlobalVersionMongoClient = new MongoClient(_mongoEventGlobalVersionStore);

            container.Register(Component.For<MongoClient>().Named("eventGlobalVersionMongoClient").Instance(eventGlobalVersionMongoClient));
        }
    }
}