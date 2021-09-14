using log4net;
using Microsoft.Owin.Hosting;
using Ninject;
using Ninject.Web.Common.OwinHost;
using Owin;
using System.Net.Http.Formatting;
using Newtonsoft.Json.Serialization;
using Microsoft.Owin.StaticFiles;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin;
using Quartz;
using Quartz.Impl;
using Strimm.AutomationWinService.Config;
using Strimm.AutomationWinService.Jobs;
using System;
using System.ComponentModel.Composition.Hosting;
using System.Web.Http;
using Topshelf;
using Ninject.Web.Common;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using System.Web;
using System.Reflection;
using Ninject.Web.WebApi;
using System.Configuration;

namespace Strimm.AutomationWinService
{
    public class JobsService : ServiceControl
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(JobsService));
        
        private IDisposable webApplication;
        private IKernel kernel;
        private IJobManager jobManager;
   
        public JobsService(IJobManager manager)
        {
            this.jobManager = manager;

            string webApiUrl = ConfigurationManager.AppSettings["WebApiHostAndPort"].ToString();

            this.webApplication = WebApp.Start(webApiUrl, (appBuilder) => 
                {
                    appBuilder.UseNinjectMiddleware(CreateKernel);
                    (new StartupConfig()).Configuration(appBuilder);
                });

        }

        #region Public Methods

        /// <summary>
        /// TopShelf's method delegated to <see cref="Start()"/>.
	    /// </summary>
	    public bool Start()
	    {
            Logger.Debug("Start method called");

            try
            {
                this.jobManager.ScheduleJobs();
            }
            catch (Exception ex)
            {
                Logger.Error("JobService failed to start/schedule jobs", ex);
                throw;
            }

	        return true;
	    }

        /// <summary>
        /// TopShelf's method delegated to <see cref="Stop()"/>.
        /// </summary>
        public bool Stop()
	    {
            Logger.Debug("Stop method called");

            try
            {
                this.jobManager.Shutdown();
                this.Dispose();
            }
            catch (Exception ex)
            {
                Logger.Error("JobService failed to stop jobs", ex);
                throw;
            }

            return true;
	    }

        public bool Pause()
        {
            Logger.Debug("Pause method called");

            try
            {
                this.jobManager.PauseJobs();
            }
            catch (Exception ex)
            {
                Logger.Error("JobService failed to pause jobs", ex);
                throw;
            }

            return true;
        }

        public bool Resume()
        {
            Logger.Debug("Resume method called");

            try
            {
                this.jobManager.ResumeJobs();
            }
            catch (Exception ex)
            {
                Logger.Error("JobService failed to resume jobs", ex);
                throw;
            }

            return true;
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public virtual void Dispose()
        {
            Logger.Debug("Disposing web application");

            if (webApplication != null)
            {
                webApplication.Dispose();
            }
        }

        #endregion

        #region Private Methods

        protected virtual ISchedulerFactory CreateSchedulerFactory()
        {
            return new StdSchedulerFactory();
        }

        private IKernel CreateKernel()
        {
            this.kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                kernel.Load(Assembly.GetExecutingAssembly());

                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        #endregion

        public bool Start(HostControl hostControl)
        {
            return this.Start();
        }

        public bool Stop(HostControl hostControl)
        {
            return this.Stop();
        }
    }
}
