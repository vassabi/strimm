using Ninject;
using Quartz;
using Quartz.Impl;
using Strimm.AutomationWinService.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.AutomationWinService.Modules.IoC
{
    public class JobServiceModule : Ninject.Modules.NinjectModule
    {
        public override void Load()
        {
            Bind<IJobManager>().To<JobManager>().InSingletonScope();
            Bind<IJobFactory>().To<JobFactory>().InSingletonScope();
        }
    }
}

