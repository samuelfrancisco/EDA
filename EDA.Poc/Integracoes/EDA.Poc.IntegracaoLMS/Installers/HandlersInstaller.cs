using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using MassTransit;

namespace EDA.Poc.IntegracaoLMS.Installers
{
    public class HandlersInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes
                                   .FromThisAssembly()
                                   .BasedOn(typeof(IConsumer<>))
                                   .WithService.Self()
                                   .LifestyleTransient());
        }
    }
}
