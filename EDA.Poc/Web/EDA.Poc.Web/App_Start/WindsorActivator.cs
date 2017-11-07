using WebActivatorEx;

[assembly: PreApplicationStartMethod(typeof(EDA.Poc.Web.App_Start.WindsorActivator), "PreStart")]
[assembly: ApplicationShutdownMethodAttribute(typeof(EDA.Poc.Web.App_Start.WindsorActivator), "Shutdown")]

namespace EDA.Poc.Web.App_Start
{
    public static class WindsorActivator
    {
        public static ContainerBootstrapper Bootstrapper;

        public static void PreStart()
        {
            Bootstrapper = ContainerBootstrapper.Bootstrap();
        }
        
        public static void Shutdown()
        {
            if (Bootstrapper != null)
                Bootstrapper.Dispose();
        }
    }
}