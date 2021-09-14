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
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using Strimm.Model.Projections;
using Strimm.Model.WebModel;
using log4net;
using System.Web.UI.HtmlControls;
using System.Configuration;
using Strimm.Model.Criteria;
using StrimmTube.Utils;

namespace StrimmTube.WebServices
{
    /// <summary>
    /// Summary description for ChannelWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class ChannelWebService : System.Web.Services.WebService
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ChannelWebService));
        private static readonly string stringClientDateTimeFormat = "MM/dd/yyyy HH:mm";
        //private static readonly int pageSize = 10;

        [WebMethod(EnableSession = true)]
        public void SetChannelSession(int selectedIndex, int selectedValue)
        {
            HttpContext.Current.Session["channelTubeId"] = selectedValue;
            HttpContext.Current.Session["selectedIndex"] = selectedIndex;

        }
        
        [WebMethod(EnableSession = true)]
        public string SendAbuseReport(string selectedOption, string videoTitle, string comments, string senderDateTime, int channelScheduleId = 0, int videoTubeId = 0, int senderUserId = 0, int channelId = 0)
        {
            string message = "";
            DateTime senderTime = DateTimeUtils.GetClientTime(senderDateTime) ?? DateTime.Now;
            UserPo userSender = new UserPo();
            UserPo userAbused = new UserPo();
            ChannelTubePo channelTubePo = new ChannelTubePo();

            if (senderUserId != 0)
            {
                ChannelSchedule currChannelSchedule = ChannelManage.GetChannelScheduleById(channelScheduleId);
                channelTubePo = ChannelManage.GetChannelTubePoById(channelId);
                userSender = UserManage.GetUserPoByUserId(senderUserId);
                userAbused = UserManage.GetUserPoByUserId(channelTubePo.UserId);
            }
            else
            {
                return message = "please login to strimm before sending the abuse report";
            }
           
          
            List<VideoSchedule> videoScheduleList = new List<VideoSchedule>();
           
            
            if (channelScheduleId != 0)
            {

                videoScheduleList = ChannelManage.GetVideoSchedulesByChannelScheduleId(channelScheduleId);
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
                    //string vrName = ManageVR.getVrNameByVideoUploadId(v.videoUploadId);

                    videoScheduleInfo = "<div>" + videoScheduleInfo + v.PlaybackStartTime.ToString() + '-' + v.PlaybackEndTime.ToString() + '-' + vu.Title.Replace('"', '_') + "</div><br/>";
                }
            }
            string subject = "Abuse report submitted by " + userAbused.UserName;
           
            var abuseReportEmailUri = Server.MapPath("~/Emails/AbuseReport.html");

            string body = String.Empty;

            using (StreamReader reader = new StreamReader(abuseReportEmailUri))
            {
                body = reader.ReadToEnd();
            }

            string domainName = ConfigurationManager.AppSettings["domainName"] ?? "www.strimm.com";

            string senderName = userSender.FirstName + " " + userSender.LastName;
            string channelOwnerName = userAbused.FirstName + " " + userAbused.LastName;
            body = body.Replace("{senderFullName}", senderName);
            body = body.Replace("{channelName}", channelTubePo.Name);
            body = body.Replace("{publicName}", userSender.UserName);
            body = body.Replace("{selectedOption}", selectedOption);
            body = body.Replace("{videoTitle}", videoTitle);
            body = body.Replace("{comments}", comments);
            body = body.Replace("{channelOwnerName}", channelOwnerName);
            body = body.Replace("{abusedPublicName}", userAbused.UserName);
            body = body.Replace("{videoscheduleinfo}", videoScheduleInfo);
            body = body.Replace("{senderEmail}", userSender.Email);
            body = body.Replace("{domainName}", domainName);           
             
         
          
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
        [WebMethod]
        public string GetChannelRatingByChannelId(int channelTubeId)
        {
            ChannelTubePo channelTubePo = ChannelManage.GetChannelTubePoById(channelTubeId);
            return MiscUtils.ToFixed(channelTubePo.Rating, 1).ToString();
        }
        [WebMethod(EnableSession = true)]
        public void RingToFollowers(int channelId)
        {
            //int offset = SendOffset();
            //ChannelSchedule cs = ChannelManage.getCurrentChannelSchedule(channelId, offset);
            //int followUserId = 0;
            //if (HttpContext.Current.Session["userId"] != null)
            //{
            //    followUserId = int.Parse(HttpContext.Current.Session["userId"].ToString());
            //    Rings ring = new Rings();
            //    ring.channelScheduleId = cs.channelScheduleId;
            //    ring.endOfSchedule = cs.endTime;
            //    ring.time = DateTime.Now;
            //    ring.followId = followUserId;
            //    ChannelManage.AddRing(ring);

            //}
        }

        [WebMethod(EnableSession = true)]
        public string PollRings(int userId)
        {
            //int offset = SendOffset();
            //Page page = new Page();
            //StringBuilder sb = new StringBuilder();
            if (userId != 0)
            {

                //List<Rings> ringList = ChannelManage.GetFollowingRings(userId);
                //List<Rings> newRings = new List<Rings>();
                //if (ringList.Count != 0)
                //{
                //    DateTime dateTimeNow = DateTime.Now;
                //    //get new rings by ring Id



                //    foreach (var r in ringList)
                //    {
                //        if (r.endOfSchedule > dateTimeNow)
                //        {
                //            newRings.Add(r);
                //        }
                //    }
                // }


                //    if (newRings.Count != 0)
                //    {
                //        newRings = newRings.OrderByDescending(x => x.ringId).ToList();
                //        var lastRing = newRings.First();

                //        foreach (var ring in newRings)
                //        {
                //            string path = "~/UC/RingBoxUC.ascx";
                //            RingBoxUC ringCtrl = (RingBoxUC)page.LoadControl(path);
                //            ChannelTube channel = ChannelManage.GetChannelTubeByChannelScheduleId(ring.channelScheduleId);
                //            ringCtrl.channelUrl ="../"+ channel.channelUrl;
                //            ringCtrl.dateTimeOfRing = ring.time.ToShortTimeString();
                //            ringCtrl.userNameRingSender = BoardManage.GetUserBoardNameByUserId(ring.followId);
                //            if(String.IsNullOrEmpty(channel.channelPictureUrl))
                //            {
                //                ringCtrl.channelImage = "/images/comingSoonBG.jpg";
                //            }
                //            else
                //            { 
                //            ringCtrl.channelImage = channel.channelPictureUrl;
                //            }
                //            ringCtrl.channelName = channel.channelName;
                //            ringCtrl.ringId = ring.ringId.ToString();
                //            StringWriter output = new StringWriter(sb);
                //            HtmlTextWriter hw = new HtmlTextWriter(output);
                //            ringCtrl.RenderControl(hw);
                //        }

                //    }
                //}
                //if (!String.IsNullOrEmpty(sb.ToString()))
                //{
                //    return sb.ToString();
                //}
                //else
                //{
                //    return "0";
                //}

            }
            return "0";

        }

        [WebMethod(EnableSession = true)]
        public bool IsNewRing()
        {
            //int offset = SendOffset();
            //Rings lastRing =new Rings();
            //bool isNew = false;

            //if(Session["lastRing"]!=null)
            //{ 
            //     lastRing = Session["lastRing"] as Rings;
            //}
            //if (HttpContext.Current.Session["userId"] != null)
            //{
            //    int userId = int.Parse(HttpContext.Current.Session["userId"].ToString());
            //    List<Rings> ringList = ChannelManage.GetFollowingRings(userId);
            //    lastRing = ringList.Last();
            //    Session["lastRing"] = lastRing;
            //    List<Rings> newRings = new List<Rings>();
            //    if (ringList.Count != 0)
            //    {
            //        DateTime dateTimeNow = DateTime.Now;
            //        //get new rings by ring Id



            //        foreach (var r in ringList)
            //        {
            //            if (r.endOfSchedule > dateTimeNow)
            //            {
            //                newRings.Add(r);
            //            }
            //        }
            //    }
            //    if (newRings.Count != 0)
            //    {
            //        newRings = newRings.OrderByDescending(x => x.ringId).ToList();
            //        var lastRingNew = newRings.First();
            //        if(lastRingNew.ringId!=lastRing.ringId)
            //        {
            //            isNew = true;
            //        }
            //        else
            //        {
            //            isNew = false;
            //        }
            //    }

            //}
            return false;
        }

        [WebMethod(EnableSession = true)]
        public List<Category> GetChannelCategories()
        {
            return ReferenceDataManage.GetAllCategories();
        }
        [WebMethod(EnableSession = true)]
        public List<Category> GetChannelCategoriesForVideos(int videoTypeId)
        {
            return ReferenceDataManage.GetAllCategories(videoTypeId);
        }

        [WebMethod]
        public List<Language> GetChannelLanguages()
        {
            return ReferenceDataManage.GetAllLanguages();
        }

        /// <summary>
        /// This method will check a possible new channel name to see if it unique.
        /// </summary>
        /// <param name="value">Channel name</param>
        /// <returns></returns>
        [WebMethod]
        public bool CheckChannelName(string value)
        {
            Logger.Info(String.Format("Processing check new channel name request. Name to check is '{0}'", value));
            bool isExistChannelNameExist = ChannelManage.IsChannelNameExists(value);
            bool isChannelNameReserved = ChannelManage.IsChannelNameUnique(value);
            if (isExistChannelNameExist || isChannelNameReserved)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }

        /// <summary>
        /// This method will add an existing video by id to a target channel
        /// </summary>
        /// <param name="channelTubeId">Target channel tube Id</param>
        /// <param name="videoTubeId">Target video tube id</param>
        /// <returns></returns>
        [WebMethod]
        public VideoTubeModel AddVideoToChannel(int channelTubeId, int videoTubeId)
        {
            return VideoTubeManage.AddVideoTubeToChannelTubeById(channelTubeId, videoTubeId);
        }

        [WebMethod]
        public VideoTubeModel AddPrivateVideoToChannel(int channelTubeId, int userId, int provideId, string description, string title, string videoUrl, int categoryId, bool isMatureContent, double durationInSec, string thumbnailUrl, string thumbnailImageData)
        {
            var channel = ChannelManage.GetChannelTubeById(channelTubeId);
            string videoPicUrl = String.Empty;
            VideoTubeModel video = null;
            VideoTubeModel existingVideo = null;
            existingVideo = VideoTubeManage.GetPrivateVideoByProviderIdAndChannelId(videoUrl, channelTubeId);
            if (!String.IsNullOrEmpty(thumbnailImageData))
            {
                videoPicUrl = AWSManage.UploadPrivateVideoImageToS3(thumbnailImageData, userId);
            }

            if ((channel != null) && (existingVideo==null))
            {
                var user = UserManage.GetUserPoByUserId(channel.UserId);

                if (user != null && channel.IsPrivate)
                {
                    if ((!isMatureContent) || (isMatureContent && channel.MatureContentEnabled ))
                    {
                        video = VideoTubeManage.AddPrivateVideoToChannelTubeById(channelTubeId, provideId, description, title, videoUrl, categoryId, isMatureContent, durationInSec, videoPicUrl);
                    }
                    else 
                    {
                        Logger.Warn(String.Format("Unable to add mature video to the channel. Mature content is not allowed for this channel or user. Video Title='{0}' and channel id={1}", title, channelTubeId));
                    }
                }
                else 
                {
                    Logger.Warn(String.Format("Unable to add private video to the channel. Private video content is not allowed for this channel or user. Video Title='{0}' and channel id={1}", title, channelTubeId));
                }
            }

            return video;
        }

        [WebMethod]
        public VideoTubeModel UpdatePrivateVideoById(int userId, int videoTubeId, int provideId, string description, string title, string videoUrl, int categoryId, bool isMatureContent, double durationInSec, string thumbnailUrl, string thumbnailImageData)
        {
            string videoPicUrl = String.Empty;
            VideoTubeModel video = null;
            if (!String.IsNullOrEmpty(thumbnailImageData))
            {
                videoPicUrl = AWSManage.UploadPrivateVideoImageToS3(thumbnailImageData, userId);
            }
            

            video = VideoTubeManage.UpdatePrivateVideoById(videoTubeId, provideId, description, title, videoUrl, categoryId, isMatureContent, durationInSec, videoPicUrl);

            return video;
        }

        /// <summary>
        /// This method will update start and end time on the live video by id
        /// </summary>
        /// <param name="videoLiveTubeId">Target live video id</param>
        /// <param name="startTime">New start time</param>
        /// <param name="endTime">New end time</param>
        /// <returns></returns>
        /// 
        [WebMethod]
        public List<VideoLiveTubePo> GetAllVideoLiveTubePosByChannelIdAndDate(int channelTubeId, string targetDate)
        {
            DateTime targetDateFromString;
            DateTime.TryParseExact(targetDate, stringClientDateTimeFormat, new CultureInfo("en-US"), DateTimeStyles.AssumeLocal, out targetDateFromString);
            return ChannelManage.GetAllVideoLiveTubePosByChannelIdAndDate(channelTubeId, targetDateFromString);
        }
        [WebMethod]
        public VideoTubeModel UpdateLiveVideoById(int videoLiveTubeId, DateTime startTime, DateTime? endTime)
        {
            return VideoTubeManage.UpdateVideoLiveTubeById(videoLiveTubeId, startTime, endTime);
        }

        [WebMethod]
        public VideoTubeModel AddExternalVideoToChannelForCategory(string providerVideoId, int channelTubeId, int categoryId)
        {
            return VideoTubeManage.AddVideoTubeToChannelTubeForCategory(String.Format("http://www.youtube.com/watch?v={0}", providerVideoId), channelTubeId, categoryId);
        }

        /// <summary>
        /// This method will add an external live video to strimm for a specific category and with 
        /// a concrete start/end time
        /// </summary>
        /// <param name="providerVideoId">Provide video id for a target live video</param>
        /// <param name="channelTubeId">Target channel id</param>
        /// <param name="categoryId">Selected category id</param>
        /// <param name="startTime">Video start time</param>
        /// <param name="endTime">Video end time</param>
        /// <returns>Instance of <see cref="VideoTubeModel"/></returns>
        [WebMethod]
        public VideoTubeModel AddExternalLiveVideoToChannelForCategory(string providerVideoId, int channelTubeId, int categoryId, string startTimeUTC, string endTimeUTC, int userId)
        {
            DateTime startTime;
            DateTime? endTime = null;

            if (!String.IsNullOrEmpty(endTimeUTC))
            {
                DateTime end;
                DateTime.TryParse(endTimeUTC, out end);
                endTime = DateTime.SpecifyKind(end, DateTimeKind.Utc);
            }

            DateTime.TryParse(startTimeUTC, out startTime);
            startTime = DateTime.SpecifyKind(startTime, DateTimeKind.Utc);

            return VideoTubeManage.AddLiveVideoTubeToChannelTubeForCategory(String.Format("http://www.youtube.com/watch?v={0}", providerVideoId), channelTubeId, categoryId, startTime, endTime, userId);

        }

        [WebMethod]
        public VideoTubeModel AddExternalVideoToChannelForCategoryWithCustomDuration(string providerVideoId, int channelTubeId, int categoryId, long customDuration)
        {
            return VideoTubeManage.AddVideoTubeToChannelTubeForCategoryWithCustomDuration(String.Format("http://www.youtube.com/watch?v={0}", providerVideoId), channelTubeId, categoryId, customDuration);
        }

        [WebMethod]
        public List<VideoTubeModel> AddAllImportedYoutubeVideos(List<string> videoTubeModel, int channelId, int categoryId)
        {
            return VideoTubeManage.AddListOfYoutubeVideosToChannelForCategory(videoTubeModel, channelId, categoryId);
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
        /// This method will remove a live video tube from channel by id
        /// </summary>
        /// <param name="channelTubeId">Target channel id</param>
        /// <param name="videoLiveTubeId">Target live video id</param>
        /// <returns>True/False</returns>
        [WebMethod]
        public bool RemoveLiveVideoFromChannel(int channelTubeId, int videoLiveTubeId)
        {
            return VideoTubeManage.RemoveVideoLiveTubeFromChannelTubeById(channelTubeId, videoLiveTubeId);
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
            var categories = ReferenceDataManage.GetAllCategories(2);
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

            string response = "Videos were successfully deleted.";

            if (!VideoTubeManage.ClearAllVideosFromChannel(channelId))
            {
                response = "Failed to delete videos from channel.";
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

            string response = "Videos were successfully removed.";

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

            string response = "Videos were successfully cleared.";

            if (!VideoTubeManage.RemoveAllVideosFromChannel(channelId))
            {
                response = "Failed to clear all videos from channel.";
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
        public ChannelPreviewModel GetCurrentlyPlayingChannelData(string clientTime, int channelId,bool isEmbeddedChannel, string userId)
        {
            Logger.Info(String.Format("Processing request for channel preview data for channel with Id={0} and for user with Id={1} at {2}", channelId, userId, clientTime));

            ChannelPreviewModel channelData = new ChannelPreviewModel();

            try
            {
                if (userId.Contains('.'))
                {
                    Logger.Debug(String.Format("Blocking bad request with user id '{0}'", userId));

                    return null;
                }

                var date = DateTimeUtils.GetClientTime(clientTime) ?? DateTime.Now;
                int clientUserId = 0;
                Int32.TryParse(userId, out clientUserId);

                if (isEmbeddedChannel)
                {
                    channelData = ChannelManage.GetCurrentlyPlayingChannelByIdForEmbedding(channelId, clientUserId, date);
                }
                else
                {
                    channelData = ChannelManage.GetCurrentlyPlayingChannelById(channelId, clientUserId, date);
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error occured while retrieving channel data", ex);
            }

            return channelData;
        }

        /// <summary>
        /// This method will retrieve all top/featured channels that are currently playing on the air
        /// </summary>
        /// <returns>Collection of ChannelTubeModels</returns>
        [WebMethod]
        public List<ChannelTubeModel> GetTopChannelsOnTheAir(string clientTime)
        {
            Logger.Info(String.Format("Processing request for top channels that are currently on the air at {0}", clientTime));

            var date = DateTimeUtils.GetClientTime(clientTime) ?? DateTime.Now;

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

            var date = DateTimeUtils.GetClientTime(clientTime) ?? DateTime.Now;

            return ChannelManage.GetAllFavoriteChannelsForUserByUserIdAndClientTime(userId, date);
        }

        [WebMethod]
        public List<ChannelTubeModel> GetChannelsByUserIdAndClientTime(int userId, string clientTime)
        {
            Logger.Info(String.Format("Processing request for all user channels for user with Id={0} at {1}", userId, clientTime));

            var date = DateTimeUtils.GetClientTime(clientTime) ?? DateTime.Now;

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

            DateTime clientTimeDateTime = DateTimeUtils.GetClientTime(clientTime) ?? DateTime.Now;

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
            Logger.Info(String.Format("Processing user unsubscription request for channel. UserId={0}, ChannelTubeId={1} as of {2}", userId, channelTubeId, clientTime));

            var response = new ResponseMessageModel();

            DateTime clientTimeDateTime = DateTimeUtils.GetClientTime(clientTime) ?? DateTime.Now;

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

            DateTime clientTimeDateTime = DateTimeUtils.GetClientTime(viewTime) ?? DateTime.Now;

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

            DateTime clientTimeDateTime = DateTimeUtils.GetClientTime(viewStartTime) ?? DateTime.Now;

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

            DateTime clientTimeDateTime = DateTimeUtils.GetClientTime(viewEndTime) ?? DateTime.Now;

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

            DateTime clientTimeDateTime = DateTimeUtils.GetClientTime(clientTime) ?? DateTime.Now;

            bool isPrevVideoSuccess = (prevVideoTubeId > 0) ? VideoTubeManage.AddVideoViewForUser(userId, prevVideoTubeId, clientTimeDateTime, false) : true;
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
                DateTime clientTimeDateTime = DateTimeUtils.GetClientTime(clientTime) ?? DateTime.Now;

                if (!VideoTubeManage.IsVideoTubePartOfUserArchive(userId, videoId))
                {
                    response.IsSuccess = VideoTubeManage.AddVideoTubeToUserArchive(userId, videoId, clientTimeDateTime);
                    response.Message = response.IsSuccess ? "The video has been added to Watch Later folder"
                                                          : "Error occured while adding video to user archive. Try again later";
                }
                else
                {
                    response.IsSuccess = true;
                    response.Message = "Selected video is already part of the user archive.";
                }
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Error occurred while adding video to the user archive, invalid user specified.";
            }

            return response;
        }

        [WebMethod(EnableSession = true)]
        public List<ChannelTubeModel> GetNamesOfAllChannelsForUser(int userId)
        {
            var models = new List<ChannelTubeModel>();
            //int userId = 0;

            //if (HttpContext.Current.Session["userId"] != null && Int32.TryParse(HttpContext.Current.Session["userId"].ToString(), out userId))
            //{
            //    Logger.Info(String.Format("Retrieving names of all channels that exist for user with Id={0}", userId));

            //    var channels = ChannelManage.GetChannelTubesForUser(userId).OrderBy(x => x.Name).ToList();

            //    if (channels != null && channels.Count > 0)
            //    {
            //        channels.ForEach(x => models.Add(new ChannelTubeModel(x)));
            //    }
            //}
            //else 
            //{
            //    models.Add(new ChannelTubeModel() { IsSuccess = false, Message = "Invalid user specified. Channel names cannot be retrieved" });
            //}
            if(userId!=0||userId!=null)
            {
                Logger.Info(String.Format("Retrieving names of all channels that exist for user with Id={0}", userId));

                var channels = ChannelManage.GetChannelTubesForUser(userId).OrderBy(x => x.Name).ToList();

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
            var date = DateTimeUtils.GetClientTime(clientTime) ?? DateTime.Now;
            var channels =  ChannelManage.GetAllChannelsForUserByUserIdAndClientTime(userId, date);

            channels.ForEach(x =>
            {
                x.PictureUrl = ImageUtils.GetChannelImageUrl(x.PictureUrl);
                x.CustomLogo = ImageUtils.GetChannelImageUrl(x.CustomLogo);
            });

            return channels;
        }

        [WebMethod]
        public List<ChannelTubeModel> GetCurrentlyPlayingChannels(string clientTime)
        {
            var date = DateTimeUtils.GetClientTime(clientTime) ?? DateTime.Now;
            var channels = ChannelManage.GetCurrentlyPlayingChannels(date);

            channels.ForEach(x =>
            {
                x.PictureUrl = ImageUtils.GetChannelImageUrl(x.PictureUrl);
                x.CustomLogo = ImageUtils.GetChannelImageUrl(x.CustomLogo);
            });

            return channels;
        }

        [WebMethod]
        public List<ChannelTubeModel> GetCurrentlyPlayingChannelsByCategoryName(string clientTime, string categoryName)
        {
            DateTime clientTimeDateTime = DateTimeUtils.GetClientTime(clientTime) ?? DateTime.Now;

            var channels = new List<ChannelTubeModel>();

            if (!String.IsNullOrEmpty(categoryName) && categoryName.ToLower() != "all channels")
            {
                channels = ChannelManage.GetCurrentlyPlayingChannelsByCategoryName(clientTimeDateTime, categoryName);
            }
            else
            {
                channels = ChannelManage.GetCurrentlyPlayingChannels(clientTimeDateTime);
            }


            channels.ForEach(x =>
            {
                x.PictureUrl = ImageUtils.GetChannelImageUrl(x.PictureUrl);
                x.CustomLogo = ImageUtils.GetChannelImageUrl(x.CustomLogo);
            });

            return channels;
        }
        [WebMethod]
        public List<ChannelTubeModel> GetCurrentlyPlayingChannelsByCategoryNameAndSelectedLanguage(string clientTime,  int languageId)
        {
            DateTime clientTimeDateTime = DateTimeUtils.GetClientTime(clientTime) ?? DateTime.Now;

            var channels = ChannelManage.GetCurrentlyPlayingChannelsByCategoryNameAndLanguageId(clientTimeDateTime,languageId);

            channels.ForEach(x =>
            {
                x.PictureUrl = ImageUtils.GetChannelImageUrl(x.PictureUrl);
                x.CustomLogo = ImageUtils.GetChannelImageUrl(x.CustomLogo);
            });

            return channels;
        }

        [WebMethod]
        public List<ChannelTubeModel> GetCurrentlyPlayingChannelsByCategoryNameForChannel(string clientTime, int currentChannelId, string categoryName)
        {
            DateTime clientTimeDateTime = DateTimeUtils.GetClientTime(clientTime) ?? DateTime.Now;

            var channels = new List<ChannelTubeModel>();
            var numberOfChannelsToPage = new List<ChannelTubeModel>();
            if (!String.IsNullOrEmpty(categoryName) && categoryName.ToLower() != "all channels")
            {
                channels = ChannelManage.GetCurrentlyPlayingChannelsByCategoryName(clientTimeDateTime, categoryName);
                numberOfChannelsToPage = channels.Where(c => c.ChannelTubeId != currentChannelId).OrderBy(x => Guid.NewGuid()).Take(6).ToList();
            }
            else
            {
                channels = ChannelManage.GetCurrentlyPlayingChannels(clientTimeDateTime);
                numberOfChannelsToPage = channels.Where(c => c.ChannelTubeId != currentChannelId).OrderBy(x => Guid.NewGuid()).OrderByDescending(x => x.PictureUrl != null).Take(6).ToList();

            }

            numberOfChannelsToPage.ForEach(x =>
            {
                x.PictureUrl = ImageUtils.GetChannelImageUrl(x.PictureUrl);
                x.CustomLogo = ImageUtils.GetChannelImageUrl(x.CustomLogo);
            });

            return numberOfChannelsToPage;
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
            DateTime clientTimeDateTime = DateTimeUtils.GetClientTime(enteredDate) ?? DateTime.Now;
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
        public bool CreateNewChannel(int categoryId, string channelName, string channelUrl, string channelImgData, int userId, string fileName, string channelDescription, int languageId, bool isWhiteLabeled, string channelPassword, bool embedEnabled, bool muteOnStartup, string customLabel, string customLogoImgData, string subscriberDomain, bool embedOnlyMode, bool matureContentEnabled, bool showPlayerControls, bool isPrivate, bool isLogoModeActive, bool playLiveFirst, bool keepGuideOpen)
        {
            int videosToAddCount = 15;

            Int32.TryParse(ConfigurationManager.AppSettings["VideosToAddToFirstChannel"], out videosToAddCount);

            string channelPicUrl = String.Empty;
            string channelLogoUrl = String.Empty;
           List<ChannelTubePo> channelTubeList = ChannelManage.GetChannelTubesForUser(userId);
           string hashedChannelPassword = String.Empty;
           if (!String.IsNullOrEmpty(channelPassword))
           {
               hashedChannelPassword=CryptoUtils.ConvertToBase64(channelPassword);
           }
          
            // MST, don't need this. This cookie is set as user confirms there e-mail address
            // on the default page (Default.aspx.cs)
            //
            //if (channelTubeList == null || channelTubeList.Count == 0)
            //{
            //    HttpCookie firstChannelCookie = new HttpCookie("firstChannelCookie");
            //    firstChannelCookie.Expires = DateTime.Now.AddDays(30);
            //    firstChannelCookie.Value = "true";
            //    HttpContext.Current.Response.Cookies.Add(firstChannelCookie);
            //}
            bool insertPublicChannels = channelTubeList == null || channelTubeList.Count == 0;
            ChannelTube channeltube = new ChannelTube();
            //channeltube always null (channel qasnt inserted yet) so images for all channels are saved in same url
          
            if (!String.IsNullOrEmpty(channelImgData))
            {
                channelPicUrl = AWSManage.UploadChannelImageToS3(channelImgData, fileName, userId, RandomUtils.RandomNumber(1,150));
            }
            if(!String.IsNullOrEmpty(customLogoImgData))
            {
                channelLogoUrl = AWSManage.UploadChannelLogoToS3(customLogoImgData, fileName, userId, RandomUtils.RandomNumber(150, 300));
            }

            if (ChannelManage.CreateChannelTube(categoryId, languageId, channelName, channelDescription, channelPicUrl, channelUrl, userId, insertPublicChannels, videosToAddCount, isWhiteLabeled, hashedChannelPassword, embedEnabled, muteOnStartup, customLabel, subscriberDomain, embedOnlyMode, matureContentEnabled, showPlayerControls, isPrivate, isLogoModeActive,  channelLogoUrl, playLiveFirst,keepGuideOpen, out channeltube))
            {
                if (!String.IsNullOrEmpty(channelPassword))
                {
                    ChannelManage.InsertChannnelPassword(channeltube.ChannelTubeId, userId, hashedChannelPassword);
                }
                if (!String.IsNullOrEmpty(subscriberDomain))
                {

                    ChannelManage.InsertSubscriberDomain(userId, channeltube.ChannelTubeId, subscriberDomain);
                    
                }
                if (channeltube!=null)
                {

                }
                //ScheduleManage.SetAutoPilotForChannelTube(channeltube.ChannelTubeId, true);
                return true;
            }
            else
            {
                return false;
            }
           
        }

        [WebMethod]
        public ChannelTubePo UpdateChannelForModal(int channelTubeId, int languageId, string fileName, string imageData, int categoryId, int userId, string channelDescription, bool isWhiteLabeled, bool embedEnable, string channelPassword, string customLabel, bool muteOnStartup, string domainName, bool embedOnlyMode, bool matureContentEnabled, bool showPlayerControls, bool isPrivate, string imageLogoData, bool isLogoModeActive, bool playLiveFirst,bool keepGuideOpen)
        {
              
            ChannelTubePo channelPo = new ChannelTubePo();
            ChannelTube channel = ChannelManage.GetChannelTubeById(channelTubeId);

            bool originalMatureContentEnabled = channel.MatureContentEnabled;

            string channelPicUrl = String.Empty;

            if (isLogoModeActive)
            {
                customLabel = String.Empty;
            }
            else
            {
                imageLogoData = String.Empty;
                channel.CustomLogo = String.Empty;
            }

            if (!String.IsNullOrEmpty(imageData))
            {
                channel.PictureUrl = AWSManage.UploadChannelImageToS3(imageData, fileName, userId, channelTubeId);
            }
            if(!String.IsNullOrEmpty(imageLogoData))
            {
                channel.CustomLogo = AWSManage.UploadChannelLogoToS3(imageLogoData, fileName, userId, channelTubeId);
            }
            else
            {
                isLogoModeActive = false;
            }
            if (!String.IsNullOrEmpty(customLabel))
            {
                channel.CustomLabel = customLabel;
            }
            else
            {
                channel.CustomLabel = string.Empty;
            }
            if (channel.CategoryId != categoryId || categoryId != 0)
            {
                channel.CategoryId = categoryId;
            }
            if (channel.LanguageId != languageId || languageId != 0)
            {
                channel.LanguageId = languageId;
            }
            channel.Description = channelDescription;
            channel.IsWhiteLabeled = isWhiteLabeled;
            channel.EmbedEnabled = embedEnable;
            channel.ChannelPassword = channelPassword;
            channel.IsPrivate = isPrivate;
            var user = UserManage.GetUserPoByUserId(userId);
            channel.ChannelPassword = CryptoUtils.ConvertToBase64(channelPassword);           
            channel.MuteOnStartup = muteOnStartup;
            channel.EmbedOnlyModeEnabled = embedOnlyMode;
            channel.MatureContentEnabled = matureContentEnabled;
            channel.CustomPlayerControlsEnabled=showPlayerControls;
            channel.IsLogoModeActive = isLogoModeActive;
            channel.CustomLabel = channel.CustomLabel;
            channel.CustomLogo = channel.CustomLogo;
            channel.PlayLiveFirst = playLiveFirst;
            channel.KeepGuideOpen = keepGuideOpen;


            if (originalMatureContentEnabled && !matureContentEnabled)
            {
                ScheduleManage.DeleteChannelSchedulesWithMatureContent(channel.UserId);
            }

            ChannelManage.UpdateChannelTube(channel);
            
            if (String.IsNullOrEmpty(domainName))
            {
                ChannelManage.DeleteSubscriberDomain(userId, channelTubeId);
            }
            else
            {
                ChannelManage.UpdateSubscribtionDomain(userId, channelTubeId, domainName);
            }

            channelPo = ChannelManage.GetChannelTubePoById(channelTubeId);
            channelPo.CustomLogo = ImageUtils.GetCustomLogoImageUrl(channelPo.CustomLogo);
            channelPo.PictureUrl = ImageUtils.GetChannelImageUrl(channelPo.PictureUrl);

            return channelPo;
           

        }

        [WebMethod]
        public bool DeleteChannelForModal(int channelTubeId)
        {
            return ChannelManage.DeleteChannelTubeById(channelTubeId);
        }

        [WebMethod]
        public int AddLike(string clientTime, int channelTubeId, int userId)
        {
            return ChannelManage.AddChannelLike(userId, channelTubeId, clientTime);
        }

        [WebMethod]
        public int UnLike(string clientTime, int channelTubeId, int userId)
        {
            return ChannelManage.DeleteChannelLike(userId, channelTubeId, clientTime);
        }

        [WebMethod]
        public int InsertVisitor(int visitorUserId, int channelTubeId, string clientTime, string visitorIp, string destination, string uri)
        {
            return VisitorManage.InsertVisitor(visitorUserId, channelTubeId, clientTime, visitorIp, destination, uri);
        }

        [WebMethod]
        public void UpdateVisitor(int visitorId, int durationCount)
        {
            VisitorManage.UpdateVisitor(visitorId, durationCount);
        }

        [WebMethod]
        public void UpdateVisitDurationByVisitorId(int visitorId, int durationCount)
        {
            VisitorManage.UpdateVisitDurationByVisitorId(visitorId, durationCount);
        }

        [WebMethod]
        public List<ChannelStatistics> GetChannelStatistics(string clientTime)
        {
            var date = DateTimeUtils.GetClientTime(clientTime) ?? DateTime.Now;
            return ChannelManage.GetChannelStatistics(date);
        }

        [WebMethod]
        public List<ChannelStatistics> GetChannelsByCalendar(int option, string clientTime)
        {
            var date = DateTimeUtils.GetClientTime(clientTime) ?? DateTime.Now;
            List<ChannelStatistics> channelStatisticsList = ChannelManage.GetChannelStatistics(date);

            List<ChannelStatistics> listToReturn = new List<ChannelStatistics>();

            switch (option)
            {
                case 2:
                    foreach (var d in channelStatisticsList)
                    {
                        if (d.lastVisit != null)
                        {
                            DateTime dateL = (DateTime)d.LastVisit;
                            if (dateL.Date.ToShortDateString() == date.ToShortDateString())
                            {
                                listToReturn.Add(d);
                            }
                        }
                       
                    }
                    
                    break;
                case 3:
                    foreach (var d in channelStatisticsList)
                    {
                        if (d.lastVisit != null)
                        {
                            DateTime dateL = (DateTime)d.LastVisit;
                            if (dateL.Date.ToShortDateString() == date.AddDays(-1).ToShortDateString())
                            {
                                listToReturn.Add(d);
                            }
    }
}
                  
                    break;
                case 4:
                    int month = date.Month;
                    foreach (var d in channelStatisticsList)
                    {
                        if (d.lastVisit != null)
                        {
                            DateTime dateL = (DateTime)d.LastVisit;
                            if (dateL.Month == month)
                            {
                                listToReturn.Add(d);
                            }
                        }
                    }
                    break;
                case 5:
                    int lastMonth = date.AddMonths(-1).Month;
                    foreach (var d in channelStatisticsList)
                    {
                        if (d.lastVisit != null)
                        {
                            DateTime dateL = (DateTime)d.LastVisit;
                            if (dateL.Month == lastMonth)
                            {
                                listToReturn.Add(d);
                            }
                        }
                    }
                    break;
            }
            return listToReturn;
        }
        [WebMethod]
        public List<EmbeddedChannelPo> GetEmbeddedChannelsByCalendar(int option, string clientTime)
        {
            var date = DateTimeUtils.GetClientTime(clientTime) ?? DateTime.Now;
            List<EmbeddedChannelPo> channelStatisticsList = ChannelManage.GetEmbeddedChannelsByDate(date);

            //List<ChannelStatistics> listToReturn = new List<ChannelStatistics>();

            //switch (option)
            //{
            //    case 2:
            //        foreach (var d in channelStatisticsList)
            //        {
            //            if (d.lastVisit != null)
            //            {
            //                DateTime dateL = (DateTime)d.LastVisit;
            //                if (dateL.Date.ToShortDateString() == date.ToShortDateString())
            //                {
            //                    listToReturn.Add(d);
            //                }
            //            }

            //        }

            //        break;
            //    case 3:
            //        foreach (var d in channelStatisticsList)
            //        {
            //            if (d.lastVisit != null)
            //            {
            //                DateTime dateL = (DateTime)d.LastVisit;
            //                if (dateL.Date.ToShortDateString() == date.AddDays(-1).ToShortDateString())
            //                {
            //                    listToReturn.Add(d);
            //                }
            //            }
            //        }

            //        break;
            //    case 4:
            //        int month = date.Month;
            //        foreach (var d in channelStatisticsList)
            //        {
            //            if (d.lastVisit != null)
            //            {
            //                DateTime dateL = (DateTime)d.LastVisit;
            //                if (dateL.Month == month)
            //                {
            //                    listToReturn.Add(d);
            //                }
            //            }
            //        }
            //        break;
            //    case 5:
            //        int lastMonth = date.AddMonths(-1).Month;
            //        foreach (var d in channelStatisticsList)
            //        {
            //            if (d.lastVisit != null)
            //            {
            //                DateTime dateL = (DateTime)d.LastVisit;
            //                if (dateL.Month == lastMonth)
            //                {
            //                    listToReturn.Add(d);
            //                }
            //            }
            //        }
            //        break;
            //}
            return new List<EmbeddedChannelPo>();
        }

        [WebMethod]
        public List<ChannelStatistics> GetCustomChannelStatistics(string dateFrom, string dateTo, string clientTime)
        {
            DateTime dateFromParse;
            DateTime dateToParse;
            string dateFormat = "dd/MM/yyyy";
            DateTime.TryParseExact(dateFrom, dateFormat, new CultureInfo("en-US"), DateTimeStyles.AssumeLocal, out dateFromParse);
            DateTime.TryParseExact(dateTo, dateFormat, new CultureInfo("en-US"), DateTimeStyles.AssumeLocal, out dateToParse);
            var date = DateTimeUtils.GetClientTime(clientTime) ?? DateTime.Now;
            List<ChannelStatistics> channelsStatisticsList = ChannelManage.GetChannelStatistics(date);
            List<ChannelStatistics> channelsStatisticsListinGap = new List<ChannelStatistics>();
            if (dateToParse.Date < dateFromParse.Date)
            {
                //
            }
            else
            {
                foreach (var d in channelsStatisticsList)
                {
                    if (d.lastVisit != null)
                    {
                        DateTime dateL = (DateTime)d.LastVisit;
                        if ((dateL.Date >= dateFromParse.Date) && (dateL.Date <= dateToParse.Date))
                        {
                            channelsStatisticsListinGap.Add(d);
                        }
                    }
                }
            }
            return channelsStatisticsListinGap;
        }

        //public List<EmbeddedChannelPo> GetCustomEmbeddedChannels(string dateFrom, string dateTo, string clientTime)
        //{
        //    DateTime dateFromParse;
        //    DateTime dateToParse;
        //    string dateFormat = "dd/MM/yyyy";
        //    DateTime.TryParseExact(dateFrom, dateFormat, new CultureInfo("en-US"), DateTimeStyles.AssumeLocal, out dateFromParse);
        //    DateTime.TryParseExact(dateTo, dateFormat, new CultureInfo("en-US"), DateTimeStyles.AssumeLocal, out dateToParse);
        //    var date = DateTimeUtils.GetClientTime(clientTime) ?? DateTime.Now;
        //    List<EmbeddedChannelPo> channelsStatisticsList = ChannelManage.GetChannelStatistics(date);
        //    List<EmbeddedChannelPo> channelsStatisticsListinGap = new List<EmbeddedChannelPo>();
        //    if (dateToParse.Date < dateFromParse.Date)
        //    {
        //        //
        //    }
        //    else
        //    {
        //        foreach (var d in channelsStatisticsList)
        //        {
        //            if (d.lastVisit != null)
        //            {
        //                DateTime dateL = (DateTime)d.LastVisit;
        //                if ((dateL.Date >= dateFromParse.Date) && (dateL.Date <= dateToParse.Date))
        //                {
        //                    channelsStatisticsListinGap.Add(d);
        //                }
        //            }
        //        }
        //    }
        //    return channelsStatisticsListinGap;
        //}

        //testing timeline
        [WebMethod]
        public List<ChannelPreviewModel> GetCurrentlyPlayingChannelsForTimeLine(string clientTime)
        {
            DateTime clientTimeDateTime = DateTimeUtils.GetClientTime(clientTime) ?? DateTime.Now;
            List<ChannelTubeModel> channelTubes = ChannelManage.GetCurrentlyPlayingChannels(clientTimeDateTime);
            List<ChannelPreviewModel> channelPreviewModel = new List<ChannelPreviewModel>();
            foreach (var c in channelTubes)
            {
                var channel = ChannelManage.GetCurrentlyPlayingChannelById(c.ChannelTubeId, 0, clientTimeDateTime);
                if (channel != null)
                {
                    channelPreviewModel.Add(channel);
                }
            }
            return channelPreviewModel;
        }

        //testing timeline
        [WebMethod]
        public TvGuideDataModel GetTvGuideAllChannels(string clientTime, int pageIndex, int pageSize)
        {
            DateTime clientTimeDateTime = DateTimeUtils.GetClientTime(clientTime) ?? DateTime.Now;

            var data = ChannelManage.GetTvGuideDataByClientTime(clientTimeDateTime, pageIndex, pageSize);

            return data;
        }

        [WebMethod]
        public int InsertEmbeddedHostChannelLoad(EmbeddedPageLoadCriteria model)
        {
            DateTime clientTimeDateTime = DateTimeUtils.GetClientTime(model.clientTime) ?? DateTime.Now;

            return ChannelManage.InsertEmbeddedHostChannelLoadWithGet(model.channelTubeId, clientTimeDateTime, model.embeddedHostUrl, model.accountNumber, model.isSingleChannelView, model.IsSubscribedDomain);
        }

        [WebMethod]
        public List<EmbeddedChannelPo> GetEmbeddedChannelsInfo()
        {
            return ChannelManage.GetEmbeddedChannelsInfo();
        }

        [WebMethod]
     public bool UpdateEmbeddedHostChannelLoadById(int embeddedChannelHostLoadId, double visitTime, string loadEndTime)
        {
            DateTime clientTimeLoadDateTime = DateTimeUtils.GetClientTime(loadEndTime) ?? DateTime.Now;

            return ChannelManage.UpdateEmbeddedHostChannelLoadById(embeddedChannelHostLoadId, visitTime, clientTimeLoadDateTime);
        }

        [WebMethod]
        public ChannelTubePo GetChannelTubeByChannelId(int channelId)
        {
            var channel = ChannelManage.GetChannelTubePoById(channelId);

            channel.PictureUrl = ImageUtils.GetChannelImageUrl(channel.PictureUrl);
            channel.CustomLogo = ImageUtils.GetCustomLogoImageUrl(channel.CustomLogo);

            return channel;
        }

        [WebMethod]
        public UserChannelEntitlements GetUserChannelEntitlementsByUserId(int userId)
        {
            Logger.Info(String.Format("Handling request for channel user entitlements for user with id={0}", userId));

            return ChannelManage.GetUserChannelEntitlementsByUserId(userId);
        }

        [WebMethod]
        public bool ValidateChannelPasswordByChannelName(string channelName, string password)
        {
            ChannelTube channel = ChannelManage.GetChannelTubeByUrl(channelName);
          //  string encodedPass = CryptoUtils.ConvertToBase64(password);
            int channelId = 0;
            if (channel != null)
            {
                channelId = channel.ChannelTubeId;
            }
            return ChannelManage.ValidateChannelPassword(channelId, password);
    }

        [WebMethod]
        public ChannelTubePo GetChannelTubePoByChannelUrl(string channelUrl)
        {
             ChannelTube channel = ChannelManage.GetChannelTubeByUrl(channelUrl);
             ChannelTubePo channelTubePo = ChannelManage.GetChannelTubePoById(channel.ChannelTubeId);

            channelTubePo.PictureUrl = ImageUtils.GetChannelImageUrl(channelTubePo.PictureUrl);
            channelTubePo.CustomLogo = ImageUtils.GetCustomLogoImageUrl(channelTubePo.CustomLogo);

             return channelTubePo;
        }

        [WebMethod]
        public VideoTube GetVideoByVideoId(int videoTubeId)
        {
            return VideoTubeManage.GetVideoTubeById(videoTubeId);
        }

        #region Tv Guide Methods

        [WebMethod]
        public TvGuideDataModel GetTvGuideTopChannels(string clientTime, int pageIndex, int pageSize)
        {
            DateTime clientTimeDateTime = DateTimeUtils.GetClientTime(clientTime) ?? DateTime.Now;

            var data = ChannelManage.GetTvGuideTopChannelDataByClientTime(clientTimeDateTime, pageIndex, pageSize);

            return data;
        }

        [WebMethod]
        public TvGuideDataModel GetTvGuideFavoriteChannelsForUser(string clientTime, int userId, int pageIndex, int pageSize)
        {
            DateTime clientTimeDateTime = DateTimeUtils.GetClientTime(clientTime) ?? DateTime.Now;

            var data = ChannelManage.GetTvGuideFavoriteChannelsForUser(clientTimeDateTime, userId, pageIndex, pageSize);

            return data;
        }

        [WebMethod]
        public TvGuideDataModel GetTvGuideUserChannelsById(string clientTime, int userId, int pageIndex, int pageSize)
        {
            DateTime clientTimeDateTime = DateTimeUtils.GetClientTime(clientTime) ?? DateTime.Now;

            var data = ChannelManage.GetTvGuideUserChannelsById(clientTimeDateTime, userId, pageIndex, pageSize);

            return data;
        }

        [WebMethod]
        public TvGuideDataModel GetEmbeddedTvGuideByUserIdAndPageIndex(string clientTime, int userId, string domain, int pageIndex, int pageSize)
        {
            if(pageSize==0 || pageSize==null)
            {
                pageSize = 10;
            }
            DateTime clientTimeDateTime = DateTimeUtils.GetClientTime(clientTime) ?? DateTime.Now;

            var data = ChannelManage.GetEmbeddedTvGuideByUserIdAndPageIndex(clientTimeDateTime, userId, domain, pageIndex, pageSize);

            return data;
        }

        [WebMethod]
        public TvGuideDataModel GetTvGuideUserChannelsByPublicUrl(string clientTime, string publicUrl, int pageIndex, int pageSize)
        {
            DateTime clientTimeDateTime = DateTimeUtils.GetClientTime(clientTime) ?? DateTime.Now;

            var data = ChannelManage.GetTvGuideUserChannelsByPublicUrl(clientTimeDateTime, publicUrl, pageIndex, pageSize);

            return data;
        }

        [WebMethod]
        public TvGuideDataModel GetTvGuideChannelsByLanguage(string clientTime, int languageId, int pageIndex, int pageSize)
        {
            DateTime clientTimeDateTime = DateTimeUtils.GetClientTime(clientTime) ?? DateTime.Now;

            var data = languageId == 0
                    ? ChannelManage.GetTvGuideDataByClientTime(clientTimeDateTime, pageIndex, pageSize)
                    : ChannelManage.GetTvGuideChannelsByLanguage(clientTimeDateTime, languageId, pageIndex, pageSize);

            return data;
        }

        [WebMethod]
        public TvGuideDataModel GetTvGuideChannelsByCategory(string clientTime, int categoryId, int pageIndex, int pageSize )
        {
            DateTime clientTimeDateTime = DateTimeUtils.GetClientTime(clientTime) ?? DateTime.Now;

            var data = categoryId == 0
                    ? ChannelManage.GetTvGuideDataByClientTime(clientTimeDateTime, pageIndex, pageSize)
                    : ChannelManage.GetTvGuideChannelsByCategory(clientTimeDateTime, categoryId, pageIndex, pageSize);

            return data;
        }

        [WebMethod]
        public TvGuideDataModel GetTvGuideChannelsByKeywords(string clientTime, string keywords, int pageIndex, int pageSize)
        {
            DateTime clientTimeDateTime = DateTimeUtils.GetClientTime(clientTime) ?? DateTime.Now;

            var data = ChannelManage.GetTvGuideChannelsByKeywords(clientTimeDateTime, keywords, pageIndex, pageSize);

            return data;
        }

        [WebMethod]
        public ChannelTubePageModel GetCurrentlyPlayingChannelPage(string clientTime, string sortBy, int? languageId, int pageIndex)
        {
            DateTime clientTimeDateTime = DateTimeUtils.GetClientTime(clientTime) ?? DateTime.Now;

            if (sortBy == "null")
            {
                sortBy = null;
            }

            var data = ChannelManage.GetCurrentlyPlayingChannelPage(clientTimeDateTime, sortBy, languageId, pageIndex, 50);

            if (data == null || data.ChannelTubeModels == null || (data.ChannelTubeModels.Count == 0 && pageIndex == 1))
            {
                data.IsSuccess = false;
                data.Message = "Sorry, there are no channels playing at this moment. Please check back again soon.";
            }

            return data;
        }

        [WebMethod]
        public List<VideoProvider> GetAllVideoProvideres()
        {
            Logger.Debug("Handling request for a collection of all system supported video providers");

            return VideoProviderManage.GetAllVideoProviders();
        }

        #endregion      

    }
}



