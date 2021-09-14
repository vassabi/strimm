using log4net;
using Strimm.Model;
using Strimm.Model.Projections;
using Strimm.Model.WebModel;
using Strimm.Shared;
using Strimm.WebApi.Models;
using Strimm.WebApi.Models.Requests;
using StrimmBL;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;
using System.Web.Services;

namespace Strimm.WebApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class ChannelScheduleController : BaseApiController
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ChannelScheduleController));

        public ChannelScheduleController()
        {

        }

        /// <summary>
        /// This method will retrieve all channel schedule events for a specific channel that
        /// were created in a specific month/year
        /// </summary>
        /// <param name="channelId">Channel Tube Id</param>
        /// <param name="month">Target month</param>
        /// <param name="year">Target year</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/channel/{channelId:int}/schedule/events/{month:int}/{year:int}", Name = "ScheduleEvents")]
        public IEnumerable<ChannelTubeScheduleEventModel> GetChannelScheduleEvents(int channelId, int month, int year)
        {
            var channelEvents = new List<ChannelTubeScheduleEventModel>();
            var modelFactory = new ModelFactory(this.Request);

            channelEvents.Add(modelFactory.CreateChannelTubeScheduleEventModel(new ChannelSchedule() {
                ChannelTubeId = 1,
                ChannelScheduleId = 1,
                CreatedDate = DateTime.Now.AddDays(-1), 
                StartTime = DateTime.Now.AddHours(-14),
                Published = true
            }));
            channelEvents.Add(modelFactory.CreateChannelTubeScheduleEventModel(new ChannelSchedule()
            {
                ChannelTubeId = 1,
                ChannelScheduleId = 2,
                CreatedDate = DateTime.Now.AddDays(1),
                StartTime = DateTime.Now.AddHours(-14).AddDays(1),
                Published = true
            }));

            return channelEvents;
        }

        /// <summary>
        /// This method will retrieve a single channel schedule event
        /// </summary>
        /// <param name="channelId">Channel Tube Id</param>
        /// <param name="scheduleDate">Schedule date</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/channel/{channelId:int}/schedule/events/{scheduleDate:datetime}", Name = "ScheduleEvent")]
        public ChannelTubeScheduleEventModel GetChannelScheduleEvent(int channelId, DateTime scheduleDate)
        {
            var modelFactory = new ModelFactory(this.Request);
            var scheduleEvents = modelFactory.CreateChannelTubeScheduleEventModel(new ChannelSchedule()
            {
                ChannelTubeId = 1,
                ChannelScheduleId = 1,
                CreatedDate = DateTime.Now.AddDays(-1),
                StartTime = DateTime.Now.AddHours(-14),
                Published = true
            });

            return scheduleEvents;
        }

        /// <summary>
        /// This method will retrieve all channel schedules created for a specific channel on a specified date
        /// </summary>
        /// <param name="channelId">Channel Tube Id</param>
        /// <param name="scheduleDate">Schedule Date</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/channel/{channelId:int}/schedule/{scheduleDate:datetime}", Name = "ChannelSchedulesByDate")]
        public IEnumerable<ChannelScheduleModel> GetChannelSchedulesByDate(int channelId, DateTime scheduleDate)
        {
            var modelFactory = new ModelFactory(this.Request);

            DateTime now = DateTime.Now;
            DateTime firstChannelStartTime = now.AddHours(-now.Hour);
            DateTime secondChannelStartTime = now.AddHours(-now.Hour + 12);

            var videoTubeSchedules = new List<VideoSchedule>();
            videoTubeSchedules.Add(new VideoSchedule()
            {
                CategoryId = 1,
                CategoryName = "Sport TV",
                ChannelScheduleId = 1,
                ChannelTubeId = 1,
                Description = "NHL 2013",
                Duration = (long)TimeSpan.FromHours(2).TotalSeconds,
                IsInPublicLibrary = true,
                IsPrivate = false,
                IsRemovedByProvider = false,
                IsRRated = false,
                PlaybackOrderNumber = 0,
                PlaybackStartTime = firstChannelStartTime,
                PlaybackEndTime = firstChannelStartTime.AddSeconds(TimeSpan.FromHours(2).TotalSeconds),
                ProviderVideoId = "tube",
                VideoProviderName = "YouTube",
                VideoTubeId = 1,
                VideoTubeTitle = "My NHL for 2013"
            });
            videoTubeSchedules.Add(new VideoSchedule()
            {
                CategoryId = 1,
                CategoryName = "Sport TV",
                ChannelScheduleId = 1,
                ChannelTubeId = 1,
                Description = "NHL 2014",
                Duration = (long)TimeSpan.FromHours(1.5).TotalSeconds,
                IsInPublicLibrary = true,
                IsPrivate = false,
                IsRemovedByProvider = false,
                IsRRated = false,
                PlaybackOrderNumber = 1,
                PlaybackStartTime = videoTubeSchedules[0].PlaybackEndTime,
                PlaybackEndTime = videoTubeSchedules[0].PlaybackEndTime.AddSeconds(TimeSpan.FromHours(1.5).TotalSeconds),
                ProviderVideoId = "tube",
                VideoProviderName = "YouTube",
                VideoTubeId = 2,
                VideoTubeTitle = "My NHL for 2014"
            });
            videoTubeSchedules.Add(new VideoSchedule()
            {
                CategoryId = 1,
                CategoryName = "Sport TV",
                ChannelScheduleId = 2,
                ChannelTubeId = 1,
                Description = "Soccer News",
                Duration = (long)TimeSpan.FromHours(1).TotalSeconds,
                IsInPublicLibrary = true,
                IsPrivate = false,
                IsRemovedByProvider = false,
                IsRRated = false,
                PlaybackOrderNumber = 0,
                PlaybackStartTime = secondChannelStartTime,
                PlaybackEndTime = secondChannelStartTime.AddSeconds(TimeSpan.FromHours(1).TotalSeconds),
                ProviderVideoId = "tube",
                VideoProviderName = "YouTube",
                VideoTubeId = 3,
                VideoTubeTitle = "Channel 24 Soccer News"
            });
            videoTubeSchedules.Add(new VideoSchedule()
            {
                CategoryId = 2,
                CategoryName = "News TV",
                ChannelScheduleId = 2,
                ChannelTubeId = 1,
                Description = "2013 Soccer Review",
                Duration = (long)TimeSpan.FromHours(2).TotalSeconds,
                IsInPublicLibrary = true,
                IsPrivate = false,
                IsRemovedByProvider = false,
                IsRRated = false,
                PlaybackOrderNumber = 1,
                PlaybackStartTime = videoTubeSchedules[videoTubeSchedules.Count - 1].PlaybackEndTime,
                PlaybackEndTime = videoTubeSchedules[videoTubeSchedules.Count - 1].PlaybackEndTime.AddSeconds(TimeSpan.FromHours(2).TotalSeconds),
                ProviderVideoId = "tube",
                VideoProviderName = "YouTube",
                VideoTubeId = 4,
                VideoTubeTitle = "2013 Soccer Season Review"
            });
            videoTubeSchedules.Add(new VideoSchedule()
            {
                CategoryId = 2,
                CategoryName = "News TV",
                ChannelScheduleId = 2,
                ChannelTubeId = 1,
                Description = "Soccer Memorable Moments",
                Duration = (long)TimeSpan.FromHours(1.5).TotalSeconds,
                IsInPublicLibrary = true,
                IsPrivate = false,
                IsRemovedByProvider = false,
                IsRRated = false,
                PlaybackOrderNumber = 2,
                PlaybackStartTime = videoTubeSchedules[videoTubeSchedules.Count - 1].PlaybackEndTime,
                PlaybackEndTime = videoTubeSchedules[videoTubeSchedules.Count - 1].PlaybackEndTime.AddSeconds(TimeSpan.FromHours(1.5).TotalSeconds),
                ProviderVideoId = "tube",
                VideoProviderName = "YouTube",
                VideoTubeId = 5,
                VideoTubeTitle = "Soccer Memorable Moments"
            });

            var schedules = modelFactory.CreateChannelScheduleModels(videoTubeSchedules);

            return schedules;
        }

        /// <summary>
        /// This method will retrieve individual channel schedule by Id
        /// </summary>
        /// <param name="scheduleId">Channel Schedule Id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/channel/schedule/{scheduleId:int}", Name = "ChannelSchedule")]
        public ChannelScheduleModel GetChannelSchedule(int scheduleId)
        {
            var modelFactory = new ModelFactory(this.Request);

            DateTime now = DateTime.Now;
            DateTime firstChannelStartTime = now.AddHours(-now.Hour);

            var videoTubeSchedules = new List<VideoSchedule>();
            videoTubeSchedules.Add(new VideoSchedule()
            {
                CategoryId = 1,
                CategoryName = "Sport TV",
                ChannelScheduleId = 1,
                ChannelTubeId = 1,
                Description = "NHL 2013",
                Duration = (long)TimeSpan.FromHours(2).TotalSeconds,
                IsInPublicLibrary = true,
                IsPrivate = false,
                IsRemovedByProvider = false,
                IsRRated = false,
                PlaybackOrderNumber = 0,
                PlaybackStartTime = firstChannelStartTime,
                PlaybackEndTime = firstChannelStartTime.AddSeconds(TimeSpan.FromHours(2).TotalSeconds),
                ProviderVideoId = "tube",
                VideoProviderName = "YouTube",
                VideoTubeId = 1,
                VideoTubeTitle = "My NHL for 2013"
            });
            videoTubeSchedules.Add(new VideoSchedule()
            {
                CategoryId = 1,
                CategoryName = "Sport TV",
                ChannelScheduleId = 1,
                ChannelTubeId = 1,
                Description = "NHL 2014",
                Duration = (long)TimeSpan.FromHours(1.5).TotalSeconds,
                IsInPublicLibrary = true,
                IsPrivate = false,
                IsRemovedByProvider = false,
                IsRRated = false,
                PlaybackOrderNumber = 1,
                PlaybackStartTime = videoTubeSchedules[0].PlaybackEndTime,
                PlaybackEndTime = videoTubeSchedules[0].PlaybackEndTime.AddSeconds(TimeSpan.FromHours(1.5).TotalSeconds),
                ProviderVideoId = "tube",
                VideoProviderName = "YouTube",
                VideoTubeId = 2,
                VideoTubeTitle = "My NHL for 2014"
            });

            var schedules = modelFactory.CreateChannelScheduleModels(videoTubeSchedules);

            return schedules.FirstOrDefault();
        }

        /// <summary>
        /// This method will retrieve schedule information for a specific video tube based
        /// on the parent channel schedule id and its order number within a schedule
        /// </summary>
        /// <param name="scheduleId">Channel Schedule Id</param>
        /// <param name="videoId">Video Tube Id</param>
        /// <param name="orderNumber">Playback Order Number</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/channel/schedule/{scheduleId:int}/video/{videoId:int}/order/{ordernumber:int}", Name = "VideoSchedule")]
        public VideoScheduleModel GetVideoSchedule(int scheduleId, int videoId, int orderNumber)
        {
            var modelFactory = new ModelFactory(this.Request);

            DateTime now = DateTime.Now;
            DateTime firstChannelStartTime = now.AddHours(-now.Hour);

            var videoSchedule = modelFactory.CreateVideoScheduleModel(new VideoSchedule()
            {
                CategoryId = 1,
                CategoryName = "Sport TV",
                ChannelScheduleId = 1,
                ChannelTubeId = 1,
                Description = "NHL 2014",
                Duration = (long)TimeSpan.FromHours(1.5).TotalSeconds,
                IsInPublicLibrary = true,
                IsPrivate = false,
                IsRemovedByProvider = false,
                IsRRated = false,
                PlaybackOrderNumber = 1,
                PlaybackStartTime = firstChannelStartTime,
                PlaybackEndTime = firstChannelStartTime.AddSeconds(TimeSpan.FromHours(1.5).TotalSeconds),
                ProviderVideoId = "tube",
                VideoProviderName = "YouTube",
                VideoTubeId = 2,
                VideoTubeTitle = "My NHL for 2014"
            });

            return videoSchedule;
        }

        [HttpPost]
        public string SendAbuseReport([FromBody]AbuseReportData reportData)
        {
            string message = "";

            DateTime senderTime = new DateTime();
            DateTime.TryParse(reportData.SenderDateTime, out senderTime);

            UserPo userSender = new UserPo();
            UserPo userAbused = new UserPo();
            ChannelTubePo channelTubePo = new ChannelTubePo();

            if (reportData.SenderUserId != 0)
            {
                ChannelSchedule currChannelSchedule = ChannelManage.GetChannelScheduleById(reportData.ChannelScheduleId);
                channelTubePo = ChannelManage.GetChannelTubePoById(reportData.ChannelId);
                userSender = UserManage.GetUserPoByUserId(reportData.SenderUserId);
                userAbused = UserManage.GetUserPoByUserId(channelTubePo.UserId);
            }
            else
            {
                return message = "please login to strimm before sending the abuse report";
            }

            List<VideoSchedule> videoScheduleList = new List<VideoSchedule>();

            if (reportData.ChannelScheduleId != 0)
            {
                videoScheduleList = ChannelManage.GetVideoSchedulesByChannelScheduleId(reportData.ChannelScheduleId);
            }
            else
            {
                return message = "There are no videos on this channel";
            }

            string videoScheduleInfo = "";
            if (videoScheduleList != null)
            {
                foreach (var v in videoScheduleList)
                {
                    var vu = VideoTubeManage.GetVideoTubeById(v.VideoTubeId);

                    videoScheduleInfo = "<div>" + videoScheduleInfo + v.PlaybackStartTime.ToString() + '-' + v.PlaybackEndTime.ToString() + '-' + vu.Title.Replace('"', '_') + "</div><br/>";
                }
            }
            string subject = "Abuse report. Account " + userAbused.AccountNumber;

            var abuseReportEmailUri = Server.MapPath("~/Emails/AbuseReport.html");

            string body = String.Empty;

            using (StreamReader reader = new StreamReader(abuseReportEmailUri))
            {
                body = reader.ReadToEnd();
            }

            string senderName = userSender.FirstName + " " + userSender.LastName;
            string channelOwnerName = userAbused.FirstName + " " + userAbused.LastName;
            body = body.Replace("{senderFullName}", senderName);
            body = body.Replace("{channelName}", channelTubePo.Name);
            body = body.Replace("{accontNumber}", userSender.AccountNumber);
            body = body.Replace("{selectedOption}", reportData.SelectedOption);
            body = body.Replace("{videoTitle}", reportData.VideoTitle);
            body = body.Replace("{comments}", comments);
            body = body.Replace("{channelOwnerName}", channelOwnerName);
            body = body.Replace("{abusedAccountNumber}", userAbused.AccountNumber);
            body = body.Replace("{videoscheduleinfo}", videoScheduleInfo);
            body = body.Replace("{senderEmail}", userSender.Email);




            bool isEmailSend = EmailUtils.SendEmail(subject, body, "report-abuse@strimm.com", userAbused.Email);
            if (isEmailSend)
            {
                message = "The report has been sent";
            }
            else
            {
                message = "the message didnt sent please try again later";
            }

            return message;
        }

        [HttpGet]
        [Route("api/channel/{channelTubeId:int}/rating", Name = "ChannelRating")]
        public string GetChannelRatingByChannelId(int channelTubeId)
        {
            ChannelTubePo channelTubePo = ChannelManage.GetChannelTubePoById(channelTubeId);
            return MiscUtils.ToFixed(channelTubePo.Rating, 1).ToString();
        }

        /// <summary>
        /// This method will retrieve all channel categories
        /// </summary>
        /// <returns>Collection of Categories</returns>
        [HttpGet]
        [Route("api/channel/categories", Name = "ChannelCategories")]
        public List<Category> GetChannelCategories()
        {
            return ReferenceDataManage.GetAllCategories();
        }

        /// <summary>
        /// This method will check a possible new channel name to see if it unique.
        /// </summary>
        /// <param name="value">Channel name</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/channel/validate/{channelName:string}", Name = "ValidateChannelName")]
        public bool CheckChannelName(string channelName)
        {
            Logger.Info(String.Format("Processing check new channel name request. Name to check is '{0}'", channelName));

            bool isExistChannelNameExist = ChannelManage.IsChannelNameExists(channelName);
            bool isChannelNameReserved = ChannelManage.IsChannelNameUnique(channelName);

            return (isExistChannelNameExist || isChannelNameReserved);
        }

        /// <summary>
        /// This method will add an existing video by id to a target channel
        /// </summary>
        /// <param name="channelTubeId">Target channel tube Id</param>
        /// <param name="videoTubeId">Target video tube id</param>
        /// <returns></returns>
        [HttpPost]
        public VideoTubeModel AddVideoToChannel(int channelTubeId, int videoTubeId)
        {
            return VideoTubeManage.AddVideoTubeToChannelTubeById(channelTubeId, videoTubeId);
        }

        [HttpPost]
        public VideoTubeModel AddExternalVideoToChannelForCategory(string providerVideoId, int channelTubeId, int categoryId)
        {
            return VideoTubeManage.AddVideoTubeToChannelTubeForCategory(String.Format("http://www.youtube.com/watch?v={0}", providerVideoId), channelTubeId, categoryId);
        }

        [WebMethod]
        public VideoTubeModel AddExternalVideoVimeoToChannelForCategory(VideoTubeModel videoTubeModel, int channelTubeId, int categoryId)
        {
            return VideoTubeManage.AddVideoTubeVimeoToChannelTubeForCategory(videoTubeModel, channelTubeId, categoryId);
        }

        /// <summary>
        /// This method will remove video tube from channel by id
        /// </summary>
        /// <param name="channelTubeId">Target channel id</param>
        /// <param name="videoTubeId">Target video tube id</param>
        /// <returns></returns>
        [WebMethod]
        public bool RemoveVideoFromChannel(int channelTubeId, int videoTubeId)
        {
            return VideoTubeManage.RemoveVideoTubeFromChannelTubeById(channelTubeId, videoTubeId);
        }

        /// <summary>
        /// This method will retrieve a list of video categories with counters that correspond to the number of
        /// videos in a specific category in the user's channel
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <returns>Collection of CategoryModels</returns>
        [WebMethod]
        public List<CategoryModel> GetVideoCategoriesForChannel(int channelId)
        {
            var categories = ReferenceDataManage.GetAllCategories();
            var videosByCategoryCounters = VideoTubeManage.CountVideoTubesInChannelTubeByCategoryByChannelTubeId(channelId);
            var categoryModels = new List<CategoryModel>();

            if (categories != null && categories.Count > 0)
            {
                categories.OrderBy(x => x.Name).ToList().ForEach(c =>
                {
                    var counter = videosByCategoryCounters != null ? videosByCategoryCounters.FirstOrDefault(cn => c.CategoryId == cn.EntityId) : null;
                    categoryModels.Add(new CategoryModel()
                    {
                        Name = counter != null ? String.Format("{0} ({1})", c.Name, counter.VideoTubeCounter) : c.Name,
                        CategoryId = c.CategoryId
                    });
                });
            }

            categoryModels.Insert(0, new CategoryModel()
            {
                Name = "all categories",
                CategoryId = 0
            });

            return categoryModels;
        }

        /// <summary>
        /// This method will clear/remove all videos that are part of a channel's library
        /// </summary>
        /// <param name="channelId">Channel Tube Id</param>
        /// <returns></returns>
        [WebMethod]
        public string ClearAllVideos(int channelId)
        {
            if (channelId <= 0)
            {
                return "Invalid channel information specified";
            }

            string response = "Videos were successfully deleted";

            if (!VideoTubeManage.ClearAllVideosFromChannel(channelId))
            {
                response = "Failed to delete videos from channel";
            }

            return response;
        }

        /// <summary>
        /// This method will remove all restricted/removed videos by provider from
        /// a target channel
        /// </summary>
        /// <param name="channelId">Channel Tube Id</param>
        /// <param name="videoIds">Collection of VideoTubeIds to remove from channel</param>
        /// <returns></returns>
        [WebMethod]
        public string ClearRestrictedOrRemovedVideos(int channelId, string videoIds)
        {
            if (channelId <= 0)
            {
                return "Invalid channel information specified";
            }

            var strVideoIds = videoIds.TrimEnd(',').Split(',');
            var intVideoIds = new List<int>();

            strVideoIds.ToList().ForEach(s =>
            {
                int id = 0;
                if (Int32.TryParse(s, out id))
                {
                    intVideoIds.Add(id);
                }
            });

            string response = "Videos were successfully removed";

            if (!VideoTubeManage.ClearRestrictedOrRemovedVideosFromChannel(channelId, intVideoIds))
            {
                response = "Unable to remove restricted or removed videos from channel";
            }

            return response;
        }

        [WebMethod]
        public string RemoveAllVideosFromChannel(int channelId)
        {
            if (channelId <= 0)
            {
                return "Invalid channel information specified";
            }

            string response = "Videos were successfully cleared";

            if (!VideoTubeManage.RemoveAllVideosFromChannel(channelId))
            {
                response = "Failed to clear all videos from channel";
            }

            return response;
        }

        /// <summary>
        /// This method will retrieve preview model for a channel that provides information
        /// on the currently playing schedule
        /// </summary>
        /// <param name="channelId">Channel Tube Id</param>
        /// <returns>Instance of ChannelPreviewModel</returns>
        [WebMethod]
        public ChannelPreviewModel GetCurrentlyPlayingChannelData(string clientTime, int channelId, int userId = 0)
        {
            Logger.Info(String.Format("Processing request for channel preview data for channel with Id={0} and for user with Id={1} at {2}", channelId, userId, clientTime));

            var date = DateTime.Parse(clientTime);

            return ChannelManage.GetCurrentlyPlayingChannelById(channelId, userId, date);
        }

        /// <summary>
        /// This method will retrieve all top/featured channels that are currently playing on the air
        /// </summary>
        /// <returns>Collection of ChannelTubeModels</returns>
        [WebMethod]
        public List<ChannelTubeModel> GetTopChannelsOnTheAir(string clientTime)
        {
            Logger.Info(String.Format("Processing request for top channels that are currently on the air at {0}", clientTime));

            var date = DateTime.Parse(clientTime);

            return ChannelManage.GetTopChannelsCurrentlyPlaying(date);
        }

        /// <summary>
        /// This method will retrieve all user favorite channels that are currently playing on the air
        /// based on user Id
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <returns>Collection of ChannelTubeModels</returns>
        [WebMethod]
        public List<ChannelTubeModel> GetAllFavoriteChannelsForUserByUserIdAndClientTime(int userId, string clientTime)
        {
            Logger.Info(String.Format("Processing request for all user subscribed channels currently on the air for user with Id={0} at {1}", userId, clientTime));

            var date = DateTime.Parse(clientTime);

            return ChannelManage.GetAllFavoriteChannelsForUserByUserIdAndClientTime(userId, date);
        }

        [WebMethod]
        public List<ChannelTubeModel> GetChannelsByUserIdAndClientTime(int userId, string clientTime)
        {
            Logger.Info(String.Format("Processing request for all user channels for user with Id={0} at {1}", userId, clientTime));

            var date = DateTime.Parse(clientTime);

            return ChannelManage.GetChannelsByUserIdAndClientTime(userId, date);
        }
        [WebMethod]
        public List<ChannelTubeModel> GetAllChannelsForUserByUserIdAndClientTime(int userId, string clientTime)
        {
            Logger.Info(String.Format("Processing request for all user subscribed channels currently on the air for user with Id={0} at {1}", userId, clientTime));

            var date = DateTime.Parse(clientTime);

            return ChannelManage.GetAllChannelsForUserByUserIdAndClientTime(userId, date);
        }

        /// <summary>
        /// This method should be used to add user subscription to a channel
        /// </summary>
        /// <param name="userId">User id</param>
        /// <param name="channelTubeId">Channel tube id</param>
        /// <param name="clientTime">Client request date and time</param>
        /// <returns></returns>
        [WebMethod]
        public ResponseMessageModel SubscribeUserForChannel(int userId, int channelTubeId, string clientTime)
        {
            Logger.Info(String.Format("Processing user subscription request for channel. UserId={0}, ChannelTubeId={1} as of {2}", userId, channelTubeId, clientTime.ToString()));

            var response = new ResponseMessageModel();

            DateTime clientTimeDateTime = new DateTime();
            DateTime.TryParseExact(clientTime, stringClientDateTimeFormat, new CultureInfo("en-US"), DateTimeStyles.AssumeLocal, out clientTimeDateTime);

            response.IsSuccess = ChannelManage.AddChannelSubscriptionForUser(userId, channelTubeId, clientTimeDateTime);
            response.Message = response.IsSuccess ? String.Empty : "Failed to subscribe user to channel. Try again later";

            return response;
        }

        /// <summary>
        /// This method will unsubscribe user from channel
        /// </summary>
        /// <param name="userId">User id</param>
        /// <param name="channelTubeId">Channel Tube Id</param>
        /// <param name="clientTime">Client request date and time</param>
        /// <returns></returns>
        [WebMethod]
        public ResponseMessageModel UnsubscribeUserFromChannel(int userId, int channelTubeId, string clientTime)
        {
            Logger.Info(String.Format("Processing user unsubscription request for channel. UserId={0}, ChannelTubeId={1} as of {2}", userId, channelTubeId, clientTime.ToString()));

            var response = new ResponseMessageModel();

            DateTime clientTimeDateTime = new DateTime();
            DateTime.TryParseExact(clientTime, stringClientDateTimeFormat, new CultureInfo("en-US"), DateTimeStyles.AssumeLocal, out clientTimeDateTime);

            response.IsSuccess = ChannelManage.DeleteChannelSubscriptionForUser(userId, channelTubeId, clientTimeDateTime);
            response.Message = response.IsSuccess ? String.Empty : "Failed to unsubscribe user from channel. Try again later";

            return response;
        }

        /// <summary>
        /// This method will add a channel view registration for a channel by user,
        /// meaning the user has viewed this channel. In case, when viewed by the user who has
        /// not loged into strimm, userId will not be specified
        /// </summary>
        /// <param name="userId">Nullable userId</param>
        /// <param name="channelTubeId">Channel tube id</param>
        /// <param name="viewTime">View time</param>
        /// <returns></returns>
        [WebMethod]
        public bool AddChannelViewForUser(int? userId, int channelTubeId, string viewTime)
        {
            Logger.Info(String.Format("Processing channel view registration request for User with Id={0}, channel Id={1} as of {2}", (userId != null ? userId.Value : -1), channelTubeId, viewTime));

            DateTime clientTimeDateTime = new DateTime();
            DateTime.TryParseExact(viewTime, stringClientDateTimeFormat, new CultureInfo("en-US"), DateTimeStyles.AssumeLocal, out clientTimeDateTime);

            List<UserChannelTubeView> views = ChannelManage.GetChannelViewByUseridChannelTubeIdAndViewTime(userId, channelTubeId, clientTimeDateTime);
            bool IsChannelViewAdded = false;
            string lastViewTime = "";
            if (views.Count != 0)
            {
                lastViewTime = views.Last().ViewTime.ToShortDateString();
                if (lastViewTime != clientTimeDateTime.ToShortDateString())
                {
                    IsChannelViewAdded = ChannelManage.AddChannelViewForUser(userId, channelTubeId, clientTimeDateTime);
                }
            }
            else
            {
                IsChannelViewAdded = ChannelManage.AddChannelViewForUser(userId, channelTubeId, clientTimeDateTime);
            }

            return IsChannelViewAdded;
        }

        /// <summary>
        /// This method will add a video view registation for a video, meaning the time when
        /// the user started to view this video. In case, when viewed by the user who
        /// has not logged into strimm, userId will not be specified
        /// </summary>
        /// <param name="userId">Nullable userId</param>
        /// <param name="videoTubeId">Video tube id</param>
        /// <param name="viewStartTime">View start</param>
        /// <returns></returns>
        [WebMethod(EnableSession = true)]
        public ResponseMessageModel AddVideoViewStartForUser(int videoTubeId, string viewStartTime)
        {
            var response = new ResponseMessageModel();
            int userId = 0;

            if (HttpContext.Current.Session["userId"] != null)
            {
                Int32.TryParse(HttpContext.Current.Session["userId"].ToString(), out userId);
            }

            Logger.Info(String.Format("Processing video view registration request for User with Id={0}, video Id={1} as of {2}", userId, videoTubeId, viewStartTime));

            DateTime clientTimeDateTime = new DateTime();
            DateTime.TryParseExact(viewStartTime, stringClientDateTimeFormat, new CultureInfo("en-US"), DateTimeStyles.AssumeLocal, out clientTimeDateTime);

            response.IsSuccess = VideoTubeManage.AddVideoViewForUser(userId, videoTubeId, clientTimeDateTime, true);
            response.Message = response.IsSuccess ? String.Empty : "Error occured while registering video view start event";

            return response;
        }

        /// <summary>
        /// This method will add a video view registation for a video, meaning the time when
        /// the user stopped watching this video. In case, when viewed by the user who
        /// has not logged into strimm, userId will not be specified
        /// </summary>
        /// <param name="userId">Nullable userId</param>
        /// <param name="videoTubeId">Video tube id</param>
        /// <param name="viewEndTime">View start</param>
        /// <returns></returns>
        [WebMethod(EnableSession = true)]
        public ResponseMessageModel AddVideoViewEndForUser(int videoTubeId, string viewEndTime)
        {
            var response = new ResponseMessageModel();
            int userId = 0;

            if (HttpContext.Current.Session["userId"] != null)
            {
                Int32.TryParse(HttpContext.Current.Session["userId"].ToString(), out userId);
            }

            Logger.Info(String.Format("Processing video view registration request for User with Id={0}, video Id={1} as of {2}", userId, videoTubeId, viewEndTime));

            DateTime clientTimeDateTime = new DateTime();
            DateTime.TryParseExact(viewEndTime, stringClientDateTimeFormat, new CultureInfo("en-US"), DateTimeStyles.AssumeLocal, out clientTimeDateTime);

            response.IsSuccess = VideoTubeManage.AddVideoViewForUser(userId, videoTubeId, clientTimeDateTime, false);
            response.Message = response.IsSuccess ? String.Empty : "Error occured while registering video view end event";

            return response;
        }

        /// <summary>
        /// This method will add video view events to previously viewed video and the currently being watched
        /// video for user. 
        /// </summary>
        /// <param name="prevVideoTubeId">Previously viewed video tube id</param>
        /// <param name="currentVideoTubeId">Currently watched video tube id</param>
        /// <param name="clientTime">Client's timestamp</param>
        /// <returns></returns>
        [WebMethod(EnableSession = true)]
        public ResponseMessageModel AddVideoViewsOnChangeForUser(int prevVideoTubeId, int currentVideoTubeId, string clientTime)
        {
            var response = new ResponseMessageModel();
            int userId = 0;

            if (HttpContext.Current.Session["userId"] != null)
            {
                Int32.TryParse(HttpContext.Current.Session["userId"].ToString(), out userId);
            }

            Logger.Info(String.Format("Processing video view registration request for User with Id={0}, prev. video Id={1}, current video Id={2} as of {3}", userId, prevVideoTubeId, currentVideoTubeId, clientTime));

            DateTime clientTimeDateTime = new DateTime();
            DateTime.TryParseExact(clientTime, stringClientDateTimeFormat, new CultureInfo("en-US"), DateTimeStyles.AssumeLocal, out clientTimeDateTime);

            bool isPrevVideoSuccess = VideoTubeManage.AddVideoViewForUser(userId, prevVideoTubeId, clientTimeDateTime, false);
            bool isNextVideoSuccess = VideoTubeManage.AddVideoViewForUser(userId, currentVideoTubeId, clientTimeDateTime, true);

            response.IsSuccess = isPrevVideoSuccess && isNextVideoSuccess;
            response.Message = response.IsSuccess ? String.Empty : "Error occured while registering video view event";

            return response;
        }

        /// <summary>
        /// This method should be used to add video to user's archive for later
        /// viewing
        /// </summary>
        /// <param name="videoId">Video Tube Id</param>
        /// <param name="clientTime">Client's timestamp</param>
        /// <returns></returns>
        [WebMethod(EnableSession = true)]
        public ResponseMessageModel AddVideoToUserArchive(int videoId, string clientTime)
        {
            Logger.Info(String.Format("Processing add video to user archive request with videoTubeId={0} as of '{1}'", videoId, clientTime));

            var response = new ResponseMessageModel();
            int userId = 0;

            if (HttpContext.Current.Session["userId"] != null && Int32.TryParse(HttpContext.Current.Session["userId"].ToString(), out userId))
            {
                DateTime clientTimeDateTime = new DateTime();
                DateTime.TryParseExact(clientTime, stringClientDateTimeFormat, new CultureInfo("en-US"), DateTimeStyles.AssumeLocal, out clientTimeDateTime);

                if (!VideoTubeManage.IsVideoTubePartOfUserArchive(userId, videoId))
                {
                    response.IsSuccess = VideoTubeManage.AddVideoTubeToUserArchive(userId, videoId, clientTimeDateTime);
                    response.Message = response.IsSuccess ? "The video has been added to Watch Later folder"
                                                          : "Error occured while adding video to user archive. Try again later";
                }
                else
                {
                    response.IsSuccess = true;
                    response.Message = "Selected video is already part of user archive";
                }
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Error occurred while adding video to user archive. Invalid user specified";
            }

            return response;
        }

        [WebMethod(EnableSession = true)]
        public List<ChannelTubeModel> GetNamesOfAllChannelsForUser()
        {
            var models = new List<ChannelTubeModel>();
            int userId = 0;

            if (HttpContext.Current.Session["userId"] != null && Int32.TryParse(HttpContext.Current.Session["userId"].ToString(), out userId))
            {
                Logger.Info(String.Format("Retrieving names of all channels that exist for user with Id={0}", userId));

                var channels = ChannelManage.GetChannelTubesForUser(userId);

                if (channels != null && channels.Count > 0)
                {
                    channels.ForEach(x => models.Add(new ChannelTubeModel(x)));
                }
            }
            else
            {
                models.Add(new ChannelTubeModel() { IsSuccess = false, Message = "Invalid user specified. Channel names cannot be retrieved" });
            }

            return models;
        }

        /// <summary>
        /// This method should be used to get all user`s channels to page
        /// 
        /// </summary>
        /// <param name="userId">user id</param>
        /// <returns>list of channel tubes</returns>
        [WebMethod]
        public List<ChannelTubeModel> GetAllChannelsByUserId(int userId, string clientTime)
        {
            Logger.Info(String.Format("Processing request for all user`s channels for user with Id={0} at {1}", userId, clientTime));
            var date = DateTime.Parse(clientTime);
            return ChannelManage.GetAllChannelsForUserByUserIdAndClientTime(userId, date);
        }

        [WebMethod]
        public List<ChannelTubeModel> GetCurrentlyPlayingChannels(string clientTime)
        {
            var date = DateTime.Parse(clientTime);
            return ChannelManage.GetCurrentlyPlayingChannels(date);
        }

        [WebMethod]
        public List<ChannelTubeModel> GetCurrentlyPlayingChannelsByCategoryName(string clientTime, string categoryName)
        {
            DateTime clientTimeDateTime = new DateTime();
            DateTime.TryParseExact(clientTime, stringClientDateTimeFormat, new CultureInfo("en-US"), DateTimeStyles.AssumeLocal, out clientTimeDateTime);
            var channels = new List<ChannelTubeModel>();

            if (!String.IsNullOrEmpty(categoryName) && categoryName.ToLower() != "most popular")
            {
                channels = ChannelManage.GetCurrentlyPlayingChannelsByCategoryName(clientTimeDateTime, categoryName);
            }
            else
            {
                channels = ChannelManage.GetTopChannelsCurrentlyPlaying(clientTimeDateTime);
            }

            return channels;
        }

        [WebMethod]
        public LandingPageDataModel GetCurrentlyPlayingChannelsForLandingPage(string clientTime)
        {
            DateTime clientTimeDateTime = new DateTime();
            DateTime.TryParseExact(clientTime, stringClientDateTimeFormat, new CultureInfo("en-US"), DateTimeStyles.AssumeLocal, out clientTimeDateTime);

            var model = ChannelManage.GetCurrentlyPlayingChannelsForLandingPage(clientTimeDateTime);

            return model;
        }

        [WebMethod]
        public bool SetChannelRatingByUserIdAndChannelTubeId(int userId, int channelTubeId, float ratingValue, string enteredDate)
        {
            DateTime clientTimeDateTime = new DateTime();
            DateTime.TryParseExact(enteredDate, stringClientDateTimeFormat, new CultureInfo("en-US"), DateTimeStyles.AssumeLocal, out clientTimeDateTime);
            return ChannelManage.SetChannelRatingByUserIdAndChannelTubeId(userId, channelTubeId, ratingValue, clientTimeDateTime);
        }

        [WebMethod]
        public string AddCreateChannelControlToPage(string channelname, int userId)
        {
            using (Page page = new Page())
            {
                HtmlForm form = new HtmlForm();
                CreateChannelUC userControl = (CreateChannelUC)page.LoadControl("~/UC/CreateChannelUC.ascx");

                userControl.channelname = channelname;
                userControl.userId = userId;

                form.Controls.Add(userControl);
                using (StringWriter writer = new StringWriter())
                {
                    page.Controls.Add(form);
                    HttpContext.Current.Server.Execute(page, writer, false);
                    return writer.ToString();
                }
            }
        }

        [WebMethod]
        public bool CheckChannelNameIsTaken(string channelName)
        {
            return ChannelManage.IsChannelNameExists(channelName);
        }

        [WebMethod]
        public bool CheckChannelNameIsReserved(string channelName)
        {
            return ChannelManage.IsChannelNameUnique(channelName);
        }

        [WebMethod]
        public bool CreateNewChannel(int categoryId, string channelName, string channelUrl, string channelImgData, int userId, string fileName)
        {
            string channelPicUrl = String.Empty;
            List<ChannelTubePo> channelTubeList = ChannelManage.GetChannelTubesForUser(userId);
            bool insertPublicChannels = channelTubeList == null || channelTubeList.Count == 0;
            ChannelTube channeltube = new ChannelTube();
            if (!String.IsNullOrEmpty(channelImgData))
            {
                channelPicUrl = AWSManage.UploadChannelImageToS3(channelImgData, fileName, userId, channeltube.ChannelTubeId);
            }

            if (ChannelManage.CreateChannelTube(categoryId, channelName, "", channelPicUrl, channelUrl, userId, insertPublicChannels, 10, out channeltube))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        [WebMethod]
        public ChannelTubePo UpdateChannelForModal(int channelTubeId, string fileName, string imageData, int categoryId, int userId)
        {
            ChannelTubePo channelPo = new ChannelTubePo();
            ChannelTube channel = ChannelManage.GetChannelTubeById(channelTubeId);
            string channelPicUrl = String.Empty;

            if (!String.IsNullOrEmpty(imageData))
            {
                channel.PictureUrl = AWSManage.UploadChannelImageToS3(imageData, fileName, userId, channelTubeId);
            }
            if (channel.CategoryId != categoryId)
            {
                channel.CategoryId = categoryId;
            }
            ChannelManage.UpdateChannelTube(channel);
            channelPo = ChannelManage.GetChannelTubePoById(channelTubeId);

            return channelPo;


        }

        [WebMethod]
        public bool DeleteChannelForModal(int channelTubeId)
        {
            return ChannelManage.DeleteChannelTubeById(channelTubeId);
        }
    }
}
