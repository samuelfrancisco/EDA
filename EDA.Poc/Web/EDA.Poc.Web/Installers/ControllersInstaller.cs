using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace EDA.Poc.Web.Installers
{
    using Plumbing;
    using System.Web.Http.Controllers;

    public class ControllersInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Classes.
                    FromThisAssembly().
                    BasedOn<IController>().
                    If(c => c.Name.EndsWith("Controller")).
                    LifestyleTransient());

            container.Register(
                Classes.
                    FromThisAssembly().
                    BasedOn<IHttpController>().
                    If(c => c.Name.EndsWith("Controller")).
                    LifestyleTransient());

            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(container));
        }
    }
}