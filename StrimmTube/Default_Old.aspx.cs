using log4net;
using Strimm.Model.Projections;
using Strimm.Model.WebModel;
using Strimm.Shared;
using StrimmBL;
using StrimmTube.UC;
using StrimmTube.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StrimmTube
{
    public partial class Default1 : BasePage
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(Default1));
        bool isHaveToken { get; set; }
        public DateTime ClientTime { get; set; }
        public string ChannelGroupName { get; set; }

        public string TutorialVideoId { get; set; }

        protected void Page_Init(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetAllowResponseInBrowserHistory(false);
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsProd)
            {
                TurnOffCrawling();
            }

            isHaveToken = false;
            string etoken = Request.QueryString["token"];
            string email = String.Empty;

            this.TutorialVideoId = ConfigurationManager.AppSettings["PromoVideoId"];

            UserPo user = null;

            if (Request.QueryString["token"] == null && Session["userId"] != null)
            {
                int userId = 0;
                
                if (Int32.TryParse(Session["userId"].ToString(), out userId)) {

                    user = UserManage.GetUserPoByUserId(userId);

                    if (user != null)
                    {
                        this.UserName = PublicNameUtils.EncodeApostropheInPublicName(user.UserName);
                    }
                    else
                    {
                        Session["userId"] = null;

                        if (HttpContext.Current.Request.Cookies["userId"] != null)
                        {
                            HttpContext.Current.Response.Cookies["userId"].Expires = DateTime.Now.AddDays(-1);
                            HttpContext.Current.Response.Cookies["isfacebook"].Expires = DateTime.Now.AddDays(-1);
                        }
                    }
                }
            }
            
            if(Request.QueryString["token"]!=null)
            {
                try
                {
                    if (!String.IsNullOrEmpty(etoken.Trim()))
                    {
                        string token = MiscUtils.DecodeFrom64(etoken);
                        string[] parts = token.Split(':');
                        email = parts[0];
                        //password = parts[1];

                        Logger.Debug(String.Format("Processing e-mail confirmation request for user with e-mail '{0}'", email));

                        user = UserManage.GetUserPoByEmail(email);// GetUserPoByLoginIdentifierAndPassword(email, password);

                        if (user != null)
                        {
                            this.UserName = PublicNameUtils.EncodeApostropheInPublicName(UserName);

                            if (user.IsTempUser)
                            {
                                UserManage.ConfirmUser(user.UserId);
                                string loginUpFunc = "WelcomeLoginUp('sameLocation');";
                                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "welcomeAboardLoginFnc", loginUpFunc, true);
                                Logger.Debug(String.Format("User with e-mail '{0}' was successfully confirmed", user.Email));
                            }
                            else
                            {
                                Logger.Debug(String.Format("User with e-mail '{0}' was previously confirmed. Redirecting to home page", user.Email));

                                Response.Redirect("/home", false);
                                Context.ApplicationInstance.CompleteRequest();
                            }
                        }
                        else
                        {
                            Logger.Debug(String.Format("Failed to retrieve existing user record for e-mail '{0}'.Redirecting to home page", email));
                            Response.Redirect("/home", false);
                            Context.ApplicationInstance.CompleteRequest();
                        }
                    }
                    else
                    {
                        Logger.Debug("Failed to retrieve user's email in order to confirm it. Redirecting to home page");
                        Response.Redirect("/home", false);
                        Context.ApplicationInstance.CompleteRequest();
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error(String.Format("Error occured while confirming user with Id={0}. Redirecting to home page", user.UserId), ex);
                    Response.Redirect("/home", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
            }
            else
            {
                this.ClientTime = WebUtils.GetClientTimeFromCookie(Request.Cookies) ?? DateTime.Now;
                              
                if (this.ClientTime != null)
                {
                    Session["ClientTime"] = this.ClientTime;

                    var landingPageDataModel = ChannelManage.GetCurrentlyPlayingChannelsForLandingPage(this.ClientTime);
                    this.ChannelGroupName = String.Format("{0}", landingPageDataModel.GroupName);
                    BuildChannelControls(landingPageDataModel.ChannelGroup);
                }
            }

            Logger.Info("Default page loaded");
        }

        public string UserName { get; set; }

        private void BuildChannelControls(List<ChannelTubeModel> channelList)
        {
            if (channelList.Count > 0)
            {
                int number = 1;
                channelList.ForEach(c =>
                {
                    var bctrl = LoadControl("~/UC/LandingPageChannelBox.ascx") as LandingPageChannelBox;

                    bctrl.channelId = c.ChannelTubeId;
                    bctrl.channelHref = String.Format("/{0}/{1}", c.ChannelOwnerUserName, c.ChannelUrl);
                    bctrl.channelName = c.Name;
                    bctrl.channelImage = (c.PictureUrl != null && !String.IsNullOrEmpty(c.PictureUrl)) ? c.PictureUrl : "/images/comingSoonBG.jpg";
                    bctrl.IsOnAir = c.PlayingVideoTubeId > 0;
                    bctrl.IsLink = false;
                    bctrl.channelNumber = number++;

                    channelHolder.Controls.Add(bctrl);
                });

                var link = LoadControl("~/UC/LandingPageChannelBox.ascx") as LandingPageChannelBox;
                link.IsLink = true;
                channelHolder.Controls.Add(link);
            }
        }
    }
}