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
    public partial class EmbededChannel : System.Web.UI.Page
    {
        public int channelTubeId { get; set; }// need for JS use
        public ChannelTubePo channelTubePo { get; set; }
        public ChannelTube channelTube { get; set; }
        public int userId { get; set; }
        public int channelScheduleId { get; set; }
        public bool isMyFavoriteChannel { get; set; }
        public bool isMyChannel { get; set; }
        public string channelImgAvatarUrl { get; set; }
        public string boardUrl { get; set; }
        public string userName { get; set; }
        public string channelName { get; set; }
        public string channelDescription { get; set; }
        public float userRating { get; set; }
        public string channelAVGRating { get; set; }
        private List<VideoProvider> AvailableProviders { get; set; }
        public string absoluteChannelUrl { get; set; }

        public string userCreatorAvatar { get; set; }

        public bool iLikeThisCahnnel { get; set; }

        public string categoryName { get; set; }

        public int channelCreatorUserId { get; set; }
        public bool IsWhiteLabeled { get; set; }

        public bool muteOnStartup { get; set; }

        public string accountNumber { get; set; }

        public string channelPasword { get; set; }

        public bool embedEnabled { get; set; }

        public bool isCustomLabelEnabled { get; set; }

        public string customLabel { get; set; }

        public string subscribtionDomainName { get; set; }

        public string nestedDoaminName { get; set; }

        public string realChannelName { get; set; }

        public bool isFlowplayerEnable { get; set; }

       public bool showPlayerControls { get; set; }

       public bool LogoModeActive { get; set; }
       public string CustomLogo { get; set; }
       public bool PlayLiveFirst { get; set; }
        public bool keepGuideOpen { get;set; }

       //public string customLogo { get; set; }

       //public bool isLogoModeActive { get; set; }

       //public string defaultMaleAvatar { get; set; }

       //public string defaultFemaleAvatar { get; set; }

        public bool IsYoutubeActive
        {
            get
            {
                return (this.AvailableProviders != null && this.AvailableProviders.Count > 0 && this.AvailableProviders.FirstOrDefault(x => x.VideoProviderId == 1) != null);
            }
        }

        public bool IsVimeoActive
        {
            get
            {
                return (this.AvailableProviders != null && this.AvailableProviders.Count > 0 && this.AvailableProviders.FirstOrDefault(x => x.VideoProviderId == 2) != null);
            }
        }

        public static string DomainName = ConfigurationManager.AppSettings["domainName"] ?? "www.strimm.com";
        public static string AmazonS3WsDomain = ConfigurationManager.AppSettings["AmazonS3WsDomain"];
        public static string DefaultImageDomain = DomainName.StartsWith("localhost") ? "https://s3.amazonaws.com/" : AmazonS3WsDomain;
        public static string ImagesBucket = ConfigurationManager.AppSettings["ImagesBucket"];
        public static string DefaultAvatarMale = ConfigurationManager.AppSettings["DefaultAvatarMale"];
        public static string DefaultAvatarFemale = ConfigurationManager.AppSettings["DefaultAvatarFemale"];
        public static string ChannelPageSocialTitle = ConfigurationManager.AppSettings["ChannelPageSocialTitle"];
        public static string ChannelPageSocialDescription = ConfigurationManager.AppSettings["ChannelPageSocialDescription"];
        public static string DefaultChannelPictureUrl = ConfigurationManager.AppSettings["DefaultChannelPictureUrl"];
        protected void Page_Init(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetAllowResponseInBrowserHistory(false);
            // HideOldFooter = true;
        }
        public bool IsProd
        {
            get
            {
                return DomainName != null && DomainName == "www.strimm.com";
            }
        }
        public void SetPageMeta(string metaName, string metaContent, bool shouldUpdate, HttpContext httpContext = null)
        {
            if (string.IsNullOrWhiteSpace(metaName))
                return;

            if (metaContent == null)
                throw new Exception("Dynamic Meta tag content can not be null. Pl pass a valid meta tag content");

            if (httpContext == null)
                httpContext = HttpContext.Current;

            Page page = httpContext.Handler as Page;
            if (page != null)
            {
                if (Header != null)
                {
                    var metaCtrls = Header.Controls != null ? (from ctrls in Header.Controls.OfType<HtmlMeta>()
                                                               where ctrls.Name.Equals(metaName, StringComparison.CurrentCultureIgnoreCase)
                                                               select ctrls) : null;

                    HtmlMeta htmlMetaCtrl = metaCtrls != null ? metaCtrls.FirstOrDefault() : null;

                    if (!shouldUpdate || htmlMetaCtrl == null)
                    {
                        if (htmlMetaCtrl != null)
                            Header.Controls.Remove(htmlMetaCtrl);

                        htmlMetaCtrl = new HtmlMeta();
                        htmlMetaCtrl.HttpEquiv = metaName;
                        htmlMetaCtrl.Name = metaName;
                        htmlMetaCtrl.Content = metaContent;
                        Header.Controls.Add(htmlMetaCtrl);
                    }
                    else
                    {
                        htmlMetaCtrl.Content = metaContent;
                    }
                }
            }
            else
            {
                throw new Exception("Web page handler context could not be obtained");
            }
        }
        public void TurnOffCrawling()
        {
            SetPageMeta("robots", "NOFOLLOW, NOINDEX", true);
            SetPageMeta("GOOGLEBOT", "NOFOLLOW, NOINDEX", true);
        }

        public string DefaultAvatarMaleImageAbsoluteUrl
        {
            get
            {
                return String.Format("{0}{1}{2}", DefaultImageDomain, ImagesBucket, DefaultAvatarMale);
            }
        }

        public string DefaultAvatarFemaleImageAbsoluteUrl
        {
            get
            {
                return String.Format("{0}{1}{2}", DefaultImageDomain, ImagesBucket, DefaultAvatarFemale);
            }
        }

        public string GetChannelPageSocialTitle(string publicName)
        {
            return String.Format(ChannelPageSocialTitle, publicName);
        }

        public string GetChannelPageSocialDescription()
        {
            return ChannelPageSocialDescription;
        }

        public bool isEmbededbyOwner { get; set; }

        public bool isUserSubscribed { get; set; }



        protected void Page_Load(object sender, EventArgs e)
        {
            isFlowplayerEnable = bool.Parse(ConfigurationManager.AppSettings["FlowplayerEnable"].ToString());
            Response.AppendHeader("Access-Control-Allow-Origin", "*");


            //this.defaultFemaleAvatar = "https://s3.amazonaws.com/tubestrimmtest/default/imgUserAvatarFemale.jpg";
            //this.defaultMaleAvatar = "https://s3.amazonaws.com/tubestrimmtest/default/imgUserAvatarMale.jpg";

            System.Web.UI.WebControls.Literal lit = new System.Web.UI.WebControls.Literal();
            System.Web.UI.WebControls.Literal lit1 = new System.Web.UI.WebControls.Literal();

            if (isFlowplayerEnable)
            {
                lit.Text = System.Web.Optimization.Styles.Render("~/bundles/embeddedFP/css").ToHtmlString();
                lit1.Text = System.Web.Optimization.Scripts.Render("~/bundles/embeddedFP/js").ToHtmlString();
            }
            else
            {
                lit.Text = System.Web.Optimization.Styles.Render("~/bundles/embedded/css").ToHtmlString();
                lit1.Text = System.Web.Optimization.Scripts.Render("~/bundles/embedded/js").ToHtmlString();
            }


            Page.Header.Controls.AddAt(0, lit);
            Page.Header.Controls.AddAt(1, lit1);

            string channelUrl = String.Empty;
            string username = String.Empty;
            bool showOtherChannels = false;
            bool mute = true;
            UserPo user = new UserPo();
            string embeddedDomainName = System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties().DomainName;
            // MST: By Default volume should be muted on startup
            // unless user specifies the correct flag in the URL
            // of the embedded page request.
            //muteOnStartup = true;

            int userId = 0;

            if (!IsPostBack)
            {
                if (Request.UrlReferrer != null)
                {
                    nestedDoaminName = HttpUtility.ParseQueryString(Request.UrlReferrer.AbsoluteUri).ToString();
                }
                else
                {
                    nestedDoaminName = "not found";
                }

                this.AvailableProviders = VideoProviderManage.GetActiveVideoProviders();
                var youtube = this.AvailableProviders.FirstOrDefault(x => x.VideoProviderId == 1);

                if (!IsProd)
                {
                    TurnOffCrawling();
                }
            }

            accountNumber = Request.QueryString["accountNumber"];
            // myIframe.Attributes["src"] = "pathtofilewith.html"
            Boolean.TryParse(Request.QueryString["showChannels"], out showOtherChannels);

            //if (Boolean.TryParse(Request.QueryString["mute"], out mute))
            //{
            //    muteOnStartup = mute;
            //}


            if (Session["userId"] != null)
            {
                Int32.TryParse(Session["userId"].ToString(), out userId);
            }

            if (Page.RouteData.Values["ChannelURL"] != null)
            {
                channelUrl = Page.RouteData.Values["ChannelURL"].ToString();
            }
            else
            {
                Response.Redirect("/home", false);
                Context.ApplicationInstance.CompleteRequest();

                return;
            }

            if (Page.RouteData.Values["ChannelOwnerUserName"] != null)
            {
                userName= Page.RouteData.Values["ChannelOwnerUserName"].ToString();
                absoluteChannelUrl = String.Format("{0}/{1}/{2}", DomainName, username, channelUrl);

                user = UserManage.GetUserByPublicUrl(username);

                if (user != null)
                {
                    if (!String.IsNullOrEmpty(user.ProfileImageUrl))
                    {
                        // imgCreator.ImageUrl = user.ProfileImageUrl;
                    }
                    else
                    {
                        if (user.Gender == "Male")
                        {
                            // imgCreator.ImageUrl = DefaultAvatarMaleImageAbsoluteUrl;

                        }
                        if (user.Gender == "Female")
                        {
                            //imgCreator.ImageUrl = DefaultAvatarFemaleImageAbsoluteUrl;

                        }
                    }

                 
                    //chCreatorBio.Text = user.UserStory;
                    if (String.IsNullOrEmpty(user.UserStory))
                    {
                        // chCreatorBioMore.Visible = false;
                        // chCreatorBio.Text = "Thank you for visiting my channel. More about me..... coming soon!";
                    }
                    else
                    {
                        if (user.UserStory.Length < 100)
                        {
                            // chCreatorBioMore.Visible = false;
                        }
                        // chCreatorBio.Text = user.UserStory;

                    }
                    if (user.ProfileImageUrl == null)
                    {
                        // imgChannel.ImageUrl = DefaultAvatarMaleImageAbsoluteUrl;
                        //imgChannel.Visible = false;
                    }
                }
            }
            else
            {
                //delete after implementation

                Response.Redirect("/home", false);
                Context.ApplicationInstance.CompleteRequest();
                return;
            }

            if (channelUrl != "null")
            {
                //AddHeaderLink(String.Format("{0}/{1}", username, channelUrl), "canonical", string.Empty, string.Empty);
                //AddTitle(String.Format("{0}", channelUrl), false);

                channelTube = ChannelManage.GetChannelTubeByUrl(channelUrl);

                if (channelTube != null)
                {
                    channelCreatorUserId = channelTube.UserId;
                    bool isChannelPasswordProtected = false;
                    string channelPassword = String.Empty;
                    bool isCookiePassMatchDBPass = false;
                    channelTubePo = ChannelManage.GetChannelTubePoById(channelTube.ChannelTubeId);
                    embedEnabled = channelTubePo.EmbedEnabled;
                    muteOnStartup = channelTubePo.MuteOnStartup;
                    showPlayerControls = channelTubePo.CustomPlayerControlsEnabled;
                    CustomLogo = ImageUtils.GetCustomLogoImageUrl(channelTubePo.CustomLogo);
                    LogoModeActive = channelTubePo.IsLogoModeActive;
                    PlayLiveFirst = channelTubePo.PlayLiveFirst;
                    keepGuideOpen = channelTubePo.KeepGuideOpen;
                    if (!String.IsNullOrEmpty(channelTubePo.CustomLabel) || (!String.IsNullOrEmpty(channelTubePo.CustomLogo)))
                    {
                        isCustomLabelEnabled = true;
                        customLabel = channelTubePo.CustomLabel;
                    }
                    subscribtionDomainName = channelTubePo.UserDomain;
                    if (!String.IsNullOrEmpty(channelTubePo.ChannelPassword))
                    {
                        isChannelPasswordProtected = true;
                    }
                    else
                    {
                        isChannelPasswordProtected = false;
                    }

                    if (isChannelPasswordProtected && embedEnabled)
                    {
                        channelPassword = WebUtils.GetChannelPassword(channelUrl, "pswrd");
                        if (!String.IsNullOrEmpty(channelPassword))
                        {
                            isCookiePassMatchDBPass = ChannelManage.ValidateChannelPassword(channelTubePo.ChannelTubeId, channelPassword);

                        }
                        else
                        {

                            Response.Redirect("/embed-channel-protect?channelName=" + channelUrl + "&userName=" + username, false);
                            Context.ApplicationInstance.CompleteRequest();
                            return;
                        }
                        if (!isCookiePassMatchDBPass)
                        {
                            Response.Redirect("/embed-channel-protect?channelName=" + channelUrl, false);
                            Context.ApplicationInstance.CompleteRequest();
                            return;
                        }
                    }



                    categoryName = channelTubePo.CategoryName;

                    channelDescription = String.Format("Watch and enjoy {0} on Strimm TV", channelTubePo.Name);
                    if (!String.IsNullOrEmpty(channelTubePo.Description))
                    {
                        //chDescription.Text = channelTubePo.Description;
                    }
                    else
                    {
                        //chDescription.Text = "Watch & enjoy!";
                    }

                    // UpdateDescription(channelDescription);

                    if (channelTubePo.ChannelOwnerPublicUrl != username)
                    {
                        // Invalid request was received, since username on request URL does not match name of the user who created the channel
                        //Response.Redirect("/home", false);
                        //Context.ApplicationInstance.CompleteRequest();
                        //return;
                    }

                    string socialTitle = GetChannelPageSocialTitle(channelTubePo.Name);
                    string socialDescription = GetChannelPageSocialDescription();

                    channelTubeId = channelTube.ChannelTubeId;
                    user = UserManage.GetUserPoByUserId(channelTube.UserId);
                    accountNumber = user.AccountNumber;
                    channelName = socialTitle;
                    realChannelName = channelTubePo.Name;
                    channelDescription = socialDescription;
                    channelAVGRating = MiscUtils.ToFixed(channelTubePo.Rating, 1);
                    userRating = ChannelManage.GetUserRatingByUserIdAndChannelTubeId(userId, channelTubePo.ChannelTubeId);

                    isMyChannel = userId > 0 && channelTubePo.UserId == userId;

                    DateTime clientTime = WebUtils.GetClientTimeFromCookie(Request.Cookies) ?? DateTime.Now;
                    List<ChannelTubeModel> channelList = ChannelManage.GetCurrentlyPlayingChannelsByUserId(clientTime, channelTube.UserId);//GetChannelsByUserIdAndClientTime(channelTube.UserId, clientTime);

                    string absoluteEmbeddedChannelUrl = "";

                    if (channelList != null && channelList.Count > 1 && showOtherChannels)
                    {
                        int number = 1;
                        var channelListToBuild = channelList.Where(ch => ch.ChannelTubeId != channelTubeId).ToList();

                        channelListToBuild.ForEach(c =>
                        {
                            username = Page.RouteData.Values["ChannelOwnerUserName"].ToString();

                            if (!String.IsNullOrEmpty(accountNumber))
                            {
                                absoluteEmbeddedChannelUrl = String.Format("https://{0}/embedded/{1}/{2}?embed=true&account={3}&showChannels=true", DomainName, c.ChannelOwnerUrl, c.ChannelUrl, accountNumber);
                            }
                            else
                            {
                                absoluteEmbeddedChannelUrl = String.Format("https://{0}/embedded/{1}/{2}?embed=true&showChannels=true", DomainName, c.ChannelOwnerUrl, c.ChannelUrl);
                            }

                            HtmlElement div = new HtmlElement();
                            div.Attributes.Add("class", "slider");

                            var bctrl = LoadControl("~/UC/EmbeddedPageChannelBox.ascx") as EmbeddedPageChannelBox;

                            bctrl.channelId = c.ChannelTubeId;
                            bctrl.channelHref = absoluteEmbeddedChannelUrl;
                            bctrl.channelName = c.Name;
                            bctrl.channelImage = (c.PictureUrl != null && !String.IsNullOrEmpty(c.PictureUrl)) ? c.PictureUrl : "/images/comingSoonBG.jpg";
                            bctrl.IsOnAir = c.PlayingVideoTubeId > 0;
                            bctrl.IsLink = false;

                            bctrl.channelNumber = number++;

                            div.Controls.Add(bctrl);

                            moreChannelsContent.Controls.Add(div);

                        });

                    }
                    else
                    {
                        morechannelsholder.Visible = false;
                    }
                    //}
                    //Embedded info hiding strimm logo


                    UserPo embededUserInfo = new UserPo();
                    if (!String.IsNullOrEmpty(accountNumber))
                    {
                        embededUserInfo = UserManage.GetUserByAccountNumber(accountNumber);

                        if (channelTube != null && embededUserInfo != null)
                        {
                            if (channelTube.UserId == embededUserInfo.UserId)
                            {
                                isEmbededbyOwner = true;
                            }
                            else
                            {
                                isEmbededbyOwner = false;
                            }

                        }



                    }
                    else
                    {
                        isEmbededbyOwner = false;
                    }

                    this.IsWhiteLabeled = (isEmbededbyOwner && channelTubePo.IsWhiteLabeled);

                    //if (isEmbededbyOwner && channelTubePo.IsWhiteLabeled) //embededUserInfo.IsSubscriber)
                    //{
                    //    logo.Visible = false;
                    //}
                    //else
                    //{
                    //    logo.Visible = true;
                    //}


                    //    else
                    //    {
                    //        Response.Redirect("/home", false);
                    //        Context.ApplicationInstance.CompleteRequest();
                    //        return;
                    //    }
                    //}
                    //else
                    //{
                    //    Response.Redirect("/home", false);
                    //    Context.ApplicationInstance.CompleteRequest();
                    //    return;
                    //}

                    //Load data to page
                    if (!IsPostBack && channelTubePo != null)
                    {
                        if (!String.IsNullOrEmpty(ImageUtils.GetChannelImageUrl(channelTube.PictureUrl)))
                        {
                            // imgChannelAvatarChannel.ImageUrl = channelTubePo.PictureUrl;
                        }
                        else
                        {
                            //imgChannelAvatarChannel.ImageUrl = DefaultChannelImageAbsoluteUrl;
                        }
                        //else
                        //{
                        //    imgChannelAvatarChannel.Visible = false;
                        //}

                        if (!String.IsNullOrEmpty(channelTubePo.Description))
                        {
                            if (channelTubePo.Description.Length < 100)
                            {
                                // chDescriptionMoreLabel.Visible = false;
                            }
                        }
                        else
                        {
                            //chDescriptionMoreLabel.Visible = false;

                            //chDescription.CssClass = "defaultDesc";
                        }


                        if (!isMyChannel)
                        {
                            //lblSubscribers.Visible = false;
                            //lblViews.Visible = false;
                        }

                        boardUrl = "/" + channelTubePo.ChannelOwnerPublicUrl;
                        // ancUserName.InnerText = channelTubePo.ChannelOwnerUserName;
                        // ancUserName.Attributes.Add("href", boardUrl);

                        // channelImgAvatarUrl = imgChannelAvatarChannel.ImageUrl;
                        // ancName.Text = channelTubePo.Name;
                        // lblCategory.Text = channelTubePo.CategoryName;
                        //lblSubscribers.Text = "fans: " + channelTubePo.SubscriberCount.ToString();
                        //lblViews.Text = "views: " + channelTubePo.ChannelViewsCount.ToString();
                        //lblLikes.Text = "likes: " + channelTubePo.LikeCount.ToString();
                        if (userId > 0)
                        {
                            List<ChannelTube> faveChannelTubes = ChannelManage.GetFaveoriteChannelsByUserId(userId);
                            if (faveChannelTubes.Count != 0)
                            {
                                var thisFaveChannel = faveChannelTubes.FirstOrDefault(x => x.ChannelTubeId == channelTube.ChannelTubeId);
                                if (thisFaveChannel == null)
                                {
                                    // lnkAddToFave.Attributes.Add("onclick", "AddToFavorite()");
                                    // lnkAddToFave.Attributes.Add("title", "Add to Favorite");
                                    isMyFavoriteChannel = false;
                                }
                                else
                                {
                                    // lnkAddToFave.Attributes.Add("onclick", "RemoveFromFavorite()");
                                    // lnkAddToFave.Attributes.Add("title", "Remove from Favorite");
                                    // lnkAddToFave.Attributes.Add("class", "addtofavorite addtofavoriteActive");
                                    //lnkAddToFave.InnerText = "";
                                    isMyFavoriteChannel = true;
                                }
                            }
                            else
                            {
                                //lnkAddToFave.Attributes.Add("onclick", "AddToFavorite()");
                                //lnkAddToFave.Attributes.Add("title", "Add to Favorite");
                                isMyFavoriteChannel = false;
                            }
                            ChannelLike channelLike = ChannelManage.GetChannelLikeByChannelIdAndUserId(channelTubePo.ChannelTubeId, userId);
                            if (channelLike != null)
                            {
                                if (channelLike.IsLike == true)
                                {
                                    // ancLike.Attributes.Add("onclick", "UnLike()");
                                    // ancLike.Attributes.Add("title", "You like this channel. Click again, if you want to remove it.");
                                    iLikeThisCahnnel = true;
                                }
                                if (channelLike.IsLike == false)
                                {
                                    //ancLike.Attributes.Add("onclick", "Like()");
                                    //ancLike.Attributes.Add("title", "Like this channel");
                                    iLikeThisCahnnel = false;
                                }

                            }
                            else
                            {
                                //ancLike.Attributes.Add("onclick", "Like()");
                                //ancLike.Attributes.Add("title", "Like this channel");
                                iLikeThisCahnnel = false;
                            }
                        }
                        else
                        {
                            // Successful login should be followed by addition of the channel to a list of favorite channgels
                            //lnkAddToFave.Attributes.Add("onclick", "loginModal('sameLocation')");
                            //lnkAddToFave.Attributes.Add("title", "Add to Favorite");
                            isMyFavoriteChannel = false;

                            //ancLike.Attributes.Add("onclick", "loginModal('sameLocation')");
                            //ancLike.Attributes.Add("title", "Like this channel");
                            iLikeThisCahnnel = false;

                        }
                    }
                }
            }
        }
    }
}
