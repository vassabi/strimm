using log4net;
using Strimm.Data.Repositories;
using Strimm.Model.Criteria;
using Strimm.Model.Order;
using Strimm.Model.Projections;
using Strimm.Shared;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrimmBL
{
    public static class EmailManage
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(EmailManage));

        public static bool SendUnpublishedScheduleEmail(int channelScheduleId, string channelName, string firstName, string userEmail, DateTime scheduleStartTime, string emailTemplateUri)
        {
            bool isSuccess = false;

            string mailFrom = ConfigurationManager.AppSettings["mailFrom"].ToString();
            DateTime timeSent;

            if (EmailUtils.SendUnpublishedChannelScheduleEmailNotification(channelName, firstName, mailFrom, userEmail, scheduleStartTime, emailTemplateUri, out timeSent))
            {
                using (var emailNotificationRepository = new EmailNotificationRepository())
                {
                    emailNotificationRepository.InsertUnpublishedChannelScheduleEmail(channelScheduleId, userEmail, timeSent);
                }
            }

            return isSuccess;
        }

        public static string ResendActivationLink(string email, string emailTemplateUri)
        {
            string message = String.Empty;
            UserPo userPo = UserManage.GetUserPoByEmail(email);

            if ((userPo != null) && (userPo.UserId != 0))
            {
                string strLink = String.Format("/welcome?token={0}", MiscUtils.EncodeStringToBase64(userPo.Email)); //MiscUtils.EncodeStringToBase64(String.Format("{0}:{1}", userPo.Email, userPo.Password)));
                string link = String.Format("http://{0}{1}", ConfigurationManager.AppSettings["domainName"], strLink);
                string name = userPo.FirstName + " " + userPo.LastName;
                string mailFrom = ConfigurationManager.AppSettings["mailFrom"].ToString();
                string domain = ConfigurationManager.AppSettings["domainName"].ToString();

                var templateUri = new Uri(emailTemplateUri);

                if (EmailUtils.SendEmailConfirmation(userPo.Email, name, userPo.AccountNumber, link, mailFrom, templateUri, domain))
                {
                    message = "the activation link has been sent, please check your email";
                }
                else
                {
                    message = "please check if you added valid email";
                }
            }
            else
            {
                message = "email doesn`t exist, please sign up";
            }

            return message;
        }

        public static string SendFeedback(int userId, string pageName, string selectedOption, string comments, Uri templateUri)
        {
            string messageReturn = String.Empty;
            string subject = String.Empty;
            string body = String.Empty;
            string emailFrom = ConfigurationManager.AppSettings["adminEmail"].ToString();
            bool isEmailSent = false;

            if (userId != null && userId > 0)
            {
                UserPo user = UserManage.GetUserPoByUserId(userId);

                if (user != null)
                {
                    isEmailSent = EmailUtils.SendFeedback(selectedOption, user.FirstName + " " + user.LastName, user.UserName, user.Email, pageName, comments, templateUri, "feedback@strimm.com", emailFrom);

                }
                else
                {
                    Logger.Warn("Failed to send feedback. User cannot be found using specified user Id");
                   isEmailSent= EmailUtils.SendFeedback(selectedOption, "not registred", "not registred", "not registred", pageName, comments, templateUri,"feedback@strimm.com", emailFrom);
                   
                }
            }
            else
            {
                isEmailSent=EmailUtils.SendFeedback(selectedOption, "not registred", "not registred", "not registred", pageName, comments, templateUri,"feedback@strimm.com", emailFrom);
            }

            if (isEmailSent)
            {
                messageReturn = "Thank you for your feedback!";
            }
            else
            {
                messageReturn = "please try later";
            }
            

            return messageReturn;
        }

        public static bool SendWelcomeEmail(UserPo userPo, Uri emailTemplateUri)
        {
            bool isSuccess = false;

            string mailFrom = ConfigurationManager.AppSettings["welcomeEmail"].ToString();
            string domain = ConfigurationManager.AppSettings["domainName"].ToString();

            if (userPo != null)
            {

                if (EmailUtils.SendEmail("Welcome to Strimm", userPo.Email, userPo.FirstName, mailFrom, emailTemplateUri, domain)) 
                {
                    isSuccess = true;
                    Logger.Debug(String.Format("Successfully send welcome e-mail to user '{0}'", userPo.UserName));
                }
                else {
                    Logger.Debug(String.Format("Failed to send welcome e-mail to user '{0}'", userPo.UserName));
                }
            }
            else
            {
                Logger.Debug("Invalid user information specified. Welcome e-mail was not sent");
            }

            return isSuccess;
        }

        public static bool SendBusinessRequestNotification(BusinessContactRequestCriteria request, Uri templateUri)
        {
            bool isSuccess = false;

            string mailFrom = ConfigurationManager.AppSettings["mailFrom"].ToString();
            string toAddress = ConfigurationManager.AppSettings["BusinessContactToEmail"].ToString();
            string domain = ConfigurationManager.AppSettings["domainName"].ToString();

            if (request != null)
            {

                if (EmailUtils.SendBusinessContactRequestEmail(toAddress, request.Name, request.Company, mailFrom, request.WebsiteUrl, request.PhoneNumber, request.PackageType, request.Comments, templateUri, domain))
                {
                    isSuccess = true;
                    Logger.Debug(String.Format("Successfully send business contact request from '{0}'", request.Name));
                }
                else
                {
                    Logger.Debug(String.Format("Failed to send business contact request from '{0}'", request.Name));
                }
            }
            else
            {
                Logger.Debug("Invalid request information specified. Business request contact e-mail was not sent");
            }

            return isSuccess;
        }

        public static bool SendOrderConfirmationEmail(UserPo userPo, OrderPo orderPo, Uri emailTemplateUri, Product product)
        {
            bool isSuccess = false;

            string mailFrom = ConfigurationManager.AppSettings["SubscriptionsEmail"].ToString();
            string domain = ConfigurationManager.AppSettings["domainName"].ToString();
            string bccEmail = ConfigurationManager.AppSettings["SubscriptionsEmail"].ToString();

            if (userPo != null)
            {

                if (EmailUtils.SendEmail("Strimm TV Subscription Confirmation", userPo.Email, userPo.FirstName, mailFrom, emailTemplateUri, domain, userPo.PublicUrl, orderPo.OrderNumber, product.Name, product.Price.Value, bccEmail))
                {
                    isSuccess = true;
                    Logger.Debug(String.Format("Successfully send order confirmation e-mail to user '{0}'", userPo.UserName));
                }
                else
                {
                    Logger.Debug(String.Format("Failed to send order confirmation e-mail to user '{0}'", userPo.UserName));
                }
            }
            else
            {
                Logger.Debug("Invalid user information specified. Order confirmation e-mail was not sent");
            }

            return isSuccess;
        }

        public static bool SendOrderCancellationEmail(UserPo userPo, OrderPo orderPo, Uri emailTemplateUri)
        {
            bool isSuccess = false;

            string mailFrom = ConfigurationManager.AppSettings["SubscriptionsEmail"].ToString();
            string domain = ConfigurationManager.AppSettings["domainName"].ToString();

            if (userPo != null)
            {

                if (EmailUtils.SendEmail("Strimm TV Subscription Cancellation", userPo.Email, userPo.FirstName, mailFrom, emailTemplateUri, domain))
                {
                    isSuccess = true;
                    Logger.Debug(String.Format("Successfully send order cancellation e-mail to user '{0}'", userPo.UserName));
                }
                else
                {
                    Logger.Debug(String.Format("Failed to send order cancellation e-mail to user '{0}'", userPo.UserName));
                }
            }
            else
            {
                Logger.Debug("Invalid user information specified. Order cancellation e-mail was not sent");
            }

            return isSuccess;
        }

        public static bool SendPaymentFailureEmail(UserPo userPo, OrderPo orderPo, Uri emailTemplateUri)
        {
            bool isSuccess = false;

            string mailFrom = ConfigurationManager.AppSettings["SubscriptionsEmail"].ToString();
            string domain = ConfigurationManager.AppSettings["domainName"].ToString();

            if (userPo != null)
            {

                if (EmailUtils.SendEmail("Subscription Payment Failure – Attention Required", userPo.Email, userPo.FirstName, mailFrom, emailTemplateUri, domain))
                {
                    isSuccess = true;
                    Logger.Debug(String.Format("Successfully send payment failure e-mail to user '{0}'", userPo.UserName));
                }
                else
                {
                    Logger.Debug(String.Format("Failed to send payment failure e-mail to user '{0}'", userPo.UserName));
                }
            }
            else
            {
                Logger.Debug("Invalid user information specified. Payment failure e-mail was not sent");
            }

            return isSuccess;
        }
    }
}
