using EDA.Poc.Web.Plumbing;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.UI.WebControls;

namespace EDA.Poc.Web.App_Start
{
    public class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = System.Web.Http.RouteParameter.Optional }
            );

            var containerBootstrapper = WindsorActivator.Bootstrapper;
            var container = containerBootstrapper.Container;
            config.DependencyResolver = new WindsorResolver(container);
        }
    }
}
