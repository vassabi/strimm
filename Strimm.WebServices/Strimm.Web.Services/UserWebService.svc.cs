using Strimm.Model.Projections;
using Strimm.Shared;
using StrimmBL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Web;

namespace Strimm.Web.Services
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class UserWebService
    {
        // To use HTTP GET, add [WebGet] attribute. (Default ResponseFormat is WebMessageFormat.Json)
        // To create an operation that returns XML,
        //     add [WebGet(ResponseFormat=WebMessageFormat.Xml)],
        //     and include the following line in the operation body:
        //         WebOperationContext.Current.OutgoingResponse.ContentType = "text/xml";

        /// <summary>
        /// This method will authenticate the user using specified
        /// username and password
        /// </summary>
        /// <param name="userName">Username</param>
        /// <param name="password">Password</param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        public bool Login(string userName, string password)
        {
            UserPo user = null;

            if (UserManage.Login(userName, password, out user))
            {
                HttpContext.Current.Session["userId"] = user.UserId;

                return true;
            }

            return false;
        }

        ///// <summary>
        ///// This method will re-send activation link to user
        ///// </summary>
        ///// <param name="email">User's email address</param>
        ///// <returns>Status message</returns>
        //[OperationContract]
        //public string ResendActivationLink(string email)
        //{
        //    string templateUri = System.Web.Hosting.HostingEnvironment.MapPath("~/Emails/ConfirmationEmail.html");

        //    return EmailManage.ResendActivationLink(email, templateUri);
        //}

        //[OperationContract]
        //public void ChangeUserPassword(string password)
        //{
        //    if (HttpContext.Current.Session["userId"] != null)
        //    {
        //        int userId = int.Parse(HttpContext.Current.Session["userId"].ToString());
        //        UserManage.ChangePassword(userId, password);
        //    }
        //}

        ///// <summary>
        ///// This method should be used to sign-out user from
        ///// his current strimm session
        ///// </summary>
        //[OperationContract]
        //public void SignOut()
        //{
        //    if (HttpContext.Current.Session["userId"] != null)
        //    {
        //        HttpContext.Current.Session["channelTubeId"] = null;
        //        HttpContext.Current.Session["userId"] = null;
        //        HttpContext.Current.Session.Abandon();
        //    }
        //}

        ///// <summary>
        ///// This method checks if a specified username is unique
        ///// </summary>
        ///// <param name="userName">Username to check</param>
        ///// <returns>True/False</returns>
        //[OperationContract]
        //public bool IsUserNameUnique(string userName)
        //{
        //    return UserManage.IsUserNameUnique(userName);
        //}

        ///// <summary>
        ///// This method will check if specified user's password
        ///// is strong
        ///// </summary>
        ///// <param name="password">User's password</param>
        ///// <returns>True/False</returns>
        //[OperationContract]
        //public bool IsPasswordStrong(string password)
        //{
        //    return UserManage.IsStrongPassword(password);
        //}

        ///// <summary>
        ///// This method will check if specified user's email address
        ///// is valid or not
        ///// </summary>
        ///// <param name="email">Email address</param>
        ///// <returns>True/False</returns>
        //[OperationContract]
        //public bool IsEmailValid(string email)
        //{
        //    return EmailUtils.isEmailValid(email);
        //}

        ///// <summary>
        ///// This method will check if specified username corresponds 
        ///// to a locked user record
        ///// </summary>
        ///// <param name="userName">Username</param>
        ///// <returns>True/False</returns>
        //[OperationContract]
        //public bool CheckeIfLocked(string userName)
        //{
        //    var user = UserManage.GetUserByLoginIdentifier(userName);

        //    return (user != null) ? user.IsLockedOut : false;
        //}

        //[OperationContract]
        //[WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.WrappedRequest, ResponseFormat = WebMessageFormat.Json)]
        //public string SendFeedback(string pageName, string selectedOption, string comments)
        //{
        //    int? userId = (int?)HttpContext.Current.Session["userId"];

        //    string messageReturn = EmailManage.SendFeedback(userId, pageName, selectedOption, comments);

        //    return messageReturn;
        //}

        //[OperationContract]
        //public string ForgotPass(string userName)
        //{
        //    string message = String.Empty;
        //    UserPo user = UserManage.GetUserByLoginIdentifier(userName);

        //    if ((user != null) && (user.UserId != 0))
        //    {
        //        string pass = MiscUtils.DecodeFrom64(user.Password);
        //        string subject = "Password recovery from Strimm.com";
        //        string emailFrom = ConfigurationManager.AppSettings["mailFrom"];
        //        string mailTo = user.Email;
        //        string body = String.Format("Dear {0},\n\nYou have requested a password reminder for Strimm.com login.\n\n" +
        //                                    "Your password is the following: {0}\n\nSincerely,\n\nStrimm.com Support Team\nwww.strimm.com", user.FirstName, pass);

        //        if (EmailUtils.SendEmail(subject, body, mailTo, emailFrom))
        //        {
        //            message = "Your password has been sent to the email provided";
        //        }
        //        else
        //        {
        //            message = "please check if you added valid email";
        //        }
        //    }
        //    else
        //    {
        //        message = "username or account # doesn`t exist";
        //    }

        //    return message;
        //}

    }
}
