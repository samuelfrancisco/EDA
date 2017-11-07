using System;
using Castle.Windsor;
using Castle.Windsor.Installer;

namespace EDA.Poc.Web.App_Start
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
            var container = new WindsorContainer();

            container.Install(new Infraestrutura.EventStore.Installers.EventStoreInstaller());

            container.Install(new Infraestrutura.MongoDb.Installers.RepositoryInstaller());

            container.Install(new Pagamentos.Consultas.MongoDb.Installers.ServiceInstaller());

            container.Install(FromAssembly.This());

            container.Install(new Infraestrutura.Installers.ServiceInstaller());

            return new ContainerBootstrapper(container);
        }

        public void Dispose()
        {
            Container.Dispose();
        }
    }
}