using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using EDA.Poc.Infraestrutura.EventSourcing.Interfaces;

namespace EDA.Poc.Infraestrutura.Installers
{
    public class RepositoryInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes
                                   .FromThisAssembly()
                                   .BasedOn(typeof(IEventSourcedFactory<>))
                                   .WithService.Base()
                                   .LifestyleTransient());

            container.Register(Classes
                                   .FromThisAssembly()
                                   .BasedOn(typeof(IEventSourcedRepository<>))
                                   .WithService.Base()
                                   .LifestyleTransient());
        }
    }
}
