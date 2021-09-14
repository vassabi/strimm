using log4net;
using Strimm.Data.Repositories;
using Strimm.Model.WebModel;
using Strimm.Shared;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using System.Xml;
using System.Threading;
using System.Net.Http.Headers;
using System.Collections.Concurrent;

namespace StrimmBL
{
    public class YouTubeServiceManage
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(YouTubeServiceManage));

        private static string GetThumbnailUrl(ThumbnailDetails details)
        {
            string url = null;

            if (details != null)
            {
                url = details.Standard != null
                        ? details.Standard.Url
                        : details.High != null
                            ? details.High.Url
                            : details.Medium != null
                                ? details.Medium.Url
                                : details.Default.Url;
            }

            return url;
        }

        /// <summary>
        /// This method will search for videos on YouTube using keywords specified by user
        /// and the starting search index.
        /// </summary>
        /// <param name="keywords">Keywords string entered by user</param>
        /// <param name="startIndex">Starting search index</param>
        /// <returns>Collection of VideoTubeModels</returns>
        public static VideoTubePageModel GetVideosByKeywords(string keywords, string pageToken, int startIndex, bool isLongDuration, bool allowMatureContent)
        {
            Logger.Info(String.Format("Executing Search by Keywords request against YouTube. Using keywords='{0}'", keywords));

            if (string.IsNullOrEmpty(keywords))
            {
                Logger.Warn("Aborting keyword search against youtube, keywords were not specified or invalid");
                return null;
            }

            var videoPageModel = new VideoTubePageModel()
            {
                VideoTubeModels = new List<VideoTubeModel>()
            };

            string devKey = ConfigurationManager.AppSettings["ydevelopementkey"];

            //Task task = new Task(() =>
            //{
                startIndex = startIndex < 0 ? 1 : startIndex;

                string pageTokenByRequest = String.Empty;

                var videos = new List<string>();
                var channels = new List<string>();
                var playlists = new List<string>();
                var videoInfo = new List<string>();
                var stringBuilder = new StringBuilder();
                var videoTubeModels = new List<VideoTubeModel>();

                try
                {
                    Logger.Debug("Initilizing Youtube libraries....");

                    var youtubeService = new YouTubeService(new BaseClientService.Initializer()
                    {
                        ApiKey = devKey,//"AIzaSyDcELCYNkJcYuK9mlG3r2pzcm94x9d3kaU",//replace to devkey after main implementation to appsettings
                        ApplicationName = "Strimm"//replace after main implementation to appsettings
                    });

                    Logger.Debug("Youtube library initialized!");

                    var searchListRequest = youtubeService.Search.List("snippet");

                      //1 - Film & Animation 
                      //  2 - Autos & Vehicles
                      //  10 - Music
                      //  15 - Pets & Animals
                      //  17 - Sports
                      //  18 - Short Movies
                      //  19 - Travel & Events
                      //  20 - Gaming
                      //  21 - Videoblogging
                      //  22 - People & Blogs
                      //  23 - Comedy
                      //  24 - Entertainment
                      //  25 - News & Politics
                      //  26 - Howto & Style
                      //  27 - Education
                      //  28 - Science & Technology
                      //  29 - Nonprofits & Activism
                      //  30 - Movies
                      //  31 - Anime/Animation
                      //  32 - Action/Adventure
                      //  33 - Classics
                      //  34 - Comedy
                      //  35 - Documentary
                      //  36 - Drama
                      //  37 - Family
                      //  38 - Foreign
                      //  39 - Horror
                      //  40 - Sci-Fi/Fantasy
                      //  41 - Thriller
                      //  42 - Shorts
                      //  43 - Shows
                      //  44 - Trailers

                    searchListRequest.Q = keywords; // Replace with your search term.
                    searchListRequest.MaxResults = 50;
                    searchListRequest.SafeSearch = SearchResource.ListRequest.SafeSearchEnum.Strict;
                    searchListRequest.Type = "video";
                    //searchListRequest.EventType = Google.Apis.YouTube.v3.SearchResource.ListRequest.EventTypeEnum.Live;
                    //searchListRequest.VideoCategoryId = "Sports";
                    //searchListRequest.VideoDefinition = SearchResource.ListRequest.VideoDefinitionEnum.High;
                    searchListRequest.VideoDuration = isLongDuration ? SearchResource.ListRequest.VideoDurationEnum.Long : SearchResource.ListRequest.VideoDurationEnum.Any;
                    
                    //if not longduration set bring videos by view count (popular)
                    if (!isLongDuration)
                    {
                        searchListRequest.Order = SearchResource.ListRequest.OrderEnum.ViewCount;
                    }
                    
                    searchListRequest.VideoEmbeddable = SearchResource.ListRequest.VideoEmbeddableEnum.True;
                    searchListRequest.Service.HttpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));

                    if (!String.IsNullOrEmpty(pageToken))
                    {
                        searchListRequest.PageToken = pageToken;
                    }

                    var searchListResponse = searchListRequest.Execute();

                    if (searchListResponse != null && searchListResponse.Items != null && searchListResponse.Items.Count > 0)
                    {
                        pageTokenByRequest = searchListResponse.NextPageToken;

                        searchListResponse.Items.ToList().ForEach(x =>
                        {
                            switch (x.Id.Kind)
                            {
                                case "youtube#video":
                                    videos.Add(String.Format("{0}", x.Id.VideoId));
                                    break;
                                case "youtube#channel":
                                    channels.Add(String.Format("{0} ({1})", x.Snippet.Title, x.Id.ChannelId));
                                    break;
                                case "youtube#playlist":
                                    playlists.Add(String.Format("{0} ({1})", x.Snippet.Title, x.Id.PlaylistId));
                                    break;
                                default:
                                    break;
                            }
                        });

                        videos.ForEach(x => stringBuilder.Append(String.Format("{0},", x)));

                        var searchedVideosList = youtubeService.Videos.List("contentDetails,snippet,status,statistics");
                        searchedVideosList.Id = stringBuilder.ToString().TrimEnd(',');

                        var searchVideosRequest = searchedVideosList.Execute();
                        searchedVideosList.Service.HttpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
                       
                           
                        if (searchVideosRequest != null && searchVideosRequest.Items != null && searchVideosRequest.Items.Count > 0)
                        {
                            searchVideosRequest.Items.ToList().ForEach(v =>
                            {
                                if (v != null)
                                {
                                    string liveVideoStatus;
                                    bool isLive = false;

                                    // Based on youtube documentation live videos have the following statuses:
                                    // 1. live
                                    // 2. none
                                    // 3. upcoming
                                    //
                                    if (!String.IsNullOrEmpty(v.Snippet.LiveBroadcastContent))
                                    {
                                        liveVideoStatus = v.Snippet.LiveBroadcastContent;
                                        isLive = (liveVideoStatus == "live" || liveVideoStatus == "upcoming");
                                    }


                                    TimeSpan youTubeDuration = XmlConvert.ToTimeSpan(v.ContentDetails.Duration.ToString());
                                    double doubleDuration = youTubeDuration.TotalSeconds;
                                    videoInfo.Add(youTubeDuration.Seconds.ToString());

                                    // filter countryRestriction and embedable(on status if false ==restricted)
                                    bool reagionRestriction = (v.ContentDetails.RegionRestriction != null || v.Status.Embeddable == false || v.ContentDetails.CountryRestriction != null);
                                    bool matureContent = v.ContentDetails.ContentRating != null && v.ContentDetails.ContentRating.YtRating == "ytAgeRestricted";

                                    var videoModel = new VideoTubeModel()
                                    {
                                        CategoryId = 0,
                                        CategoryName = v.Snippet.CategoryId,//need to check how to get category name by this id
                                        Description = v.Snippet.Description,
                                        IsInChannel = false,
                                        IsInPublicLibrary = false,
                                        IsPrivate = (bool)v.Status.PublicStatsViewable, //bool? type in api
                                        IsRemovedByProvider = false,
                                        IsRestrictedByProvider = reagionRestriction || (matureContent && !allowMatureContent),
                                        IsRRated = matureContent,
                                        IsScheduled = false,
                                        ProviderVideoId = v.Id,
                                        Title = v.Snippet.Title,
                                        UseCounter = 0,
                                        VideoProviderId = 1,
                                        VideoProviderName = "YouTube",
                                        VideoTubeId = 0,
                                        Thumbnail = GetThumbnailUrl(v.Snippet.Thumbnails),
                                        Duration = Convert.ToInt64(doubleDuration),
                                        IsLive = isLive,
                                        DurationString = DateTimeUtils.PrintTimeSpan(Convert.ToInt64(doubleDuration)),
                                        ViewCounter = (v.Statistics != null && (v.Statistics.ViewCount != null || v.Statistics.ViewCount != 0))
                                                            ? Convert.ToInt64(v.Statistics.ViewCount)
                                                            : 0
                                    };

                                    videoTubeModels.Add(videoModel);
                                }
                                else
                                {
                                    Logger.Warn("Invalid video returned as part of result set from YouTube");
                                }
                            });

                            if (videoTubeModels != null)
                            {
                                videoPageModel.VideoTubeModels.AddRange(videoTubeModels);
                            }
                        }
                        else
                        {
                            Logger.Warn("Failed to retrieve videos during the view search. Returned collection is emtpy");
                        }

                        videoPageModel.NextPageIndex = startIndex + 50;
                        videoPageModel.PrevPageIndex = startIndex == 1 ? 1 : startIndex - 50;
                        videoPageModel.PageSize = 50;
                        videoPageModel.PageCount = 1000000 / 50;
                        videoPageModel.PageToken = pageTokenByRequest;
                    }
                    else
                    {
                        Logger.Warn(String.Format("Youtube did not return any videos for search requests based on keyworkd='{0}'", keywords));
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error(String.Format("Error occured while searching for videos on YouTube using keywords '{0}' and pageToken={1}", keywords, pageToken), ex);
                }
            //});

            //task.Start();
            //task.Wait();

            return videoPageModel;
        }

        /// <summary>
        /// This method will retrieve a YouTube video by URL
        /// </summary>
        /// <param name="url">YouTube video URL</param>
        /// <returns>Instance of VideoTubeModel</returns>
        public static VideoTubeModel GetVideoByUrl(string url, bool allowMatureContent)
        {
            Logger.Info(String.Format("Executing get video by url against YouTube. Using url='{0}'", url));

            if (String.IsNullOrEmpty(url))
            {
                Logger.Warn("Unable to retrieve YouTube video by URL. Invalid URL specified");
                return null;
            }

            var videoModel = new VideoTubeModel();
            
            string youTubeVideoRegex = ConfigurationManager.AppSettings["YouTubeVideoRegex"].ToString();
            string devKey = ConfigurationManager.AppSettings["ydevelopementkey"];
            var youtubeService = new Google.Apis.YouTube.v3.YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = devKey,//"AIzaSyDcELCYNkJcYuK9mlG3r2pzcm94x9d3kaU",//replace to devkey after main implementation to appsettings
                ApplicationName = "Strimm"//replace after main implementation to appsettings                
            });

            try
            {
                string providerVideoId = String.Empty;
                var regex = new Regex(youTubeVideoRegex);
                var match = regex.Match(url);

                if (match.Success)
                {
                    providerVideoId = match.Groups[1].Value;
                    var searchedVideosList = youtubeService.Videos.List("contentDetails,snippet,status,statistics");
                    searchedVideosList.Id = providerVideoId;
                    searchedVideosList.Service.HttpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));

                    var searchVideosRequest = searchedVideosList.Execute();

                    if (searchVideosRequest != null && searchVideosRequest.Items != null && searchVideosRequest.Items.Count > 0)
                    {
                        var videoList = searchVideosRequest.Items.ToList();
                        var video =  videoList.FirstOrDefault();
                        var privacyStatus = video.Status.PrivacyStatus;
                        var videoLiveDetails = video.Snippet.LiveBroadcastContent.ToList();
                        var videoDescription = String.Empty;

                        if(!String.IsNullOrEmpty(video.Snippet.Description))
                        {
                            videoDescription = MiscUtils.RemoveSpecialCharacters(video.Snippet.Description);
                        }

                        if (video != null)// && video.Snippet.LiveBroadcastContent != "live")
                        {
                            TimeSpan youTubeDuration = XmlConvert.ToTimeSpan(video.ContentDetails.Duration.ToString());
                            double doubleDuration = youTubeDuration.TotalSeconds;

                            var usageStatus = new VideoUsageStatus(video);
                            var liveVideoStatus = String.Empty;
                            var isLive=false;
                            var regionRestriction = new List<string>();
                            if (video.ContentDetails.RegionRestriction!=null)
                            {
                                if (video.ContentDetails.RegionRestriction.Blocked!=null)
                                {
                                    regionRestriction = video.ContentDetails.RegionRestriction.Blocked.ToList();
                                }
                                
                            }
                            var isEmbedable = video.Status.Embeddable;
                            // Based on YouTube documentation, live videos have the following statuses:
                            // 1. live
                            // 2. none
                            // 3. upcoming
                            //
                            if (!String.IsNullOrEmpty(video.Snippet.LiveBroadcastContent))
                            {
                                liveVideoStatus = video.Snippet.LiveBroadcastContent;
                                isLive = (liveVideoStatus == "live" || liveVideoStatus == "upcoming");
                            }
                                                          
                            bool matureContent = video.ContentDetails.ContentRating != null && video.ContentDetails.ContentRating.YtRating == "ytAgeRestricted";

                            videoModel = new VideoTubeModel()
                            {
                                CategoryId = 0,
                                CategoryName = video.Snippet.CategoryId,
                                Description = videoDescription,
                                IsInChannel = false,
                                IsInPublicLibrary = false,
                                IsPrivate = usageStatus.State == VideoStateByProvider.PRIVATE, //bool? type in api
                                IsRemovedByProvider = usageStatus.State == VideoStateByProvider.DELETED,
                                IsRestrictedByProvider = usageStatus.State == VideoStateByProvider.RESTRICTED || (matureContent && !allowMatureContent),
                                IsRRated = (usageStatus.State != VideoStateByProvider.VIEWABLE || matureContent),
                                IsScheduled = false,
                                ProviderVideoId = video.Id,
                                Title = video.Snippet.Title,
                                UseCounter = 0,
                                VideoProviderId = 1,
                                VideoProviderName = "YouTube",
                                VideoTubeId = 0,
                                Thumbnail = GetThumbnailUrl(video.Snippet.Thumbnails),
                                Duration = Convert.ToInt64(doubleDuration),
                                DurationString = DateTimeUtils.PrintTimeSpan(Convert.ToInt64(doubleDuration)),
                                IsLive = isLive
                            };

                            videoModel.ViewCounter = (video.Statistics.ViewCount != null || video.Statistics.ViewCount != 0)
                                                        ? Convert.ToInt32(video.Statistics.ViewCount)
                                                        : 0;

                            if (usageStatus.State != VideoStateByProvider.VIEWABLE)
                            {
                                Logger.Debug(String.Format("Media rating set on video w/ Id='{0}' to '{1}'. Video will not be retrieved", video.Id, video.ContentDetails.ContentRating));
                            }
                        }
                        else
                        {
                            videoModel = GetLiveStreamVideo(providerVideoId).Result;
                        }
                    }
                    else
                    {
                        Logger.Warn(String.Format("Search by url against YouTube did not return any results. Url='{0}'", url));
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Error occured while retrieving YouTube video by URL='{0}'", url), ex);
            }

            return videoModel;
        }

        public static async Task<VideoTubeModel> GetLiveStreamVideo(string providerVideoId)
        {
            var credentials = await GoogleWebAuthorizationBroker.AuthorizeAsync(new ClientSecrets()
            {
                ClientId = "666520747239-qbp19slc5q976naob54lnt3oh513lr93.apps.googleusercontent.com"
            }, new[] { "https://www.googleapis.com/auth/youtube.readonly" }, "user", CancellationToken.None);

            var initializer = new BaseClientService.Initializer()
            {
                HttpClientInitializer = credentials,
                ApplicationName = "Strimm"
            };

            var youtubeService = new Google.Apis.YouTube.v3.YouTubeService(initializer);

            var liveBroadcastRequest = youtubeService.LiveBroadcasts.List("contentDetails,snippet,status,statistics");
            liveBroadcastRequest.Id = providerVideoId;
            liveBroadcastRequest.BroadcastStatus = Google.Apis.YouTube.v3.LiveBroadcastsResource.ListRequest.BroadcastStatusEnum.All;

            var response = liveBroadcastRequest.Execute();
            var responselist = response.Items;

            var liveStream = responselist != null ? responselist.FirstOrDefault() : null;

            VideoTubeModel videoModel = null;

            if (liveStream != null)
            {
                TimeSpan youTubeDuration = (liveStream.Snippet.ScheduledStartTime != null &&
                                            liveStream.Snippet.ScheduledEndTime != null)
                                                ? liveStream.Snippet.ScheduledEndTime.Value - liveStream.Snippet.ActualStartTime.Value
                                                : TimeSpan.Zero;

                double doubleDuration = youTubeDuration.TotalSeconds;

                var usageStatus = new VideoUsageStatus(liveStream);

                videoModel = new VideoTubeModel()
                {
                    CategoryId = 0,
                    CategoryName = String.Empty, // No category specified
                    Description = liveStream.Snippet.Description,
                    IsInChannel = false,
                    IsInPublicLibrary = false,
                    IsPrivate = usageStatus.State == VideoStateByProvider.PRIVATE, //bool? type in api
                    IsRemovedByProvider = usageStatus.State == VideoStateByProvider.DELETED,
                    IsRestrictedByProvider = usageStatus.State == VideoStateByProvider.RESTRICTED,
                    IsRRated = false,
                    IsScheduled = false,
                    ProviderVideoId = liveStream.Id,
                    Title = liveStream.Snippet.Title,
                    UseCounter = 0,
                    VideoProviderId = 1,
                    VideoProviderName = "YouTube",
                    VideoTubeId = 0,
                    Thumbnail = GetThumbnailUrl(liveStream.Snippet.Thumbnails),
                    Duration = Convert.ToInt64(doubleDuration),
                    DurationString = DateTimeUtils.PrintTimeSpan(Convert.ToInt64(doubleDuration))
                };

                videoModel.ViewCounter = (liveStream.Statistics.ConcurrentViewers != null || liveStream.Statistics.ConcurrentViewers != 0)
                                            ? Convert.ToInt32(liveStream.Statistics.ConcurrentViewers)
                                            : 0;

                if (usageStatus.State != VideoStateByProvider.VIEWABLE)
                {
                    Logger.Debug(String.Format("Media rating set on video w/ Id='{0}' to '{1}'. Video will not be retrieved", liveStream.Id, usageStatus.State));
                }
            }

            return videoModel;
        }

        public static VideoTubePageModel GetVideosById(List<string>videoIds, int startIndex, string pageToken, bool allowMatureContent)
        {
            Logger.Info(String.Format("Executing get video by ids against YouTube. Using video ids collection: '{0}'", videoIds));

            if (videoIds.Count == 0)
            {
                Logger.Warn("Unable to retrieve YouTube video by ids.");
                return null;
            }

            var videoPageModel = new VideoTubePageModel()
            {
                VideoTubeModels = new List<VideoTubeModel>()
            };

            startIndex = startIndex < 0 ? 1 : startIndex;
            string youTubeVideoRegex = ConfigurationManager.AppSettings["YouTubeVideoRegex"].ToString();
            string devKey = ConfigurationManager.AppSettings["ydevelopementkey"];

            var youtubeService = new Google.Apis.YouTube.v3.YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = devKey,//"AIzaSyDcELCYNkJcYuK9mlG3r2pzcm94x9d3kaU",//replace to devkey after main implementation to appsettings
                ApplicationName = "Strimm"//replace after main implementation to appsettings
            });

            string pageTokenByRequest = String.Empty;

            var videos = new List<string>();
            var channels = new List<string>();
            var playlists = new List<string>();
            var videoInfo = new List<string>();
            var stringBuilder = new StringBuilder();
            var videoTubeModels = new List<VideoTubeModel>();
            var videoList = new List<Video>();

            try
            {
                string providerVideoId = String.Empty;
                // var regex = new Regex(youTubeVideoRegex);
                //  var match = regex.Match(url);

                if (videoIds.Count != 0 && videoIds != null)
                {
                    var searchVideosRequest = new VideoListResponse();
                    foreach (var video in videoIds)
                    {
                        if (video != null)
                        {
                            providerVideoId = video.ToString();

                            var searchedVideosList = youtubeService.Videos.List("contentDetails,snippet,status,statistics");

                            searchedVideosList.Id = providerVideoId;
                            searchedVideosList.Service.HttpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));

                            searchVideosRequest = searchedVideosList.Execute();
                        }

                        foreach (var v in searchVideosRequest.Items.ToList())
                        {
                            if (v != null)
                            {
                                TimeSpan youTubeDuration = XmlConvert.ToTimeSpan(v.ContentDetails.Duration.ToString());
                                double doubleDuration = youTubeDuration.TotalSeconds;
                                videoInfo.Add(youTubeDuration.Seconds.ToString());

                                // filter countryRestriction and embedable(on status if false ==restricted)
                                bool reagionRestriction = (v.ContentDetails.RegionRestriction != null || v.Status.Embeddable == false || v.ContentDetails.CountryRestriction != null);
                                bool matureContent = v.ContentDetails.ContentRating != null && v.ContentDetails.ContentRating.YtRating == "ytAgeRestricted";
                                bool publicStatsViewable = (bool)v.Status.PublicStatsViewable;
                                bool isPrivateVideo = true;
                                if(publicStatsViewable==true)
                                {
                                    isPrivateVideo = false;
                                }
                                var videoModel = new VideoTubeModel()
                                {
                                    CategoryId = 0,
                                    CategoryName = v.Snippet.CategoryId,//need to check how to get category name by this id
                                    Description = v.Snippet.Description,
                                    IsInChannel = false,
                                    IsInPublicLibrary = false,
                                    IsPrivate = isPrivateVideo, //bool? type in api
                                    IsRemovedByProvider = false,
                                    IsRestrictedByProvider = reagionRestriction || (matureContent && !allowMatureContent),
                                    IsRRated = matureContent,
                                    IsScheduled = false,
                                    ProviderVideoId = v.Id,
                                    Title = v.Snippet.Title,
                                    UseCounter = 0,
                                    VideoProviderId = 1,
                                    VideoProviderName = "YouTube",
                                    VideoTubeId = 0,
                                    Thumbnail = GetThumbnailUrl(v.Snippet.Thumbnails),
                                    Duration = Convert.ToInt64(doubleDuration),
                                    DurationString = DateTimeUtils.PrintTimeSpan(Convert.ToInt64(doubleDuration)),
                                    ViewCounter = (v.Statistics.ViewCount != null || v.Statistics.ViewCount != 0)
                                                        ? Convert.ToInt32(v.Statistics.ViewCount)
                                                        : 0
                                };

                                videoTubeModels.Add(videoModel);
                            };
                        }
                    }

                    if (videoTubeModels != null)
                    {
                        videoPageModel.VideoTubeModels.AddRange(videoTubeModels);
                    }
                    else
                    {
                        Logger.Warn("Invalid video returned as part of result set from YouTube");
                    }

                    videoPageModel.NextPageIndex = startIndex + 50;
                    videoPageModel.PrevPageIndex = startIndex == 1 ? 1 : startIndex - 50;
                    videoPageModel.PageSize = 50;
                    videoPageModel.PageCount = 1000000 / 50;
                    videoPageModel.PageToken = pageTokenByRequest;

                }

                else
                {
                    Logger.Warn(String.Format("Search by videoIds against YouTube did not return any results. videoIds='{0}'", videoIds));
                }

            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Error occured while retrieving YouTube video by videoIds='{0}'", videoIds), ex);
            }
           
            return videoPageModel;
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

            Logger.Info(String.Format("Updating video status of {0} YouTube videos", videos.Count));

            string devKey = ConfigurationManager.AppSettings["ydevelopementkey"];
            int batchSize = 40;

            var stringBuilder = new StringBuilder();

            var videoIdStrings = String.Empty;

            List<VideoTubeModel> videoRequestList = new List<VideoTubeModel>();

            DateTime now = DateTime.Now;

            // Get unique and non-private videos only
            //
            var uniqueVideos = videos.Distinct()
                                     .ToList()
                                     .Where(x => x.VideoProviderId == 1 && !x.PrivateVideoModeEnabled)
                                     .ToList();

            if (uniqueVideos.Count == 0)
            {
                Logger.Debug(String.Format("No unique videos to update. Status for YouTube videos will not be updated!"));
                return;
            }
            else
            {
                Logger.Debug(String.Format("Updating status for {0} YouTube videos", uniqueVideos.Count));
            }

            try
            {
                var youtubeService = new Google.Apis.YouTube.v3.YouTubeService(new BaseClientService.Initializer()
                {
                    ApiKey = devKey,//"AIzaSyDcELCYNkJcYuK9mlG3r2pzcm94x9d3kaU",//replace to devkey after main implementation to appsettings
                    ApplicationName = "Strimm"//replace after main implementation to appsettings
                });

                int youTubeRequestCount = (uniqueVideos.Count / batchSize) + (uniqueVideos.Count % batchSize == 0 ? 0 : 1);
                var searchedVideosList = youtubeService.Videos.List("snippet,contentDetails,status");

                searchedVideosList.Service.HttpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));

                using (var repository = new VideoTubeRepository())
                {
                    for (int i = 0; i < youTubeRequestCount; i++)
                    {
                        videoRequestList.Clear();
                        videoRequestList = uniqueVideos.Count < batchSize ? uniqueVideos : uniqueVideos.Skip(batchSize * i).Take(batchSize).ToList();

                        stringBuilder = new StringBuilder();

                        videoRequestList.ForEach(x =>
                        {
                            if (x != null)
                            {
                                stringBuilder.Append(String.Format("{0},", x.ProviderVideoId));
                                x.IsRemovedByProvider = true;
                            }
                        });

                        searchedVideosList.Id = stringBuilder.ToString().TrimEnd(',');

                        VideoListResponse searchVideosRequest = null;

                        try
                        {
                            searchVideosRequest = searchedVideosList.Execute();

                            if (searchVideosRequest != null && searchVideosRequest.Items != null && searchVideosRequest.Items.Count > 0)
                            {
                                var videoList = new List<Video>(searchVideosRequest.Items.ToList());

                                if ((videoRequestList != null) || (videoRequestList.Count != 0))
                                {
                                    videoList.ForEach(e =>
                                    {
                                        var existingVideos = videos.Where(x => x != null && x.ProviderVideoId == e.Id);

                                        if (existingVideos != null)
                                        {
                                            if (e != null)
                                            {
                                                var usageStatus = new VideoUsageStatus(e);

                                                bool isRemoved = false;
                                                bool isRestricted = false;
                                                bool isPrivate = false;

                                                if (usageStatus.State != VideoStateByProvider.VIEWABLE)
                                                {
                                                    isRemoved = (usageStatus.State == VideoStateByProvider.DELETED);
                                                    isRestricted = (usageStatus.State == VideoStateByProvider.FAILED ||
                                                                                    usageStatus.State == VideoStateByProvider.PRIVATE ||
                                                                                    usageStatus.State == VideoStateByProvider.REJECTED ||
                                                                                    usageStatus.State == VideoStateByProvider.RESTRICTED);
                                                    isPrivate = usageStatus.State == VideoStateByProvider.PRIVATE;

                                                    Logger.Warn(String.Format("Video with Provider Id='{0}' is not usable. Failure reason: '{1}", e.Id, usageStatus.UsageFailureReason));
                                                }

                                                bool matureContent = e.ContentDetails.ContentRating != null && e.ContentDetails.ContentRating.YtRating == "ytAgeRestricted";

                                                var uniqueVideo = uniqueVideos.FirstOrDefault(x => x.ProviderVideoId == e.Id);

                                                if (uniqueVideo != null)
                                                {
                                                    repository.UpdateVideoTubeStatusById(uniqueVideo.VideoTubeId, isPrivate, isRestricted, isRemoved, matureContent);
                                                }

                                                existingVideos.ToList().ForEach(x =>
                                                {
                                                    x.IsRemovedByProvider = isRemoved;
                                                    x.IsRestrictedByProvider = isRestricted;
                                                    x.IsPrivate = isPrivate;
                                                    x.IsRRated = matureContent;
                                                });
                                            }
                                        }
                                        else
                                        {
                                            Logger.Warn(String.Format("Video with Provider Id='{0}' was not part of the original request and will be ignored", e.Id));
                                        }
                                    });
                                }
                                else
                                {
                                    Logger.Warn("Unable to retrieve data from YouTube. VideoFeed was not returned");
                                }
                            }
                            else
                            {
                                Logger.Warn("Video search did not return any requests from YouTube");
                            }
                        }
                        catch (Google.GoogleApiException gex)
                        {
                            Logger.Error("Error occured while executing google API request", gex);

                            videoRequestList.ForEach(x =>
                            {
                                if (x != null)
                                {
                                    x.IsRemovedByProvider = false;
                                }
                            });
                        }
                    }

                    Logger.Debug(String.Format("{0} YouTube videos were marked as restricted/removed", uniqueVideos.Where(x => x.IsRestrictedByProvider || x.IsRemovedByProvider).ToList().Count));
                }
            }
            catch (NullReferenceException objex)
            {
                Logger.Error("Error occured while making YouTube update status batch request", objex);

                videoRequestList.ForEach(x =>
                {
                    if (x != null)
                    {
                        x.IsRemovedByProvider = false;
                    }
                });
            }
            catch (Exception ex)
            {
                Logger.Error("Error occured while processing YouTube update status batch request", ex);

                videoRequestList.ForEach(x =>
                {
                    if (x != null)
                    {
                        x.IsRemovedByProvider = false;
                    }
                });
            }
        }

        #region OldYoutubeAPI
        /// <summary>
        /// This method will retrieve a YouTube video by URL
        /// </summary>
        /// <param name="url">YouTube video URL</param>
        /// <returns>Instance of VideoTubeModel</returns>
        //public static VideoTubeModel GetVideoByUrl(string url)
        //{
        //    if (String.IsNullOrEmpty(url))
        //    {
        //        Logger.Warn("Unable to retrieve YouTube video by URL. Invalid URL specified");
        //        return null;
        //    }

        //    VideoTubeModel videoModel = null;

        //    string youTubeVideoRegex = ConfigurationManager.AppSettings["YouTubeVideoRegex"].ToString();
        //    string devKey = ConfigurationManager.AppSettings["ydevelopementkey"];

        //    try
        //    {
        //        string providerVideoId = String.Empty;
        //        var regex = new Regex(youTubeVideoRegex);
        //        var match = regex.Match(url);

        //        if (match.Success)
        //        {
        //            providerVideoId = match.Groups[1].Value;
        //            string uri = String.Format("http://gdata.youtube.com/feeds/api/videos/{0}", providerVideoId);

        //            var settings = new YouTubeRequestSettings("development", devKey);
        //            var request = new YouTubeRequest(settings);

        //            var video = request.Retrieve<Video>(new Uri(uri));

        //            if (video != null)
        //            {
        //                var mediaRating = video.Media != null ? video.Media.Rating : null;
        //                var mediaRestricted = video.Media != null ? video.Media.Restriction : null;

        //                bool isVideoRestricted = (video.Status != null && video.Status.Name == "restricted") || mediaRating != null || mediaRestricted != null;

        //                videoModel = new VideoTubeModel()
        //                {
        //                    Title = video.Title,
        //                    Description = video.Description,
        //                    ViewCounter = video.ViewCount,
        //                    ProviderVideoId = providerVideoId,
        //                    IsInChannel = false,
        //                    IsRemovedByProvider = false,
        //                    IsRestrictedByProvider = isVideoRestricted,
        //                    IsRRated = false,
        //                    CategoryName = video.Categories.Last().Label,
        //                    VideoProviderId = 1 // Hardcoding for now for YouTube
        //                };

        //                long duration = 0;
        //                foreach (var mediacontent in video.Contents)
        //                {
        //                    duration = long.Parse(mediacontent.Duration);
        //                }

        //                videoModel.Duration = duration;
        //                videoModel.DurationString = DateTimeUtils.PrintTimeSpan(duration);

        //                foreach (var thumbnail in video.Thumbnails)
        //                {
        //                    videoModel.Thumbnail = thumbnail.Url;
        //                }

        //                if (isVideoRestricted)
        //                {
        //                    Logger.Debug(String.Format("Media rating set on video w/ Id='{0}' to '{1}'. Video wil not be retrieved", video.VideoId, mediaRating.Value));
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error(String.Format("Error occured while retrieving YouTube video by URL='{0}'", url));
        //    }

        //    return videoModel;
        //}

        /// <summary>
        /// This method will search for videos on YouTube using keywords specified by user
        /// and the starting search index.
        /// </summary>
        /// <param name="keywords">Keywords string entered by user</param>
        /// <param name="startIndex">Starting search index</param>
        /// <returns>Collection of VideoTubeModels</returns>

        /// <summary>
        /// This method will update IsRemovedByProvider and/or IsRestrictedByProvider flags on 
        /// each video model object within the collection
        /// </summary>
        /// <param name="videos">Collection of VideoTubeModels to check</param>
        //public static void UpdateVideoStatus(List<VideoTubeModel> videos)
        //{
        //    if (videos == null || videos.Count == 0)
        //    {
        //        return;
        //    }

        //    Logger.Info(String.Format("Updating video status of {0} YouTube videos", videos.Count));

        //    string devKey = ConfigurationManager.AppSettings["ydevelopementkey"];
        //    int batchSize = 50;

        //    var settings = new YouTubeRequestSettings("development", devKey);
        //    var request = new YouTubeRequest(settings);
        //    var entries = new List<Video>();

        //    Feed<Video> videoFeed = null;

        //    try
        //    {
        //        videos.ForEach(x =>
        //        {
        //            var video = new Video() { Id = String.Format("http://gdata.youtube.com/feeds/api/videos/{0}", x.ProviderVideoId), VideoId = x.ProviderVideoId };
        //            entries.Add(video);
        //            Logger.Debug(String.Format("Adding video request for: http://gdata.youtube.com/feeds/api/videos/{0}", x.ProviderVideoId));
        //        });

        //        int youTubeRequestCount = (entries.Count / batchSize) + (entries.Count % batchSize == 0 ? 0 : 1);

        //        for (int i = 0; i < youTubeRequestCount; i++)
        //        {
        //            var videoRequestList = entries.Count < batchSize ? entries : entries.Skip(batchSize * i).Take(batchSize).ToList();
        //            videoFeed = request.Batch<Video>(videoRequestList, new Uri("https://gdata.youtube.com/feeds/api/videos/batch?v=2"), GDataBatchOperationType.query);

        //            if (videoFeed != null)
        //            {
        //                var submitedVideoIds = videoRequestList.Select(y => y.VideoId).ToList();
        //                videos.Where(x => submitedVideoIds.Contains(x.ProviderVideoId)).ToList().ForEach(x => x.IsRemovedByProvider = !videoFeed.Entries.Any(e => e != null && e.VideoId == x.ProviderVideoId));

        //                videoFeed.Entries.ToList().ForEach(e =>
        //                {
        //                    var video = videos.FirstOrDefault(x => x.ProviderVideoId == e.VideoId);

        //                    if (video != null)
        //                    {
        //                        var mediaRating = e.Media.Rating;

        //                        if (e.Content != "Video not found")
        //                        {
        //                            if (e.Status != null && e.Status.Name == "restricted")
        //                            {
        //                                video.IsRestrictedByProvider = (e.Status.Name == "restricted");
        //                                Logger.Debug(String.Format("Video '{0}' was marked as restricted", e.VideoId));
        //                            }
        //                            else
        //                            {
        //                                video.IsRestrictedByProvider = false;
        //                            }
        //                        }

        //                        if (mediaRating != null)
        //                        {
        //                            Logger.Debug(String.Format("Video '{0}' has media rating set by YouTube to '{1}'. Marking video as restricted", e.VideoId, mediaRating.Value));
        //                            video.IsRestrictedByProvider = true;
        //                        }
        //                    }
        //                    else
        //                    {
        //                        Logger.Warn(String.Format("Video with Provider Id='{0}' was not part of the original request and will be ignored", e.Id));
        //                    }
        //                });
        //            }
        //            else
        //            {
        //                Logger.Warn("Unable to retrieve data from YouTube. VideoFeed was not returned");
        //            }
        //        }
        //    }
        //    catch (NullReferenceException objex)
        //    {
        //        Logger.Error("Error occured while making YouTube batch request", objex);
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error("Error occured while processing YouTube batch request", ex);
        //    }
        //    finally
        //    {
        //        if (videoFeed != null)
        //        {
        //            videoFeed = null;
        //        }
        //    }
        //}

        #endregion
    }
}
