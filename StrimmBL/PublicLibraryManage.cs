using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Strimm.Model;
using Strimm.Data.Repositories;
using System.Diagnostics.Contracts;
using Strimm.Model.Projections;
using Strimm.Model.WebModel;
using Strimm.Shared;
using System.Configuration;
using System.Collections.Concurrent;
using Strimm.Model.Criteria;

namespace StrimmBL
{
    public static class PublicLibraryManage
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(PublicLibraryManage));

        public static void AddVideoTubeToPublicLibrary(VideoTube videoTube)
        {
            Contract.Requires(videoTube != null, "Invalid videoTube specified");

            Logger.Info(String.Format("Adding video Tube '{0}' to a public library", videoTube.Title));

           using(var videoTubeRepository = new VideoTubeRepository())
           {
               if (videoTube.VideoTubeId > 0)
               {
                   videoTube.IsInPublicLibrary = true;
                   videoTubeRepository.UpdateVideoTube(videoTube);
               }
               else
               {
                   if (videoTubeRepository.InsertVideoTube(videoTube.Title, videoTube.Description, videoTube.ProviderVideoId, videoTube.Duration,
                                                       videoTube.CategoryId, videoTube.VideoProviderId, videoTube.IsRRated, videoTube.IsInPublicLibrary, videoTube.IsPrivate))
                   {
                       Logger.Debug(String.Format("Successfully added videoTube Titled='{}' with ProviderVideoId={1} for provider Id='{2}' to public library", videoTube.Title, videoTube.ProviderVideoId, videoTube.VideoProviderId));
                   }
                   else
                   {
                       Logger.Debug(String.Format("Failed to add videoTube Titled='{}' with ProviderVideoId={1} for provider Id='{2}' to public library", videoTube.Title, videoTube.ProviderVideoId, videoTube.VideoProviderId));
                   }
               }
           }

            
        }

        public static bool CheckIfVideoTubeExists(string providerVideoId)
        {
            Logger.Info(String.Format("Checking if video tube exists by ProviderVideoId '{0}'", providerVideoId));
            bool isExist = false;
            using (var videoTubeRepository = new VideoTubeRepository())
            {
                var videoTube = videoTubeRepository.GetVideoTubeByProviderVideoId(providerVideoId);
                isExist = videoTube != null;
            }
            
            return isExist;
        }

        public static List<VideoTubeCounterEntity> CountVideoTubesByCategory()
        {
            Logger.Info("Retrieving number of video tubes in each category");
            var list = new List<VideoTubeCounterEntity>();
            using( var videoTubeRepository = new VideoTubeRepository())
            {
                list = videoTubeRepository.GetVideoTubeCountsByCategory();
            }

            return list;
        }

        public static List<VideoTubePo> GetAllVideoTubesByProvider(string providerName)
        {
            Logger.Info(String.Format("Retrieving all video tubes for provider '{0}", providerName));
            var videoTubePoList = new List<VideoTubePo>();
            using ( var videoTubeRepository = new VideoTubeRepository())
            {
                videoTubePoList = videoTubeRepository.GetAllVideoTubeByProviderName(providerName);
            }
            return videoTubePoList;
           
        }

        public static VideoTube GetVideoTubeById(int videoTubeId)
        {
            Logger.Info(String.Format("Retrieving video tube by Id={0}", videoTubeId));
            var videoTube = new VideoTube();
            using( var videoTubeRepository = new VideoTubeRepository())
            {
                videoTube = videoTubeRepository.GetVideoTubeById(videoTubeId);
            }

            return videoTube;

        }

        public static bool RemoveVideoTubeFromPublicLibrary(int videoTubeId)
        {
            Logger.Info(String.Format("Removing VideoTube with Id={0} from Public Library", videoTubeId));

            bool isSuccess = false;
            using(var videoTubeRepository = new VideoTubeRepository())
            {
                var videoTube = videoTubeRepository.GetVideoTubeById(videoTubeId);
                if (videoTube != null)
                {
                    videoTube.IsInPublicLibrary = false;
                    videoTube.CreatedDate = DateTime.Now;
                    isSuccess = videoTubeRepository.UpdateVideoTube(videoTube);
                }
            }
            return isSuccess;
        }

        public static bool UpdateVideoTube(VideoTube videoTube)
        {
            Contract.Requires(videoTube != null, "Invalid video tube specified");
            Contract.Requires(videoTube.VideoTubeId > 0, "VideoTube record does not exist and cannot be updated");

            Logger.Info(String.Format("Updating video tube with Id={0}", videoTube.VideoTubeId));

            bool isSuccess = false;
            using (var videoTubeRepository = new VideoTubeRepository())
            {
                isSuccess = videoTubeRepository.UpdateVideoTube(videoTube);
            }
         
            return isSuccess;
        }

        public static bool CheckIfVideoTubeExistsInVideoRoomTube(int userId, string providerVideoId)
        {
            Logger.Info(String.Format("Checking if VideoTube with ProviderVideoId='{0}' exists in Video Room for User Id={1}", providerVideoId, userId));
            bool isSuccess = false;
            using (var videoTubeRepository = new VideoTubeRepository())
            {
                isSuccess = videoTubeRepository.CheckIfVideoTubeExistsInVideoRoomTubeByUserIdAndProviderVideoId(userId, providerVideoId);
            }

            return isSuccess;
        }

        public static List<VideoTubePo> GetAllPublicVideoTubes()
        {
            Logger.Info("Retrieving all public video tubes");

            var videos = new List<VideoTubePo>();

            using (var videoTubeRepository = new VideoTubeRepository())
            {
                videos = videoTubeRepository.GetAllPublicVideoTubes();
            }

            return videos;
        }

        public static VideoTubePageModel GetAllPublicVideoTubesByPageIndex(PublicLibraryVideoSearchCriteria criteria, int pageSize = 10)
        {
            Logger.Info(String.Format("Retrieving page {0} of public video tubes; page size is {1}", criteria.PageIndex, pageSize));

            int configPageSize = 0;

            if (Int32.TryParse(ConfigurationManager.AppSettings["AddVideoPageSize"].ToString(), out configPageSize))
            {
                pageSize = configPageSize;
            }

            var videoPage = new VideoTubePageModel();
            int pageCount = 0;

            using (var videoTubeRepository = new VideoTubeRepository())
            {
                var videosInChannel = videoTubeRepository.GetAllVideoTubeByChannelTubeId(criteria.ChannelTubeId);
                var videos = videoTubeRepository.GetPublicVideoTubesByPageIndex(criteria.PageIndex, criteria.Keywords, out pageCount, pageSize);

                videoPage.PageIndex = criteria.PageIndex;
                videoPage.PageSize = pageSize;
                videoPage.PrevPageIndex = criteria.PageIndex <= 1 ? 1 : criteria.PageIndex - 1;
                videoPage.NextPageIndex = criteria.PageIndex >= pageCount ? pageCount : criteria.PageIndex + 1;
                videoPage.PageCount = pageCount;
                videoPage.VideoTubeModels = new List<VideoTubeModel>();

                videos.ForEach(v =>
                {
                    videoPage.VideoTubeModels.Add(new VideoTubeModel(v)
                    {
                        IsInChannel = videosInChannel.Any(x => x.VideoTubeId == v.VideoTubeId)
                    });
                });

                //YouTubeServiceV3Manage.UpdateVideoStatus(videoPage.VideoTubeModels);
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
                //YouTubeServiceManage.UpdateVideoStatus(videoModels.ToList());
                //VimeoServiceManage.UpdateVideoStatus(vimeoModels.ToList());
            }

            return videoPage;
        }

        public static VideoTubePageModel GetAllPublicVideoTubesByCategoryIdAndPageIndex(PublicLibraryVideoSearchCriteria criteria, int pageSize = 10)
        {
            Logger.Info(String.Format("Retrieving page {0} of public video tubes for category with Id={1}, page size = {2} ", criteria.PageIndex, criteria.CategoryId, pageSize));

            int configPageSize = 0;

            if (Int32.TryParse(ConfigurationManager.AppSettings["AddVideoPageSize"].ToString(), out configPageSize))
            {
                pageSize = configPageSize;
            }

            var videoPage = new VideoTubePageModel();
            int pageCount = 0;

            using (var videoTubeRepository = new VideoTubeRepository())
            {
                var videosInChannel = videoTubeRepository.GetAllVideoTubeByChannelTubeId(criteria.ChannelTubeId);
                var videos = videoTubeRepository.GetPublicVideoTubesByCategoryIdAndPageIndex(criteria.CategoryId, criteria.PageIndex, criteria.Keywords, out pageCount, pageSize);

                videoPage.PageIndex = criteria.PageIndex;
                videoPage.PageSize = pageSize;
                videoPage.PrevPageIndex = criteria.PageIndex <= 1 ? 1 : criteria.PageIndex - 1;
                videoPage.NextPageIndex = criteria.PageIndex >= pageCount ? pageCount : criteria.PageIndex + 1;
                videoPage.PageCount = pageCount;
                videoPage.VideoTubeModels = new List<VideoTubeModel>();

                videos.ForEach(v =>
                {
                    videoPage.VideoTubeModels.Add(new VideoTubeModel(v)
                    {
                        IsInChannel = videosInChannel.Any(x => x.VideoTubeId == v.VideoTubeId)
                    });
                });
                var videoModels = new List<VideoTubeModel>();
                var vimeoModels = new List<VideoTubeModel>();
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
                //MST: 03152017 - Turning off update all status updates because Vimeo started to throttle us. Th
                //these updates will be done with JAS by throttling requests to both providers
                //YouTubeServiceManage.UpdateVideoStatus(videoModels.ToList());
                //VimeoServiceManage.UpdateVideoStatus(vimeoModels.ToList());
            }

            return videoPage;
        }
    }
}
