using Strimm.Jobs.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.AutomationWinService.Jobs
{
    public interface IJobFactory
    {
        void DiscoverJobs();

        List<IStrimmJob> GetJobs();
    }
}
