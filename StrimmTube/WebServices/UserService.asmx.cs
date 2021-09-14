using StrimmBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Configuration;
using System.Collections.Specialized;
using System.Data;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Text;
using StrimmTube.UC;
using System.IO;
using System.Web.UI.HtmlControls;
using Strimm.Model.Projections;
using Strimm.Shared;
using Strimm.Model;
using Strimm.Model.Projections;
using log4net;
using Strimm.Model.WebModel;
using System.Globalization;
using Strimm.Model.Criteria;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using System.Collections;
using Strimm.Model.Order;
using System.Threading;
using System.Web.Security;
using System.Web.Configuration;

namespace StrimmTube.WebServices
{
    /// <summary>
    /// Summary description for UserService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class UserService : System.Web.Services.WebService
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(UserService));
        private static readonly string stringClientDateTimeFormat = "MM/dd/yyyy HH:mm";
        public class CountryInfo
        {
            public string text;
            public int value;
            public int selectedIndex;
            public bool selected;
        }

        /// <summary>
        /// This method will authenticate the user using specified
        /// username and password
        /// </summary>
        /// <param name="userName">Username</param>
        /// <param name="password">Password</param>
        /// <returns></returns>
        [WebMethod(EnableSession = true)]
        public bool IsValidSession()
        {
            return HttpContext.Current.Session["userId"] != null &&
                    !String.IsNullOrEmpty(HttpContext.Current.Session["userId"].ToString()) &&
                    Convert.ToInt32(HttpContext.Current.Session["userId"].ToString()) > 0;
        }

        /// <summary>
        /// This method will authenticate the user using specified
        /// username and password
        /// </summary>
        /// <param name="userName">Username</param>
        /// <param name="password">Password</param>
        /// <returns></returns>
        [WebMethod(EnableSession = true)]
        public string Login(string userName, string password)
        {
            UserPo user = null;

            if (UserManage.Login(userName, password, out user))
            {
                if (HttpContext.Current.Session["userId"] == null)
                {
                    Logger.Debug(String.Format("Added session for user {0}", user.UserId));
                    HttpContext.Current.Session.Add("userId", user.UserId);
                }
                else
                {
                    Logger.Debug(String.Format("Reset session to user {0}", user.UserId));
                    HttpContext.Current.Session["userId"] = user.UserId;
                }

                return user.PublicUrl;
            }

            return null;
        }

        [WebMethod(EnableSession = true)]
        public string EncryptedLogin(string userName, string saltedPassword)
        {
            UserPo user = null;
            var publicUrl = String.Empty;
            UserManage.EncryptedLogin(userName,saltedPassword,out user);
            if(user!=null)
            {
                if (saltedPassword == user.Password)
                {
                    if (HttpContext.Current.Session["userId"] == null)
                    {
                        Logger.Debug(String.Format("Added session for user {0}", user.UserId));
                        HttpContext.Current.Session.Add("userId", user.UserId);
                    }
                    else
                    {
                        Logger.Debug(String.Format("Reset session to user {0}", user.UserId));
                        HttpContext.Current.Session["userId"] = user.UserId;
                    }

                    publicUrl = user.PublicUrl;
                }
            }
            
            return publicUrl;
        }

        /// <summary>
        /// This method will re-send activation link to user
        /// </summary>
        /// <param name="email">User's email address</param>
        /// <returns>Status message</returns>
        [WebMethod(EnableSession = true)]
        public string ResendActivationLink(string email)
        {
            string templateUri = Server.MapPath("~/Emails/ConfirmationEmail.html");

            return EmailManage.ResendActivationLink(email, templateUri);
        }

        [WebMethod(EnableSession = true)]
        public void ChangeUserPassword(string password)
        {
            if (HttpContext.Current.Session["userId"] != null)
            {
                int userId = int.Parse(HttpContext.Current.Session["userId"].ToString());
                UserManage.ChangePassword(userId, password, DateTime.Now);
            }
        }

        /// <summary>
        /// This method should be used to sign-out user from
        /// his current strimm session
        /// </summary>
        [WebMethod(EnableSession = true)]
        public void SignOut()
        {
            //HttpContext.Current.Session.RemoveAll();
            //HttpContext.Current.Session.Clear();
            //HttpContext.Current.Session.Abandon();

            //HttpContext.Current.Response.Cookies["ASP.NET_SessionId"].Value = string.Empty;
            //HttpContext.Current.Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddMonths(-10);
            //HttpContext.Current.Response.Cookies.Remove("ASP.NET_SessionId");

            FormsAuthentication.SignOut();

            HttpContext.Current.Session.RemoveAll();
            HttpContext.Current.Session.Clear();
            HttpContext.Current.Session.Abandon();

            // clear authentication cookie
            HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            cookie1.Expires = DateTime.Now.AddYears(-1);
            HttpContext.Current.Response.Cookies.Add(cookie1);

            // clear session cookie (not necessary for your current problem but i would recommend you do it anyway)
            SessionStateSection sessionStateSection = (SessionStateSection)WebConfigurationManager.GetSection("system.web/sessionState");
            HttpCookie cookie2 = new HttpCookie(sessionStateSection.CookieName, "");
            cookie2.Expires = DateTime.Now.AddYears(-1);
            HttpContext.Current.Response.Cookies.Add(cookie2);
        }

        /// <summary>
        /// This method checks if a specified username is unique
        /// </summary>
        /// <param name="userName">Username to check</param>
        /// <returns>True/False</returns>
        [WebMethod]
        public bool IsUserNameUnique(string userName)
        {            
            return UserManage.IsUserNameUnique(userName);
        }

        [WebMethod]
        public bool  IsUserDeleted (string email)
        {

            UserPo user = UserManage.GetDeletedUser(email);
            if(user!=null)
            {
                return user.IsDeleted;
            }
            else
            {
                return false;
            }
            
        }
        [WebMethod]
        public bool IsTemporaryUser(string email)
        {

            UserPo user = UserManage.GetUserByEmail(email);
            if (user != null)
            {
                return user.IsTempUser;
            }
            else
            {
                return false;
            }

        }


        /// <summary>
        /// This method will check if specified user's password
        /// is strong
        /// </summary>
        /// <param name="password">User's password</param>
        /// <returns>True/False</returns>
        [WebMethod]
        public bool IsPasswordStrong(string password)
        {
            return UserManage.IsStrongPassword(password);
        }

        /// <summary>
        /// This method will check if specified user's email address
        /// is valid or not
        /// </summary>
        /// <param name="email">Email address</param>
        /// <returns>True/False</returns>
        [WebMethod]
        public bool IsEmailValid(string email)
        {
            return EmailUtils.isEmailValid(email);
        }

        /// <summary>
        /// This method will check if specified username corresponds 
        /// to a locked user record
        /// </summary>
        /// <param name="userName">Username</param>
        /// <returns>True/False</returns>
        [WebMethod]
        public bool CheckeIfLocked(string userName)
        {
            var user = UserManage.GetUserByLoginIdentifier(userName);

            return (user != null) ? user.IsLockedOut : false;
        }

        [WebMethod(EnableSession = true)]
        public string SendFeedback(string pageName, string selectedOption, string comments)
        {
            int userId=0;
            if(HttpContext.Current.Session["userId"]!=null)
            {
                userId = int.Parse(HttpContext.Current.Session["userId"].ToString());
            }
            
            Uri templateUri = new Uri(Server.MapPath("~/Emails/Feedback.html"));
            string messageReturn = EmailManage.SendFeedback(userId, pageName, selectedOption, comments, templateUri);

            return messageReturn;
        }

        [WebMethod(EnableSession = true)]
        public string FollowUser(string userUrl, int offset)
        {
            // MAX-TODO: Need to re-write this because we should not be generating HTML from
            // services.

            Page page = new Page();
            StringBuilder stringBuilder = new StringBuilder();
            HtmlTextWriter htmlTextWriter = new HtmlTextWriter(new StringWriter(stringBuilder));

            UserPo user = UserManage.GetUserByPublicUrl(userUrl);
            int followUserId = user.UserId;
            string path = "~/UC/FollowerUC.ascx";

            if (HttpContext.Current.Session["userId"] != null)
            {
                int userId = int.Parse(HttpContext.Current.Session["userId"].ToString());

                UserManage.FollowUser(userId, followUserId, offset);
                UserPo userFollower = UserManage.GetUserPoByUserId(userId);

                FollowerUC followCtrl = (FollowerUC)page.LoadControl(path);
                followCtrl.followerBoardUrl = userFollower.UserName;
                followCtrl.followerTitle = userFollower.UserName;
                followCtrl.followerId = userId.ToString();

                if (!String.IsNullOrEmpty(userFollower.ProfileImageUrl))
                {
                    followCtrl.imSrc = userFollower.ProfileImageUrl;
                }
                else
                {
                    followCtrl.imSrc = (userFollower.Gender == "Male")
                                            ? "/images/imgUserAvatarMale.jpg"
                                            : "/images/imgUserAvatarFemale.jpg";
                }

                followCtrl.RenderControl(htmlTextWriter);
            }

            return stringBuilder.ToString();
        }

        [WebMethod(EnableSession = true)]
        public int UnFollowUser(string userUrl)
        {
            UserPo user = UserManage.GetUserByPublicUrl(PublicNameUtils.GetUrl(userUrl));
            int followUserId = user.UserId;
            int id = 0;

            if (HttpContext.Current.Session["userId"] != null)
            {
                int userId = int.Parse(HttpContext.Current.Session["userId"].ToString());
                UserManage.UnFollowUser(followUserId, userId);
                id = userId;
            }

            if (HttpContext.Current.Session["followersList"] != null)
            {
                HttpContext.Current.Session["followersList"] = null;
            }

            return id;
        }

        // -----------------------------------------------------------------------------------------------
        // TODO: This method needs to be either eliminated or changed. Bad security, can't send 
        // password to users in clear text
        //------------------------------------------------------------------------------------------------
        [WebMethod]
        public string ForgotPass(string userName)
        {
            string message = String.Empty;
            UserPo user = UserManage.GetUserByLoginIdentifier(userName);
            string emailLink = String.Empty;
            string mailFrom = String.Empty;

            if ((user != null) && (user.UserId != 0))
            {
                //string pass         = MiscUtils.DecodeFrom64(user.Password);
                //string subject      = "Password recovery from Strimm.com";
                //string emailFrom    = ConfigurationManager.AppSettings["mailFrom"];
                //string mailTo       = user.Email;
                //string body         = String.Format("Dear {0},\n\nYou have requested a password reminder for Strimm.com login.\n\n" +
                //                            "Your password is the following: {0}\n\nSincerely,\n\nStrimm.com Support Team\nwww.strimm.com", user.FirstName, pass);
                string domainName = ConfigurationManager.AppSettings["domainName"] ?? "www.strimm.com";

                emailLink = String.Format("https://{0}/password-recovery?token={1}", domainName, MiscUtils.EncodeStringToBase64(userName)); //MiscUtils.EncodeStringToBase64(String.Format("{0}:{1}", email, password)));
                mailFrom = ConfigurationManager.AppSettings["mailFrom"].ToString();
                var emailTempalteUri = new Uri(Server.MapPath("~/Emails/PasswordRecovery.html"));
                if (EmailUtils.SendPasswordRecovery(user.Email, user.FirstName, user.AccountNumber, emailLink, "support@strimm.com", emailTempalteUri, domainName))
                {
                    message = "We have emailed you a link to reset your password.";
                }
                else
                {
                    message = "Please check if you specified a valid email.";
                }
            }
            else
            {
                message = "Specified e-mail does not match any valid user account. Please try again.";
            }

            return message;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetCountries()
        {
            string output = String.Empty;
            int userId = int.Parse(HttpContext.Current.Session["userId"].ToString());
            int stateId = 0;
            int countryId = 0;
            UserPo userPo = null;

            if (HttpContext.Current.Session["userId"] != null)
            {
                userId = int.Parse(HttpContext.Current.Session["userId"].ToString());

                userPo = UserManage.GetUserPoByUserId(userId);
                if (userPo == null)
                {
                    Logger.Error(String.Format("Failed to retrieve user using Id={0}", userId));
                    return null;
                }

                stateId = userPo.StateId;
                countryId = userPo.CountryId;
            }

            CountryInfo countryInfo = new CountryInfo();
            JavaScriptSerializer js = new JavaScriptSerializer();
            List<Country> countries = ReferenceDataManage.GetCountries();

            countries.ForEach(c =>
            {
                countryInfo.text = c.Name;
                countryInfo.value = c.CountryId;
                countryInfo.selected = countryId == c.CountryId;

                if (countryId == 0)
                {
                    countryInfo.selectedIndex = 1;
                }

                output += js.Serialize(countryInfo) + ",";
            });

            output = output.TrimEnd(',');
            output = String.Format("[{0}]", output);

            return output;
        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetStates(string country)
        {
            int userId = 0;
            int stateId = 0;
            int countryId = 0;
            UserPo userPo = null;

            if (HttpContext.Current.Session["userId"] != null)
            {
                userId = int.Parse(HttpContext.Current.Session["userId"].ToString());

                userPo = UserManage.GetUserPoByUserId(userId);

                if (userPo == null)
                {
                    Logger.Error(String.Format("Failed to retrieve user using Id={0}", userId));
                    return null;
                }

                stateId = userPo.StateId;
                countryId = userPo.CountryId;
            }

            string output = String.Empty;
            List<State> stateList = ReferenceDataManage.GetStates();
            CountryInfo countryInfo = new CountryInfo();
            JavaScriptSerializer js = new JavaScriptSerializer();
            List<State> usStates = new List<State>();
            List<State> canadaStates = new List<State>();

            usStates = stateList.Where(x => x.CountryId == 223).ToList();
            canadaStates = stateList.Where(x => x.CountryId == 38).ToList();


            //canada states
            if (country == "USA" || country == "United States Of America")
            {
                usStates.ForEach(s =>
                {
                    countryInfo.text = s.Name;
                    countryInfo.value = s.StateId;
                    countryInfo.selected = stateId == s.StateId;

                    if (stateId == 0)
                    {
                        countryInfo.selectedIndex = 1;
                    }

                    output += js.Serialize(countryInfo) + ",";
                });

                output = output.TrimEnd(',');
                output = "[" + output + "]";
            }
            else if (country == "Canada")
            {
                canadaStates.ForEach(s =>
                    {
                        countryInfo.text = s.Name;
                        countryInfo.value = s.StateId;
                        countryInfo.selected = stateId == s.StateId;

                        if (stateId == 0)
                        {
                            countryInfo.selectedIndex = 1;
                        }

                        output += js.Serialize(countryInfo) + ",";
                    });

                output = output.TrimEnd(',');
                output = String.Format("[{0}]", output);
            }

            return output;
        }

        /// <summary>
        /// This method will search for channels using keywords in their name and description
        /// </summary>
        /// <param name="keywords">Space separated list of keywords</param>
        /// <returns>List of ChannelTubeModels</returns>
        [WebMethod]
        public List<UserModel> FindUsersByKeywords(string keywords)
        {
            var keywordsList = keywords.Split(' ').ToList();
            return SearchManage.GetUsersByKeywords(keywordsList);
        }

        [WebMethod]
        public string UpdateUserPassword(int userId, string clientDateTime, string newPassword, string oldPassword, string currPass, string email)
        {
            DateTime clientTime = DateTimeUtils.GetClientTime(clientDateTime) ?? DateTime.Now;
            bool isUpdated = false;

            var hashedPass = CryptoUtils.HashPassword(CryptoUtils.GetPasswordWithSalt(email, oldPassword));
            var user = UserManage.GetUserPoByUserId(userId);

            if (hashedPass == user.Password)
            {
                isUpdated = UserManage.UpdateUserPassword(userId, clientTime, newPassword);
                return "Your password has been changed!";
            }
            else
            {
                return "Specified password does is invalid. Please try again!";
            }


        }

        internal class data
        {
            public string firstName, lastName, gender, publicName, email, company, country, state, address, city, zipCode, dateofsignup, age, numbOfChannels, accountStatus, userId, signUpGeoLocation, lastGeoLocation;
            public List<ChannelTubePo> channelTubeList;
            public bool isfilmmaker;


        }

        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public string GetAllUsers()
        {
            string output = "";
            List<UserPo> userpoList = UserManage.GetAllUserPosForAdmin();
            data dt = new data();
            JavaScriptSerializer js = new JavaScriptSerializer();
            if (userpoList.Count != 0)
            {
                foreach (var up in userpoList)
                {
                    List<ChannelTubePo>channelList = ChannelManage.GetChannelTubesForUser(up.UserId);
                    dt.userId = up.UserId.ToString();
                    dt.firstName = up.FirstName;
                    dt.lastName = up.LastName;
                    dt.gender = up.Gender;
                    dt.age = (DateTime.Now.Year - up.BirthDate.Year).ToString();
                    dt.publicName = up.UserName;
                    dt.email = up.Email;
                    dt.dateofsignup = up.UserMembershipCreatedDate.ToShortDateString();
                    dt.company = up.Company;
                    dt.country = up.Country;
                    dt.state = up.StateOrProvince;
                    dt.address = up.Address;
                    dt.city = up.City;
                    dt.zipCode = up.ZipCode;
                    dt.isfilmmaker = up.IsFilmMaker;

                    string signupLocationCountry="none";
                    string signupLocationCity = "none";
                    string signupLocationStateOrProvince="none";
                    string signupLocationZipCode = "none";
                    string lastLocationCountry="none";
                    string lastLocationCity = "none";
                    string lastLocationStateOrProvince = "none";
                    string lastLocationZipCode = "none";

                    if(!String.IsNullOrEmpty(up.SignupLocationCountry))
                    {
                        signupLocationCountry = up.SignupLocationCountry;

                    }
                    if (!String.IsNullOrEmpty(up.SignupLocationCity))
                    {
                        signupLocationCity = up.SignupLocationCity;

                    }
                    if (!String.IsNullOrEmpty(up.SignupLocationStateOrProvince))
                    {
                        signupLocationStateOrProvince = up.SignupLocationStateOrProvince;

                    }
                    if (!String.IsNullOrEmpty(up.SignupLocationZipCode))
                    {
                        signupLocationZipCode = up.SignupLocationZipCode;

                    }
                    if (!String.IsNullOrEmpty(up.LastLocationCountry))
                    {
                        lastLocationCountry = up.LastLocationCountry;

                    }
                    if (!String.IsNullOrEmpty(up.LastLocationCity))
                    {
                        lastLocationCity = up.LastLocationCity;

                    }
                    if (!String.IsNullOrEmpty(up.LastLocationStateOrProvince))
                    {
                        lastLocationStateOrProvince = up.LastLocationStateOrProvince;

                    }
                    if (!String.IsNullOrEmpty(up.LastLocationZipCode))
                    {
                        lastLocationZipCode = up.LastLocationZipCode;

                    }
                    dt.signUpGeoLocation = "country: " + signupLocationCountry + "<br>" +
                                           "city: " + signupLocationCity + "<br>" +
                                           "state/province: " + signupLocationStateOrProvince + "<br>" +
                                           "zipcode: " +signupLocationZipCode;

                    dt.lastGeoLocation = "country: " + lastLocationCountry + "<br>" +
                                           "city: " + lastLocationCity + "<br>" +
                                           "state/province: " + lastLocationStateOrProvince + "<br>" +
                                           "zipcode: " + lastLocationZipCode;
                    if(up.IsDeleted)
                    {
                        dt.accountStatus = "deleted";
                    }
                    else if(up.IsLockedOut)
                    {
                        dt.accountStatus = "locked out";
                    }
                    else
                    {
                        dt.accountStatus = "active";
                    }
                    dt.numbOfChannels = channelList.Count(x => x.IsDeleted == false).ToString();
                    dt.channelTubeList = channelList.ToList();
                    output += js.Serialize(dt) + ",";

                }

            }
            output = output.TrimEnd(',');
            output = "[" + output + "]";
            return output;
        }

        [WebMethod]
        public bool ResetPassword(string clientDateTime, string newPassword, string etoken)
        {
            DateTime clientTime = DateTimeUtils.GetClientTime(clientDateTime) ?? DateTime.Now;

            bool isUpdated = false;

            if (!String.IsNullOrEmpty(etoken))
            {
                string[] parts = etoken.Split(',');

                var user = UserManage.GetUserByEmail(CryptoUtils.Decrypt(parts[0]));
                if (user != null)
                {
                    isUpdated = UserManage.UpdateUserPassword(user.UserId, clientTime, newPassword);


                }
            }
 
            return isUpdated;

        }

        [WebMethod]
        public UserPo GetUserByEmail(string email)
        {
            return UserManage.GetUserByEmail(email);
        }

        [WebMethod(EnableSession = true)]
        public int InsertNewUserFromFacebookLogin(string userName, string userIp, string email, string firstName, string lastName, string countryName, string gender, string facebookId, string city, string state, string zipcode)
        {         
            string password = AccountUtils.GenerateAccountNumber();
            var cryptedPass = CryptoUtils.HashPassword(CryptoUtils.GetPasswordWithSalt(email, password));
            var facebookPictureUrl = "https://graph.facebook.com/" + facebookId + "/picture?type=normal";

            countryName = countryName == null || countryName == "undefined" ? null : countryName;
            city = city == null || city == "undefined" ? null : city;
            state = state == null || state == "undefined" ? null : state;
            zipcode = zipcode == null || zipcode == "undefined" ? null : zipcode;

            bool isUserInsert = UserManage.InsertUserByFacebookLogin(userName, userIp, cryptedPass, email, firstName, lastName, DateTime.Now.AddYears(-21), countryName, gender, facebookPictureUrl, city, state, zipcode);
            int userId = 0;
            if (isUserInsert)
            {
                var user = UserManage.GetUserByEmail(email);

                if (user != null)
                {
                    userId = user.UserId;
                    Session["UserId"] = userId;
                }
            }
            return userId;
        }

        [WebMethod(EnableSession = true)]
        public void SetUserIdInSession(string userId)
        {
            Session["UserId"] = userId;
        }

        [WebMethod]
        public string UpladImageToAmazon(string image)
        {
            byte[] bytes = Convert.FromBase64String(image);
            var bucketName = ConfigurationManager.AppSettings["ImagesBucket"];
            var amazonS3WsDomain = ConfigurationManager.AppSettings["AmazonS3WsDomain"];
            int userId = 1016;
            int channelTubeId = 1033;
            string fileName = "valula Images1.jpg";
            string s3fileName = String.Format("{0}/channel/{1}/{2}", userId, channelTubeId, fileName.Replace(' ', '_'));
            using (MemoryStream streamAvatr = new MemoryStream(bytes))
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

                        if (elapsedTimeInSec < 10)
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
                return String.Format("{0}/{1}/{2}", amazonS3WsDomain, bucketName, s3fileName);
            }
        }

        [WebMethod]
        public string UpdateUserAvatar(string fileName, string imageData, int userId)
        {
            UserPo userPo = UserManage.GetUserPoByUserId(userId);
          
            string userPoAvatarUrl = String.Empty;

            if (!String.IsNullOrEmpty(imageData))
            {
                userPo.ProfileImageUrl = AWSManage.UploadUserAvatarToS3(imageData, fileName, userId);
            }

            BoardManage.UpdateUserBoard(userPo.UserId, userPo.BoardName, userPo.ProfileImageUrl, userPo.BackgroundImageUrl, userPo.UserStory);

            return ImageUtils.GetProfileImageUrl(userPo.ProfileImageUrl);
        }

        [WebMethod]
        public string UpdateUserBackGround(string fileName, string imageData, int userId)
        {
            UserPo userPo = UserManage.GetUserPoByUserId(userId);

            string userPoAvatarUrl = String.Empty;

            if (!String.IsNullOrEmpty(imageData))
            {
                userPo.BackgroundImageUrl = AWSManage.UploadUserBackGroundToS3(imageData, fileName, userId);
            }

            BoardManage.UpdateUserBoard(userPo.UserId, userPo.BoardName, userPo.ProfileImageUrl, userPo.BackgroundImageUrl, userPo.UserStory);

            return ImageUtils.GetBackgroundImageUrl(userPo.BackgroundImageUrl);
        }

        [WebMethod]
        public bool IsCurrentPasswordMatch(int userId, string currentPassword)
        {
            var userPo = UserManage.GetUserPoByUserId(userId);
            var saltedPassword = CryptoUtils.HashPassword(CryptoUtils.GetPasswordWithSalt(userPo.Email, currentPassword));
            if(userPo.Password==saltedPassword)
            {
                return true;
            }
            else
            {
                return false;
            }


        }

        [WebMethod]
        public void UpdateUserLocation(UserLocation location, string username)
        {
            if (location != null)
            {
                UserManage.UpdateUserLastKnownLocationByUsername(location, username);
            }
        }

        [WebMethod]

        public string GetUserBio(int userId)
        {

            string msg = String.Empty;
            if(!String.IsNullOrEmpty(UserManage.GetUserPoByUserId(userId).UserStory))
            {
                msg = UserManage.GetUserPoByUserId(userId).UserStory;
            }


            return msg;
        }


        [WebMethod]
        public bool UpdateUserBio (int userId, string userStory)
        {

            UserPo user = UserManage.GetUserPoByUserId(userId);
           
            user.UserStory=userStory;

            return UserManage.UpdateUserProfile(user);
        }

        [WebMethod]
        public int GetNumberOfNotChannelVisitors()
        {
            List<Visitor> visitorList = VisitorManage.GetAllVisitors();
            List<Visitor> notChannelVisitorList = new List<Visitor>();
            foreach (var v in visitorList)
            {
                if(v.Destination!="Channel")
                {
                    notChannelVisitorList.Add(v);
                }
            }

            return notChannelVisitorList.Count();
        }

        [WebMethod]
        public List<UserEmailOpoutGroup> GetUserEmailPreferences(int userId)
        {
            return UserManage.GetUserEmailOptoutGroupsByUserId(userId);
        }

        [WebMethod]
        public ResponseMessageModel UpdateUserEmailPreferences(int userId, bool uGreetings, bool uReminders, bool uSocial, bool uNews, bool uMarketing)
        {
            ResponseMessageModel response = new ResponseMessageModel();

            try
            {
                if (userId > 0)
                {
                    response.IsSuccess = UserManage.UpdateUserEmailPreferences(userId, uGreetings, uReminders, uSocial, uNews, uMarketing);
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Unable to update email preferences. User does not exist.";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Unable to update email preferences. Try again later";
            }

            return response;
        }

        [WebMethod]
        public ResponseMessageModel AddBusinessContactRequest(BusinessContactRequestCriteria request)
        {
            ResponseMessageModel response = new ResponseMessageModel();

            try
            {
                if (request != null)
                {
                    response.IsSuccess = UserManage.AddBusinessContactRequest(request, new Uri(Server.MapPath("~/Emails/BusinessContactRequestEmail.html")));
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Unable to add business contact request. Invalid contact data specified";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Unable to add business contact request. Try again later";
            }

            return response;
        }

        [WebMethod]
        public ResponseMessageModel UpdateUserInterests(int userId, string interests)
        {
            ResponseMessageModel response = new ResponseMessageModel();

            try
            {
                if (userId > 0)
                {
                    response.IsSuccess = UserManage.AddUserInterests(userId, interests);
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Unable to update user interests. User does not exist.";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Unable to update user interests. Try again later";
            }

            return response;
        }

        [WebMethod]
        public OrderConfirmation PlaceOrder(CreateOrderBindingModel model)
        {
            Logger.Info("Handling a new order request");

            if (model == null)
            {
                return (new OrderConfirmation() { IsSuccess = false, Message = "Invalid request received." });
            }

            if (model.UserId <= 0 || model.ProductId <= 0)
            {
                return (new OrderConfirmation() { IsSuccess = false, Message = "Invalid user or product details specified." });
            }

            Logger.Debug(String.Format("New order request was submitted for product with id={0} by user with id={1}", model.ProductId, model.UserId));

            OrderConfirmation confirmation = null;

            try
            {
                confirmation = OrderManager.PlaceOrder(model);

                Logger.Debug(String.Format("Order was successfully created. New order number is '{0}'", confirmation.OrderNumber));
            }
            catch (Exception ex)
            {
                Logger.Error("Error occured while creating new order", ex);

                confirmation = new OrderConfirmation()
                {
                    IsSuccess = false,
                    Message = "Error occured while creating new order. Please try again"
                };
            }

            return confirmation;
        }

        public OrderCancellation CancelOrder(string orderNumber)
        {
            Logger.Info(String.Format("Handling cancel order request for order number='{0}'", orderNumber));

            OrderCancellation confirmation = null;

            try
            {
                confirmation = OrderManager.CancelOrder(orderNumber);

                Logger.Debug(String.Format("Order '{0}' was successfully canceled.", confirmation.OrderNumber));
            }
            catch (Exception ex)
            {
                Logger.Error("Error occured while creating new order", ex);

                confirmation = new OrderCancellation()
                {
                    IsSuccess = false,
                    Message = "Error occured while canceling your order. Please contact customer support"
                };
            }

            return confirmation;
        }

        [WebMethod]
        public void IPN()
        {
            Logger.Debug("New IPN message received. Initializing processing...");

            try
            {
                Context.Request.InputStream.Seek(0, SeekOrigin.Begin);

                string payload = new StreamReader(Context.Request.InputStream).ReadToEnd();
                payload = WebUtility.UrlDecode(payload);

                Logger.Debug(String.Format("Extracted new IPN message: '{0}'", payload));

                var segments = payload.Split('&').Select(x => new KeyValuePair<string, string>(x.Split('=')[0], x.Split('=')[1])).ToList();
                segments.Insert(0, new KeyValuePair<string, string>("cmd", "_notify-validate"));

                bool useSandboxForIpnVerifications = Convert.ToBoolean(ConfigurationManager.AppSettings["UseSandboxForIPNVerifications"].ToString());

                Logger.Debug(String.Format("Is using sandbox environment: {0}", useSandboxForIpnVerifications));

                // if you want to use the PayPal sandbox change this from false to true
                //string response = OrderManager.GetPayPalResponse(useSandboxForIpnVerifications, segments);
                string response = OrderManager.GetPayPalResponse(useSandboxForIpnVerifications, String.Format("cmd=_notify-validate&{0}", payload));

                Logger.Debug(String.Format("Response received from PayPal verification request for IPN message: {0}", response));

                if (payload.StartsWith("MANUAL") || response == "VERIFIED")
                {
                    if (payload.StartsWith("MANUAL"))
                    {
                        payload = payload.TrimStart("MANUAL".ToCharArray());
                    }

                    //check the payment_status is Completed
                    //check that txn_id has not been previously processed
                    //check that receiver_email is your Primary PayPal email
                    //check that payment_amount/payment_currency are correct 
                    var ipnObj = new IpnMessage(payload);

                    var existingIpnMessage = OrderManager.SaveIpnMessage(ipnObj);

                    if (existingIpnMessage != null)
                    {
                        Logger.Debug(String.Format("Processing a PayPal ipn notification message from PayPal for SubscriberId='{0}'", existingIpnMessage.SubscriberId));

                        if (ipnObj.TxnType == "subscr_signup")
                        {
                            ProcessPayPalSubscriptionSignupMessage(existingIpnMessage);
                        }
                        else if (ipnObj.TxnType == "subscr_payment")
                        {
                            ProcessPayPalSubscriberPaymentMessage(existingIpnMessage);
                        }
                        else if (ipnObj.TxnType == "subscr_modify")
                        {
                            ProcessPayPalSubscriptionModifiedMessage(existingIpnMessage);
                        }
                        else if (ipnObj.TxnType == "subscr_failed")
                        {
                            ProcessPayPalPaymentFailedMessage(existingIpnMessage);
                        }
                        else if (ipnObj.TxnType == "subscr_eot")
                        {
                            ProcessPayPalSubscriptionExpiredMessage(existingIpnMessage);
                        }
                        else if (ipnObj.TxnType == "subscr_cancel")
                        {
                            ProcessPayPalSubscriptionCanceledMessage(existingIpnMessage);
                        }
                    }
                    else
                    {
                        Logger.Warn(String.Format("Failed to persist IPN message: '{0}'", ipnObj.RawMessage));
                    }
                }
                else
                {
                    Context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                    if (response == "VERIFIED")
                    {
                        Logger.Error(String.Format("Verification failed for IPN message: '{0}'", payload));
                    }
                    else
                    {
                        Logger.Error(String.Format("Failed to process non-PayPal IPN message: '{0}'", payload));
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Error occured while processing PayPal IPN message", ex));

                Context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
        }
        [WebMethod]
        public void CreateNManualSubscribtion(string payload)
        {
            Logger.Debug("New IPN message received. Initializing processing...");

            try
            {
               // Context.Request.InputStream.Seek(0, SeekOrigin.Begin);

               // string payload = new StreamReader(Context.Request.InputStream).ReadToEnd();
                payload = WebUtility.UrlDecode(payload);

                Logger.Debug(String.Format("Extracted new IPN message: '{0}'", payload));

                var segments = payload.Split('&').Select(x => new KeyValuePair<string, string>(x.Split('=')[0], x.Split('=')[1])).ToList();
                segments.Insert(0, new KeyValuePair<string, string>("cmd", "_notify-validate"));

                bool useSandboxForIpnVerifications = Convert.ToBoolean(ConfigurationManager.AppSettings["UseSandboxForIPNVerifications"].ToString());

                Logger.Debug(String.Format("Is using sandbox environment: {0}", useSandboxForIpnVerifications));

                // if you want to use the PayPal sandbox change this from false to true
                //string response = OrderManager.GetPayPalResponse(useSandboxForIpnVerifications, segments);
                string response = OrderManager.GetPayPalResponse(useSandboxForIpnVerifications, String.Format("cmd=_notify-validate&{0}", payload));

                Logger.Debug(String.Format("Response received from PayPal verification request for IPN message: {0}", response));

                if (payload.StartsWith("MANUAL") || response == "VERIFIED")
                {
                    if (payload.StartsWith("MANUAL"))
                    {
                        payload = payload.TrimStart("MANUAL".ToCharArray());
                    }

                    //check the payment_status is Completed
                    //check that txn_id has not been previously processed
                    //check that receiver_email is your Primary PayPal email
                    //check that payment_amount/payment_currency are correct 
                    var ipnObj = new IpnMessage(payload);

                    var existingIpnMessage = OrderManager.SaveIpnMessage(ipnObj);

                    if (existingIpnMessage != null)
                    {
                        Logger.Debug(String.Format("Processing a PayPal ipn notification message from PayPal for SubscriberId='{0}'", existingIpnMessage.SubscriberId));

                        if (ipnObj.TxnType == "subscr_signup")
                        {
                            ProcessPayPalSubscriptionSignupMessage(existingIpnMessage);
                        }
                        else if (ipnObj.TxnType == "subscr_payment")
                        {
                            ProcessManualPayPalSubscriberPaymentMessage(existingIpnMessage);
                        }
                        else if (ipnObj.TxnType == "subscr_modify")
                        {
                            ProcessPayPalSubscriptionModifiedMessage(existingIpnMessage);
                        }
                        else if (ipnObj.TxnType == "subscr_failed")
                        {
                            ProcessPayPalPaymentFailedMessage(existingIpnMessage);
                        }
                        else if (ipnObj.TxnType == "subscr_eot")
                        {
                            ProcessPayPalSubscriptionExpiredMessage(existingIpnMessage);
                        }
                        else if (ipnObj.TxnType == "subscr_cancel")
                        {
                            ProcessPayPalSubscriptionCanceledMessage(existingIpnMessage);
                        }
                    }
                    else
                    {
                        Logger.Warn(String.Format("Failed to persist IPN message: '{0}'", ipnObj.RawMessage));
                    }
                }
                else
                {
                    Context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

                    if (response == "VERIFIED")
                    {
                        Logger.Error(String.Format("Verification failed for IPN message: '{0}'", payload));
                    }
                    else
                    {
                        Logger.Error(String.Format("Failed to process non-PayPal IPN message: '{0}'", payload));
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Error occured while processing PayPal IPN message", ex));

                Context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
        }

        [WebMethod]
        public ResponseMessageModel GetUserChannelEntitlements(int userId)
        {
            Logger.Info(String.Format("Handling request for user channel entitlements for user with id={0}", userId));

            ResponseMessageModel response = new ResponseMessageModel();

            try
            {
                if (userId > 0)
                {
                    response.Response = OrderManager.GetChannelUserEntitlementsById(userId);
                    response.IsSuccess = true;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Unable to retrieve user entitlements. Invalid user information specified on request";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Error occured while retrieving user channel entitlements";
            }

            return response;
        }

        [WebMethod]
        public ResponseMessageModel GetUserProductSubscriptions(int userId)
        {
            Logger.Info(String.Format("Handling request for user product subscriptions for user with id={0}", userId));

            ResponseMessageModel response = new ResponseMessageModel();

            try
            {
                if (userId > 0)
                {
                    response.Response = OrderManager.GetUserProductSubscriptionsByUserId(userId);
                    response.IsSuccess = true;
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Unable to retrieve user product subscriptions. Invalid user information specified on request";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Error occured while retrieving user product subscriptions";
            }

            return response;
        }

        private void ProcessPayPalSubscriptionSignupMessage(IpnMessage ipnObj)
        {
            Logger.Info(String.Format("Handling new paypal subscriber signup message with ipn: '{0}'", ipnObj.RawMessage));

            string facilitator = ConfigurationManager.AppSettings["PaymentReceiverEmail"].ToString();

            if (ipnObj != null && ipnObj.TxnType == "subscr_signup")
            {
                var orderConfirmationTemplate = new Uri(Server.MapPath("~/Emails/ThankYouForYourSubscriptionEmail.html"));

                if (ipnObj.ReceiverEmail != facilitator)
                {
                    Logger.Warn(String.Format("Ignoring ipn message received for a wrong receiver e-mail. Expected '{0}' and got '{1}'", facilitator, ipnObj.ReceiverEmail));
                    return;
                }

                string orderNumber = ipnObj.OptionSelection1; // Used with subscription with selection options ipnObj.OptionSelection2;

                if (!String.IsNullOrEmpty(orderNumber))
                {
                    var existingOrder = OrderManager.GetOrderByOrderNumber(orderNumber);

                    var possibleTxnAmounts = OrderManager.GetPossiblePaymentAmounts(orderNumber);

                    var priorTxn = OrderManager.GetPriorTransactionBySubscriberId(ipnObj.SubscriberId);

                    if (priorTxn.Count > 0)
                    {
                        Logger.Warn(String.Format("Ignoring duplicate ipn message. There are '{0}' order transactions in the db associated to this message already", priorTxn.Count));
                        return;
                    }

                    var user = UserManage.GetUserPoByUserId(existingOrder.UserId);

                    var product = ProductManager.GetProductById(existingOrder.ProductId);

                    if (possibleTxnAmounts.Any(x => x == ipnObj.Amount3 || x % product.Price == 0))
                    {
                        bool sendEmail = true;

                        Logger.Debug(String.Format("Subscription was successfully created for Order='{0}', by subscriber with id={1} and e-mail='{2}', in the amount of ${3}", existingOrder.OrderNumber, ipnObj.SubscriberId, ipnObj.PayerEmail, ipnObj.Amount1));

                        var subscriber = OrderManager.GetPayingSubscriberByOrderId(existingOrder.OrderId);
                        if (subscriber == null)
                        {
                            subscriber = OrderManager.AssociatePayingSubscriberWithOrder(existingOrder.OrderId, ipnObj);

                            Logger.Debug(String.Format("New paying subscriber with id={0} was associated with order '{1}'", subscriber.PayingSubscriberId, existingOrder.OrderId));
                        }

                        var orderProfile = OrderManager.GetOrderSubscriptionProfileByOrderId(existingOrder.OrderId);
                        if (orderProfile == null)
                        {
                            orderProfile = OrderManager.AssociateSubscriptionProfileWithOrder(existingOrder.OrderId, subscriber.PayingSubscriberId, ipnObj);

                            Logger.Debug(String.Format("Subscription profile with id={0} was successfully associated with order with id={1}", orderProfile.OrderSubscriptionProfileId, existingOrder.OrderId));
                        }
                        else
                        {
                            orderProfile = OrderManager.UpdateBillingDetailsOnOrderSubscriptionProfileById(orderProfile.OrderSubscriptionProfileId, ipnObj.Amount1, ipnObj.Amount3, ipnObj.Period1, ipnObj.Period3);

                            Logger.Debug(String.Format("Updated subscription profile with id={0} for order with id={1}", orderProfile.OrderSubscriptionProfileId, existingOrder.OrderId));
                        }

                        int channelCount = (int)(orderProfile.CycleBillingAmount / possibleTxnAmounts.FirstOrDefault());

                        //if (!product.IsPricePerChannel)
                        //{
                        channelCount = product.MaxChannelCount;
                        //}

                        Logger.Debug(String.Format("Subscriber with id={0} purchased channel subscription for {1} channels with order={2}", subscriber.PayingSubscriberId, channelCount, existingOrder.OrderId));

                        int priorTransactionCount = OrderManager.GetPriorOrderCountByUserId(user.UserId);

                        existingOrder.OrderStatusId = priorTransactionCount > 0 
                                            ? (int)OrderStatusEnum.Active : (int)OrderStatusEnum.InTrial;
                        existingOrder.ChannelCount = channelCount;

                        existingOrder = OrderManager.UpdateOrderByOrderId(existingOrder);

                        Logger.Debug(String.Format("Updated order status to '{0}'", existingOrder.OrderStatusId));

                        var productSubscription = OrderManager.GetProductSubscriptionByOrderId(existingOrder.OrderId);

                        if (productSubscription == null)
                        {
                            productSubscription = OrderManager.CreateProductSubscription(existingOrder.OrderId, existingOrder.ProductId, ProductSubscriptionStatusEnum.InTrial);

                            Logger.Debug(String.Format("Added new product subscription record with id={0} to track order with id={1}", productSubscription.ProductSubscriptionId, existingOrder.OrderId));
                        }
                        else
                        {
                            sendEmail = false;
                        }

                        if (product.ProductId == (int)ProductEnum.ProfessionalSubscription)
                        {
                            user.MatureContentAllowed = true;
                            user.PrivateVideoModeEnabled = true;

                            Logger.Debug(String.Format("Enabling private video mode and mature content for user with id={0}", user.UserId));

                            UserManage.UpdateUser(user);
                        }

                        if (sendEmail)
                        {
                            bool emailSend = EmailManage.SendOrderConfirmationEmail(user, existingOrder, orderConfirmationTemplate, product);

                            Logger.Debug(String.Format("Subscription confirmation e-mail was send to '{0}' for order '{1}'", user.Email, existingOrder.OrderNumber));
                        }
                    }
                    else
                    {
                        Logger.Warn(String.Format("Received ipn message is not associated with any valid transaction amounts. Transaction amount received is ${0}", ipnObj.McAmount3));
                    }
                }
                else
                {
                    Logger.Warn(String.Format("Invalid order number specified on the received IPN message: '{0}'", ipnObj.RawMessage));
                }
            }
        }
        private void ProcessManualPayPalSubscriberPaymentMessage(IpnMessage ipnObj)
        {
            string facilitator = ConfigurationManager.AppSettings["PaymentReceiverEmail"].ToString();

            if (ipnObj.TxnType == "subscr_payment")
            {
                if (ipnObj.ReceiverEmail != facilitator)
                {
                    Logger.Warn(String.Format("Ignoring ipn message received for a wrong receiver e-mail. Expected '{0}' and got '{1}'", facilitator, ipnObj.ReceiverEmail));
                    return;
                }

                string subscriberId = ipnObj.SubscriberId;

                if (!String.IsNullOrEmpty(subscriberId))
                {
                    var subscriber = OrderManager.GetPayingSubscriberByPayPalSubscriberId(subscriberId);

                    if (subscriber != null)
                    {
                        var existingOrder = OrderManager.GetOrderPoById(subscriber.OrderId);

                        if (existingOrder != null)
                        {
                            if (existingOrder.OrderStatusId == (int)OrderStatusEnum.PendingPaymentResolution)
                            {
                                existingOrder.OrderStatusId = (int)OrderStatusEnum.Active;
                                existingOrder.OrderExpirationDate = null;

                                existingOrder = OrderManager.UpdateOrderStatusById(existingOrder);

                                Logger.Debug(String.Format("Updated order status from 'Pending Payment Resolution' to 'Active' for order with id={0}", existingOrder.OrderId));
                            }

                            // Handle payment posted for a new subscription
                            Logger.Debug(String.Format("Payment received for subscription from Subscriber with Id={0}, Order Number='{1}' in the amount={2}", ipnObj.SubscriberId, existingOrder.OrderNumber, ipnObj.Amount1));

                            var orderTransaction = OrderManager.CreateOrderTransaction(existingOrder.OrderId, subscriber.PayingSubscriberId, ipnObj);

                            Logger.Debug(String.Format("Order trunsaction with id={0} and amount={1} was created for order number = '{2}'", orderTransaction.OrderTransactionId, orderTransaction.McGross, existingOrder.OrderNumber));

                            var productSubscription = OrderManager.UpdateProductSubscriptionStatus(existingOrder.OrderId, ProductSubscriptionStatusEnum.Active);

                            if (productSubscription != null)
                            {
                                Logger.Debug(String.Format("Updated product subscription record with id={0} to Active", productSubscription.ProductSubscriptionId));
                            }
                            else
                            {
                                Logger.Warn(String.Format("Failed to update product subscription record for order with id={0} to Active", existingOrder.OrderId));
                            }
                        }
                        else
                        {
                            Logger.Warn(String.Format("Failed to retrieve order details for order with id={0}", subscriber.OrderId));
                        }
                    }
                    else
                    {
                        string orderNumber = ipnObj.OptionSelection1; // Used with subscription with selection options ipnObj.OptionSelection2;

                        if (!String.IsNullOrEmpty(orderNumber))
                        {
                            CreateManualOrderDetailsUsingPayPalSubscriptionPaymentMessage(ipnObj);
                            ProcessPayPalSubscriberPaymentMessage(ipnObj);
                        }

                        Logger.Warn(String.Format("Unable to located paying subscriber record for PayPal subscriber id='{0}'", subscriberId));
                    }
                }
            }
        }

        private void ProcessPayPalSubscriberPaymentMessage(IpnMessage ipnObj)
        {
            string facilitator = ConfigurationManager.AppSettings["PaymentReceiverEmail"].ToString();

            if (ipnObj.TxnType == "subscr_payment")
            {
                if (ipnObj.ReceiverEmail != facilitator)
                {
                    Logger.Warn(String.Format("Ignoring ipn message received for a wrong receiver e-mail. Expected '{0}' and got '{1}'", facilitator, ipnObj.ReceiverEmail));
                    return;
                }

                string subscriberId = ipnObj.SubscriberId;

                if (!String.IsNullOrEmpty(subscriberId))
                {
                    var subscriber = OrderManager.GetPayingSubscriberByPayPalSubscriberId(subscriberId);

                    if (subscriber != null)
                    {
                        var existingOrder = OrderManager.GetOrderPoById(subscriber.OrderId);

                        if (existingOrder != null)
                        {
                            if (existingOrder.OrderStatusId == (int)OrderStatusEnum.PendingPaymentResolution)
                            {
                                existingOrder.OrderStatusId = (int)OrderStatusEnum.Active;
                                existingOrder.OrderExpirationDate = null;

                                existingOrder = OrderManager.UpdateOrderStatusById(existingOrder);

                                Logger.Debug(String.Format("Updated order status from 'Pending Payment Resolution' to 'Active' for order with id={0}", existingOrder.OrderId));
                            }

                            // Handle payment posted for a new subscription
                            Logger.Debug(String.Format("Payment received for subscription from Subscriber with Id={0}, Order Number='{1}' in the amount={2}", ipnObj.SubscriberId, existingOrder.OrderNumber, ipnObj.Amount1));

                            var orderTransaction = OrderManager.CreateOrderTransaction(existingOrder.OrderId, subscriber.PayingSubscriberId, ipnObj);

                            Logger.Debug(String.Format("Order trunsaction with id={0} and amount={1} was created for order number = '{2}'", orderTransaction.OrderTransactionId, orderTransaction.McGross, existingOrder.OrderNumber));

                            var productSubscription = OrderManager.UpdateProductSubscriptionStatus(existingOrder.OrderId, ProductSubscriptionStatusEnum.Active);

                            if (productSubscription != null)
                            {
                                Logger.Debug(String.Format("Updated product subscription record with id={0} to Active", productSubscription.ProductSubscriptionId));
                            }
                            else
                            {
                                Logger.Warn(String.Format("Failed to update product subscription record for order with id={0} to Active", existingOrder.OrderId));
                            }
                        }
                        else
                        {
                            Logger.Warn(String.Format("Failed to retrieve order details for order with id={0}", subscriber.OrderId));
                        }
                    }
                    else
                    {
                        string orderNumber = ipnObj.OptionSelection1; // Used with subscription with selection options ipnObj.OptionSelection2;

                        if (!String.IsNullOrEmpty(orderNumber))
                        {
                            CreateOrderDetailsUsingPayPalSubscriptionPaymentMessage(ipnObj);
                            ProcessPayPalSubscriberPaymentMessage(ipnObj);
                        }

                        Logger.Warn(String.Format("Unable to located paying subscriber record for PayPal subscriber id='{0}'", subscriberId));
                    }
                }
            }
        }

        private void CreateOrderDetailsUsingPayPalSubscriptionPaymentMessage(IpnMessage ipnObj)
        {
            Logger.Info(String.Format("Handling new paypal subscriber signup message with ipn: '{0}'", ipnObj.RawMessage));

            string facilitator = ConfigurationManager.AppSettings["PaymentReceiverEmail"].ToString();

            if (ipnObj != null && ipnObj.TxnType == "subscr_payment")
            {
                var orderConfirmationTemplate = new Uri(Server.MapPath("~/Emails/ThankYouForYourSubscriptionEmail.html"));

                if (ipnObj.ReceiverEmail != facilitator)
                {
                    Logger.Warn(String.Format("Ignoring ipn message received for a wrong receiver e-mail. Expected '{0}' and got '{1}'", facilitator, ipnObj.ReceiverEmail));
                    return;
                }

                string orderNumber = ipnObj.OptionSelection1;

                if (!String.IsNullOrEmpty(orderNumber))
                {
                    var existingOrder = OrderManager.GetOrderByOrderNumber(orderNumber);

                    var possibleTxnAmounts = OrderManager.GetPossiblePaymentAmounts(orderNumber);

                    var priorTxn = OrderManager.GetPriorTransactionBySubscriberId(ipnObj.SubscriberId);

                    if (priorTxn.Count > 0)
                    {
                        Logger.Warn(String.Format("Ignoring duplicate ipn message. There are '{0}' order transactions in the db associated to this message already", priorTxn.Count));
                        return;
                    }

                    var user = UserManage.GetUserPoByUserId(existingOrder.UserId);

                    var product = ProductManager.GetProductById(existingOrder.ProductId);

                    if (possibleTxnAmounts.Any(x => x == ipnObj.McGross || x % product.Price == 0))
                    {
                        Logger.Debug(String.Format("Subscription was successfully created for Order='{0}', by subscriber with id={1} and e-mail='{2}', in the amount of ${3}", existingOrder.OrderNumber, ipnObj.SubscriberId, ipnObj.PayerEmail, ipnObj.McGross));

                        var subscriber = OrderManager.AssociatePayingSubscriberWithOrder(existingOrder.OrderId, ipnObj);

                        Logger.Debug(String.Format("New paying subscriber with id={0} was associated with order '{1}'", subscriber.PayingSubscriberId, existingOrder.OrderId));

                        var profile = OrderManager.AssociateSubscriptionProfileWithOrder(existingOrder.OrderId, subscriber.PayingSubscriberId, ipnObj);

                        Logger.Debug(String.Format("Subscription profile with id={0} was successfully associated with order with id={1}", profile.OrderSubscriptionProfileId, existingOrder.OrderId));

                        int channelCount = (int)(ipnObj.McGross / possibleTxnAmounts.FirstOrDefault());

                        if (!product.IsPricePerChannel)
                        {
                            channelCount = product.MaxChannelCount;
                        }

                        Logger.Debug(String.Format("Subscriber with id={0} purchased channel subscription for {1} channels with order={2}", subscriber.PayingSubscriberId, channelCount, existingOrder.OrderId));

                        existingOrder.OrderStatusId = (int)OrderStatusEnum.InTrial;
                        existingOrder.ChannelCount = channelCount;

                        existingOrder = OrderManager.UpdateOrderByOrderId(existingOrder);

                        Logger.Debug(String.Format("Updated order status to '{0}'", existingOrder.OrderStatusId));

                        var productSubscription = OrderManager.GetProductSubscriptionByOrderId(existingOrder.OrderId);

                        if (productSubscription == null)
                        {
                            productSubscription = OrderManager.CreateProductSubscription(existingOrder.OrderId, existingOrder.ProductId, ProductSubscriptionStatusEnum.InTrial);

                            Logger.Debug(String.Format("Added new product subscription record with id={0} to track order with id={1}", productSubscription.ProductSubscriptionId, existingOrder.OrderId));
                        }

                        bool emailSend = EmailManage.SendOrderConfirmationEmail(user, existingOrder, orderConfirmationTemplate, product);

                        Logger.Debug(String.Format("Subscription confirmation e-mail was send to '{0}' for order '{1}'", user.Email, existingOrder.OrderNumber));
                    }
                    else
                    {
                        Logger.Warn(String.Format("Received ipn message is not associated with any valid transaction amounts. Transaction amount received is ${0}", ipnObj.McAmount3));
                    }
                }
                else
                {
                    Logger.Warn(String.Format("Invalid order number specified on the received IPN message: '{0}'", ipnObj.RawMessage));
                }
            }
        }
        private void CreateManualOrderDetailsUsingPayPalSubscriptionPaymentMessage(IpnMessage ipnObj)
        {
            Logger.Info(String.Format("Handling new paypal subscriber signup message with ipn: '{0}'", ipnObj.RawMessage));

            string facilitator = ConfigurationManager.AppSettings["PaymentReceiverEmail"].ToString();

            if (ipnObj != null && ipnObj.TxnType == "subscr_payment")
            {
                var orderConfirmationTemplate = new Uri(Server.MapPath("~/Emails/ThankYouForYourSubscriptionEmail.html"));

                if (ipnObj.ReceiverEmail != facilitator)
                {
                    Logger.Warn(String.Format("Ignoring ipn message received for a wrong receiver e-mail. Expected '{0}' and got '{1}'", facilitator, ipnObj.ReceiverEmail));
                    return;
                }

                string orderNumber = ipnObj.OptionSelection1;

                if (!String.IsNullOrEmpty(orderNumber))
                {
                    var existingOrder = OrderManager.GetOrderByOrderNumber(orderNumber);
                    existingOrder.Price = ipnObj.McGross;

                    var possibleTxnAmounts = OrderManager.GetPossiblePaymentAmounts(orderNumber);

                    var priorTxn = OrderManager.GetPriorTransactionBySubscriberId(ipnObj.SubscriberId);

                    if (priorTxn.Count > 0)
                    {
                        Logger.Warn(String.Format("Ignoring duplicate ipn message. There are '{0}' order transactions in the db associated to this message already", priorTxn.Count));
                        return;
                    }

                    var user = UserManage.GetUserPoByUserId(existingOrder.UserId);

                    var product = ProductManager.GetProductById(existingOrder.ProductId);

                    if (possibleTxnAmounts.Any(x => x == ipnObj.McGross || x % product.Price == 0))
                    {
                        Logger.Debug(String.Format("Subscription was successfully created for Order='{0}', by subscriber with id={1} and e-mail='{2}', in the amount of ${3}", existingOrder.OrderNumber, ipnObj.SubscriberId, ipnObj.PayerEmail, ipnObj.McGross));

                        var subscriber = OrderManager.AssociatePayingSubscriberWithOrder(existingOrder.OrderId, ipnObj);

                        Logger.Debug(String.Format("New paying subscriber with id={0} was associated with order '{1}'", subscriber.PayingSubscriberId, existingOrder.OrderId));

                        var profile = OrderManager.AssociateSubscriptionProfileWithOrder(existingOrder.OrderId, subscriber.PayingSubscriberId, ipnObj);

                        Logger.Debug(String.Format("Subscription profile with id={0} was successfully associated with order with id={1}", profile.OrderSubscriptionProfileId, existingOrder.OrderId));

                        int channelCount = (int)(ipnObj.McGross / possibleTxnAmounts.FirstOrDefault());

                        if (!product.IsPricePerChannel)
                        {
                            channelCount = product.MaxChannelCount;
                        }

                        Logger.Debug(String.Format("Subscriber with id={0} purchased channel subscription for {1} channels with order={2}", subscriber.PayingSubscriberId, channelCount, existingOrder.OrderId));

                        existingOrder.OrderStatusId = (int)OrderStatusEnum.Active;
                        existingOrder.ChannelCount = channelCount;

                        existingOrder = OrderManager.UpdateOrderByOrderId(existingOrder);

                        Logger.Debug(String.Format("Updated order status to '{0}'", existingOrder.OrderStatusId));

                        var productSubscription = OrderManager.GetProductSubscriptionByOrderId(existingOrder.OrderId);

                        if (productSubscription == null)
                        {
                            productSubscription = OrderManager.CreateProductSubscription(existingOrder.OrderId, existingOrder.ProductId, ProductSubscriptionStatusEnum.InTrial);

                            Logger.Debug(String.Format("Added new product subscription record with id={0} to track order with id={1}", productSubscription.ProductSubscriptionId, existingOrder.OrderId));
                        }

                       // bool emailSend = EmailManage.SendOrderConfirmationEmail(user, existingOrder, orderConfirmationTemplate, product);

                        Logger.Debug(String.Format("Subscription confirmation e-mail was send to '{0}' for order '{1}'", user.Email, existingOrder.OrderNumber));
                    }
                    else
                    {
                        Logger.Warn(String.Format("Received ipn message is not associated with any valid transaction amounts. Transaction amount received is ${0}", ipnObj.McAmount3));
                    }
                }
                else
                {
                    Logger.Warn(String.Format("Invalid order number specified on the received IPN message: '{0}'", ipnObj.RawMessage));
                }
            }
        }

        private void ProcessPayPalSubscriptionModifiedMessage(IpnMessage ipnObj)
        {
            if (ipnObj != null && ipnObj.TxnType == "subscr_modify")
            {
                // Handle change of the existing subscription
                // At this point we don't allow modifications
                Logger.Warn(String.Format("Ignoring subscription modified notification for Subscriber='{0}', ipn: '{1}'", ipnObj.SubscriberId, ipnObj.RawMessage));
            }
        }

        private void ProcessPayPalPaymentFailedMessage(IpnMessage ipnObj)
        {
            if (ipnObj != null && ipnObj.TxnType == "subscr_failed")
            {
                var paymentFailureTemplate = new Uri(Server.MapPath("~/Emails/PaymentFailureEmail.html"));

                string subscriberId = ipnObj.SubscriberId;

                if (!String.IsNullOrEmpty(subscriberId))
                {
                    var subscriber = OrderManager.GetPayingSubscriberByPayPalSubscriberId(subscriberId);

                    if (subscriber != null)
                    {
                        var existingOrder = OrderManager.GetOrderPoById(subscriber.OrderId);

                        if (existingOrder != null)
                        {
                            Logger.Debug(String.Format("Received a payment failure IPN mesasge for subscriber={0} and order '{1}'", ipnObj.SubscriberId, existingOrder.OrderNumber));

                            existingOrder.OrderStatusId = (int)OrderStatusEnum.PendingPaymentResolution;
                            existingOrder.OrderExpirationDate = DateTime.Now.AddDays(10);

                            Logger.Debug(String.Format("Updated order status to 'PendingPaymentResolution' for order with id={0} and set its cancellation date to '{1}'", existingOrder.OrderId, existingOrder.OrderExpirationDate.Value.ToString("MM/dd/YYYY")));

                            // Handle payment failure for the existing subscription
                            existingOrder = OrderManager.UpdateOrderByOrderId(existingOrder);

                            string orderExpirationDate = existingOrder.OrderExpirationDate != null ? existingOrder.OrderExpirationDate.Value.ToShortDateString() : String.Empty;

                            Logger.Debug(String.Format("Order status for order '{0}' was updated to PENDING. Subscription features will be removed from the account on {1}", existingOrder.OrderNumber, orderExpirationDate));

                            var productSubscription = OrderManager.UpdateProductSubscriptionStatus(existingOrder.OrderId, ProductSubscriptionStatusEnum.OnHold);

                            Logger.Debug(String.Format("Updated product subscription record with id={0} to Failed", productSubscription.ProductSubscriptionId));

                            var user = UserManage.GetUserPoByUserId(existingOrder.UserId);

                            bool emailSend = EmailManage.SendPaymentFailureEmail(user, existingOrder, paymentFailureTemplate);

                            Logger.Debug(String.Format("Payment failure e-mail was send to '{0}' for order '{1}'", user.Email, existingOrder.OrderNumber));
                        }
                        else
                        {
                            Logger.Warn(String.Format("Failed to retrieve order with id={0}", subscriber.OrderId));
                        }
                    }
                    else
                    {
                        Logger.Warn(String.Format("Failed to retrieve paying subscriber details using paypal subscriber id='{0}'", subscriberId));
                    }
                }
            }
        }

        private void ProcessPayPalSubscriptionExpiredMessage(IpnMessage ipnObj)
        {
            if (ipnObj != null && ipnObj.TxnType == "subscr_eot")
            {
                string subscriberId = ipnObj.SubscriberId;

                if (!String.IsNullOrEmpty(subscriberId))
                {
                    var subscriber = OrderManager.GetPayingSubscriberByPayPalSubscriberId(subscriberId);

                    if (subscriber != null)
                    {
                        var existingOrder = OrderManager.GetOrderPoById(subscriber.OrderId);

                        if (existingOrder != null)
                        {
                            // Handle expiration of the existing subscription
                            // We don't have expiration set and our subscriptions
                            // will automatically renew. As such, nothing to do here for now
                            var productSubscription = OrderManager.UpdateProductSubscriptionStatus(existingOrder.OrderId, ProductSubscriptionStatusEnum.Expired);

                            if (productSubscription != null)
                            {
                                Logger.Debug(String.Format("Updated product subscription record with id={0} to Expired", productSubscription.ProductSubscriptionId));
                            }
                            else
                            {
                                Logger.Debug(String.Format("Updated product subscription record with id={0} to Expired", productSubscription.ProductSubscriptionId));
                            }

                            existingOrder.OrderStatusId = (int)OrderStatusEnum.Expired;

                            existingOrder = OrderManager.UpdateOrderStatusById(existingOrder);

                            Logger.Debug(String.Format("Order status was updated to EXPIRED for order number '{0}' and subscriber '{1}'", existingOrder.OrderNumber, ipnObj.SubscriberId));

                            var user = UserManage.GetUserPoByUserId(existingOrder.UserId);

                            if (user != null)
                            {
                                // At this point we need to turn off user's features based on what was canceled
                                OrderManager.UpdateUserChannelsSettingsBasedOnCanceledProduct(user, existingOrder, productSubscription);
                            }
                            else
                            {
                                Logger.Warn(String.Format("Failed to retrieve existing user details using userId={0} specified on order with id={1}", existingOrder.UserId, existingOrder.OrderId));
                            }
                        }
                        else
                        {
                            Logger.Warn(String.Format("Failed to retrieve existing order with id={0}", subscriber.OrderId));
                        }
                    }
                    else
                    {
                        Logger.Warn(String.Format("Failed to retrieve paying subscriber details using paypal subscriber id='{0}'", subscriberId));
                    }
                }
                else
                {
                    Logger.Warn(String.Format("Failed to retrieve paypal subscriber id from IPN message: '{0}'", ipnObj.RawMessage));
                }
            }
        }

        private void ProcessPayPalSubscriptionCanceledMessage(IpnMessage ipnObj)
        {
            if (ipnObj != null && ipnObj.TxnType == "subscr_cancel")
            {
                var orderCancelTemplate = new Uri(Server.MapPath("~/Emails/SubscriptionCancelledEmail.html"));
                string emailOriginator = ConfigurationManager.AppSettings["SubscriptionsEmail"].ToString();

                string subscriberId = ipnObj.SubscriberId;

                if (!String.IsNullOrEmpty(subscriberId))
                {
                    var subscriber = OrderManager.GetPayingSubscriberByPayPalSubscriberId(subscriberId);

                    if (subscriber != null)
                    {
                        var existingOrder = OrderManager.GetOrderPoById(subscriber.OrderId);

                        if (existingOrder != null)
                        {
                            // Handle cancellation of the existing subscriptions
                            // Need to update the status set on the original order
                            // and e-mail user explaining to him that his subcription
                            // benefits will be taken away
                            existingOrder.OrderStatusId = (int)OrderStatusEnum.Canceled;

                            var productSubscription = OrderManager.UpdateProductSubscriptionStatus(existingOrder.OrderId, ProductSubscriptionStatusEnum.Canceled);

                            Logger.Debug(String.Format("Updated product subscription record with id={0} to Canceled", productSubscription.ProductSubscriptionId));

                            existingOrder = OrderManager.UpdateOrderStatusById(existingOrder);

                            Logger.Debug(String.Format("Order status was updated to CANCELED for order number '{0}' and subscriber '{1}'", existingOrder.OrderNumber, ipnObj.SubscriberId));

                            var user = UserManage.GetUserPoByUserId(existingOrder.UserId);

                            if (user != null)
                            {
                                bool emailSend = EmailManage.SendOrderCancellationEmail(user, existingOrder, orderCancelTemplate);

                                Logger.Debug(String.Format("Order canceled e-mail was send to '{0}' for order '{1}'", user.Email, existingOrder.OrderNumber));

                                // At this point we need to turn off user's features based on what was canceled
                                OrderManager.UpdateUserChannelsSettingsBasedOnCanceledProduct(user, existingOrder, productSubscription);
                            }
                            else
                            {
                                Logger.Warn(String.Format("Failed to retrieve existing user details using userId={0} specified on order with id={1}", existingOrder.UserId, existingOrder.OrderId));
                            }
                        }
                        else
                        {
                            Logger.Warn(String.Format("Failed to retrieve existing order with id={0}", subscriber.OrderId));
                        }
                    }
                    else
                    {
                        Logger.Warn(String.Format("Failed to retrieve paying subscriber details using paypal subscriber id='{0}'", subscriberId));
                    }
                }
                else
                {
                    Logger.Warn(String.Format("Failed to retrieve paypal subscriber id from IPN message: '{0}'", ipnObj.RawMessage));
                }
            }
        }

        [WebMethod]
        public bool EnableMatureContentDateStamp(string clientTime, int userId)
        {
            return true;
        }
    }
}
