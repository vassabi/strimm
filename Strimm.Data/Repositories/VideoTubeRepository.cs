using Dapper;
using log4net;
using Strimm.Data.Interfaces;
using Strimm.Model;
using Strimm.Model.Criteria;
using Strimm.Model.Projections;
using Strimm.Model.WebModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace Strimm.Data.Repositories
{
    public class VideoTubeRepository : RepositoryBase, IVideoTubeRepository
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(VideoTubeRepository));

        public VideoTubeRepository()
            : base()
        {

        }

        public bool InsertVideoTube(string title, string description, string providerVideoId, long duration, int categoryId, int videoProviderId, bool isRRated, bool isInPublicLibrary, bool isPrivate)
        {
            Contract.Requires(!String.IsNullOrEmpty(title), "Video title was not specified");
            Contract.Requires(!String.IsNullOrEmpty(providerVideoId), "Provider video Id is missing or invalid");
            Contract.Requires(duration > 0, "Video duration was not specified");
            Contract.Requires(categoryId > 0, "Invalid category id was specified");
            Contract.Requires(videoProviderId > 0, "Invalid video provider id was specified");

            bool isSuccess = false;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    int rowcount = this.StrimmDbConnection.Execute("strimm.InsertVideoTube", new {
                                                                                                    Title = title,
                                                                                                    Description = description,
                                                                                                    ProviderVideoId = providerVideoId,
                                                                                                    Duration = duration,
                                                                                                    CategoryId = categoryId,
                                                                                                    VideoProviderId = videoProviderId,
                                                                                                    IsRRated = isRRated,
                                                                                                    IsInPublicLibrary = isInPublicLibrary,
                                                                                                    IsPrivate = isPrivate
                                                                                                 }, null, 30, commandType: CommandType.StoredProcedure);
                    isSuccess = rowcount == 1;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to insert video tube with VideoProviderId = {0}", providerVideoId), ex);
            }

            return isSuccess;
        }

        public void RemoveAllRestrictedAndDeletedVideosAsOfDate(DateTime asOfDate)
        {
            Contract.Requires(asOfDate > DateTime.MinValue && asOfDate < DateTime.Now, "Invalid as of date specified on request to remove all deleted and restricted videos");

            Logger.Info(String.Format("Deleting all restricted and deleted videos as of: {0}", asOfDate.ToString("yyyy-MM-dd")));

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    this.StrimmDbConnection.Execute("strimm.RemoveAllRestrictedAndDeletedVideosAsOfDate", new { AsOfDate = asOfDate.ToString("yyyy-MM-dd") }, null, null, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to delete all restricted and deleted videoTubes as of {0}", asOfDate.ToString("yyyy-MM-dd")), ex);
            }
        }

        public VideoTube InsertVideoTubeWithGet(string title, string description, string providerVideoId, long duration, int categoryId, int videoProviderId, bool isRRated,
            bool isInPublicLibrary, bool isPrivate, string thumbnail, bool PrivateVideoModeEnabled)
        {
            Contract.Requires(!String.IsNullOrEmpty(title), "Video title was not specified");
            Contract.Requires(!String.IsNullOrEmpty(providerVideoId), "Provider video Id is missing or invalid");
            Contract.Requires(duration > 0, "Video duration was not specified");
            Contract.Requires(categoryId > 0, "Invalid category id was specified");
            Contract.Requires(videoProviderId > 0, "Invalid video provider id was specified");

            VideoTube videoTube = null;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    var results = this.StrimmDbConnection.Query<VideoTube>("strimm.InsertVideoTubeWithGet", new
                    {
                        Title = title,
                        Description = description,
                        ProviderVideoId = providerVideoId,
                        Duration = duration,
                        CategoryId = categoryId,
                        VideoProviderId = videoProviderId,
                        IsRRated = isRRated,
                        IsInPublicLibrary = isInPublicLibrary,
                        IsPrivate = isPrivate,
                        Thumbnail = thumbnail,
                        PrivateVideoModeEnabled = PrivateVideoModeEnabled
                    }, null, false, 30, commandType: CommandType.StoredProcedure);
                    videoTube = results.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to insert video tube with VideoProviderId = {0}", providerVideoId), ex);
            }

            return videoTube;
        }

        public List<VideoTubeModel> GetVideoTubeByKeywordAndChannelId(List<string> keywords, int channelTubeId)
        {
            Logger.Info("Retrieving video tubes by keywords");

            Contract.Requires(keywords != null && keywords.Count > 0, "No keywords specified to perform the search");

            if (keywords == null || keywords.Count == 0)
            {
                throw new ArgumentException("No keywords specified to perform the search");
            }

            var videos = new List<VideoTubeModel>();
            var builder = new StringBuilder();

            keywords.ForEach(x =>
            {
                builder.Append(x).Append(",");
            });

            string keywordsString = builder.ToString().TrimEnd(',');

            try
            {

                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    videos = this.StrimmDbConnection.Query<VideoTubeModel>("strimm.GetVideoTubeByKeywordAndChannelId", new { Keywords = keywordsString, ChannelTubeId = channelTubeId}, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve videotubes by keywords='{0}'", keywordsString), ex);
            }

            return videos;
        }

        public List<VideoTubeModel> GetVideoTubeByKeywordForPublicLibrary(List<string> keywords)
        {
            Logger.Info("Retrieving video tubes  by keywords");

            Contract.Requires(keywords != null && keywords.Count > 0, "No keywords specified to perform the search");

            if (keywords == null || keywords.Count == 0)
            {
                throw new ArgumentException("No keywords specified to perform the search");
            }

            var videos = new List<VideoTubeModel>();
            var builder = new StringBuilder();

            keywords.ForEach(x =>
            {
                builder.Append(x).Append(",");
            });

            string keywordsString = builder.ToString().TrimEnd(',');

            try
            {

                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    videos = this.StrimmDbConnection.Query<VideoTubeModel>("strimm.GetVideoTubeByKeywordForPublicLibrary", new { Keywords = keywordsString }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve videotubes by keywords='{0}'", keywordsString), ex);
            }

            return videos;
        }

        public List<VideoTubeModel> GetVideoTubesByUserId(int userId)
        {
            Logger.Info("Retrieving video tubes by userId");

            Contract.Requires(userId != null , "userId cant be null");
            var videos = new List<VideoTubeModel>();

            try
            {

                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    videos = this.StrimmDbConnection.Query<VideoTubeModel>("strimm.GetUserUploadedVideoTubesByUserId", new { UserId = userId }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve videotubes by userId='{0}'", userId), ex);
            }

            return videos;
        }
        public bool BulkInsertVideoTubeListIntoChannelTubeById(int channelTubeId, List<int> videoTubeIds, bool addToVideoRoom)
        {
            Contract.Requires(channelTubeId > 0, "Invalid channelTubeId was specified");
            Contract.Requires(videoTubeIds != null && videoTubeIds.Count > 0, "Collection of VideoTubeIds is empty or was not defined");

            bool isSuccess = false;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    var builder = new StringBuilder();
                    videoTubeIds.ForEach(x => builder.Append(x).Append(','));
                    
                    int rowcount = this.StrimmDbConnection.Execute("strimm.BulkInsertVideoTubeListIntoChannelTubeById", new
                    {
                        ChannelTubeId = channelTubeId,
                        VideoTubeIds = builder.ToString().TrimEnd(','),
                        AddToVideoRoom = addToVideoRoom
                    }, null, 30, commandType: CommandType.StoredProcedure);
                    isSuccess = rowcount > 0;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to insert video tubes into channel tube with Id = {0}", channelTubeId), ex);
            }

            return isSuccess;
        }

        public bool BulkInsertVideoTubeListIntoVideoRoomTubeById(int videoRoomTubeId, List<int> videoTubeIds)
        {
            Contract.Requires(videoRoomTubeId > 0, "Invalid VideoRoomTubeId was specified");
            Contract.Requires(videoTubeIds != null && videoTubeIds.Count > 0, "Collection of VideoTubeIds is empty or was not defined");

            bool isSuccess = false;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    var builder = new StringBuilder();
                    videoTubeIds.ForEach(x => builder.Append(x).Append(','));

                    int rowcount = this.StrimmDbConnection.Execute("strimm.BulkInsertVideoTubeListIntoVideoRoomTubeById", new
                    {
                        VideoRoomTubeId = videoRoomTubeId,
                        VideoTubeIds = builder.ToString().TrimEnd(',')
                    }, null, 30, commandType: CommandType.StoredProcedure);
                    isSuccess = rowcount > 0;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to insert video tubes into video room tube with Id = {0}", videoRoomTubeId), ex);
            }

            return isSuccess;
        }

        public VideoTube GetVideoTubeById(int videoTubeId)
        {
            Contract.Requires(videoTubeId > 0, "VideoTubeId should be greater then 0");

            VideoTube video = null;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    var results = this.StrimmDbConnection.Query<VideoTube>("strimm.GetVideoTubeById", new { VideoTubeId = videoTubeId }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                    video = results.FirstOrDefault();
                }            
            }
            catch(Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve video tube by id = {0}", videoTubeId), ex);
            }

            return video;
        }

        public bool DeleteVideoTubeById(int videoTubeId)
        {
            Contract.Requires(videoTubeId > 0, "Invalid VideoTubeId specified");

            bool isSuccess = false;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    int rowcount = this.StrimmDbConnection.Execute("strimm.DeleteVideoTubeById", new { VideoTubeId = videoTubeId }, null, 30, commandType: CommandType.StoredProcedure);
                    isSuccess = rowcount == 1;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to delete video tube with Id = {0}", videoTubeId), ex);
            }

            return isSuccess;
        }

        public bool RemoveVideoTubeFromPublicLib(int videoTubeId)
        {
            Contract.Requires(videoTubeId > 0, "VideoTubeId should be greater then 0");

            var video = this.GetVideoTubeById(videoTubeId);
            bool isSuccess = false;

            if (video != null && video.VideoTubeId > 0)
            {
                video.IsInPublicLibrary = false;
                isSuccess = this.UpdateVideoTube(video);
                Logger.Debug(String.Format("Successfully removed video with Id={0} from public library", videoTubeId));
            }
            else
            {
                Logger.Debug(String.Format("Failed to removed video with Id={0} from public library. Specified video id does not correspond to a valid video record", videoTubeId));
            }

            return isSuccess;
        }

        public bool UpdateVideoTube(VideoTube videoTube)
        {
            Contract.Requires(videoTube != null, "Invalid or missing video tube specified");
            Contract.Requires(videoTube.VideoTubeId > 0, "Video tube does not correspond to any existing record");

            bool isSuccess = false;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    int rowcount = this.StrimmDbConnection.Execute("strimm.UpdateVideoTube",new{
                        VideoTubeId = videoTube.VideoTubeId,
                        Title = videoTube.Title,
                        Description = videoTube.Description,
                        ProviderVideoId = videoTube.ProviderVideoId,
                        Duration = videoTube.Duration,
                        CategoryId = videoTube.CategoryId,
                        VideoProviderId = videoTube.VideoProviderId,
                        IsRRated = videoTube.IsRRated,
                        IsInPublicLibrary = videoTube.IsInPublicLibrary,
                        IsPrivate = videoTube.IsPrivate,
                        IsRestrictedByProvider = videoTube.IsRestrictedByProvider,
                        IsRemovedByProvider = videoTube.IsRemovedByProvider,
                        CreatedDate = videoTube.CreatedDate,
                        VideoKey = videoTube.VideoKey,
                        VideoStatus = videoTube.VideoStatus,
                        VideoStatusMessage = videoTube.VideoStatusMessage
                    }, null, 30, commandType: CommandType.StoredProcedure);
                    isSuccess = rowcount == 1;
                }
            }
            catch(Exception ex)
            {
                Logger.Error(String.Format("Failed to update video tube with Id={0}", videoTube.VideoTubeId), ex);
            }

            return isSuccess;
        }

        public List<VideoTubePo> GetAllPublicVideoTubes()
        {
            var videoTubesPos = new List<VideoTubePo>();

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    videoTubesPos = this.StrimmDbConnection.Query<VideoTubePo>("strimm.GetAllPublicVideoTubes", null, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to retrieve all public videos", ex);
            }

            return videoTubesPos;
        }

        public List<VideoTubePo> GetAllPublicVideoTubesForStatusUpdate()
        {
            var videoTubesPos = new List<VideoTubePo>();

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    videoTubesPos = this.StrimmDbConnection.Query<VideoTubePo>("strimm.GetAllPublicVideoTubesForStatusUpdate", null, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to retrieve all public videos", ex);
            }

            return videoTubesPos;
        }

        public List<VideoTubePo> GetVideoTubePosFromVideoRoomTubeByUserId(int userId)
        {
            var videoTubesPos = new List<VideoTubePo>();

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    videoTubesPos = this.StrimmDbConnection.Query<VideoTubePo>("strimm.GetVideoTubePosFromVideoRoomTubeByUserId", new { UserId = userId }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve all videos from video room by user Id={0}", userId), ex);
            }

            return videoTubesPos;
        }

        public VideoTube GetVideoTubeByProviderVideoId(string providerVideoId)
        {
            Contract.Requires(!String.IsNullOrEmpty(providerVideoId), "ProviderVideoId was not specified");

            VideoTube videoTube = null;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    var results = this.StrimmDbConnection.Query<VideoTubePo>("strimm.GetVideoTubeByProviderVideoId", new { ProviderVideoId = providerVideoId }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                    videoTube = results.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve videoTube by providerVideoId = {0}", providerVideoId), ex);
            }

            return videoTube;
        }

        public VideoTube GetVideoTubeByProviderVideoIdAndVideoRoomTubeId(string providerVideoId, int videoRoomTubeId)
        {
            Contract.Requires(!String.IsNullOrEmpty(providerVideoId), "Invalid videoTubeId specified");
            Contract.Requires(videoRoomTubeId > 0, "Invalid videoRoomTubeId specified");

            VideoTube videoTube = null;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    var results = this.StrimmDbConnection.Query<VideoTubePo>("strimm.GetVideoTubeByProviderVideoIdAndVideoRoomTubeId", new { ProviderVideoId = providerVideoId, VideoRoomTubeId = videoRoomTubeId }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                    videoTube = results.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve videoTube '{0}' for videoRoomTubeId={1}", providerVideoId, videoRoomTubeId), ex);
            }

            return videoTube;
        }

        public VideoTube GetVideoTubeByIdAndVideoRoomTubeId(int videoTubeId, int videoRoomTubeId)
        {
            Contract.Requires(videoTubeId > 0, "Invalid videoTubeId specified");
            Contract.Requires(videoRoomTubeId > 0, "Invalid videoRoomTubeId specified");

            VideoTube videoTube = null;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    var results = this.StrimmDbConnection.Query<VideoTubePo>("strimm.GetVideoTubeByIdAndVideoRoomTubeId", new { VideoTubeId = videoTubeId, VideoRoomTubeId = videoRoomTubeId }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                    videoTube = results.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve videoTube with Id={0} for videoRoomTubeId={1}", videoTubeId, videoRoomTubeId), ex);
            }

            return videoTube;
        }

        public List<VideoTubePo> GetPublicVideoTubesByCategoryId(int categoryId)
        {
            Contract.Requires(categoryId > 0, "CategoryId should be greater then 0");

            var videoTubesPos = new List<VideoTubePo>();

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    videoTubesPos = this.StrimmDbConnection.Query<VideoTubePo>("strimm.GetPublicVideoTubesByCategoryId", new { CategoryId = categoryId }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve all public videos by categoryId = {0}", categoryId), ex);
            }

            return videoTubesPos;
        }

        public List<VideoTubePo> GetVideoTubePosFromVideoRoomTubeByUserIdAndCategoryIdAndPageIndex(int userId, int categoryId, int pageIndex, bool retrieveMyVideos, bool retrieveLicensedVideos, bool retrieveExternalVideos, string keywords, out int pageCount, int pageSize = 0)
        {
            var videoTubesPos = new List<VideoTubePo>();
            int count = 0;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    var dynamicParameter = new DynamicParameters();
                    dynamicParameter.Add("@UserId", userId);
                    dynamicParameter.Add("@CategoryId", categoryId);
                    dynamicParameter.Add("@PageIndex", pageIndex);
                    dynamicParameter.Add("@PageSize", pageSize);
                    dynamicParameter.Add("@RetrieveMyVideos", retrieveMyVideos);
                    dynamicParameter.Add("@RetrieveLicensedVideos", retrieveLicensedVideos);
                    dynamicParameter.Add("@RetrieveExternalVideos", retrieveExternalVideos);
                    dynamicParameter.Add("@Keywords", keywords, DbType.AnsiString);
                    dynamicParameter.Add("@PageCount", null, DbType.Int32, ParameterDirection.Output);

                    videoTubesPos = this.StrimmDbConnection.Query<VideoTubePo>("strimm.GetVideoTubePosFromVideoRoomTubeByUserIdAndCategoryIdAndPageIndex", dynamicParameter, null, false, 30, commandType: CommandType.StoredProcedure).ToList();

                    count = dynamicParameter.Get<int>("@PageCount");
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve all videos from video room by user Id={0}", userId), ex);
            }

            pageCount = count;
            
            return videoTubesPos;
        }

        public List<VideoTubePo> GetVideoTubePosFromVideoRoomTubeByUserIdAndPageIndex(int userId, int pageIndex, bool retrieveMyVideos, bool retrieveLicensedVideos, bool retrieveExternalVideos, string keywords, out int pageCount, int pageSize = 0)
        {
            var videoTubesPos = new List<VideoTubePo>();
            int count = 0;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    var dynamicParameter = new DynamicParameters();
                    dynamicParameter.Add("@UserId", userId);
                    dynamicParameter.Add("@PageIndex", pageIndex);
                    dynamicParameter.Add("@PageSize", pageSize);
                    dynamicParameter.Add("@RetrieveMyVideos", retrieveMyVideos, DbType.Boolean);
                    dynamicParameter.Add("@RetrieveLicensedVideos", retrieveLicensedVideos, DbType.Boolean);
                    dynamicParameter.Add("@RetrieveExternalVideos", retrieveExternalVideos, DbType.Boolean);
                    dynamicParameter.Add("@Keywords", keywords, DbType.AnsiString);
                    dynamicParameter.Add("@PageCount", null, DbType.Int32, ParameterDirection.Output);

                    videoTubesPos = this.StrimmDbConnection.Query<VideoTubePo>("strimm.GetVideoTubePosFromVideoRoomTubeByUserIdAndPageIndex", dynamicParameter, null, false, 30, commandType: CommandType.StoredProcedure).ToList();

                    count = dynamicParameter.Get<int>("@PageCount");
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve all videos from video room by user Id={0}", userId), ex);
            }

            pageCount = count;

            return videoTubesPos;
        }

        public List<VideoTubePo> GetPublicVideoTubesByCategoryIdAndPageIndex(int categoryId, int pageIndex, string keywords, out int pageCount, int pageSize = 10)
        {
            Contract.Requires(categoryId > 0, "CategoryId should be greater then 0");

            var videoTubesPos = new List<VideoTubePo>();
            int count = 0;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    var dynamicParameter = new DynamicParameters();
                    dynamicParameter.Add("@CategoryId", categoryId);
                    dynamicParameter.Add("@PageIndex", pageIndex);
                    dynamicParameter.Add("@PageSize", pageSize);
                    dynamicParameter.Add("@Keywords", keywords);
                    dynamicParameter.Add("@PageCount", null, DbType.Int32, ParameterDirection.Output);

                    videoTubesPos = this.StrimmDbConnection.Query<VideoTubePo>("strimm.GetPublicVideoTubesByCategoryIdAndPageIndex", dynamicParameter, null, false, 30, commandType: CommandType.StoredProcedure).ToList();

                    count = dynamicParameter.Get<int>("@PageCount");
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve all public videos by categoryId = {0} and page index={1}", categoryId, pageIndex), ex);
            }

            pageCount = count;

            return videoTubesPos;
        }

        public List<VideoTubePo> GetPublicVideoTubesByVideoProviderId(int videoProviderId)
        {
            Contract.Requires(videoProviderId > 0, "VideoProviderId should be greater then 0");

            var videoTubesPos = new List<VideoTubePo>();

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    videoTubesPos = this.StrimmDbConnection.Query<VideoTubePo>("strimm.GetPublicVideoTubesByVideoProviderId", new { VideoProviderId = videoProviderId }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve all public videos by videoProviderId = {0}", videoProviderId), ex);
            }

            return videoTubesPos;
        }

        public List<VideoTubePo> GetPublicVideoTubesByVideoProviderName(string videoProviderName)
        {
            Contract.Requires(!String.IsNullOrEmpty(videoProviderName), "UserId should be greater then 0");

            var videos = new List<VideoTubePo>();

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    videos = this.StrimmDbConnection.Query<VideoTubePo>("strimm.GetVideoTubesByProviderName", new { VideoProviderName = videoProviderName }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to all videos for video provider '{0}'", videoProviderName), ex);
            }

            return videos;
        }

        public List<VideoTubePo> GetVideoTubesByTitleKeywords(List<string> keywordList)
        {
            Contract.Requires(keywordList != null && keywordList.Count > 0, "Invalid or empty list of keywords specified");

            var videos = new List<VideoTubePo>();
            var stringBuilder = new StringBuilder();
            
            keywordList.ForEach(x => stringBuilder.Append(x).Append(','));

            string keywords = stringBuilder.ToString().TrimEnd(',');

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    videos = this.StrimmDbConnection.Query<VideoTubePo>("strimm.GetVideoTubesByTitleKeywords", new { Keyworkds = keywords }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to get videos by keywords '{0}'", keywords), ex);
            }

            return videos;
        }

        public List<VideoTubePo> GetVideoTubesByVideoRoomTubeId(int videoRoomTubeId)
        {
            Contract.Requires(videoRoomTubeId > 0, "VideoRoomTubeId should be greater then 0");

            var videos = new List<VideoTubePo>();

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    videos = this.StrimmDbConnection.Query<VideoTubePo>("strimm.GetVideoTubesByVideoRoomTubeId", new { VideoRoomTubeId = videoRoomTubeId }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to all videos for video room with Id '{0}'", videoRoomTubeId), ex);
            }

            return videos;
        }

        public List<VideoTubePo> GetVideoTubesByVideoRoomTubeIdAndCategoryId(int videoRoomTubeId, int categoryId)
        {
            Contract.Requires(videoRoomTubeId > 0, "VideoRoomTubeId should be greater then 0");
            Contract.Requires(categoryId > 0, "CategoryId should be greater then 0");

            var videos = new List<VideoTubePo>();

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    videos = this.StrimmDbConnection.Query<VideoTubePo>("strimm.GetVideoTubesByVideoRoomTubeIdAndCategoryId", new { VideoRoomTubeId = videoRoomTubeId, CategoryId = categoryId }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to all videos for video room with Id={0} and category Id={1}", videoRoomTubeId, categoryId), ex);
            }

            return videos;
        }

        public bool AddVideoTubeToUserArchive(int userId, int videoTubeId, DateTime clientTime)
        {
            Contract.Requires(userId > 0, "UerId should be greater then 0");
            Contract.Requires(videoTubeId > 0, "VideoTubeId should be greater then 0");
            Contract.Requires(clientTime >= DateTime.MinValue && clientTime <= DateTime.MaxValue, "Invalid client date and time specified");

            bool isSuccess = false;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    int rowcount = this.StrimmDbConnection.Execute("strimm.AddVideoTubeToUserArchive", new { UserId = userId, VideoTubeId = videoTubeId, ClientTime = clientTime }, null, 30, commandType: CommandType.StoredProcedure);
                    isSuccess = rowcount == 1;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to add video Tube with Id={0} to archive for user Id={1} as of '{2}'", videoTubeId, userId, clientTime), ex);
            }

            return isSuccess;
        }

        public VideoTube GetVideoTubeFromArchiveByUserIdAndVideoTubeId(int userId, int videoTubeId)
        {
            Contract.Requires(userId > 0, "UerId should be greater then 0");
            Contract.Requires(videoTubeId > 0, "VideoTubeId should be greater then 0");

            VideoTube archivedVideo = null;
            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    var results = this.StrimmDbConnection.Query<VideoTube>("strimm.GetVideoTubeFromArchiveByUserIdAndVideoTubeId", 
                            new { VideoTubeId = videoTubeId, UserId = userId }, 
                            null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                    archivedVideo = results.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve archived video with Id={0} for user Id={1}", videoTubeId, userId), ex);
            }

            return archivedVideo;
        }

        public List<VideoTubePo> GetArchivedUserVideoTubesByUserId(int userId)
        {
            Contract.Requires(userId > 0, "UerId should be greater then 0");

            var archivedVideos = new List<VideoTubePo>();

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    archivedVideos = this.StrimmDbConnection.Query<VideoTubePo>("strimm.GetAllArchivedUserVideoTubesByUserId", new { UserId = userId }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve archived videos for user Id={0}", userId), ex);
            }

            return archivedVideos;
        }

        public List<VideoTubePo> GetAllVideoTubeByChannelTubeId(int channelTubeId)
        {
            Contract.Requires(channelTubeId > 0, "ChannelTubeId should be greater then 0");

            var channelVideos = new List<VideoTubePo>();

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    channelVideos = this.StrimmDbConnection.Query<VideoTubePo>("strimm.GetAllVideoTubeByChannelTubeId", new { ChannelTubeId = channelTubeId }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve videos for channel Tube Id={0}", channelTubeId), ex);
            }

            return channelVideos;
        }

        public List<VideoTubePo> GetAllVideoTubeInUserChannelsByUserId(int userId)
        {
            Contract.Requires(userId > 0, "UserId should be greater then 0");

            var channelVideos = new List<VideoTubePo>();

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    channelVideos = this.StrimmDbConnection.Query<VideoTubePo>("strimm.GetAllVideoTubePosInChannelsByUserId", new { UserId = userId }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve videos that were added to all user's channels for user Id={0}", userId), ex);
            }

            return channelVideos;
        }

        public List<VideoTubePo> GetAllVideoTubeByVideoRoomTubeId(int videoRoomTubeId)
        {
            Contract.Requires(videoRoomTubeId > 0, "VideoRoomTubeId should be greater then 0");

            var channelVideos = new List<VideoTubePo>();

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    channelVideos = this.StrimmDbConnection.Query<VideoTubePo>("strimm.GetAllVideoTubeByVideoRoomTubeId", new { VideoRoomTubeId = videoRoomTubeId }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve videos for video room tube Id={0}", videoRoomTubeId), ex);
            }

            return channelVideos;
        }

        public bool AddVideoTubeToChannelTube(int channelTubeId, int videoTubeId)
        {
            Contract.Requires(channelTubeId > 0, "ChannelTubeId should be greater then 0");
            Contract.Requires(videoTubeId > 0, "VideoTubeId should be greater then 0");

            bool isSuccess = false;
            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    int rowcount = this.StrimmDbConnection.Execute("strimm.AddVideoTubeToChannelTube", new { ChannelTubeId = channelTubeId, VideoTubeId = videoTubeId }, null, 30, commandType: CommandType.StoredProcedure);
                    isSuccess = rowcount > 0;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to add videoTube with Id={0} to channelTube with Id={1}", videoTubeId, channelTubeId), ex);
            }

            return isSuccess;
        }

        public bool AddVideoTubeToVideoRoomTube(int videoRoomTubeId, int videoTubeId)
        {
            Contract.Requires(videoRoomTubeId > 0, "VideoRoomTubeId should be greater then 0");
            Contract.Requires(videoTubeId > 0, "VideoTubeId should be greater then 0");

            bool isSuccess = false;
            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    int rowcount = this.StrimmDbConnection.Execute("strimm.AddVideoTubeToVideoRoomTube", new { VideoRoomTubeId = videoRoomTubeId, VideoTubeId = videoTubeId }, null, 30, commandType: CommandType.StoredProcedure);
                    isSuccess = rowcount == 1;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to add videoTube with Id={0} to videoRoomTube with Id={1}", videoTubeId, videoRoomTubeId), ex);
            }

            return isSuccess;
        }

        public bool DeleteVideoTubeFromChannelTubeById(int channelTubeId, int videoTubeId)
        {
            Contract.Requires(channelTubeId > 0, "ChannelTubeId should be greater then 0");
            Contract.Requires(videoTubeId > 0, "VideoTubeId should be greater then 0");

            bool isSuccess = false;
            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    int rowcount = this.StrimmDbConnection.Execute("strimm.DeleteVideoTubeFromChannelTubeById", new { ChannelTubeId = channelTubeId, VideoTubeId = videoTubeId }, null, 30, commandType: CommandType.StoredProcedure);
                    isSuccess = rowcount == 1;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to delete videoTube with Id={0} from channelTube with Id={1}", videoTubeId, channelTubeId), ex);
            }

            return isSuccess;
        }

        public bool DeleteAllVideoTubesFromChannelTubeByChannelTubeId(int channelTubeId)
        {
            Contract.Requires(channelTubeId > 0, "ChannelTubeId should be greater then 0");

            bool isSuccess = false;
            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    int rowcount = this.StrimmDbConnection.Execute("strimm.DeleteAllVideoTubesFromChannelTubeByChannelTubeId", new { ChannelTubeId = channelTubeId }, null, 30, commandType: CommandType.StoredProcedure);
                    isSuccess = rowcount > 0;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to delete all videoTubes from channelTube with Id={0}", channelTubeId), ex);
            }

            return isSuccess;
        }

        public bool DeleteAllRestrictedOrRemovedVideosFromChannelByChannelTubeId(int channelTubeId)
        {
            Contract.Requires(channelTubeId > 0, "ChannelTubeId should be greater then 0");

            bool isSuccess = false;
            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    int rowcount = this.StrimmDbConnection.Execute("strimm.DeleteAllRestrictedOrRemovedVideosFromChannelByChannelTubeId", new { ChannelTubeId = channelTubeId }, null, 30, commandType: CommandType.StoredProcedure);
                    isSuccess = rowcount > 0;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to delete all restricted or removed videoTubes from channelTube with Id={0}", channelTubeId), ex);
            }

            return isSuccess;
        }

        public bool DeleteAllVideoTubesFromVideoRoomTubeByVideoRoomTubeId(int videoRoomTubeId)
        {
            Contract.Requires(videoRoomTubeId > 0, "VideoRoomTubeId should be greater then 0");

            bool isSuccess = false;
            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    int rowcount = this.StrimmDbConnection.Execute("strimm.DeleteAllVideoTubesFromVideoRoomTubeByChannelTubeId", new { VideoRoomTubeId = videoRoomTubeId }, null, 30, commandType: CommandType.StoredProcedure);
                    isSuccess = rowcount > 0;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to delete all videoTubes from videoRoomTube with Id={0}", videoRoomTubeId), ex);
            }

            return isSuccess;
        }


        public List<VideoTubePo> GetAllVideoTubeByProviderName(string providerName)
        {
            Contract.Requires(!String.IsNullOrEmpty(providerName), "Provider name is missing or invalid");

            var videos = new List<VideoTubePo>();

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    videos = this.StrimmDbConnection.Query<VideoTubePo>("strimm.GetAllVideoTubeByProviderName", new { VideoProviderName = providerName }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve videos for provider '{0}'", providerName), ex);
            }

            return videos;
        }

        public List<VideoTubeCounterEntity> GetVideoTubeCountsByCategory()
        {
            var counters = new List<VideoTubeCounterEntity>();

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    counters = this.StrimmDbConnection.Query<VideoTubeCounterEntity>("strimm.GetVideoTubeCountsByCategory", null, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to retrieve video tube counters for all categories", ex);
            }

            return counters;
        }

        public List<VideoTubeCounterEntity> GetVideoTubeCountsByCategoryAndChannelTubeId(int channelTubeId)
        {
            var counters = new List<VideoTubeCounterEntity>();

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    counters = this.StrimmDbConnection.Query<VideoTubeCounterEntity>("strimm.GetVideoTubeCountsByCategoryAndChannelTubeId", new { ChannelTubeId = channelTubeId }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve video tube counters for all categories in channel with Id={0}", channelTubeId), ex);
            }

            return counters;
        }

        public List<VideoTubeCounterEntity> GetVideoTubeCountsByCategoryAndVideoRoomTubeId(int videoRoomTubeId)
        {
            var counters = new List<VideoTubeCounterEntity>();

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    counters = this.StrimmDbConnection.Query<VideoTubeCounterEntity>("strimm.GetVideoTubeCountsByCategoryAndVideoRoomTubeId", new { VideoRoomTubeId = videoRoomTubeId }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve video tube counters for all categories in video room tube with Id={0}", videoRoomTubeId), ex);
            }

            return counters;
        }

        public List<VideoTubeCounterEntity> GetVideoTubeCountsInPublicLibraryByCategory()
        {
            var counters = new List<VideoTubeCounterEntity>();

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    counters = this.StrimmDbConnection.Query<VideoTubeCounterEntity>("strimm.GetVideoTubeCountsInPublicLibraryByCategory", null, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to retrieve public video tube counters for all categories", ex);
            }

            return counters;
        }

        public bool CheckIfVideoTubeExistsInVideoRoomTubeByUserIdAndProviderVideoId(int userId, string providerVideoId)
        {
            Contract.Requires(userId > 0, "UserId should be greater then 0");

            bool exists = false;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    exists = this.StrimmDbConnection.ExecuteScalar<bool>("strimm.CheckIfVideoTubeExistsInVideoRoomTubeByUserIdAndProviderVideoId", new { UserId = userId, ProviderVideoId = providerVideoId }, null, 30, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to determine if video '{0}' exists in video room for user Id={1}", providerVideoId, userId), ex);
            }

            return exists;
        }

        public bool DeleteVideoTubeFromUserArchiveByVideoTubeIdAndUserId(int videoTubeId, int userId)
        {
            Contract.Requires(videoTubeId > 0, "Invalid videoTubeId specified");
            Contract.Requires(userId > 0, "Invalid user id specified");

            bool isSuccess = false;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    int rowcount = this.StrimmDbConnection.Execute("strimm.RemoveVideoTubeFromUserArchiveByVideoTubeIdAndUserId", new { UserId = userId, VideoTubeId = videoTubeId }, null, 30, commandType: CommandType.StoredProcedure);
                    isSuccess = rowcount == 1;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to remove videoTube with Id={0} from archive for user with Id={1}", videoTubeId, userId), ex);
            }

            return isSuccess;
        }

        public bool DeleteVideoTubeFromVideoRoomByVideoTubeIdAndUserId(int videoTubeId, int userId, bool isAdmin = false)
        {
            Contract.Requires(videoTubeId > 0, "Invalid videoTubeId specified");
            Contract.Requires(userId > 0, "Invalid user id specified");

            bool isSuccess = false;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    int rowcount = this.StrimmDbConnection.Execute("strimm.DeleteVideoTubeFromVideoRoomByVideoTubeIdAndUserId", new { UserId = userId, VideoTubeId = videoTubeId, IsAdmin = isAdmin }, null, 30, commandType: CommandType.StoredProcedure);
                    isSuccess = rowcount > 0;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to remove videoTube with Id={0} from video room for user with Id={1}", videoTubeId, userId), ex);
            }

            return isSuccess;
        }

        public bool InsertUserVideoTubeViewByUserIdAndVideoTubeId(int videoTubeId, int? userId, DateTime? viewStartTime, DateTime? viewEndTime)
        {
            Logger.Info(String.Format("Recording viewing occurance for video with Id={0} for user with Id={1} that occured from '{2}' till '{3}'", videoTubeId, userId, viewStartTime.ToString(), viewEndTime != null ? viewEndTime.ToString() : "[Not Specified]"));

            bool isSuccess = false;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    int rowcount = this.StrimmDbConnection.Execute("strimm.InsertUserVideoTubeViewByUserIdAndVideoTubeId", new
                    {
                        VideoTubeId = videoTubeId,
                        UserId = userId,
                        ViewStartTime = viewStartTime,
                        ViewEndTime = viewEndTime
                    }, 
                    null, 30, commandType: CommandType.StoredProcedure);
                    isSuccess = rowcount == 1;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to insert userVideoTubeView for video with Id={0} for user with Id={1} that occured from '{2}' till '{3}'", videoTubeId, userId, viewStartTime.ToString(), viewEndTime.ToString()), ex);
            }

            return isSuccess;
        }

        public List<VideoTubePo> GetPublicVideoTubesByPageIndex(int pageIndex, string keywords, out int pageCount, int pageSize = 10)
        {
            Logger.Info(String.Format("Retrieving page {0} of videos in public library, {1} videos per page", pageIndex, pageSize));

            List<VideoTubePo> publicVideos = new List<VideoTubePo>();

            int count = 0;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    var dynamicParameter = new DynamicParameters();
                    dynamicParameter.Add("@PageIndex", pageIndex);
                    dynamicParameter.Add("@PageSize", pageSize);
                    dynamicParameter.Add("@Keywords", keywords);
                    dynamicParameter.Add("@PageCount", null, DbType.Int32, ParameterDirection.Output);

                    publicVideos = this.StrimmDbConnection.Query<VideoTubePo>("strimm.GetPublicVideoTubesByPageIndex", dynamicParameter, null, false, 30, commandType: CommandType.StoredProcedure).ToList();

                    count = dynamicParameter.Get<int>("@PageCount");
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve page {0} of public videos. Page size set to {1}", pageIndex, pageSize), ex);
            }

            pageCount = count;

            return publicVideos;
        }

        public VideoTubePo GetVideoTubePoById(int videoTubeId)
        {
            Logger.Info(String.Format("Retrieving video tube projection by Id={0}", videoTubeId));

            VideoTubePo videoTube = null;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    var results = this.StrimmDbConnection.Query<VideoTubePo>("strimm.GetVideoTubePoById", new { VideoTubeId = videoTubeId }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                    videoTube = results.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve video tube projection with Id={0}", videoTubeId), ex);
            }

            return videoTube;
        }

        public List<VideoTubeModel> GetVideoTubePoByChannelTubeIdAndCategoryIdAndPageIndex(int channelTubeId, int categoryId, int pageIndex, string keywords, out int pageCount, int pageSize = 10)
        {
            Logger.Info(String.Format("Retrieving page {0} of videos for channel with Id={1} and category id={2}, {3} videos per page", pageIndex, channelTubeId, categoryId, pageSize));

            List<VideoTubeModel> publicVideos = new List<VideoTubeModel>();

            int count = 0;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    var dynamicParameter = new DynamicParameters();
                    dynamicParameter.Add("@ChannelTubeId", channelTubeId);
                    dynamicParameter.Add("@CategoryId", categoryId);
                    dynamicParameter.Add("@PageIndex", pageIndex);
                    dynamicParameter.Add("@PageSize", pageSize);
                    dynamicParameter.Add("@Keywords", keywords);
                    dynamicParameter.Add("@PageCount", null, DbType.Int32, ParameterDirection.Output);

                    publicVideos = this.StrimmDbConnection.Query<VideoTubeModel>("strimm.GetVideoTubePoByChannelTubeIdAndCategoryIdAndPageIndex", dynamicParameter, null, false, 30, commandType: CommandType.StoredProcedure).ToList();

                    count = dynamicParameter.Get<int>("@PageCount");
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve page {0} of videos for channel with Id={1} and category id={2}, {3} videos per page", pageIndex, channelTubeId, categoryId, pageSize), ex);
            }

            pageCount = count;

            return publicVideos;
        }

        public List<VideoTubeModel> GetVideoTubePoByChannelTubeIdAndPageIndex(int channelTubeId, int pageIndex, string keywords, out int pageCount, int pageSize = 10)
        {
            Logger.Info(String.Format("Retrieving page {0} of videos for channel with Id={1}, {2} videos per page", pageIndex, channelTubeId, pageSize));

            List<VideoTubeModel> publicVideos = new List<VideoTubeModel>();

            int count = 0;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    var dynamicParameter = new DynamicParameters();
                    dynamicParameter.Add("@ChannelTubeId", channelTubeId);
                    dynamicParameter.Add("@PageIndex", pageIndex);
                    dynamicParameter.Add("@PageSize", pageSize);
                    dynamicParameter.Add("@Keywords", keywords);
                    dynamicParameter.Add("@PageCount", null, DbType.Int32, ParameterDirection.Output);

                    publicVideos = this.StrimmDbConnection.Query<VideoTubeModel>("strimm.GetVideoTubePoByChannelTubeIdAndPageIndex", dynamicParameter, null, false, 30, commandType: CommandType.StoredProcedure).ToList();

                    count = dynamicParameter.Get<int>("@PageCount");
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve page {0} of videos for channel with Id={1}, {2} videos per page", pageIndex, channelTubeId, pageSize), ex);
            }

            pageCount = count;

            return publicVideos;
        }

        public bool RemoveAllVideosFromChannelTubeByChannelTubeId(int channelTubeId)
        {
            Contract.Requires(channelTubeId > 0, "ChannelTubeId should be greater then 0");

            bool isSuccess = true;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    int rowcount = this.StrimmDbConnection.Execute("strimm.DeleteAllVideoTubesFromChannelTubeByChannelTubeId", new { ChannelTubeId = channelTubeId }, null, 30, commandType: CommandType.StoredProcedure);
                    isSuccess = rowcount > 0;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to delete all videoTubes for channelTube with Id={0}", channelTubeId), ex);
            }

            return isSuccess;
        }

        public bool DeleteRestrictedOrRemovedVideosFromChannelTubeByChannelTubeId(int channelTubeId, List<int> videoIds)
        {
            Contract.Requires(channelTubeId > 0, "ChannelTubeId should be greater then 0");
            Contract.Requires(videoIds != null && videoIds.Count > 0, "No videos specified for deletion");

            bool isSuccess = false;
            var builder = new StringBuilder();

            videoIds.ForEach(x => builder.Append(x).Append(','));

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    int rowcount = this.StrimmDbConnection.Execute("strimm.DeleteRestrictedOrRemovedVideosFromChannelTubeByChannelTubeId", new { ChannelTubeId = channelTubeId, VideoIds = builder.ToString().TrimEnd(',') }, null, 30, commandType: CommandType.StoredProcedure);
                    isSuccess = rowcount > 0;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to delete {0} restricted or removed videoTubes for channelTube with Id={1}",  videoIds.Count, channelTubeId), ex);
            }

            return isSuccess;
        }

        public List<VideoSchedule> DeleteVideoTubeFromChannelScheduleById(int channelScheduleId, int videoTubeId)
        {
            Contract.Requires(channelScheduleId > 0, "Channel schedule Id should be greater then 0");
            Contract.Requires(videoTubeId > 0, "Video Tube Id should be greater then 0");

            List<VideoSchedule> videoSchedules = new List<VideoSchedule>();

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    videoSchedules = this.StrimmDbConnection.Query<VideoSchedule>("strimm.DeleteVideoTubeFromChannelScheduleByIdWithGet",
                                new { ChannelScheduleId = channelScheduleId, VideoTubeId = videoTubeId },
                                null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to delete video Tube with Id={0} to channel schedule with Id={1}", videoTubeId, channelScheduleId), ex);
            }

            return videoSchedules;

        }

        public List<VideoSchedule> AddVideoTubeToChannelScheduleById(int channelScheduleId, int videoTubeId)
        {
            Contract.Requires(channelScheduleId > 0, "Channel schedule Id should be greater then 0");
            Contract.Requires(videoTubeId > 0, "Video Tube Id should be greater then 0");
            
            List<VideoSchedule> videoSchedules = new List<VideoSchedule>();

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    videoSchedules = this.StrimmDbConnection.Query<VideoSchedule>("strimm.AddVideoTubeToChannelScheduleWithGet",
                                new { ChannelScheduleId = channelScheduleId, VideoTubeId = videoTubeId }, 
                                null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to add video Tube with Id={0} to channel schedule with Id={1}", videoTubeId, channelScheduleId), ex);
            }

            return videoSchedules;

        }

        public List<VideoSchedule> AddMultipleVideoTubesToChannelScheduleById(int channelScheduleId, List<int> videosAvailableToBeScheduled)
        {
            Contract.Requires(channelScheduleId > 0, "ChannelTubeId should be greater then 0");
            Contract.Requires(videosAvailableToBeScheduled != null && videosAvailableToBeScheduled.Count > 0, "No videos specified");

            List<VideoSchedule> videoSchedules = new List<VideoSchedule>();

            try
            {
                var builder = new StringBuilder();
                videosAvailableToBeScheduled.ForEach(x => builder.Append(x).Append(','));

                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    videoSchedules = this.StrimmDbConnection.Query<VideoSchedule>("strimm.AddMultipleVideoTubesToChannelScheduleById",
                                new { ChannelScheduleId = channelScheduleId, VideoIds = builder.ToString().TrimEnd(',') }, 
                                null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to add {0} videos to channel schedule with Id={1}", videosAvailableToBeScheduled.Count, channelScheduleId), ex);
            }

            return videoSchedules;
        }

        public int DeleteMultipleVideosFromVideoTubeRoomByUserName(List<int> videoTubeIds, string userName)
        {
            Contract.Requires(!String.IsNullOrEmpty(userName), "Invalid username specified");
            Contract.Requires(videoTubeIds != null && videoTubeIds.Count > 0, "No videos specified for deletion");

            var builder = new StringBuilder();

            videoTubeIds.ForEach(x => builder.Append(x).Append(','));

            int videoCount = 0;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    videoCount = this.StrimmDbConnection.Execute("strimm.DeleteMultipleVideosFromVideoTubeRoomByUserName", 
                        new { VideoTubeIds = builder.ToString().TrimEnd(','), UserName = userName }, null, 30, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to delete {0} restricted or removed videoTubes from video room for user {1}", videoTubeIds.Count, userName), ex);
            }

            return videoCount;
        }

        public int MoveUserPrivateVideosToPublicLibraryByUsername(string userName)
        {
            Contract.Requires(!String.IsNullOrEmpty(userName), "Invalid username specified");

            int countOfVideosAddedToPublicLibrary = 0;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    countOfVideosAddedToPublicLibrary = this.StrimmDbConnection.Execute("strimm.MoveUserPrivateVideosToPublicLibraryByUsername",
                                new { Username = userName },
                                null, 30, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to move videos to public library for user '{0}'", userName), ex);
            }

            return countOfVideosAddedToPublicLibrary;
        }

        public int DeleteMultipleVideosFromPublicLibrary(List<int> videoTubeIds)
        {
            Contract.Requires(videoTubeIds != null && videoTubeIds.Count > 0, "No videos specified for deletion");

            var builder = new StringBuilder();

            videoTubeIds.ForEach(x => builder.Append(x).Append(','));

            int videoCount = 0;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    videoCount = this.StrimmDbConnection.Execute("strimm.DeleteMultipleVideosFromPublicLibrary", new { VideoTubeIds = builder.ToString().TrimEnd(',') }, null, 30, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to delete {0} restricted or removed videoTubes from Public Library", videoTubeIds.Count), ex);
            }

            return videoCount;
        }

        public bool DeleteArchivedVideoByVideotubeIdAndUserId(int userId, int videoTubeId)
        {
            bool isSuccess = false;
                    
            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    int rowcount = this.StrimmDbConnection.Execute("strimm.DeleteArchivedVideoByVideotubeIdAndUserId", new { UserId =userId, VideoTubeId =videoTubeId }, null, 30, commandType: CommandType.StoredProcedure);
                    isSuccess = rowcount > 0;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to delete videotubeId {0} from archive for user Id={1} ", videoTubeId, userId), ex);
            }

            return isSuccess;
        }

        public CustomVideoTubeUploadPo InitializeVideoTubeUploadForUser(int userId, string fileName, string videoTubeStagingKey, float duration, DateTime clientTime)
        {
            CustomVideoTubeUploadPo videoUpload = null; ;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    var results = this.StrimmDbConnection.Query<CustomVideoTubeUploadPo>("strimm.InitializeUploadedVideoForUser",
                                new { 
                                        UserId = userId,
                                        VideoTubeStagingKey = videoTubeStagingKey,
                                        Duration = duration,
                                        CreatedDateTime = clientTime,
                                        FileName = fileName
                                },
                                null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                    videoUpload = results.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to initialize user uploaded video '{0}' for user id={1} of duration={2} sec on '{3}'", videoTubeStagingKey, userId, duration, clientTime.ToString()), ex);
            }

            return videoUpload;
        }

        public bool UpdateCustomVideoTube(int videoTubeId, string title, string keywords, string description, bool isPrivate, bool isRRated, int categoryId)
        {
            bool isSuccess = false;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    int row = this.StrimmDbConnection.Execute("strimm.UpdateUploadedVideo",
                                new
                                {
                                    VideoTubeId = videoTubeId,
                                    Title = title,                                    
                                    Keywords = keywords,
                                    Description = description,
                                    IsPrivate = isPrivate,
                                    IsRRated = isRRated,
                                    CategoryId = categoryId
                                },
                                null, 30, commandType: CommandType.StoredProcedure);
                    isSuccess = row > 0;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to update user uploaded video id='{0}'", videoTubeId), ex);
            }

            return isSuccess;
        }

        public List<VideoTubeThumbnail> GetThumbnailsByVideoId(int videoTubeId)
        {
            List<VideoTubeThumbnail> thumbnails = null; ;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    thumbnails = this.StrimmDbConnection.Query<VideoTubeThumbnail>("strimm.GetVideoTubeThumbnailsByVideoId",
                                new
                                {
                                    VideoTubeId = videoTubeId
                                },
                                null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve thumbnails for video id='{0}'", videoTubeId), ex);
            }

            return thumbnails;
        }

        public bool InsertVideoTubeThumbnail(VideoTubeThumbnail thumbnail)
        {
            bool isSuccess = false;
           
            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    int rowcount = this.StrimmDbConnection.Execute("strimm.InsertVideoTubeThumbnail", new
                                {
                                    VideoTubeId = thumbnail.VideoTubeId,
                                    ThumbnailKey = thumbnail.ThumbnailKey,
                                    IsActive = thumbnail.IsActive,
                                    Position = thumbnail.Position
                                }, null, 30, commandType: CommandType.StoredProcedure);
                    isSuccess = rowcount == 1;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to insert video tube thumbnail with key={0} for video id={1}", thumbnail.ThumbnailKey, thumbnail.VideoTubeId), ex);
            }

            return isSuccess;
        }
            
        public bool UpdateVideoTubeThumbnail(VideoTubeThumbnail thumbnail)
        {
            bool isSuccess = false;
            VideoTubeThumbnail thumbToUpdate = null;
            if(thumbnail.VideoTubeThumbnailId==null || thumbnail.VideoTubeThumbnailId==0)
            {
                thumbToUpdate = GetThumbnailsByVideoId(thumbnail.VideoTubeId).First(x => x.IsActive == true);
            }
            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    int rowcount = this.StrimmDbConnection.Execute("strimm.UpdateVideoTubeThumbnail", new
                                {
                                    VideoTubeThumbnailId = thumbToUpdate.VideoTubeThumbnailId,
                                    VideoTubeId = thumbnail.VideoTubeId,
                                    ThumbnailKey = thumbnail.ThumbnailKey,
                                    IsActive = thumbnail.IsActive,
                                    Position = thumbnail.Position
                                }, null, 30, commandType: CommandType.StoredProcedure);
                    isSuccess = rowcount == 1;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to update video tube thumbnail with Id={0}", thumbnail.VideoTubeThumbnailId), ex);
            }

            return isSuccess;
        }

        public VideoTubePreviewClip GetVideoTubePreviewClipByVideoTubeId(int videoTubeId)
        {
            VideoTubePreviewClip previewClip = null; ;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    var results = this.StrimmDbConnection.Query<VideoTubePreviewClip>("strimm.GetVideoTubePreviewClipByVideoTubeId",
                                new
                                {
                                    VideoTubeId = videoTubeId
                                },
                                null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                    previewClip = results.FirstOrDefault(); 
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve preview clip for video id='{0}'", videoTubeId), ex);
            }

            return previewClip;
        }

        public bool InsertVideoTubePreviewClip(VideoTubePreviewClip videoTubePreviewClip)
        {
            bool isSuccess = false;
           
            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    int row = this.StrimmDbConnection.Execute("strimm.InsertVideoTubePreviewClip", new
                                {
                                    VideoTubeId = videoTubePreviewClip.VideoTubeId,
                                    PreviewClipKey = videoTubePreviewClip.PreviewClipKey,
                                    Duration = videoTubePreviewClip.Duration,
                                    IsAutoGenerated = videoTubePreviewClip.IsAutoGenerated
                                },
                                null, 30, commandType: CommandType.StoredProcedure);
                    isSuccess = row > 0;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to insert video preview clip for video id='{0}', clip {1}", videoTubePreviewClip.VideoTubeId, videoTubePreviewClip.PreviewClipKey), ex);
            }

            return isSuccess;
        }
            
        public bool UpdateVideoTubeLastScheduleDateTimeByChannelScheduleId(int channelScheduleId)
        {
            bool isSuccess = false;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    int rowcount = this.StrimmDbConnection.Execute("strimm.UpdateVideoTubeLastScheduleDateTimeByChannelScheduleId", new { ChannelScheduleId = channelScheduleId }, null, 30, commandType: CommandType.StoredProcedure);
                    isSuccess = rowcount > 0;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to update last schedule time on videos for schedule with id = {0} ", channelScheduleId), ex);
            }

            return isSuccess;
        }

        public bool UpdateVideoTubePreviewClip(VideoTubePreviewClip existingClip)
        {
            bool isSuccess = false;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    int row = this.StrimmDbConnection.Execute("strimm.UpdateVideoTubePreviewClip", new
                    {
                        VideoTubePreviewClipId = existingClip.VideoTubePreviewClipId,
                        VideoTubeId = existingClip.VideoTubeId,
                        PreviewClipKey = existingClip.PreviewClipKey,
                        Duration = existingClip.Duration,
                        IsAutoGenerated = existingClip.IsAutoGenerated
                    }, null, 30, commandType: CommandType.StoredProcedure);
                    isSuccess = row > 0;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to update video preview clip for video id='{0}', clip id={1}", existingClip.VideoTubeId, existingClip.VideoTubePreviewClipId), ex);
            }

            return isSuccess;
        }

        public CustomVideoTubeUploadPo GetCustomVideoTubeById(int videoTubeId)
        {
            Logger.Info(String.Format("Retrieving custom video using id={0}", videoTubeId));

            CustomVideoTubeUploadPo video = null; ;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    var results = this.StrimmDbConnection.Query<CustomVideoTubeUploadPo>("strimm.GetCustomVideoTubeById",
                                new
                                {
                                    VideoTubeId = videoTubeId
                                },
                                null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                    video = results.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve custom video with id='{0}'", videoTubeId), ex);
            }

            return video;
        }

        public List<VideoTubePo> GetUserUploadedVideoTubesByPageIndex(VideoStoreVideoSearchCriteria searchCriteria, out int pageCount, int pageSize)
        {
            var videoTubesPos = new List<VideoTubePo>();
            int count = 0;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    var dynamicParameter = new DynamicParameters();
                    dynamicParameter.Add("@PageIndex", searchCriteria.PageIndex);
                    dynamicParameter.Add("@PageSize", pageSize);
                    dynamicParameter.Add("@OwnerUsernameKeywords", searchCriteria.OwnerUsernameKeyword);
                    dynamicParameter.Add("@VideoContentKeywords", searchCriteria.VideoContentKeywords);
                    dynamicParameter.Add("@PageCount", null, DbType.Int32, ParameterDirection.Output);

                    videoTubesPos = this.StrimmDbConnection.Query<VideoTubePo>("strimm.GetUserUploadedVideoTubesByPageIndex", dynamicParameter, null, false, 30, commandType: CommandType.StoredProcedure).ToList();

                    count = dynamicParameter.Get<int>("@PageCount");
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve videos in video store for page Id={0}", searchCriteria.PageIndex), ex);
            }

            pageCount = count;

            return videoTubesPos;
        }

        public List<VideoTubePo> GetUserUploadedVideoTubesByCategoryIdAndPageIndex(VideoStoreVideoSearchCriteria searchCriteria, out int pageCount, int pageSize)
        {
            var videoTubesPos = new List<VideoTubePo>();
            int count = 0;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    var dynamicParameter = new DynamicParameters();
                    dynamicParameter.Add("@CategoryId", searchCriteria.CategoryId);
                    dynamicParameter.Add("@PageIndex", searchCriteria.PageIndex);
                    dynamicParameter.Add("@PageSize", pageSize);
                    dynamicParameter.Add("@OwnerUsernameKeywords", searchCriteria.OwnerUsernameKeyword);
                    dynamicParameter.Add("@VideoContentKeywords", searchCriteria.VideoContentKeywords);
                    dynamicParameter.Add("@PageCount", null, DbType.Int32, ParameterDirection.Output);

                    videoTubesPos = this.StrimmDbConnection.Query<VideoTubePo>("strimm.GetUserUploadedVideoTubesByCategoryIdAndPageIndex", dynamicParameter, null, false, 30, commandType: CommandType.StoredProcedure).ToList();

                    count = dynamicParameter.Get<int>("@PageCount");
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve videos in video store for category Id={0}, page index={1}", searchCriteria.CategoryId, searchCriteria.PageIndex), ex);
            }

            pageCount = count;

            return videoTubesPos;
        }


        public List<VideoTubeModel> RetrievePageOfVideosToUpdate()
        {
            List<VideoTubeModel> videos = new List<VideoTubeModel>();

            try
            {

                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    videos = this.StrimmDbConnection.Query<VideoTubeModel>("strimm.GetVideoTubesForStatusUpdate", null, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to retrieve videotubes to update");
            }

            return videos;
        }

        public bool UpdateVideoTubeStatusById(int videoTubeId, bool isPrivate, bool isRestrictedByProvider, bool isRemovedByProvider, bool isMatureContent)
        {
            bool isSuccess = false;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    int rowcount = this.StrimmDbConnection.Execute("strimm.UpdateVideoTubeStatusById", new
                    {
                        VideoTubeId = videoTubeId,
                        IsPrivate = isPrivate,
                        IsRestrictedByProvider = isRestrictedByProvider,
                        IsRemovedByProvider = isRemovedByProvider,
                        IsRRated = isMatureContent
                    }, null, 30, commandType: CommandType.StoredProcedure);

                    isSuccess = rowcount == 1;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to update status of video tube with videoTubeId = {0}", videoTubeId), ex);
            }

            return isSuccess;
        }

        public VideoTubeModel InsertPrivateVideoForChannel(string description, string title, string videoUrl, int categoryId, bool isMatureContent, double durationInSec, int providerId)
        {
            VideoTubeModel video = null;

            try
            {
                
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    var videos = this.StrimmDbConnection.Query<VideoTubeModel>("strimm.InsertPrivateVideoTube", new
                    {
                        Title = title,
                        Description = description,
                        ProviderVideoId = videoUrl,
                        Duration = durationInSec,
                        CategoryId = categoryId,
                        IsRRated = isMatureContent,
                        VideoProviderId = providerId
                    }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                    video = videos.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to retrieve videotubes to update");
            }

            return video;
        }

        public VideoTubeModel UpdatePrivateVideoTube(int videoTubeId, int providerId, string title, string description, string videoUrl, int categoryId, bool isMatureContent, double durationInSec)
        {
            VideoTubeModel video = null;

            try
            {

                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    var videos = this.StrimmDbConnection.Query<VideoTubeModel>("strimm.UpdatePrivateVideoTubeById", new
                    {
                        VideoTubeId = videoTubeId,
                        Title = title,
                        Description = description,
                        ProviderVideoId = videoUrl,
                        Duration = durationInSec,
                        CategoryId = categoryId,
                        IsRRated = isMatureContent,
                        VideoProviderId = providerId
                    }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                    video = videos.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to retrieve videotubes to update", ex);
            }

            return video;
        }

        public VideoLiveTubePo InsertVideoLiveTubeWithGet(string title, string description, string providerVideoId, int categoryId, int videoProviderId, bool isRRated, string thumbnail, DateTime startDate, DateTime? endDate, int userId)
        {
            Logger.Debug(String.Format("Inserting new live video with title='{0}', description='{1}', provider video id='{2}', category id={3}, video provider id={4}, isRRated={5}, thumbnail='{6}', start date='{7}', end date='{8}'",
                title, description, providerVideoId, categoryId, videoProviderId, isRRated, thumbnail, startDate.ToString("yyyy-MM-dd"), endDate != null ? endDate.Value.ToString("yyyy-MM-dd") : "NOT SET"));

            VideoLiveTubePo video = null;

            try
            {

                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    var results = this.StrimmDbConnection.Query<VideoLiveTubePo>("strimm.InsertVideoLiveTubeWithGet", new
                    {
                        Title = title,
                        Description = description,
                        ProviderVideoId = providerVideoId,
                        CategoryId = categoryId,
                        VideoProviderId = videoProviderId,
                        IsRRated = isRRated,
                        Thumbnail = thumbnail,
                        StartTime = startDate,
                        EndTime = endDate,
                        UserId= userId
                    }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                    video = results.FirstOrDefault();
                }
            }
             catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to add live video with title='{0}'", title), ex);
            }

            return video;
        }

        /// <summary>
        /// This method will add live video record to a specific channel by id
        /// </summary>
        /// <param name="channelTubeId">Id of the target channel record</param>
        /// <param name="videoLiveTubeId">Id of the live video to add to the channel</param>
        /// <returns>True/False</returns>
        public bool AddVideoLiveTubeToChannelTube(int channelTubeId, int videoLiveTubeId)
        {
            Logger.Debug(String.Format("Adding live video with id={0} to channel with id={1}", videoLiveTubeId, channelTubeId));

            bool isSuccess = false;

            try
            {

                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    var rowcount = this.StrimmDbConnection.Execute("strimm.AddVideoLiveTubeToChannelById", new
                    {
                        ChannelTubeId = channelTubeId,
                        VideoLiveTubeId = videoLiveTubeId
                    }, null, 30, commandType: CommandType.StoredProcedure);
                    isSuccess = rowcount == 1;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to add live video with id={0} to channel with id={1}", videoLiveTubeId, channelTubeId));
            }

            return isSuccess;
        }

        /// <summary>
        /// This method will retrieve VideoLiveTubePo object by id
        /// </summary>
        /// <param name="videoLiveTubeId">Id of the live video</param>
        /// <returns>Instance of <see cref="VideoLiveTubePo"/></returns>
        public VideoLiveTubePo GetVideoLiveTubePoById(int videoLiveTubeId)
        {
            Logger.Debug(String.Format("Retrieving live video with id={0}", videoLiveTubeId));

            VideoLiveTubePo video = null;

            try
            {

                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    var results = this.StrimmDbConnection.Query<VideoLiveTubePo>("strimm.GetVideoLiveTubePoById", new
                    {
                        VideoLiveTubeId = videoLiveTubeId
                    }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                    video = results.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve live video with id={0}", videoLiveTubeId));
            }

            return video;
        }
        
        /// <summary>
        /// This method will let user to delete a live video from Channel Tube by id
        /// and channel tube id
        /// </summary>
        /// <param name="channelTubeId">Id of the channel from which live video should be deleted</param>
        /// <param name="videoLiveTubeId">Id of the live video to be deleted</param>
        /// <returns>True/False</returns>
        public bool DeleteVideoLiveTubeFromChannelTubeById(int channelTubeId, int videoLiveTubeId)
        {
            Logger.Debug(String.Format("Removing live video with id={0} from channel with id={1}", videoLiveTubeId, channelTubeId));

            bool isSuccess = false;

            try
            {

                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    var rowcount = this.StrimmDbConnection.Execute("strimm.DeleteVideoLiveTubeFromChannelById", new
                    {
                        ChannelTubeId = channelTubeId,
                        VideoLiveTubeId = videoLiveTubeId
                    }, null, 30, commandType: CommandType.StoredProcedure);
                    isSuccess = rowcount == 1;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to delete live video with id={0} from channel with id={1}", videoLiveTubeId, channelTubeId));
            }

            return isSuccess;
        }

        /// <summary>
        /// This method will update start time and end time for an existing live video and will return an updated
        /// video object to caller.
        /// </summary>
        /// <param name="videoLiveTubeId">Id of the live video tube record that should be updated</param>
        /// <param name="startTime">New start time</param>
        /// <param name="endTime">New end time</param>
        /// <returns>Instance of <see cref="VideoLiveTubePo"/></returns>
        public VideoLiveTubePo UpdateVideoLiveTubeById(int videoLiveTubeId, DateTime startTime, DateTime? endTime)
        {
            Logger.Debug(String.Format("Updating live video with id={0} to start at '{1}' and end on '{2}'", videoLiveTubeId, startTime.ToString("yyyy-MM-dd"), endTime != null ? endTime.Value.ToString("yyyy-MM-dd") : "NOT SET"));

            VideoLiveTubePo video = null;

            try
            {

                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    var results = this.StrimmDbConnection.Query<VideoLiveTubePo>("strimm.UpdateVideoLiveTubeById", new
                    {
                        VideoLiveTubeId = videoLiveTubeId,
                        StartTime = startTime,
                        EndTime = endTime
                    }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                    video = results.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to update live video with id={0} with start date='{1}' and end date='{2}'", videoLiveTubeId, startTime.ToString("yyyy-MM-dd"), endTime != null ? endTime.Value.ToString("yyyy-MM-dd") : "NOT SET"));
            }

            return video;
        }

        /// <summary>
        /// This method will retrieve all live videos for a specific channel
        /// </summary>
        /// <param name="channelTubeId">Channel Id</param>
        /// <param name="targetDate">Target date for which live events should be retrieved</param>
        /// <returns>Collection of <see cref="VideoLiveTubePo"/></returns>
        public List<VideoLiveTubePo> GetAllVideoLiveTubePosByChannelIdAndDate(int channelTubeId, DateTime targetDate)
        {
            Logger.Debug(String.Format("Retrieving live videos for channel with id={0} and date='{1}'", channelTubeId, targetDate.ToString("MM/dd/yyyy")));

            List<VideoLiveTubePo> videos = null;

            try
            {

                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    videos = this.StrimmDbConnection.Query<VideoLiveTubePo>("strimm.GetAllVideoLiveTubePosByChannelIdAndDate", new
                    {
                        ChannelTubeId = channelTubeId,
                        TargetDate = targetDate.ToString("yyyy-MM-dd")
                    }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve live videos for channel with id={0} and date='{1}'", channelTubeId, targetDate.ToString("MM/dd/yyyy")));
            }

            return videos;
        }

        public VideoTubeModel GetPrivateVideoByProviderIdAndChannelId(string providerId, int channelTubeId)
        {
            Contract.Requires(channelTubeId > 0, "ChannelTubeId should be greater then 0");

            var video = new VideoTubeModel();

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    var results = this.StrimmDbConnection.Query<VideoTubeModel>("strimm.GetPrivateVideoByProviderIdAndChannelId", new { ProviderId = providerId, ChannelTubeId = channelTubeId }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                    video = results.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve videos for channel Tube Id={0}", channelTubeId), ex);
            }

            return video;
        }
    }
}
