using log4net;
using Strimm.Jobs.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.AutomationWinService.Jobs
{
    public class JobFactory : IJobFactory
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(JobFactory));

        private CompositionContainer compositionContainer;

        /// <summary>
        /// Collection of all automation jobs
        /// </summary>
        [ImportMany(typeof(IStrimmJob))]
        public List<IStrimmJob> AutomationJobs { get; set; }

        public JobFactory()
        {
        }

        public void DiscoverJobs()
        {
            Logger.Debug("Discovering strimm jobs");

            try
            {
                var catalog = new AggregateCatalog();
                catalog.Catalogs.Add(new DirectoryCatalog(AppDomain.CurrentDomain.BaseDirectory));

                this.compositionContainer = new CompositionContainer(catalog);
                this.compositionContainer.ComposeParts(this);
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to build jobs catalog using mef", ex.InnerException);
                throw ex;
            }
        }

        public List<IStrimmJob> GetJobs()
        {
            return this.AutomationJobs;
        }
    }
}
