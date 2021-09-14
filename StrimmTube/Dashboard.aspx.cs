using Strimm.Data.Repositories;
using Strimm.Model.Projections;
using Strimm.Model.WebModel;
using Strimm.Shared;
using StrimmBL;
using StrimmTube.UC;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace StrimmTube
{
    public partial class Dashboard : BasePage
    {
        UserBoard userBoard { get; set; }

        bool isBoardBelongToUser { get; set; }

        public  int userId { get; set; }

        List<ChannelTubeModel> channelList = new List<ChannelTubeModel>();

        public List<ChannelTubePo> topChannels { get; set; }

        public List<ChannelTubePo> favoriteChannels { get; set; }

        public int boardOwnerUserId { get; set; }

        public string userAvatar { get; set; }

        public string userName { get; set; }

        public string userDescription { get; set; }

        public int favoriteChannelCount { get; set; }

        public bool isOwner { get; set; }

        public DateTime ClientTime { get; set; }

        public string userBoardUrl { get; set; }

        public string howToPageTutorialVideoId { get; set; }

        protected void Page_Init(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetAllowResponseInBrowserHistory(false);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            howToPageTutorialVideoId = ConfigurationManager.AppSettings["NetworkPageTutorialVideoId"];

            userName = Page.RouteData.Values["UserName"] != null ? Page.RouteData.Values["UserName"].ToString() : String.Empty;
            userDescription = "Strimm is a free online video platform to create your own TV network or watch channels online.";

            if (!IsProd)
            {
                TurnOffCrawling();
            }

            if (Session["ClientTime"] != null)
            {
                string strClientTime = Session["ClientTime"].ToString();

                DateTime time;
                if (DateTime.TryParse(strClientTime, out time))
                {
                    this.ClientTime = time;
                }
                else
                {
                    this.ClientTime = DateTime.Now;
                }
            }
            else
            {
                this.ClientTime = DateTime.Now;
            }

            if (Session["userId"] != null)
            {
                int id = 0;
                if (Int32.TryParse(Session["userId"].ToString(), out id))
                {
                    userId = id;
                }
            }
            else
            {
                ancCreateChannel.Visible = false;
                //ancCreateChannel.Visible = false;
                btnStartTourHolder.Visible = false;
              //  ancToggleUserInfo.Visible = false;
            }

            if (this.ClientTime > DateTime.MinValue)
            {
                userBoard = BoardManage.GetUserBoardDataByPublicUrl(userName, this.ClientTime);

                if (userBoard != null)
                {
                    AddTitle(String.Format("{0} Network", PublicNameUtils.EncodeApostropheInPublicName(userBoard.UserName)), false);
                   // AddHeaderLink(userBoard.PublicUrl, "canonical", string.Empty, string.Empty);
                    userBoardUrl = userBoard.PublicUrl;
                    UpdateDescription(String.Format("{0} Network on Strimm", userBoard.UserName));

                    isOwner = userBoard.UserId == userId;

                    boardOwnerUserId = userBoard.UserId;
                    favoriteChannelCount = userBoard.SubscribedChannels != null ? userBoard.SubscribedChannels.Count : 0;


                    if (!String.IsNullOrEmpty(userBoard.ProfileImageUrl))
                    {
                        userAvatar = userBoard.ProfileImageUrl;
                    }

                    if (!String.IsNullOrEmpty(userBoard.UserStory))
                    {
                        userDescription = userBoard.UserStory;
                    }

                    var channelCount = userBoard.MyChannels.Count();
                    lblChannelCount.Text = channelCount.ToString();

                    if (!String.IsNullOrEmpty(userBoard.UserStory))
                    {
                        lblAbout.Text = userBoard.UserStory;
                    }
                    else
                    {
                        lblAbout.Visible = false;
                    }

                    channelList = userBoard.MyChannels;
                    BuildChannelControls(channelList);

                    ancCreateChannel.Visible = isOwner;// && channelList.Count <= 12;
                    btnStartTourHolder.Visible = isOwner;


                    lblUserInfo.Text = String.Format("{0}", userBoard.UserName);

                    lblCountry.Text = String.Format("{0}", userBoard.Country);

                    
                        //if (!String.IsNullOrEmpty(userBoard.UserStory))
                        //{
                        //    lblBio.Text = userBoard.UserStory.Replace("\n","<br/>");
                        //}
                        //else
                        //{
                        //    lblBio.Text = "Hi!" + "<br />"
                        //                  + "Thank you for visiting my page." + "<br />"
                        //                  + "I have not entered my Bio yet, but I will do it soon!";
                        //}
                            
                   

                    if (!String.IsNullOrEmpty(userBoard.ProfileImageUrl))
                    {
                        imgDashboardAvatar.ImageUrl = ImageUtils.GetProfileImageUrl(userBoard.ProfileImageUrl);
                        imgDashBoardAvatarPrev.ImageUrl = ImageUtils.GetProfileImageUrl(userBoard.ProfileImageUrl);
                        userAvatar = ImageUtils.GetProfileImageUrl(userBoard.ProfileImageUrl);
                    }
                    else
                    {

                        imgDashboardAvatar.ImageUrl = ImageUtils.DefaultAvatarImageAbsoluteUrl;
                        imgDashBoardAvatarPrev.ImageUrl = ImageUtils.DefaultAvatarImageAbsoluteUrl;

                        userAvatar = imgDashboardAvatar.ImageUrl;
                    }


                    if (!String.IsNullOrEmpty(userBoard.BackgroundImageUrl))
                    {
                        imgDashboard.ImageUrl = ImageUtils.GetBackgroundImageUrl(userBoard.BackgroundImageUrl);
                        imgDashboardBackgroundPrev.ImageUrl = ImageUtils.GetBackgroundImageUrl(userBoard.BackgroundImageUrl);
                    }
                   

                    if (userBoard.UserId != userId)
                    {
                        isBoardBelongToUser = false;
                        dboardHolder.Visible = false;
                        //ancEdit.Visible = false;
                        ancCrop.Visible = false;
                        ancCropBackground.Visible = false;
                        bioEditHolder.Visible = false;

                    }
                    else
                    {
                        isBoardBelongToUser = true;
                        if (!IsPostBack)
                        {
                            txtBio.Text = userBoard.UserStory;

                           
                        }
                    }

                    string socialTitle = base.GetNetworkPageSocialTitle(userBoard.UserName);
                    string socialDescription = base.GetNetworkPageSocialDescription();

                    this.userName = socialTitle;
                    this.userDescription = socialDescription;

                    AddSocialMetaTags(socialTitle, userAvatar, socialDescription, Request.Url.AbsoluteUri);
                }
                else
                {
                    Response.Redirect("/home", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
            }
            else
            {
                Response.Redirect("/home", false);
                Context.ApplicationInstance.CompleteRequest();
            }
        }

        private void BuildChannelControls(List<ChannelTubeModel> channelList)
        {
            if (channelList.Count > 0)
            {
                channelList.ForEach(c =>
                {
                    var bctrl = LoadControl("~/UC/BoardChannelBox.ascx") as BoardChannelBox;

                    bctrl.isMyChannel = userBoard.UserId == userId;
                    bctrl.editChannelUrl = String.Format("/" + userBoard.PublicUrl + "/my-studio/{0}", c.Url);
                    bctrl.channelHref = String.Format("/{0}/{1}", userBoard.PublicUrl, c.Url);
                    bctrl.channelName = c.Name;
                    bctrl.channelImage = ImageUtils.GetChannelImageUrl(c.PictureUrl);
                    bctrl.rating = c.Rating.ToString();
                    bctrl.views = c.ChannelViewsCount.ToString();
                    bctrl.subscribers = c.SubscriberCount.ToString();
                    bctrl.channelCategory = c.CategoryName;
                    bctrl.channelId = c.ChannelTubeId;
                    bctrl.userName = userName;
                    bctrl.IsOnAir = c.PlayingVideoTubeId > 0;

                    channelsHolder.Controls.Add(bctrl);
                });
            }
            else
            {
                
                //string welcomePopup = "ShowNewUserWelcomePopup('" + userBoard.FirstName + "')";
                //ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "ShowNewUserWelcomePopup", welcomePopup, true);
            }
        }

        protected void btnSaveDashBoardInfo_Click(object sender, EventArgs e)
        {
            string userPictureUrl = String.Empty;
            string backgroundUrl = String.Empty;
            string bucketName = ConfigurationManager.AppSettings["ImagesBucket"];
            bool isFileTiBig = false;
            bool isExtentionIsValid = true;

            if (fuAvatar.HasFile)
            {

                string ext = Path.GetExtension(this.fuAvatar.PostedFile.FileName);

                if (fuAvatar.PostedFile.ContentLength > 307200)
                {
                    isFileTiBig = true;
                }

                if (ext.ToUpper().Trim() != ".JPG" && ext.ToUpper() != ".PNG" && ext.ToUpper() != ".GIF" && ext.ToUpper() != ".JPEG")
                {
                    isExtentionIsValid = false;
                }

                if (!isFileTiBig && isExtentionIsValid)
                {
                    // userId+filename
                    string fileName = userBoard.UserId.ToString() + "/avatar/" + fuAvatar.FileName.Replace(' ', '_');
                    Stream streamAvatr = fuAvatar.FileContent;

                    // add it after userid will be active
                    // AWSManage.UploadAvatarToS3(userId.ToString()+"/"+fuImgAvatar.FileName, streamAvatr, "tubestrimmtest");
                    AWSManage.DeletePrevImage(userBoard.UserId.ToString() + "/avatar/", bucketName, fileName);
                    AWSManage.UploadAvatarToS3(fileName, streamAvatr, bucketName);

                    // add it to configurationmanager.appsettings bucket name
                    while (!AWSManage.CheckIfBucketExists(fileName, bucketName))
                    {
                        AWSManage.UploadAvatarToS3(fileName, streamAvatr, bucketName);
                    }

                    if (AWSManage.CheckIfBucketExists(fileName, bucketName))
                    {
                          userBoard.ProfileImageUrl = "https://s3.amazonaws.com/" + bucketName + "/" + fileName;
                     
                    }
                }
            }

            if (fuBackground.HasFile)
            {
                string ext = Path.GetExtension(this.fuBackground.PostedFile.FileName);

                if (fuBackground.PostedFile.ContentLength > 307200)
                {
                    isFileTiBig = true;
                }

                if (ext.ToUpper().Trim() != ".JPG" && ext.ToUpper() != ".PNG" && ext.ToUpper() != ".GIF" && ext.ToUpper() != ".JPEG")
                {
                    isExtentionIsValid = false;
                }

                if (!isFileTiBig && isExtentionIsValid)
                {
                    // userId+filename
                    string fileName = userBoard.UserId.ToString() + "/background/" + fuBackground.FileName.Replace(' ', '_');
                    Stream streamAvatr = fuBackground.FileContent;

                    // add it after userid will be active
                    // AWSManage.UploadAvatarToS3(userId.ToString()+"/"+fuImgAvatar.FileName, streamAvatr, "tubestrimmtest");
                    AWSManage.DeletePrevImage(userBoard.UserId.ToString() + "/background/", bucketName, fileName);
                    AWSManage.UploadAvatarToS3(fileName, streamAvatr, bucketName);

                    // add it to configurationmanager.appsettings bucket name
                    while (!AWSManage.CheckIfBucketExists(fileName, bucketName))
                    {
                        AWSManage.UploadAvatarToS3(fileName, streamAvatr, bucketName);
                    }

                    if (AWSManage.CheckIfBucketExists(fileName, bucketName))
                    {
                        userBoard.BackgroundImageUrl = String.Format("{0}/{1}", bucketName, fileName);                      
                    }
                }
            }

          //  userBoard.BoardName = txtTitle.Text;
            userBoard.UserStory = txtBio.Text;
            userBoard.BackgroundImageUrl = userBoard.BackgroundImageUrl;
            userBoard.ProfileImageUrl = userBoard.ProfileImageUrl;
            
            //Board manage update board
            BoardManage.UpdateUserBoard(userBoard.UserId, userBoard.BoardName, userBoard.ProfileImageUrl, userBoard.BackgroundImageUrl, userBoard.UserStory);

            userBoard.BackgroundImageUrl = ImageUtils.GetBackgroundImageUrl(userBoard.BackgroundImageUrl);
            userBoard.ProfileImageUrl = ImageUtils.GetProfileImageUrl(userBoard.ProfileImageUrl);

            Response.Redirect(Request.Url.AbsoluteUri, false);
            Context.ApplicationInstance.CompleteRequest();
        }
    }
}