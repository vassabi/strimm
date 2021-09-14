using Common.Logging;
using Quartz;
using Strimm.Jobs.Core;
using StrimmBL;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Jobs.ArchiveOldVideoSchedules
{
    [Export(typeof(IStrimmJob))]
    public class ArchiveOldVideoSchedulesJob : BaseJob
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ArchiveOldVideoSchedulesJob));

        private int DAY_BUFFER = 7;

        public ArchiveOldVideoSchedulesJob()
            : base(typeof(ArchiveOldVideoSchedulesJob).Name)
        {
            Int32.TryParse(this.JobAppSettings.Settings["ArchiveDayBufferForChannelSchedules"].Value.ToString(), out DAY_BUFFER);
        }

        public override void Execute(IJobExecutionContext context)
        {
            Logger.Debug(String.Format("Exectuing 'AutoPilotJob' job at {0}", DateTime.Now.ToShortTimeString()));
            var errors = new List<string>();

            DateTime priorToDate = DateTime.MinValue;

            try 
            {
                priorToDate = DateTime.Now.AddDays(-DAY_BUFFER);

                ScheduleManage.ArchiveOldChannelSchedules(priorToDate);
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Error occured while archiving channel schedules prior to '{0}'", priorToDate.ToShortDateString()), ex);
            }
        }
    }
}
