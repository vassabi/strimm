using Strimm.Model;
using Strimm.Model.Projections;
using Strimm.Shared;
using StrimmBL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StrimmTube.UC
{
    public partial class ChannelManagementUC : System.Web.UI.UserControl
    {
        public int videoRoomId { get; set; }
        public string channelTubeName { get; set; }
        ChannelTubePo channelTube { get; set; }
        public string channelTubeUrl { get; set; }
        public string channelName { get; set; }
        public int channelTubeId { get; set; }
        public string channelOwnerName { get; set; }
        public string userName { get; set; }
        public string userId { get; set; }
        public string channelPictureUrl { get; set; }
        public int channelCategoryValue { get; set; }

        public string defaultChannelPictureUrl { get; set; }
        public int languageId { get; set; }
        public int categoryId { get; set; }

        public string studioPageTutorialVideoId { get; set; }

        public bool isWhiteLabeled { get; set; }

        public bool embedEnable { get; set; }

        public bool isPasswordProtect {get;set;}

        public bool muteOnStartup {get;set;}

        public bool enabelCutomBranding { get; set; }

        public string accountNumber { get; set; }

        public string strimmDomain { get; set; }

        public string channelUrl { get; set; }

        public string channelPassword { get; set; }

        public bool showPrivateVideoMode { get; set; }

        public bool matureChannelContentEnabled { get; set; }

        public string customLogoUrl { get; set; }
        public bool isLogoModeActive { get; set; }
        public bool LiveFirst { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            studioPageTutorialVideoId = ConfigurationManager.AppSettings["StudioPageTutorialVideoId"];

            if (Session["userId"] != null)
            {
                videoRoomId = VideoRoomTubeManage.GetVideoRoomTubeIdByUserId(int.Parse(Session["userId"].ToString()));
            }
            else
            {
                Response.Redirect("/home", false);
                Context.ApplicationInstance.CompleteRequest();
            }

            int channelId = 0;
            if (Session["ChannelTubeId"] != null)
            {
                Int32.TryParse(Session["ChannelTubeId"].ToString(), out channelId);
            }
            else
            {
                Response.Redirect("/home", false);
                Context.ApplicationInstance.CompleteRequest();
            }

            this.channelTubeId = channelId;
            this.channelName = Page.RouteData.Values["ChannelName"] != null ? Page.RouteData.Values["ChannelName"].ToString() : String.Empty;
           

            ChannelTube channelTubeByChannelUrl = ChannelManage.GetChannelTubeByUrl(channelName.Replace(" ", ""));
            if (channelTubeByChannelUrl != null)
            {
                if (channelTubeByChannelUrl.UserId != int.Parse(Session["userId"].ToString()))
                {
                    Response.Redirect("/home", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
                else
                {
                    Session["ChannelTubeId"] = channelTubeByChannelUrl.ChannelTubeId;
                }
            }
            else
            {
                Response.Redirect("/home", false);
                Context.ApplicationInstance.CompleteRequest();
            }

            if (this.channelTubeId > 0)
            {
              
                channelTube = ChannelManage.GetChannelTubePoById(int.Parse(Session["ChannelTubeId"].ToString()));
                UserPo user = UserManage.GetUserPoByUserId(channelTube.UserId);
                channelOwnerName = channelTube.ChannelOwnerPublicUrl;
                var channelEntitlemnets = ChannelManage.GetUserChannelEntitlementsByUserId(channelTube.UserId);
                isLogoModeActive = channelTube.IsLogoModeActive;
                channelTubeUrl = channelTube.Url;
                channelTubeName = channelTube.Name;
                lblCategory.Text = "category: " + channelTube.CategoryName;
                lblSubscribers.Text = "fans: " + channelTube.SubscriberCount.ToString();
                userName = PublicNameUtils.EncodeApostropheInPublicName(user.UserName);
                lblViews.Text = "views: " + channelTube.ChannelViewsCount.ToString();
                imgChannelAvatar.ImageUrl = ImageUtils.GetChannelImageUrl(channelTube.PictureUrl);
                channelPictureUrl = channelTube.PictureUrl ?? this.defaultChannelPictureUrl;
                LiveFirst = channelTube.PlayLiveFirst;
                customLogoUrl = ImageUtils.GetCustomLogoImageUrl(channelTube.CustomLogo);

                categoryId = channelTube.CategoryId;
                languageId = channelTube.LanguageId;
                channelName = channelTube.Name;
                channelCategoryValue = channelTube.CategoryId;
                isWhiteLabeled = channelTube.IsWhiteLabeled;
                matureChannelContentEnabled = channelTube.MatureContentEnabled;
              embedEnable = channelTube.EmbedEnabled;
              accountNumber = user.AccountNumber;
              strimmDomain = ConfigurationManager.AppSettings["domainName"].ToString();
              showPrivateVideoMode = channelTube.IsPrivate;
             
              if (!String.IsNullOrEmpty(channelTube.ChannelPassword))
              {
                  channelPassword = channelTube.ChannelPassword;
              }
            
                //if(isWhiteLabeled)
                //{
                //    enabelCutomBranding = true;
                //}
                //else
                //{
                //    enabelCutomBranding = false;
                //}
              if (!String.IsNullOrEmpty(channelPassword))
              {
                  isPasswordProtect = true;
              }
              else
              {
                  isPasswordProtect = false;

              }

               muteOnStartup = channelTube.MuteOnStartup;
                
            }
            else
            {
                //this.ancEdit1.Visible = false;
            }
        }

        protected void ancEditBlack_Click(object sender, EventArgs e)
        {

        }
    }
}