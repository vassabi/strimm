using log4net;
using Quartz;
using Strimm.Jobs.Core;
using StrimmBL;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;

namespace Strimm.Jobs.AutoPilot
{
    [Export(typeof(IStrimmJob))]
    public class AutoPilotJob : BaseJob
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(AutoPilotJob));

        public AutoPilotJob()
            : base(typeof(AutoPilotJob).Name)
        {
        }

        public override void Execute(IJobExecutionContext context)
        {
            Logger.Debug(String.Format("Exectuing 'AutoPilotJob' job at {0}", DateTime.Now.ToShortTimeString()));
            var errors = new List<string>();

                // Get all channels that have AutoPilot flag set to true
                var channelsOnAutoPilot = ChannelManage.GetAllChannelsOnAutoPilot();
               
                if (channelsOnAutoPilot != null && channelsOnAutoPilot.Count > 0)
                {
                    Logger.Debug(String.Format("Retrieved {0} channels on AutoPilot", channelsOnAutoPilot.Count));

                    // Loop through each channel and do the following:
                    channelsOnAutoPilot.ForEach(c =>
                    {
                        Logger.Debug(String.Format("Processing channel '{0}' with id={1}", c.Name, c.ChannelTubeId));

                        // Define new schedule's start date and time
                        DateTime now = DateTime.Now;
                        DateTime startTime = now.AddHours(-now.Hour)
                                                .AddMinutes(-now.Minute)
                                                .AddSeconds(-now.Second)
                                                .AddDays(1);

                        try
                        {
                            // Create new instant schedule at the pre-determined date and time
                            var existingScheduleEvents = ScheduleManage.GetChannelTubeSchedulesByDate(c.ChannelTubeId, startTime);

                            Logger.Debug(String.Format("Retrieved {0} schedule events for channel id={1} and time='{2}'", existingScheduleEvents != null ? existingScheduleEvents.Count : 0, c.ChannelTubeId, startTime.ToString()));

                            if (existingScheduleEvents.Count() == 0 || !existingScheduleEvents.Any(x => x.StartDateAndTime.Day == startTime.Day &&
                                                                                                        x.StartDateAndTime.Month == startTime.Month &&
                                                                                                        x.StartDateAndTime.Year == startTime.Year))
                            {
                                Logger.Debug(String.Format("Creating instant schedule for channel id={0} and start time '{1}'", c.ChannelTubeId, startTime.ToString()));

                                var channelSchedule = ScheduleManage.CreateInstantSchedule(c.ChannelTubeId, startTime);

                                Logger.Debug(String.Format("Created valid channel schedule? {0}. Channel schedule has videos? {1}. Video count is {2}",
                                    (channelSchedule != null && channelSchedule.ChannelScheduleId > 0),
                                    (channelSchedule != null && channelSchedule.VideoSchedules != null && channelSchedule.VideoSchedules.Count > 0),
                                    (channelSchedule != null && channelSchedule.VideoSchedules != null) ? channelSchedule.VideoSchedules.Count : 0));

                                if (channelSchedule != null && channelSchedule.VideoSchedules != null && channelSchedule.VideoSchedules.Count > 0)
                                {
                                    var playbackStartTime = channelSchedule.VideoSchedules.First().PlaybackStartTime;
                                    var playbackEndTime = channelSchedule.VideoSchedules.Last().PlaybackEndTime;

                                    Logger.Debug(String.Format("Successfully created new channel schedule for channel with Id={0}. Start time '{1}' and end time '{2}'",
                                                            c.ChannelTubeId, playbackStartTime.ToString("MM/dd/yyyy HH:mm:ss tt"), playbackEndTime.ToString("MM/dd/yyyy HH:mm:ss tt")));

                                    if (ScheduleManage.PublishOrUnpublishChannelSchedule(channelSchedule.ChannelScheduleId, true))
                                    {
                                        Logger.Debug(String.Format("Successfully published new channel schedule for channel with Id={0}. Start time '{1}' and end time '{2}'",
                                                        c.ChannelTubeId, playbackStartTime.ToString("MM/dd/yyyy HH:mm:ss tt"), playbackEndTime.ToString("MM/dd/yyyy HH:mm:ss tt")));
                                    }
                                    else
                                    {
                                        Logger.Error(String.Format("Failed to publish new channel schedule for channel with Id={0}. Start time '{1}' and end time '{2}'",
                                                        c.ChannelTubeId, playbackStartTime.ToString("MM/dd/yyyy HH:mm:ss tt"), playbackEndTime.ToString("MM/dd/yyyy HH:mm:ss tt")));

                                    }
                                }
                                else
                                {
                                    Logger.Warn(String.Format("Failed to create new channel schedule for channel with Id={0}, Error Message={1}", c.ChannelTubeId, channelSchedule.Message));

                                    ScheduleManage.DeleteEmptySchedulesForChannelOnOrBeforeDate(c.ChannelTubeId, startTime);

                                    Logger.Debug(String.Format("Deleting empty schedules for channel tube id={0}, on or before '{1}'", c.ChannelTubeId, startTime.ToString()));
                                }
                            }
                            else
                            {
                                Logger.Warn(String.Format("Schedule creation on AutoPilot failed for channel with Id={0}. User already created channel schedules for '{1}", c.ChannelTubeId, startTime));
                            }
                        }
                        catch (Exception jex)
                        {
                            Logger.Error(String.Format("Error occurred while creating schedule for channel tube with id={0} and date/time={1}", c.ChannelTubeId, startTime), jex);
                            errors.Add(jex.Message);
                        }
                    });
                }
                else
                {
                    Logger.Warn("There are no channels with IsAutoPilotOn flag set to true. No channel schedules created.");
                }

        }
    }
}
