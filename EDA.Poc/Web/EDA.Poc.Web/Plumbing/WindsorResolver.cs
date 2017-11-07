using System;
using System.Linq;
using System.Web.Routing;
using Castle.Windsor;
using System.Collections.Generic;
using System.Web.Http.Dependencies;

namespace EDA.Poc.Web.Plumbing
{
    public class WindsorResolver : IDependencyResolver
    {
        protected IWindsorContainer _container;

        public WindsorResolver(IWindsorContainer container)
        {
            _container = container;
        }

        public IDependencyScope BeginScope()
        {
            return new WindsorResolver(_container);
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return _container.Resolve(serviceType);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return _container.ResolveAll<object>(serviceType);
            }
            catch (Exception)
            {
                return new List<object>();
            }
        }

        public void Dispose()
        {
            
        }
    }
}