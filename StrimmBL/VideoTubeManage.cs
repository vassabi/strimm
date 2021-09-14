using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Strimm.Model;
using Strimm.Data;
using Strimm.Shared;
using log4net;
using Strimm.Data.Repositories;
using Strimm.Model.Projections;
using Strimm.Model.WebModel;
using System.Configuration;
using Strimm.Model.Criteria;

namespace StrimmBL
{
    public static class VideoTubeManage
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(VideoTubeManage));

        private static readonly string S3_DOMAIN;
        private static string AWS_CLOUD_FRONT_WEB_DIST_DOMAIN;

        static VideoTubeManage()
        {
            try
            {
                S3_DOMAIN = ConfigurationManager.AppSettings["AmazonCloudFrontDomain"];
                AWS_CLOUD_FRONT_WEB_DIST_DOMAIN = ConfigurationManager.AppSettings["AmazonWebDistribDomain"];
            }
            catch (Exception ex)
            {
                Logger.Error("Error occured while reading configuration", ex);
            }
        }

        public static void RemoveRestrictedAndDeletedVideosAsOfDate(DateTime asOfDate)
        {
            Logger.Debug(String.Format("Removing deleted and restricted videos for all users as of: {0}", asOfDate.ToString("MM/dd/yyyy")));

            using (var repository = new VideoTubeRepository())
            {
                repository.RemoveAllRestrictedAndDeletedVideosAsOfDate(asOfDate);
            }
        }

        public static bool IsVideoTubePartOfVideoRoomTube(int videoRoomTubeId, string providerVideoId)
        {
            Logger.Info(String.Format("Checking if videoRoomTube with Id={0} contains videoTube '{1}'", videoRoomTubeId, providerVideoId));

            bool isPart = false;

            using (var videoTubeRepository = new VideoTubeRepository())
            {
                var videoTube = videoTubeRepository.GetVideoTubeByProviderVideoIdAndVideoRoomTubeId(providerVideoId, videoRoomTubeId);
                isPart = videoTube != null && videoTube.VideoTubeId > 0;
            }

            return isPart;
        }

        public static VideoTube GetVideoTubeById(int videoTubeId)
        {
            Logger.Info(String.Format("Retrieving video tube by id={0}", videoTubeId));

            VideoTube video = null;

            using (var videoTubeRepository = new VideoTubeRepository())
            {
                video = videoTubeRepository.GetVideoTubeById(videoTubeId);
            }

            return video;
        }

        public static VideoTubePo GetVideoTubePoById(int videoTubeId)
        {
            Logger.Info(String.Format("Retrieving video tube by id={0}", videoTubeId));

            VideoTubePo video = null;

            using (var videoTubeRepository = new VideoTubeRepository())
            {
                video = videoTubeRepository.GetVideoTubePoById(videoTubeId);
            }

            return video;
        }

        public static bool UpdateVideoTube(VideoTube videoTube)
        {
            if (videoTube == null)
            {
                Logger.Warn("Failed to update video Tube. Video Tube was not specified");
                return false;
            }

            if (videoTube.VideoTubeId == 0)
            {
                Logger.Warn("Failed to update video Tube. Video Tube record does not exist");
                return false;
            }

            Logger.Info(String.Format("Updating video tube with Id={0}", videoTube.VideoTubeId));

            bool isSuccess = false;

            using (var videoTubeRepository = new VideoTubeRepository())
            {
                isSuccess = videoTubeRepository.UpdateVideoTube(videoTube);
            }

            return isSuccess;
        }

        public static bool AddVideoTubeToUserArchive(int userId, int videoTubeId, DateTime clientTimeDateTime)
        {
            Logger.Info(String.Format("Archiving video with Id={0} for user with Id={1} as of '{2}'", videoTubeId, userId, clientTimeDateTime.ToString()));

            bool isSuccess = false;

            using (var videoTubeRepository = new VideoTubeRepository())
            {
                isSuccess = videoTubeRepository.AddVideoTubeToUserArchive(userId, videoTubeId, clientTimeDateTime);
            }

            return isSuccess;
        }

        public static bool RemoveVideoTubeFromUserArchive(int userId, int videoTubeId)
        {
            Logger.Info(String.Format("Removing video Tube with Id={0} from archive for user with Id={1}", videoTubeId, userId));

            bool isSuccess = false;

            using (var videoTubeRepository = new VideoTubeRepository())
            {
                isSuccess = videoTubeRepository.DeleteVideoTubeFromUserArchiveByVideoTubeIdAndUserId(userId, videoTubeId);
            }

            return isSuccess;
        }

        public static bool RemoveVideoTubeFromVideoRoomTube(int videoTubeId, int userId)
        {
            Logger.Info(String.Format("Removing videoTube with Id={0} for user with Id={1} from video room", videoTubeId, userId));

            bool isSuccess = false;

            try
            {
                var adminUserName = ConfigurationManager.AppSettings["PublicLibAdminUser"].ToString();

                using (var userRepository = new UserRepository())
                using (var videoTubeRepository = new VideoTubeRepository())
                {
                    var user = userRepository.GetUserByUserName(adminUserName);
                    bool isAdmin = (user != null && user.UserId == userId);

                    if (isAdmin)
                    {
                        Logger.Debug(String.Format("Handling admin user request. Video with id='{0}' will be removed from public libary", videoTubeId));
                    }

                    isSuccess = videoTubeRepository.DeleteVideoTubeFromVideoRoomByVideoTubeIdAndUserId(videoTubeId, userId, isAdmin);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to remove videoTube with Id={0} for user with Id={1} from video room", videoTubeId, userId), ex);
            }

            return isSuccess;
        }

        public static bool IsVideoTubePartOfVideoRoomTube(int videoRoomTubeId, int videoTubeId)
        {
            Logger.Info(String.Format("Checking if video Tube with Id={0} is part of VideoRoomTube with Id={1}", videoTubeId, videoRoomTubeId));

            bool isPart = false;

            using (var videoTubeRepository = new VideoTubeRepository())
            {
                var videoTube = videoTubeRepository.GetVideoTubeByIdAndVideoRoomTubeId(videoTubeId, videoRoomTubeId);
                isPart = videoTube != null && videoTube.VideoTubeId > 0;
            }

            return isPart;
        }

        public static bool IsVideoTubePartOfUserArchive(int userId, int videoTubeId)
        {
            Logger.Info(String.Format("Checking if video Tube with Id={0} was archived for user with Id={1}", videoTubeId, userId));

            bool isPart = false;

            using (var videoTubeRepository = new VideoTubeRepository())
            {
                var videoTube = videoTubeRepository.GetVideoTubeFromArchiveByUserIdAndVideoTubeId(userId, videoTubeId);
                isPart = videoTube != null && videoTube.VideoTubeId > 0;
            }

            return isPart;
        }

        public static List<VideoTubeModel> GetArchivedVideosByUserId(int userId)
        {
            Logger.Debug(String.Format("Retrieving archived videos for user with Id={0}", userId));
            var archivedList = new List<VideoTubeModel>();
            
            using (var videoTubeRepository = new VideoTubeRepository())
            {
                var videos = videoTubeRepository.GetArchivedUserVideoTubesByUserId(userId);
                videos.ForEach(x => archivedList.Add(new VideoTubeModel(AWS_CLOUD_FRONT_WEB_DIST_DOMAIN, x)));
            }

            return archivedList;
        }

        public static bool AddVideoTubeToVideoRoomTube(int videoRoomTubeId, int videoTubeId)
        {
            Logger.Info(String.Format("Adding videoTube with Id={0} to videoRoomTube with Id={1}", videoTubeId, videoRoomTubeId));

            bool isSuccess = false;

            using (var videoTubeRepository = new VideoTubeRepository())
            {
                isSuccess = videoTubeRepository.AddVideoTubeToVideoRoomTube(videoRoomTubeId, videoTubeId);
            }

            return isSuccess;
        }

        public static List<VideoTubeCounterEntity> CountVideoTubesInVideoRoomTubeByCategoryAndUserId(int userId)
        {
            Logger.Info(String.Format("Retrieving all counters for all videos by category in videoRoomTube with User Id={0}", userId));

            var videoTubeCounters = new List<VideoTubeCounterEntity>();

            using (var videoRoomTubeRepository = new VideoRoomTubeRepository())
            using (var videoTubeRepository = new VideoTubeRepository())
            {
                var videoRoomTube = videoRoomTubeRepository.GetVideoRoomTubeByUserId(userId);
                if (videoRoomTube != null)
                {
                    videoTubeCounters = videoTubeRepository.GetVideoTubeCountsByCategoryAndVideoRoomTubeId(videoRoomTube.VideoRoomTubeId);
                }
            }

            return videoTubeCounters;
        }

        public static List<VideoTubeCounterEntity> CountVideoTubesInChannelTubeByCategoryByChannelTubeId(int channelId)
        {
            Logger.Info(String.Format("Retrieving all counters for all videos by category in channel with Channel Id={0}", channelId));

            var videoTubeCounters = new List<VideoTubeCounterEntity>();

            using (var videoTubeRepository = new VideoTubeRepository())
            {
                videoTubeCounters = videoTubeRepository.GetVideoTubeCountsByCategoryAndChannelTubeId(channelId);
            }

            return videoTubeCounters;
        }

        public static List<VideoTubeCounterEntity> CountVideoTubesInPublicLibraryByCategory()
        {
            Logger.Info("Retrieving counters for all public videos by category");

            var videoTubeCounters = new List<VideoTubeCounterEntity>();

            using (var videoTubeRepository = new VideoTubeRepository())
            {
                videoTubeCounters = videoTubeRepository.GetVideoTubeCountsInPublicLibraryByCategory();
            }

            return videoTubeCounters;
        }

        public static List<VideoTubePo> GetAllVideoTubesForVideoRoom(int videoRoomTubeId)
        {
            Logger.Info(String.Format("Retrieving all videos in video room with id={0}", videoRoomTubeId));

            var videos = new List<VideoTubePo>();

            using (var videoTubeRepository = new VideoTubeRepository())
            {
                videos = videoTubeRepository.GetVideoTubesByVideoRoomTubeId(videoRoomTubeId);
            }

            return videos;
        }

        public static List<VideoTubePo> GetVideoTubePosFromVideoRoomTubeByUserId(int userId)
        {
            if (userId == 0)
            {
                Logger.Warn("Unable to retrieve videos from video room. Invalid user id specified");
                return null;
            }

            Logger.Info(String.Format("Retrieving videos from video room for user with Id={0}", userId));

            var videos = new List<VideoTubePo>();

            using (var videoTubeRepository = new VideoTubeRepository())
            {
                videos = videoTubeRepository.GetVideoTubePosFromVideoRoomTubeByUserId(userId);
            }

            return videos;
        }

        public static VideoTubePageModel GetVideoTubesFromVideoRoomByUserAndByPageIndex(VideoRoomVideoSearchCriteria searchCriteria, int pageSize = 10)
        {
            if (searchCriteria == null)
        {
                Logger.Warn("Failed to retrieve videos from video room. Search criteria was not specified");
                return null;
            }

            if (searchCriteria.UserId == 0)
            {
                Logger.Warn("Failed to retrieve videos from video room. Search criteria was not specified");
                return null;
            }

            if (searchCriteria.UserId == 0)
            {
                Logger.Warn("Unable to retrieve videos from video room. Invalid user id specified");
                return null;
            }

            int configPageSize = 0;

            if (Int32.TryParse(ConfigurationManager.AppSettings["AddVideoPageSize"].ToString(), out configPageSize))
            {
                pageSize = configPageSize;
            }

            string originDomain = ConfigurationManager.AppSettings["AmazonWebDistribDomain"];

            Logger.Info(String.Format("Retrieving page {0} of videos from video room for userId={1}; page size is {2}", searchCriteria.PageIndex, searchCriteria.UserId, pageSize));

            var videoPage = new VideoTubePageModel();
            int pageCount = 0;

            using (var videoTubeRepository = new VideoTubeRepository())
            {
                var videosInChannel = searchCriteria.ChannelTubeId > 0
                                        ? videoTubeRepository.GetAllVideoTubeByChannelTubeId(searchCriteria.ChannelTubeId)
                                        : videoTubeRepository.GetAllVideoTubeInUserChannelsByUserId(searchCriteria.UserId);

                var videos = videoTubeRepository.GetVideoTubePosFromVideoRoomTubeByUserIdAndPageIndex(searchCriteria.UserId, searchCriteria.PageIndex, searchCriteria.RetrieveMyVideos, searchCriteria.RetrieveLicensedVideos, searchCriteria.RetrieveExternalVideos, searchCriteria.Keywords, out pageCount, pageSize);

                videoPage.PageIndex = searchCriteria.PageIndex;
                videoPage.PageSize = pageSize;
                videoPage.PrevPageIndex = searchCriteria.PageIndex <= 1 ? 1 : searchCriteria.PageIndex - 1;
                videoPage.NextPageIndex = searchCriteria.PageIndex >= pageCount ? pageCount : searchCriteria.PageIndex + 1;
                videoPage.PageCount = pageCount;
                videoPage.VideoTubeModels = new List<VideoTubeModel>();

                var videoModels = new List<VideoTubeModel>();
                var vimeoModels = new List<VideoTubeModel>();

                videos.ForEach(v =>
                {
                    videoPage.VideoTubeModels.Add(new VideoTubeModel(originDomain, v)
                    {
                        IsInChannel = videosInChannel.Any(x => x.VideoTubeId == v.VideoTubeId)
                    });
                });

                if (videoPage.VideoTubeModels != null && videoPage.VideoTubeModels.Count > 0)
                {
                    videoPage.VideoTubeModels.ForEach(video =>
                    {
                        if (video.VideoProviderId == 2)
                        {
                            vimeoModels.Add(video);
                        }
                        else if (video.VideoProviderId == 1)
                        {
                            videoModels.Add(video);
                        }
                    });
                }

                //MST: 03152017 - Turning off update all status updates because Vimeo started to throttle us. Th
                //these updates will be done with JAS by throttling requests to both providers
                //YouTubeServiceManage.UpdateVideoStatus(videoModels);
                //VimeoServiceManage.UpdateVideoStatus(vimeoModels);
            }

            return videoPage;
        }

        public static List<VideoTubePo> GetVideoTubesForChannel(int channelTubeId)
        {
            Logger.Info(String.Format("Retrieving video tubes for channel with Id={0}", channelTubeId));

            List<VideoTubePo> channelVideos = new List<VideoTubePo>();

            using (var videoTubeRepository = new VideoTubeRepository())
            {
                channelVideos = videoTubeRepository.GetAllVideoTubeByChannelTubeId(channelTubeId);
            }

            return channelVideos;
        }

        public static VideoTubePageModel GetVideoTubesForVideoStoreByCategoryIdAndByPageIndex(VideoStoreVideoSearchCriteria searchCriteria, int pageSize = 10)
        {
            if (searchCriteria == null)
            {
                Logger.Warn("Unable to retrieve videos for video room. Search criteria was not specified");
                return null;
            }

            int configPageSize = 0;

            if (Int32.TryParse(ConfigurationManager.AppSettings["AddVideoPageSize"].ToString(), out configPageSize))
            {
                pageSize = configPageSize;
            }

            Logger.Info(String.Format("Retrieving page {0} of public video tubes in video store for categoryId={0}; page index={1}, page size is {2}", searchCriteria.CategoryId, searchCriteria.PageIndex, pageSize));

            var videoPage = new VideoTubePageModel();
            int pageCount = 0;

            using (var videoTubeRepository = new VideoTubeRepository())
        {
                var videos = searchCriteria.CategoryId > 0
                                ? videoTubeRepository.GetUserUploadedVideoTubesByCategoryIdAndPageIndex(searchCriteria, out pageCount, pageSize)
                                : videoTubeRepository.GetUserUploadedVideoTubesByPageIndex(searchCriteria, out pageCount, pageSize);

                videoPage.PageIndex = searchCriteria.PageIndex;
                videoPage.PageSize = pageSize;
                videoPage.PrevPageIndex = searchCriteria.PageIndex <= 1 ? 1 : searchCriteria.PageIndex - 1;
                videoPage.NextPageIndex = searchCriteria.PageIndex >= pageCount ? pageCount : searchCriteria.PageIndex + 1;
                videoPage.PageCount = pageCount;
                videoPage.VideoTubeModels = new List<VideoTubeModel>();

                videos.ForEach(v => videoPage.VideoTubeModels.Add(new VideoTubeModel(AWS_CLOUD_FRONT_WEB_DIST_DOMAIN, v)));
            }

            return videoPage;

        }

        public static VideoTubePageModel GetVideoTubesFromVideoRoomByUserAndCategoryIdAndByPageIndex(VideoRoomVideoSearchCriteria searchCriteria, int pageSize = 10)
            {
            if (searchCriteria == null)
            {
                Logger.Warn("Unable to retrieve videos from video room. Search criteria was not specified");
                return null;
            }

            if (searchCriteria.UserId == 0)
            {
                Logger.Warn("Unable to retrieve videos from video room. Invalid user id specified");
                return null;
            }

            int configPageSize = 0;

            if (Int32.TryParse(ConfigurationManager.AppSettings["AddVideoPageSize"].ToString(), out configPageSize))
            {
                pageSize = configPageSize;
            }

            string originDomain = ConfigurationManager.AppSettings["AmazonWebDistribDomain"];

            Logger.Info(String.Format("Retrieving page {0} of public video tubes from video room for userId={1} and categoryId={2}; page size is {3}", searchCriteria.PageIndex, searchCriteria.UserId, searchCriteria.CategoryId, pageSize));

            var videoPage = new VideoTubePageModel();
            int pageCount = 0;

            using (var videoTubeRepository = new VideoTubeRepository())
            {
                var videosInChannel = searchCriteria.ChannelTubeId > 0
                                        ? videoTubeRepository.GetAllVideoTubeByChannelTubeId(searchCriteria.ChannelTubeId)
                                        : videoTubeRepository.GetAllVideoTubeInUserChannelsByUserId(searchCriteria.UserId);

                var videos = videoTubeRepository.GetVideoTubePosFromVideoRoomTubeByUserIdAndCategoryIdAndPageIndex(searchCriteria.UserId, searchCriteria.CategoryId, searchCriteria.PageIndex, searchCriteria.RetrieveMyVideos, searchCriteria.RetrieveLicensedVideos, searchCriteria.RetrieveExternalVideos, searchCriteria.Keywords, out pageCount, pageSize);

                videoPage.PageIndex = searchCriteria.PageIndex;
                videoPage.PageSize = pageSize;
                videoPage.PrevPageIndex = searchCriteria.PageIndex <= 1 ? 1 : searchCriteria.PageIndex - 1;
                videoPage.NextPageIndex = searchCriteria.PageIndex >= pageCount ? pageCount : searchCriteria.PageIndex + 1;
                videoPage.PageCount = pageCount;
                videoPage.VideoTubeModels = new List<VideoTubeModel>();

                videos.ForEach(v =>
                {
                    videoPage.VideoTubeModels.Add(new VideoTubeModel(originDomain, v)
                    {
                        IsInChannel = videosInChannel.Any(x => x.VideoTubeId == v.VideoTubeId)
                    });
                });

                var videoModels = new List<VideoTubeModel>();
                var vimeoModels = new List<VideoTubeModel>();

                if (videoPage.VideoTubeModels != null && videoPage.VideoTubeModels.Count > 0)
                {
                    videoPage.VideoTubeModels.ForEach(video =>
                    {
                        if (video.VideoProviderId == 2)
                        {
                            vimeoModels.Add(video);
                        }
                        else if (video.VideoProviderId == 1)
                        {
                            videoModels.Add(video);
                        }
                    });
                }

                //MST: 03152017 - Turning off update all status updates because Vimeo started to throttle us. Th
                //these updates will be done with JAS by throttling requests to both providers
                //YouTubeServiceManage.UpdateVideoStatus(videoModels);
                //VimeoServiceManage.UpdateVideoStatus(vimeoModels);
            }

            return videoPage;
        }

        public static void AddRandomListOfVideosToVideoRoomTube(List<VideoTube> videos, int userId, int channelId)
        {
            if (videos == null)
            {
                Logger.Warn("Unable to add random videos to video room and schedule them. Video list is empty");
                return;
            }

            Logger.Info(String.Format("Adding a list of {0} random video tubes to video room that belongs to user with Id={1} and to channel with Id={2}", videos.Count(), userId, channelId));

            using (var videoTubeRepository = new VideoTubeRepository())
            {
                var videoRoomTube = VideoRoomTubeManage.GetVideoRoomTubeByUserId(userId);

                if (videoRoomTube != null && videoRoomTube.VideoRoomTubeId > 0)
                {
                    var existingVideoTubesInVideoRoomTube = VideoTubeManage.GetAllVideoTubesForVideoRoom(videoRoomTube.VideoRoomTubeId);
                    videos.ForEach(v =>
                    {
                        if (!existingVideoTubesInVideoRoomTube.Any(x => x.VideoTubeId == v.VideoTubeId))
                        {
                            videoTubeRepository.AddVideoTubeToVideoRoomTube(videoRoomTube.VideoRoomTubeId, v.VideoTubeId);

                            // TODO-Need to schedule the video by adding it to a current channel schedule 
                        }
                    });
                }
            }
        }

        public static VideoTubeModel AddVideoTubeToChannelTubeById(int channelTubeId, int videoTubeId)
        {
            if (channelTubeId <= 0 || videoTubeId <= 0)
            {
                throw new ArgumentException("Invalid channel tube id or video tube id specified");
            }

            Logger.Info(String.Format("Adding video tube with Id={0} to channel tube with Id={1}", videoTubeId, channelTubeId));

            VideoTubeModel videoTubeModel;

            using (var videoTubeRepository = new VideoTubeRepository())
            {
                if (videoTubeRepository.AddVideoTubeToChannelTube(channelTubeId, videoTubeId))
                {
                    var videoPo = videoTubeRepository.GetVideoTubePoById(videoTubeId);

                    videoTubeModel = new VideoTubeModel(AWS_CLOUD_FRONT_WEB_DIST_DOMAIN, videoPo);
                }
                else
                {
                    videoTubeModel = new VideoTubeModel()
                    {
                        Message = "Error occurred while adding video to channel. Try again later"
                    };
                }
            }

            return videoTubeModel;
        }

        public static bool RemoveVideoTubeFromChannelTubeById(int channelTubeId, int videoTubeId)
        {
            if (channelTubeId <= 0 || videoTubeId <= 0)
            {
                throw new ArgumentException("Invalid channel tube id or video tube id specified");
            }

            Logger.Info(String.Format("Removing video tube with Id={0} from channel tube with Id={1}", videoTubeId, channelTubeId));

            bool isSuccess = false;

            using (var videoTubeRepository = new VideoTubeRepository())
            {
                isSuccess = videoTubeRepository.DeleteVideoTubeFromChannelTubeById(channelTubeId, videoTubeId);
            }

            return isSuccess;
        }

        public static bool ClearAllVideosFromChannel(int channelId)
        {
            Logger.Info(String.Format("Removing all videos from channel with id={0}", channelId));

            bool isSuccess = false;

            using (var videoTubeRepository = new VideoTubeRepository())
            {
                isSuccess = videoTubeRepository.DeleteAllVideoTubesFromChannelTubeByChannelTubeId(channelId);
            }

            return isSuccess;
        }

        public static bool ClearRestrictedOrRemovedVideosFromChannel(int channelTubeId, List<int> videoIds)
        {
            Logger.Info(String.Format("Removing {0} restricted and removed videos from channel with id={0}", videoIds != null ? videoIds.Count() : 0, channelTubeId));

            bool isSuccess = false;

            using (var videoTubeRepository = new VideoTubeRepository())
            {
                isSuccess = videoTubeRepository.DeleteRestrictedOrRemovedVideosFromChannelTubeByChannelTubeId(channelTubeId, videoIds);
            }

            return isSuccess;
        }

        public static bool RemoveAllVideosFromChannel(int channelTubeId)
        {
            Logger.Info(String.Format("Removing all videos from channel with id={0}", channelTubeId));

            bool isSuccess = false;

            using (var videoTubeRepository = new VideoTubeRepository())
            {
                isSuccess = videoTubeRepository.RemoveAllVideosFromChannelTubeByChannelTubeId(channelTubeId);
            }

            return isSuccess;
        }

        public static List<VideoSchedule> AddMultipleVideosToChannelSchedule(List<VideoTubeModel> videosAvailableToBeScheduled, int channelScheduleId)
        {
            Logger.Info(String.Format("Adding {0} videos to channel schedule with Id={1}", videosAvailableToBeScheduled.Count, channelScheduleId));

            var videoSchedules = new List<VideoSchedule>();
            var idsOfVideosToAdd = videosAvailableToBeScheduled.Select(x => x.VideoTubeId).ToList();

            using (var videoTubeRepository = new VideoTubeRepository())
            {
                videoSchedules = videoTubeRepository.AddMultipleVideoTubesToChannelScheduleById(channelScheduleId, idsOfVideosToAdd);
            }

            return videoSchedules;
        }

        public static void MoveUserPrivateVideosToPublicLibrary(string userName)
        {
            Logger.Info(String.Format("Moving private videos for user '{0}' to Public Library", userName));

            using (var videoTubeRepository = new VideoTubeRepository())
            {
                int count = videoTubeRepository.MoveUserPrivateVideosToPublicLibraryByUsername(userName);
                Logger.Debug(String.Format("Add {0} videos to Public Library", userName));
            }
        }

        public static void RemoveRestrictedAndDeletedVideosFromPublicLibrary(string publicLibOwnerUsername)
        {
            Logger.Info("Removing restricted/deleted videos by provider from Public Library");

            using (var videoTubeRepository = new VideoTubeRepository())
            {
                var publicVideos = videoTubeRepository.GetAllPublicVideoTubes();
                var videoModels = new List<VideoTubeModel>();
                var vimeoModels = new List<VideoTubeModel>();

                if (publicVideos != null && publicVideos.Count > 0)
                {
                    publicVideos.ForEach(video => 
                    {
                        if (video.VideoProviderId == 2)
                        {
                            vimeoModels.Add(new VideoTubeModel(video));
                        }
                        else if (video.VideoProviderId == 1)
                        {
                            videoModels.Add(new VideoTubeModel(video));
                        }
                    });

                    //MST, Status should have been already updated with UpdateVideoStatus job
                    //YouTubeServiceManage.UpdateVideoStatus(videoModels);
                    //VimeoServiceManage.UpdateVideoStatus(vimeoModels);

                    var restrictedOrRemovedVideos = videoModels.Where(video => video.IsRemovedByProvider || video.IsRestrictedByProvider).Select(video => video.VideoTubeId).ToList();
                    Logger.Debug(String.Format("There {0} YouTube restricted or removed videos in Public Library", restrictedOrRemovedVideos.Count));

                    RemoveMultipleVideosFromPublicLibrary(restrictedOrRemovedVideos, videoTubeRepository, publicLibOwnerUsername, "YouTube");

                    restrictedOrRemovedVideos = vimeoModels.Where(video => video.IsRemovedByProvider || video.IsRestrictedByProvider).Select(video => video.VideoTubeId).ToList();
                    Logger.Debug(String.Format("There {0} Vimeo restricted or removed videos in Public Library", restrictedOrRemovedVideos.Count));

                    RemoveMultipleVideosFromPublicLibrary(restrictedOrRemovedVideos, videoTubeRepository, publicLibOwnerUsername, "Vimeo");
                }
                else
                {
                    Logger.Warn("Public Library does not contain any videos!");
                }
            }
        }

        private static void RemoveMultipleVideosFromPublicLibrary(List<int> videoIds, VideoTubeRepository repository, string publicLibOwnerUsername, string providerName)
        {
            if (videoIds != null && videoIds.Count > 0)
            {
                Logger.Debug(String.Format("There {0} {1} restricted or removed videos in Public Library", videoIds.Count, providerName));

                int count = repository.DeleteMultipleVideosFromPublicLibrary(videoIds);
                Logger.Debug(String.Format("Removed {0} {1} restricted or removed videos by provider from Public Library", count, providerName));

                //int vcount = repository.DeleteMultipleVideosFromVideoTubeRoomByUserName(videoIds, publicLibOwnerUsername);
                //Logger.Debug(String.Format("Removed {0} {2} restricted or removed videos by provider from Video Room of user='{1}", vcount, publicLibOwnerUsername, providerName));
            }
            else
            {
                Logger.Debug(String.Format("There are no {0} restricted or removed videos in Public Library.", providerName));
            }
        }

        public static bool AddVideoViewForUser(int? userId, int videoTubeId, DateTime clientTimeDateTime, bool isStart)
        {
            Logger.Info(String.Format("Adding a video view {0} for user. UserId={1}, channel tube Id={2}, viewed on '{3}'", (isStart ? "start" : "end"), userId, videoTubeId, clientTimeDateTime));

            bool isSuccess = false;

            using (var videoTubeRepository = new VideoTubeRepository())
            {
                isSuccess = isStart ? videoTubeRepository.InsertUserVideoTubeViewByUserIdAndVideoTubeId(videoTubeId, userId, clientTimeDateTime, null)
                                    : videoTubeRepository.InsertUserVideoTubeViewByUserIdAndVideoTubeId(videoTubeId, userId, null, clientTimeDateTime);
            }

            return isSuccess;
        }


        public static VideoTubeModel AddVideoTubeToChannelTubeForCategoryWithCustomDuration(string providerVideoId, int channelTubeId, int categoryId, long customDuration)
        {
            Logger.Info(String.Format("Adding external video with ProviderId='{0}' to channel with Id={1} under category with Id={2}", providerVideoId, channelTubeId, categoryId));

            var channel = ChannelManage.GetChannelTubeById(channelTubeId);
            var user = channel != null ? UserManage.GetUserByChannelName(channel.Name) : null;

            bool allowMatureContent = user != null ? user.MatureContentAllowed : false;
            bool PrivateVideoModeEnabled = user != null ? user.PrivateVideoModeEnabled : false;

            var externalVideo = YouTubeServiceManage.GetVideoByUrl(providerVideoId, allowMatureContent);

            long externalVideoDuration = 0;
            if(externalVideo.Duration==0)
            {
                externalVideoDuration = customDuration;
            }
            else
            {
                externalVideoDuration = externalVideo.Duration;
            }
            VideoTubeModel videoTubeModel = new VideoTubeModel();

            if (externalVideo != null)
            {
                using (var videoTubeRepository = new VideoTubeRepository())
                {
                    var videoTube = videoTubeRepository.InsertVideoTubeWithGet(externalVideo.Title, externalVideo.Description, externalVideo.ProviderVideoId, externalVideoDuration, categoryId, externalVideo.VideoProviderId, externalVideo.IsRRated, false, false, externalVideo.Thumbnail, PrivateVideoModeEnabled);
                    
                    if (videoTube != null && videoTube.VideoTubeId > 0)
                    {
                        videoTubeRepository.AddVideoTubeToChannelTube(channelTubeId, videoTube.VideoTubeId);
                        Logger.Debug(String.Format("Successfully added external video using ProviderId={0} to channel with Id={1} for category with Id={2}", providerVideoId, channelTubeId, categoryId));

                        var videoPo = videoTubeRepository.GetVideoTubePoById(videoTube.VideoTubeId);

                        videoTubeModel = new VideoTubeModel(AWS_CLOUD_FRONT_WEB_DIST_DOMAIN, videoPo);
                    }
                    else
                    {
                        videoTubeModel.Message = String.Format("Error occurred while adding video '{0}' to channel. Try again later", externalVideo.Title);
                        Logger.Warn(String.Format("Failed to add external video using ProviderId={0}. Unable to add video to channel with Id={1}", providerVideoId, channelTubeId));
                    }
                }
            }
            else
            {
                Logger.Warn(String.Format("Failed to retrieve external video using ProviderId={0}. Unable to add video to channel with Id={1}", providerVideoId, channelTubeId));
            }

            return videoTubeModel;
        }
        public static VideoTubeModel AddVideoTubeToChannelTubeForCategory(string providerVideoId, int channelTubeId, int categoryId)
        {
            Logger.Info(String.Format("Adding external video with ProviderId='{0}' to channel with Id={1} under category with Id={2}", providerVideoId, channelTubeId, categoryId));

            var channel = ChannelManage.GetChannelTubeById(channelTubeId);
            var user = channel != null ? UserManage.GetUserByChannelName(channel.Name) : null;

            bool allowMatureContent = user != null ? user.MatureContentAllowed : false;
            bool PrivateVideoModeEnabled = user != null ? user.PrivateVideoModeEnabled : false;

            var externalVideo = YouTubeServiceManage.GetVideoByUrl(providerVideoId, allowMatureContent);

            VideoTubeModel videoTubeModel = new VideoTubeModel();

            if (externalVideo != null)
            {
                using (var videoTubeRepository = new VideoTubeRepository())
                {
                    var videoTube = videoTubeRepository.InsertVideoTubeWithGet(externalVideo.Title, externalVideo.Description, externalVideo.ProviderVideoId, externalVideo.Duration, categoryId, externalVideo.VideoProviderId, externalVideo.IsRRated, false, false, externalVideo.Thumbnail, PrivateVideoModeEnabled);

                    if (videoTube != null && videoTube.VideoTubeId > 0)
                    {
                        videoTubeRepository.AddVideoTubeToChannelTube(channelTubeId, videoTube.VideoTubeId);
                        Logger.Debug(String.Format("Successfully added external video using ProviderId={0} to channel with Id={1} for category with Id={2}", providerVideoId, channelTubeId, categoryId));

                        var videoPo = videoTubeRepository.GetVideoTubePoById(videoTube.VideoTubeId);

                        videoTubeModel = new VideoTubeModel(AWS_CLOUD_FRONT_WEB_DIST_DOMAIN, videoPo);
                    }
                    else
                    {
                        videoTubeModel.Message = String.Format("Error occurred while adding video '{0}' to channel. Try again later", externalVideo.Title);
                        Logger.Warn(String.Format("Failed to add external video using ProviderId={0}. Unable to add video to channel with Id={1}", providerVideoId, channelTubeId));
                    }
                }
            }
            else
            {
                Logger.Warn(String.Format("Failed to retrieve external video using ProviderId={0}. Unable to add video to channel with Id={1}", providerVideoId, channelTubeId));
            }

            return videoTubeModel;
        }

        public static bool DeleteArchivedVideoByVideotubeIdAndUserId(int userId, int videoTubeId)
        {
            Logger.Info(String.Format("delete a video  from archive for user. UserId={0}, videotubeId Id={1}", userId, videoTubeId));

            bool isSuccess = false;

            using (var videoTubeRepository = new VideoTubeRepository())
            {
                isSuccess = videoTubeRepository.DeleteArchivedVideoByVideotubeIdAndUserId(userId, videoTubeId);
            }

            return isSuccess;
        }

        public static VideoTubeModel AddVideoTubeVimeoToChannelTubeForCategory(VideoTubeModel videoTubeModel, int channelTubeId, int categoryId)
        {
            
            Logger.Info(String.Format("Adding external video with ProviderId='{0}' to channel with Id={1} under category with Id={2}", videoTubeModel.ProviderVideoId, channelTubeId, categoryId));

            var externalVideo = videoTubeModel;

            if (externalVideo != null)
            {
                var channel = ChannelManage.GetChannelTubeById(channelTubeId);
                var user = channel != null ? UserManage.GetUserByChannelName(channel.Name) : null;

                bool PrivateVideoModeEnabled = user != null ? user.PrivateVideoModeEnabled : false;

                using (var videoTubeRepository = new VideoTubeRepository())
                {
                    var videoTube = videoTubeRepository.InsertVideoTubeWithGet(externalVideo.Title, externalVideo.Description, externalVideo.ProviderVideoId, externalVideo.Duration, categoryId, externalVideo.VideoProviderId, externalVideo.IsRRated, false, false, externalVideo.Thumbnail, PrivateVideoModeEnabled);

                    if (videoTube != null && videoTube.VideoTubeId > 0)
                    {
                        videoTubeRepository.AddVideoTubeToChannelTube(channelTubeId, videoTube.VideoTubeId);
                        Logger.Debug(String.Format("Successfully added external video using ProviderId={0} to channel with Id={1} for category with Id={2}", videoTubeModel.ProviderVideoId, channelTubeId, categoryId));

                        var videoPo = videoTubeRepository.GetVideoTubePoById(videoTube.VideoTubeId);

                        videoTubeModel = new VideoTubeModel(videoPo);
                    }
                    else
                    {
                        videoTubeModel.Message = String.Format("Error occurred while adding video '{0}' to channel. Try again later", externalVideo.Title);
                        Logger.Warn(String.Format("Failed to add external video using ProviderId={0}. Unable to add video to channel with Id={1}", videoTubeModel.ProviderVideoId, channelTubeId));
                    }
                }
            }
            else
            {
                Logger.Warn(String.Format("Failed to retrieve external video using ProviderId={0}. Unable to add video to channel with Id={1}", videoTubeModel.ProviderVideoId, channelTubeId));
            }

            return videoTubeModel;
        }

        public static bool UpdateVideoTubeLastScheduleDateTimeByScheduleId(int channelScheduleId)
        {
            Logger.Info(String.Format("Updating last schedule date and time for all videos in schedule with id={0}", channelScheduleId));

            if (channelScheduleId <= 0)
            {
                return false;
            }

            bool isSuccess = false;

            using (var videoTubeRepository = new VideoTubeRepository())
            {
                isSuccess = videoTubeRepository.UpdateVideoTubeLastScheduleDateTimeByChannelScheduleId(channelScheduleId);
            }

            return isSuccess;
        }

        public static List<VideoTubeModel> AddListOfYoutubeVideosToChannelForCategory(List<string> providerVideoIds, int channelId, int categoryId)
        {
            List<VideoTubeModel> addedVideoTubestoChannel = new List<VideoTubeModel>();
            if (providerVideoIds.Count != 0)
            {
                foreach (var providerVideoId in providerVideoIds)
                {
                    VideoTubeModel videoModel = AddVideoTubeToChannelTubeForCategory(String.Format("https://www.youtube.com/watch?v={0}", providerVideoId), channelId, categoryId);
                    if (videoModel != null && (!videoModel.IsRestrictedByProvider) && (!videoModel.IsRemovedByProvider))
                    {
                        addedVideoTubestoChannel.Add(videoModel);
                    }
                }
            }
            return addedVideoTubestoChannel;

        }

        public static CustomVideoTubeUploadModel InitializeVideoTubeUploadForUser(int userId, string filename, string videoTubeStagingKey, float duration, DateTime clientTime)
        {
            Logger.Info(String.Format("Initializing custom video type upload for user={0}, video='{1}' of duration {2} at {3}", userId, videoTubeStagingKey, duration, clientTime.ToString()));

            CustomVideoTubeUploadModel videoTubeModel = null;

            using (var videoTubeRepository = new VideoTubeRepository())
            {
                CustomVideoTubeUploadPo videoTube = videoTubeRepository.InitializeVideoTubeUploadForUser(userId, filename, videoTubeStagingKey, duration, clientTime);

                if (videoTube != null)
                {
                    videoTubeModel = new CustomVideoTubeUploadModel(S3_DOMAIN, videoTube);
                }
                else
                {
                    videoTubeModel = new CustomVideoTubeUploadModel();
                    videoTubeModel.Message = "Failed to initialize video tube upload";
                }
            }

            return videoTubeModel;
        }

        public static void UpdateUploadedVideo(CustomVideoTubeUploadModel videoModel)
        {
            if (videoModel == null)
            {
                throw new Exception("Failed to update uploaded video. Invalid video specified");
            }

            Logger.Info(String.Format("Updating uploaded video with id={0}", videoModel.VideoTubeId));

            using (var repository = new VideoTubeRepository())
            {
                bool isSuccess = repository.UpdateCustomVideoTube(videoModel.VideoTubeId, videoModel.Title, videoModel.Keywords, videoModel.Description,
                                                                     videoModel.IsPrivate, videoModel.IsRRated, videoModel.CategoryId);

                List<VideoTubeThumbnail> thumbnails = repository.GetThumbnailsByVideoId(videoModel.VideoTubeId);

                if (isSuccess)
                {
                    var firstThumbnail = thumbnails.FirstOrDefault(x => x.Position == 1);
                    var secondThumbnail = thumbnails.FirstOrDefault(x => x.Position == 2);
                    var thirdThumbnail = thumbnails.FirstOrDefault(x => x.Position == 3);

                    InsertOrUpdateVideoThumbnail(videoModel.VideoTubeId, firstThumbnail, videoModel.FirstThumbnailKey, videoModel.FirstThumbnailKey == videoModel.ActiveThumbnailKey, 1);
                    InsertOrUpdateVideoThumbnail(videoModel.VideoTubeId, secondThumbnail, videoModel.SecondThumbnailKey, videoModel.SecondThumbnailKey == videoModel.ActiveThumbnailKey, 2);
                    InsertOrUpdateVideoThumbnail(videoModel.VideoTubeId, thirdThumbnail, videoModel.ThirdThumbnailKey, videoModel.ThirdThumbnailKey == videoModel.ActiveThumbnailKey, 3);

                    InsertOrUpdateVideoPreviewClip(videoModel.VideoTubeId, videoModel.VideoPreviewKey, duration: 30, isAutoGenerated: true);

                    Logger.Debug(String.Format("Custom uploaded video id={0} was successfully updated", videoModel.VideoTubeId));
                }
                else
                {
                    throw new Exception(String.Format("Failed to update uploaded video with id={0}", videoModel.VideoTubeId));
                }
            }
        }

        private static void InsertOrUpdateVideoPreviewClip(int videoTubeId, string videoTubePreviewClipKey, float duration, bool isAutoGenerated)
        {
            using (var repository = new VideoTubeRepository())
            {
                VideoTubePreviewClip existingClip = repository.GetVideoTubePreviewClipByVideoTubeId(videoTubeId);

                if (existingClip != null && existingClip.VideoTubeId > 0)
                {
                    existingClip.Duration = duration;
                    existingClip.PreviewClipKey = videoTubePreviewClipKey;
                    existingClip.IsAutoGenerated = isAutoGenerated;

                    repository.UpdateVideoTubePreviewClip(existingClip);
                }
                else
                {
                    repository.InsertVideoTubePreviewClip(new VideoTubePreviewClip()
                    {
                        VideoTubeId = videoTubeId,
                        PreviewClipKey = videoTubePreviewClipKey,
                        Duration = duration,
                        IsAutoGenerated = isAutoGenerated
                    });
                }
            }
        }

        private static void InsertOrUpdateVideoThumbnail(int videoTubeId, VideoTubeThumbnail thumbnail, string thumbnailKey, bool isActive, int position)
        {
            using (var repository = new VideoTubeRepository())
            {
                if (!String.IsNullOrEmpty(thumbnailKey))
                {
                    thumbnail = thumbnail ?? new VideoTubeThumbnail()
                                                    {
                                                        VideoTubeId = videoTubeId,
                                                        ThumbnailKey = thumbnailKey,
                                                        Position = position,
                                                        IsActive = isActive,
                                                        AddedDateTime = DateTime.Now
                                                    };

                    if (thumbnail.VideoTubeThumbnailId > 0)
                    {
                        thumbnail.ThumbnailKey = thumbnailKey;
                        thumbnail.IsActive = isActive;

                        if (repository.UpdateVideoTubeThumbnail(thumbnail))
                        {
                            Logger.Debug(String.Format("Updated thumbnail at position={0} for video id={1}", position, videoTubeId));
                        }
                        else
                        {
                            Logger.Debug(String.Format("Failed to update thumbnail at position={0} for video id={1}", position, videoTubeId));
                        }
                    }
                    else
                    {
                        if (repository.InsertVideoTubeThumbnail(thumbnail))
                        {
                            Logger.Debug(String.Format("Added thumbnail at position={0} for video id={1}", position, videoTubeId));
                        }
                        else
                        {
                            Logger.Debug(String.Format("Failed to add thumbnail at position={0} for video id={1}", position, videoTubeId));
                        }
                    }
                }

            }
        }

        public static CustomVideoTubeUploadModel GetCustomVideoTubeById(int videoTubeId)
        {
            Logger.Info(String.Format("Retrieving custom video tube with id={0}", videoTubeId));

            CustomVideoTubeUploadModel model = null;

            using (var repository = new VideoTubeRepository())
            {
                var video = repository.GetCustomVideoTubeById(videoTubeId);
                if (video != null)
                {
                    model = new CustomVideoTubeUploadModel(S3_DOMAIN, video);
                }
            }

            return model;
        }

        public static List<VideoTubeModel> GetVideoTubesByOwnerUserId(int userId)
        {
            Logger.Info(String.Format("Retrieving all videos by user  id={0}", userId));
            List<VideoTubeModel> videoList = new List<VideoTubeModel>();
          
            using (var repository = new VideoTubeRepository())
            {
                videoList = repository.GetVideoTubesByUserId(userId);
            }

            return videoList;
        }
        public static VideoTubeModel GetPrivateVideoByProviderIdAndChannelId(string providerId, int channelTubeId)
        {
            Logger.Debug(String.Format("Get private video from channel with id={0} with parameters: provider id={1}", channelTubeId, providerId));
            VideoTubeModel video = null;
            using (var repository = new VideoTubeRepository())
            {
                 video = repository.GetPrivateVideoByProviderIdAndChannelId(providerId, channelTubeId);
              
            }

            return video;

        }
        public static VideoTubeModel AddPrivateVideoToChannelTubeById(int channelTubeId, int provideId, string description, string title, string videoUrl, int categoryId, bool isMatureContent, double durationInSec, string thumbnailUrl)
        {
            Logger.Debug(String.Format("Adding private video to channel with id={0} with parameters: provider id={1}, description='{2}', title='{3}', videoUrl='{4}', categoryId={5}, isMatureContent={6}, durationInSec={7}, thumbnail='{8}'",
                channelTubeId, provideId, description, title, videoUrl, categoryId, isMatureContent, durationInSec, thumbnailUrl));

            VideoTubeModel video = null;

            description = TextUtils.ReplaceNonPrintableCharacters(description);
            title = TextUtils.ReplaceNonPrintableCharacters(title);

            using (var repository = new VideoTubeRepository())
            {
                var newVideo = repository.InsertPrivateVideoForChannel(description, title, videoUrl, categoryId, isMatureContent, durationInSec, provideId);
              
              
                if (newVideo != null)
                {
                    repository.AddVideoTubeToChannelTube(channelTubeId, newVideo.VideoTubeId);
                    repository.InsertVideoTubeThumbnail(new VideoTubeThumbnail()
                    {
                        VideoTubeId = newVideo.VideoTubeId,
                        ThumbnailKey = thumbnailUrl,
                        Position = 1,
                        IsActive = true
                    });

                    var videoPo = repository.GetVideoTubePoById(newVideo.VideoTubeId);

                    video = new VideoTubeModel(videoPo);
                }
            }

            return video;
        }

        public static void UpdateVideoTubeStatuses()
        {
            Logger.Info("Updating provider statuses for system/user videos");

            using (var repository = new VideoTubeRepository())
            {
                List<VideoTubeModel> videosToUpdate = repository.RetrievePageOfVideosToUpdate();

                YouTubeServiceManage.UpdateVideoStatus(videosToUpdate);
                VimeoServiceManage.UpdateVideoStatus(videosToUpdate);

                videosToUpdate.Where(x => x.VideoProviderId == 3).ToList().ForEach(x =>
                {
                    x.IsRemovedByProvider = false;
                    x.IsRestrictedByProvider = false;
                    x.IsPrivate = false;
                    x.IsRRated = false;
                });

                videosToUpdate.ForEach(x =>
                {
                    repository.UpdateVideoTubeStatusById(x.VideoTubeId, x.IsPrivate, x.IsRestrictedByProvider, x.IsRemovedByProvider, x.IsRRated);
                });
            }
        }

        public static VideoTubeModel UpdatePrivateVideoById(int videoTubeId, int providerId, string description, string title, string videoUrl, int categoryId, bool isMatureContent, double durationInSec, string thumbnailUrl)
        {
            Logger.Debug(String.Format("Updating private video with id={0}", videoTubeId));

            var existing = VideoTubeManage.GetVideoTubeById(videoTubeId);

            VideoTubeModel updated = null;

            if (existing != null)
            {
               
                using (var repository = new VideoTubeRepository()) 
                {
                    updated = repository.UpdatePrivateVideoTube(videoTubeId, providerId, title, description, videoUrl, categoryId, isMatureContent, durationInSec);

                    if (updated != null)
                    {
                        if (!String.IsNullOrEmpty(thumbnailUrl))
                        {
                            repository.UpdateVideoTubeThumbnail(new VideoTubeThumbnail()
                            {
                                VideoTubeId = videoTubeId,
                                ThumbnailKey = thumbnailUrl,
                                Position = 1,
                                IsActive = true
                            });
                        }
                        

                        var videoPo = repository.GetVideoTubePoById(videoTubeId);

                        updated = new VideoTubeModel(videoPo);
                    }

                }
            }

            return updated;
        }

        public static VideoTubeModel AddLiveVideoTubeToChannelTubeForCategory(string providerVideoId, int channelTubeId, int categoryId, DateTime startDate, DateTime? endDate, int userId)
        {
            Logger.Info(String.Format("Adding external live video with ProviderId='{0}' to channel with Id={1} under category with Id={2} that starts at '{3}', ends at '{4}'",
                providerVideoId, channelTubeId, categoryId, startDate.ToString(), endDate != null ? endDate.ToString() : "N/A"));

            var channel = ChannelManage.GetChannelTubeById(channelTubeId);
            var user = channel != null ? UserManage.GetUserByChannelName(channel.Name) : null;

            bool allowMatureContent = user != null ? user.MatureContentAllowed : false;
            bool PrivateVideoModeEnabled = user != null ? user.PrivateVideoModeEnabled : false;

            var externalVideo = YouTubeServiceManage.GetVideoByUrl(providerVideoId, allowMatureContent);

            VideoTubeModel videoTubeModel = new VideoTubeModel();

            if (externalVideo != null)
            {
                using (var videoTubeRepository = new VideoTubeRepository())
                {
                    
                    
                    var videoTube = videoTubeRepository.InsertVideoLiveTubeWithGet(externalVideo.Title, externalVideo.Description, externalVideo.ProviderVideoId, categoryId, externalVideo.VideoProviderId, externalVideo.IsRRated, externalVideo.Thumbnail, startDate, endDate, userId);

                    if (videoTube != null && videoTube.VideoLiveTubeId > 0)
                    {
                        videoTubeRepository.AddVideoLiveTubeToChannelTube(channelTubeId, videoTube.VideoLiveTubeId);
                        Logger.Debug(String.Format("Successfully added external live video using ProviderId={0} to channel with Id={1} for category with Id={2}", providerVideoId, channelTubeId, categoryId));

                        var videoPo = videoTubeRepository.GetVideoLiveTubePoById(videoTube.VideoLiveTubeId);

                        videoTubeModel = new VideoTubeModel(AWS_CLOUD_FRONT_WEB_DIST_DOMAIN, videoPo);
                    }
                    else
                    {
                        videoTubeModel.Message = String.Format("Error occurred while adding video '{0}' to channel. Try again later", externalVideo.Title);
                        Logger.Warn(String.Format("Failed to add external video using ProviderId={0}. Unable to add video to channel with Id={1}", providerVideoId, channelTubeId));
                    }
                }
            }
            else
            {
                Logger.Warn(String.Format("Failed to retrieve external video using ProviderId={0}. Unable to add video to channel with Id={1}", providerVideoId, channelTubeId));
            }
         
            return videoTubeModel;
        }

        public static bool RemoveVideoLiveTubeFromChannelTubeById(int channelTubeId, int videoLiveTubeId)
        {
            if (channelTubeId <= 0 || videoLiveTubeId <= 0)
            {
                throw new ArgumentException("Invalid channel tube id or video live tube id specified");
            }

            Logger.Info(String.Format("Removing video live tube with Id={0} from channel tube with Id={1}", videoLiveTubeId, channelTubeId));

            bool isSuccess = false;

            using (var videoTubeRepository = new VideoTubeRepository())
            {
                isSuccess = videoTubeRepository.DeleteVideoLiveTubeFromChannelTubeById(channelTubeId, videoLiveTubeId);
            }

            return isSuccess;
        }

        public static VideoTubeModel UpdateVideoLiveTubeById(int videoLiveTubeId, DateTime startTime, DateTime? endTime)
        {
            Logger.Debug(String.Format("Updating live video with id={0}", videoLiveTubeId));

            var existing = VideoTubeManage.GetVideoLiveTubeById(videoLiveTubeId);

            VideoTubeModel model = null;

            if (existing != null)
            {

                using (var repository = new VideoTubeRepository())
                {
                    var updated = repository.UpdateVideoLiveTubeById(videoLiveTubeId, startTime, endTime);
                    model = new VideoTubeModel(updated);
                }
            }

            return model;
        }

        private static VideoLiveTubePo GetVideoLiveTubeById(int videoLiveTubeId)
        {
            if (videoLiveTubeId <= 0)
            {
                return null;
            }

            Logger.Debug(String.Format("Requesting live video with id={0}", videoLiveTubeId));

            VideoLiveTubePo video = null;

            using (var repository = new VideoTubeRepository())
            {
                video = repository.GetVideoLiveTubePoById(videoLiveTubeId);
            }

            return video;
        }
    }
}
