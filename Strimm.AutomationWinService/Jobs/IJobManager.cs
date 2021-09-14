using Quartz;
using Strimm.AutomationWinService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.AutomationWinService.Jobs
{
    public interface IJobManager
    {
        void ScheduleJobs();

        void StopJobs();

        void PauseJobs();

        void ResumeJobs();

        void Shutdown();

        List<StrimmJobModel> GetAllScheduledJobs();

        void DeleteJob(string jobKey);

        void PauseJob(string jobKey);

        void ResumeJob(string jobKey);

        List<SchedulerMetaData> GetSchedulersData();
    }
}
