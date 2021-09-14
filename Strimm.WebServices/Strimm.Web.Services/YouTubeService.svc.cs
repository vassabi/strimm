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
    public class YouTubeService
    {
        // To use HTTP GET, add [WebGet] attribute. (Default ResponseFormat is WebMessageFormat.Json)
        // To create an operation that returns XML,
        //     add [WebGet(ResponseFormat=WebMessageFormat.Xml)],
        //     and include the following line in the operation body:
        //         WebOperationContext.Current.OutgoingResponse.ContentType = "text/xml";

        [OperationContract]
        public VideoTubePageModel FindVideosByKeywords(string searchString, int startIndex, int channelTubeId)
        {
            var videoPageModel = YouTubeServiceManage.GetVideosByKeywords(searchString, startIndex);
            var videosInChannel = VideoTubeManage.GetVideoTubesForChannel(channelTubeId);

            if (videoPageModel != null)
            {
                videoPageModel.VideoTubeModels.ForEach(v => v.IsInChannel = videosInChannel.Any(x => x.ProviderVideoId == v.ProviderVideoId));
            }

            return videoPageModel;
        }

        [OperationContract]
        public VideoTubeModel FindVideoByUrl(string url, int channelTubeId)
        {
            var video = YouTubeServiceManage.GetVideoByUrl(url);

            if (video != null)
            {
                var videosInChannel = VideoTubeManage.GetVideoTubesForChannel(channelTubeId);

                video.IsInChannel = videosInChannel.Any(x => x.ProviderVideoId == video.ProviderVideoId);
            }

            return video;
        }

        [OperationContract]
        public VideoTubeModel AddVideoToChannel(VideoTubeModel videoModel, int channelTubeId, int categoryId)
        {
            return YouTubeServiceManage.AddVideoToChannel(videoModel, channelTubeId, categoryId);
        }
    }
}
