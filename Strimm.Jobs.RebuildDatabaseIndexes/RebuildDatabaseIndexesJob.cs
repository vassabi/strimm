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

namespace Strimm.Jobs.RebuildDatabaseIndexes
{
    [Export(typeof(IStrimmJob))]
    public class RebuildDatabaseIndexesJob : BaseJob
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(RebuildDatabaseIndexesJob));

        public RebuildDatabaseIndexesJob()
            : base(typeof(RebuildDatabaseIndexesJob).Name)
        {
        }

        public override void Execute(IJobExecutionContext context)
        {
            Logger.Debug(String.Format("Exectuing 'RebuildDatabaseIndexesJob' job at {0}", DateTime.Now.ToShortTimeString()));

            var errors = new List<string>();

            try
            {
                SystemManage.RebuildAllIndexesOnDatabase();
                Logger.Debug("Successfully rebuild all indexes on the database");
            }
            catch (Exception ex)
            {
                Logger.Error("Error occured while rebuilding database indexes on a database!!!!", ex);
            }
        }
    }
}
