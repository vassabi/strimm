using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using Strimm.Model;
using StrimmBL;
using System.Web.UI;
using System.Text;
using StrimmTube.admcp.mgmt.UC;
using System.IO;
using Strimm.Shared;
using System.Configuration;
using Strimm.Model.Projections;
using Strimm.Model.WebModel;
using log4net;

namespace StrimmTube.admcp.mgmt.WebServiceAdmin
{
    /// <summary>
    /// Summary description for UserManagmentService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class UserManagmentService : System.Web.Services.WebService
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(UserManagmentService));

        internal class UserInfo
        {
            public string name, gender, userName, accountNum, email, dateOfSignUp, company, country, state, address, city, zipCode, board, adminNotes, publicUrl, accountStatus;
            public int age, channels, userId;
            public bool islocked;
            public bool isProEnabled;
            public bool isSubscriber;

            public int SubscriberDomainCount { get; set; }
        }

        [WebMethod]
        public List<SubscriberDomain> GetAuthorizedDomainsForUser(int userId)
        {
            List<SubscriberDomain> domains = null;

            try
            {
                domains = UserManage.GetUserDomainsForSubscriberByUserId(userId);
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieved authorized subscriber domains for user with id={0}", userId), ex);
            }

            return domains;
        }

        [WebMethod]
        public List<SubscriberDomain> AuthorizedNewDomainForUser(int userId, string domain)
        {
            List<SubscriberDomain> domains = null;

            try
            {
                domains = UserManage.AddSubscriberDomainToUserByUserId(domain, userId);
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to add authorized subscriber domain '{0} for user with id={1}", domain, userId), ex);
            }

            return domains;
        }

        [WebMethod]
        public List<SubscriberDomain> RemoveDomainAuthorizationForUser(int domainId, int userId)
        {
            List<SubscriberDomain> domains = null;

            try
            {
                domains = UserManage.DeleteSubscriberDomainByIdAndUserId(domainId, userId);
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to remove subscriber domain with id={0} for user with id={1}", domainId, userId), ex);
            }

            return domains;
        }

        [WebMethod]
        public string GetUserInfo(string inputText, int selectedOption)
        {
            UserInfo userInfo = new UserInfo();
            var user = new UserPo();
            switch (selectedOption)
            {
                case 1:
                    user = UserManage.GetUserByUserName(inputText);
                    if (user == null)
                    {
                        user = UserManage.GetUserByPublicUrl(PublicNameUtils.GetUrl(inputText));
                    }
                    break;
                case 2:
                    user = UserManage.GetUserByAccountNumber(inputText);
                    break;
                case 3:
                    user = UserManage.GetUserByEmail(inputText);
                    break;
                case 4:
                    ChannelTube channel = ChannelManage.GetChannelTubeByName(inputText);
                    if (channel != null)
                    {
                        user = UserManage.GetUserPoByUserId(channel.UserId);
                    }
                    break;
            }
            if (user != null)
            {
                userInfo.userId = user.UserId;
                userInfo.name = user.FirstName + " " + user.LastName;
                userInfo.gender = user.Gender;
                userInfo.age = DateTimeUtils.GetAge(user.BirthDate);
                userInfo.userName = user.UserName;
                userInfo.publicUrl = user.PublicUrl;
                userInfo.accountNum = user.AccountNumber;
                userInfo.email = user.Email;
                userInfo.dateOfSignUp = user.CreatedDate.ToString();
                userInfo.isProEnabled = user.IsProEnabled;
                userInfo.isSubscriber = user.IsSubscriber;
                userInfo.SubscriberDomainCount = user.SubscriberDomainCount ?? 0;

                if(user.IsDeleted)
                {
                    userInfo.accountStatus = "deleted";
                }
                else if(user.IsLockedOut)
                {
                    userInfo.accountStatus = "locked out";
                }
                else
                {
                    userInfo.accountStatus = "active";
                }

                
                if(!String.IsNullOrEmpty(user.Company))
                {
                userInfo.company = user.Company;
                }
                else
                {
                    userInfo.company = "N/A";
                }

                
                userInfo.country = user.Country;
                if(!String.IsNullOrEmpty(user.StateOrProvince))
                {
                    userInfo.state = user.StateOrProvince;
                }
                else
                {
                     userInfo.state = "N/A";
                }

                if (!String.IsNullOrEmpty(user.Address))
                {
                userInfo.address = user.Address;
                }
                else
                {
                    userInfo.address = "N/A";
                }
                if (!String.IsNullOrEmpty(user.City))
                {
                    userInfo.city = user.City;
                }
                else
                {
                    userInfo.city = "N/A";
                }
                if (!String.IsNullOrEmpty(user.ZipCode))
                {
                    userInfo.zipCode = user.ZipCode;
                }
                else
                {
                    userInfo.zipCode = "N/A";
                }
                
                
                
                userInfo.channels = ChannelManage.GetChannelTubesForUser(user.UserId).Count;
                userInfo.userId = user.UserId;
                userInfo.islocked = user.IsLockedOut;
                
                //TODO admin notes
                //if(String.IsNullOrEmpty(user.))
                //{
                //    userInfo.adminNotes = "No notes for current user";
                //}
                //else
                //{
                //    userInfo.adminNotes = user.adminNotes;
                //}
                if (user.UserName != null)
                {
                    userInfo.board = "yes";
                }
                else
                {
                    userInfo.board = "no";
                }

                string output = "";
                JavaScriptSerializer js = new JavaScriptSerializer();
                output += js.Serialize(userInfo) + ",";
                output = output.TrimEnd(',');
                output = "[" + output + "]";

                return output;
            }
            else
            {
                return "0";
            }

        }

        [WebMethod]
        public List<ChannelTubePo> GetUserChannels(int userId)
        {
            Page page = new Page();
            StringBuilder sb = new StringBuilder();
            //retrieving list of videoschedule from the 
            List<ChannelTubePo> channelList = ChannelManage.GetChannelTubesForAdmin(userId);

            //if (channelList.Count != 0)
            //{
            //    foreach (var c in channelList)
            //    {
            //        string path = "~/admcp/mgmt/UC/ChannelBoxUC.ascx";
            //        ChannelBoxUC ctrl = (ChannelBoxUC)page.LoadControl(path);
            //        ctrl.channelId = c.ChannelTubeId;
            //        ctrl.channelUrl = c.Url;
            //        ctrl.channelName = c.Name;
                
            //        StringWriter output = new StringWriter(sb);
            //        HtmlTextWriter hw = new HtmlTextWriter(output);
            //        ctrl.RenderControl(hw);
            //    }
                
            //}
            return channelList.OrderBy(x=>x.IsDeleted).ToList();
        }

        [WebMethod]
        public void ClearSchedules(string selectedChannelId)
        {
            string emailFrom        = ConfigurationManager.AppSettings["adminEmail"].ToString();
            string[] channelIdArr   = selectedChannelId.Split(',');
            ChannelTube channel     = null;
            int typeOfAction        = 1;    //clear schedules

            string publicname = String.Empty;

            int channelId = 0;

            Int32.TryParse(channelIdArr[0], out channelId);

            if(channelId != 0)
            {
                channel = ChannelManage.GetChannelTubeById(channelId);
                //TODO make method below

                ScheduleManage.DeleteAllSchedulesByChannelId(channelId);
                UserPo user = UserManage.GetUserPoByChannelName(channel.Name);
                publicname = user.UserName;
                Uri templateUri = new Uri(Server.MapPath("~/Emails/ClearScheduleNotification.html"));

                EmailUtils.SendEmailNotification(user.FirstName, user.AccountNumber, emailFrom, user.Email, typeOfAction, new List<String>() { channel.Name }, templateUri, publicname);
            }

         
        }
        
        [WebMethod]
        public void DeleteChannels(int selectedChannelId)
        {
            string channelName = ChannelManage.GetChannelTubeById(selectedChannelId).Name;
            string emailFrom = ConfigurationManager.AppSettings["adminEmail"].ToString();
            UserPo user = UserManage.GetUserPoByChannelName(channelName);
            Uri templateUri = new Uri(Server.MapPath("~/Emails/DeletedChannelNotification.html"));
            string publicName = user.UserName;
            EmailUtils.SendEmailNotification(user.FirstName, user.AccountNumber, emailFrom, user.Email, 2, new List<string>() {channelName }, templateUri, publicName);
            ChannelManage.DeleteChannelTubeById(selectedChannelId);
            
            }

        [WebMethod]
        public void LockUnclockUser(string acountNum, bool locking)
        {
            string emailFrom = ConfigurationManager.AppSettings["adminEmail"].ToString();
            Uri templateUri;
            UserManage.LockUnlockUserByAccountNumber(acountNum, locking);
       
           
                UserPo user = UserManage.GetUserByAccountNumber(acountNum);
                string publicName = user.UserName;
                if(locking)
                {
                    templateUri = new Uri(Server.MapPath("~/Emails/LockedUserNotification.html"));
                }
                else
                {
                    templateUri = new Uri(Server.MapPath("~/Emails/UnlockAccountNotification.html"));
                }
                    
                EmailUtils.SendEmailNotification(user.FirstName, user.AccountNumber, emailFrom, user.Email, 3, new List<string>() { "0" }, templateUri, publicName);
           
        }

        [WebMethod]
        public ResponseMessageModel EnableProForUser(string accountNum, bool isProEnabled)
        {
            ResponseMessageModel response = new ResponseMessageModel();

            try {
                UserPo user = UserManage.EnableProForUserByAccountNumberWithGet(accountNum, isProEnabled);

                response.IsSuccess = (user != null);
                response.Response = user;

                if (!response.IsSuccess)
                {
                    response.Message = "Failed to update user account for pro";
                }
            }
            catch (Exception ex) {
                response.Message = "Error occured while enabling pro features for user";
                response.IsSuccess = false;
            }

            return response;
        }

        [WebMethod]
        public ResponseMessageModel AddSubscriptionToUser(string accountNum, bool isSubscriber)
        {
            ResponseMessageModel response = new ResponseMessageModel();

            try
        {
                UserPo user = UserManage.AddSubscriptionToUserByAccountNumberWithGet(accountNum, isSubscriber);

                response.IsSuccess = (user != null);
                response.Response = user;
       
                if (!response.IsSuccess)
                {
                    response.Message = "Failed to update user account to enable subscription";
                }
            }
            catch (Exception ex)
            {
                response.Message = "Error occured while setting subscription for user";
                response.IsSuccess = false;
            }

            return response;
        }

        [WebMethod]
        public void SaveNotes(string notes,string userAccountNum)
        {
            //User user = UserManage.GetUserByAccountNumber(userAccountNum);
            //user.adminNotes = notes;
            //UserManage.UpdateUser(user);
        }

        [WebMethod]
        public void DeleteAccount(string userName)
        {
            string emailFrom = ConfigurationManager.AppSettings["adminEmail"].ToString();

            UserPo user = UserManage.GetUserByUserName(userName);
            if(user!=null)
            {
                Uri templateUri = new Uri(Server.MapPath("~/Emails/DeletedAccountNotification.html"));
                string publicName = user.UserName;
                EmailUtils.SendEmailNotification(user.FirstName, user.AccountNumber, emailFrom, user.Email, 4, new List<string>() { "0" }, templateUri, publicName);
                List<ChannelTubePo> channelList = ChannelManage.GetChannelTubesForUser(user.UserId);
                UserManage.DeleteUser(user.UserId);
            }
                    }
                 
        [WebMethod]
        public bool SendWelcomeEmailToUser(string userName)
        {
            bool isSuccess = false;
                    
            UserPo user = UserManage.GetUserByUserName(userName);

            if (user != null)
                {
                isSuccess = EmailManage.SendWelcomeEmail(user, new Uri(Server.MapPath("~/Emails/welcomePage.html")));
        }

            return isSuccess;
        }
    }
}
