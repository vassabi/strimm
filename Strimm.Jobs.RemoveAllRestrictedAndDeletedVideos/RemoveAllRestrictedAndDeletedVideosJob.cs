using log4net;
using Quartz;
using Strimm.Jobs.Core;
using StrimmBL;
using System;
using System.ComponentModel.Composition;

namespace Strimm.Jobs.RemoveAllRestrictedAndDeletedVideos
{
    [Export(typeof(IStrimmJob))]
    public class RemoveAllRestrictedAndDeletedVideosJob : BaseJob
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(RemoveAllRestrictedAndDeletedVideosJob));

        private int numberOfDaysThreshold;
        private DateTime asOfDate;

        public RemoveAllRestrictedAndDeletedVideosJob()
            : base(typeof(RemoveAllRestrictedAndDeletedVideosJob).Name)
        {
            this.numberOfDaysThreshold = Int32.Parse(this.JobAppSettings.Settings["NumberOfDaysThreshold"].Value);

            this.numberOfDaysThreshold = this.numberOfDaysThreshold < 2 ? 2 : this.numberOfDaysThreshold;

            this.asOfDate = DateTime.Now.AddDays(-this.numberOfDaysThreshold);
        }

        public override void Execute(IJobExecutionContext context)
        {
            Logger.Info("Executing 'RemoveAllRestrictedAndDeletedVideosJob' job");

            VideoTubeManage.RemoveRestrictedAndDeletedVideosAsOfDate(this.asOfDate);
        }
    }
}
