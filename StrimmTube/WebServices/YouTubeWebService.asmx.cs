using Google.GData.Client;
using Google.GData.YouTube;
using Google.YouTube;
using StrimmBL;
using Strimm.Model;
using StrimmTube.UC;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using Strimm.Model.Projections;
using Strimm.Model.WebModel;
using log4net;
namespace StrimmTube.WebServices
{
    /// <summary>
    /// Summary description for YouTubeWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class YouTubeWebService : System.Web.Services.WebService
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(YouTubeWebService));

        #region Public Methods

        [WebMethod]
        public VideoTubePageModel FindVideosByKeywords(string searchString, int startIndex, int channelTubeId, string pageToken, int providerId, bool isLong)
        {
            var channel = ChannelManage.GetChannelTubeById(channelTubeId);
            var user = channel != null ? UserManage.GetUserPoByUserId(channel.UserId) : null;

            // Disabled for search by keywords
            bool isMatureContentAllowed = false; // user != null ? user.MatureContentAllowed : false;
            
            var videoPageModel = (providerId < 2)
                                    ? YouTubeServiceManage.GetVideosByKeywords(searchString, pageToken, startIndex, isLong, isMatureContentAllowed)
                                    : VimeoServiceManage.GetVideosByKeywords(searchString, startIndex, isLong, isMatureContentAllowed);

            var videosInChannel = VideoTubeManage.GetVideoTubesForChannel(channelTubeId);

            if (videoPageModel != null)
            {
                videoPageModel.VideoTubeModels.AsParallel().ForAll(v => 
                {
                    if (v != null) 
                    {
                        v.IsInChannel = videosInChannel.Any(x => x != null && x.ProviderVideoId == v.ProviderVideoId);
                    }
                });
            }

            return videoPageModel;
        }


        [WebMethod]
        public VideoTubePageModel GetVideosById(string videoIds, int startIndex, string pageToken, int channelTubeId)
        {
            List<string> videoIdsList = videoIds.Split(',').ToList();

            var channel = ChannelManage.GetChannelTubeById(channelTubeId);
            var user = channel != null ? UserManage.GetUserByChannelName(channel.Name) : null;
            var allowMatureContent = user != null ? user.MatureContentAllowed : false;

            VideoTubePageModel videoPageModel = YouTubeServiceManage.GetVideosById(videoIdsList, startIndex, pageToken, allowMatureContent);

            foreach (var video in videoPageModel.VideoTubeModels)
            {
                var videosInChannel = VideoTubeManage.GetVideoTubesForChannel(channelTubeId);

                video.IsInChannel = videosInChannel.Any(x => x != null && x.ProviderVideoId == video.ProviderVideoId);
            }

            return videoPageModel;
        }
        
        [WebMethod]
        public VideoTubeModel FindVideoByUrl(string url, int channelTubeId, int providerId)
        {
            var channel = ChannelManage.GetChannelTubeById(channelTubeId);
            var user = channel != null ? UserManage.GetUserByChannelName(channel.Name) : null;

            bool allowMatureContent = user != null ? user.MatureContentAllowed : false;

            var video = (providerId < 2)
                                    ? YouTubeServiceManage.GetVideoByUrl(url, allowMatureContent)
                                    : VimeoServiceManage.GetVideoByUrl(url, allowMatureContent);

            if (video != null)
            {
                var videosInChannel = VideoTubeManage.GetVideoTubesForChannel(channelTubeId);

                video.IsInChannel = videosInChannel.Any(x => x != null && x.ProviderVideoId == video.ProviderVideoId);
            }

            return video;
        }

        #endregion

        //#region Depricated Methods

        //[WebMethod]
        //public VideoTubeModel AddVideoToChannel(VideoTubeModel videoModel, int channelTubeId, int categoryId)
        //{
        //    return null; // YouTubeServiceManage.AddVideoToChannel(videoModel, channelTubeId, categoryId);
        //}

        //[WebMethod(EnableSession = true)]
        //public void ClearSession()
        //{
        //    if (Session["videoList"] != null)
        //    {
        //        Session["videoList"] = null;
        //    }
        //    if (Session["startIndex"] != null)
        //    {
        //        Session["startIndex"] = null;
        //    }
        //    if (Session["startIndexPubLib"] != null)
        //    {
        //        Session["startIndexPubLib"] = null;
        //    }
        //}

        //private static readonly Regex YoutubeVideoRegex = new Regex(@"youtu(?:\.be|be\.com)/(?:.*v(?:/|=)|(?:.*/)?)([a-zA-Z0-9-_]+)", RegexOptions.IgnoreCase);

        //[WebMethod(EnableSession = true)]
        //public string GetVideoByUrl(string url, string uniqueId)
        //{
        //    //List<Video> videoList = new List<Video>();
        //    //if (Session["videoList"] != null)
        //    //{
        //    //    videoList=(List<Video>)Session["videoList"];
        //    //}
        //    // create uri from url and retrieve video
        //    Match youtubeMatch = YoutubeVideoRegex.Match(url);
        //    string id = String.Empty;
        //    if (youtubeMatch.Success)
        //    {
        //        id = youtubeMatch.Groups[1].Value;
        //        string uri = "http://gdata.youtube.com/feeds/api/videos/" + id;
        //        string devKey = ConfigurationManager.AppSettings["ydevelopementkey"];
        //        YouTubeRequestSettings settings = new YouTubeRequestSettings("development", devKey);
        //        YouTubeRequest request = new YouTubeRequest(settings);

        //        string guid = uniqueId;
        //        Uri videoentryUrl = new Uri(uri);
        //        Video video = new Video();
        //        try
        //        {
        //            video = request.Retrieve<Video>(videoentryUrl);
        //        }
        //        catch
        //        {
        //            return "<span id='notValid'>url not valid</span>";
        //        }
        //        if (video != null)
        //        {
        //            //videoList.Add(video);
        //            //videoList.Reverse();
        //            Page page = new Page();
        //            StringBuilder sb = new StringBuilder();
        //            //foreach (var v in videoList)
        //            //{
        //            //get title
        //            string title = video.Title;
        //            //get description
        //            string description = video.Description;
        //            //get duration
        //            double duration = 0;
        //            foreach (var mediacontent in video.Contents)
        //            {
        //                duration = Convert.ToDouble(mediacontent.Duration);
        //            }
        //            //get viewes
        //            long views = video.ViewCount;
        //            //get thumbnail of video;
        //            string thumbnailUrl = "";
        //            foreach (var thumbnail in video.Thumbnails)
        //            {
        //                thumbnailUrl = thumbnail.Url;
        //            }
        //            //build user control
        //            string path = "~/UC/SearchedVideoBoxUC.ascx";
        //            SearchedVideoBoxUC videoBoxCtrl = (SearchedVideoBoxUC)page.LoadControl(path);
        //            double dur = TimeSpan.FromSeconds(duration).TotalMinutes;

        //            videoBoxCtrl.duration = ScheduleManage.PrintTimeSpan(dur);
        //            videoBoxCtrl.doubleDuration = dur;
        //            videoBoxCtrl.originalTitle = title;
        //            videoBoxCtrl.originalDescriptiom = description;
        //            videoBoxCtrl.srcImage = thumbnailUrl;
        //            videoBoxCtrl.views = views.ToString("#,##0");
        //            videoBoxCtrl.actionId = "action_" + guid.ToString();
        //            videoBoxCtrl.txtCustomizeDescription = "txtCustomizeDescription_" + guid.ToString();
        //            videoBoxCtrl.txtCustomTitle = "txtCustomTitle_" + guid.ToString();
        //            videoBoxCtrl.closeId = "url" + id;
        //            videoBoxCtrl.addToSchedule = "addToSchedule_" + guid.ToString();
        //            videoBoxCtrl.addToVr = "addToVr_0";
        //            videoBoxCtrl.boxContentId = "boxContent_" + guid.ToString();
        //            videoBoxCtrl.countDesc = "countDesc_" + guid.ToString();
        //            videoBoxCtrl.countTitle = "countTitle_" + guid.ToString();
        //            videoBoxCtrl.playId = id;
        //            videoBoxCtrl.addId = "addId_" + guid.ToString();
        //            videoBoxCtrl.selectId = "selectId_" + guid.ToString();
        //            if (video.Status != null)
        //            {
        //                if (video.Status.Name == "restricted")
        //                {
        //                    videoBoxCtrl.isRestricted = true;

        //                }
        //                else
        //                {
        //                    videoBoxCtrl.isRestricted = false;
        //                }
        //            }
        //            StringWriter output = new StringWriter(sb);
        //            HtmlTextWriter hw = new HtmlTextWriter(output);
        //            videoBoxCtrl.RenderControl(hw);
        //            //}
        //            // Session["videoList"] = videoList;
        //            return sb.ToString();
        //        }
        //        else
        //        {
        //            return "the video wasnt found";
        //        }
        //    }
        //    else
        //    {
        //        return "<span id='notValid'>url not valid</span>";
        //    }
        //    //string[] strUrl = url.Replace("//", "/").Split('/');
        //    //string id = strUrl.Last();
        //}
        
        //public List<Video> GetAllVideosByKeyWord(string keyword, int startIndex)
        //{
           
        //    List<Video> videoList = new List<Video>();
            
        //        string devKey = ConfigurationManager.AppSettings["ydevelopementkey"];
        //        YouTubeRequestSettings settings = new YouTubeRequestSettings("development", devKey);
        //        YouTubeRequest request = new YouTubeRequest(settings);
        //        string uri = "http://gdata.youtube.com/feeds/api/videos?q=" + keyword + "&start-index=" + startIndex + "&max-results=50&videoDuration=long&videoDefinition=high&safeSearch=moderate&v=2";
        //        YouTubeQuery query = new YouTubeQuery(uri);
        //        //query.NumberToRetrieve = 50;
        //        // int numbToRetrieve = query.NumberToRetrieve;
        //        // query.SafeSearch = YouTubeQuery.SafeSearchValues.None;

        //       // query.StartIndex = i;
        //         //AtomCategory category1 = new AtomCategory("News", YouTubeNameTable.CategorySchema);
        //      //  AtomCategory category2 = new AtomCategory(keyword, YouTubeNameTable.KeywordSchema);
        //        //AtomCategory category3 = new AtomCategory(keyword, YouTubeNameTable.KeywordSchema);
        //         //query.Categories.Add(new QueryCategory(category1));
        //        //query.OrderBy = "viewCount";

        //        // query.OrderBy = "duration";
        //       // query.Categories.Add(new QueryCategory(category2.UriString, QueryCategoryOperator.OR));
        //        // query.ExtraParameters = "filters=long&lclk=long";
        //        //string querstr = query.ToString();
        //       // query.Categories.Add(new QueryCategory(category3));
            
        //        //string querstr = query.ToString();
        //        Feed<Video> videoFeed= request.Get<Video>(query);
        //        int count = videoFeed.Entries.Count();
        //        List<Video> longestFirst = new List<Video>();
        //        if (count != 0)
        //        {
        //            foreach (var v in videoFeed.Entries)
        //            {
        //                videoList.Add(v);
        //            }

        //           longestFirst = videoList.Distinct().OrderByDescending(x => int.Parse(x.Media.Duration.Seconds)).ToList();
        //        }
        //    return longestFirst;
          
        //}

        //[WebMethod(EnableSession = true)]
        //public string GetVideoByKeyWord(string keyword, string durationVal, int startIndex)
        //{
        //    #region oldCode
        //    // string devKey = ConfigurationManager.AppSettings["ydevelopementkey"];
        //   // YouTubeRequestSettings settings = new YouTubeRequestSettings("development", devKey);
        //   // YouTubeRequest request = new YouTubeRequest(settings);
        //   // YouTubeQuery query = new YouTubeQuery(YouTubeQuery.DefaultVideoUri);
        //   // query.NumberToRetrieve = 50;
        //   //// int numbToRetrieve = query.NumberToRetrieve;
        //   // // query.SafeSearch = YouTubeQuery.SafeSearchValues.None;

        //   // query.StartIndex = startIndex;
        //   // // AtomCategory category1 = new AtomCategory("News", YouTubeNameTable.CategorySchema);
        //   // AtomCategory category2 = new AtomCategory(keyword, YouTubeNameTable.KeywordSchema);
        //   // // AtomCategory category3 = new AtomCategory(keyword, YouTubeNameTable.KeywordSchema);
        //   // // query.Categories.Add(new QueryCategory(category1));
        //   // //query.OrderBy = "viewCount";

        //   // // query.OrderBy = "duration";
        //   // query.Categories.Add(new QueryCategory(category2.UriString, QueryCategoryOperator.OR));
        //   // query.ExtraParameters = "filters=long&lclk=long";
        //   // string querstr = query.ToString();
        //   // //query.Categories.Add(new QueryCategory(category3));
        //   // List<Video> videoList = new List<Video>();
        //   // Feed<Video> videoFeed = request.Get<Video>(query);

        //   // if (videoFeed != null)
        //   // {
        //   //     if (Session["videoList"] != null)
        //   //     {
        //   //         Session["videoList"] = null;
        //   //     }
        //   //     foreach (var v in videoFeed.Entries)
        //   //     {
        //   //         // int count = videoFeed.Entries.;
        //   //         double duration = 0;
        //   //         foreach (var mediacontent in v.Contents)
        //   //         {
        //   //             duration = Convert.ToDouble(mediacontent.Duration);
        //   //         }
        //   //         #region selectedDuration
        //   //         switch (durationVal)
        //   //         {
        //   //             case "0":
        //   //                 videoList.Add(v);
        //   //                 break;
        //   //             case "1":
        //   //                 if (duration <= 300)
        //   //                 {
        //   //                     videoList.Add(v);
        //   //                 }
        //   //                 break;
        //   //             case "2":
        //   //                 if (duration >= 300 && duration <= 900)
        //   //                 {
        //   //                     videoList.Add(v);
        //   //                 }
        //   //                 break;
        //   //             case "3":
        //   //                 if (duration >= 1800 && duration <= 3600)
        //   //                 {
        //   //                     videoList.Add(v);
        //   //                 }
        //   //                 break;
        //   //             case "4":
        //   //                 if (duration >= 3600)
        //   //                 {
        //   //                     videoList.Add(v);
        //   //                 }
        //   //                 break;
        //   //         }
        //   //         #endregion
        //   //     }
        //   // }
        //   // else
        //   // {
        //   //     //create user control for message 
        //   //     return "0";
        //    // }
        //    #endregion
        //    int userId = 0;
        //    int vrId = 0;
        //    if(HttpContext.Current.Session["userId"]!=null)
        //    {
        //        userId = int.Parse(HttpContext.Current.Session["userId"].ToString());
        //        vrId = VideoRoomTubeManage.GetVideoRoomTubeIdByUserId(userId);
        //    }
        //    List<Video> videoList = new List<Video>();

        //    if (startIndex > 1)
        //    {
        //        startIndex = startIndex * 50 - 50;

        //    }
        //    else
        //    {
        //        startIndex = 1;
        //    }
        //        videoList = GetAllVideosByKeyWord(keyword, startIndex);
               
            
        //    //StringBuilder sb = new StringBuilder();
        //    string output = "";

        //    if(videoList.Count!=0)
        //    {
        //        output = BuildSearchBoxControl(videoList,vrId);


        //        Session["videoList"] = videoList;
        //        if (Session["startIndex"] == null)
        //        {
        //            Session["startIndex"] = 1;
        //        }
                
        //    }
        //    else
        //    {
        //        output = "0";
        //    }

        //    return output;
        //}

        //public string  BuildSearchBoxControl(List<Video> videoList,int vrId)
        //{
        //    Page page = new Page();
        //    StringBuilder sb = new StringBuilder();
        //    foreach (var v in videoList)
        //    {
        //        //get title
        //        string title = v.Title;
        //        //get description
        //        string description = v.Description;
        //        double duration = 0;
        //        foreach (var mediaContent in v.Contents)
        //        {
        //            duration = Convert.ToDouble(mediaContent.Duration);
        //        }
        //        //get viewes
        //        long views = v.ViewCount;
        //        //get thumbnail of video;
        //        string thumbnailUrl = "";
        //        foreach (var thumbnail in v.Thumbnails)
        //        {
        //            thumbnailUrl = thumbnail.Url;
        //        }
        //        //build user control
        //        string path = "~/UC/SearchedVideoBoxUC.ascx";
        //        SearchedVideoBoxUC videoBoxCtrl = (SearchedVideoBoxUC)page.LoadControl(path);
        //        double dur = TimeSpan.FromSeconds(duration).TotalMinutes;
        //        videoBoxCtrl.duration = ScheduleManage.PrintTimeSpan(dur); //Math.Round(dur, 1, MidpointRounding.AwayFromZero); // TimeSpan.FromSeconds(duration).TotalMinutes;
        //        videoBoxCtrl.doubleDuration = dur;
        //        videoBoxCtrl.originalTitle = title;
        //        videoBoxCtrl.originalDescriptiom = description;
        //        videoBoxCtrl.srcImage = thumbnailUrl;
        //        videoBoxCtrl.views = views.ToString("#,##0");
        //        videoBoxCtrl.actionId = "action_" + videoList.IndexOf(v).ToString();
        //        videoBoxCtrl.txtCustomizeDescription = "txtCustomizeDescription_" + videoList.IndexOf(v).ToString();
        //        videoBoxCtrl.txtCustomTitle = "txtCustomTitle_" + videoList.IndexOf(v).ToString();
        //        videoBoxCtrl.closeId = videoList.IndexOf(v).ToString();
        //        videoBoxCtrl.addToSchedule = "addToSchedule_" + videoList.IndexOf(v).ToString();
        //        videoBoxCtrl.addToVr = "addToVr_" + videoList.IndexOf(v).ToString();
        //        videoBoxCtrl.boxContentId = "boxContent_" + videoList.IndexOf(v).ToString();
        //        videoBoxCtrl.countDesc = "countDesc_" + videoList.IndexOf(v).ToString();
        //        videoBoxCtrl.countTitle = "countTitle_" + videoList.IndexOf(v).ToString();
        //        videoBoxCtrl.playId = v.VideoId.ToString();
        //        videoBoxCtrl.durInt = (int)Math.Round(dur);
        //        videoBoxCtrl.viewsInt = views;
        //        videoBoxCtrl.isInVr = VideoTubeManage.IsVideoTubePartOfVideoRoomTube(vrId, v.VideoId.ToString());
        //        var entry = v.YouTubeEntry;
        //        videoBoxCtrl.addId = "addId_" + videoList.IndexOf(v).ToString();
        //        videoBoxCtrl.selectId = "selectId_" + videoList.IndexOf(v).ToString();
        //        if (videoList.IndexOf(v) <= 24)
        //        {
        //            videoBoxCtrl.side = "left";
        //        }
        //        else
        //        {
        //            videoBoxCtrl.side = "right";
        //        }
        //        if (v.Status != null)
        //        {
        //            if (v.Status.Name == "restricted")
        //            {
        //                videoBoxCtrl.isRestricted = true;

        //            }
        //            else
        //            {
        //                videoBoxCtrl.isRestricted = false;
        //            }
        //        }
        //        StringWriter output = new StringWriter(sb);
        //        HtmlTextWriter hw = new HtmlTextWriter(output);
        //        videoBoxCtrl.RenderControl(hw);
        //    }
        //    return sb.ToString();
        //}
        
        //[WebMethod(EnableSession = true)]
        //public string GetMoreResults(string keyword, string durationVal, string countIndex)
        //{
        //    Session["startIndex"] = countIndex;
        //    var response = GetVideoByKeyWord(keyword, durationVal, int.Parse(countIndex));
        //    return response;
        //}

        
        //[WebMethod(EnableSession = true)]
        //public void RemoveVideo(string videoIndex)
        //{
        //    List<Video> videoList = new List<Video>();
        //    if (Session["videoList"] != null)
        //    {
        //        videoList = (List<Video>)Session["videoList"];
        //    }
        //    videoList.RemoveAt(int.Parse(videoIndex));
        //    Session["videoList"] = videoList;
        //}
        
        //[WebMethod(EnableSession = true)]
        //public string SortVideos(string value)
        //{
        //    List<Video> videoList = new List<Video>();
        //    string output = "";
        //    int vrid=0;
        //    if(HttpContext.Current.Session["userId"]!=null)
        //    {
        //        int userId = int.Parse(HttpContext.Current.Session["userId"].ToString());
        //        vrid=VideoRoomTubeManage.GetVideoRoomTubeIdByUserId(userId);
        //    }
        //    if (Session["videoList"] != null)
        //    {
        //        videoList = Session["videoList"] as List<Video>;
        //        switch (value)
        //        {

        //            case "1":
        //                List<Video> duartionDesc = videoList.OrderByDescending(x => int.Parse(x.Media.Duration.Seconds)).ToList();
                        
        //                output = BuildSearchBoxControl(duartionDesc,vrid);
        //                Session["videoList"] = duartionDesc;

        //                break;
        //            case "2":
        //                var duartionAcs = videoList.OrderBy(x => int.Parse(x.Media.Duration.Seconds)).ToList();

        //                output = BuildSearchBoxControl(duartionAcs,vrid);
        //                Session["videoList"] = duartionAcs;

        //                break;
        //            case "3":
        //                var mostViewed = videoList.OrderByDescending(x => x.ViewCount).ToList();
        //                output = BuildSearchBoxControl(mostViewed,vrid);
        //                Session["videoList"] = mostViewed;
        //                break;
        //        }
        //        return output;
        //    }
        //    else
        //    {
        //        return "you dont have any videos to sort";
        //    }


           

        //}
          
        ////TODO
        ////[WebMethod(EnableSession = true)]
        ////public string PostScheduleList(ScheduleList scheduleList)
        ////{
        ////    //check if schedule already in database; 
        ////    ScheduleList list = scheduleList;
        ////    int userId = 0;
        ////    if (HttpContext.Current.Session["userId"] != null)
        ////    {
        ////        userId = int.Parse(HttpContext.Current.Session["userId"].ToString());
        ////        list.userId = userId;
        ////        list.videoViews = 0;
        ////        list.isInVideoRoom = false;
        ////    }
        ////    int channelId = int.Parse(HttpContext.Current.Session["channelTubeId"].ToString());
        ////    bool isExist = ScheduleManage.checkIfScheduleListExist(userId, list.videoId, channelId);
        ////    if (!isExist)
        ////    {

        ////        //ScheduleManage.addScheduleList(list);
        ////        return "video has been added to schedule";
        ////    }
        ////    else
        ////    {
        ////        return "video already in schedule";
        ////    }
        ////}

        //[WebMethod(EnableSession = true)]
        //public string AddVideoTube(VideoTube videoTube)
        //{
        //    //check if schedule already in database; 

        //    int userId = 0;
        //    int vrId = 0;
        //    if (HttpContext.Current.Session["userId"] != null)
        //    {
        //        userId = int.Parse(HttpContext.Current.Session["userId"].ToString());
        //        vrId = VideoRoomTubeManage.GetVideoRoomTubeIdByUserId(userId);

        //    }
        //    bool isExist = VideoTubeManage.IsVideoTubePartOfVideoRoomTube(vrId, videoTube.ProviderVideoId);
        //    if (!isExist)
        //    {
        //        VideoTube vt = videoTube;
        //        vrId = VideoRoomTubeManage.GetVideoRoomTubeIdByUserId(userId);
        //       // videoTube.videoRooomId = vrId;
        //       // VideoTubeManage.AddVideoTube(videoTube);
        //        return "video has been added to video room";
        //    }
        //    else
        //    {
        //        return "video already in video room";
        //    }
        //}

        //#endregion

        //#region Private Methods

        //internal bool CheckIfVideoRemoved(VideoTube t)
        //{
        //    string devKey = ConfigurationManager.AppSettings["ydevelopementkey"];
        //    YouTubeRequestSettings settings = new YouTubeRequestSettings("development", devKey);
        //    YouTubeRequest request = new YouTubeRequest(settings);
        //    Uri videoEntryUri = new Uri("http://gdata.youtube.com/feeds/api/videos/" + t.ProviderVideoId);
        //    Video video = null;
        //    try
        //    {
        //        video = request.Retrieve<Video>(videoEntryUri);

        //    }
        //    catch (Exception ex)
        //    {
        //        string message = ex.ToString();
        //    }
        //    if (video != null)
        //    {
        //        return false;
        //    }



        //    else
        //    {
        //        return true;
        //    }
        //}
        
        //internal void GetYoutubeFeed(List<VideoTube> list)
        //{
        //    string devKey = ConfigurationManager.AppSettings["ydevelopementkey"];
        //    YouTubeRequestSettings settings = new YouTubeRequestSettings("development", devKey);
        //    YouTubeRequest request = new YouTubeRequest(settings);
        //    string multiRequiest = "";
        //    if (list.Count != 0)
        //    {
        //        foreach (var l in list)
        //        {
        //            multiRequiest += l.ProviderVideoId + '|';
        //        }
        //    }
        //    multiRequiest = multiRequiest.TrimEnd('|');

        //    string videoEntryUri = "http://gdata.youtube.com/feeds/api/videos?q=" + multiRequiest;


        //    YouTubeQuery query = new YouTubeQuery(videoEntryUri);
        //    Feed<Video> videoFeed = request.Get<Video>(query);
        //    List<Video> videoList = new List<Video>();
        //    foreach (var v in videoFeed.Entries)
        //    {
        //        if (v != null)
        //        {
        //            videoList.Add(v);
        //        }
        //    }

        //}
        
        //internal string VideoRemovedOrRestricted(VideoTube t)
        //{
        //    string devKey = ConfigurationManager.AppSettings["ydevelopementkey"];
        //    YouTubeRequestSettings settings = new YouTubeRequestSettings("development", devKey);
        //    YouTubeRequest request = new YouTubeRequest(settings);
        //    Uri videoEntryUri = new Uri("http://gdata.youtube.com/feeds/api/videos/" + t.ProviderVideoId);
        //    Video video = null;
        //    string removedOrRestricted = "";

        //    try
        //    {
        //        video = request.Retrieve<Video>(videoEntryUri);
        //    }
        //    catch (Exception ex)
        //    {
        //        string message = ex.ToString();
        //    }
        //    if (video != null)
        //    {
        //        if (video.Status != null)
        //        {
        //            if (video.Status.Name == "restricted")
        //            {
        //                removedOrRestricted = "restricted";
        //            }
        //        }
        //    }
        //    else
        //    {
        //        removedOrRestricted = "removed";
        //    }
        //    return removedOrRestricted;



        //}
        
        //internal bool IsVideoRestricted(VideoTube t)
        //{
        //    string devKey = ConfigurationManager.AppSettings["ydevelopementkey"];
        //    YouTubeRequestSettings settings = new YouTubeRequestSettings("development", devKey);
        //    YouTubeRequest request = new YouTubeRequest(settings);
        //    Uri videoEntryUri = new Uri("http://gdata.youtube.com/feeds/api/videos/" + t.ProviderVideoId);
        //    Video video = null;
        //    bool isrestricred = false;
        //    try
        //    {
        //        video = request.Retrieve<Video>(videoEntryUri);
        //    }
        //    catch (Exception ex)
        //    {
        //        string message = ex.ToString();
        //    }
        //    if (video != null)
        //    {
        //        if (video.Status != null)
        //        {
        //            if (video.Status.Name == "restricted")
        //            {
        //                isrestricred = true;
        //            }
        //            else
        //            {
        //                isrestricred = false;
        //            }
        //        }


        //    }
        //    return isrestricred;




        //}
        ////public Feed<Video> MakeBatchRequest(List<VideoTube> videosToUpdate)
        ////{
        ////    YouTubeRequestSettings settings = new YouTubeRequestSettings("development", "AIzaSyAREOn3e0gbN55UZfe32jKLEp2Niv63fCI");
        ////    YouTubeRequest request = new YouTubeRequest(settings);
        ////    int batchSize = 50;
        ////    int countFiftyRequests = (videosToUpdate.Count / batchSize) + (videosToUpdate.Count % batchSize == 0 ? 0 : 1);
        ////    List<Video> entries = new List<Video>();
        ////    Feed<Video> videoFeed;
        ////    List<VideoTube> ListToReturn = new List<VideoTube>();
        ////    videosToUpdate.ForEach(x =>
        ////    {
        ////        Video video = new Video() { Id = String.Format("http://gdata.youtube.com/feeds/api/videos/{0}", x.videoPath) };
        ////        entries.Add(video);
        ////    });
        ////    videoFeed = request.Batch<Video>(entries, new Uri("https://gdata.youtube.com/feeds/api/videos/batch?v=2"), GDataBatchOperationType.query);
        ////    List<VideoTube> videoTubeList = new List<VideoTube>();
        ////    foreach(var v in videoFeed.Entries)
        ////    {
        ////        VideoTube tube = new VideoTube();
        ////        VideoTube query = videosToUpdate.Where(x => x.videoPath.Contains(v.VideoId)).SingleOrDefault();
        ////        if(v.Status!=null)
        ////        {
        ////            if(v.Status.Name=="restricted")
        ////            {
        ////                query.isRestricted = true;
        ////            }
        ////            else
        ////            {
        ////                query.isRestricted = false;
        ////            }
        ////        }


        ////        ListToReturn.Add(query);


        ////    }
        ////    List<VideoTube> intersect = videosToUpdate.Intersect(ListToReturn).ToList();

        ////     return request.Batch<Video>(entries, new Uri("https://gdata.youtube.com/feeds/api/videos/batch?v=2"), GDataBatchOperationType.query);
        ////}
       
        //public List<VideoTubePo> MakeBatchRequest(List<VideoTubePo> videosToUpdate)
        //{
        //    List<VideoTubePo> newList = new List<VideoTubePo>();
        //    foreach (var v in videosToUpdate)
        //    {
        //        v.IsRemovedByProvider = true;
        //        newList.Add(v);
        //    }
        //    string devKey = ConfigurationManager.AppSettings["ydevelopementkey"];
        //    YouTubeRequestSettings settings = new YouTubeRequestSettings("development", devKey);
        //    YouTubeRequest request = new YouTubeRequest(settings);
        //    int batchSize = 50;
        //    List<Video> entries = new List<Video>();
        //    List<Video> entriesBy50 = new List<Video>();
        //    Feed<Video> videoFeed;

        //    newList.ForEach(x =>
        //    {
        //        Video video = new Video() { Id = String.Format("http://gdata.youtube.com/feeds/api/videos/{0}", x.ProviderVideoId) };
        //        entries.Add(video);
        //    });

        //    int countFiftyRequests = (entries.Count / batchSize) + (entries.Count % batchSize == 0 ? 0 : 1);
        //    //List<VideoTube> videosFromBatch = new List<VideoTube>();
        //    List<VideoTubePo> listToReturn = new List<VideoTubePo>();
        //    for (int i = 0; i < countFiftyRequests; i++)
        //    {//videosToUpdate.Skip(batchSize*i).Take(batchSize).ToList()
        //        List<Video> listForeach = entries.Skip(batchSize * i).Take(batchSize).ToList();
        //        videoFeed = request.Batch<Video>(listForeach, new Uri("https://gdata.youtube.com/feeds/api/videos/batch?v=2"), GDataBatchOperationType.query);
              
        //        foreach (var e in videoFeed.Entries)
        //        {
                  
        //            if (e.Content != "Video not found")
        //            {
        //                var query = newList.FirstOrDefault(x => x.ProviderVideoId == e.VideoId);
        //                var title = query.Title;
        //                if (e.Status != null)
        //                {

        //                    if (e.Status.Name == "restricted")
        //                    {

        //                        query.IsRemovedByProvider = false;
        //                        query.IsRestrictedByProvider = true;
        //                        listToReturn.Add(query);
        //                    }

        //                }
        //                else
        //                {
        //                    query.IsRemovedByProvider = false;
        //                    query.IsRestrictedByProvider = false;
        //                    listToReturn.Add(query);
        //                }
        //            }

        //        }
        //    }

           
        //  //  listToReturn = newList.ToList();

        //    //return request.Batch<Video>(entries, new Uri("https://gdata.youtube.com/feeds/api/videos/batch?v=2"), GDataBatchOperationType.query);
        //    return listToReturn;
        //}

        //[WebMethod(EnableSession = true)]
        //public string AddToVrFromPubliLib(int publicId)
        //{


        //    if (HttpContext.Current.Session["userId"] != null)
        //    {
        //        //int userId = int.Parse(HttpContext.Current.Session["userId"].ToString());
        //        //int vrId = VideoRoomTubeManage.GetVideoRoomTubeIdByUserId(userId);
        //        //PublicLib publicTube = PublicLibraryManage.GetVideoTubeById(publicId);
        //        //VideoTube tube = new VideoTube();
        //        //tube.addedDate = DateTime.Now;
        //        //tube.category = publicTube.category;
        //        //tube.categoryId = publicTube.categoryId;
        //        //tube.description = publicTube.description;
        //        //tube.duration = publicTube.duration;
        //        //tube.isRemoved = false;
        //        //tube.isRestricted = false;
        //        //tube.isScheduled = false;
        //        //tube.provider = publicTube.provider;
        //        //tube.r_rated = publicTube.r_rated;
        //        //tube.title = publicTube.title;
        //        //tube.useCount = 0;
        //        //tube.videoCount = 0;
        //        //tube.videoPath = publicTube.videoPath;
        //        //tube.videoThumbnail = publicTube.videoThumbnail;
        //        //VideoTubeManage.AddVideoTubeToVr(vrId, tube);
        //        return "the video is succefuly added to videoroom";
        //    }
        //    else
        //    {
        //        return "please try agin later";
        //    }





        //}

        //#endregion
    }
}
