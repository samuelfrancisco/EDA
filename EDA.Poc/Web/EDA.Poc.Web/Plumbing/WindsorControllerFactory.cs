using System;
using System.Web.Mvc;
using System.Web.Routing;
using Castle.Windsor;

namespace EDA.Poc.Web.Plumbing
{
    public class WindsorControllerFactory : DefaultControllerFactory
    {
        readonly IWindsorContainer _container;

        public WindsorControllerFactory(IWindsorContainer container)
        {
            _container = container;
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {            
			if (controllerType != null && _container.Kernel.HasComponent(controllerType))
				return (IController)_container.Resolve(controllerType);

			return base.GetControllerInstance(requestContext, controllerType);
        }

        public override void ReleaseController(IController controller)
        {
            if(controller == null) return;

            var controllerType = controller.GetType();

            if (_container.Kernel.HasComponent(controllerType))
            {
                _container.Release(controller);
                return;
            }

            base.ReleaseController(controller);
        }
    }
}