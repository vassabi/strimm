using log4net;
using Quartz;
using Strimm.Jobs.Core;
using StrimmBL;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Strimm.Jobs.MailChimpExistingUserManager
{
    [Export(typeof(IStrimmJob))]
    public class MailChimpExistingUserManagerJob : BaseJob
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(MailChimpExistingUserManagerJob));

        private static string MAILING_LIST_ID;

        public MailChimpExistingUserManagerJob()
            : base(typeof(MailChimpExistingUserManagerJob).Name)
        {
            MAILING_LIST_ID = this.JobAppSettings.Settings["MAILCHIMP_LIST_ID"].Value.ToString();
        }

        public override void Execute(IJobExecutionContext context)
        {
            Logger.Debug(String.Format("Exectuing 'MailChimpNewUserManagementJob' job at {0}", DateTime.Now.ToShortTimeString()));

            var errors = new List<string>();

            try
            {
                var users = UserManage.GetUsersRegisteredWithMailChimpForUpdate();

                if (users != null && users.Count > 0)
                {
                    //MailChimpManage.AddOrUpdateAllMailChimpUserSubscriptionsInBatch(users, MAILING_LIST_ID);

                    users.ForEach(x =>
                    {
                        try
                        {
                            Thread.Sleep(1000);

                            MailChimpManage.AddOrUpdateMailingChimpSubscriptionAsync(x, MAILING_LIST_ID);
                        }
                        catch (Exception ex)
                        {
                            Logger.Error(String.Format("Failed to update MailChimp subscription for user with e-mail '{0}'", x.Email));
                        }
                    });
                }
                else
                {
                    Logger.Warn("Unable to find users whose MailChimp subscription requires an update. No users were updated");
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error occured updating subscriptions of MailChimp users", ex);
            }        
        }
    }
}
