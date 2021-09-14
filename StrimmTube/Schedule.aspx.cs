using StrimmBL;
using Strimm.Model;
using StrimmTube.UC;
using StrimmTube.WebServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Strimm.Model.WebModel;
using System.Configuration;
using Strimm.Shared;
namespace StrimmTube
{
    public partial class Schedule : BasePage
    {
        public int userId { get; set; }

        public int channelId { get; set; }

        public int isEditPage { get; set; }

        public int channelScheduleId { get; set; }

        public string dateVideoSchedule { get; set; }

        public int vrId { get; set; }

        public bool isAutoPilotOn { get; set; }

        public string channelName { get; set; }

        public string userName { get; set; }

        public bool isNewChannel = false;

        public bool isProviderEnable { get; set; }

        private List<VideoProvider> AvailableProviders { get; set; }

        public string availableProvidersArrData { get; set; }

        public string channelDescription { get; set; }

        public bool matureChannelContentEnabled { get; set; }

        public bool matureUserContentEnabled { get; set; }

        public bool privateVideoEnabled { get; set; }

        public bool showPrivateVideoMode { get; set; }

       

      
        public bool IsYoutubeActive
        {
            get
            {
                return (this.AvailableProviders != null && this.AvailableProviders.Count > 0 && this.AvailableProviders.FirstOrDefault(x => x.VideoProviderId == 1) != null);
            }
            set
            {
                IsYoutubeActive = value;
            }
        }

        public bool IsVimeoActive
        {
            get
            {
                return (this.AvailableProviders != null && this.AvailableProviders.Count > 0 && this.AvailableProviders.FirstOrDefault(x => x.VideoProviderId == 2) != null);
            }
            set
            {
                IsVimeoActive = value;
            }
        }

        public bool IsDmotionActive
        {
            get
            {
                return (this.AvailableProviders != null && this.AvailableProviders.Count > 0 && this.AvailableProviders.FirstOrDefault(x => x.VideoProviderId == 3) != null);
            }
            set
            {
                IsDmotionActive = value;
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetAllowResponseInBrowserHistory(false);
        }

        protected override void OnPreRender(EventArgs e)
        {
            if (!IsPostBack)
            {
                TurnOffCrawling();
            }

            base.OnPreRender(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.AvailableProviders = VideoProviderManage.GetActiveVideoProviders();
                System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                availableProvidersArrData = serializer.Serialize(AvailableProviders);

                var youtube = this.AvailableProviders.FirstOrDefault(x => x.VideoProviderId == 1);

                var uid = 0;
                isProviderEnable = Convert.ToBoolean(ConfigurationManager.AppSettings["AllowProviderSelection"].ToString());

                if (Session["userId"] == null || !Int32.TryParse(Session["userId"].ToString(), out uid)
                    || Page.RouteData.Values["ChannelName"] == null
                    || Page.RouteData.Values["UserName"] == null)
                {
                    Response.Redirect("/home", false);
                    Context.ApplicationInstance.CompleteRequest();
                    return;
                }

                this.userId = uid;
                this.channelName = Page.RouteData.Values["ChannelName"].ToString();
                this.userName = PublicNameUtils.EncodeApostropheInPublicName(Page.RouteData.Values["UserName"].ToString());
                var channelModel = ChannelManage.GetChannelTubeByUrl(this.channelName);
                var userPoByPublicUrl = UserManage.GetUserByPublicUrl(PublicNameUtils.GetUrl(userName));
                channelDescription = channelModel.Description;
                matureUserContentEnabled = userPoByPublicUrl.MatureContentAllowed;
               

                //enablePrivateVideoMode = userPoByPublicUrl.PrivateVideoModeEnabled;
                //enableMatureContentMode = userPoByPublicUrl.MatureContentAllowed;

                //channel.AllowMatureContent = true;

                //chboxMContent.Enabled = channel.AllowMatureContent && enablMatureContentMode
                
               
                if (Session["newChannel"] != null)
                {
                    isNewChannel = true;

                    Session["newChannel"] = null;
                }

                if (userId != userPoByPublicUrl.UserId)
                {
                    Response.Redirect("/home", false);
                    Context.ApplicationInstance.CompleteRequest();
                    return;
                }

                if (channelModel != null)
                {
                    AddTitle(String.Format("{0} Production Studio", userPoByPublicUrl.PublicUrl), false);

                    this.channelId = channelModel.ChannelTubeId;
                    this.channelName = channelModel.Name;

                    Session["ChannelTubeId"] = this.channelId;

                    var channelManagementUC = LoadControl("~/UC/ChannelManagementUC.ascx") as ChannelManagementUC;
                    channelManagementUC.channelTubeName = channelName;
                    channelManagementUC.defaultChannelPictureUrl = ImageUtils.DefaultChannelImageAbsoluteUrl;

                    this.divBackendmenuHolder.Controls.Add(channelManagementUC);

                    this.isAutoPilotOn = channelModel.IsAutoPilotOn;
                }
                else
                {
                    Response.Redirect("/home", false);
                    Context.ApplicationInstance.CompleteRequest();
                    return;
                }
            }
        }
    }
}
