using System;
using Castle.Windsor;
using Castle.Windsor.Installer;
using EDA.Poc.Infraestrutura.MongoDb.Installers;
using EDA.Poc.Pagamentos.Consultas.MongoDb.Installers;

namespace EDA.Poc.IntegracaoTemporaria
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

            windsorContainer.Install(new RepositoryInstaller());

            windsorContainer.Install(new ServiceInstaller());

            windsorContainer.Install(FromAssembly.This());

            return windsorContainer;
        }
    }
}
