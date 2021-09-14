using log4net;
using Strimm.Model.WebModel;
using StrimmBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace StrimmTube.WebServices
{
    /// <summary>
    /// Summary description for VimeoWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
     [System.Web.Script.Services.ScriptService]
    public class VimeoWebService : System.Web.Services.WebService
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ScheduleWebService));

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        //[WebMethod]
        //public VideoTubePageModel GetVimeoVideoByKeyword(string keywords, int startIndex)
        //{
        //    Logger.Info(String.Format("getting vimeo videos by keyword={0} and startIndex={1}", keywords,startIndex));
        //    return VimeoServiceManage.GetVideosByKeywords(keywords, startIndex);
        //}

        [WebMethod]
        public VideoTubeModel GetVimeoVideoByURL (string url, bool allowMatureContent)
        {
            Logger.Info(String.Format("getting vimeo video by url={0}",url));
            return VimeoServiceManage.GetVideoByUrl(url, allowMatureContent);
        }
    }
}
