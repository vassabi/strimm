using Strimm.Data.Repositories;
using StrimmBL;
using Strimm.Model;
using StrimmTube.UC;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using Strimm.Model.Projections;
using Strimm.Model.WebModel;
using Strimm.Shared;
using Strimm.Model.Criteria;

namespace StrimmTube.WebServices
{
    /// <summary>
    /// Summary description for PublicLibraryService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class PublicLibraryService : System.Web.Services.WebService
    {

        [WebMethod(EnableSession = true)]
        public string AddVideoTube(VideoTube videoTube)
        {
            //check if schedule already in database; 

            int userId = 0;
            int vrId = 0;
            if (HttpContext.Current.Session["userId"] != null)
            {
                userId = int.Parse(HttpContext.Current.Session["userId"].ToString());
                vrId = VideoRoomTubeManage.GetVideoRoomTubeIdByUserId(userId);

            }
            bool isExist = VideoTubeManage.IsVideoTubePartOfVideoRoomTube(vrId, videoTube.ProviderVideoId);
            if (!isExist)
            {
                VideoTube vt = videoTube;
                vrId = VideoRoomTubeManage.GetVideoRoomTubeIdByUserId(userId);
                videoTube.VideoTubeId = vrId;
                //VideoTubeManage.AddVideoTube(videoTube);
                return "video has been added to video room";
            }
            else
            {
                return "video already in video room";
            }
        }

        [WebMethod(EnableSession = true)]
        public string AddVideoToPUblicLibrary(VideoTube videoTube)
        {
            string message = "";
             //check if tube exist in library

            bool isTubeExist = PublicLibraryManage.CheckIfVideoTubeExists(videoTube.ProviderVideoId); 
             if(!isTubeExist)
             {
                 //videoTube.CreatedDate = DateTime.Now;
                 PublicLibraryManage.AddVideoTubeToPublicLibrary(videoTube);
                 message = "video has been added to public library";
             }
             else
             {
                 message = "video already exist in public library";
             }
            return message;
         }

        [WebMethod]
        public bool CheckIfVideoInOublicLib(string videoPath)
        {
            bool isTubeExist = PublicLibraryManage.CheckIfVideoTubeExists(videoPath);
            return isTubeExist;
        }

        [WebMethod]
        public string GetDescription(int videoTubeId)
        {
            VideoTubeRepository tubeRepository = new VideoTubeRepository();
            VideoTube tube = tubeRepository.GetVideoTubeById(videoTubeId);
            return tube.Description;
        }

        [WebMethod]
        public int GetCategoryId(int videoTubeId)
        {
            VideoTubeRepository tubeRepository = new VideoTubeRepository();
            VideoTube tube = tubeRepository.GetVideoTubeById(videoTubeId);
            return tube.CategoryId;
        }

        [WebMethod]
        public void RemoveVideoFromPUblicLib(int videoTubeId)
        {
            PublicLibraryManage.RemoveVideoTubeFromPublicLibrary(videoTubeId);
        }

        [WebMethod]
        public string UpdateVideoTube(int videoTubeId, bool r_rated, int categoryId)
        {
            VideoTubeRepository tubeRepository = new VideoTubeRepository();
            VideoTube tube = tubeRepository.GetVideoTubeById(videoTubeId);
          
            tube.Title = tube.Title;
            tube.Description = tube.Description;
            tube.CategoryId = categoryId;
            if (r_rated == false)
            {
                tube.IsRRated= false;
            }
            else
            {
                tube.IsRRated = true;
            }
            tubeRepository.UpdateVideoTube(tube);
            return "video has been updated";
        }
       
        //TODO
        [WebMethod(EnableSession=true)]
        public string AddVideoToVrFromPublicLib(int publicVideoId)
        {

            //PublicLib tube = PublicLibraryManage.GetVideoTubeById(publicVideoId);
            //string message = "";
            //int vrId = 0;
            //int userId=0;
            //if(HttpContext.Current.Session["userId"]!=null)
            //{
            //   userId=int.Parse(HttpContext.Current.Session["userId"].ToString());
            //}
            //bool isVideoExist = PublicLibraryManage.CheckIfVideoTubeExistsInVideoRoomTube(userId, tube.videoPath);
            //if(HttpContext.Current.Session["userId"]!=null)
            //{
            //    userId = int.Parse(HttpContext.Current.Session["userId"].ToString());
            //    vrId = VideoRoomTubeManage.GetVideoRoomTubeIdByUserId(userId);
            //}
            //if(!isVideoExist)
            //{
            //try
            //{ 
            //VideoTube tubeToAdd = new VideoTube();
            //tubeToAdd.useCount = 0;
            //tubeToAdd.videoCount = 0;
            //tubeToAdd.videoPath = tube.videoPath;
            //tubeToAdd.videoRooomId = vrId;
            //tubeToAdd.videoThumbnail = tube.videoThumbnail;
            //tubeToAdd.title = tube.title;
            //tubeToAdd.r_rated = tube.r_rated;
            //tubeToAdd.provider = tube.provider;
            //tubeToAdd.isScheduled = false;
            //tubeToAdd.duration = tube.duration;
            //tubeToAdd.description = tube.description;
            //tubeToAdd.categoryId = tube.categoryId;
            //tubeToAdd.addedDate = DateTime.Now;
            //VideoTubeManage.AddVideoTube(tubeToAdd);
            //message = "video is added to video room";
            //    }
            //catch
            //{
            //    message = "adding video not succeeded please try again later";
            //}
            //}
            //else
            //{
            //    message="video is already in video room";
            //}
            return "TODO";

            
        }

        /// <summary>
        /// This method will retrieve video categories with video counts associated with
        /// videos in public library
        /// </summary>
        /// <returns>Collection of CategoryModels</returns>
        [WebMethod]
        public List<CategoryModel> GetVideoCategoriesForPublicLibrary()
        {
            var categories = ReferenceDataManage.GetAllCategories();
            var videosByCategoryCounters = VideoTubeManage.CountVideoTubesInPublicLibraryByCategory();
            var categoryModels = new List<CategoryModel>();

            int totalCount = 0;

            if (videosByCategoryCounters != null && videosByCategoryCounters.Count > 0)
            {
                categories.OrderBy(x => x.Name).ToList().ForEach(c =>
                {
                    var counter = videosByCategoryCounters.FirstOrDefault(cn => c.CategoryId == cn.EntityId);
                    categoryModels.Add(new CategoryModel()
                    {
                        Name = counter != null && counter.VideoTubeCounter > 0 ? String.Format("{0} ({1})", c.Name, counter.VideoTubeCounter) : c.Name,
                        CategoryId = c.CategoryId
                    });
                });
            }

            videosByCategoryCounters.ForEach(x => totalCount += x.VideoTubeCounter);

            categoryModels.Insert(0, new CategoryModel()
            {
                Name = "All Categories", //String.Format("All Categories ({0})", totalCount),
                CategoryId = 0
            });

            return categoryModels;
        }

        /// <summary>
        /// This method will retrieve a page of public videos from public library based
        /// on specified page id
        /// </summary>
        /// <param name="pageIndex">Page Index</param>
        /// <param name="categoryId">Category id</param>
        /// <returns></returns>
        [WebMethod]
        public VideoTubePageModel GetAllPublicVideosByPageIndex(PublicLibraryVideoSearchCriteria criteria)
        {
            return criteria.CategoryId > 0
                            ? PublicLibraryManage.GetAllPublicVideoTubesByCategoryIdAndPageIndex(criteria)
                            : PublicLibraryManage.GetAllPublicVideoTubesByPageIndex(criteria);
        }

        private List<VideoTubePo> RemovedRestrictedOrRemoved(List<VideoTubePo> pubLibList)
        {
            List<VideoTubePo> videoList = new List<VideoTubePo>();
            if (pubLibList.Count != 0 || pubLibList != null)
            {
                foreach (var t in pubLibList)
                {
                    //VideoTube tube = new VideoTube();
                    //tube.addedDate = t.addedDate;
                    //tube.category = t.category;
                    //tube.categoryId = t.categoryId;
                    //tube.description = t.description;
                    //tube.duration = t.duration;
                    //tube.isRemoved = true;
                    //tube.isRestricted = true;
                    //tube.isScheduled = false;
                    //tube.provider = t.provider;
                    //tube.r_rated = t.r_rated;
                    //tube.title = t.title;
                    //tube.useCount = t.useCount;
                    //tube.videoCount = t.videoCount;
                    //tube.videoPath = t.videoPath;
                    //tube.videoThumbnail = t.videoThumbnail;
                    //tube.videoUploadId = t.videoUploadId;
                    //videoList.Add(tube);

                }

            }
            YouTubeWebService service = new YouTubeWebService();
            //var newVideoTubeList = service.MakeBatchRequest(pubLibList);
            List<VideoTubePo> newPublicLIst = new List<VideoTubePo>();
            foreach (var p in pubLibList)
            {
                //var query = newVideoTubeList.FirstOrDefault(x => x.ProviderVideoId == p.ProviderVideoId);
                //if (query != null && (!query.IsRemovedByProvider || !query.IsRestrictedByProvider))
                //{
                //    newPublicLIst.Add(p);
                //}
            }
            return newPublicLIst;

        }

        //TODO
        private string BuildVrTubeControl(List<VideoTubePo> partitialList)
        {
            Page page = new Page();
            StringBuilder sb = new StringBuilder();

            int userId = 0;
            if (HttpContext.Current.Session["userId"] != null)
            {
                userId = int.Parse(HttpContext.Current.Session["userId"].ToString());
            }
            int vrId = VideoRoomTubeManage.GetVideoRoomTubeIdByUserId(userId);
            List<VideoTubePo> videoList = VideoTubeManage.GetAllVideoTubesForVideoRoom(vrId);
            List<VideoTubePo> partitialListOnlyNew = new List<VideoTubePo>();
            foreach (var v in partitialList)
            {
                var query = videoList.SingleOrDefault(x => x.ProviderVideoId == v.ProviderVideoId);
                if (query == null)
                {

                    partitialListOnlyNew.Add(v);
                }
            }
            if (partitialListOnlyNew != null)
            {
                foreach (var v in partitialListOnlyNew)
                {
                    //get title
                    string title = v.Title;
                    //get description
                    string description = v.Description;
                    double duration = 0;
                    var query = videoList.SingleOrDefault(x => x.ProviderVideoId == v.ProviderVideoId);

                    duration = v.Duration;

                    //get viewes
                    long views = v.ViewCounter;
                    //get thumbnail of video;
                    string thumbnailUrl = "";

                    thumbnailUrl = v.Thumbnail;

                    //build user control
                    string path = "~/UC/PublicLibBox.ascx";
                    PublicLibBox videoBoxCtrl = (PublicLibBox)page.LoadControl(path);
                    double dur = TimeSpan.FromSeconds(duration).TotalMinutes;
                    videoBoxCtrl.duration = DateTimeUtils.PrintTimeSpan(dur); //Math.Round(dur, 1, MidpointRounding.AwayFromZero); // TimeSpan.FromSeconds(duration).TotalMinutes;
                    videoBoxCtrl.doubleDuration = dur;
                    videoBoxCtrl.originalTitle = title;
                    videoBoxCtrl.originalDescriptiom = description;
                    videoBoxCtrl.srcImage = v.Thumbnail;
                    videoBoxCtrl.views = views.ToString("#,##0");
                    videoBoxCtrl.actionId = "action_" + v.VideoTubeId.ToString();
                    videoBoxCtrl.txtCustomizeDescription = "txtCustomizeDescription_" + v.VideoTubeId.ToString();
                    videoBoxCtrl.txtCustomTitle = "txtCustomTitle_" + v.VideoTubeId.ToString();
                    videoBoxCtrl.closeId = v.VideoTubeId.ToString();
                    videoBoxCtrl.addToSchedule = "addToSchedule_" + v.VideoTubeId.ToString();
                    videoBoxCtrl.addToVr = "addToVr_" + v.VideoTubeId.ToString();
                    videoBoxCtrl.boxContentId = "boxContent_" + v.VideoTubeId.ToString();
                    videoBoxCtrl.countDesc = "countDesc_" + v.VideoTubeId.ToString();
                    videoBoxCtrl.countTitle = "countTitle_" + v.VideoTubeId.ToString();
                    videoBoxCtrl.hiddenCatVal = v.VideoTubeId.ToString();
                    videoBoxCtrl.playId = v.ProviderVideoId;
                    if (query != null)
                    {
                        videoBoxCtrl.isInVr = true;
                    }
                    else
                    { videoBoxCtrl.isInVr = false; }
                    string category = v.CategoryName;
                    videoBoxCtrl.category = category;


                    videoBoxCtrl.addId = "addId_" + v.VideoTubeId.ToString();
                    videoBoxCtrl.selectId = "selectId_" + v.VideoTubeId.ToString();
                    //if (partitialList.IndexOf(v) <= 24)
                    //{
                    //    videoBoxCtrl.side = "left";
                    //}
                    //else
                    //{
                    //    videoBoxCtrl.side = "right";
                    //}
                    //if (v.Status != null)
                    //{
                    //    if (v.Status.Name == "restricted")
                    //    {
                    //        videoBoxCtrl.isRestricted = true;

                    //    }
                    //    else
                    //    {
                    //        videoBoxCtrl.isRestricted = false;
                    //    }
                    //}
                    StringWriter output = new StringWriter(sb);
                    HtmlTextWriter hw = new HtmlTextWriter(output);
                    videoBoxCtrl.RenderControl(hw);
                }
            }
            Session["publiLib"] = partitialList;
            if (Session["startIndexPubLib"] == null)
            {
                Session["startIndexPubLib"] = 1;
            }
            return sb.ToString();
        }
       
    }
}
