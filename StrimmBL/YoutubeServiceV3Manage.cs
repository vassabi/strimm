
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

namespace StrimmBL
{
    public class YoutubeServiceV3Manage
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(YouTubeServiceManage));
        public static List<string> GetVideosByKeywordsV3(string keywords, string startIndex)
        {

           
            var youtubeService = new Google.Apis.YouTube.v3.YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyDcELCYNkJcYuK9mlG3r2pzcm94x9d3kaU",//replace after main implementation to appsettings
                ApplicationName = "Strimm"//replace after main implementation to appsettings
            });

            var searchListRequest = youtubeService.Search.List("snippet");
            searchListRequest.Q = keywords; // Replace with your search term.
            searchListRequest.MaxResults = 50;
            searchListRequest.SafeSearch = SearchResource.ListRequest.SafeSearchEnum.Strict;
            searchListRequest.Type = "video";
            searchListRequest.VideoDefinition = SearchResource.ListRequest.VideoDefinitionEnum.High;
            searchListRequest.VideoDuration = SearchResource.ListRequest.VideoDurationEnum.Long;
            searchListRequest.VideoEmbeddable = SearchResource.ListRequest.VideoEmbeddableEnum.True;
            //searchListRequest.PageToken="pageToken"

            var searchListResponse = searchListRequest.Execute();
            // Call the search.list method to retrieve results matching the specified query term.
            // var searchListResponse = await searchListRequest.ExecuteAsync();

            List<string> videos = new List<string>();
            List<string> channels = new List<string>();
            List<string> playlists = new List<string>();
            List<string> videoInfo = new List<string>();
            // Add each result to the appropriate list, and then display the lists of
            // matching videos, channels, and playlists.
            foreach (var searchResult in searchListResponse.Items)
            {
                switch (searchResult.Id.Kind)
                {
                    case "youtube#video":
                        videos.Add(String.Format("{0}", searchResult.Id.VideoId));
                        var videoIdStrings = String.Empty;
                        foreach (var videoId in videos)
                        {
                            videoIdStrings += videoId + ",";
                        }
                        videoIdStrings.TrimEnd(',');

                        //videoInfo.Add(String.Format("{0}", searchVideosRequest.()));
                        break;

                    case "youtube#channel":
                        channels.Add(String.Format("{0} ({1})", searchResult.Snippet.Title, searchResult.Id.ChannelId));
                        break;

                    case "youtube#playlist":
                        playlists.Add(String.Format("{0} ({1})", searchResult.Snippet.Title, searchResult.Id.PlaylistId));
                        break;
                }
            }
            var searchedVideosList = youtubeService.Videos.List("contentDetails,snippet,status");
            searchedVideosList.Id = videos[0] + ", UWH3vFQhC1o";//videoIdStrings;
            bool reagionRestriction = false;

            var searchVideosRequest = searchedVideosList.Execute();
            List<Video> videoList = searchVideosRequest.Items.ToList();
            foreach (var v in searchVideosRequest.Items)
            {
                TimeSpan youTubeDuration = XmlConvert.ToTimeSpan(v.ContentDetails.Duration.ToString());
                videoInfo.Add(youTubeDuration.Seconds.ToString());
                if (v.ContentDetails.RegionRestriction != null)
                {
                    reagionRestriction = true;
                }
                else
                {
                    reagionRestriction = false;
                }
            }
            return videoInfo;


        }

        public static VideoTubePageModel GetVideosByKeywords(string keywords, string pageToken, int startIndex)
        {
            //TODO Return pageToken for load more
            if (string.IsNullOrEmpty(keywords))
            {
                return null;
            }

            startIndex = startIndex < 0 ? 1 : startIndex;

            string devKey = ConfigurationManager.AppSettings["ydevelopementkey"];

            var youtubeService = new Google.Apis.YouTube.v3.YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyDcELCYNkJcYuK9mlG3r2pzcm94x9d3kaU",//replace to devkey after main implementation to appsettings
                ApplicationName = "Strimm"//replace after main implementation to appsettings
            });


            var videoTubeModel = new List<VideoTubeModel>();
            string pageTokenByRequest = String.Empty;
            List<string> videos = new List<string>();
            List<string> channels = new List<string>();
            List<string> playlists = new List<string>();
            List<string> videoInfo = new List<string>();
            var videoPageModel = new VideoTubePageModel()
            {
                VideoTubeModels = new List<VideoTubeModel>()
            };
            try
            {
                        var searchListRequest = youtubeService.Search.List("snippet");
                        searchListRequest.Q = keywords; // Replace with your search term.
                        searchListRequest.MaxResults = 50;
                        searchListRequest.SafeSearch = SearchResource.ListRequest.SafeSearchEnum.Strict;
                        searchListRequest.Type = "video";
                        searchListRequest.VideoDefinition = SearchResource.ListRequest.VideoDefinitionEnum.High;
                        searchListRequest.VideoDuration = SearchResource.ListRequest.VideoDurationEnum.Long;
                        searchListRequest.VideoEmbeddable = SearchResource.ListRequest.VideoEmbeddableEnum.True;
                if(!String.IsNullOrEmpty(pageToken))
                {
                    searchListRequest.PageToken = pageToken;
                }
                        var searchListResponse = searchListRequest.Execute();
                        var videoIdStrings = String.Empty;
                
                    pageTokenByRequest = searchListResponse.NextPageToken;
               
                    
                        foreach (var searchResult in searchListResponse.Items)
                        {
                            switch (searchResult.Id.Kind)
                            {
                                case "youtube#video":
                                    videos.Add(String.Format("{0}", searchResult.Id.VideoId));

                                    

                                    break;

                                case "youtube#channel":
                                    channels.Add(String.Format("{0} ({1})", searchResult.Snippet.Title, searchResult.Id.ChannelId));
                                    break;

                                case "youtube#playlist":
                                    playlists.Add(String.Format("{0} ({1})", searchResult.Snippet.Title, searchResult.Id.PlaylistId));
                                    break;
                            }
                        }
                        foreach (var videoId in videos)
                        {

                            videoIdStrings += videoId + ",";
                        }
                                              
                        var searchedVideosList = youtubeService.Videos.List("contentDetails,snippet,status,statistics");
                        searchedVideosList.Id = videoIdStrings.TrimEnd(',');
                                      
                        var searchVideosRequest = searchedVideosList.Execute();
                        List<Video> videoList = searchVideosRequest.Items.ToList();
                       
                            foreach (var v in searchVideosRequest.Items)
                            {
                                TimeSpan youTubeDuration = XmlConvert.ToTimeSpan(v.ContentDetails.Duration.ToString());
                                double doubleDuration = youTubeDuration.TotalSeconds;  
                                videoInfo.Add(youTubeDuration.Seconds.ToString());
                                bool reagionRestriction = false;
                                //filter countryRestriction and embedable(on status if false ==restricted)
                                if (v.ContentDetails.RegionRestriction != null||v.Status.Embeddable==false||v.ContentDetails.CountryRestriction!=null)
                                {
                                    reagionRestriction = true;
                                }
                                else
                                {
                                    reagionRestriction = false;
                                }
                                var videoModel = new VideoTubeModel();
                                videoModel.CategoryId = 0;
                                videoModel.CategoryName = v.Snippet.CategoryId;//need to check how to get category name by this id
                                videoModel.Description = v.Snippet.Description;
                                videoModel.IsInChannel = false;
                                videoModel.IsInPublicLibrary = false;
                                videoModel.IsPrivate = (bool)v.Status.PublicStatsViewable; //bool? type in api
                                videoModel.IsRemovedByProvider = false;
                                videoModel.IsRestrictedByProvider = reagionRestriction;
                                videoModel.IsRRated = false;
                                videoModel.IsScheduled = false;
                                videoModel.ProviderVideoId = v.Id;
                                videoModel.Title = v.Snippet.Title;
                                videoModel.UseCounter = 0;
                                videoModel.VideoProviderId = 1;
                                videoModel.VideoProviderName = "YouTube";
                                videoModel.VideoTubeId = 0;
                                
                                if(v.Statistics.ViewCount!=null||v.Statistics.ViewCount!=0)
                                {
                                    videoModel.ViewCounter = Convert.ToInt32(v.Statistics.ViewCount); //ulong type in api
                                }
                                else
                                {
                                    videoModel.ViewCounter = 0;
                                }
                                videoModel.Thumbnail = v.Snippet.Thumbnails.Standard.Url;
                                videoModel.Duration = Convert.ToInt64(doubleDuration);
                                videoModel.DurationString = DateTimeUtils.PrintTimeSpan(videoModel.Duration);
                                videoTubeModel.Add(videoModel);
                            }

                            videoPageModel.VideoTubeModels.AddRange(videoTubeModel);
                            videoPageModel.NextPageIndex = startIndex + 50;
                            videoPageModel.PrevPageIndex = startIndex == 1 ? 1 : startIndex - 50;
                            videoPageModel.PageSize = 50;
                            videoPageModel.PageCount = 1000000 / 50;
                            videoPageModel.PageToken = pageTokenByRequest;

                
            }
            
            catch (Exception ex)
            {
                Logger.Error(String.Format("Error occured while searching for videos on YouTube using keywords '{0}' and pageToken={1}", keywords, pageToken), ex);
            }
            
                return videoPageModel;
           }

        public static VideoTubeModel GetVideoByUrl(string url)
        {
            if (String.IsNullOrEmpty(url))
            {
                Logger.Warn("Unable to retrieve YouTube video by URL. Invalid URL specified");
                return null;
            }

            VideoTubeModel videoModel = new VideoTubeModel();
            string youTubeVideoRegex = ConfigurationManager.AppSettings["YouTubeVideoRegex"].ToString();
            string devKey = ConfigurationManager.AppSettings["ydevelopementkey"];
            var youtubeService = new Google.Apis.YouTube.v3.YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyDcELCYNkJcYuK9mlG3r2pzcm94x9d3kaU",//replace to devkey after main implementation to appsettings
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


                    var searchVideosRequest = searchedVideosList.Execute();
                    List<Video> videoList = searchVideosRequest.Items.ToList();
                    Video video = new Video();
                    if (videoList.Count != 0)
                    {
                        video = videoList.First();
                    }


                    if (video != null)
                    {

                        TimeSpan youTubeDuration = XmlConvert.ToTimeSpan(video.ContentDetails.Duration.ToString());
                        double doubleDuration = youTubeDuration.TotalSeconds;

                        bool reagionRestriction = false;
                        if (video.ContentDetails.RegionRestriction != null)
                        {
                            reagionRestriction = true;
                        }
                        else
                        {
                            reagionRestriction = false;
                        }

                        videoModel.CategoryId = 0;
                        videoModel.CategoryName = video.Snippet.CategoryId;//need to check how to get category name by this id
                        videoModel.Description = video.Snippet.Description;
                        videoModel.IsInChannel = false;
                        videoModel.IsInPublicLibrary = false;
                        videoModel.IsPrivate = (bool)video.Status.PublicStatsViewable; //bool? type in api
                        videoModel.IsRemovedByProvider = false;
                        videoModel.IsRestrictedByProvider = reagionRestriction;
                        videoModel.IsRRated = false;
                        videoModel.IsScheduled = false;
                        videoModel.ProviderVideoId = video.Id;
                        videoModel.Title = video.Snippet.Title;
                        videoModel.UseCounter = 0;
                        videoModel.VideoProviderId = 1;
                        videoModel.VideoProviderName = "YouTube";
                        videoModel.VideoTubeId = 0;
                        if (video.Statistics.ViewCount != null || video.Statistics.ViewCount != 0)
                        {
                            videoModel.ViewCounter = Convert.ToInt32(video.Statistics.ViewCount); //ulong type in api
                        }
                        else
                        {
                            videoModel.ViewCounter = 0;
                        }
                        videoModel.Thumbnail = video.Snippet.Thumbnails.Standard.Url;
                        videoModel.Duration = Convert.ToInt64(doubleDuration);
                        videoModel.DurationString = DateTimeUtils.PrintTimeSpan(videoModel.Duration);

                        if (reagionRestriction)
                        {
                            Logger.Debug(String.Format("Media rating set on video w/ Id='{0}' to '{1}'. Video wil not be retrieved", video.Id, video.ContentDetails.ContentRating));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Error occured while retrieving YouTube video by URL='{0}'", url));
            }

            return videoModel;
        }

        public static void UpdateVideoStatus(List<VideoTubeModel> videos)
        {
            if (videos == null || videos.Count == 0)
            {
                return;
            }

            Logger.Info(String.Format("Updating video status of {0} YouTube videos", videos.Count));

            string devKey = ConfigurationManager.AppSettings["ydevelopementkey"];
            int batchSize = 50;

            var youtubeService = new Google.Apis.YouTube.v3.YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyDcELCYNkJcYuK9mlG3r2pzcm94x9d3kaU",//replace to devkey after main implementation to appsettings
                ApplicationName = "Strimm"//replace after main implementation to appsettings
            });
         //   var entries = new List<Video>();

           // Feed<Video> videoFeed = null;
         var videoIdStrings = String.Empty;

            try
            {
                int youTubeRequestCount = (videos.Count / batchSize) + (videos.Count % batchSize == 0 ? 0 : 1);

                for (int i = 0; i < youTubeRequestCount; i++)
                {
                    var videoRequestList = videos.Count < batchSize ? videos : videos.Skip(batchSize * i).Take(batchSize).ToList();
                    videoIdStrings = String.Empty;
                    videoRequestList.ForEach(x =>
                    {
                        videoIdStrings += x.ProviderVideoId + ",";
                    });
                    foreach(var videoReq in videoRequestList)
                    {
                        videoReq.IsRemovedByProvider = true;
                    }
                       var searchedVideosList = youtubeService.Videos.List("contentDetails,snippet,status,statistics");
                   
                        searchedVideosList.Id = videoIdStrings.TrimEnd(',');
                                      
                        var searchVideosRequest = searchedVideosList.Execute();
                        List<Video> videoList = searchVideosRequest.Items.ToList();
                   //videoFeed = request.Batch<Video>(videoRequestList, new Uri("https://gdata.youtube.com/feeds/api/videos/batch?v=2"), GDataBatchOperationType.query);

                        if ((videoRequestList != null) || (videoRequestList.Count != 0))
                    {

                        foreach (var e in videoList)
                        {
                            var video = videoRequestList.FirstOrDefault(x => x.ProviderVideoId == e.Id);

                            if (video != null)
                            {
                                

                                if (e != null)
                                {
                                    if (e.Status != null && e.ContentDetails.CountryRestriction!=null||e.ContentDetails.RegionRestriction!=null||e.Status.Embeddable==false)
                                    {
                                        video.IsRemovedByProvider = false;
                                        video.IsRestrictedByProvider = true;
                                        Logger.Debug(String.Format("Video '{0}' was marked as restricted", e.Id));
                                    }
                                    else
                                    {
                                        video.IsRestrictedByProvider = false;
                                        video.IsRemovedByProvider = false;
                                    }
                                }

                                //if (mediaRating != null)
                                //{
                                //    Logger.Debug(String.Format("Video '{0}' has media rating set by YouTube to '{1}'. Marking video as restricted", e.Id, mediaRating));
                                //    video.IsRestrictedByProvider = true;
                                //}????
                            }
                            
                            else
                            {
                              
                                video.IsRemovedByProvider=true;                            
                                Logger.Warn(String.Format("Video with Provider Id='{0}' was not part of the original request and will be ignored", e.Id));
                            }
                        };
                    }
                    else
                    {
                        Logger.Warn("Unable to retrieve data from YouTube. VideoFeed was not returned");
                    }
                }
            }
            catch (NullReferenceException objex)
            {
                Logger.Error("Error occured while making YouTube batch request", objex);
            }
            catch (Exception ex)
            {
                Logger.Error("Error occured while processing YouTube batch request", ex);
            }
          
        }

        /// <summary>
        /// This method will check if a specified video was removed by provider
        /// </summary>
        /// <param name="video">Instance of VideoTubeModel</param>
        /// <returns>True/False</returns>
        //public static bool CheckIfVideoWasRemovedByProvider(VideoTubeModel video)
        //{
        //    if (video == null)
        //    {
        //        return false;
        //    }

        //    Logger.Info(String.Format("Checking if video Id='{0}' was removed by provider", video.VideoTubeId));

        //    string devKey = ConfigurationManager.AppSettings["ydevelopementkey"];
        //    var settings = new YouTubeRequestSettings("development", devKey);
        //    var request = new YouTubeRequest(settings);
        //    var videoEntryUri = new Uri("http://gdata.youtube.com/feeds/api/videos/" + video.ProviderVideoId);

        //    bool wasRemoved = false;

        //    try
        //    {
        //        var youTubeVideo = request.Retrieve<Video>(videoEntryUri);
        //        wasRemoved = youTubeVideo == null;
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error(String.Format("Error occured while checking if video with Id={0} was removed by YouTube", video.VideoTubeId), ex);
        //    }

        //    return wasRemoved;
        //}
        
    }
}
