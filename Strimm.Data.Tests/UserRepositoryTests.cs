using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Strimm.Data.Repositories;
using Strimm.Shared;
using System.Collections.Generic;

namespace Strimm.Data.Tests
{
    [TestClass]
    public class UserRepositoryTests
    {
        private UserRepository userRepository = new UserRepository();
        
        private string accountNumber;
        private string userName;
        private string email;
        private string firstName;
        private string lastName;
        private string userIp;
        private string password;
        private DateTime birthDate;
        private string countryName;
        private string gender;
        private string boardName;
        private string backgroundImageUrl;
        private string profileUrl;
        private string userStory;
        private bool isExternalUser;
        private string city;
        private string state;
        private string zipcode;
        private bool isFilmMaker;

        public UserRepositoryTests()
        {
            accountNumber = AccountUtils.GenerateAccountNumber();
            userName = "testUser";
            email = "info@strimm.com";
            firstName = "Test1";
            lastName = "Test2";
            userIp = "::1";
            password = CryptoUtils.HashPassword(CryptoUtils.GetPasswordWithSalt(email, "password"));
            birthDate = DateTime.Now.AddYears(-13);
            gender = "Male";
            countryName = "United States of America";
            boardName = "Test Board";
            backgroundImageUrl = "http://backgroundurl.com";
            profileUrl = "http://profileurl.com";
            userStory = "Test story for test userboard";
            city = "Buffalo Grove";
            state = "IL";
            zipcode = "60063";
            isFilmMaker = false;
        }

        [TestInitialize]
        public void Init()
        {
            // This method will be called before every test
        }

        [TestMethod]
        public void InsertUserTest()
        {
            string account = AccountUtils.GenerateAccountNumber();

            userRepository.InsertUser(userName, PublicNameUtils.GetUrl(userName), accountNumber, userIp, password, email, firstName, lastName, birthDate, countryName, gender, isExternalUser, city, state, zipcode, isFilmMaker);

            var user = userRepository.GetUserByUserName(userName);

            Assert.IsNotNull(user, String.Format("Unable to retrieve user with userName={0}", userName));
            Assert.AreEqual(user.UserName, userName, "User names are not the same");
        }

        [TestMethod]
        public void GetAuthenticationTokenByUserIdTest()
        {
            var user = userRepository.GetUserByUserName(userName);

            Assert.IsNotNull(user, String.Format("Unable to retrieve user with userName={0}", userName));

            Guid authToken = userRepository.CreateAuthTokenForUserById(user.UserId);

            Assert.IsNotNull(authToken, "AuthToken was not generated for user");
            Assert.AreNotEqual(Guid.Empty, authToken, "AuthToken should not be empty");
        }

        [TestMethod]
        public void ValidateAuthTokenTest()
        {
            var user = userRepository.GetUserByUserName(userName);

            Assert.IsNotNull(user, String.Format("Unable to retrieve user with userName={0}", userName));

            Guid authToken = userRepository.CreateAuthTokenForUserById(user.UserId);

            Assert.IsNotNull(authToken, "AuthToken was not generated for user");
            Assert.AreNotEqual(Guid.Empty, authToken, "AuthToken should not be empty");

            bool isValidToken = userRepository.ValidateAuthToken(authToken);

            Assert.IsTrue(isValidToken, "Authentication token is not valid");
        }

        [TestMethod]
        public void SignOutByAuthTokenTest()
        {
            var user = userRepository.GetUserByUserName(userName);

            Assert.IsNotNull(user, String.Format("Unable to retrieve user with userName={0}", userName));

            Guid authToken = userRepository.CreateAuthTokenForUserById(user.UserId);

            Assert.IsNotNull(authToken, "AuthToken was not generated for user");
            Assert.AreNotEqual(Guid.Empty, authToken, "AuthToken should not be empty");

            userRepository.SignOut(authToken);

            bool isValidToken = userRepository.ValidateAuthToken(authToken);

            Assert.IsFalse(isValidToken, "Authentication token is still valid. Signout failed");
        }

        [TestMethod]
        public void GetUserPoByLoginIdentifierAndPasswordTest()
        {
            var userPo = userRepository.GetUserPoByLoginIdentifierAndPassword(email, password);

            Assert.IsNotNull(userPo, String.Format("Failed to retrieve user with e-mail '{0}'", email));
            Assert.AreEqual(userPo.Email, email, String.Format("Retrieved user's email '{0}', does not match test user e-mail '{1}'", userPo.Email, email));
            Assert.IsTrue(userPo.UserId > 0, String.Format("Invalid user id corresponds to user with e-mail '{0}'", email));
        }

        [TestMethod]
        public void UpdateUserLastLoginDateByUserIdTest()
        {
            var user = userRepository.GetUserByEmail(email);

            Assert.IsNotNull(user, String.Format("Unable to retrieve user using e-mail '{0}'", email));

            userRepository.UpdateUserLastLoginDateByUserId(user.UserId);

            var userMembership = userRepository.GetUserMembershipByUserId(user.UserId);

            Assert.IsNotNull(userMembership, String.Format("Failed to retrieve user membership by Id '{0}'", user.UserId));
            Assert.AreEqual(userMembership.LastLoginDate.Value.Date, DateTime.Now.Date, "Invalid last login specified date");
        }

        [TestMethod]
        public void GetUserProfileByUserIdTest()
        {
            var user = userRepository.GetUserByEmail(email);

            Assert.IsNotNull(user, String.Format("Unable to retrieve user using e-mail '{0}'", email));

            var userProfile = userRepository.GetUserProfileByUserId(user.UserId);

            Assert.IsNotNull(userProfile, String.Format("Failed to retrieve user profile for user with Id={0}", user.UserId));
            Assert.IsTrue(userProfile.UserProfileId > 0, String.Format("User profile Id '{0}' is invalid for user profile retrieved for user with Id={1}", userProfile.UserProfileId, user.UserId));
        }

        [TestMethod]
        public void UpdateUserProfileTest()
        {
            var user = userRepository.GetUserByEmail(email);

            Assert.IsNotNull(user, String.Format("Unable to retrieve user using e-mail '{0}'", email));

            var userProfile = userRepository.GetUserProfileByUserId(user.UserId);

            Assert.IsNotNull(userProfile, String.Format("Failed to retrieve user profile for user with Id={0}", user.UserId));
            Assert.IsTrue(userProfile.UserProfileId > 0, String.Format("User profile Id '{0}' is invalid for user profile retrieved for user with Id={1}", userProfile.UserProfileId, user.UserId));

            userProfile.BoardName = this.boardName;
            userProfile.BackgroundImageUrl = this.backgroundImageUrl;
            userProfile.@ProfileImageUrl = this.profileUrl;
            userProfile.UserStory = this.userStory;

            bool isSuccess = userRepository.UpdateUserProfile(userProfile);

            Assert.IsTrue(isSuccess, "Unable to update user profile");

            userProfile = userRepository.GetUserProfileByUserId(user.UserId);

            Assert.AreEqual(boardName, userProfile.BoardName);
            Assert.AreEqual(backgroundImageUrl, userProfile.BackgroundImageUrl);
            Assert.AreEqual(profileUrl, userProfile.@ProfileImageUrl);
            Assert.AreEqual(userStory, userProfile.UserStory);
        }

        [TestMethod]
        public void GetUserIdByLoginIdentifierAndPasswordTest()
        {
            int userId = userRepository.GetUserIdByLoginIdentifierAndPassword(userName, password);
            Assert.IsTrue(userId > 0, "Unable to retrieve userId for user. Login failed");
        }

        [TestMethod]
        public void DeleteUserByIdTest()
        {
            var user = userRepository.GetUserByEmail(email);
            if (user != null)
            {
                userRepository.DeleteUserById(user.UserId);
            }

            user = userRepository.GetUserById(user.UserId);

            Assert.IsNull(user, "User was not deleted");
        }

        [TestMethod]
        public void GetUserPoByKeywordsTest()
        {
            var users = userRepository.GetUserPoByKeywords(new List<string>() { "Max", "Val" });

            Assert.IsNotNull(users, "Failed to retrieve users by specified keywords");
            Assert.IsTrue(users.Count > 0, "Failed to retrieve users. Collection is empty");
        }

        [TestCleanup]
        public void TearDown()
        {
            // This method will be called after every test
        }
    }
}
