using Castle.Windsor;
using Castle.Windsor.Installer;
using EDA.Poc.Pagamentos.Consultas.MongoDb.Installers;
using System;

namespace EDA.Poc.Worker.ConsoleApplication
{
    public class ContainerBootstrapper : IContainerAccessor, IDisposable
    {
        readonly IWindsorContainer _container;

        ContainerBootstrapper(IWindsorContainer container)
        {
            _container = container;
        }

        public IWindsorContainer Container
        {
            get { return _container; }
        }

        public static ContainerBootstrapper Bootstrap()
        {
            var container = CreateContainer();

            return new ContainerBootstrapper(container);
        }

        public void Dispose()
        {
            Container.Dispose();
        }

        private static IWindsorContainer CreateContainer()
        {
            var windsorContainer = new WindsorContainer();

            windsorContainer.Install(new Infraestrutura.Installers.RepositoryInstaller());

            windsorContainer.Install(new Infraestrutura.EventStore.Installers.EventStoreInstaller());

            windsorContainer.Install(new Infraestrutura.MongoDb.Installers.RepositoryInstaller());

            windsorContainer.Install(new Pagamentos.CommandHandlers.Installers.HandlersInstaller());

            windsorContainer.Install(new Pagamentos.EventHandlers.Installers.HandlersInstaller());

            windsorContainer.Install(new Pagamentos.Denormalizers.Installers.HandlersInstaller());

            windsorContainer.Install(new IntegracaoLMS.Installers.HandlersInstaller());

            windsorContainer.Install(new ServiceInstaller());

            windsorContainer.Install(FromAssembly.This());

            return windsorContainer;
        }
    }
}