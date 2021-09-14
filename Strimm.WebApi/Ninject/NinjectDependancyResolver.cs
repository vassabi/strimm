using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;

namespace Strimm.WebApi.Ninject
{
    public class NinjectDependancyResolver : NinjectDependancyScope, IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependancyResolver(IKernel kernel)
            : base (kernel)
        {
            this.kernel = kernel;
        }

        public IDependencyScope BeginScope()
        {
            return new NinjectDependancyScope(kernel.BeginBlock());
        }
    }
}