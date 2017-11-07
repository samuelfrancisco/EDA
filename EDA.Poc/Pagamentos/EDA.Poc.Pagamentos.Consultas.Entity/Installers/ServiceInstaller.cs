using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace EGuru.MarketPlace.Loja.Consultas.Entity.Installers
{
    public class ServiceInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromThisAssembly()
                                   .Where(x => x.Name.Contains("Servico"))
                                   .WithServiceDefaultInterfaces()
                                   .LifestyleTransient());
        }
    }
}
