using Strimm.Model.Projections;
using StrimmBL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StrimmTube.UC
{
    public partial class DashboardProfileUC : System.Web.UI.UserControl
    {
        public string title { get; set; }
        public string userPictureUrl { get; set; }
        public string backgroundPictureUrl { get; set; }
        public string story { get; set; }
        public UserPo user { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            txtTitle.Text = title;
            txtBio.Text = story;

        }

        protected void btnSave_Click(object sender, EventArgs e)
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
                    string fileName = user.UserId.ToString() + "/avatar/" + fuAvatar.FileName.Replace(' ', '_');
                    Stream streamAvatr = fuAvatar.FileContent;

                    // add it after userid will be active
                    // AWSManage.UploadAvatarToS3(userId.ToString()+"/"+fuImgAvatar.FileName, streamAvatr, "tubestrimmtest");
                    AWSManage.DeletePrevImage(user.UserId.ToString() + "/avatar/", bucketName, fileName);
                    AWSManage.UploadAvatarToS3(fileName, streamAvatr, bucketName);

                    // add it to configurationmanager.appsettings bucket name
                    while (!AWSManage.CheckIfBucketExists(fileName, bucketName))
                    {
                        AWSManage.UploadAvatarToS3(fileName, streamAvatr, bucketName);
                    }

                    if (AWSManage.CheckIfBucketExists(fileName, bucketName))
                    {
                        userPictureUrl = "https://s3.amazonaws.com/" + bucketName + "/" + fileName;
                        user.ProfileImageUrl = userPictureUrl;
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
                    string fileName = user.UserId.ToString() + "/background/" + fuAvatar.FileName.Replace(' ', '_');
                    Stream streamAvatr = fuBackground.FileContent;

                    // add it after userid will be active
                    // AWSManage.UploadAvatarToS3(userId.ToString()+"/"+fuImgAvatar.FileName, streamAvatr, "tubestrimmtest");
                    AWSManage.DeletePrevImage(user.UserId.ToString() + "/background/", bucketName, fileName);
                    AWSManage.UploadAvatarToS3(fileName, streamAvatr, bucketName);

                    // add it to configurationmanager.appsettings bucket name
                    while (!AWSManage.CheckIfBucketExists(fileName, bucketName))
                    {
                        AWSManage.UploadAvatarToS3(fileName, streamAvatr, bucketName);
                    }

                    if (AWSManage.CheckIfBucketExists(fileName, bucketName))
                    {
                        backgroundPictureUrl = String.Format("{0}/{1}", bucketName, fileName);
                        user.BackgroundImageUrl = backgroundPictureUrl;
                    }
                }


            }
            if (title != String.Empty)
            {
                user.BoardName = title;

            }
            if(user.UserStory!=String.Empty)
            {
                user.UserStory = story;
            }
           
           
            UserManage.UpdateUser(user);
            Response.Redirect(Request.Url.AbsoluteUri);
        }
    }
}
