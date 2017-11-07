using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace EDA.Poc.Infraestrutura.Installers
{
    public class ServiceInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromThisAssembly()
                                   .Where(x => x.Name.Contains("Servico") || x.Name.Contains("Service"))
                                   .WithService.DefaultInterfaces()
                                   .LifestyleTransient());
        }
    }
}
