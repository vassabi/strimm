using log4net;
using Strimm.Model.Projections;
using Strimm.Model.WebModel;
using Strimm.Shared;
using StrimmBL;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;

namespace Strimm.Web.Services
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ScheduleWebService
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ScheduleWebService));
        private static readonly string stringClientDateTimeFormat = "MM/dd/yyyy HH:mm";

        // To use HTTP GET, add [WebGet] attribute. (Default ResponseFormat is WebMessageFormat.Json)
        // To create an operation that returns XML,
        //     add [WebGet(ResponseFormat=WebMessageFormat.Xml)],
        //     and include the following line in the operation body:
        //         WebOperationContext.Current.OutgoingResponse.ContentType = "text/xml";

        /// <summary>
        /// This method will retrieve calendar schedule events for a specified channel for a specified
        /// month, year
        /// </summary>
        /// <param name="month">Target month</param>
        /// <param name="year">Target year</param>
        /// <param name="channelTubeId">Channel Tube Id</param>
        /// <returns></returns>
        [OperationContract]
        public List<ChannelTubeScheduleCalendarEvent> GetChannelTubeScheduleCalendarEvents(int month, int year, int channelTubeId)
        {
            var events = ScheduleManage.GetChannelTubeScheduleCalendarEvents(month, year, channelTubeId);

            if (events.Count == 0)
            {
                DateTime start = DateTime.Now.AddHours(-DateTime.Now.Hour);
                DateTime end = start.AddHours(15);

                events.Add(new ChannelTubeScheduleCalendarEvent()
                {
                    ChannelScheduleId = 1,
                    ChannelTubeId = 1033,
                    IsActive = true,
                    StartTime = start.ToString("MM/dd/yyyy HH:mm:ss tt"),
                    EndTime = start.ToString("MM/dd/yyyy HH:mm:ss tt"),
                    Message = String.Format("Playtime: [{0} - {1}]", start.ToString("HH:mm tt"), end.ToString("HH:mm tt"))
                });
            }

            return events;
        }

        /// <summary>
        /// This method will retrieve videos for a channel that can be used in creating a schedule
        /// </summary>
        /// <param name="pageIndex">Requested page index</param>
        /// <param name="categoryId">Selected category Id</param>
        /// <param name="channelTubeId">Current channel tube id</param>
        /// <returns></returns>
        [OperationContract]
        public VideoTubePageModel GetVideoTubeModelForChannelAndCategoryByPage(int pageIndex, int categoryId, int channelTubeId)
        {
            return categoryId > 0
                    ? ScheduleManage.GetVideoTubePoByChannelTubeIdAndCategoryIdAndPageIndex(channelTubeId, categoryId, pageIndex)
                    : ScheduleManage.GetVideoTubePoByChannelTubeIdAndPageIndex(channelTubeId, pageIndex);
        }

        /// <summary>
        /// This method will retrieve all channel tube schedules for a specified channel and a target date
        /// </summary>
        /// <param name="channelTubeId">Channel Tube Id</param>
        /// <param name="scheduleDate">Target Date</param>
        /// <returns></returns>
        [OperationContract]
        public List<ChannelScheduleModel> GetChannelTubeSchedulesByDate(int channelTubeId, string scheduleDate)
        {
            if (channelTubeId <= 0)
            {
                Logger.Warn("Unable to retrieve existing channel's schedules. Invalid channelTubeId specified");
                return null;
            }

            if (String.IsNullOrEmpty(scheduleDate))
            {
                Logger.Warn(String.Format("Unable to create channel schedules. Specified date is invalid '{0}'", scheduleDate));
                return null;
            }

            var date = DateTime.Parse(scheduleDate);
            var schedules = ScheduleManage.GetChannelTubeSchedulesByDate(channelTubeId, date);
            var today = DateTime.Now;

            if (schedules.Count == 0)
            {
                schedules.Add(new ChannelScheduleModel()
                {
                    ChannelScheduleId = 1,
                    ChannelTubeId = 1,
                    Message = "First Schedule",
                    AllowDelete = false,
                    AllowEdit = true,
                    AllowRepeat = true,
                    VideoSchedules = new List<VideoScheduleModel>() {
                        new VideoScheduleModel() {
                            VideoTubeTitle = "Madonna 4 minutes Official Video",
                            Thumbnail = "http://i.ytimg.com/vi/Gi5OP7fecrc/3.jpg",
                            PlaybackOrderNumber = 0,
                            PlaybackStartTime = DateTime.Parse("2014-10-24 2:54:00 PM"),
                            PlaybackEndTime = DateTime.Parse("2014-10-24 2:58:00 PM"),
                            CategoryName = "Sports",
                            Duration = 4,
                            IsInPublicLibrary = true,
                            IsPrivate = false,
                            IsRemovedByProvider = false,
                            IsRRated = false,
                            AllowDeleted = true,
                            PlaytimeMessage = DateTimeUtils.PrintPlaytimeDuration(DateTime.Parse("2014-10-24 2:54:00 PM"), DateTime.Parse("2014-10-24 2:58:00 PM"))
                        },
                        new VideoScheduleModel() {
                            VideoTubeTitle = "Muse Resistance",
                            Thumbnail = "http://i.ytimg.com/vi/TPE9uSFFxrI/3.jpg",
                            PlaybackOrderNumber = 1,
                            PlaybackStartTime = DateTime.Parse("2014-10-24 2:58:00 PM"),
                            PlaybackEndTime = DateTime.Parse("2014-10-24 3:03:00 PM"),
                            CategoryName = "Sports",
                            Duration = 4,
                            IsInPublicLibrary = true,
                            IsPrivate = false,
                            IsRemovedByProvider = false,
                            IsRRated = false,
                            AllowDeleted = true,
                            PlaytimeMessage = DateTimeUtils.PrintPlaytimeDuration(DateTime.Parse("2014-10-24 2:58:00 PM"), DateTime.Parse("2014-10-24 3:03:00 PM"))
                        },
                        new VideoScheduleModel() {
                            VideoTubeTitle = "How It s Made Hot Dogs",
                            Thumbnail = "http://i.ytimg.com/vi/2NzUm7UEEIY/3.jpg",
                            PlaybackOrderNumber = 2,
                            PlaybackStartTime = DateTime.Parse("2014-10-24 3:03:00 PM"),
                            PlaybackEndTime = DateTime.Parse("2014-10-24 3:08:00 PM"),
                            CategoryName = "Sports",
                            Duration = 4,
                            IsInPublicLibrary = true,
                            IsPrivate = false,
                            IsRemovedByProvider = false,
                            IsRRated = false,
                            AllowDeleted = true,
                            PlaytimeMessage = DateTimeUtils.PrintPlaytimeDuration(DateTime.Parse("2014-10-24 3:03:00 PM"), DateTime.Parse("2014-10-24 3:08:00 PM"))
                        }
                    }
                });
                schedules.Add(new ChannelScheduleModel()
                {
                    ChannelScheduleId = 1,
                    ChannelTubeId = 1,
                    Message = "Second Schedule",
                    AllowDelete = true,
                    AllowEdit = true,
                    AllowRepeat = true,
                    VideoSchedules = new List<VideoScheduleModel>() {
                        new VideoScheduleModel() {
                            VideoTubeTitle = "Madonna 4 minutes Official Video",
                            Thumbnail = "http://i.ytimg.com/vi/Gi5OP7fecrc/3.jpg",
                            PlaybackOrderNumber = 0,
                            PlaybackStartTime = DateTime.Parse("2014-10-24 3:03:00 PM"),
                            PlaybackEndTime = DateTime.Parse("2014-10-24 3:08:00 PM"),
                            CategoryName = "Sports",
                            Duration = 8,
                            IsInPublicLibrary = true,
                            IsPrivate = false,
                            IsRemovedByProvider = false,
                            IsRRated = false,
                            AllowDeleted = true,
                            PlaytimeMessage = DateTimeUtils.PrintPlaytimeDuration(DateTime.Parse("2014-10-24 3:31:00 PM"), DateTime.Parse("2014-10-24 3:35:00 PM"))
                        },
                        new VideoScheduleModel() {
                            VideoTubeTitle = "Muse Resistance",
                            Thumbnail = "http://i.ytimg.com/vi/TPE9uSFFxrI/3.jpg",
                            PlaybackOrderNumber = 1,
                            PlaybackStartTime = DateTime.Parse("2014-10-24 3:35:00 PM"),
                            PlaybackEndTime = DateTime.Parse("2014-10-24 3:40:00 PM"),
                            CategoryName = "Sports",
                            Duration = 5,
                            IsInPublicLibrary = true,
                            IsPrivate = false,
                            IsRemovedByProvider = false,
                            IsRRated = false,
                            AllowDeleted = true,
                            PlaytimeMessage = DateTimeUtils.PrintPlaytimeDuration(DateTime.Parse("2014-10-24 3:35:00 PM"), DateTime.Parse("2014-10-24 3:40:00 PM"))
                        },
                        new VideoScheduleModel() {
                            VideoTubeTitle = "How It s Made Hot Dogs",
                            Thumbnail = "http://i.ytimg.com/vi/2NzUm7UEEIY/3.jpg",
                            PlaybackOrderNumber = 2,
                            PlaybackStartTime = DateTime.Parse("2014-10-24 3:40:00 PM"),
                            PlaybackEndTime = DateTime.Parse("2014-10-24 3:45:00 PM"),
                            CategoryName = "Sports",
                            Duration = 5,
                            IsInPublicLibrary = true,
                            IsPrivate = false,
                            IsRemovedByProvider = false,
                            IsRRated = false,
                            AllowDeleted = true,
                            PlaytimeMessage = DateTimeUtils.PrintPlaytimeDuration(DateTime.Parse("2014-10-24 3:40:00 PM"), DateTime.Parse("2014-10-24 3:45:00 PM"))
                        }
                    }
                });

            }

            return schedules;
        }

        /// <summary>
        /// This method will create a new instant schedule for today for a specific Channel Tube
        /// </summary>
        /// <param name="channelTubeId">Channel Tube Id</param>
        /// <returns></returns>
        [OperationContract]
        public ChannelScheduleModel CreateInstantSchedule(int channelTubeId)
        {
            if (channelTubeId <= 0)
            {
                Logger.Warn("Unable to create a new instant channel schedule. Invalid channelTubeId specified");
                return new ChannelScheduleModel() { Message = "Unable to create a new instant schedule" };
            }

            return ScheduleManage.CreateInstantSchedule(channelTubeId);
        }

        /// <summary>
        /// This method will create a new channel schedule for a selected channel and a target, user picked,
        /// start date and time
        /// </summary>
        /// <param name="channelTubeId">Channel Tube Id</param>
        /// <param name="startDateAndTime">Start Data and Time</param>
        /// <returns></returns>
        [OperationContract]
        public ChannelScheduleModel CreateChannelSchedule(int channelTubeId, string scheduleDate)
        {
            if (channelTubeId <= 0)
            {
                Logger.Warn("Unable to create a new instant channel schedule. Invalid channelTubeId specified");
                return new ChannelScheduleModel() { Message = "Unable to create a new channel schedule" };
            }

            if (String.IsNullOrEmpty(scheduleDate))
            {
                Logger.Warn(String.Format("Unable to create channel schedule in the past. Specified Start Date and Time is '{0}'", scheduleDate));
                return new ChannelScheduleModel() { Message = "Unable to craete a new channel schedule in the past" };
            }

            DateTime formatedScheduleDate = DateTime.ParseExact(scheduleDate, stringClientDateTimeFormat, CultureInfo.InvariantCulture);


            return ScheduleManage.CreateChannelSchedule(channelTubeId, formatedScheduleDate);
        }

        /// <summary>
        /// This method should be used to add new video to a channel schedule being edited.
        /// </summary>
        /// <param name="channelScheduleId">Channel Schedule Id</param>
        /// <param name="videoTubeId">Video Tube Id</param>
        /// <returns></returns>
        [OperationContract]
        public ChannelScheduleModel AddVideoToChannelSchedule(int channelScheduleId, int videoTubeId)
        {
            if (channelScheduleId <= 0)
            {
                Logger.Warn("Unable to add video to channel's schedule. Invalid channel schedule id specified");
                return new ChannelScheduleModel() { Message = "Unable to add video to channel's schedule" };
            }

            if (videoTubeId <= 0)
            {
                Logger.Warn("Unable to add video to channel's schedule. Invalid video Tube Id specified");
                return new ChannelScheduleModel() { Message = "Unable to add video to channel's schedule" };
            }

            return ScheduleManage.AddVideoToChannelSchedule(channelScheduleId, videoTubeId);
        }

        /// <summary>
        /// This method will remove a video that is part of the channel schedule from it
        /// </summary>
        /// <param name="channelScheduleId">Channel Schedule Id</param>
        /// <param name="videoTubeId">Video Tube Id</param>
        /// <returns></returns>
        [OperationContract]
        public ChannelScheduleModel RemoveVideoFromChannelSchedule(int channelScheduleId, int videoTubeId)
        {
            if (channelScheduleId <= 0)
            {
                Logger.Warn("Unable to remove video to channel's schedule. Invalid channel schedule id specified");
                return new ChannelScheduleModel() { Message = "Unable to remove video to channel's schedule" };
            }

            if (videoTubeId <= 0)
            {
                Logger.Warn("Unable to remove video to channel's schedule. Invalid video Tube Id specified");
                return new ChannelScheduleModel() { Message = "Unable to remove video to channel's schedule" };
            }

            return ScheduleManage.RemoveVideoFromChannelSchedule(channelScheduleId, videoTubeId);
        }

        /// <summary>
        /// This method will clear channel schedule by removing all videos from it
        /// </summary>
        /// <param name="channelScheduleId">Channel Schedule Id</param>
        /// <returns></returns>
        [OperationContract]
        public ChannelScheduleModel ClearChannelSchedule(int channelScheduleId)
        {
            if (channelScheduleId <= 0)
            {
                Logger.Warn("Unable to clear channel's schedule. Invalid channel schedule id specified");
                return new ChannelScheduleModel() { Message = "Unable to clear channel's schedule" };
            }

            return ScheduleManage.ClearChannelScheduleById(channelScheduleId);
        }

        /// <summary>
        /// This method should be used to retrieve video schedules for a specific channel schedule
        /// </summary>
        /// <param name="channelScheduleId">Channel Schedule Id</param>
        /// <returns></returns>
        [OperationContract]
        public List<VideoScheduleModel> GetVideoSchedulesForChannelSchedule(int channelScheduleId)
        {
            if (channelScheduleId <= 0)
            {
                Logger.Warn("Unable to retrieve video schedules for channel schedule. Invalid channel schedule id specified");
                return null;
            }

            return ScheduleManage.GetVideoSchedulesForChannelScheduleById(channelScheduleId);
        }

        /// <summary>
        /// This method will update Published flag on ChannelSchedule
        /// </summary>
        /// <param name="channelScheduleId">ChannelScheduleId</param>
        /// <param name="shouldPublish">Published flag</param>
        /// <returns></returns>
        [OperationContract]
        public bool UpdatePublishFlagForChannelSchedule(int channelScheduleId, bool shouldPublish)
        {
            if (channelScheduleId <= 0)
            {
                Logger.Warn("Unable to update publish flag for channel schedule. Invalid channel schedule id specified");
                return false;
            }

            return ScheduleManage.PublishOrUnpublishChannelSchedule(channelScheduleId, shouldPublish);
        }

        /// <summary>
        /// This method will update start date and time of an existing channel schedule
        /// </summary>
        /// <param name="channelScheduleId">Channel Schedule Id</param>
        /// <param name="startDateAndTime">New start date and time</param>
        /// <returns></returns>
        [OperationContract]
        public ChannelScheduleModel UpdateChannelScheduelStartDateAndTime(int channelScheduleId, string startDateAndTime)
        {
            if (channelScheduleId <= 0)
            {
                Logger.Warn("Unable to update channel schedule. Invalid channelScheduleId specified");
                return new ChannelScheduleModel() { Message = "Unable to update channel schedule" };
            }

            if (String.IsNullOrEmpty(startDateAndTime))
            {
                Logger.Warn(String.Format("Unable to update channel schedule. Specified Start Date and Time is '{0}'", startDateAndTime));
                return new ChannelScheduleModel() { Message = "Unable to create update channel schedule" };
            }

            var date = DateTime.Parse(startDateAndTime);

            return ScheduleManage.UpdateChannelScheduleStartDateAndTime(channelScheduleId, date);
        }

        /// <summary>
        /// This method will remove existing channel schedule using its Id
        /// </summary>
        /// <param name="channelScheduleId">Channel Schedule Id</param>
        /// <returns></returns>
        [OperationContract]
        public bool RemoveChannelSchedule(int channelScheduleId)
        {
            Logger.Info(String.Format("Deleting channel schedule with Id={0}", channelScheduleId));

            return ScheduleManage.RemoveChannelSchedule(channelScheduleId);
        }
    }
}
