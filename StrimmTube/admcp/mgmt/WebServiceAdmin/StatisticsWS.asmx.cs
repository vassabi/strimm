
using Strimm.Shared;
using StrimmBL;
using Strimm.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using Strimm.Model.Projections;

namespace StrimmTube.admcp.mgmt.WebServiceAdmin
{
    /// <summary>
    /// Summary description for StatisticsWS
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class StatisticsWS : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        internal class AllUsers
        {
            public string UserFullName, age, gender, accountNum, userName, dateOdSignup, country, state, city, zip, channels, board;
        }
        internal class RootObject
        {
            public List<AllUsers> UserItems;
        }
        [WebMethod(EnableSession = true)]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetUsers()
        {


            List<UserPo> userList = new List<UserPo>();
            userList = UserManage.GetAllUserPos();
            JavaScriptSerializer js = new JavaScriptSerializer();
            var allUsersList = new RootObject();
            List<AllUsers> allUsersListArray = new List<AllUsers>();
            foreach (var c in userList)
            {
                AllUsers allUsers = new AllUsers();
                allUsers.UserFullName = c.FirstName + " " + c.LastName;
                allUsers.age = DateTimeUtils.GetAge(c.BirthDate).ToString();
                allUsers.gender = c.Gender;
                allUsers.accountNum = c.AccountNumber;
                allUsers.userName = c.UserName;
                allUsers.dateOdSignup = c.UserProfileCreatedDate.ToShortDateString();
                allUsers.country = c.Country;
                allUsers.state = c.Address;
                //allUsers.city = c.city;
                //allUsers.zip = c.zipCode;
                List<ChannelTubePo> channelTubelist = ChannelManage.GetChannelTubesForUser(c.UserId);
                allUsers.channels = channelTubelist.Count.ToString();
              //  CheckList check = UserManage.isCheckListExists(c.userId);

                //if (check != null)
                //{
                //    if (check.isHasProfile == true)
                //    {
                //        allUsers.board = "yes";
                //    }
                //    else
                //    {
                //        allUsers.board = "now";
                //    }
                //}
                //else
                //{
                //    allUsers.board = "now";
                //}

                allUsersListArray.Add(allUsers);
            }
            allUsersList.UserItems = allUsersListArray.ToList();

            //foreach (var u in allUsersListArray)
            //{
            //    allUsersList.UserItems = new List<AllUsers>
            //    {new AllUsers{

            //            UserFullName = u.UserFullName,
            //            age = u.age,
            //            gender = u.gender,
            //            accountNum = u.accountNum,
            //            userName = u.userName,
            //          dateOdSignup = u.dateOdSignup,
            //            country = u.country,
            //            state = u.state,
            //            city = u.city,
            //            zip = u.zip,
            //            channels = u.channels,
            //            board = u.board
            //        }
            //};

            string fileName = HttpContext.Current.Server.MapPath("AppDataTxt/allUsers.txt");

            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
            if (!File.Exists(fileName))
            {
                string[] ArrayToWrite = ToSringArray(allUsersListArray);
                File.WriteAllLines(fileName, ArrayToWrite);
            }
            //using (FileStream fs = File.Open(fileName, FileMode.CreateNew))
            //using (StreamWriter sw = new StreamWriter(fs))
            //using (JsonWriter jw = new JsonTextWriter(sw))
            //{
            //    jw.Formatting = Formatting.Indented;


            //    JsonSerializer serializer = new JsonSerializer();
            //    serializer.Serialize(jw, allUsersList);
            //    RootObject[] usersArr = serializer.Deserialize(jw,allUsersList);
            //}
            //create new file


        }

        private string[] ToSringArray(List<AllUsers> allUsersListArray)
        {
            var collection = allUsersListArray as System.Collections.IEnumerable;
            if (collection != null)
            {
                return collection
                    .Cast<object>()
                    .Select(x => x.ToString())
                    .ToArray();
            }
            if (allUsersListArray == null)
            {
                return new string[] { };
            }
            return new string[] { allUsersListArray.ToString() };
        }

        [WebMethod]
        public string SenEmailToUser(string userEmail, string emailBody, string subject)
        {
            string message = "";
            string strSubject = subject;
            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
            //It would be better if all messages will come from database (email messages)
            //this table will be updated from admin panel by admin.
            msg.Body = emailBody;
            msg.Subject = strSubject;
            msg.From = new System.Net.Mail.MailAddress(ConfigurationManager.AppSettings["mailFrom"]);
            msg.To.Add(userEmail);
            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
            client.Send(msg);           
            message = "The email has been sent";
            return message;
        }

        [WebMethod]
        public List<State> GetStatesByCountry(string selectedCountryVal)
        {
            List<State> stateList = new List<State>();
            stateList = ReferenceDataManage.GetStatesByCountryName(selectedCountryVal);
            return stateList;

        }

        [WebMethod]
        public List<State> GetStatesByCountryId(int selectedCountryId)
        {
            List<State> stateList = new List<State>();
            stateList = ReferenceDataManage.GetStatesByCountryId(selectedCountryId);
            return stateList;

        }
        [WebMethod]
        public int GetUsersByCountry (string selectedCountryVal)
        {
            int userCount = 0;
            userCount = UserManage.GetUsersCountByCountry(selectedCountryVal);
            return userCount;
        }
        [WebMethod]
        public int GetUsersByState(string state)
        {
            int usersCount = UserManage.GetUsersCountByState(state);
            return usersCount;
        }
        [WebMethod]
        public int GetUsersByCalendar(int option)
        {
            List<UserPo> userList = UserManage.GetAllUserPos();
            int count = 0;
            switch (option)
            {
                case 2:
                    DateTime today = DateTime.Now;
                    var todayList = userList.Where(x => x.UserMembershipCreatedDate.Date == today.Date).ToList();
                    count = todayList.Count;
                    break;
                case 3:
                    DateTime yesterday = DateTime.Now.AddDays(-1).Date;
                    var yesterdayList = userList.Where(x => x.UserMembershipCreatedDate.Date == yesterday.Date).ToList();
                    count = yesterdayList.Count;
                    break;
                case 4:
                    int thisMonth = DateTime.Now.Month;
                    var thisMonthList = userList.Where(x => x.UserMembershipCreatedDate.Month == thisMonth).ToList();
                    count = thisMonthList.Count;
                    break;
                case 5:
                    int lastMonth = DateTime.Now.AddMonths(-1).Month;
                    var lastMotnthList = userList.Where(x => x.UserMembershipCreatedDate.Month == lastMonth).ToList();
                    count = lastMotnthList.Count;
                    break;

            }
            return count;
        }
        [WebMethod]
        public string GetCustomCalendarCount(string dateFrom, string dateTo)
        {
            string message;
            DateTime dateFromParse;
            DateTime dateToParse;
            string dateFormat = "dd/MM/yyyy";
            DateTime.TryParseExact(dateFrom, dateFormat, new CultureInfo("en-US"), DateTimeStyles.AssumeLocal, out dateFromParse);
            DateTime.TryParseExact(dateTo, dateFormat, new CultureInfo("en-US"), DateTimeStyles.AssumeLocal, out dateToParse);
            if(dateToParse.Date<dateFromParse.Date)
            {
                message = "invalid date"; 
            }
            else
            {
                List<UserPo> userList = UserManage.GetAllUserPos();
                List<User> userInGap = new List<User>();
                foreach(var u in userList)
                {
                   
                    if ((u.UserMembershipCreatedDate.Date >= dateFromParse.Date) && (u.UserMembershipCreatedDate.Date <= dateToParse.Date))
                    {
                        userInGap.Add(u);
                    }
                }
                message = userInGap.Count.ToString();
            }
            return message;
        }

        [WebMethod]
        public List<Country> GetCountries()
        {
            return ReferenceDataManage.GetCountries();
        }
    }
   
}

