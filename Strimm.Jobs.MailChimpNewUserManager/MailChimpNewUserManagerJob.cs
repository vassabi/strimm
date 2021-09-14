using log4net;
using Quartz;
using Strimm.Jobs.Core;
using System;
using StrimmBL;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Strimm.Model.MailChimp;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Threading;
using System.Net;
using System.IO;
using Strimm.Model.Projections;

namespace Strimm.Jobs.MailChimpNewUserManager
{
    [Export(typeof(IStrimmJob))]
    public class MailChimpNewUserManagerJob : BaseJob
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(MailChimpNewUserManagerJob));

        private static string MAILING_LIST_ID;

        public MailChimpNewUserManagerJob()
            : base(typeof(MailChimpNewUserManagerJob).Name)
        {
            MAILING_LIST_ID = this.JobAppSettings.Settings["MAILCHIMP_LIST_ID"].Value.ToString();
        }

        public override void Execute(IJobExecutionContext context)
        {
            Logger.Debug(String.Format("Exectuing 'MailChimpNewUserManagementJob' job at {0}", DateTime.Now.ToShortTimeString()));

            var errors = new List<string>();

            try
            {
                var newUsers = UserManage.GetUsersNotRegisteredWithMailChimp();

                if (newUsers != null && newUsers.Count > 0)
                {
                 
                    //MailChimpManage.AddOrUpdateAllMailChimpUserSubscriptionsInBatch(newUsers, MAILING_LIST_ID);

                    newUsers.ForEach(x =>
                    {
                        try
                        {
                            Thread.Sleep(1000);

                            MailChimpManage.AddOrUpdateMailingChimpSubscriptionAsync(x, MAILING_LIST_ID);
                        }
                        catch (Exception ex)
                        {
                            Logger.Error(String.Format("Failed to process MailChimp subscription for user with e-mail '{0}'", x.Email));
                        }
                    });
                }
                else
                {
                    Logger.Warn("Unable to find recently signed up users. No users were added to the MailChimp's mailing list");
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error occured while processing users that were added within the last 24 hrs. Unable to add them to the MailChimp's mailing list", ex);
            }
        }
    }
}
