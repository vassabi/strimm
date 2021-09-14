using log4net;
using Strimm.Model;
using Strimm.Model.Projections;
using StrimmBL;
using Strimm.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Strimm.Shared;

namespace StrimmTube.UC
{
    public partial class UpdateChannelUC : System.Web.UI.UserControl
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(UpdateChannelUC));

        bool IsNewChannel = false;

        bool IsHasChannel;

        private int maxUserChannelCount;

        private int maxUploadFileSize;

        private string bucketName;

        private string amazonS3WsDomain;

        private int amazonS3FileUploadTimoutInSec;

        public int userId;

        public int channelTubeId { get; set; }

        public string channelCategory { get; set; }

        public int selectIndexCategory { get; set; }

        public string domainName { get; set; }

        public ChannelTube channel { get; set; }

        List<ChannelTubePo> channelTubeList { get; set; }

        private List<Category> allCategories;

        private string defaultChannelPictureUrl;

        public string channelname { get; set; }

        public bool isChannelCreateMode { get; set; }

        public string UserName { get; set; }


        protected void Page_Load(object sender, EventArgs e)
        {
            Initialize();

            Int32.TryParse(Session["UserId"].ToString(), out userId);

            if (userId == 0)
            {
                Response.Redirect("/home", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            else
            {
                var userPo = UserManage.GetUserPoByUserId(userId);
                UserName = userPo != null ? userPo.PublicUrl : String.Empty;
            }

            if (!IsPostBack)
            {
                ddlChannelCategory_Update.Items.Clear();
                allCategories.ForEach(x => ddlChannelCategory_Update.Items.Add(new ListItem(x.Name, x.CategoryId.ToString())));
                ddlChannelCategory_Update.DataBind();

                this.channelname = Page.RouteData.Values["ChannelName"] != null ? Page.RouteData.Values["ChannelName"].ToString() : String.Empty;

                this.channel = ChannelManage.GetChannelTubeByUrl(channelname);
                this.channelTubeId = channel.ChannelTubeId;

                ddlChannelCategory_Update.SelectedValue = channel.CategoryId.ToString();

                Session["ChannelTubeId"] = channel.ChannelTubeId;
                Session["ChannelTubeName"] = channel.Name;

                if (this.channel.UserId != this.userId)
                {
                    // This is not the user's channel
                    //Response.Redirect("/home", false);
                    //  Context.ApplicationInstance.CompleteRequest();
                }
                else
                {
                    lblChannelName_Update.Visible = true;
                    btnDeleteChannel_Update.Visible = true;

                    lblChannelName_Update.Text = this.channel.Name;
                    imgChannelAvatar_Update.ImageUrl = ImageUtils.GetChannelImageUrl(this.channel.PictureUrl);
                    selectIndexCategory = this.channel.CategoryId;
                    lblChannelUrl_Update.Text = ConfigurationManager.AppSettings["domainName"].ToString() + "/" + this.channel.Url;
                    btnSubmit_Update.Text = "Update";
                }
            }
        }

        protected void Initialize()
        {
            this.domainName = ConfigurationManager.AppSettings["domainName"];
            this.bucketName = ConfigurationManager.AppSettings["ImagesBucket"];
            this.amazonS3WsDomain = ConfigurationManager.AppSettings["AmazonS3WsDomain"];
            this.defaultChannelPictureUrl = ConfigurationManager.AppSettings["DefaultChannelPictureUrl"];

            if (!Int32.TryParse(ConfigurationManager.AppSettings["maxUserChannelCount"].ToString(), out this.maxUserChannelCount))
            {
                this.maxUserChannelCount = 10;
            }

            if (!Int32.TryParse(ConfigurationManager.AppSettings["maxUploadFileSize"].ToString(), out this.maxUploadFileSize))
            {
                this.maxUploadFileSize = 307200;
            }

            if (!Int32.TryParse(ConfigurationManager.AppSettings["AmazonS3FileUploadTimoutInSec"].ToString(), out this.amazonS3FileUploadTimoutInSec))
            {
                this.amazonS3FileUploadTimoutInSec = 10;
            }

            this.allCategories = ReferenceDataManage.GetAllCategories();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int channelId = 0;
            Int32.TryParse(Session["ChannelTubeId"].ToString(), out channelId);
            this.channelTubeId = isChannelCreateMode ? 0 : channelId;

            if (this.channelTubeId > 0)
            {
                UpdateChannel();
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int channelId = 0;
            Int32.TryParse(Session["ChannelTubeId"].ToString(), out channelId);
            this.channelTubeId = isChannelCreateMode ? 0 : channelId;
            
            if (this.channelTubeId > 0)
            {
                if (ChannelManage.DeleteChannelTubeById(channelTubeId))
                {
                   Response.Redirect(String.Format("/{0}", this.UserName), false);
                   Context.ApplicationInstance.CompleteRequest();
                }
                else
                {
                    lblMsg_Update.Text = "Unable to delete the channel. Please contact web administrator.";
                }
            }
        }

        private string UploadAvatarToS3(int userId, int channelTubeId, string fileName)
        {
            if (this.defaultChannelPictureUrl.EndsWith(fileName))
            {
                return null;
            }

            string pictureUrl = String.Empty;
            string s3fileName = String.Format("{0}/channel/{1}/{2}", userId, channelTubeId, fileName.Replace(' ', '_'));

            using (Stream streamAvatr = fuChannelAvatar_Update.FileContent)
            {
                if (channelTubeId > 0)
                {
                    string originalImageS3Filename = String.Format("{0}/channel/{1}", userId, channelTubeId);
                    AWSManage.DeletePrevImage(originalImageS3Filename, bucketName, s3fileName);
                }

                AWSManage.UploadAvatarToS3(s3fileName, streamAvatr, bucketName);

                bool continueIfBucketDoesNotExistsCheck = !AWSManage.CheckIfBucketExists(s3fileName, bucketName);

                if (continueIfBucketDoesNotExistsCheck)
                {
                    DateTime start = DateTime.Now;
                    DateTime end = DateTime.Now;
                    bool hasTimedout = false;

                    while (continueIfBucketDoesNotExistsCheck)
                    {
                        end = DateTime.Now;
                        int elapsedTimeInSec = (end - start).Seconds;

                        if (elapsedTimeInSec < this.amazonS3FileUploadTimoutInSec)
                        {
                            continueIfBucketDoesNotExistsCheck = !AWSManage.CheckIfBucketExists(s3fileName, bucketName);
                        }
                        else
                        {
                            continueIfBucketDoesNotExistsCheck = false;
                            hasTimedout = true;
                        }
                    }
                }

                pictureUrl = String.Format("{0}/{1}", this.bucketName, s3fileName);
            }

            return pictureUrl;
        }

        private void UpdateChannel()
        {
            channel = ChannelManage.GetChannelTubeById(channelTubeId);

            if (fuChannelAvatar_Update.HasFile)
            {
                string ext = Path.GetExtension(this.fuChannelAvatar_Update.PostedFile.FileName).ToUpper().Trim();

                bool isFileTiBig = fuChannelAvatar_Update.PostedFile.ContentLength > this.maxUploadFileSize;

                bool isExtentionIsValid = ext == ".JPG" || ext == ".PNG" || ext == ".GIF" || ext == ".JPEG";

                if (!isFileTiBig && isExtentionIsValid)
                {
                    channel.PictureUrl = UploadAvatarToS3(userId, channel.ChannelTubeId, this.fuChannelAvatar_Update.PostedFile.FileName);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "SizeAlertUp();", "SizeAlertUp();", true);
                    return;
                }

            }
            else
            {
                channel.PictureUrl = "/images/comingSoonBG.jpg";
            }

            channel.CategoryId = Int32.Parse(ddlChannelCategory_Update.SelectedValue); //Int32.Parse(category.Value);

            if (!chkBoxterms_Update.Checked)
            {
                lblMsg_Update.Text = "*Please agree with strimm.com terms of use";
                return;
            }
            else
            {
                btnSubmit_Update.Text = "Update";
              

                selectIndexCategory = channel.CategoryId;

                imgChannelAvatar_Update.ImageUrl = channel.PictureUrl;

                lblChannelUrl_Update.Text = ConfigurationManager.AppSettings["domainName"].ToString() + "/" + this.UserName + "/" + channel.Url;

                Session["ChannelTubeId"] = channel.ChannelTubeId;

                ChannelManage.UpdateChannelTube(channel);
                lblMsg_Update.Text = "*your channel has been updated";

                Response.Redirect(String.Format(UserName+"/my-network/{0}", channel.Url), false);
               Context.ApplicationInstance.CompleteRequest();
            }
        }

        protected void SelectedCategoryChanged(object sender, EventArgs e)
        {
        }
    }
}