using Strimm.Model;
using Strimm.Model.Projections;
using StrimmBL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
//using System.Web.UI;

namespace StrimmTube.UC
{
    public partial class TopMenuAfterLogin : System.Web.UI.UserControl
    {
        public int maxUploadFileSize;

        private string bucketName;

        private string amazonS3WsDomain;

        private int amazonS3FileUploadTimoutInSec;

        public string userName { get; set; }
        
        public string boardUrl { get; set; }
        
        public string userUrl { get; set; }

        public int userId { get; set; }
        
        public string controlPanelLink { get; set; }

        public string channelName { get; set; }

        public string channelManageUrl { get; set; }

        public int channelTubeId { get; set; }

        public string channelCategory { get; set; }

        public int selectIndexCategory { get; set; }

        public string domainName { get; set; }

        public ChannelTube channel { get; set; }

        List<ChannelTubePo> channelTubeList { get; set; }

        private int maxUserChannelCount;

        public List<Category> allCategories;

        public string browseMenuHtml;

        private string defaultChannelPictureUrl;

        public int channelCount { get; set; }

        public int maxUserChannels { get; set; }

        public bool isUserlockedOut { get; set; }
        
        public bool isUserDeleted { get; set; }

        public bool isProEnabled { get; set; }

        public string firstName { get; set; }

        public bool hasInterests { get; set; }

        public string accountNumber { get; set; }
        protected void Page_Init(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetAllowResponseInBrowserHistory(false);
           
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            //Initialize();

            if (!IsPostBack)
            {

            }

            RetrieveBrowseCategoriesWithCounts();

            if (Session["userId"] != null)
            {
                this.domainName = ConfigurationManager.AppSettings["domainName"];
                userId = int.Parse(Session["userId"].ToString());

                UserPo user = UserManage.GetUserPoByUserId(userId);

                if (user != null)
                {
                    isProEnabled = user.IsProEnabled;
                    isUserlockedOut = user.IsLockedOut;
                    isUserDeleted = user.IsDeleted;
                    userName = user.FirstName;
                    userUrl = user.PublicUrl;
                    accountNumber = user.AccountNumber;
                    if (!Int32.TryParse(ConfigurationManager.AppSettings["maxUserChannelCount"].ToString(), out maxUserChannelCount))
                    {
                        this.maxUserChannelCount = 12;
                        maxUserChannels = 12;
                    }
                    else
                    {
                        maxUserChannels = maxUserChannelCount;
                    }

                    var channelList = ChannelManage.GetChannelTubesForUser(userId);

                    if (channelList != null)
                    {
                        if (channelList.Count != 0)
                        {
                            ChannelTubePo channel = channelList[0];
                            channelManageUrl = "/" + userUrl + "/my-studio/" + channel.Url;
                            channelCount = channelList.Count;
                            ancMyStudio.HRef = channelManageUrl;
                            ancMyStudioMobile.HRef = channelManageUrl;
                        }
                        else
                        {
                            channelManageUrl = "#";
                            ancMyStudio.Attributes.Add("onclick", "OpenCreateChannelConfirmation()");
                            ancMyStudioMobile.Attributes.Add("onclick", "OpenCreateChannelConfirmation()");
                        }
                    }
                    firstName = user.FirstName;
                    hasInterests = user.HasInterests;

                    boardUrl = "/" + userUrl;
                }
            }
            else
            {
                Session["userId"] = null;
                Response.Redirect("/home", false);
                Context.ApplicationInstance.CompleteRequest();
                isUserDeleted = true;
                return;
            }
        }

        protected void RetrieveBrowseCategoriesWithCounts()
        {
            DateTime clientTime = DateTime.Now;

            if (Session["ClientTime"] != null)
            {
                string strClientTime = Session["ClientTime"].ToString();

                DateTime time;
                if (DateTime.TryParse(strClientTime, out time))
                {
                    clientTime = time > DateTime.MinValue ? time : clientTime;
                }
            }

            var categories = ReferenceDataManage.GetChannelCategoriesWithCurrentlyPlayingChannelsCountForBrowseChannels(clientTime);

            var currentColumn = 0;
            var currentRow = 0;
            var maxColumnCount = 6;
            var maxRowCount = 2;

            if (categories != null) {
                string html = String.Empty;

                categories.ForEach(x => {
                    html += "<li class='";

                    if (currentRow < maxRowCount) 
                    {
                        if (currentColumn == 0) 
                        {
                            html += "first firstColumn";
                        }
                        else if (currentColumn == maxColumnCount) 
                        {
                            html += "first lastColumn";
                        }
                        else 
                        {
                            html += "first";
                        }
                    }
                    else 
                    {
                        if (currentColumn == 0) 
                        {
                            html += "last firstColumn";
                        }
                        else if (currentColumn == maxColumnCount) 
                        {
                            html += "last lastColumn";
                        }
                        else 
                        {
                            html += "last";
                        }
                    }

                    html += "'><a href='/browse-channel?category=" + x.Name + "'>" + x.Name;
                    html += "<div id='ch" + x.CategoryId + "' class='channelCount'>" + (x.ChannelCount == 0 ? String.Empty : x.ChannelCount.ToString()) + "</div></a></li>";

                    currentColumn += 1;

                    if (currentColumn > maxColumnCount) {
                        currentColumn = 0;
                        currentRow += 1;
                    }
                });

                browseMenuHtml = html;
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
        }

        protected void SelectedCategoryChanged(object sender, EventArgs e)
        {
        }

        protected void createChannelLink_Click(object sender, EventArgs e)
        {
        }
    }
}