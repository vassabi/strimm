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
    public class ChannelWebService
    {
        // To use HTTP GET, add [WebGet] attribute. (Default ResponseFormat is WebMessageFormat.Json)
        // To create an operation that returns XML,
        //     add [WebGet(ResponseFormat=WebMessageFormat.Xml)],
        //     and include the following line in the operation body:
        //         WebOperationContext.Current.OutgoingResponse.ContentType = "text/xml";

        /// <summary>
        /// This method will add an existing video by id to a target channel
        /// </summary>
        /// <param name="channelTubeId">Target channel tube Id</param>
        /// <param name="videoTubeId">Target video tube id</param>
        /// <returns></returns>
        [OperationContract]
        public bool AddVideoToChannel(int channelTubeId, int videoTubeId)
        {
            return VideoTubeManage.AddVideoTubeToChannelTubeById(channelTubeId, videoTubeId);
        }

        /// <summary>
        /// This method will remove video tube from channel by id
        /// </summary>
        /// <param name="channelTubeId">Target channel id</param>
        /// <param name="videoTubeId">Target video tube id</param>
        /// <returns></returns>
        [OperationContract]
        public bool RemoveVideoFromChannel(int channelTubeId, int videoTubeId)
        {
            return VideoTubeManage.RemoveVideoTubeFromChannelTubeById(channelTubeId, videoTubeId);
        }

        /// <summary>
        /// This method will search for channels using keywords in their name and description
        /// </summary>
        /// <param name="keywords">Space separated list of keywords</param>
        /// <returns>List of ChannelTubeModels</returns>
        [OperationContract]
        public List<ChannelTubeModel> FindChannelsByKeywords(string keywords)
        {
            var keywordsList = keywords.Split(' ').ToList();
            return SearchManage.GetChannelsByKeywords(keywordsList);
        }

        /// <summary>
        /// This method will retrieve a list of video categories with counters that correspond to the number of
        /// videos in a specific category in the user's channel
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <returns>Collection of CategoryModels</returns>
        [OperationContract]
        public List<CategoryModel> GetVideoCategoriesForChannel(int channelId)
        {
            var categories = ReferenceDataManage.GetAllCategories();
            var videosByCategoryCounters = VideoTubeManage.CountVideoTubesInChannelTubeByCategoryByChannelTubeId(channelId);
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

        /// <summary>
        /// This method will remove all videos from channel identified by Id
        /// </summary>
        /// <param name="channelId">Channel Tube Id</param>
        /// <returns></returns>
        [OperationContract]
        public string ClearAllVideos(int channelId)
        {
            if (channelId <= 0)
            {
                return "Invalid channel information specified";
            }

            string response = "Videos were successfully deleted";

            if (!VideoTubeManage.ClearAllVideosFromChannel(channelId))
            {
                response = "Failed to delete videos from channel";
            }

            return response;
        }

        /// <summary>
        /// This method will remove specified restricted/removed videos by provider from
        /// channel
        /// </summary>
        /// <param name="channelId">Channel Tube Id</param>
        /// <param name="videoIds">Collection of Video Ids</param>
        /// <returns></returns>
        [OperationContract]
        public string ClearRestrictedOrRemovedVideos(int channelId, List<int> videoIds)
        {
            if (channelId <= 0)
            {
                return "Invalid channel information specified";
            }

            string response = "Videos were successfully deleted";

            if (!VideoTubeManage.ClearRestrictedOrRemovedVideosFromChannel(channelId, videoIds))
            {
                response = "Failed to delete restricted or removed videos from channel";
            }

            return response;
        }

        [OperationContract]
        public string ArchiveVideo(int userId, int videoTubeId)
        {
            return null;
        }

        [OperationContract]
        public string SubscribeUserToChannel(int subscriberUserId, int channelId)
        {
            return null;
        }

        [OperationContract]
        public string UnsubscribeUserFromChannel(int subscriberUserId, int channelId)
        {
            return null;
        }

        [OperationContract]
        public string DeleteUserChannel(int userId, int channelId)
        {
            return null;
        }
    }
}
