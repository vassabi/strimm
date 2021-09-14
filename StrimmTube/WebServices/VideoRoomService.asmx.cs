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
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Strimm.Model.Projections;
using Strimm.Model.WebModel;
using Strimm.Model.Criteria;
using log4net;
using StrimmBL.Exeptions;

namespace StrimmTube.WebServices
{
    /// <summary>
    /// Summary description for VideoRoomService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class VideoRoomService : System.Web.Services.WebService
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(VideoRoomService));

        /// <summary>
        /// This method will retrieve a page of video tubes from a video room based
        /// for a user based on specified page index
        /// </summary>
        /// <param name="pageIndex">Index of the page to retrieve</param>
        /// <param name="userId">User Id</param>
        /// <returns></returns>
        [WebMethod(EnableSession = true)]
        public VideoTubePageModel GetAllVideoTubePoByPageIndexAndUserId(VideoRoomVideoSearchCriteria searchCriteria)
        {
            return searchCriteria != null && searchCriteria.CategoryId > 0
                ? VideoTubeManage.GetVideoTubesFromVideoRoomByUserAndCategoryIdAndByPageIndex(searchCriteria)
                : VideoTubeManage.GetVideoTubesFromVideoRoomByUserAndByPageIndex(searchCriteria);
        }


        /// <summary>
        /// This method will removed a video tube by id from the user's video room
        /// </summary>
        /// <param name="userId">UserId</param>
        /// <param name="videoTubeId">VideoTubeId</param>
        /// <returns></returns>
        [WebMethod(EnableSession = true)]
        public bool RemoveVideoFromVideoRoom(int userId, int videoTubeId)
        {
            return VideoTubeManage.RemoveVideoTubeFromVideoRoomTube(videoTubeId, userId);
        }

        [WebMethod]
        public List<CategoryModel> GetAllCategories()
        {
            var categories = ReferenceDataManage.GetAllCategories();
            var categoryModels = new List<CategoryModel>();

            categories.OrderBy(x => x.Name).ToList().ForEach(c => categoryModels.Add(new CategoryModel(c)));

            return categoryModels;
        }

        /// <summary>
        /// This method will retrieve a list of video categories with counters that correspond to the number of
        /// videos in a specific category in the user's video room
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <returns>Collection of CategoryModels</returns>
        [WebMethod]
        public List<CategoryModel> GetVideoCategoriesForVideoRoom(int userId)
        {
            var categories = ReferenceDataManage.GetAllCategories();
            var videosByCategoryCounters = VideoTubeManage.CountVideoTubesInVideoRoomTubeByCategoryAndUserId(userId);
            var categoryModels = new List<CategoryModel>();

            int totalCount = 0;

            if (categories != null && categories.Count > 0)
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

        [WebMethod]
        public List<VideoTubeModel> GetArchivedVideosByUserId(int userId)
        {
            return VideoTubeManage.GetArchivedVideosByUserId(userId);
        }

        [WebMethod]
        public bool DeleteArchivedVideoByVideotubeIdAndUserId(int userId, int videoTubeId)
        {
            return VideoTubeManage.DeleteArchivedVideoByVideotubeIdAndUserId(userId, videoTubeId);
        }

        [WebMethod]
        public ResponseMessageModel GetCustomVideoTubeById(int videoTubeId)
        {
            Logger.Info(String.Format("Retrieving custom video tube with id={0}", videoTubeId));

            var response = new ResponseMessageModel();

            try
            {
                var customVideoTubeModel = VideoTubeManage.GetCustomVideoTubeById(videoTubeId);

                if (customVideoTubeModel != null)
            {
                    response.IsSuccess = true;
                    response.Response = customVideoTubeModel;
            }
            else
            {
                    response.Response = "Unknown error. Failed to retrieve specified error";
            }
        }
            catch (VideoTubeManagerException vex)
        {
                Logger.Error(String.Format("Error occured while retrieving video id='{0}'", videoTubeId), vex);
                response.Message = vex.Message;
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Error occured while retrieving video id='{0}'", videoTubeId), ex);
                response.Message = "Failed to retrieve video";
            }

            return response;
        }

        [WebMethod]
        public ResponseMessageModel DeleteVideoTubeById(int userId, int videoTubeId)
        {
            Logger.Info(String.Format("Deleteing video tube with id={0} from video room for user with id={1}", videoTubeId, userId));

            var response = new ResponseMessageModel();

            try
            {
                if (VideoRoomTubeManage.DeleteVideoTubeFromVideoRoomById(userId, videoTubeId))
        {
                    response.IsSuccess = true;
                    response.Response = "Video was successfully deleted from user\'s video room";
                }
                else
            {
                    response.Response = "Unknown error. Failed to delete specified video";
                }
            }
            catch (VideoTubeManagerException vex)
            {
                Logger.Error(String.Format("Error occured while deleting video id='{0}'", videoTubeId), vex);
                response.Message = vex.Message;
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Error occured while deleting video id='{0}'", videoTubeId), ex);
                response.Message = "Failed to retrieve video";
        }

            return response;
        }

    }
}
