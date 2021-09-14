using log4net;
using Quartz;
using Strimm.Jobs.Core;
using StrimmBL;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Jobs.RemoveOldVideoSchedules
{
    [Export(typeof(IStrimmJob))]
    public class RemoveOldVideoSchedulesJob : BaseJob
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(RemoveOldVideoSchedulesJob));

        private int numberOfDaysThreshold;
        private DateTime asOfDate;

        public RemoveOldVideoSchedulesJob()
            : base(typeof(RemoveOldVideoSchedulesJob).Name)
        {
            this.numberOfDaysThreshold = Int32.Parse(this.JobAppSettings.Settings["NumberOfDaysThreshold"].Value);

            this.numberOfDaysThreshold = this.numberOfDaysThreshold < 2 ? 2 : this.numberOfDaysThreshold;

            this.asOfDate = DateTime.Now.AddDays(-this.numberOfDaysThreshold);
        }

        public override void Execute(IJobExecutionContext context)
        {
            Logger.Info("Executing 'RemoveAllRestrictedAndDeletedVideosJob' job");

            ScheduleManage.RemoveOldVideoSchedulesAsOfDate(this.asOfDate);
        }
    }
}
