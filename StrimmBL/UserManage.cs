using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Security.Cryptography;
using System.IO;
using Strimm.Shared;
using Strimm.Data.Repositories;
using Strimm.Model.Projections;
using System.Configuration;
using log4net;
using Strimm.Model;
using RestSharp.Contrib;
using Strimm.Model.WebModel;
using Strimm.Model.Criteria;

namespace StrimmBL
{
    public static class UserManage
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(UserManage));

        public static bool IsStrongPassword(string password)
        {
            var score = AccountUtils.GetPasswordStrengthScore(password);
            return score > PasswordScore.VeryWeak;
        }
        

        public static void ConfirmUser(int userId)
        {
            var userRepository = new UserRepository();
            var videoRoomTubeRepository = new VideoRoomTubeRepository();

            try
            {
                if (userRepository.ConfirmUserByUserId(userId))
                {
                    Logger.Debug(String.Format("User with Id={0} was successfully confirmed", userId));

                    // MST: VideoRoomTube record is already being created together with the user record inside InsertUser() sproc
                    //
                    //if (videoRoomTubeRepository.InsertVideoRoomTube(userId))
                    //{
                    //    Logger.Debug(String.Format("Video room was successfully created for user with Id={0}", userId));
                    //}
                    //else
                    //{
                    //    throw new Exception(String.Format("Failed to create VideoRoomTube for User with Id={0}", userId));
                    //}
                }
                else
                {
                    throw new Exception(String.Format("Failed to confirm/activate user with Id={0}", userId));
                }
            }
            finally
            {
                if (userRepository != null)
                {
                    userRepository = null;
                }

                if (videoRoomTubeRepository != null)
                {
                    videoRoomTubeRepository = null; // cannot assign as null when use using statement
                }
            }

        }

        public static bool IsEmailExist(string email)
        {
            bool isExists = false;

            using (var userRepository = new UserRepository())
            {
                var user = userRepository.GetUserByEmail(email);
                isExists = user != null;
            }

            return isExists;
        }

        public static void SetUserLastLoginDate(int userId)
        {
            using (var userRepository = new UserRepository())
            {
                userRepository.UpdateUserLastLoginDateByUserId(userId);
            }
        }

        public static bool  Login(string loginIdentifier, string password, out UserPo user)
        {
            Logger.Info(String.Format("Logging user with login identifier '{0}'", loginIdentifier));

            using (var userRepository = new UserRepository())
            {
                user = userRepository.GetUserPoByLoginIdentifier(loginIdentifier);

                if (user != null)
                {
                    var saltedPassword = CryptoUtils.HashPassword(CryptoUtils.GetPasswordWithSalt(user.Email, password));
                    int userId = userRepository.GetUserIdByLoginIdentifierAndPassword(loginIdentifier, saltedPassword);

                    if (userId > 0)
                    {
                        userRepository.UpdateUserLastLoginDateByUserId(userId);
                    }

                    return userId > 0;
                }
            }

            return false;
        }
        public static bool EncryptedLogin(string loginIdentifier, string saltedPassword, out UserPo user)
        {
             Logger.Info(String.Format("Logging user with login identifier '{0}'", loginIdentifier));

            using (var userRepository = new UserRepository())
            {
                user = userRepository.GetUserPoByLoginIdentifier(loginIdentifier);

                if (user != null)
                {
                    //var saltedPassword = CryptoUtils.HashPassword(CryptoUtils.GetPasswordWithSalt(user.Email, password));
                    int userId = userRepository.GetUserIdByLoginIdentifierAndPassword(loginIdentifier, saltedPassword);

                    if (userId > 0)
                    {
                        userRepository.UpdateUserLastLoginDateByUserId(userId);
                    }

                    return userId > 0;
                }
            }

            return false;
        }

        public static void ChangePassword(int userId, string password, DateTime clientTime)
        {
            Logger.Info(String.Format("Changing password for user with Id={0}", userId));

            using (var userRepository = new UserRepository())
            {
                var userMembership = userRepository.GetUserMembershipByUserId(userId);

                if (userMembership != null)
                {
                    userMembership.Password = CryptoUtils.HashPassword(CryptoUtils.GetPasswordWithSalt(userMembership.Email, password));
                    userMembership.LastPasswordChangeDate = clientTime;
                    userRepository.UpdateUserMembership(userMembership);
                }
            }
        }

        public static UserPo GetUserPoByLoginIdentifierAndPassword(string loginIdentifier, string password)
        {
            UserPo user = null;

            using (var userRepository = new UserRepository())
            {
                user = userRepository.GetUserPoByLoginIdentifierAndPassword(loginIdentifier, password);
            }

            return user;
        }

        public static UserPo GetUserByLoginIdentifier(string loginIdentifier)
        {
            UserPo user = null;

            using (var userRepository = new UserRepository())
            {
                user = userRepository.GetUserPoByLoginIdentifier(loginIdentifier);
            }

            return user;
        }

        public static void LockUnlockUserByAccountNumber(string accountNumber, bool isLocked)
        {
            using (var userRepository = new UserRepository())
            {
                userRepository.LockUnlockUserByAccountNumber(accountNumber, isLocked);
            }
        }

        public static bool UpdateUser(UserPo user)
        {
            if (user == null || user.UserId == 0)
            {
                throw new Exception("Failed to update user. Specified user is invalid or does not exist");
            }

            Logger.Info(String.Format("Updating user with Id={0}", user.UserId));

            bool isSuccess;

            using (var userRepository = new UserRepository())
            {
                isSuccess = userRepository.UpdateUser(user);
            }

            return isSuccess;
        }

        public static bool UpdateUserProfile(UserPo user)
        {
            if (user == null || user.UserId == 0)
            {
                throw new Exception("Failed to update user. Specified user is invalid or does not exist");
            }

            Logger.Info(String.Format("Updating user with Id={0}", user.UserId));

            bool isSuccess;

            using (var userRepository = new UserRepository())
            {
                isSuccess = userRepository.UpdateUserProfile(user);
            }

            return isSuccess;
        }

        public static bool IsUserNameUnique(string userName)
        {
            if (String.IsNullOrEmpty(userName))
            {
                return false;
            }

            var decodedName = PublicNameUtils.UrlDecodePublicName(userName);

            User user = null;
            User userByPublicName = null;
            bool isReservedName = false;
            bool userExists = false;

            using (var userRepository = new UserRepository())
            {
                user = userRepository.GetUserByUserName(decodedName);
                if (user == null)
                {
                    userByPublicName = userRepository.GetUserByPublicUrl(PublicNameUtils.GetUrl(decodedName));
                }

                userExists = user != null || userByPublicName != null;

                isReservedName = (ReferenceDataManage.IsReservedName(decodedName) || ReferenceDataManage.IsReservedName(PublicNameUtils.GetUrl(decodedName)));
            }

            return !userExists && !isReservedName;
        }

        public static void FollowUser(int userId, int followUserId, int offset)
        {
            using (var userRepository = new UserRepository())
            {
                var startFollowDate = DateTime.UtcNow.AddMinutes(offset);
                userRepository.InsertUserFollower(userId, followUserId, startFollowDate);
            }
        }

        public static bool IsFollowing(int followerId, int userId)
        {
            bool isFollowing;

            using (var userRepository = new UserRepository())
            {
                var userFollower = userRepository.GetUserFollowerByUserIdAndFollowerUserId(userId, followerId);
                isFollowing = userFollower != null;
            }

            return isFollowing;
        }

        public static void UnFollowUser(int userId, int followUserId)
        {
            using (var userRepository = new UserRepository())
            {
                userRepository.DeleteUserFollowerByFollowerUserIdAndUserId(followUserId, userId);
            }
        }

        public static List<Strimm.Model.UserFollower> GetFollowersList(int userId)
        {
            var userFollowers = new List<UserFollower>();

            using (var userRepository = new UserRepository())
            {
                userFollowers = userRepository.GetAllFollowersByUserId(userId);
            }

            return userFollowers;
        }

        public static List<UserFollower> GetAllFollowersByUserId(int userId)
        {
            var userFollowers = new List<UserFollower>();

            using (var userRepository = new UserRepository())
            {
                userFollowers = userRepository.GetAllFollowersByUserId(userId);
            }

            return userFollowers;
        }

        public static List<User> GetAllUsers()
        {
            var users = new List<User>();

            using (var userRepository = new UserRepository())
            {
                users = userRepository.GetAllUsers();
            }

            return users;
        }

        public static List<UserPo> GetAllUserPos()
        {
            var users = new List<UserPo>();

            using (var userRepository = new UserRepository())
            {
                users = userRepository.GetAllUserPos();
            }

            return users;
        }

        public static List<UserPo> GetAllUserPosForAdmin()
        {
            var users = new List<UserPo>();

            using (var userRepository = new UserRepository())
            {
                users = userRepository.GetAllUserPosForAdmin();
            }

            return users;
        }

        public static int GetUsersCountByCountry(string countryName)
        {
            int count = 0;

            using (var userRepository = new UserRepository())
            {
                count = userRepository.GetUserCountByCountry(countryName);
            }

            return count;
        }

        public static int GetUsersCountByState(string stateName)
        {
            int count = 0;

            using (var userRepository = new UserRepository())
            {
                count = userRepository.GetUserCountByState(stateName);
            }

            return count;
        }

        public static UserPo GetUserByUserName(string userName)
        {
            UserPo user = null;

            using (var userRepository = new UserRepository())
            {
                user = userRepository.GetUserPoByUserName(userName);
            }

            return user;
        }

        public static UserPo GetUserByPublicUrl(string publicUrl)
        {
            UserPo user = null;

            using (var userRepository = new UserRepository())
            {
                user = userRepository.GetUserPoByPublicUrl(publicUrl);
            }

            return user;
        }

        public static UserPo GetUserByAccountNumber(string accountNumber)
        {
            UserPo user = null;

            using (var userRepository = new UserRepository())
            {
                user = userRepository.GetUserPoByAccountNumber(accountNumber);
            }

            return user;
        }

        public static UserPo GetUserByEmail(string email)
        {
            UserPo user = null;

            using (var userRepository = new UserRepository())
            {
                user = userRepository.GetUserPoByEmail(email);
                if (user == null)
                {
                    user = userRepository.GetDeletedUser(email);
                }
            }

            return user;
        }

        public static UserPo GetDeletedUser(string email)
        {
            UserPo user = null;

            using (var userRepository = new UserRepository())
            {
                user = userRepository.GetDeletedUser(email);
            }

            return user;
        }

        public static UserPo GetUserByChannelName(string channelName)
        {
            UserPo user = null;

            using (var userRepository = new UserRepository())
            {
                user = userRepository.GetUserPoByChannelTubeName(channelName);
            }

            return user;
        }

        public static void DeleteUser(int userId)
        {
            using (var userRepository = new UserRepository())
            {
                userRepository.DeleteUserById(userId);
            }
        }

        public static void RemoveFollower(int userId, int followerUserId)
        {
            using (var userRepository = new UserRepository())
            {
                userRepository.DeleteUserFollowerByFollowerUserIdAndUserId(followerUserId, userId);
            }
        }

        public static UserPo GetUserPoByEmail(string email)
        {
            UserPo user = null;

            using (var userRepository = new UserRepository())
            {
                user = userRepository.GetUserPoByEmail(email);
            }

            return user;
        }

        public static UserPo GetUserPoByUserId(int userId)
        {
            UserPo user = null;

            using (var userRepository = new UserRepository())
            {
                user = userRepository.GetUserPoByUserId(userId);
            }

            return user;
        }

        public static UserPo GetUserPoByUserIdForAdmin(int userId)
        {
            UserPo user = null;

            using (var userRepository = new UserRepository())
            {
                user = userRepository.GetUserPoByUserIdForAdmin(userId);
            }

            return user;
        }

        public static void InsertUser(string userName, string userIp, string password,
                                        string email, string firstName, string lastName, DateTime birthDate,
                                        string countryName, string gender, Uri emailTemplateUri,
                                        UserLocation location, bool isFilmMaker)
        {
            string accountNumber = String.Empty;
            string emailLink = String.Empty;
            string mailFrom = String.Empty;
            string domain = String.Empty;

            string domainName = ConfigurationManager.AppSettings["domainName"];

            using (var userRepository = new UserRepository())
            using (var videoRoomTubeRepository = new VideoRoomTubeRepository())
            {
                mailFrom = ConfigurationManager.AppSettings["mailFrom"].ToString();

                accountNumber = AccountUtils.GenerateAccountNumber();
                emailLink = String.Format("https://{0}/home?token={1}", domainName, MiscUtils.EncodeStringToBase64(email)); //MiscUtils.EncodeStringToBase64(String.Format("{0}:{1}", email, password)));

                // MAX-TODO: Need to create insert with Get in order to immideately retrieve
                // user object or at least its id
                if (userRepository.InsertUser(userName, PublicNameUtils.GetUrl(userName), accountNumber, userIp, password, email, firstName, lastName, birthDate, countryName, gender, false, null, null, null, isFilmMaker))
                {
                    UpdateUserLastKnownLocationByUsername(location, userName);

                    if (EmailUtils.SendEmailConfirmation(email, firstName, accountNumber, emailLink, mailFrom, emailTemplateUri, domainName))
                    {
                        if (userRepository.UpdateActivationEmailSendDateByUserName(userName))
                        {
                            Logger.Debug(String.Format("Successfully set activation e-mail send date for user with userName='{0}'", userName));
                        }
                        else
                        {
                            Logger.Warn(String.Format("Failed to set activation e-mail send date for user with userName='{0}'", userName));
                        }

                        Logger.Debug(String.Format("Activation e-mail was successfully send to '{0}, {1}', username='{2}', email={3}, account number={4}, ip={5}, birthdate={6}, country={7}, gender={8}",
                            lastName, firstName, userName, email, accountNumber, userIp, birthDate, countryName, gender));
                    }
                    else
                    {
                        Logger.Error(String.Format("Failed to send activation e-mail to '{0}, {1}', username='{2}', email={3}, account number={4}, ip={5}, birthdate={6}, country={7}, gender={8}",
                            lastName, firstName, userName, email, accountNumber, userIp, birthDate, countryName, gender));

                        throw new Exception(String.Format("Failed to send activation email to user '{0}'", userName));
                    }
                }
                else
                {
                    Logger.Error(String.Format("Failed to create user '{0}, {1}', username='{2}', email={3}, account number={4}, ip={5}, birthdate={6}, country={7}, gender={8}",
                        lastName, firstName, userName, email, accountNumber, userIp, birthDate, countryName, gender));

                    throw new Exception(String.Format("Failed to create user {0}", userName));
                }
            }
        }

        public static bool UpdateUserLastKnownLocationByUsername(UserLocation location, string username)
        {
            if (location == null)
            {
                return false;
            }

            Logger.Info(String.Format("Updating last known user location for user '{0}'", username));

            bool isSuccess = false;

            using (var referenceDataRepository = new ReferenceDataRepository())
            using (var userRepository = new UserRepository())
            {
                var states = referenceDataRepository.GetAllStates();
                var countries = referenceDataRepository.GetAllCountries();

                if (countries != null && countries.Count > 0 && states != null && states.Count > 0 && !String.IsNullOrEmpty(location.Country))
                {
                    var country = countries.FirstOrDefault(x => x != null && x.Name.Contains(location.Country));
                    if (country != null)
                    {
                        var state = states.FirstOrDefault(x => x.Code_2 == location.State && x.CountryId == country.CountryId);
                        if (state != null)
                        {
                            location.State = state.Name;
                        }
                    }
                }

                isSuccess = userRepository.UpdateUserLastKnownLocationByUsername(location, username);
            }

            return isSuccess;
        }


        public static bool InsertUserByFacebookLogin(string userName, string userIp, string password,
                                      string email, string firstName, string lastName, DateTime birthDate,
                                      string countryName, string gender, string facebookPictureUrl,
                                      string city, string state, string zipcode)
        {

            string accountNumber = String.Empty;
            string emailLink = String.Empty;
            string mailFrom = String.Empty;
            bool isNewUserCreated = false;
            bool isExternalUser = true;

            using (var userRepository = new UserRepository())
            using (var videoRoomTubeRepository = new VideoRoomTubeRepository())
            {
                //Insert user from facebook user data
                if (userRepository.InsertUser(userName, PublicNameUtils.GetUrl(userName), accountNumber, userIp, password, email, firstName, lastName, birthDate, countryName, gender, isExternalUser, city, state, zipcode, false))
                {
                    userRepository.UpdateUserLastKnownLocationByUsername(new UserLocation()
                {
                    City = city,
                    Country = countryName,
                    isSignUp = true,
                    PostalCode = zipcode,
                    State = state,
                    UserIp = userIp
                }, userName);

                    if (userRepository.UpdateActivationEmailSendDateByUserName(userName))
                    {
                        Logger.Debug(String.Format("Successfully set activation e-mail send date for user with userName='{0}'", userName));
                    }
                    else
                    {
                        Logger.Warn(String.Format("Failed to set activation e-mail send date for user with userName='{0}'", userName));
                    }


                    //Get inserted user and confirm
                    UserPo user = GetUserByEmail(email);
                    BoardManage.UpdateUserBoard(user.UserId, String.Empty, facebookPictureUrl, String.Empty, String.Empty);
                    ConfirmUser(user.UserId);
                    isNewUserCreated = true;

                }
                else
                {
                    Logger.Error(String.Format("Failed to create user '{0}, {1}', username='{2}', email={3}, account number={4}, ip={5}, birthdate={6}, country={7}, gender={8}",
                        lastName, firstName, userName, email, accountNumber, userIp, birthDate, countryName, gender));

                    throw new Exception(String.Format("Failed to create user {0}", userName));

                }
                return isNewUserCreated;
            }
        }

        public static UserPo GetUserPoByChannelName(string channelTubeName)
        {
            UserPo user = null;

            using (var userRepository = new UserRepository())
            {
                user = userRepository.GetUserPoByChannelTubeName(channelTubeName);
            }

            return user;
        }

        public static bool UpdateUserPassword(int userId, DateTime clientDateTime, string newPassword)
        {
            Logger.Info(String.Format("Changing password for user with Id={0}", userId));

            using (var userRepository = new UserRepository())
            {
                var userMembership = userRepository.GetUserMembershipByUserId(userId);
                var hashedPassword = CryptoUtils.HashPassword(CryptoUtils.GetPasswordWithSalt(userMembership.Email, newPassword));
                return userRepository.UpdateUserPassword(userId, clientDateTime, hashedPassword);

            }
        }

        public static UserPo EnableProForUserByAccountNumberWithGet(string accountNumber, bool isProEnabled)
        {
            Logger.Info(String.Format("Enabling PRO features on user account with account number '{0}': {1}", accountNumber, isProEnabled));

            UserPo user = null;

            using (var userRepository = new UserRepository())
            {
                user = userRepository.EnableProForUserByAccountNumberWithGet(accountNumber, isProEnabled);
            }

            return user;
        }

        public static UserPo AddSubscriptionToUserByAccountNumberWithGet(string accountNumber, bool isSubscriber)
        {
            Logger.Info(String.Format("Adding subscription flag to user account with account number '{0}': {1}", accountNumber, isSubscriber));

            UserPo user = null;

            using (var userRepository = new UserRepository())
            {
                user = userRepository.AddSubscriptionToUserByAccountNumberWithGet(accountNumber, isSubscriber);
            }

            return user;
        }

        public static List<SubscriberDomain> GetUserDomainsForSubscriberByUserId(int userId)
        {
            Logger.Info(String.Format("Retrieving list of registered domains for subscriber with user id={0}", userId));

            List<SubscriberDomain> domains = null;

            using (var userRepository = new UserRepository())
            {
                domains = userRepository.GetUserDomainsForSubscriberByUserId(userId);
            }

            return domains;
        }

        public static List<SubscriberDomain> AddSubscriberDomainToUserByUserId(string userDomain, int userId)
        {
            Logger.Info(String.Format("Adding user domain '{0}' to subscriber with user id={1}", userDomain, userId));

            List<SubscriberDomain> domains = null;

            using (var userRepository = new UserRepository())
            {
                domains = userRepository.AddSubscriberDomainToUserByUserId(userDomain, userId);
            }

            return domains;
        }

        public static List<SubscriberDomain> DeleteUserDomainByUserId(string userDomain, int userId)
        {
            Logger.Info(String.Format("Removing user domain '{0}' from subscriber with user id={1}", userDomain, userId));

            List<SubscriberDomain> domains = null;

            using (var userRepository = new UserRepository())
            {
                domains = userRepository.DeleteUserDomainByUserId(userDomain, userId);
            }

            return domains;
        }

        public static List<SubscriberDomain> DeleteSubscriberDomainByIdAndUserId(int domainId, int userId)
        {
            Logger.Info(String.Format("Removing user domain id='{0}' from subscriber with user id={1}", domainId, userId));

            List<SubscriberDomain> domains = null;

            using (var userRepository = new UserRepository())
            {
                domains = userRepository.DeleteUserDomainByIdAndUserId(domainId, userId);
            }

            return domains;
        }
      public static bool DeleteUserDomainByChannelIdAndUserId(int channelId, int userId)
        {
            Logger.Info(String.Format("Removing user domain id='{0}' from subscriber with user id={1}", channelId, userId));

            bool isDeleted = false;

            using (var userRepository = new UserRepository())
            {
                isDeleted = userRepository.DeleteUserDomainByChannelIdAndUserId(channelId, userId);
            }

            return isDeleted;
        }

        public static List<UserMailingListPo> GetUsersNotRegisteredWithMailChimp()
        {
            List<UserMailingListPo> users = new List<UserMailingListPo>();

            using (var userRepository = new UserRepository())
            {
                users = userRepository.GetAllUsersNotRegisteredWithMailChimp();
            }

            return users;
        }

        public static List<UserMailingListPo> GetUsersRegisteredWithMailChimpForUpdate()
        {
            List<UserMailingListPo> users = new List<UserMailingListPo>();

            using (var userRepository = new UserRepository())
            {
                users = userRepository.GetUsersRegisteredWithMailChimpForUpdate();
                users = users.Where(x => !x.UnsubscribeFromAllEmail).ToList();
            }

            return users;
        }

        public static List<UserEmailOpoutGroup> GetUserEmailOptoutGroupsByUserId(int userId)
        {
            List<UserEmailOpoutGroup> groups = null;

            using (var userRepository = new UserRepository())
            {
                groups = userRepository.GetUserEmailOptoutGroupsByUserId(userId);
            }

            return groups;
        }

        public static bool MarkUserRegisteredWithMailChimp(int userId, string mailchimpId)
        {
            bool isSuccess = false;

            using (var userRepository = new UserRepository())
            {
                isSuccess = userRepository.MarkUserRegisteredWithMailChimp(userId, mailchimpId);
            }

            return isSuccess;
        }

        public static bool MarksUsersAsRegisteredWithMailChimp(List<UserPo> users)
        {
            return true;
        }

        internal static bool UpdateUserRegistrationWithMailChimp(int userId)
        {
            bool isSuccess = false;

            using (var userRepository = new UserRepository())
            {
                isSuccess = userRepository.UpdateUserRegistrationWithMailChimp(userId);
            }

            return isSuccess;
        }

        public static bool UpdateUserEmailPreferences(int userId, bool uGreetings, bool uReminders, bool uSocial, bool uNews, bool uMarketing)
        {
            bool isSuccess = false;

            using (var userRepository = new UserRepository())
            {
                isSuccess = userRepository.UpdateUserOptoutGroupsByUserId(userId, uGreetings, uReminders, uSocial, uNews, uMarketing);
            }

            return isSuccess;
        }

        public static bool AddBusinessContactRequest(BusinessContactRequestCriteria request, Uri templateUrl)
        {
            bool isSuccess = false;

            using (var userRepository = new UserRepository())
            {
                isSuccess = userRepository.InsertBusinessContactRequest(request);

                if (isSuccess)
                {
                    EmailManage.SendBusinessRequestNotification(request, templateUrl);
                }
            }

            return isSuccess;
        }

        public static bool AddUserInterests(int userId, string interests)
        {
            bool isSuccess = false;

            using (var userRepository = new UserRepository())
            {
                isSuccess = userRepository.AddUserInterests(userId, interests);
            }

            return isSuccess;
        }
    }
}


