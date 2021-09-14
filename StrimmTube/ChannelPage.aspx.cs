using Strimm.Model;
using Strimm.Model.Projections;
using Strimm.Model.WebModel;
using Strimm.Shared;
using StrimmBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace StrimmTube
{
    public partial class ChannelPage : BasePage
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

        public bool isChannelPasswordProtected { get; set; }
        

     
        public bool iLikeThisCahnnel { get; set; }
        public bool IsYoutubeActive
        {
            get
            {
                return (this.AvailableProviders != null && this.AvailableProviders.Count > 0 && this.AvailableProviders.FirstOrDefault(x => x.VideoProviderId ==1) != null);
            }
        }

        public bool IsVimeoActive
        {
            get
            {
                return (this.AvailableProviders != null && this.AvailableProviders.Count > 0 && this.AvailableProviders.FirstOrDefault(x => x.VideoProviderId == 2) != null);
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetAllowResponseInBrowserHistory(false);
            HideOldFooter = true;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string channelUrl = String.Empty;
            string username = String.Empty;

            if(!IsPostBack)
            {
                this.AvailableProviders = VideoProviderManage.GetActiveVideoProviders();
                var youtube = this.AvailableProviders.FirstOrDefault(x => x.VideoProviderId == 1);

                if (!IsProd)
                {
                    TurnOffCrawling();
                }
            }
           
            if (Session["userId"] != null)
            {
                userId = int.Parse(Session["userId"].ToString());
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
                username = Page.RouteData.Values["ChannelOwnerUserName"].ToString();
                absoluteChannelUrl = String.Format("{0}/{1}/{2}", DomainName, username, channelUrl);
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
                AddHeaderLink(String.Format("{0}/{1}", username, channelUrl), "canonical", string.Empty, string.Empty);
                AddTitle(String.Format("{0}", channelUrl), false);

                channelTube = ChannelManage.GetChannelTubeByUrl(channelUrl);

                if(channelTube != null)
                {
                    channelTubePo = ChannelManage.GetChannelTubePoById(channelTube.ChannelTubeId);
                    if(!String.IsNullOrEmpty(channelTubePo.ChannelPassword))
                    {
                        isChannelPasswordProtected = true;
                    }
                    channelDescription = String.Format("Watch and enjoy {0} on Strimm TV", channelTubePo.Name);
                    UpdateDescription(channelDescription);

                    if (channelTubePo.ChannelOwnerPublicUrl != username)
                    {
                        // Invalid request was received, since username on request URL does not match name of the user who created the channel
                        Response.Redirect("/home", false);
                        Context.ApplicationInstance.CompleteRequest();
                        return;
                    }

                    string socialTitle = base.GetChannelPageSocialTitle(channelTubePo.Name);
                    string socialDescription = base.GetChannelPageSocialDescription();

                    channelTubeId       = channelTube.ChannelTubeId;
                    channelName         = socialTitle;
                    channelDescription  = socialDescription;
                    channelAVGRating    = MiscUtils.ToFixed(channelTubePo.Rating,1);
                    userRating          = ChannelManage.GetUserRatingByUserIdAndChannelTubeId(userId, channelTubePo.ChannelTubeId);

                    isMyChannel         = userId > 0 && channelTubePo.UserId == userId;

                    AddSocialMetaTags(socialTitle, ImageUtils.GetChannelImageUrl(channelTubePo.PictureUrl), socialDescription, Request.Url.AbsoluteUri);
                }
                else
                {
                    Response.Redirect("/home", false);
                    Context.ApplicationInstance.CompleteRequest();
                    return;
                }
            }
            else
            {
                Response.Redirect("/home", false);
                Context.ApplicationInstance.CompleteRequest();
                return;
            }

            //Load data to page
            if(!IsPostBack && channelTubePo != null)
            {
                imgChannelAvatar.ImageUrl = ImageUtils.GetChannelImageUrl(channelTubePo.PictureUrl);

                if (!isMyChannel)
                {
                    lblSubscribers.Visible = false;
                    lblViews.Visible = false;
                }

                boardUrl = "/" + channelTubePo.ChannelOwnerPublicUrl;
                ancUserName.InnerText = channelTubePo.ChannelOwnerUserName;
                ancUserName.Attributes.Add("href", boardUrl);
               
                channelImgAvatarUrl = imgChannelAvatar.ImageUrl;
                ancName.Text        = channelTubePo.Name;
                lblCategory.Text    = channelTubePo.CategoryName;
                lblSubscribers.Text = "fans: " + channelTubePo.SubscriberCount.ToString();
                lblViews.Text = "views: " + channelTubePo.ChannelViewsCount.ToString();
                lblLikes.Text = "likes: " + channelTubePo.LikeCount.ToString();
                if (userId > 0)
                {
                    List<ChannelTube> faveChannelTubes = ChannelManage.GetFaveoriteChannelsByUserId(userId);
                    if (faveChannelTubes.Count != 0)
                    {
                        var thisFaveChannel = faveChannelTubes.FirstOrDefault(x => x.ChannelTubeId == channelTube.ChannelTubeId);
                        if (thisFaveChannel == null)
                        {
                            lnkAddToFave.Attributes.Add("onclick", "AddToFavorite()");
                            lnkAddToFave.Attributes.Add("title", "Add to Favorites");
                            isMyFavoriteChannel = false;
                        }
                        else
                        {
                            lnkAddToFave.Attributes.Add("onclick", "RemoveFromFavorite()");
                            lnkAddToFave.Attributes.Add("title", "Remove from Favorites");
                            lnkAddToFave.Attributes.Add("class", "addtofavorite addtofavoriteActive");
                            lnkAddToFave.InnerText = "";
                            isMyFavoriteChannel = true;
                        }
                    }
                    else
                    {
                        lnkAddToFave.Attributes.Add("onclick", "AddToFavorite()");
                        lnkAddToFave.Attributes.Add("title", "Add to Favorites");
                        isMyFavoriteChannel = false;
                    }
                    ChannelLike channelLike = ChannelManage.GetChannelLikeByChannelIdAndUserId(channelTubePo.ChannelTubeId, userId);
                    if (channelLike != null)
                    {
                        if (channelLike.IsLike == true)
                        {
                            ancLike.Attributes.Add("onclick", "UnLike()");
                            ancLike.Attributes.Add("title", "You like this channel. Click again, if you want to remove it.");
                            iLikeThisCahnnel = true;
                        }
                       if(channelLike.IsLike==false)
                        {
                            ancLike.Attributes.Add("onclick", "Like()");
                            ancLike.Attributes.Add("title", "Like this channel");
                            iLikeThisCahnnel = false;
                        }
                      
                    }
                    else
                    {
                        ancLike.Attributes.Add("onclick", "Like()");
                        ancLike.Attributes.Add("title", "Like this channel");
                        iLikeThisCahnnel = false;
                    }
                }
                else
                {
                    // Successful login should be followed by addition of the channel to a list of favorite channgels
                    lnkAddToFave.Attributes.Add("onclick", "loginModal('sameLocation')");
                    lnkAddToFave.Attributes.Add("title", "Add to Favorites");
                    isMyFavoriteChannel = false;

                    ancLike.Attributes.Add("onclick", "loginModal('sameLocation')");
                    ancLike.Attributes.Add("title", "Like this channel");
                    iLikeThisCahnnel = false;

                }
            }
        }
    }
}
