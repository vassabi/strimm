using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Strimm.Data.Repositories;
using Strimm.Shared;
using Strimm.Model;

namespace Strimm.Data.Tests
{
    [TestClass]
    public class UserBoardTests
    {
        private UserRepository userRepository = new UserRepository();
        private BoardRepository boardRepository = new BoardRepository();

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

        private User user;

        public UserBoardTests()
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

            userRepository.InsertUser(userName, PublicNameUtils.GetUrl(userName), accountNumber, userIp, password, email, firstName, lastName, birthDate, countryName, gender, isExternalUser, city, state, zipcode, isFilmMaker);
            user = userRepository.GetUserByEmail(email);
        }

        [TestMethod]
        public void GetUserBoadDataByUserIdTest()
        {
            Assert.IsNotNull(user, "Unable to proceed. User is invalid");

            var userBoard = this.boardRepository.GetUserBoardDataByUserId(user.UserId);

            Assert.IsNotNull(userBoard, String.Format("Failed to retrieve userboard for user with Id={0}", user.UserId));
        }

        [TestMethod]
        public void GetUserBoardDataByUserNameTest()
        {
            Assert.IsNotNull(user, "Unable to proceed. User is invalid");

            var userBoard = this.boardRepository.GetUserBoardDataByUserName("valula", DateTime.Now);

            Assert.IsNotNull(userBoard, String.Format("Failed to retrieve userboard for user with Id={0}", user.UserId));
        }

        [TestMethod]
        public void DeleteUserByIdTest()
        {
            userRepository.DeleteUserById(user.UserId);
            user = userRepository.GetUserById(user.UserId);

            Assert.IsNull(user, "User was not deleted");

            userRepository = null;
            boardRepository = null;
        }

    }
}
