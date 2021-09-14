using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Services;


namespace Strimm.Shared
{
    public static class EmailUtils
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(EmailUtils));

        public static bool SendEmailConfirmation(string emailAddress, string name, string accountNumber, string link, string emailFrom, Uri templateUri, string domain)
        {
            string subject = "Login Confirmation from Strimm.com";
            string body = String.Empty;
            
            body = System.IO.File.ReadAllText(templateUri.AbsolutePath);

            body = body.Replace("{firstName}", name);
            body = body.Replace("{email}", emailAddress);
            body = body.Replace("{accountNumber}", accountNumber);
            body = body.Replace("{url}", link);
            body = body.Replace("{domainName}", domain);

            bool isSent = SendEmail(subject, body, emailAddress, emailFrom);

            return isSent;
        }

        public static bool SendEmail(string subject, string emailAddress, string name, string emailFrom, Uri templateUri, string domain, string publicUrl, string orderNumber, string productName, decimal productPrice, string bccEmailAddress)
        {
            string body = String.Empty;

            using (StreamReader reader = new StreamReader(templateUri.AbsolutePath))
            {
                body = reader.ReadToEnd();
            }

            body = body.Replace("{firstName}", name);
            body = body.Replace("{domainName}", domain);
            body = body.Replace("{publicName}", publicUrl);
            body = body.Replace("{orderNumber}", orderNumber);
            body = body.Replace("{productName}", productName);
            body = body.Replace("{productPrice}", String.Format("${0}", productPrice));

            bool isSent = SendEmail(subject, body, emailAddress, emailFrom, bccEmailAddress);

            return isSent;
        }
        
        public static bool SendEmail(string subject, string emailAddress, string name, string emailFrom, Uri templateUri, string domain)
        {
            string body = String.Empty;

            using (StreamReader reader = new StreamReader(templateUri.AbsolutePath))
            {
                body = reader.ReadToEnd();
            }

            body = body.Replace("{firstName}", name);
            body = body.Replace("{domainName}", domain);

            bool isSent = SendEmail(subject, body, emailAddress, emailFrom);

            return isSent;
        }

        public static bool SendEmailNotification(string firstName, string accountNumber, string emailFrom, string emailTo, int typeOfAction, List<String> channelNames, Uri templateUri, string publicName)
        {
            if (String.IsNullOrEmpty(firstName) ||
                String.IsNullOrEmpty(accountNumber) ||
                String.IsNullOrEmpty(emailFrom) ||
                String.IsNullOrEmpty(emailTo) ||
                typeOfAction < 1)
            {
                throw new ArgumentException("Email notification was not sent. Insufficient data specified");
            }

            string message =  String.Empty;
            string action   = String.Empty;
            string subject  = String.Empty;
            string body     = String.Empty;

            bool isSent     = false;

            StringBuilder channelStringBuilder = new StringBuilder();
            if (channelNames != null && channelNames.Count > 0)
            {
                channelNames.ForEach(x => 
                    {
                        channelStringBuilder.Append(x).Append(',');
                    });
            }

            string allChannels = channelStringBuilder.ToString().TrimEnd(',');

            switch (typeOfAction)
            {
                case 1:
                    {
                        action = "All schedules of your channel have been deleted";
                        subject = "Important notification from Strimm";

                        using (StreamReader reader = new StreamReader(templateUri.AbsolutePath)) //"~Emails/ClearScheduleNotification.html"))
                        {
                            body = reader.ReadToEnd();
                        }

                        body = body.Replace("{firstName}", firstName);

                        body = body.Replace("{publicName}", publicName);

                        body = body.Replace("{channelName}", allChannels);
                        break;
                    }
                case 2:
                    {
                       

                        subject = "Important Notification from Strimm";

                        action = "One of your channels has been deleted";

                        using (StreamReader reader = new StreamReader(templateUri.AbsolutePath)) //"~Emails/DeletedChannelNotification.html"))
                        {
                            body = reader.ReadToEnd();
                        }

                        body = body.Replace("{firstName}", firstName);

                        body = body.Replace("{publicName}", publicName);

                        body = body.Replace("{channelName}", allChannels);

                        break;
                    }
                case 3:
                    {
                        

                        subject = "Important Notification from Strimm";

                        action = "An access to your Strimm account has been blocked";

                        using (StreamReader reader = new StreamReader(templateUri.AbsolutePath)) //"~Emails/LockedUserNotification.html"))
                        {
                            body = reader.ReadToEnd();
                        }

                        body = body.Replace("{firstName}", firstName);

                        body = body.Replace("{publicName}", publicName);



                        break;
                    }

                case 4:
                    {
                        subject = "Important notification from Strimm";

                        using (StreamReader reader = new StreamReader(templateUri.AbsolutePath)) //"~Emails/DeletedAccountNotification.html"))
                        {
                            body = reader.ReadToEnd();
                        }

                        body = body.Replace("{firstName}", firstName);

                        body = body.Replace("{publicName}", publicName);
                        
                        break;
                    }
                default:
                    {
                        throw new Exception("Invalid action specified");
                    }
            }

            isSent = SendEmail(subject, body, emailTo, emailFrom);

            return isSent;
        }

        public static bool SendBusinessContactRequestEmail(string emailAddress, string name, string company, string email, string websiteUrl, string phone, string packageType, string comments, Uri templateUri, string domain)
        {
            string subject = "Business Contact Request";
            string body = String.Empty;

            body = System.IO.File.ReadAllText(templateUri.AbsolutePath);

            body = body.Replace("{Name}", name);
            body = body.Replace("{Company}", company);
            body = body.Replace("{Email}", email);
            body = body.Replace("{WebsiteUrl}", websiteUrl);
            body = body.Replace("{PhoneNumber}", phone);
            body = body.Replace("{PackageType}", packageType);
            body = body.Replace("{Comments}", comments);

            bool isSent = SendEmail(subject, body, emailAddress, email);

            return isSent;
        }

        public static bool SendEmail(string subject, string body, string emailTo, string emailFrom, string bccEmail)
        {
            MailMessage msg = new MailMessage();
            bool isSent = false;
            try
            {
                msg.Body = body;
                msg.Subject = subject;
                msg.From = new MailAddress(emailFrom);
                msg.To.Add(new MailAddress(emailTo));
                msg.IsBodyHtml = true;
                msg.Bcc.Add(bccEmail);

                using (SmtpClient client = new SmtpClient())
                {
                    client.Send(msg);
                    isSent = true;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to send email to {0}.", emailTo), ex);
            }

            return isSent;
        }

        public static bool SendEmail(string subject, string body, string emailTo, string emailFrom)
        {
            MailMessage msg = new MailMessage();
            bool isSent = false;
            try
            {
                msg.Body = body;
                msg.Subject = subject;
                msg.From = new MailAddress(emailFrom);
                msg.To.Add(new MailAddress(emailTo));
                msg.IsBodyHtml = true;
                msg.Bcc.Add(emailFrom);

                using (SmtpClient client = new SmtpClient())
                {
                    client.Send(msg);
                    isSent = true;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to send email to {0}.", emailTo), ex);
            }

            return isSent;
        }

        /// <summary>
        /// This method will check if specified e-mail is valid
        /// </summary>
        /// <param name="emailAddress">Email address to check</param>
        /// <returns>True/Falses</returns>
        public static bool isEmailValid(string emailAddress)
        {
            return Regex.Match(emailAddress, @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$").Success;
        }

        /// <summary>
        /// This method will send unpublished channel schedule email notification to user
        /// </summary>
        /// <param name="channelName">Channel Name</param>
        /// <param name="firstName">User's first name</param>
        /// <param name="mailFrom">From e-mail address</param>
        /// <param name="userEmail">User's e-mail address</param>
        /// <param name="scheduleStartTime">Channel Schedule start time and date</param>
        /// <param name="emailTemplateUri">Uri to email tempate</param>
        /// <param name="timeSent">Date and time when e-mail was sent</param>
        /// <returns></returns>
        public static bool SendUnpublishedChannelScheduleEmailNotification(string channelName, string firstName, string mailFrom, string userEmail, DateTime scheduleStartTime, string emailTemplateUri, out DateTime timeSent)
        {
            string subject = "Unpublished Channel Schedule Notification from Strimm.com";

            string body = String.Empty;

            using (StreamReader reader = new StreamReader(emailTemplateUri))
            {
                body = reader.ReadToEnd();
            }

            body = body.Replace("{firstName}", firstName);
            body = body.Replace("{channelName}", channelName);
            body = body.Replace("{startDate}", scheduleStartTime.ToString("MM/dd/yyyy"));
            body = body.Replace("{startTime}", DateTimeUtils.PrintAirTime(scheduleStartTime));

            bool isSent = SendEmail(subject, body, userEmail, mailFrom);

            timeSent = DateTime.Now;

            return isSent;
        }

        public static bool SendPasswordRecovery(string emailAddress, string name, string accountNumber, string link, string emailFrom, Uri templateUri, string domainName)
        {
            string subject = "Password Recovery from Strimm.com";
            //string body = String.Format("Dear {0},\n\nThank you for registration on Strimm.com, a Public Broadcast Network for 21st Century™!"
            //                + "\n\nYour login: {1}\nYour password: {2}\nYour username: {3}\nYour account number: {4}\n\nPlease follow the confirmation link: \n{5}\n\n"
            //                + "Together, we will create a new era of public television!\n\nSincerely,\n\nStrimm.com Team\nwww.strimm.com\nOur “Terms of Use” can be found here:   http://www.strimm.com/terms-of-use",
            //                name, emailAddress, password, username, accountNumber, link);
            string body = String.Empty;

            using (StreamReader reader = new StreamReader(templateUri.AbsolutePath)) //"~Emails/ConfirmationEmail.html"))
            {
                body = reader.ReadToEnd();
            }

            body = body.Replace("{firstName}", name);
            body = body.Replace("{domainName}", domainName);
            body = body.Replace("{url}", link);

            bool isSent = SendEmail(subject, body, emailAddress, emailFrom);

            return isSent;
        }

        public static bool SendFeedback(string type, string userFullName, string publicName, string email, string pageName, string comments, Uri templateUri, string emailAddress, string emailFrom)
        {
            string subject = "Feedback report";
           
            string body = String.Empty;

            using (StreamReader reader = new StreamReader(templateUri.AbsolutePath)) //"~Emails/Feedback.html"))
            {
                body = reader.ReadToEnd();
            }

            body = body.Replace("{type}", type);

            body = body.Replace("{senderFullName}", userFullName);

            body = body.Replace("{publicName}", publicName);

            body = body.Replace("{email}", email);
            body = body.Replace("{pageName}", pageName);
            body = body.Replace("{comments}", comments);
           

            bool isSent = SendEmail(subject, body, emailAddress, emailFrom);
       
            return isSent;
        }
    }
}
