using Strimm.Data.Interfaces;
using Strimm.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using System.Diagnostics.Contracts;
using log4net;
using Strimm.Model.Projections;

namespace Strimm.Data.Repositories
{
    public class ChannelScheduleRepository : RepositoryBase, IChannelScheduleRepository
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ChannelScheduleRepository));

        public ChannelScheduleRepository()
            : base()
        {

        }

        public List<ChannelSchedule> GetAllChannelSchedules()
        {
            var channels = new List<ChannelSchedule>();

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    channels = this.StrimmDbConnection.Query<ChannelSchedule>("strimm.GetAllChannelSchedules", null, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to retrieve all channel schedules", ex);
            }

            return channels;
        }

        public List<ChannelSchedule> GetChannelSchedulesByDateAndChannelTubeId(DateTime startTime, int channelTubeId)
        {
            Logger.Info(String.Format("Retrieving channel schedules for date '{0}' and channel tube Id={1}", startTime, channelTubeId));

            Contract.Requires(channelTubeId > 0, "ChannelTubeId should be greater then 0");
            Contract.Requires(startTime > DateTime.MinValue && startTime < DateTime.MaxValue, "Channel schedule start date is invalid");

            var channels = new List<ChannelSchedule>();

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    channels = this.StrimmDbConnection.Query<ChannelSchedule>("strimm.GetChannelSchedulesByDateAndChannelTubeId", new { StartTime = startTime, ChannelTubeId = channelTubeId }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve channel schedules for date '{0}' and channel tube Id={1}", startTime, channelTubeId), ex);
            }

            return channels;
        }

        public void RemoveOldVideoSchedulesAsOfDate(DateTime asOfDate)
        {
            Contract.Requires(asOfDate > DateTime.MinValue && asOfDate < DateTime.Now, "Invalid as of date specified on request to remove all old video schedules");

            Logger.Info(String.Format("Deleting old video schedules as of: {0}", asOfDate.ToString("yyyy-MM-dd")));

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    this.StrimmDbConnection.Execute("strimm.RemoveOldVideoSchedulesAsOfDate", new { AsOfDate = asOfDate.ToString("yyyy-MM-dd") }, null, null, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to delete old video schedules as of {0}", asOfDate.ToString("yyyy-MM-dd")), ex);
            }
        }

        public ChannelSchedule GetChannelScheduleById(int channelScheduleId)
        {
            Contract.Requires(channelScheduleId > 0, "ChannelScheduleId should be greater then 0");

            ChannelSchedule channel = null;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    var results = this.StrimmDbConnection.Query<ChannelSchedule>("strimm.GetChannelScheduleById", new { ChannelScheduleId = channelScheduleId }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                    channel = results.Count == 1 ? results.ToList().First() : null;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Error occured while retrieving channel schedule by Id={0}", channelScheduleId), ex);
            }

            return channel;
        }

        public ChannelSchedulePo GetChannelSchedulePoById(int channelScheduleId)
        {
            Contract.Requires(channelScheduleId > 0, "ChannelScheduleId should be greater then 0");

            ChannelSchedulePo channel = null;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    var results = this.StrimmDbConnection.Query<ChannelSchedulePo>("strimm.GetChannelSchedulePoById", new { ChannelScheduleId = channelScheduleId }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                    channel = results.Count == 1 ? results.ToList().First() : null;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Error occured while retrieving channel schedule by Id={0}", channelScheduleId), ex);
            }

            return channel;
        }

        public List<ChannelSchedule> GetChannelSchedulesByChannelTubeId(int channelTubeId)
        {
            Contract.Requires(channelTubeId > 0, "ChannelTubeId should be greater then 0");

            var channels = new List<ChannelSchedule>();

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    channels = this.StrimmDbConnection.Query<ChannelSchedule>("strimm.GetChannelSchedulesByChannelTubeId", new { ChannelTubeId = channelTubeId }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Error occured while retrieving channel schedules by channelTubeId={0}", channelTubeId), ex);
            }

            return channels;
        }
        public bool DeleteVideoFromScheduleOnDrop(int channelScheduleId, int videoTubeId, int playbackOrderNumber)
        {
            Contract.Requires(channelScheduleId > 0, "ChannelScheduleId should be greater then 0");

            bool isSuccess = false;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    this.StrimmDbConnection.Execute("strimm.dd_DeleteVideoTubeFromChannelSchedule", new { ChannelScheduleId = channelScheduleId, VideoTubeId = videoTubeId, PlaybackOrderNumber=playbackOrderNumber }, null, 30, commandType: CommandType.StoredProcedure);
                    isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to delete channel schedule with Id={0}", channelScheduleId), ex);
            }

            return isSuccess;
        }
        public bool ReorderSchedule(int channelScheduleId, int targetPlaybackOrderNumber, int playbackOrderNumber, int videoTubeId)
           
        {
            Contract.Requires(channelScheduleId > 0, "ChannelScheduleId should be greater then 0");

            bool isSuccess = false;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    this.StrimmDbConnection.Execute("strimm.dd_ChangeVideoTubePlaybackOrderNumberInChannelSchedule", new { ChannelScheduleId = channelScheduleId, PlaybackOrderNumber = playbackOrderNumber, TargetPlaybackOrderNumber = targetPlaybackOrderNumber, VideoTubeId = videoTubeId }, null, 30, commandType: CommandType.StoredProcedure);
                    isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to delete channel schedule with Id={0}", channelScheduleId), ex);
            }

            return isSuccess;
        }
        public bool DeleteChannelScheduleById(int channelScheduleId)
        {
            Contract.Requires(channelScheduleId > 0, "ChannelScheduleId should be greater then 0");

            bool isSuccess = false;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    this.StrimmDbConnection.Execute("strimm.DeleteChannelScheduleById", new { ChannelScheduleId = channelScheduleId }, null, 30, commandType: CommandType.StoredProcedure);
                    isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to delete channel schedule with Id={0}", channelScheduleId), ex);
            }

            return isSuccess;
        }

        public bool InsertChannelSchedule(ChannelSchedule channelSchedule)
        {
            Contract.Requires(channelSchedule != null, "ChannelSchedule cannot be null");
            Contract.Requires(channelSchedule.ChannelTubeId == 0, "ChannelTubeId should be equal to 0");
            Contract.Requires(channelSchedule.StartTime > DateTime.Now, "Channel start time should be in the future");

            bool isSuccess = false;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    this.StrimmDbConnection.Execute("strimm.InsertChannelSchedule", channelSchedule, null, 30, commandType: CommandType.StoredProcedure);
                    isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to insert channel schedule for channel Tube Id={0}, starting at '{1}'", channelSchedule.ChannelTubeId, channelSchedule.StartTime.ToString()), ex);
            }

            return isSuccess;
        }

        public bool UpdateChannelSchedule(ChannelSchedule channelSchedule)
        {
            Contract.Requires(channelSchedule != null, "ChannelSchedule cannot be null");
            Contract.Requires(channelSchedule.ChannelTubeId > 0, "ChannelTubeId should be greater then 0");

            bool isSuccess = false;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    this.StrimmDbConnection.Execute("strimm.UpdateChannelSchedule", channelSchedule, null, 30, commandType: CommandType.StoredProcedure);
                    isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to update channel schedule with Id={0}", channelSchedule.ChannelScheduleId), ex);
            }

            return isSuccess;
        }

        public List<ChannelTubeScheduleCalendarEvent> GetChannelTubeScheduleCalendarEvents(int month, int year, int channelTubeId)
        {
            Contract.Requires(month >= 1 && month <= 12, "Invalid month specified");
            Contract.Requires(year > 1970 && year < 9999, "Invalid year specified");
            Contract.Requires(channelTubeId > 0, "Invalid channel tube id specified");

            var events = new List<ChannelTubeScheduleCalendarEvent>();

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    events = this.StrimmDbConnection.Query<ChannelTubeScheduleCalendarEvent>("strimm.GetChannelTubeScheduleCalendarEvents", 
                            new { 
                                ChannelTubeId = channelTubeId,
                                Month = month,
                                Year = year
                            }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Error occured while retrieving channel schedule events for calendar using channelTubeId={0}, month={1} and year={2}", channelTubeId, month, year), ex);
            }

            return events;
        }

        public List<ChannelTubeScheduleCalendarEvent> GetChannelTubeScheduleCalendarEvents(int day, int month, int year, int channelTubeId)
        {
            Contract.Requires(month >= 1 && month <= 12, "Invalid month specified");
            Contract.Requires(year > 1970 && year < 9999, "Invalid year specified");
            Contract.Requires(day > 0 && day < 31, "Invalid day specified");
            Contract.Requires(channelTubeId > 0, "Invalid channel tube id specified");

            var events = new List<ChannelTubeScheduleCalendarEvent>();

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    events = this.StrimmDbConnection.Query<ChannelTubeScheduleCalendarEvent>("strimm.GetChannelTubeScheduleCalendarEvents",
                            new
                            {
                                ChannelTubeId = channelTubeId,
                                Month = month,
                                Year = year,
                                Day = day
                            }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Error occured while retrieving channel schedule events for calendar using channelTubeId={0}, day={1}, month={2} and year={3}", channelTubeId, day, month, year), ex);
            }

            return events;
        }

        public ChannelSchedule InsertChannelSchedule(int channelTubeId, DateTime startDateAndTime)
        {
            Contract.Requires(channelTubeId > 0, "Invalid channel tube id specified");
            Contract.Requires(startDateAndTime > DateTime.MinValue && startDateAndTime <= DateTime.MaxValue, "Invalid schedule start date and time specified");

            ChannelSchedule channelSchedule = null;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    var results = this.StrimmDbConnection.Query<ChannelSchedule>("strimm.InsertChannelScheduleWithGet",
                            new
                            {
                                ChannelTubeId = channelTubeId,
                                StartTime = startDateAndTime
                            }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();

                    channelSchedule = results.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to create channel schedule for channel with Id={0} at start date and time of '{1}'", channelTubeId, startDateAndTime));
            }

            return channelSchedule;
        }

        public List<ChannelSchedule> GetChannelSchedulesByChannelTubeIdAndDate(int channelTubeId, DateTime targetDate)
        {
            Contract.Requires(channelTubeId > 0, "Invalid channel tube id specified");
            Contract.Requires(targetDate > DateTime.MinValue && targetDate <= DateTime.MaxValue, "Invalid schedule date specified");

            var channelSchedules = new List<ChannelSchedule>();

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    channelSchedules = this.StrimmDbConnection.Query<ChannelSchedule>("strimm.GetChannelSchedulesByChannelTubeIdAndDate",
                            new
                            {
                                ChannelTubeId = channelTubeId,
                                TargetDate = targetDate
                            }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve channel schedules for channel with Id={0} and target date '{1}'", channelTubeId, targetDate));
            }

            return channelSchedules;
        }

        public ChannelSchedule UpdateChannelScheduleStartDateAndTimeById(int channelScheduleId, DateTime startDateAndTime)
        {
            Contract.Requires(channelScheduleId > 0, "Invalid channel schedule id specified");
            Contract.Requires(startDateAndTime > DateTime.MinValue && startDateAndTime <= DateTime.MaxValue, "Invalid schedule date specified");

            ChannelSchedule schedule = null;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    var results = this.StrimmDbConnection.Query<ChannelSchedule>("strimm.UpdateChannelScheduleStartDateAndTimeByIdWithGet",
                            new
                            {
                                ChannelTubeId = channelScheduleId,
                                StartTime = startDateAndTime
                            }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                    schedule = results.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to update channel schedule with Id={0} and new start date and time of '{1}'", channelScheduleId, startDateAndTime));
            }

            return schedule;
        }

        public bool UpdatePublishFlagForChannelScheduleById(int channelScheduleId, bool shouldPublish)
        {
            Contract.Requires(channelScheduleId > 0, "Invalid channel schedule id specified");

            bool isSuccess = false;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    this.StrimmDbConnection.Execute("strimm.UpdatePublishFlagForChannelScheduleById", new { ChannelScheduleId = channelScheduleId, ShouldPublish = shouldPublish }, 
                                            null, 30, commandType: CommandType.StoredProcedure);
                    isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to update channel schedule with Id={0} publish flag to '{1}'", channelScheduleId, shouldPublish), ex);
            }

            return isSuccess;
        }

        public List<UnpublishedChannelSchedulePo> GetAllUnpublishedFutureSchedules()
        {
            var futureSchedules = new List<UnpublishedChannelSchedulePo>();

            try
            {
                futureSchedules = this.StrimmDbConnection.Query<UnpublishedChannelSchedulePo>("strimm.GetAllUnpublishedFutureSchedules", null, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error("Error occured while retrieving all unpublished future channel schedules", ex);
            }

            return futureSchedules;
        }

        public List<UnpublishedChannelScheduleEmailPo> GetUnpublishedChannelScheduleEmailPoForAllUnpublishedChannelSchedules()
        {
            var unpublishedEmailPos = new List<UnpublishedChannelScheduleEmailPo>();

            try
            {
                unpublishedEmailPos = this.StrimmDbConnection.Query<UnpublishedChannelScheduleEmailPo>("strimm.GetUnpublishedChannelScheduleEmailPoForAllUnpublishedChannelSchedules", null, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
            }
            catch (Exception ex)
            {
                Logger.Error("Error occured while retrieving all unpublished channel schedules with the counts of emails sent", ex);
            }

            return unpublishedEmailPos;
        }

        public ChannelSchedule RepeatChannelScheduleByChannelScheduleIdAndTargetDateWithGet(int channelScheduleId, DateTime targetDate)
        {
            Contract.Requires(channelScheduleId > 0, "Invalid channel schedule id specified");
            Contract.Requires(targetDate > DateTime.MinValue && targetDate <= DateTime.MaxValue, "Invalid schedule date specified");

            ChannelSchedule schedule = null;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    var results = this.StrimmDbConnection.Query<ChannelSchedule>("strimm.RepeatChannelScheduleByChannelScheduleIdAndTargetDateWithGet",
                            new
                            {
                                ChannelScheduleId = channelScheduleId,
                                TargetDate = targetDate
                            }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                    schedule = results.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to repeat channel schedule with Id={0} and new start date of '{1}'", channelScheduleId, targetDate.ToString("MM/dd/yyyy")), ex);
            }

            return schedule;
        }

        public void DeleteEmptySchedulesByChannelIdOnOrBeforeDate(int channelTubeId, DateTime scheduleDate)
        {
            Contract.Requires(channelTubeId > 0, "Invalid channel tube id specified");
            Contract.Requires(scheduleDate > DateTime.MinValue && scheduleDate <= DateTime.MaxValue, "Invalid schedule date specified");

            ChannelSchedule schedule = null;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    this.StrimmDbConnection.Execute("strimm.DeleteEmptySchedulesByChannelIdOnOrBeforeDate",
                            new
                            {
                                ChannelTubeId = channelTubeId,
                                TargetDate = scheduleDate
                            }, null, 30, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to delete empty schedules for channel with Id={0} that were created prior or on date '{1}'", channelTubeId, scheduleDate.ToString("MM/dd/yyyy")), ex);
            }
        }

        public void DeleteAllSchedulesByChannelId(int channelTubelId)
        {
            Contract.Requires(channelTubelId > 0, "Invalid channel tube id specified");

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    this.StrimmDbConnection.Execute("strimm.DeleteAllSchedulesByChannelId",
                            new
                            {
                                ChannelTubeId = channelTubelId
                            }, null, 30, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to delete all schedules for channel with Id={0}", channelTubelId, ex));
            }
        }

        public void DeleteFutureChannelSchedulesWithMatureContentByUserId(int userId)
        {
            Contract.Requires(userId > 0, "Invalid user id specified");

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    this.StrimmDbConnection.Execute("strimm.DeleteFutureChannelSchedulesWithMatureContentByUserId",
                            new
                            {
                                UserId = userId
                            }, null, 30, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to delete future channel schedules for user with Id={0}", userId, ex));
            }
        }

        public void ArchiveOldChannelSchedulesPriorToDate(DateTime priorToDate)
        {
            Contract.Requires(priorToDate > DateTime.MinValue, "Invalid prior date specified");
            Contract.Requires(priorToDate < DateTime.Now.AddDays(-2), "Invalid prior date specified");

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    this.StrimmDbConnection.Execute("strimm.ClearOldChannelTubeVideoTubeRecords",
                            new
                            {
                                AsOfDate = priorToDate.ToString("yyyy-MM-dd")
                            }, null, 30, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to archive channel schedules prior to date '{0}'", priorToDate.ToString("yyyy-MM-dd"), ex));
            }
        }
    }
}
