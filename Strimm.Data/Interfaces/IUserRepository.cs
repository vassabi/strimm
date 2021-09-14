using Strimm.Model;
using Strimm.Model.Projections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Data.Interfaces
{
    public interface IUserRepository
    {
        List<User> GetAllUsers();

        List<UserPo> GetAllUserPos();
        
        bool InsertUser(string userName, string publicUrl, string accountNumber, string userIp, string password,
                        string email, string firstName, string lastName, DateTime birthDate, string countryName, string gender, bool isExternalUser, string city, string state, string zipcode, bool isFilmMaker);

        UserPo InsertUserWithGet(string userName, string publicUrl, string accountNumber, string userIp, string password,
                                 string email, string firstName, string lastName, DateTime birthDate, string countryName, string gender, bool isExternalUser, bool isFilmMaker);

        User GetUserByEmail(string email);

        User GetUserById(int userId);

        User GetUserByUserName(string userName);

        User GetUserByPublicUrl(string publicUrl);

        User GetUserByAccountNumber(string accountNumber);

        User GetUserByChannelName(string channelName);

        User GetUserByEmailAndIp(string email, string ip);

        UserPo GetUserPoByLoginIdentifierAndPassword(string loginIdentifier, string pass);

        UserPo GetUserPoByLoginIdentifier(string loginIdentifier);

        UserPo GetUserPoByUserId(int userId);

        bool DeleteUserById(int userId);

        bool UpdateUser(User user);

        bool UpdateUser(UserPo user);

        UserProfile GetUserProfileByUserId(int userId);

      //  bool InsertUserFollower(UserFollower follower);

        UserFollower GetUserFollowerByUserIdAndFollowerUserId(int userId, int followerUserId);

        List<UserFollower> GetAllFollowersByUserId(int userId);

        int GetUserCountByCountry(string countryName);

        int GetUserCountByState(string stateName);

        bool DeleteUserFollowerByFollowerUserIdAndUserId(int followerUserId, int userId);

        List<UserPo> GetUsersByUserNameKeywords(List<string> keywords);

        List<UserPo> GetUsersByBoardTitleKeywords(List<string> keywords);

        UserPo GetUserPoByEmail(string email);

        UserPo GetUserPoByUserName(string userName);

        UserPo GetUserPoByPublicUrl(string publicUrl);

        UserPo GetUserPoByChannelTubeName(string channelTubeName);

        UserPo GetUserPoByAccountNumber(string accountNumber);

        bool ConfirmUserByUserId(int userId);

        UserMembership GetUserMembershipByUserId(int userId);

        bool UpdateUserMembership(UserMembership userMembership);

        bool LockUnlockUserByAccountNumber(string accountNumber, bool isLocked);

        int GetCountOfBoardVisitorsByUserId(int userId);

        bool UpdateActivationEmailSendDateByUserName(string userName);

        void UpdateUserLastLoginDateByUserId(int userId);

        bool UpdateUserProfile(UserProfile userProfile);

        int GetUserIdByLoginIdentifierAndPassword(string loginIdentifier, string password);

        Guid CreateAuthTokenForUserById(int userId);

        bool ValidateAuthToken(Guid authToken);

        void SignOut(Guid authToken);

        List<UserPo> GetUserPoByKeywords(List<string> keywords);

        bool UpdateUserProfile(UserPo user);

        bool UpdateUserPassword(int userId, DateTime clientDateTime, string newPassword);

        UserPo GetDeletedUser(string email);

        List<UserPo> GetAllUserPosForAdmin();

        UserPo GetUserPoByUserIdForAdmin(int userId);

        bool UpdateUserLastKnownLocationByUsername(UserLocation location, string username);

        UserPo EnableProForUserByAccountNumberWithGet(string accountNumber, bool isProEnabled);

        UserPo AddSubscriptionToUserByAccountNumberWithGet(string accountNumber, bool isSubscriber);

        List<SubscriberDomain> GetUserDomainsForSubscriberByUserId(int userId);

        List<SubscriberDomain> AddSubscriberDomainToUserByUserId(string userDomain, int userId);

        List<SubscriberDomain> DeleteUserDomainByUserId(string userDomain, int userId);

        List<SubscriberDomain> DeleteUserDomainByIdAndUserId(int domainId, int userId);
        bool DeleteUserDomainByChannelIdAndUserId(int channelId, int userId);

        List<UserMailingListPo> GetAllUsersNotRegisteredWithMailChimp();

        List<UserEmailOpoutGroup> GetUserEmailOptoutGroupsByUserId(int userId);

        bool MarkUserRegisteredWithMailChimp(int userId, string mailchimpId);

        bool UpdateUserRegistrationWithMailChimp(int userId);

        bool AddUserInterests(int userId, string interests);
    }
}
