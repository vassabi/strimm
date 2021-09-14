using Strimm.Shared;
using StrimmBL;
using Strimm.Model;
using StrimmTube.UC;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Strimm.Shared.Extensions;
using Strimm.Model.Projections;
using Strimm.Model.WebModel;
using log4net;
using StrimmTube.Utils;
using Strimm.Model.Criteria;

namespace StrimmTube.WebServices
{
    /// <summary>
    /// Summary description for ScheduleWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class ScheduleWebService : System.Web.Services.WebService
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ScheduleWebService));
        private static readonly string stringClientDateTimeFormat = "MM/dd/yyyy HH:mm";


        [WebMethod(EnableSession = true)]
        public void ClearSession()
        {
            Session["videoScheduleList"] = null;
            Session["channelSchedule"] = null;
        }

        /// <summary>
        /// This method will retrieve calendar schedule events for a specified channel for a specified
        /// month, year
        /// </summary>
        /// <param name="month">Target month</param>
        /// <param name="year">Target year</param>
        /// <param name="channelTubeId">Channel Tube Id</param>
        /// <returns></returns>
        [WebMethod]
        public List<ChannelTubeScheduleCalendarEvent> GetChannelTubeScheduleCalendarEvents(int month, int year, int channelTubeId)
        {
            var events = ScheduleManage.GetChannelTubeScheduleCalendarEvents(month, year, channelTubeId);
            return events;
        }

        /// <summary>
        /// This method will retrieve videos for a channel that can be used in creating a schedule
        /// </summary>
        /// <param name="pageIndex">Requested page index</param>
        /// <param name="categoryId">Selected category Id</param>
        /// <param name="channelTubeId">Current channel tube id</param>
        /// <returns></returns>
        [WebMethod]
        public VideoTubePageModel GetVideoTubeModelForChannelAndCategoryByPage(ChannelVideoSearchCriteria searchCriteria)
        {
            return searchCriteria != null && searchCriteria.CategoryId > 0
                    ? ScheduleManage.GetVideoTubePoByChannelTubeIdAndCategoryIdAndPageIndex(searchCriteria.ChannelTubeId, searchCriteria.CategoryId, searchCriteria.PageIndex, searchCriteria.Keywords)
                    : ScheduleManage.GetVideoTubePoByChannelTubeIdAndPageIndex(searchCriteria.ChannelTubeId, searchCriteria.PageIndex, searchCriteria.Keywords);
        }


        /// <summary>
        /// This method will retrieve all channel tube schedules for a specified channel and a target date
        /// </summary>
        /// <param name="channelTubeId">Channel Tube Id</param>
        /// <param name="scheduleDate">Target Date</param>
        /// <returns></returns>
        [WebMethod]
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

            // MAX: This should be moved some place else, but for now, do empty channel cleanup here
            ScheduleManage.DeleteEmptySchedulesForChannelOnOrBeforeDate(channelTubeId, date.Date);

            var schedules = ScheduleManage.GetChannelTubeSchedulesByDate(channelTubeId, date.Date);

            return schedules;
        }

        /// <summary>
        /// This method will create a new instant schedule for today for a specific Channel Tube
        /// </summary>
        /// <param name="channelTubeId">Channel Tube Id</param>
        /// <param name="clientDateAndTime">Start Data and Time</param>
        /// <returns></returns>
        [WebMethod]
        public List<ChannelScheduleModel> CreateInstantSchedule(int channelTubeId, string clientDateAndTime)
        {
            if (channelTubeId <= 0)
            {
                Logger.Warn("Unable to create a new instant channel schedule. Invalid channelTubeId specified");
                return new List<ChannelScheduleModel>() { new ChannelScheduleModel() { Message = "Unable to create a new instant schedule" } };
            }

            if (String.IsNullOrEmpty(clientDateAndTime))
            {
                Logger.Warn(String.Format("Unable to create channel instant schedule. Specified Start Date and Time is '{0}'", clientDateAndTime));
                return new List<ChannelScheduleModel>() { new ChannelScheduleModel() { Message = "Unable to craete a new channel schedule. Invalid date and time specified" } };
            }

            DateTime clientTime = DateTime.ParseExact(clientDateAndTime, stringClientDateTimeFormat, CultureInfo.InvariantCulture);

            // MAX: This should be moved some place else, but for now, do empty channel cleanup here
            ScheduleManage.DeleteEmptySchedulesForChannelOnOrBeforeDate(channelTubeId, clientTime);

            var newInstantSchedule = ScheduleManage.CreateInstantSchedule(channelTubeId, clientTime);
            var existingSchedules = ScheduleManage.GetChannelTubeSchedulesByDate(channelTubeId, clientTime);

            var newFromExistingList = existingSchedules.SingleOrDefault(x => x.ChannelScheduleId == newInstantSchedule.ChannelScheduleId);

            if (newFromExistingList != null)
            {
                newFromExistingList.ExpandVideoSchedulesList = true;
            }

            return existingSchedules;
        }

        /// <summary>
        /// This method will create a new channel schedule for a selected channel and a target, user picked,
        /// start date and time
        /// </summary>
        /// <param name="channelTubeId">Channel Tube Id</param>
        /// <param name="startDateAndTime">Start Data and Time</param>
        /// <returns></returns>
        [WebMethod]
        public List<ChannelScheduleModel> CreateChannelSchedule(int channelTubeId, string scheduleDate)
        {
            if (channelTubeId <= 0)
            {
                Logger.Warn("Unable to create a new channel schedule. Invalid channelTubeId specified");
                return new List<ChannelScheduleModel>() { new ChannelScheduleModel() { Message = "Unable to create a new channel schedule" } };
            }

            if (String.IsNullOrEmpty(scheduleDate))
            {
                Logger.Warn(String.Format("Unable to create channel schedule. Specified Start Date and Time is '{0}'", scheduleDate));
                return new List<ChannelScheduleModel>() { new ChannelScheduleModel() { Message = "Unable to craete a new channel schedule. Invalid start time specified" } };
            }

            DateTime formatedScheduleDate = DateTime.ParseExact(scheduleDate, stringClientDateTimeFormat, CultureInfo.InvariantCulture);

            // MAX: REVIEW
            //if (formatedScheduleDate < DateTime.Now.AddMinutes(-2))
            //{
            //    Logger.Warn(String.Format("Unable to create channel instant schedule in the past. Specified Start Date and Time is '{0}'", formatedScheduleDate));
            //    return new List<ChannelScheduleModel>() { new ChannelScheduleModel() { Message = "Unable to create a new channel schedule. Specified date and time is in the past" }};
            //}

            // MAX: This should be moved some place else, but for now, do empty channel cleanup here
            ScheduleManage.DeleteEmptySchedulesForChannelOnOrBeforeDate(channelTubeId, formatedScheduleDate);

            var newCustomSchedule = ScheduleManage.CreateChannelSchedule(channelTubeId, formatedScheduleDate);

            var existingSchedules = ScheduleManage.GetChannelTubeSchedulesByDate(channelTubeId, formatedScheduleDate);
            var newFromExistingList = existingSchedules.SingleOrDefault(x => x.ChannelScheduleId == newCustomSchedule.ChannelScheduleId);

            if (newFromExistingList != null)
            {
                newFromExistingList.ExpandVideoSchedulesList = true;
            }

            return existingSchedules;
        }

        /// <summary>
        /// This method should be used to add new video to a channel schedule being edited.
        /// </summary>
        /// <param name="channelScheduleId">Channel Schedule Id</param>
        /// <param name="videoTubeId">Video Tube Id</param>
        /// <returns></returns>
        [WebMethod]
        public List<ChannelScheduleModel> AddVideoToChannelSchedule(int channelScheduleId, int videoTubeId)
        {
            if (channelScheduleId <= 0)
            {
                Logger.Warn("Unable to add video to channel's schedule. Invalid channel schedule id specified");
                return new List<ChannelScheduleModel>() { new ChannelScheduleModel() { Message = "Unable to add video to channel's schedule" } };
            }

            if (videoTubeId <= 0)
            {
                Logger.Warn("Unable to add video to channel's schedule. Invalid video Tube Id specified");
                return new List<ChannelScheduleModel>() { new ChannelScheduleModel() { Message = "Unable to add video to channel's schedule" } };
            }

            var modifiedSchedule = ScheduleManage.AddVideoToChannelSchedule(channelScheduleId, videoTubeId);
            var existingSchedules = modifiedSchedule.ChannelScheduleId > 0
                    ? ScheduleManage.GetChannelTubeSchedulesByDate(modifiedSchedule.ChannelTubeId, modifiedSchedule.StartDateAndTime)
                    : new List<ChannelScheduleModel>() { modifiedSchedule };

            var modifiedFromExistingList = existingSchedules.SingleOrDefault(x => x.ChannelScheduleId == modifiedSchedule.ChannelScheduleId);

            if (modifiedFromExistingList != null)
            {
                modifiedFromExistingList.ExpandVideoSchedulesList = true;
            }

            return existingSchedules;
        }

        [WebMethod]
        public List<ChannelScheduleModel> AddScheduleOnDrop(int channelScheduleId, int videoTubeId, int orderNumber)
        {
            if (channelScheduleId <= 0)
            {
                Logger.Warn("Unable to add video to channel's schedule. Invalid channel schedule id specified");
                return new List<ChannelScheduleModel>() { new ChannelScheduleModel() { Message = "Unable to add video to channel's schedule" } };
            }

            if (videoTubeId <= 0)
            {
                Logger.Warn("Unable to add video to channel's schedule. Invalid video Tube Id specified");
                return new List<ChannelScheduleModel>() { new ChannelScheduleModel() { Message = "Unable to add video to channel's schedule" } };
            }
            var videotubeschedule = ScheduleManage.GetVideoSchedulesForChannelScheduleById(channelScheduleId);


            ChannelScheduleModel modifiedSchedule = new ChannelScheduleModel();

            modifiedSchedule = ScheduleManage.AddVideoToChannelScheduleOnDrop(channelScheduleId, videoTubeId, orderNumber);


            var existingSchedules = modifiedSchedule.ChannelScheduleId > 0
                    ? ScheduleManage.GetChannelTubeSchedulesByDate(modifiedSchedule.ChannelTubeId, modifiedSchedule.StartDateAndTime)
                    : new List<ChannelScheduleModel>() { modifiedSchedule };

            var modifiedFromExistingList = existingSchedules.SingleOrDefault(x => x.ChannelScheduleId == modifiedSchedule.ChannelScheduleId);

            if (modifiedFromExistingList != null)
            {
                modifiedFromExistingList.ExpandVideoSchedulesList = true;
            }




            return existingSchedules;
        }

        [WebMethod]
        public List<ChannelScheduleModel> UpdateScheduleOnDrop(int channelScheduleId, int videoTubeId, int orderNumber)
        {
            if (channelScheduleId <= 0)
            {
                Logger.Warn("Unable to add video to channel's schedule. Invalid channel schedule id specified");
                return new List<ChannelScheduleModel>() { new ChannelScheduleModel() { Message = "Unable to add video to channel's schedule" } };
            }

            if (videoTubeId <= 0)
            {
                Logger.Warn("Unable to add video to channel's schedule. Invalid video Tube Id specified");
                return new List<ChannelScheduleModel>() { new ChannelScheduleModel() { Message = "Unable to add video to channel's schedule" } };
            }
            var videotubeschedule = ScheduleManage.GetVideoSchedulesForChannelScheduleById(channelScheduleId);


            ChannelScheduleModel modifiedSchedule = new ChannelScheduleModel();

            modifiedSchedule = ScheduleManage.AddVideoToChannelScheduleOnDrop(channelScheduleId, videoTubeId, orderNumber);


            var existingSchedules = modifiedSchedule.ChannelScheduleId > 0
                    ? ScheduleManage.GetChannelTubeSchedulesByDate(modifiedSchedule.ChannelTubeId, modifiedSchedule.StartDateAndTime)
                    : new List<ChannelScheduleModel>() { modifiedSchedule };

            var modifiedFromExistingList = existingSchedules.SingleOrDefault(x => x.ChannelScheduleId == modifiedSchedule.ChannelScheduleId);

            if (modifiedFromExistingList != null)
            {
                modifiedFromExistingList.ExpandVideoSchedulesList = true;
            }




            return existingSchedules;
        }
        [WebMethod]
        public List<ChannelScheduleModel> DeleteScheduleOnDrop(int channelScheduleId, int videoTubeId, int playbackOrderNumber)
        {
            if (channelScheduleId <= 0)
            {
                Logger.Warn("Unable to add video to channel's schedule. Invalid channel schedule id specified");
                return new List<ChannelScheduleModel>() { new ChannelScheduleModel() { Message = "Unable to add video to channel's schedule" } };
            }





            ChannelScheduleModel modifiedSchedule = new ChannelScheduleModel();
            ScheduleManage.RemoveVideoFromChannelScheduleOnDrop(channelScheduleId, videoTubeId, playbackOrderNumber);




            var existingSchedules = modifiedSchedule.ChannelScheduleId > 0
                     ? ScheduleManage.GetChannelTubeSchedulesByDate(modifiedSchedule.ChannelTubeId, modifiedSchedule.StartDateAndTime)
                     : new List<ChannelScheduleModel>() { modifiedSchedule };

            var modifiedFromExistingList = existingSchedules.SingleOrDefault(x => x.ChannelScheduleId == modifiedSchedule.ChannelScheduleId);

            if (modifiedFromExistingList != null)
            {
                modifiedFromExistingList.ExpandVideoSchedulesList = true;
            }




            return existingSchedules;
        }
        [WebMethod]
        public List<ChannelScheduleModel> ReoderSchedule(int channelScheduleId, int targetPlaybackOrderNumber, int playbackOrderNumber, int videoTubeId)
        {
            if (channelScheduleId <= 0)
            {
                Logger.Warn("Unable to add video to channel's schedule. Invalid channel schedule id specified");
                return new List<ChannelScheduleModel>() { new ChannelScheduleModel() { Message = "Unable to add video to channel's schedule" } };
            }

       
           


            ChannelSchedule modifiedSchedule = new ChannelSchedule();
            modifiedSchedule = ScheduleManage.GetChannelScheduleByChannelScheduleById(channelScheduleId);
            ScheduleManage.ReorderSchedule(channelScheduleId, targetPlaybackOrderNumber, playbackOrderNumber, videoTubeId);




            var existingSchedules = ScheduleManage.GetChannelTubeSchedulesByDate(modifiedSchedule.ChannelTubeId, modifiedSchedule.StartTime);
                   

            var modifiedFromExistingList = existingSchedules.SingleOrDefault(x => x.ChannelScheduleId == modifiedSchedule.ChannelScheduleId);

            if (modifiedFromExistingList != null)
            {
                modifiedFromExistingList.ExpandVideoSchedulesList = true;
            }




            return existingSchedules;
        }

        [WebMethod]
        public List<ChannelScheduleModel> ShuffleSchedule(int channelScheduleId, int videoTubeId, int orderNumber, int videoOrderNumber)
        {
            if (channelScheduleId <= 0)
            {
                Logger.Warn("Unable to add video to channel's schedule. Invalid channel schedule id specified");
                return new List<ChannelScheduleModel>() { new ChannelScheduleModel() { Message = "Unable to add video to channel's schedule" } };
            }

            if (videoTubeId <= 0)
            {
                Logger.Warn("Unable to add video to channel's schedule. Invalid video Tube Id specified");
                return new List<ChannelScheduleModel>() { new ChannelScheduleModel() { Message = "Unable to add video to channel's schedule" } };
            }
            ScheduleManage.RemoveVideoFromChannelSchedule(channelScheduleId, videoTubeId, videoOrderNumber);
            var videotubeschedule = ScheduleManage.GetVideoSchedulesForChannelScheduleById(channelScheduleId);


            ChannelScheduleModel modifiedSchedule = new ChannelScheduleModel();
            var orderVideoschedule = videotubeschedule.OrderBy(x => x.PlaybackOrderNumber);
            var partOfSchedule = orderVideoschedule.Where(x => x.PlaybackOrderNumber > orderNumber).ToList();
            if (partOfSchedule.Count != 0)
            {
                ScheduleManage.RemoveVideosFromChannelScheduleFromOrderNumber(channelScheduleId, orderNumber);
                modifiedSchedule = ScheduleManage.AddVideoToChannelSchedule(channelScheduleId, videoTubeId);
                foreach (var part in partOfSchedule)
                {

                    modifiedSchedule = ScheduleManage.AddVideoToChannelSchedule(channelScheduleId, part.VideoTubeId);
                }
            }
            else
            {
                return AddVideoToChannelSchedule(channelScheduleId, videoTubeId);
            }


            var existingSchedules = modifiedSchedule.ChannelScheduleId > 0
                    ? ScheduleManage.GetChannelTubeSchedulesByDate(modifiedSchedule.ChannelTubeId, modifiedSchedule.StartDateAndTime)
                    : new List<ChannelScheduleModel>() { modifiedSchedule };

            var modifiedFromExistingList = existingSchedules.SingleOrDefault(x => x.ChannelScheduleId == modifiedSchedule.ChannelScheduleId);

            if (modifiedFromExistingList != null)
            {
                modifiedFromExistingList.ExpandVideoSchedulesList = true;
            }




            return existingSchedules;
        }



        /// <summary>
        /// This method will remove a video that is part of the channel schedule from it
        /// </summary>
        /// <param name="channelScheduleId">Channel Schedule Id</param>
        /// <param name="videoTubeId">Video Tube Id</param>
        /// <returns></returns>
        [WebMethod]
        public List<ChannelScheduleModel> RemoveVideoFromChannelSchedule(int channelScheduleId, int videoTubeId, int playbackOrderNumber)
        {
            if (channelScheduleId <= 0)
            {
                Logger.Warn("Unable to remove video from channel's schedule. Invalid channel schedule id specified");
                return new List<ChannelScheduleModel>() { new ChannelScheduleModel() { Message = "Unable to remove video to channel's schedule" }};
            }

            if (videoTubeId <= 0)
            {
                Logger.Warn("Unable to remove video from channel's schedule. Invalid video Tube Id specified");
                return new List<ChannelScheduleModel>() { new ChannelScheduleModel() { Message = "Unable to remove video to channel's schedule" }};
            }

            if (playbackOrderNumber < 0)
            {
                Logger.Warn("Unable to remove video from channel's schedule. Invalid playback order number specified");
                return new List<ChannelScheduleModel>() { new ChannelScheduleModel() { Message = "Unable to remove video to channel's schedule" }};
            }

            var modifiedSchedule = ScheduleManage.RemoveVideoFromChannelSchedule(channelScheduleId, videoTubeId, playbackOrderNumber);
            var existingSchedules = ScheduleManage.GetChannelTubeSchedulesByDate(modifiedSchedule.ChannelTubeId, modifiedSchedule.StartDateAndTime);

            var modifiedFromExistingList = existingSchedules.SingleOrDefault(x => x.ChannelScheduleId == modifiedSchedule.ChannelScheduleId);

            if (modifiedFromExistingList != null)
            {
                modifiedFromExistingList.ExpandVideoSchedulesList = true;
            }

            return existingSchedules;
        }

        /// <summary>
        /// This method will clear channel schedule by removing all videos from it
        /// </summary>
        /// <param name="channelScheduleId">Channel Schedule Id</param>
        /// <returns></returns>
        [WebMethod]
        public ChannelScheduleModel ClearChannelSchedule(int channelScheduleId)
        {
            if (channelScheduleId <= 0)
            {
                Logger.Warn("Unable to clear channel's schedule! Invalid channel schedule id specified.");
                return new ChannelScheduleModel() { Message = "Unable to clear channel's schedule." };
            }

            return ScheduleManage.ClearChannelScheduleById(channelScheduleId);
        }

        /// <summary>
        /// This method should be used to retrieve video schedules for a specific channel schedule
        /// </summary>
        /// <param name="channelScheduleId">Channel Schedule Id</param>
        /// <returns></returns>
        [WebMethod]
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
        [WebMethod]
        public ResponseMessageModel UpdatePublishFlagForChannelSchedule(int channelScheduleId, bool shouldPublish)
        {
            if (channelScheduleId <= 0)
            {
                Logger.Warn("Unable to update publish flag for channel schedule. Invalid channel schedule id specified");
                return new ResponseMessageModel() { Message = "Error occured while publishing your schedule, invalid schedule specified.", IsSuccess = false };
            }

            var response = new ResponseMessageModel();
         
            var videoSchedules = ScheduleManage.GetVideoSchedulesForChannelScheduleById(channelScheduleId);

            if (videoSchedules != null && videoSchedules.Count() > 0)
            {
                if (!ScheduleManage.PublishOrUnpublishChannelSchedule(channelScheduleId, shouldPublish))
                {
                    response.Message = String.Format("Error occured while {0} a schedule. Try again later", shouldPublish ? "Publishing" : "Unpublishing");
                }
                else
                {
                    response.IsSuccess = true;
                    response.Message = String.Format("Schedule was successfully {0}", shouldPublish ? "published" : "taken off the air");
                }
            }
            else
            {
                response.Message = "Unable to update schedule state. Specified schedule is empty";
            }

            return response;
        }

        /// <summary>
        /// This method will update start date and time of an existing channel schedule
        /// </summary>
        /// <param name="channelScheduleId">Channel Schedule Id</param>
        /// <param name="startDateAndTime">New start date and time</param>
        /// <returns></returns>
        public ChannelScheduleModel UpdateChannelScheduelStartDateAndTime(int channelScheduleId, string startDateAndTime)
        {
            if (channelScheduleId <= 0)
            {
                Logger.Warn("Unable to update channel schedule. Invalid channelScheduleId specified");
                return new ChannelScheduleModel() { Message = "Unable to update channel's schedule." };
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
        [WebMethod]
        public List<ChannelScheduleModel> RemoveChannelSchedule(int channelScheduleId, int channelTubeId, string targetDate)
        {
            Logger.Info(String.Format("Deleting channel schedule with Id={0}", channelScheduleId));
            ScheduleManage.RemoveChannelSchedule(channelScheduleId);

            DateTime formatedScheduleDate = DateTime.ParseExact(targetDate, stringClientDateTimeFormat, CultureInfo.InvariantCulture);

           

            return ScheduleManage.GetChannelTubeSchedulesByDate(channelTubeId, formatedScheduleDate);
        }

        /// <summary>
        /// This method will repeat existing schedule on the target date.
        /// </summary>
        /// <param name="channelScheduleId">Channel Schedule Id</param>
        /// <param name="channelTubeId">Channel Tube Id</param>
        /// <param name="targetDate">New schedule date</param>
        /// <returns></returns>
        [WebMethod]
        public ChannelScheduleModel RepeatSchedule(int channelScheduleId, int channelTubeId, string targetDate)
        {
            Logger.Info(String.Format("Repeating existing schedule with Id={0} for channel Tube with Id={1} on '{2}", channelScheduleId, channelTubeId, targetDate));

            DateTime formatedScheduleDate = DateTime.ParseExact(targetDate, stringClientDateTimeFormat, CultureInfo.InvariantCulture);

            return ScheduleManage.RepeatChannelSchedule(channelScheduleId, channelTubeId, formatedScheduleDate);
        }

        /// <summary>
        /// This method will turn the auto-pilot to ON or OFF for a target channel tube
        /// </summary>
        /// <param name="channelTubeId">Channel Tube Id</param>
        /// <param name="isAutoPilot">IsAutoPilot flag</param>
        /// <returns></returns>
        [WebMethod]
        public bool SetAutoPilotForChannel(int channelTubeId, bool isAutoPilot)
        {
            Logger.Info(String.Format("Setting Auto-Pilot for channel tube with Id={0} to: {1}", channelTubeId, isAutoPilot));

            return ScheduleManage.SetAutoPilotForChannelTube(channelTubeId, isAutoPilot);
        }

        [WebMethod]
        public ChannelSchedule GetChannelScheduleByChannelScheduleId(int channelScheduleId)
        {
            Logger.Info(String.Format("getting chnannel schedule by cnannelscheduleId={0}", channelScheduleId));
            return ScheduleManage.GetChannelScheduleByChannelScheduleById(channelScheduleId);
        }

      
    }
}
    

