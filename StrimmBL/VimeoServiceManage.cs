using log4net;
using RestSharp;
using Strimm.Data.Repositories;
using Strimm.Model.WebModel;
using Strimm.Shared;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VimeoDotNet;
using VimeoDotNet.Models;
using Newtonsoft.Json;
using VimeoDotNet.Net;
using VimeoDotNet.Constants;
using System.Text.RegularExpressions;
using System.Collections.Concurrent;
using System.Net;

namespace StrimmBL
{
    public class VimeoServiceManage
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(VimeoServiceManage));
        private static string DevKey;
        private static string vimeoRegex;

        static VimeoServiceManage()
        {
            try
            {
                DevKey = ConfigurationManager.AppSettings["vdevelopmentkey"];
                vimeoRegex = ConfigurationManager.AppSettings["VimeoRegex"].ToString();
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to retrieve Vimeo API key", ex);
            }
        }

        /// <summary>
        /// This method will search for videos on Vimeo using keywords specified by user
        /// and the starting search index.
        /// </summary>
        /// <param name="keywords">Keywords string entered by user</param>
        /// <param name="startIndex">Starting search index</param>
        /// <returns>Collection of VideoTubeModels</returns>
        public static VideoTubePageModel GetVideosByKeywords(string keywords, int startIndex, bool isLongDuration, bool allowMatureContent)
        {
            Logger.Info(String.Format("Executing Search by Keywords request against Vimeo. Using keywords='{0}'", keywords));

            if (string.IsNullOrEmpty(keywords))
            {
                Logger.Warn("Aborting keyword search against Vimeo, keywords were not specified or invalid");
                return null;
            }

            var videoPageModel = new VideoTubePageModel()
            {
                VideoTubeModels = new List<VideoTubeModel>()
            };

            Task task = new Task(() =>
            {
                startIndex = startIndex < 0 ? 1 : startIndex;

                var videoTubeModels = new List<VideoTubeModel>();

                try
                {
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                    ApiRequestFactory requestFactory = new ApiRequestFactory();
                    IApiRequest apiRequest = requestFactory.GetApiRequest(DevKey);

                    apiRequest.Path = Endpoints.Videos;
                    apiRequest.Method = Method.GET;

                    apiRequest.Query.Add(new KeyValuePair<string, string>("query", String.Format("'{0}'", keywords)));
                    apiRequest.Query.Add(new KeyValuePair<string, string>("method", "vimeo.videos.getThumbnailUrls"));
                    apiRequest.Query.Add(new KeyValuePair<string, string>("page", Convert.ToString(startIndex)));

                    if(!isLongDuration)
                    {
                        apiRequest.Query.Add(new KeyValuePair<string, string>("sort", "plays"));
                    }
                    else
                    {
                        apiRequest.Query.Add(new KeyValuePair<string, string>("sort", "duration"));
                    }
                   
                    apiRequest.Query.Add(new KeyValuePair<string, string>("direction", "desc"));
                    apiRequest.Query.Add(new KeyValuePair<string, string>("per_page", "50"));

                    apiRequest.Headers.Add(new KeyValuePair<string, string>("Accept-Encoding", "gzip, deflate"));
                    
                    IRestResponse<Paginated<Video>> response = apiRequest.ExecuteRequest<Paginated<Video>>();

                    if (response.Data != null)
                    {
                        Paginated<Video> videos = response.Data;
                        var searchListResponse = videos.data;



                        if (searchListResponse != null && searchListResponse.Count > 0)
                        {
                            searchListResponse.ForEach(x =>
                            {
                                bool isMatureContent = IsVideoContentAllowed(x);
                                bool isRestrictedContent = !(x.VideoStatus == VimeoDotNet.Enums.VideoStatusEnum.Available && x.privacy.EmbedPrivacy == VimeoDotNet.Enums.VideoEmbedPrivacyEnum.Public);

                                if (isMatureContent || allowMatureContent)
                                {
                                    var videoModel = new VideoTubeModel()
                                    {
                                        IsRestrictedByProvider = isRestrictedContent || (isMatureContent && allowMatureContent),
                                        IsInPublicLibrary = false,
                                        IsPrivate = (x.privacy.ViewPrivacy == VimeoDotNet.Enums.VideoPrivacyEnum.Nobody),
                                        Thumbnail = (x.pictures != null && x.pictures.Count == 1 && x.pictures[0].sizes != null && x.pictures[0].sizes.Count > 0) ? GetVimeoVideoThumbnail(x.pictures[0].sizes) : String.Empty,
                                        VideoProviderId = 2,
                                        VideoProviderName = "vimeo",
                                        DurationString = DateTimeUtils.PrintTimeSpan((double)x.duration),
                                        DateAdded = DateTime.Now.ToShortDateString(),
                                        Title = x.name,
                                    // IsRRated = isMatureContent, always true even if video not have mature content
                                    Description = x.description,
                                        ProviderVideoId = Convert.ToString(x.id),
                                        Url = x.link,
                                        ViewCounter = x.stats.plays,
                                        Duration = x.duration
                                    };

                                    if (videoModel.IsPrivate)
                                    {
                                    // Private videos are not returned in API calls. should not even get here.
                                }

                                    videoTubeModels.Add(videoModel);
                                }
                            });

                            if (videoTubeModels != null)
                            {
                                videoPageModel.VideoTubeModels.AddRange(videoTubeModels);
                            }

                            videoPageModel.NextPageIndex = startIndex + 1;
                            videoPageModel.PrevPageIndex = startIndex == 1 ? 1 : startIndex - 1;
                            videoPageModel.PageSize = 50;
                            videoPageModel.PageCount = videos.total / 50;
                            videoPageModel.PageIndex = startIndex;
                            

                        }
                        else
                        {
                            Logger.Warn(String.Format("Vimeo did not return any videos for search requests based on keyworkd='{0}'", keywords));

                        }
                    }
                    else
                    {
                        videoPageModel.Message = response.ErrorMessage;
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error(String.Format("Error occured while searching for videos on Vimeo using keywords '{0}' and pageIndex={1}", keywords, startIndex), ex);
                }
            });

            task.Start();
            task.Wait();


            return videoPageModel;
        }

        private static bool IsVideoContentAllowed(Video video)
        {
            bool isAllowed = video != null && (video.content_rating == null || (video.content_rating != null && !video.content_rating.Contains("nudity") && !video.content_rating.Contains("drugs")));

            return isAllowed;
        }

        private static string GetVimeoVideoThumbnail(List<Size> thumbnails)
        {
            string thumbnailUrl = String.Empty;

            if (thumbnails == null || thumbnails.Count == 0)
            {
                return thumbnailUrl;
            }

            for (int i = thumbnails.Count - 1; i >= 0; i--)
            {
                var thumbnail = thumbnails[i];
                if (thumbnail != null && !String.IsNullOrEmpty(thumbnail.link))
                {
                    thumbnailUrl = thumbnail.link;
                    break;
                }
            }
                
            return thumbnailUrl;
        }

        /// <summary>
        /// This method will retrieve a Vimeo video by URL
        /// </summary>
        /// <param name="url">Vimeo video URL</param>
        /// <returns>Instance of VideoTubeModel</returns>
        public static VideoTubeModel GetVideoByUrl(string url, bool allowMatureContent)
        {
            Logger.Info(String.Format("Executing get video by url against Vimeo. Using url='{0}'", url));

            if (String.IsNullOrEmpty(url))
            {
                Logger.Warn("Unable to retrieve Vimeo video by URL. Invalid URL specified");
                return null;
            }

            var videoModel = new VideoTubeModel();
            var id = url.Split('/').Last();
            Task task = new Task(() =>
            {
                try
                {
                    ApiRequestFactory requestFactory = new ApiRequestFactory();
                    IApiRequest apiRequest = requestFactory.GetApiRequest(DevKey);

                    apiRequest.Path = String.Format("{0}/{1}", Endpoints.Videos, id);
                    apiRequest.Method = Method.GET;
                    apiRequest.Headers.Add(new KeyValuePair<string, string>("Accept-Encoding", "gzip, deflate"));

                    IRestResponse<Video> response = apiRequest.ExecuteRequest<Video>();

                    var video = response.Data;

                    if (video != null && video.id != null)
                    {
                        bool isAllowedContent = IsVideoContentAllowed(video);

                        if (isAllowedContent || allowMatureContent)
                        {
                            if (allowMatureContent)
                            {
                                Logger.Debug(String.Format("Mature content is allowed for Video url='{0}'", url));
                            }

                            bool isContentRestricted = !(video.VideoStatus == VimeoDotNet.Enums.VideoStatusEnum.Available && video.privacy.EmbedPrivacy == VimeoDotNet.Enums.VideoEmbedPrivacyEnum.Public);
                            bool isMatureContent = !isAllowedContent;

                            videoModel = new VideoTubeModel()
                            {
                                IsRestrictedByProvider = isContentRestricted || (isMatureContent && !allowMatureContent),
                                IsInPublicLibrary = false,
                                IsPrivate = (video.privacy.ViewPrivacy == VimeoDotNet.Enums.VideoPrivacyEnum.Nobody),
                                Thumbnail = (video.pictures != null && video.pictures.Count == 1 && video.pictures[0].sizes != null && video.pictures[0].sizes.Count > 0) ? GetVimeoVideoThumbnail(video.pictures[0].sizes) : String.Empty,
                                VideoProviderId = 2,
                                VideoProviderName = "vimeo",
                                DurationString = DateTimeUtils.PrintTimeSpan((double)video.duration),
                                DateAdded = DateTime.Now.ToShortDateString(),
                                Title = video.name,
                                Description = video.description,
                                ProviderVideoId = Convert.ToString(video.id),
                                Url = video.link,
                                IsRRated = isMatureContent
                            };
                        }
                        else
                        {
                            Logger.Debug(String.Format("Ignoring video with restricted/mature content content. Video url='{0}'", url));
                        }
                    }
                    else
                    {
                        Logger.Warn(String.Format("Vimeo did not return any videos that match url='{0}'. Video could be private or missing", url));
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error(String.Format("Error occured while searching for video on Vimeo using url '{0}'", url), ex);
                }
            });

            task.Start();
            task.Wait();

            return videoModel;
        }

        /// <summary>
        /// This method will update IsRemovedByProvider and/or IsRestrictedByProvider flags on 
        /// each video model object within the collection
        /// </summary>
        /// <param name="videos">Collection of VideoTubeModels to check</param>
        public static void UpdateVideoStatus(List<VideoTubeModel> videos)
        {
            if (videos == null || videos.Count == 0)
            {
                Logger.Info("Aborting update of status for collection of videos. Invalid or emtpy collection specified");
                return;
            }

            Logger.Info(String.Format("Updating video status of {0} Vimeo videos", videos.Count));

            DateTime now = DateTime.Now;

            // Get unique and non-private videos only
            //
            var uniqueVideos = videos.Distinct()
                                     .ToList()
                                     .Where(x => x.VideoProviderId == 2 && !x.PrivateVideoModeEnabled)
                                     .ToList();

            if (uniqueVideos.Count == 0)
            {
                Logger.Debug(String.Format("No unique videos to update. Status for vimeo videos will not be updated!"));
                return;
            }
            else
            {
                Logger.Debug(String.Format("Updating status for {0} Vimeo videos", uniqueVideos.Count));
            }

            Task task = new Task(() =>
            {
                var models = new List<VideoTubeModel>(uniqueVideos);

                using (var repository = new VideoTubeRepository())
                {
                    models.ForEach(x =>
                    {
                        try
                        {
                            // Ignore all videos that were marked private
                            //
                            if (x != null && !x.PrivateVideoModeEnabled)
                            {
                                ApiRequestFactory requestFactory = new ApiRequestFactory();
                                IApiRequest apiRequest = requestFactory.GetApiRequest(DevKey);

                                apiRequest.Path = String.Format("{0}/{1}", Endpoints.Videos, x.ProviderVideoId);
                                apiRequest.Method = Method.GET;
                                apiRequest.Headers.Add(new KeyValuePair<string, string>("Accept-Encoding", "gzip, deflate"));

                                IRestResponse<Video> response = apiRequest.ExecuteRequest<Video>();

                                var video = response.Data;

                                var existingVideos = videos.Where(v => v.ProviderVideoId == x.ProviderVideoId).ToList();

                                bool isRemoved = false;
                                bool isRestricted = false;
                                bool isPrivate = false;
                                bool isRRated = IsVideoContentAllowed(video);

                                if (video != null)
                                {
                                    if (video.privacy != null)
                                    {
                                        isRestricted = !(video.VideoStatus == VimeoDotNet.Enums.VideoStatusEnum.Available && video.privacy.EmbedPrivacy == VimeoDotNet.Enums.VideoEmbedPrivacyEnum.Public);
                                        isPrivate = (video.privacy.ViewPrivacy == VimeoDotNet.Enums.VideoPrivacyEnum.Nobody);
                                    }
                                    else
                                    {
                                        if (video.id == null && video.VideoStatus == VimeoDotNet.Enums.VideoStatusEnum.Unknown)
                                        {
                                            isRemoved = true;
                                        }
                                    }
                                }
                                else
                                {
                                    isRestricted = true;

                                    Logger.Warn(String.Format("Vimeo did not return any videos that match url='{0}'", x.Url));
                                }

                                repository.UpdateVideoTubeStatusById(x.VideoTubeId, isPrivate, isRestricted, isRemoved, isRRated);

                                existingVideos.ForEach(v =>
                                {
                                    v.IsRestrictedByProvider = isRemoved;
                                    v.IsPrivate = isPrivate;
                                    v.IsRemovedByProvider = isRestricted;
                                    v.IsRRated = isRRated;
                                });
                            }
                            else
                            {
                                if (x.PrivateVideoModeEnabled)
                                {
                                    Logger.Debug(String.Format("Skipping video status/existance check. PrivateVideoModeEnabled flag set to {0}", x.PrivateVideoModeEnabled));
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Logger.Error(String.Format("Error occured while searching for video on Vimeo using url '{0}'", x.Url), ex);
                        }
                    });

                    Logger.Debug(String.Format("{0} Vimeo videos were marked as restricted/removed", uniqueVideos.Where(x => x.IsRestrictedByProvider || x.IsRemovedByProvider).ToList().Count));
                }
            });

            task.Start();
            task.Wait();
        }
    }
}
