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
using Strimm.Shared;

namespace StrimmTube.WebServices
{
    /// <summary>
    /// Summary description for VideoStoreService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]
    public class VideoStoreService : System.Web.Services.WebService
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(VideoStoreService));

        [WebMethod]
        public VideoTubePageModel GetAllVideoTubePoByPageIndex(VideoStoreVideoSearchCriteria searchCriteria)
        {
            return VideoTubeManage.GetVideoTubesForVideoStoreByCategoryIdAndByPageIndex(searchCriteria);
        }

        [WebMethod]
        public List<CategoryModel> GetAllCategories()
        {
            var categories = ReferenceDataManage.GetAllCategories();
            var categoryModels = new List<CategoryModel>() { new CategoryModel() { Name = "All Categories", CategoryId = 0 } };

            categories.OrderBy(x => x.Name).ToList().ForEach(c => categoryModels.Add(new CategoryModel(c)));

            return categoryModels;
        }

        [WebMethod]
        public List<VideoTubeModel> GetVideosForExpandentPanel(int videoId, int ownerUserId)
        {
            List<VideoTubeModel> videoTubeList = new List<VideoTubeModel>();
            List<VideoTubeModel> videoTubeListToReturn = new List<VideoTubeModel>();
            List<VideoTubeModel> allVideosByUserId = VideoTubeManage.GetVideoTubesByOwnerUserId(ownerUserId);
            
            VideoTubeModel video = allVideosByUserId.Find(x => x.VideoTubeId == videoId);
            videoTubeList.Add(video);
            videoTubeList.AddRange(allVideosByUserId.Where(v => v.VideoTubeId != videoId).OrderBy(x => Guid.NewGuid()).Take(4).ToList());
            videoTubeList.ForEach(x => x.DurationString = DateTimeUtils.PrintTimeSpan(x.Duration));
            return videoTubeList;
        }

        [WebMethod]
        public int GetVideosCountByUserId(int ownerUserId)
        {

            return VideoTubeManage.GetVideoTubesByOwnerUserId(ownerUserId).Count;

           
        }
    }
}
