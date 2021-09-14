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
    public partial class PlayerPrototype : BasePage
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
        protected void Page_Load(object sender, EventArgs e)
        {
            string channelUrl = "valentinachannel";
            string username = "valula";

            if (Session["userId"] != null)
            {
                userId = int.Parse(Session["userId"].ToString());
            }

            if (!IsProd)
            {
                TurnOffCrawling();
            }

            if (channelUrl != "null")
            {
                channelTube = ChannelManage.GetChannelTubeByUrl(channelUrl);

                if (channelTube != null)
                {
                    channelTubePo = ChannelManage.GetChannelTubePoById(channelTube.ChannelTubeId);

                    if (channelTubePo.ChannelOwnerUserName != username)
                    {
                        // Invalid request was received, since username on request URL does not match name of the user who created the channel
                        Response.Redirect("/home", false);
                        Context.ApplicationInstance.CompleteRequest();
                        return;
                    }

                    channelTubeId = channelTube.ChannelTubeId;
                    channelName = channelTubePo.Name;
                    //  channelDescription  = channelTubePo.Description;
                    channelAVGRating = MiscUtils.ToFixed(channelTubePo.Rating, 1);
                    userRating = ChannelManage.GetUserRatingByUserIdAndChannelTubeId(userId, channelTubePo.ChannelTubeId);

                    isMyChannel = userId > 0 && channelTubePo.UserId == userId;

                    AddMetaTags(channelTubePo, Request.Url.AbsoluteUri);
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
            if (!IsPostBack && channelTubePo != null)
            {
                if (!String.IsNullOrEmpty(channelTube.PictureUrl))
                {
                    imgChannelAvatar.ImageUrl = ImageUtils.GetChannelImageUrl(channelTubePo.PictureUrl);
                }
                else
                {
                    imgChannelAvatar.ImageUrl = "/images/comingSoonBG.jpg";
                }

                boardUrl = "/" + channelTubePo.ChannelOwnerPublicUrl;
                ancUserName.InnerText = "by " + channelTubePo.ChannelOwnerUserName;
                ancUserName.Attributes.Add("href", boardUrl);

                channelImgAvatarUrl = imgChannelAvatar.ImageUrl;
                ancName.Text = channelTubePo.Name;
                lblCategory.Text = channelTubePo.CategoryName;
                lblSubscribers.Text = "fans: " + channelTubePo.SubscriberCount.ToString();
                lblViews.Text = "views: " + channelTubePo.ChannelViewsCount.ToString();

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
                            lnkAddToFave.InnerText = "";
                            lnkAddToFave.InnerText = "Remove from Favorites";
                            isMyFavoriteChannel = true;
                        }
                    }
                    else
                    {
                        lnkAddToFave.Attributes.Add("onclick", "AddToFavorite()");
                        lnkAddToFave.Attributes.Add("title", "Add to Favorites");
                        isMyFavoriteChannel = false;
                    }
                }
                else
                {
                    // Successful login should be followed by addition of the channel to a list of favorite channgels
                    lnkAddToFave.Attributes.Add("onclick", "loginModal('sameLocation')");
                    lnkAddToFave.Attributes.Add("title", "Add to Favorites");
                    isMyFavoriteChannel = false;
                }
            }
        }

        private void AddMetaTags(ChannelTubePo channel, string absoluteUri)
        {
            var metaImageUrl = new HtmlMeta();
            metaImageUrl.Attributes.Add("itemprop", "image");
            metaImageUrl.Attributes.Add("content", ImageUtils.GetChannelImageUrl(channelTubePo.PictureUrl));
            Header.Controls.AddAt(0, metaImageUrl);


            var metaDescription = new HtmlMeta();
            metaDescription.Attributes.Add("itemprop", "description");
            metaDescription.Attributes.Add("content", channelTubePo.Description);
            Header.Controls.AddAt(1, metaDescription);


            var metaTitle = new HtmlMeta();
            metaTitle.Attributes.Add("itemprop", "name");
            metaTitle.Attributes.Add("content", channelTubePo.Name);
            Header.Controls.AddAt(2, metaTitle);

            var metaOgTitle = new HtmlMeta();
            metaOgTitle.Attributes.Add("property", "og:title");
            metaOgTitle.Attributes.Add("content", channel.Name);
            Header.Controls.AddAt(4, metaOgTitle);

            var metaOgType = new HtmlMeta();
            metaOgType.Attributes.Add("property", "og:type");
            metaOgType.Attributes.Add("content", "video");
            Header.Controls.AddAt(5, metaOgType);

            var metaOgImage = new HtmlMeta();
            metaOgImage.Attributes.Add("property", "og:image");
            metaOgImage.Attributes.Add("content", ImageUtils.GetChannelImageUrl(channel.PictureUrl));
            Header.Controls.AddAt(6, metaOgImage);

            var metaOgDescription = new HtmlMeta();
            metaOgDescription.Attributes.Add("property", "og:description");
            metaOgDescription.Attributes.Add("content", channel.Description);
            Header.Controls.AddAt(7, metaOgDescription);

            var metaOgSite = new HtmlMeta();
            metaOgSite.Attributes.Add("property", "og:site_name");
            metaOgSite.Attributes.Add("content", "Strimm.com");
            Header.Controls.AddAt(8, metaOgSite);

            var metaOgUrl = new HtmlMeta();
            metaOgUrl.Attributes.Add("property", "og:url");
            metaOgUrl.Attributes.Add("content", absoluteUri);
            Header.Controls.AddAt(9, metaOgUrl);

            var metaFbAppId = new HtmlMeta();
            metaFbAppId.Attributes.Add("property", "fb:app_id");
            metaFbAppId.Attributes.Add("content", "576305899083877");
            Header.Controls.AddAt(10, metaFbAppId);

            //$('head').append('<meta property="fb:app_id" content="576305899083877" /> ')
            //$('head').append('<meta property="og:title" content="' + '<%=channelName%>' + '" /> ');
            //$('head').append('<meta property="og:type" content="video" /> ');
            //$('head').append('<meta property="og:url" content="' + window.location.href + '" /> ');
            //$('head').append('<meta property="og:image" content="' + '<%=channelImgAvatarUrl%>' + '" /> ');
            //$('head').append('<meta property="og:description" content="' + '<%=channelDescription%>' + '" /> ');
            //$('head').append('<meta property="og:site_name" content="Strimm.com" /> ');
        }
    }
}