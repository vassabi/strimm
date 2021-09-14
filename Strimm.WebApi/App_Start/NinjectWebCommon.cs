using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Web.Common;
using Strimm.Data.Interfaces;
using Strimm.Data.Repositories;
using Strimm.WebApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Strimm.WebApi.App_Start.NinjectWebCommon), "Start")]

namespace Strimm.WebApi.App_Start
{
    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        public static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                // This does not work. Getting the following exception
                //An exception of type 'Ninject.ActivationException' occurred in Ninject.dll but was not handled in user code
                //Additional information: Error activating ModelValidatorProvider using binding from ModelValidatorProvider to NinjectDefaultModelValidatorProvider
                //A cyclical dependency was detected between the constructors of two services.

                //Activation path:
                //  3) Injection of dependency ModelValidatorProvider into parameter defaultModelValidatorProviders of constructor of type DefaultModelValidatorProviders
                //  2) Injection of dependency DefaultModelValidatorProviders into parameter defaultModelValidatorProviders of constructor of type NinjectDefaultModelValidatorProvider
                //  1) Request for ModelValidatorProvider

                //Suggestions:
                //  1) Ensure that you have not declared a dependency for ModelValidatorProvider on any implementations of the service.
                //  2) Consider combining the services into a single one to remove the cycle.
                //  3) Use property injection instead of constructor injection, and implement IInitializable

                //     if you need initialization logic to be run after property values have been injected

                //GlobalConfiguration.Configuration.DependencyResolver = new WebApiContrib.IoC.Ninject.NinjectResolver(kernel);
                // or
                //GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(kernel);

                RegisterServices(kernel);

                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IStrimmIdentityService>().To<StrimmIdentityService>().InRequestScope();
            kernel.Bind<IUserRepository>().To<UserRepository>().InRequestScope();
            kernel.Bind<IVideoTubeRepository>().To<VideoTubeRepository>().InRequestScope();
        }
    }
}