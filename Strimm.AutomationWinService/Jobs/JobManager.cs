using log4net;
using Quartz;
using Quartz.Impl.Matchers;
using Strimm.AutomationWinService.Models;
using Strimm.Jobs.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.AutomationWinService.Jobs
{
    [Export(typeof(IJobManager))]
    public class JobManager : IJobManager
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(JobManager));

        private readonly IJobFactory jobFactory;
        private readonly ISchedulerFactory schedulerFactory;

        public JobManager(ISchedulerFactory schedulerFactory, IJobFactory jobFactory)
        {
            this.schedulerFactory = schedulerFactory;
            this.jobFactory = jobFactory;

            this.jobFactory.DiscoverJobs();
        }

        public void ScheduleJobs()
        {
            Logger.Debug("Building and scheduling Strimm jobs");

            var strimmJobs = this.jobFactory.GetJobs();

            if (strimmJobs != null && strimmJobs.Count > 0)
            {
                Logger.Info(String.Format("Retrieved {0} jobs to initialize", strimmJobs.Count));

                strimmJobs.ForEach(x =>
                {
                    var scheduler = this.schedulerFactory.GetScheduler();
                    scheduler.Start();

                    var jobDetail = JobBuilder.Create(x.GetType())
                                              .RequestRecovery(true)
                                              .StoreDurably(false)
                                              .Build();

                    var job = (x as BaseJob);

                    if (job.IsJobActive) 
                    {
                        if (job.IsScheduledJob)
                        {
                            var trigger = job.GetTrigger();
                            scheduler.ScheduleJob(jobDetail, trigger);
                            Logger.Debug(String.Format("Job '{0}' was initialized and scheduled using trigger named '{1}'", job.JobName, job.TriggerName));
                        }
                        else
                        {
                            job.Execute(null);
                        }
                    }
                    else
                    {
                        Logger.Debug(String.Format("Job '{0}' was initialized but not schedule. Job is not active", job.JobName));
                    }

                });
            }
            else
            {
                Logger.Info("WARNING!!! Retrieved no jobs to initialize");
            }
        }

        public void StopJobs()
        {
            Logger.Info("Stopping all jobs...");

            this.schedulerFactory.AllSchedulers.ToList().ForEach(s =>
                {
                    s.Shutdown(true);
                });

            Logger.Info("All jobs stopped");
        }

        public void PauseJobs()
        {
            Logger.Info("Pausing all jobs...");

            this.schedulerFactory.AllSchedulers.ToList().ForEach(s =>
                {
                    s.PauseAll();
                });

            Logger.Info("All jobs paused");
        }

        public void ResumeJobs()
        {
            Logger.Info("Resuming all jobs...");

            this.schedulerFactory.AllSchedulers.ToList().ForEach(s =>
                {
                    s.ResumeAll();
                });

            Logger.Info("All jobs resumed");
        }

        public void Shutdown()
        {
            this.StopJobs();

            this.schedulerFactory.AllSchedulers.ToList().ForEach(s => s.Shutdown());
        }

        public List<StrimmJobModel> GetAllScheduledJobs()
        {
            var jobs = new List<StrimmJobModel>();

            this.schedulerFactory.AllSchedulers.ToList().ForEach(scheduler =>
                {
                    IList<string> jobGroups = scheduler.GetJobGroupNames();
                    IList<string> triggerGroups = scheduler.GetTriggerGroupNames();

                    foreach (string group in jobGroups)
                    {
                        var groupMatcher = GroupMatcher<JobKey>.GroupContains(group);
                        var jobKeys = scheduler.GetJobKeys(groupMatcher);

                        foreach (var jobKey in jobKeys)
                        {
                            var detail = scheduler.GetJobDetail(jobKey);
                            var triggers = scheduler.GetTriggersOfJob(jobKey);

                            foreach (ITrigger trigger in triggers)
                            {
                                var job = new StrimmJobModel()
                                {
                                    GroupName = group,
                                    JobKey = jobKey.Name,
                                    Description = detail.Description,
                                    TriggerKey = trigger.Key.Name,
                                    TriggerGroup = trigger.Key.Group,
                                    TriggerState = scheduler.GetTriggerState(trigger.Key).ToString(),
                                    TriggerType = trigger.GetType().Name
                                };

                                DateTimeOffset? nextFireTime = trigger.GetNextFireTimeUtc();
                                if (nextFireTime.HasValue)
                                {
                                    job.NextRunTime = nextFireTime.Value.LocalDateTime.ToString();
                                }

                                DateTimeOffset? previousFireTime = trigger.GetPreviousFireTimeUtc();
                                if (previousFireTime.HasValue)
                                {
                                    job.LastRunTime = previousFireTime.Value.LocalDateTime.ToString();
                                }

                                jobs.Add(job);
                            }
                        }
                    }
                });

            return jobs;
        }

        public void DeleteJob(string jobKey)
        {
            this.schedulerFactory.AllSchedulers.ToList().ForEach(scheduler =>
            {
                IList<string> jobGroups = scheduler.GetJobGroupNames();
                IList<string> triggerGroups = scheduler.GetTriggerGroupNames();

                foreach (string group in jobGroups)
                {
                    var groupMatcher = GroupMatcher<JobKey>.GroupContains(group);
                    var jobKeys = scheduler.GetJobKeys(groupMatcher);

                    if (jobKey.Contains(jobKey))
                    {
                        scheduler.DeleteJob(new JobKey(jobKey));
                        break;
                    }
                }
            });
        }

        public void PauseJob(string jobKey)
        {
            this.schedulerFactory.AllSchedulers.ToList().ForEach(scheduler =>
            {
                IList<string> jobGroups = scheduler.GetJobGroupNames();
                IList<string> triggerGroups = scheduler.GetTriggerGroupNames();

                foreach (string group in jobGroups)
                {
                    var groupMatcher = GroupMatcher<JobKey>.GroupContains(group);
                    var jobKeys = scheduler.GetJobKeys(groupMatcher);

                    if (jobKey.Contains(jobKey))
                    {
                        scheduler.PauseJob(new JobKey(jobKey));
                        break;
                    }
                }
            });
        }

        public void ResumeJob(string jobKey)
        {
            this.schedulerFactory.AllSchedulers.ToList().ForEach(scheduler =>
            {
                IList<string> jobGroups = scheduler.GetJobGroupNames();
                IList<string> triggerGroups = scheduler.GetTriggerGroupNames();

                foreach (string group in jobGroups)
                {
                    var groupMatcher = GroupMatcher<JobKey>.GroupContains(group);
                    var jobKeys = scheduler.GetJobKeys(groupMatcher);

                    if (jobKey.Contains(jobKey))
                    {
                        scheduler.ResumeJob(new JobKey(jobKey));
                        break;
                    }
                }
            });
        }

        public List<SchedulerMetaData> GetSchedulersData()
        {
            var data = new List<SchedulerMetaData>();

            this.schedulerFactory.AllSchedulers.ToList().ForEach(scheduler =>
            {
                data.Add(scheduler.GetMetaData());
            });

            return data;
        }
    }
}
