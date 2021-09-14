using Strimm.Model.WebModel;
using StrimmBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;

namespace Strimm.Web.Services
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class VideoRoomService
    {
        // To use HTTP GET, add [WebGet] attribute. (Default ResponseFormat is WebMessageFormat.Json)
        // To create an operation that returns XML,
        //     add [WebGet(ResponseFormat=WebMessageFormat.Xml)],
        //     and include the following line in the operation body:
        //         WebOperationContext.Current.OutgoingResponse.ContentType = "text/xml";

        /// <summary>
        /// This method will retrieve a page of video tubes from a video room based
        /// for a user based on specified page index
        /// </summary>
        /// <param name="pageIndex">Index of the page to retrieve</param>
        /// <param name="userId">User Id</param>
        /// <returns></returns>
        [OperationContract]
        public VideoTubePageModel GetAllVideoTubePoByPageIndexAndUserId(int pageIndex, int userId, int categoryId, int channelTubeId)
        {
            return categoryId > 0
                ? VideoTubeManage.GetVideoTubesFromVideoRoomByUserAndCategoryIdAndByPageIndex(userId, categoryId, pageIndex, channelTubeId)
                : VideoTubeManage.GetVideoTubesFromVideoRoomByUserAndByPageIndex(userId, pageIndex, channelTubeId);
        }

        /// <summary>
        /// This method will removed a video tube by id from the user's video room
        /// </summary>
        /// <param name="userId">UserId</param>
        /// <param name="videoTubeId">VideoTubeId</param>
        /// <returns></returns>
        [OperationContract]
        public bool RemoveVideoFromVideoRoom(int userId, int videoTubeId)
        {
            return VideoTubeManage.RemoveVideoTubeFromVideoRoomTube(videoTubeId, userId);
        }

        /// <summary>
        /// This method will retrieve a list of video categories with counters that correspond to the number of
        /// videos in a specific category in the user's video room
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <returns>Collection of CategoryModels</returns>
        [OperationContract]
        public List<CategoryModel> GetVideoCategoriesForVideoRoom(int userId)
        {
            var categories = ReferenceDataManage.GetAllCategories();
            var videosByCategoryCounters = VideoTubeManage.CountVideoTubesInVideoRoomTubeByCategoryAndUserId(userId);
            var categoryModels = new List<CategoryModel>();

            int totalCount = 0;

            if (videosByCategoryCounters != null && videosByCategoryCounters.Count > 0)
            {
                categories.OrderBy(x => x.Name).ToList().ForEach(c =>
                {
                    var counter = videosByCategoryCounters.FirstOrDefault(cn => c.CategoryId == cn.EntityId);
                    categoryModels.Add(new CategoryModel()
                    {
                        Name = counter != null ? String.Format("{0} ({1})", c.Name, counter.VideoTubeCounter) : c.Name,
                        CategoryId = c.CategoryId
                    });
                });
            }

            videosByCategoryCounters.ForEach(x => totalCount += x.VideoTubeCounter);

            categoryModels.Insert(0, new CategoryModel()
            {
                Name = String.Format("All ({0})", totalCount),
                CategoryId = 0
            });

            return categoryModels;
        }
    }
}
