using log4net;
//using MailChimp.Api.Net.Domain.BatchOperation;
//using MailChimp.Api.Net.Domain.Lists;
//using MailChimp.Api.Net.Enum;
//using MailChimp.Api.Net.Helper;
//using MailChimp.Api.Net.Services.BatchOperation;
//using MailChimp.Api.Net.Services.Lists;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Strimm.Model;
//using Strimm.Model.MailChimp;
//using Strimm.Model.MailChimp.Requests;
//using Strimm.Model.MailChimp.Responses;
using Strimm.Model.Projections;
using Strimm.Shared;
using Strimm.Shared.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Strimm.Shared.Async;
using MailChimp.Net.Interfaces;
using MailChimp.Net;
using MailChimp.Api.Net.Enum;
using MailChimp.Net.Models;

namespace StrimmBL
{
    public static class MailChimpManage
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(MailChimpManage));

        private static string MAIL_CHIMP_API_URI;
        private static string MAIL_CHIMP_API_KEY;
                
        public static void init(string apiKey,string uri)
        {
            MAIL_CHIMP_API_KEY = String.IsNullOrEmpty(apiKey) ? ConfigurationManager.AppSettings["MailChimpApiKey"] : apiKey;
            MAIL_CHIMP_API_URI = String.IsNullOrEmpty(uri) ? ConfigurationManager.AppSettings["MailChimpUri"] : uri;
        }

        public static async void AddOrUpdateMailingChimpSubscriptionAsync(UserMailingListPo user, string listId)
        {
            if (user == null || String.IsNullOrEmpty(listId))
            {
                throw new ArgumentException("Invalid user or mailing list id specified");
            }

            try
            {
                var member = MapSubscriber(user);

                var myContentJson = SerializeMailChimpEntity(member);

                IMailChimpManager manager = new MailChimpManager(MAIL_CHIMP_API_KEY);

                Member membermc = await manager.Members.AddOrUpdateAsync(listId, member);

                if (String.IsNullOrEmpty(user.MailChimpEmailId))
                {
                    user.MailChimpEmailId = membermc.Id;
                    Logger.Info(String.Format("User with e-mail '{0}' was successfully subscribed to a MailChimp list: {0}", user.Email));

                    UserManage.MarkUserRegisteredWithMailChimp(user.UserId, user.MailChimpEmailId);
                    Logger.Debug(String.Format("User with e-mail '{0}' and MailChimp id '{1}' was successfully marked as subscriber in the db", user.Email, user.MailChimpEmailId));
                }
                else
                {
                    Logger.Debug(String.Format("User with e-mail '{0}' and MailChimp id '{1}' was successfully updated in MailChimp", user.Email, user.MailChimpEmailId));

                    UserManage.UpdateUserRegistrationWithMailChimp(user.UserId);
                    Logger.Debug(String.Format("User with e-mail '{0}' and MailChimp id '{1}' was successfully updated in the db", user.Email, user.MailChimpEmailId));
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Error occured while adding/updating user '{0}' with MailChimpId={1} in List with Id={2}", user.Email, user.MailChimpEmailId, listId), ex);
                
                UserManage.UpdateUserEmailPreferences(user.UserId, false, false, false, false, false);
                Logger.Debug(String.Format("Turning off notifications to user with id={0}. User unsubscribed on Mail Chimp", user.UserId));
            }
        }

        //public static void AddOrUpdateAllMailChimpUserSubscriptionsInBatch(List<UserMailingListPo> newUsers, string listId)
        //{
        //    if ((newUsers == null || newUsers.Count == 0) || String.IsNullOrEmpty(listId))
        //    {
        //        throw new ArgumentException("Invalid list of users or list id specified");
        //    }

        //    MailChimpList mcList = new MailChimpList();
        //    var mcLists = AsyncHelper.RunSync<RootMCLists>(() => mcList.GetAllListsAsync());

        //    if (mcLists.lists.Count > 0)
        //    {
        //        var targetList = mcLists.lists.FirstOrDefault(x => x.id == listId);

        //        if (targetList != null)
        //        {
        //            RootBatch batchObj = new RootBatch();

        //            int i = 1;

        //            newUsers.ForEach(u => {

        //                var member = MapSubscriber(u);

        //                var myContentJson = SerializeMailChimpEntity(member);

        //                var singleOpt = BuildSingleBatchOperation(targetList.id, myContentJson, i, u.Email);

        //                batchObj.operations.Add(singleOpt);

        //                i++;
        //            });
                    
        //            MailChimpBatch goBatch = new MailChimpBatch();
        //            var batchResult = goBatch.PostBatchOperationAsync(batchObj).Result;

        //            if (batchResult != null)
        //            {
        //                var batchId = batchResult.Result.id;
        //                RootBatch result = null;

        //                do
        //                {
        //                    result = goBatch.GetBatchReportById(batchId).Result;
        //                    Thread.Sleep(2000);
        //                }
        //                while (result.status != "finished");

        //                if (result.errored_operations > 0)
        //                {
        //                    string detailsReportForIssueTrackingURL = result.response_body_url.ToString();

        //                    try
        //                    {
        //                        string newFileName = @"c:\" + batchId + ".tar.gz";
        //                        FileDownloader.download(detailsReportForIssueTrackingURL, newFileName);
        //                        Logger.Warn(String.Format("Error occured while adding new subscribers: {0}", "<<need to retrieve error>>"));
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        Logger.Error(String.Format("Error occured while reading error response message from mailchimp using url '{0}'", detailsReportForIssueTrackingURL), ex);
        //                    }
        //                }

        //                RefreshUsersSubscriptionStatus(listId, ref mcList, newUsers);
        //            }
        //        }
        //    }
        //}

        private static Member MapSubscriber(UserMailingListPo user)
        {
            Member subscriber = null;

            if (user != null)
            {
                subscriber = new Member();

                subscriber.EmailAddress = user.Email;
                subscriber.EmailType = "html";
                subscriber.Language = "English";
                subscriber.StatusIfNew = Status.Subscribed;
                subscriber.Status = Status.Subscribed;

                subscriber.MergeFields = new Dictionary<string, string>();

                subscriber.MergeFields.Add("FNAME", user.FirstName);
                subscriber.MergeFields.Add("LNAME", user.LastName);
                subscriber.MergeFields.Add("BDAYSHRT", user.UnsubscribeFromBirthdayEmail ? null : (user.BirthDate != null ? user.BirthDate.Value.ToString("MM/dd") : DateTime.MinValue.ToString("MM/dd")));
                subscriber.MergeFields.Add("BDAYLONG", user.UnsubscribeFromBirthdayEmail ? null : (user.BirthDate != null ? user.BirthDate.Value.ToString("MM/dd/yyyy") : DateTime.MinValue.ToString("MM/dd/yyyy")));
                subscriber.MergeFields.Add("SIGNUPDATE", user.SignUpDate.ToString("MM/dd/yyyy"));
                subscriber.MergeFields.Add("USERNAME", user.UserName);
                subscriber.MergeFields.Add("NETWORK", user.PublicUrl);
                subscriber.MergeFields.Add("1STCHDATE", (user.FirstChannelTubeCreatedDate != null ? user.FirstChannelTubeCreatedDate.Value.ToString("MM/dd/yyyy") : DateTime.MinValue.ToString("MM/dd/yyyy")));
                subscriber.MergeFields.Add("NETWORKURL", String.Format("https://www.strimm.com/{0}", user.PublicUrl));
                subscriber.MergeFields.Add("LASTCHDATE", (user.LastChannelTubeCreatedDate != null ? user.LastChannelTubeCreatedDate.Value.ToString("MM/dd/yyyy") : DateTime.MinValue.ToString("MM/dd/yyyy")));
                subscriber.MergeFields.Add("NOUSRAVATR", (user.MissingUserAvatarOrBio != null && user.MissingUserAvatarOrBio.Value) ? "TRUE" : "FALSE");
                subscriber.MergeFields.Add("NOCHAVATAR", (user.MissingChannelAvatar != null && user.MissingChannelAvatar.Value) ? "TRUE" : "FALSE");
                subscriber.MergeFields.Add("EMAILPFURL", String.Format("https://www.strimm.com/email-preferences/email={0}", user.Email));
                subscriber.MergeFields.Add("NEWCHLIKES", (user.HasNewChannelLikes != null && user.HasNewChannelLikes.Value) ? "TRUE" : "FALSE");
                subscriber.MergeFields.Add("NEWFANS", (user.HasNewFans != null && user.HasNewFans.Value) ? "TRUE" : "FALSE");
                subscriber.MergeFields.Add("STNWSDATE", user.UnsubscribeFromSiteNewsNotificationEmail ? null : (user.SiteNewsTargetDate != null ? user.SiteNewsTargetDate.Value.ToString("MM/dd/yyyy") : DateTime.MinValue.ToString("MM/dd/yyyy")));
                subscriber.MergeFields.Add("STUPDTDATE", user.UnsubscribeFromSiteUpdateNotificationEmail ? null : (user.SiteUpdateStartDate != null ? user.SiteUpdateStartDate.Value.ToString("MM/dd/yyyy") : DateTime.MinValue.ToString("MM/dd/yyyy")));
                subscriber.MergeFields.Add("PRSSRLDATE", user.UnsubscribeFromNewPressReleaseEmail ? null : (user.PressReleaseTargetDate != null ? user.PressReleaseTargetDate.Value.ToString("MM/dd/yyyy") : DateTime.MinValue.ToString("MM/dd/yyyy")));
                subscriber.MergeFields.Add("INDNWSDATE", user.UnsubscribeFromIndustryNewsEmail ? null : (user.IndustryNewsTargetDate != null ? user.IndustryNewsTargetDate.Value.ToString("MM/dd/yyyy") : DateTime.MinValue.ToString("MM/dd/yyyy")));
                subscriber.MergeFields.Add("NOSCHEDULE", user.MissingNextDaySchedule != null && user.MissingNextDaySchedule.Value ? "TRUE" : "FALSE");

                subscriber.MergeFields.Add("UNSCRB_ALL", user.UnsubscribeFromAllEmail ? "TRUE" : "FALSE");
                subscriber.MergeFields.Add("UNSCRB_DOB", user.UnsubscribeFromBirthdayEmail ? "TRUE" : "FALSE");
                subscriber.MergeFields.Add("UNSCRB_1CH", user.UnsubscribeFromFirstChannelEmail ? "TRUE" : "FALSE");
                subscriber.MergeFields.Add("UNSCRB_NCH", user.UnsubscribeFromNewChannelEmail ? "TRUE" : "FALSE");
                subscriber.MergeFields.Add("UNSCRB_AVR", user.UnsubscribeFromAvatarBioReminderEmail ? "TRUE" : "FALSE");
                subscriber.MergeFields.Add("UNSCRB_CAV", user.UnsubscribeFromChannelAvatarReminderEmail ? "TRUE" : "FALSE");
                subscriber.MergeFields.Add("UNSCRB_TIP", user.UnsubscribeFromChannelTipsInfoEmail ? "TRUE" : "FALSE");
                subscriber.MergeFields.Add("UNSCRB_LIK", user.UnsubscribeFromChannelLikedNotificationEmail ? "TRUE" : "FALSE");
                subscriber.MergeFields.Add("UNSCRB_FAN", user.UnsubscribeFromNewChannelFanNotificationEmail ? "TRUE" : "FALSE");
            }

            return subscriber;
        }

        //private static SingleOperation BuildSingleBatchOperation(string listId, string body, int increment, string email)
        //{
        //    SingleOperation singleOpt = new SingleOperation();

        //    singleOpt.method = "PUT";
        //    singleOpt.path = String.Format("/{0}/{1}/{2}/{3}", TargetTypes.lists, listId, SubTargetType.members, CryptoUtils.CalculateMD5Hash(email));
        //    singleOpt.operation_id = String.Format("{0}", String.Format("STRIMM_{0}", (DateTime.Now.Ticks + increment)));
        //    singleOpt.body = body;

        //    return singleOpt;
        //}

        private static string SerializeMailChimpEntity<T>(T entity)
        {
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,

                Converters = new List<JsonConverter> 
                            { 
                                new IsoDateTimeConverter()
                                {
                                    DateTimeFormat= "yyyy-MM-dd HH:mm:ss"
                                }
                            }
            };

            return JsonConvert.SerializeObject(entity, settings);
        }

        //private static void RefreshUsersSubscriptionStatus(string listId, ref MailChimpList mcList, List<UserMailingListPo> newUsers)
        //{
        //    var mcusers = mcList.GetAllMemberInfoAsync(listId).Result;

        //    if (mcusers != null)
        //    {
        //        mcusers.members.ForEach(m =>
        //        {
        //            var user = newUsers.SingleOrDefault(x => x.Email == m.email_address);

        //            if (user != null)
        //            {
        //                if (String.IsNullOrEmpty(user.MailChimpEmailId))
        //                {
        //                    UserManage.MarkUserRegisteredWithMailChimp(user.UserId, m.unique_email_id);
        //                    Logger.Debug(String.Format("User with email '{0}' was successfully registered with MailChimp", m.unique_email_id));
        //                }
        //                else
        //                {
        //                    UserManage.UpdateUserRegistrationWithMailChimp(user.UserId);
        //                    Logger.Debug(String.Format("MailChimp subscription was successfully updated for user with e-mail '{0}'", user.Email));
        //                }
        //            }
        //        });
        //    }

        //    Logger.Debug("Successfully added users to the mailing list");
        //}
    }
}
