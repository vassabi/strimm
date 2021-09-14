using log4net;
using Quartz;
using Strimm.Jobs.Core;
using StrimmBL;
using System;
using System.ComponentModel.Composition;
using System.Configuration;
using System.IO;
using System.Linq;

namespace Strimm.Jobs.VideoTubeStatusUpdater
{
    [Export(typeof(IStrimmJob))]
    public class VideoTubeStatusUpdaterJob : BaseJob
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(VideoTubeStatusUpdaterJob));

        public VideoTubeStatusUpdaterJob()
            : base(typeof(VideoTubeStatusUpdaterJob).Name)
        {
        }

        public override void Execute(IJobExecutionContext context)
        {
            Logger.Debug(String.Format("Executing VideoTubeStatusUpdaterJob at {0}", DateTime.Now.ToShortTimeString()));

            try
            {
                VideoTubeManage.UpdateVideoTubeStatuses();
            }
            catch (Exception ex)
            {
                Logger.Error("Error occured while update video status of system/user videos", ex);
            }
        }
    }
}
