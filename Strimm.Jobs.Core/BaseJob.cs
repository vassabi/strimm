using log4net;
using Quartz;
using System;
using System.Configuration;
using System.IO;

namespace Strimm.Jobs.Core
{
    public abstract class BaseJob : IStrimmJob
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(BaseJob));

        private string cronJobSchedule;
        private string jobName;
        private string triggerName;

        private static string triggerGroup = "StrimmJobs";
        private static string appSettingName = "Cron.Schedule";

        private AppSettingsSection jobAppSettings;
        private bool isJobActive;
        private bool isScheduledJob;

        public BaseJob(string name)
        {
            this.jobName = name;
            this.triggerName = String.Format("{0}_Trigger", this.jobName);

            SetCronJobScheduleFromConfigFile();

            Logger.Debug(String.Format("Retrieved cron job schedule '{0}' for job '{1}'", this.cronJobSchedule, this.jobName));
        }

        public string CronJobSchedule
        {
            get
            {
                return this.cronJobSchedule;
            }
        }

        public string JobName
        {
            get
            {
                return this.jobName;
            }
        }

        public string TriggerName
        {
            get
            {
                return this.triggerName;
            }
        }

        public virtual ITrigger GetTrigger()
        {

            ITrigger trigger = TriggerBuilder.Create()
                                             .WithIdentity(triggerName, triggerGroup)
                                             .WithCronSchedule(this.CronJobSchedule)
                                             .StartNow()
                                             .Build();

            Logger.Debug(String.Format("Created job trigger named '{0}'", triggerName));

            return trigger;
        }

        public AppSettingsSection JobAppSettings
        {
            get
            {
                return this.jobAppSettings;
            }
        }

        public bool IsJobActive
        {
            get
            {
                return this.isJobActive;
            }
        }

        public bool IsScheduledJob
        {
            get
            {
                return this.isScheduledJob;
            }
        }

        public abstract void Execute(IJobExecutionContext context);

        private void SetCronJobScheduleFromConfigFile()
        {
            ExeConfigurationFileMap map = new ExeConfigurationFileMap();
            Configuration libConfig;

            try
            {
                map.ExeConfigFilename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, String.Format(@"Strimm.Jobs.{0}.dll.config", this.jobName.Substring(0, this.jobName.Length - 3)));
                
                libConfig = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);
                this.jobAppSettings = (libConfig.GetSection("appSettings") as AppSettingsSection);

                this.cronJobSchedule = this.JobAppSettings.Settings[appSettingName].Value;
                this.isJobActive = Boolean.Parse(this.JobAppSettings.Settings["IsActive"].Value);

                this.isScheduledJob = true;
                if (String.IsNullOrEmpty(cronJobSchedule.Trim()))
                {
                    this.isScheduledJob = false;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve and set CRON job schedule details by reading '{0}'", map.ExeConfigFilename), ex);
            }
        }
    }
}
