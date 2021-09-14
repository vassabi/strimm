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

namespace StrimmTube.UC
{
    public partial class CreateChannelUC : System.Web.UI.UserControl
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(CreateChannelUC));

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
                UserName = userPo != null ? userPo.UserName : String.Empty;
            }

            if (!IsPostBack)
            {
                ddlChannelCategory.Items.Clear();
                allCategories.ForEach(x => ddlChannelCategory.Items.Add(new ListItem(x.Name, x.CategoryId.ToString())));
                ddlChannelCategory.DataBind();

                if (String.IsNullOrEmpty(channelname))
                {
                    txtChannelName.Visible = true;
                    lblChannelName.Visible = false;

                    // Get channels of user if < maxUserChannelCount creation allowed
                    this.channelTubeList = ChannelManage.GetChannelTubesForUser(userId);

                    btnDeleteChannel.Visible = false;

                    if (this.channelTubeList.Count >= this.maxUserChannelCount)
                    {
                        //Response.Redirect("/home", false);
                        //Context.ApplicationInstance.CompleteRequest();
                    }
                }
                else
                {

                   // var channelname = Page.RouteData.Values["ChannelName"].ToString();
                   // ChannelManagementUC chnlUc = (ChannelManagementUC)LoadControl("~/UC/ChannelManagementUC.ascx");
                   // divBackendmenuHolder.Controls.Add(chnlUc);
                    this.channel = ChannelManage.GetChannelTubeByUrl(channelname);
                    this.channelTubeId = channel.ChannelTubeId;

                    ddlChannelCategory.SelectedValue = channel.CategoryId.ToString();

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
                        txtChannelName.Visible = false;
                        lblChannelName.Visible = true;
                        btnDeleteChannel.Visible = true;

                        lblChannelName.Text = this.channel.Name;
                        imgChannelAvatar.ImageUrl = this.channel.PictureUrl ?? this.defaultChannelPictureUrl;
                        selectIndexCategory = this.channel.CategoryId;
                        lblChannelUrl.Text = ConfigurationManager.AppSettings["domainName"].ToString() + "/" + this.channel.Url;
                        btnSubmit.Text = "Update";
                    }
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

            //if (userId == 0)
            //{
            //    Response.Redirect("/home", false);
            //    Context.ApplicationInstance.CompleteRequest();
            //}
            //else
            //{
            //    userId = int.Parse(Session["userId"].ToString());
            //}

            this.allCategories = ReferenceDataManage.GetAllCategories();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //int channelId = 0;
            //Int32.TryParse(Session["ChannelTubeId"].ToString(), out channelId);
          //  this.channelTubeId = isChannelCreateMode ? 0 : channelId;

            if (!String.IsNullOrEmpty(channelname))
            {
                UpdateChannel();
            }
            else
            {
                //CreateNewChannel();
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.channelTubeId > 0)
            {
                if (ChannelManage.DeleteChannelTubeById(channelTubeId))
                {
                    var user = UserManage.GetUserPoByUserId(userId);
                   Response.Redirect(String.Format("/board/{0}", user.UserName), false);
                   Context.ApplicationInstance.CompleteRequest();
                }
                else
                {
                    lblMsg.Text = "Unable to delete the channel. Please contact web administrator.";
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

            using (Stream streamAvatr = fuChannelAvatar.FileContent)
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

                pictureUrl = String.Format("/{0}/{1}", this.bucketName, s3fileName);
            }

            return pictureUrl;
        }

        private void UpdateChannel()
        {
            channel = ChannelManage.GetChannelTubeById(channelTubeId);

            if (fuChannelAvatar.HasFile)
            {
                string ext = Path.GetExtension(this.fuChannelAvatar.PostedFile.FileName).ToUpper().Trim();

                bool isFileTiBig = fuChannelAvatar.PostedFile.ContentLength > this.maxUploadFileSize;

                bool isExtentionIsValid = ext == ".JPG" || ext == ".PNG" || ext == ".GIF" || ext == ".JPEG";

                if (!isFileTiBig && isExtentionIsValid)
                {
                    channel.PictureUrl = UploadAvatarToS3(userId, channel.ChannelTubeId, this.fuChannelAvatar.PostedFile.FileName);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "SizeAlertUp();", "SizeAlertUp();", true);
                    return;
                }

            }
            else
            {
                if(String.IsNullOrEmpty(channel.PictureUrl))
                {
                    channel.PictureUrl = "/images/comingSoonBG.jpg";
                }
                
            }

            channel.CategoryId = Int32.Parse(ddlChannelCategory.SelectedValue); //Int32.Parse(category.Value);

            if (!chkBoxterms.Checked)
            {
                lblMsg.Text = "*Please agree with strimm.com terms of use";
                return;
            }
            else
            {
                btnSubmit.Text = "Update";
              

                selectIndexCategory = channel.CategoryId;

                imgChannelAvatar.ImageUrl = channel.PictureUrl;

                lblChannelUrl.Text = ConfigurationManager.AppSettings["domainName"].ToString() + "/" + channel.Url;

                Session["ChannelTubeId"] = channel.ChannelTubeId;

                ChannelManage.UpdateChannelTube(channel);
                lblMsg.Text = "*your channel has been updated";

                Response.Redirect(String.Format(UserName+"/my-network/{0}", channel.Url), false);
               Context.ApplicationInstance.CompleteRequest();
            }
        }

        //private void CreateNewChannel()
        //{
        //    // create new channel
        //    channel = new ChannelTube();

        //    // check if username is available
        //    RegexOptions options = RegexOptions.None;
        //    Regex reg = new Regex(@"[ ]{2,}", options);

        //    if (String.IsNullOrWhiteSpace(txtChannelName.Text))
        //    {
        //        lblMsg.Text = "Please provide channel name";

        //        return;
        //    }

        //    string channelNameLength = txtChannelName.Text.Replace(" ", string.Empty);

        //    if (channelNameLength.Length < 3)
        //    {
        //        lblMsg.Text = "*channel name must have 3 characters minimum";

        //        return;
        //    }

        //    List<string> wordsInName = new List<string>();

        //    if ((txtChannelName.Text != String.Empty) && (!ChannelManage.IsChannelNameExists(txtChannelName.Text)))
        //    {
        //        var nameTrimmed = txtChannelName.Text.TrimEnd();
        //        Regex specialChar = new Regex(@"^[a-zA-Z0-9_@.-]+$");
        //        string[] name = nameTrimmed.Split(null);

        //        foreach (var n in name)
        //        {
        //            if (!String.IsNullOrWhiteSpace(n))
        //            {
        //                wordsInName.Add(n);
        //            }
        //        }

        //        foreach (var n in name)
        //        {
        //            if (!specialChar.IsMatch(n))
        //            {
        //                lblMsg.Text = "No double spacing, letters and/or digits only in the channel name";
        //                return;
        //            }
        //        }

        //        if (wordsInName.Count != 0)
        //        {

        //            string channelName = String.Join(" ", wordsInName.ToArray());
        //            if (ChannelManage.IsChannelNameUnique(channelName))
        //            {
        //                channel.Name = channelName;
        //            }
        //            else
        //            {
        //                lblMsg.Text = "this name may be reserved as a premium name for trademark holders. Please contact us with proof of legal rights to this name, if you wish to have it, or choose another name.";
        //                return;
        //            }
        //        }
        //        else
        //        {
        //            lblMsg.Text = "Please choose another channel name";
        //            return;
        //        }
        //    }

        //    else
        //    {
        //        lblMsg.Text = "*channel name is not available, please choose another channel name";
        //        return;
        //    }

           

        //    channel.CategoryId = Int32.Parse(ddlChannelCategory.SelectedItem.Value);// SelectedIndex; //Int32.Parse(category.Value);

        //    channel.Url = String.Join("", wordsInName.ToArray()); ;

        //    channel.UserId = userId;

        //    if (userId > 0)
        //    {
        //        this.channelTubeList = ChannelManage.GetChannelTubesForUser(userId);

        //        if (this.channelTubeList.Count >= this.maxUserChannelCount)
        //        {
        //            lblMsg.Text = String.Format("*You can create only up to {0} channels", this.maxUserChannelCount);
        //        }
        //        else
        //        {
        //            ChannelTube channelTube;
        //            int randomVideoCount = 0;
        //            if (!Int32.TryParse(ConfigurationManager.AppSettings["RandomPublicVideosAddCountForChannel"].ToString(), out randomVideoCount))
        //            {
        //                randomVideoCount = 10;
        //            }

        //            bool insertPublicChannels = this.channelTubeList == null || this.channelTubeList.Count == 0;

        //            if (ChannelManage.CreateChannelTube(channel.CategoryId,1, channel.Name, channel.Description, channel.PictureUrl, channel.Url, userId, insertPublicChannels, randomVideoCount, out channelTube))
        //            {
        //                channel = channelTube;

        //                if (fuChannelAvatar.HasFile)
        //                {
        //                    string ext = Path.GetExtension(this.fuChannelAvatar.PostedFile.FileName).ToUpper().Trim();

        //                    bool isFileTiBig = fuChannelAvatar.PostedFile.ContentLength > this.maxUploadFileSize;

        //                    bool isExtentionIsValid = ext == ".JPG" || ext == ".PNG" || ext == ".GIF" || ext == ".JPEG";

        //                    if (!isFileTiBig && isExtentionIsValid)
        //                    {
        //                        channel.PictureUrl = UploadAvatarToS3(userId, channel.ChannelTubeId, fuChannelAvatar.FileName);
        //                        ChannelManage.UpdateChannelTube(channel);
        //                    }
        //                    else
        //                    {
        //                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "SizeAlertUp();", "SizeAlertUp();", true);
        //                        return;
        //                    }


        //                }
        //                else
        //                {
        //                    channel.PictureUrl = "/images/comingSoonBG.jpg";
        //                }
        //                Session["selectedIndex"] = null;
        //                Session["ChannelTubeId"] = channel.ChannelTubeId;
        //                Session["newChannel"] = "new";

        //                var user = UserManage.GetUserPoByUserId(userId);

        //                  Response.Redirect(String.Format(UserName+"/my-network/{0}", channel.Url), false);
        //                  Context.ApplicationInstance.CompleteRequest();
        //            }
        //            else
        //            {
        //                lblMsg.Text = "Failed to create channel";
        //            }
        //        }
        //    }
        //    //delete
        //}

        protected void SelectedCategoryChanged(object sender, EventArgs e)
        {
        }
    }
}