using System.Linq;
using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using EDA.Poc.Infraestrutura.EventSourcing.Interfaces;
using EDA.Poc.Infraestrutura.Messaging.Interfaces;
using EDA.Poc.IntegracaoTemporaria.Events;
using EDA.Poc.Pagamentos.Commands;
using EDA.Poc.Pagamentos.Events;
using MongoDB.Bson.Serialization;
using NEventStore;
using NEventStore.Serialization;
using EDA.Poc.Pagamentos.Domain;

namespace EDA.Poc.Worker.ConsoleApplication.Installers
{
    public class EventStoreInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            var eventStore = ConfigureEventStore();
            container.Register(Component.For<IStoreEvents>().Instance(eventStore));
        }

        private IStoreEvents ConfigureEventStore()
        {
            RegisterCommands();

            RegisterEvents();

            RegisterIntegrationEvents();

            RegisterMementos();

            var store = Wireup.Init()
                .UsingMongoPersistence("MongoDBEventStore", new DocumentObjectSerializer())
                .InitializeStorageEngine()
                .UsingJsonSerialization()
                .Compress()
                .Build();

            return store;
        }

        private void RegisterCommands()
        {
            var type = typeof(ICommand);

            var types = Assembly.GetAssembly(typeof(ComprarConteudo))
                .GetTypes()
                .Where(type.IsAssignableFrom)
                .Where(t => t.IsClass);

            foreach (var t in types)
                BsonClassMap.LookupClassMap(t);
        }

        private void RegisterEvents()
        {
            var type = typeof(IVersionedEvent);

            var types = Assembly.GetAssembly(typeof(ConteudoCadastrado))
                .GetTypes()
                .Where(type.IsAssignableFrom)
                .Where(t => t.IsClass);

            foreach (var t in types)
                BsonClassMap.LookupClassMap(t);
        }

        private void RegisterIntegrationEvents()
        {
            var type = typeof(IEvent);

            var types = Assembly.GetAssembly(typeof(NovoConteudoCadastrado))
                .GetTypes()
                .Where(type.IsAssignableFrom)
                .Where(t => t.IsClass);

            foreach (var t in types)
                BsonClassMap.LookupClassMap(t);
        }

        private void RegisterMementos()
        {
            var type = typeof(IMemento<>);

            var types = Assembly.GetAssembly(typeof(Conteudo))
                .GetTypes()                
                .Where(t => type.IsAssignableFrom(t) || t.GetInterfaces().Any(x => x.IsGenericType && x.GetGenericTypeDefinition() == type))
                .Where(t => t.IsClass);

            foreach (var t in types)
                BsonClassMap.LookupClassMap(t);
        }
    }
}
