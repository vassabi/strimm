using log4net;
using Strimm.Model.WebModel;
using Strimm.Shared;
using StrimmBL;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace StrimmTube.WebServices
{
    /// <summary>
    /// Summary description for SearchWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class SearchWebService : System.Web.Services.WebService
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(SearchWebService));
        private static readonly string stringClientDateTimeFormat = "MM/dd/yyyy HH:mm";


        [WebMethod]
        public List<ChannelTubeModel> GetChannelsByKeywords(List<string> keywords, string clientTime)
        {
            Logger.Info(String.Format("Retrieving channels by keywords for date '{0}'", clientTime));

            DateTime formattedDateTime = DateTimeUtils.GetClientTime(clientTime) ?? DateTime.Now;

            return SearchManage.GetChannelsByKeywords(keywords, formattedDateTime);
        }

        [WebMethod]
        public List<UserModel> GetUsersByKeywords(List<string> keywords)
        {
            Logger.Info("Retrieving system users by keywords");

            return SearchManage.GetUsersByKeywords(keywords);
        }

        [WebMethod]
        public List<CategoryModel> GetCategoriesWithCurrentlyPlayingChannelCount(string clientDateAndTime)
        {
            Logger.Info(String.Format("Retrieving channel categories with currently playing channel count for date '{0}'", clientDateAndTime));

            DateTime formattedDateTime = DateTimeUtils.GetClientTime(clientDateAndTime) ?? DateTime.Now;

            return ReferenceDataManage.GetChannelCategoriesWithCurrentlyPlayingChannelsCount(formattedDateTime);
        }

        [WebMethod]
        public List<CategoryModel> GetCategoriesWithCurrentlyPlayingChannelCountBrowseChannels(string clientDateAndTime)
        {
            Logger.Info(String.Format("Retrieving channel categories with currently playing channel count for browse channels for date '{0}'", clientDateAndTime));

            DateTime formattedDateTime = DateTimeUtils.GetClientTime(clientDateAndTime) ?? DateTime.Now;

            return ReferenceDataManage.GetChannelCategoriesWithCurrentlyPlayingChannelsCountForBrowseChannels(formattedDateTime);
        }


        [WebMethod]
        public List<VideoTubeModel> GetVideoTubeByKeywordAndChannelId(List<string> keywords, int channelTubeId)
        {
            Logger.Info(String.Format("Retrieving videos by keywords for channelTUbeId '{0}'", channelTubeId));

            return SearchManage.GetVideoTubeByKeywordAndChannelId(keywords, channelTubeId);
        }

        [WebMethod]
        public List<VideoTubeModel> GetVideoTubeByKeywordForPublicLibrary(List<string> keywords, int channelTubeId)
        {
            Logger.Info(String.Format("Retrieving videos by keywords for public library"));

            return SearchManage.GetVideoTubeByKeywordForPublicLibrary(keywords, channelTubeId);
        }

        [WebMethod]
        public List<VideoScheduleModel> GetCurrentlyPlayingVideoTubeByKeyword(List<string>keywords, string clientDateAndTime)
        {
            Logger.Info(String.Format("Retrieving current plaing videos by keywords"));
              DateTime formattedDateTime = DateTimeUtils.GetClientTime(clientDateAndTime) ?? DateTime.Now;
              return SearchManage.GetCurrentlyPlayingVideoTubeByKeyword(keywords, formattedDateTime);

        }


    }
}
