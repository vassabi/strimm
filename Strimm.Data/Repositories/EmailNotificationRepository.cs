using log4net;
using Strimm.Data.Interfaces;
using Strimm.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace Strimm.Data.Repositories
{
    public class EmailNotificationRepository: RepositoryBase, IEmailNotificationRepository
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(EmailNotificationRepository));

        public EmailNotificationRepository()
            : base()
        {

        }

        public UnpublishedChannelScheduleEmail InsertUnpublishedChannelScheduleEmail(int channelScheduleId, string userEmail, DateTime timeSent)
        {
            Logger.Info(String.Format("Adding new e-mail notification for unpublished channel schedule with Id={0} sent at '{1}' sent to '{2}'", channelScheduleId, timeSent, userEmail));

            UnpublishedChannelScheduleEmail emailRecord = null;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    var results = this.StrimmDbConnection.Query<UnpublishedChannelScheduleEmail>("strimm.InsertUnpublishedChannelScheduleEmail", 
                                                new { ChannelScheduleId = channelScheduleId, TimeSent = timeSent, UserEmail = userEmail }, 
                                                null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                    emailRecord = results.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to add new e-mail notification for unpublished channel schedule with Id={0} sent at '{1}' sent to '{2}'", channelScheduleId, timeSent, userEmail), ex);
            }
            return emailRecord;
        }
    }
}
