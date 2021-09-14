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
using System.Configuration;

namespace Strimm.Jobs.RemoveRestrictedAndDeletedVideosFromPublicLibrary
{
    [Export(typeof(IStrimmJob))]
    public class RemoveRestrictedAndDeletedVideosFromPublicLibraryJob : BaseJob
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(RemoveRestrictedAndDeletedVideosFromPublicLibraryJob));

        private string publicLibraryOwner;

        public RemoveRestrictedAndDeletedVideosFromPublicLibraryJob()
            : base(typeof(RemoveRestrictedAndDeletedVideosFromPublicLibraryJob).Name)
        {
            this.publicLibraryOwner = this.JobAppSettings.Settings["PublicLibraryOwner"].Value.ToString();
        }

        public override void Execute(IJobExecutionContext context)
        {
            Logger.Info("Executing 'RemoveRestrictedAndDeletedVideosFromPublicLibraryJob' job");

            VideoTubeManage.RemoveRestrictedAndDeletedVideosFromPublicLibrary(publicLibraryOwner);
        }
    }
}
