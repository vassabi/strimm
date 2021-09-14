using log4net;
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

namespace Strimm.Jobs.MoveVideosFromAdminUserToPublicLibrary
{
    [Export(typeof(IStrimmJob))]
    public class MoveVideosFromAdminUserToPublicLibraryJob : BaseJob
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(MoveVideosFromAdminUserToPublicLibraryJob));
        private string publicLibraryOwner;

        public MoveVideosFromAdminUserToPublicLibraryJob()
            : base(typeof(MoveVideosFromAdminUserToPublicLibraryJob).Name)
        {
            this.publicLibraryOwner = this.JobAppSettings.Settings["PublicLibraryOwner"].Value.ToString();
        }

        public override void Execute(IJobExecutionContext context)
        {
            if (String.IsNullOrEmpty(this.publicLibraryOwner))
            {
                Logger.Warn("Unable to executing 'MoveVideosFromAdminUserToPublicLibraryJob' job. Public library owner's username was not set");
                return;
            }

            Logger.Info(String.Format("Executing 'MoveVideosFromAdminUserToPublicLibraryJob' job for Public Library Owner '{0}'", this.publicLibraryOwner));

            VideoTubeManage.MoveUserPrivateVideosToPublicLibrary(this.publicLibraryOwner);
        }
    }
}
