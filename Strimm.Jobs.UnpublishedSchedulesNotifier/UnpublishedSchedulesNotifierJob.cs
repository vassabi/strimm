using log4net;
using Quartz;
using Strimm.Jobs.Core;
using StrimmBL;
using System;
using System.ComponentModel.Composition;
using System.Configuration;
using System.IO;
using System.Linq;

namespace Strimm.Jobs.UnpublishedSchedulesNotifier
{
    [Export(typeof(IStrimmJob))]
    public class UnpublishedSchedulesNotifierJob : BaseJob
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(UnpublishedSchedulesNotifierJob));

        private int firstNotificationTimeInMin = 24 * 60;
        private int secondNotificationTimeInMin = 15;
        private string emailFolder;
        private string emailFilePath;

        public UnpublishedSchedulesNotifierJob()
            : base(typeof(UnpublishedSchedulesNotifierJob).Name)
        {
            if (!Int32.TryParse(this.JobAppSettings.Settings["FirstUnpublishedEmailNotificationTimeInMin"].ToString(), out firstNotificationTimeInMin))
            {
                firstNotificationTimeInMin = 24 * 60;
            }

            if (!Int32.TryParse(this.JobAppSettings.Settings["SecondUnpublishedEmailNotificationTimeInMin"].ToString(), out secondNotificationTimeInMin))
            {
                secondNotificationTimeInMin = 30;
            }

            string email = this.JobAppSettings.Settings["EmailFilename"].Value;

            this.emailFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, email);
        }

        public override void Execute(IJobExecutionContext context)
        {
            Logger.Debug(String.Format("Executing UnpublishedScheduleNotifierJob at {0}", DateTime.Now.ToShortTimeString()));

            if (!(new FileInfo(this.emailFilePath).Exists))
            {
                Logger.Error(String.Format("Unable to located e-mail template for unpublished channel schedule e-mail notifications using path='{0}'. Aborting job", this.emailFilePath));
                return;
            }

            var unpublishedFutureSchedules = ScheduleManage.GetAllUnpublishedFutureSchedules();
            var unpublishedSchedulesWithSentEmailCounts = ScheduleManage.GetUnpublishedSchedulesWithSentEmailCounts();

            var unpublishedSchedulesToProcess = (from schedule in unpublishedFutureSchedules
                                                join emailCount in unpublishedSchedulesWithSentEmailCounts
                                                    on schedule.ChannelScheduleId equals emailCount.ChannelScheduleId into ec
                                                    from email in ec.DefaultIfEmpty()
                                                select new
                                                {
                                                    ChannelScheduleId = schedule.ChannelScheduleId,
                                                    StartTime = schedule.StartTime,
                                                    UserFirstName = schedule.UserFirstName,
                                                    UserEmail = schedule.UserEmail,
                                                    SentEmailCount = email != null ? email.SentEmailCount : 0,
                                                    ChannelName = schedule.ChannelName
                                                })
                                                .ToList();


            if (unpublishedSchedulesToProcess != null && unpublishedSchedulesToProcess.Count > 0)
            {
                DateTime now = DateTime.Now;

                unpublishedSchedulesToProcess.Where(x => x.SentEmailCount < 2).ToList().ForEach(schedule =>
                {
                    double timeInMinBeforeStart = (schedule.StartTime - now).TotalMinutes;

                    if (schedule.SentEmailCount == 0 && (timeInMinBeforeStart > 60 && timeInMinBeforeStart <= firstNotificationTimeInMin))
                    {
                        if (EmailManage.SendUnpublishedScheduleEmail(schedule.ChannelScheduleId, schedule.ChannelName, schedule.UserFirstName, schedule.UserEmail, schedule.StartTime, this.emailFilePath))
                        {
                            Logger.Debug(String.Format("Successfully sent 1st unpublished channel schedule notification email to user '{0}' for channel '{1}' that starts at '{2}'", schedule.UserFirstName, schedule.ChannelName, schedule.StartTime.ToString()));
                        }
                        else
                        {
                            Logger.Debug(String.Format("Failed to sent 1st unpublished channel schedule notification email to user '{0}' for channel '{1}' that starts at '{2}'", schedule.UserFirstName, schedule.ChannelName, schedule.StartTime.ToString()));
                        }
                    }
                    else if (schedule.SentEmailCount == 1 && (timeInMinBeforeStart > 60 && timeInMinBeforeStart <= secondNotificationTimeInMin))
                    {
                        if (EmailManage.SendUnpublishedScheduleEmail(schedule.ChannelScheduleId, schedule.ChannelName, schedule.UserFirstName, schedule.UserEmail, schedule.StartTime, this.emailFilePath))
                        {
                            Logger.Debug(String.Format("Successfully sent 2nd unpublished channel schedule notification email to user '{0}' for channel '{1}' that starts at '{2}'", schedule.UserFirstName, schedule.ChannelName, schedule.StartTime.ToString()));
                        }
                        else
                        {
                            Logger.Debug(String.Format("Failed to sent 2nd unpublished channel schedule notification email to user '{0}' for channel '{1}' that starts at '{2}'", schedule.UserFirstName, schedule.ChannelName, schedule.StartTime.ToString()));
                        }
                    }
                });
            }
            else
            {
                Logger.Debug("There are no unpublished schedules in the next 24 hrs. Email notifications will not be sent. Job completed");
            }
        }
    }
}
