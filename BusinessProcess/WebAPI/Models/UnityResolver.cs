using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;
using Unity;
using Unity.Exceptions;

namespace WebAPI.Models
{
    public class UnityResolver:IDependencyResolver
    {
        private readonly IUnityContainer _theContainer;

        public UnityResolver(IUnityContainer theContainer)
        {
            _theContainer = theContainer;
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return _theContainer.Resolve(serviceType);
            }
            catch(ResolutionFailedException)
            { return null; }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return _theContainer.ResolveAll(serviceType);
            }
            catch(ResolutionFailedException)
            { return new List<object>(); }
        }

        public IDependencyScope BeginScope()
        {
            return new UnityResolver(_theContainer.CreateChildContainer());
        }

        public void Dispose()
        {
            _theContainer.Dispose();
        }
    }
}