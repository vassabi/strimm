using log4net;
using Strimm.Model;
using Strimm.Model.Projections;
using Strimm.Model.WebModel;
using Strimm.Shared;
using StrimmBL;
using StrimmTube.UC;
using StrimmTube.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace StrimmTube
{
    public partial class Default_playerBlack : BasePage
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(Default1));
        bool isHaveToken { get; set; }
        public DateTime ClientTime { get; set; }
        public string ChannelGroupName { get; set; }

        public string TutorialVideoId { get; set; }

        public int playingChannelId { get; set; }

        public string creatorUrl { get; set; }

        public string UserName { get; set; }

        public ChannelPreviewModel currentlyPlayingData { get; set; }

        public string channelOnlandingPageName { get; set; }

        public string channelDescription { get; set; }

        public string chCreatorBio { get; set; }
        protected void Page_Init(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetAllowResponseInBrowserHistory(false);
            HideOldFooter = true;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            TurnOffCrawling();

            var domainVerifyPinterest = new HtmlMeta();
            domainVerifyPinterest.Name = "p:domain_verify";
            domainVerifyPinterest.Content = "1283df95a7aa0e31f24f5cc4402be76a";
            Header.Controls.AddAt(0, domainVerifyPinterest);

            isHaveToken = false;
            string etoken = Request.QueryString["token"];
            string email = String.Empty;

            this.TutorialVideoId = ConfigurationManager.AppSettings["PromoVideoId"];
            string playingChannelNameOnLandingPage = ConfigurationManager.AppSettings["PlayingChannelOnLandingPage"].ToString();

            ChannelTube playingChannelOnLandingPage = ChannelManage.GetChannelTubeByName(playingChannelNameOnLandingPage);
            playingChannelId = playingChannelOnLandingPage.ChannelTubeId;
            //DateTime clientTime =(DateTime) WebUtils.GetClientTimeFromCookie(Request.Cookies);
            //string clientDateTime = hiddenTime.Value;
            //currentlyPlayingData = ChannelManage.GetCurrentlyPlayingChannelById(playingChannelId, 0, clientTime);
            channelOnlandingPageName = playingChannelNameOnLandingPage;// Name of channel on info 
            UserPo user = null;
            UserPo playinChannelCreatorUser = UserManage.GetUserByChannelName(playingChannelOnLandingPage.Name);
            ancCreatorLink.HRef = "/" + playinChannelCreatorUser.PublicUrl;
            imgCreator.ImageUrl = playinChannelCreatorUser.ProfileImageUrl;
            spnCreatorName.Text = playinChannelCreatorUser.UserName;
            imgChannel.ImageUrl = playingChannelOnLandingPage.PictureUrl;
            hrefToChannel.HRef = "/" + playinChannelCreatorUser.UserName + "/" + playingChannelOnLandingPage.Url;
            if (!String.IsNullOrEmpty(playingChannelOnLandingPage.Description))
            {
                channelDescription = playingChannelOnLandingPage.Description;
            }
            else
            {
                channelDescription = "Watch & enjoy!";
            }

            if (!String.IsNullOrEmpty(playinChannelCreatorUser.UserStory))
            {
                chCreatorBio = playinChannelCreatorUser.UserStory;

            }
            else
            {
                chCreatorBio = "Thank you for visiting my channel. More about me..... coming soon!";
            }
            if (Request.QueryString["token"] == null && Session["userId"] != null)
            {
                int userId = 0;

                if (Int32.TryParse(Session["userId"].ToString(), out userId))
                {

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

            if (Request.QueryString["token"] != null)
            {
                try
                {
                    if (!String.IsNullOrEmpty(etoken.Trim()))
                    {
                        string token = MiscUtils.DecodeFrom64(etoken);
                        string[] parts = token.Split(':');
                        email = parts[0];
                        //password = parts[1];

                        HttpCookie newUserCookie = new HttpCookie("isNewUser");
                        newUserCookie.Expires = DateTime.Now.AddDays(180);
                        newUserCookie.Value = "true";
                        HttpContext.Current.Response.Cookies.Add(newUserCookie);

                        HttpCookie firstChannelCookie = new HttpCookie("isfirstChannel");
                        firstChannelCookie.Expires = DateTime.Now.AddDays(180);
                        firstChannelCookie.Value = "true";
                        HttpContext.Current.Response.Cookies.Add(firstChannelCookie);

                        //newUserCookie.Name="isNewUser";
                        HttpContext.Current.Response.Cookies.Add(newUserCookie);
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

                                EmailManage.SendWelcomeEmail(user, new Uri(Server.MapPath("~/Emails/welcomePage.html")));
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
                    var landingPageDataModelTop = ChannelManage.GetCurrentlyPlayingChannelsForLandingPageTop(this.ClientTime);
                    this.ChannelGroupName = String.Format("{0}", landingPageDataModel.GroupName);
                    BuildChannelControls(landingPageDataModelTop.ChannelGroup, true);
                    BuildChannelControls(landingPageDataModel.ChannelGroup, false);
                }

                var location = WebUtils.GetUserLocationFromCookie();
                if (user != null)
                {
                    UserManage.UpdateUserLastKnownLocationByUsername(location, user.UserName);
                }
            }

            Logger.Info("Default page loaded");
        }




        private void BuildChannelControls(List<ChannelTubeModel> channelList, bool isTopChannelBuild)
        {

            if (channelList.Count > 0)
            {
                var categoryChannelList = channelList.GroupBy(c => c.CategoryName)
                                                   .Select(c => c.ToList());
                if (isTopChannelBuild)
                {
                    var topChannelList = channelList.ToList();
                    if (topChannelList.Count > 0)
                    {
                        var bctrl = LoadControl("~/UC/ChannelsHolderCategoryUC.ascx") as ChannelsHolderCategoryUC;
                        bctrl.channelList = topChannelList;
                        bctrl.categoryName = "TOP Channels";
                        homeBlocksHolder.Controls.Add(bctrl);

                    }
                }
                else
                {
                    foreach (var channelsByCategoryList in categoryChannelList)
                    {
                        var bctrl = LoadControl("~/UC/ChannelsHolderCategoryUC.ascx") as ChannelsHolderCategoryUC;
                        bctrl.channelList = channelsByCategoryList;
                        bctrl.categoryName = channelsByCategoryList.First().CategoryName;
                        homeBlocksHolder.Controls.Add(bctrl);

                    }
                }




            }



            //var link = LoadControl("~/UC/LandingPageChannelBox.ascx") as LandingPageChannelBox;
            //link.IsLink = true;
            //channelHolder.Controls.Add(link);
        }
    }
}
