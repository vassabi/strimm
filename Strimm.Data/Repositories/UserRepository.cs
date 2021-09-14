using Strimm.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Strimm.Model;
using System.Data;
using System.Diagnostics.Contracts;
using log4net;
using Strimm.Model.Projections;
using Dapper;

namespace Strimm.Data.Repositories
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(UserRepository));

        public UserRepository()
            : base()
        {
        }
        
        public List<User> GetAllUsers()
        {
            List<User> users = null;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    users = this.StrimmDbConnection.Query<User>("strimm.GetAllUsers", null, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to retrieve all users", ex);
            }

            return users;
        }

        public List<UserPo> GetAllUserPos()
        {
            List<UserPo> users = null;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    users = this.StrimmDbConnection.Query<UserPo>("strimm.GetAllUserPos", null, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to retrieve all users", ex);
            }

            return users;
        }

        public bool InsertUser(string userName, string publicUrl, string accountNumber, string userIp, string password, 
                               string email, string firstName, string lastName, DateTime birthDate, 
                               string countryName, string gender, bool isExternalUser,
                               string city, string state, string zipcode, bool isFilmMaker)
        {
            Contract.Requires(!String.IsNullOrEmpty(userName), "Username was not specified");
            Contract.Requires(!String.IsNullOrEmpty(accountNumber), "Account number was not specified");
            Contract.Requires(!String.IsNullOrEmpty(userIp), "User IP was not specified");
            Contract.Requires(!String.IsNullOrEmpty(password), "Password was not specified");
            Contract.Requires(!String.IsNullOrEmpty(email), "Email was not specified");
            Contract.Requires(!String.IsNullOrEmpty(firstName), "First name was not specified");
            Contract.Requires(!String.IsNullOrEmpty(lastName), "Last name was not specified");
            Contract.Requires(birthDate > DateTime.MinValue && birthDate <= DateTime.MaxValue, "Birth date is missing or invalid");
            Contract.Requires(!String.IsNullOrEmpty(countryName), "Country name was not specified");
            Contract.Requires(!String.IsNullOrEmpty(gender), "Gender was not specified");

            bool isSuccess = false;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    int rowcount = this.StrimmDbConnection.Execute("strimm.InsertUser", new { 
                                                                                              UserName =  userName, 
                                                                                              AccountNumber = accountNumber,
                                                                                              UserIp = userIp,
                                                                                              Password = password,
                                                                                              Email = email,
                                                                                              FirstName = firstName,
                                                                                              LastName = lastName,
                                                                                              BirthDate = birthDate,
                                                                                              Country = countryName,
                                                                                              Gender = gender,
                                                                                              IsExternalUser = isExternalUser,
                                                                                              PublicUrl = publicUrl,
                                                                                              City = city,
                                                                                              StateOrProvince = state,
                                                                                              ZipCode = zipcode,
                                                                                              IsFilmMaker = isFilmMaker
                                                                                        }, null, 30, commandType: CommandType.StoredProcedure);
                    isSuccess = rowcount > 0;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to insert new user with username '{0}'", userName), ex);
            }

            return isSuccess;
        }

        public UserPo InsertUserWithGet(string userName, string publicUrl, string accountNumber, string userIp, string password,
                       string email, string firstName, string lastName, DateTime birthDate, string countryName, string gender, bool isExternalUser, bool isFilmMaker)
        {
            Contract.Requires(!String.IsNullOrEmpty(userName), "Username was not specified");
            Contract.Requires(!String.IsNullOrEmpty(publicUrl), "PublicUrl was not specified");
            Contract.Requires(!String.IsNullOrEmpty(accountNumber), "Account number was not specified");
            Contract.Requires(!String.IsNullOrEmpty(userIp), "User IP was not specified");
            Contract.Requires(!String.IsNullOrEmpty(password), "Password was not specified");
            Contract.Requires(!String.IsNullOrEmpty(email), "Email was not specified");
            Contract.Requires(!String.IsNullOrEmpty(firstName), "First name was not specified");
            Contract.Requires(!String.IsNullOrEmpty(lastName), "Last name was not specified");
            Contract.Requires(birthDate > DateTime.MinValue && birthDate <= DateTime.MaxValue, "Birth date is missing or invalid");
            Contract.Requires(!String.IsNullOrEmpty(countryName), "Country name was not specified");
            Contract.Requires(!String.IsNullOrEmpty(gender), "Gender was not specified");

            UserPo user = null;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    var results = this.StrimmDbConnection.Query<UserPo>("strimm.InsertUserWithGet", new
                    {
                        UserName = userName,
                        AccountNumber = accountNumber,
                        UserIp = userIp,
                        Password = password,
                        Email = email,
                        FirstName = firstName,
                        LastName = lastName,
                        BirthDate = birthDate,
                        Country = countryName,
                        Gender = gender,
                        IsExternalUser = isExternalUser,
                        PublicUrl = publicUrl,
                        IsFilmMaker = isFilmMaker
                    }, null, false, 30, commandType: CommandType.StoredProcedure);
                    user = results.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to insert new user with username '{0}'", userName), ex);
            }

            return user;
        }

        public User GetUserByEmail(string email)
        {
            Contract.Requires(!String.IsNullOrEmpty(email), "Email is either invalid or missing");

            User user = null;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    var results = this.StrimmDbConnection.Query<User>("strimm.GetUserByEmail", new { Email = email }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                    user = results.Count > 0 ? results.FirstOrDefault() : null;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve user using e-mail '{0}'", email), ex);
            }

            return user;
        }

        public UserPo GetDeletedUser(string email)
        {
            Contract.Requires(!String.IsNullOrEmpty(email), "Email is either invalid or missing");

            UserPo user = null;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    var results = this.StrimmDbConnection.Query<UserPo>("strimm.GetDeletedUser", new { Email = email }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                    user = results.Count > 0 ? results.FirstOrDefault() : null;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve user using e-mail '{0}'", email), ex);
            }

            return user;
        }
        public User GetUserById(int userId)
        {
            Contract.Requires(userId > 0, "UserId should be greater then 0");

            User user = null;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    var results = this.StrimmDbConnection.Query<User>("strimm.GetUserById", new { UserId = userId }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                    user = results.Count > 0 ? results.FirstOrDefault() : null;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve user by id = {0}", userId), ex);
            }

            return user;
        }

        public User GetUserByUserName(string userName)
        {
            Contract.Requires(!String.IsNullOrEmpty(userName), "Username is invalid or missing");

            User user = null;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    var results = this.StrimmDbConnection.Query<User>("strimm.GetUserByUserName", new { UserName = userName }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                    user = results.Count > 0 ? results.FirstOrDefault() : null;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve user by username '{0}'", userName), ex);
            }

            return user;
        }

        public User GetUserByPublicUrl(string publicUrl)
        {
            Contract.Requires(!String.IsNullOrEmpty(publicUrl), "PublicName URL is invalid or missing");

            User user = null;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    var results = this.StrimmDbConnection.Query<User>("strimm.GetUserByPublicUrl", new { PublicUrl = publicUrl }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                    user = results.Count > 0 ? results.FirstOrDefault() : null;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve user by publicUrl '{0}'", publicUrl), ex);
            }

            return user;
        }

        public User GetUserByAccountNumber(string accountNumber)
        {
            Contract.Requires(!String.IsNullOrEmpty(accountNumber), "Account number is invalid or missing");

            User user = null;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    var results = this.StrimmDbConnection.Query<User>("strimm.GetUserByAccountNumber", new { AccountNumber = accountNumber }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                    user = results.Count > 0 ? results.FirstOrDefault() : null;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve user by account number '{0}'", accountNumber), ex);
            }

            return user;
        }

        public User GetUserByChannelName(string channelName)
        {
            Contract.Requires(!String.IsNullOrEmpty(channelName), "ChannelName is invalid or missing");

            User user = null;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    var results = this.StrimmDbConnection.Query<User>("strimm.GetUserByChannelName", new { ChannelName = channelName }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                    user = results.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve user by channelName = '{0}'", channelName), ex);
            }

            return user;
        }

        public User GetUserByEmailAndIp(string email, string ip)
        {
            Contract.Requires(!String.IsNullOrEmpty(email), "Email is invalid or missing");
            Contract.Requires(!String.IsNullOrEmpty(ip), "IP Address is invalid or missing");

            User user = null;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    var results = this.StrimmDbConnection.Query<User>("strimm.GetUserByEmailAndIp", new { Email = email, Ip = ip }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                    user = results.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve user by email '{0}' and Ip address '{1}'", email, ip), ex);
            }

            return user;
        }

        public UserPo GetUserPoByLoginIdentifierAndPassword(string loginIdentifier, string pass)
        {
            Contract.Requires(!String.IsNullOrEmpty(loginIdentifier), "Login identifier is invalid or missing");
            Contract.Requires(!String.IsNullOrEmpty(pass), "Password is invalid or missing");

            UserPo user = null;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    var results = this.StrimmDbConnection.Query<UserPo>("strimm.GetUserPoByLoginIdentifierAndPassword", new { LoginIdentifier = loginIdentifier, Password = pass }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                    user = results.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve user by login idetifier = '{0}' and password = '{1}'", loginIdentifier, pass), ex);
            }

            return user;
        }

        public UserPo GetUserPoByLoginIdentifier(string loginIdentifier)
        {
            Contract.Requires(!String.IsNullOrEmpty(loginIdentifier), "Login identifier is invalid or missing");

            UserPo user = null;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    var results = this.StrimmDbConnection.Query<UserPo>("strimm.GetUserPoByLoginIdentifier", new { LoginIdentifier = loginIdentifier }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                    user = results.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve user by login idetifier = '{0}'", loginIdentifier), ex);
            }

            return user;
        }

        public bool DeleteUserById(int userId)
        {
            Contract.Requires(userId > 0, "UserId should be greater then 0");

            bool isSuccess = false;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    int rowCount = this.StrimmDbConnection.Execute("strimm.DeleteUserById", new { UserId = userId }, null, 30, commandType: CommandType.StoredProcedure);
                    isSuccess = rowCount == 1;
                }
            }
            catch(Exception ex)
            {
                Logger.Error(String.Format("Failed to delete user with Id = '{0}'", userId), ex);
            }

            return isSuccess;
        }

        public bool UpdateUser(User user)
        {
            Contract.Requires(user != null, "Invalid or missing user specified");
            Contract.Requires(user.UserId > 0, "New user specified. Update aborted");

            bool isSuccess = false;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    int rowcount = this.StrimmDbConnection.Execute("strimm.UpdateUser", user, null, 30, commandType: CommandType.StoredProcedure);
                    isSuccess = rowcount == 1;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to update user with username '{0}', UserId = {1}", user.UserName, user.UserId), ex);
            }

            return isSuccess;
        }

        public bool UpdateUser(UserPo user)
        {
            Contract.Requires(user != null, "Invalid or missing user specified");
            Contract.Requires(user.UserId > 0, "New user specified. Update aborted");

            bool isSuccess = false;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    int rowcount = this.StrimmDbConnection.Execute("strimm.UpdateUserPo", user, null, 30, commandType: CommandType.StoredProcedure);
                    isSuccess = rowcount == 1;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to update user with username '{0}', UserId = {1}", user.UserName, user.UserId), ex);
            }

            return isSuccess;
        }

        public UserProfile GetUserProfileByUserId(int userId)
        {
            Contract.Requires(userId > 0, "UserId should be greater then 0");

            UserProfile userProfile = null;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    var results = this.StrimmDbConnection.Query<UserProfile>("strimm.GetUserProfileByUserId", new { UserId = userId }, null, false, 30, commandType: CommandType.StoredProcedure);
                    userProfile = results.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to get user profile by userId = {0}", userId), ex);
            }

            return userProfile;
        }

        public bool InsertUserFollower(int userId, int followerUserId, DateTime startFollowDate)
        {
            Contract.Requires(userId > 0, "User follower is mising or undefined");
            Contract.Requires(followerUserId > 0, "Unable to insert existing record");
            Contract.Requires(startFollowDate >= DateTime.MinValue && startFollowDate <= DateTime.MaxValue, "StartFollowDate is invalid");

            bool isSuccess = false;

            try
            {
                int rowcount = this.StrimmDbConnection.Execute("strimm.InsertUserFollower", new { UserId = userId, FollowerUserId = followerUserId, StartedFollowDate = startFollowDate }, null, 30, commandType: CommandType.StoredProcedure);
                isSuccess = rowcount == 1;
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to create new user follower record, follower user id={0}, user id = {1}", followerUserId, userId), ex);
            }

            return isSuccess;
        }

        public UserFollower GetUserFollowerByUserIdAndFollowerUserId(int userId, int followerUserId)
        {
            Contract.Requires(userId > 0, "UserId should be greater then 0");
            Contract.Requires(followerUserId > 0, "FollerUserId should be greater then 0");

            UserFollower userFollower = null;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    var results = this.StrimmDbConnection.Query<UserFollower>("strimm.GetFollowerByUserIdAndFollowerId", new { UserId = userId, FollowerUserId = followerUserId }, null, false, 30, commandType: CommandType.StoredProcedure);
                    userFollower = results.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve UserFollower by UserId={0} and FollowerUserId={1}", userId, followerUserId), ex);
            }

            return userFollower;
        }

        public List<UserFollower> GetAllFollowersByUserId(int userId)
        {
            Contract.Requires(userId > 0, "UserId should be greater then 0");

            var userFollowerList = new List<UserFollower>();

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    userFollowerList = this.StrimmDbConnection.Query<UserFollower>("strimm.GetAllFollowersByUserId", new { UserId = userId }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve all user followers by user Id = {0}", userId), ex);
            }

            return userFollowerList;

        }

        public int GetUserCountByCountry(string countryName)
        {
            Contract.Requires(!String.IsNullOrEmpty(countryName), "Invalid or missing country name specified");

            int count = 0;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    count = this.StrimmDbConnection.ExecuteScalar<int>("strimm.GetUserCountByCountry", new { CountryName = countryName }, null, 30, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve user count for county '{0}'", countryName), ex);
            }

            return count;
        }

        public int GetUserCountByState(string stateName)
        {
            Contract.Requires(!String.IsNullOrEmpty(stateName), "Invalid or missing state name specified");

            int userCount = 0;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    userCount = this.StrimmDbConnection.ExecuteScalar<int>("strimm.GetUserCountByState", new { StateName = stateName }, null, 30, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve users for state '{0}'", stateName), ex);
            }

            return userCount;
        }

        public int GetCountOfBoardVisitorsByUserId(int userId)
        {
            Contract.Requires(userId > 0, "UserId should be greater then 0");

            int userCount = 0;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    userCount = this.StrimmDbConnection.ExecuteScalar<int>("strimm.GetCountOfBoardVisitorsByUserId", new { UserId = userId }, null, 30, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve count of visitors for board associated with user Id='{0}'", userId), ex);
            }

            return userCount;
        }

        public bool DeleteUserFollowerByFollowerUserIdAndUserId(int followerUserId, int userId)
        {
            Contract.Requires(followerUserId > 0, "FollowerUserId should be greater then 0");
            Contract.Requires(userId > 0, "UserId should be greater then 0");

            bool isSuccess = false;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    int rowcount = this.StrimmDbConnection.Execute("strimm.DeleteUserFollowerByFollowerUserIdAndUserId", new { FollowerUserId = followerUserId, UserId = userId }, null, 30, commandType: CommandType.StoredProcedure);
                    isSuccess = rowcount == 1;
                }
            }
            catch(Exception ex)
            {
                Logger.Error(String.Format("Failed to delete UserFollower record by follower user Id = {0} and User Id = {1}", followerUserId, userId), ex);
            }

            return isSuccess;
        }

        public List<UserPo> GetUsersByUserNameKeywords(List<string> keywords)
        {
            Contract.Requires(keywords != null && keywords.Count > 0, "Keywords list is empty");

            List<UserPo> users = null;
            var buffer = new StringBuilder();

            keywords.ForEach(x => buffer.Append(x).Append(','));

            String keywordString = buffer.ToString().TrimEnd(',');

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    users = this.StrimmDbConnection.Query<UserPo>("strimm.GetUserPosByUserNameKeywords", new { Keywords = keywords }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch(Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve users by username keywords"), ex);
            }

            return users;
        }

        public List<UserPo> GetUsersByBoardTitleKeywords(List<string> keywords)
        {
            Contract.Requires(keywords != null && keywords.Count > 0, "Keywords list is empty");

            List<UserPo> users = null;
            var buffer = new StringBuilder();

            keywords.ForEach(x => buffer.Append(x).Append(','));

            String keywordString = buffer.ToString().TrimEnd(',');

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    users = this.StrimmDbConnection.Query<UserPo>("strimm.GetUserPosByBoardTitleKeywords", new { Keywords = keywords }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve users by board name keywords"), ex);
            }

            return users;
        }

        public UserPo GetUserPoByEmail(string email)
        {
            Contract.Requires(!String.IsNullOrEmpty(email), "Email is invalid or missing");
           
            UserPo userPo = null;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    var results = this.StrimmDbConnection.Query<UserPo>("strimm.GetUserPoByEmail", new { Email = email }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                    userPo = results.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve user by email '{0}'", email), ex);
            }

            return userPo;
        }

        public UserPo GetUserPoByUserId(int userId)
        {
            Contract.Requires(userId > 0, "UserId should be greater then 0");

            UserPo userPo = null;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    var results = this.StrimmDbConnection.Query<UserPo>("strimm.GetUserPoByUserId", new { UserId = userId }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                    userPo = results.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve user by Id={0}", userId), ex);
            }

            return userPo;
        }
        public UserPo GetUserPoByUserIdForAdmin(int userId)
        {
            Contract.Requires(userId > 0, "UserId should be greater then 0");

            UserPo userPo = null;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    var results = this.StrimmDbConnection.Query<UserPo>("strimm.GetUserPoByUserIdForAdmin", new { UserId = userId }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                    userPo = results.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve user by Id={0}", userId), ex);
            }

            return userPo;
        }

        public UserPo GetUserPoByAccountNumber(string accountNumber)
        {
            Contract.Requires(!String.IsNullOrEmpty(accountNumber), "AccountNumber is invalid or missing");
           

            UserPo userPo = null;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    var results = this.StrimmDbConnection.Query<UserPo>("strimm.GetUserPoByAccountNumber", new { AccountNumber = accountNumber }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                    userPo = results.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve user by account number '{0}'", accountNumber), ex);
            }

            return userPo;
        }

        public UserPo GetUserPoByUserName(string userName)
        {
            Contract.Requires(!String.IsNullOrEmpty(userName), "UserName is invalid or missing");


            UserPo userPo = null;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    var results = this.StrimmDbConnection.Query<UserPo>("strimm.GetUserPoByUserName", new { UserName = userName }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                    userPo = results.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve user by userNumber '{0}'", userName), ex);
            }

            return userPo;
        }

        public UserPo GetUserPoByPublicUrl(string publicUrl)
        {
            Contract.Requires(!String.IsNullOrEmpty(publicUrl), "PublicUrl is invalid or missing");


            UserPo userPo = null;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    var results = this.StrimmDbConnection.Query<UserPo>("strimm.GetUserPoByPublicUrl", new { PublicUrl = publicUrl }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                    userPo = results.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve user by publicUrl '{0}'", publicUrl), ex);
            }

            return userPo;
        }

        public UserPo GetUserPoByChannelTubeName(string channelTubeName)
        {
            Contract.Requires(!String.IsNullOrEmpty(channelTubeName), "ChannelTubeName is invalid or missing");


            UserPo userPo = null;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    var results = this.StrimmDbConnection.Query<UserPo>("strimm.GetUserPoByChannelTubeName", new { ChannelTubeName = channelTubeName }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                    userPo = results.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve user by ChannelTubeName '{0}'", channelTubeName), ex);
            }

            return userPo;
        }

        public bool ConfirmUserByUserId(int userId)
        {
            Contract.Requires(userId > 0, "UserId should be greater then 0");

            bool isSuccess = false;
            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    int rowcount = this.StrimmDbConnection.Execute("strimm.ConfirmUserByUserId", new { UserId = userId }, null, 30, commandType: CommandType.StoredProcedure);
                    isSuccess = rowcount == 1;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to confirm user by userId='{0}'", userId), ex);
            }

            return isSuccess;
        }     

        public UserMembership GetUserMembershipByUserId(int userId)
        {
            Contract.Requires(userId > 0, "UserId should be greater then 0");


            UserMembership userMembership = null;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    var results = this.StrimmDbConnection.Query<UserMembership>("strimm.GetUserMembershipByUserId", new { UserId = userId }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                    userMembership = results.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve user membership by userId '{0}'", userId), ex);
            }

            return userMembership;
        }

        public bool UpdateUserMembership(UserMembership userMembership)
        {
            Contract.Requires(userMembership != null, "Invalid or missing user membership specified");
            Contract.Requires(userMembership.UserMembershipId > 0, "New user membership Id specified. Update aborted");

            bool isSuccess = false;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    int rowcount = this.StrimmDbConnection.Execute("strimm.UpdateUserMembership", userMembership, null, 30, commandType: CommandType.StoredProcedure);
                    isSuccess = rowcount == 1;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to update user membership with Id='{0}'", userMembership.UserId), ex);
            }

            return isSuccess;
        }

        public bool LockUnlockUserByAccountNumber(string accountNumber, bool isLocked)
        {
            Contract.Requires(!String.IsNullOrEmpty(accountNumber), "Account number is invalid or missing");

            bool isSuccess = false;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    int rowcount = this.StrimmDbConnection.Execute("strimm.LockUnlockUserByAccountNumber", new { AccountNumber = accountNumber, IsLocked = isLocked }, null, 30, commandType: CommandType.StoredProcedure);
                    isSuccess = rowcount == 1;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to {0} user with Account Number = '{1}'", isLocked ? "Lock" : "Unlock", accountNumber), ex);
            }

            return isSuccess;
        }

        public bool UpdateActivationEmailSendDateByUserName(string userName)
        {
            Contract.Requires(!String.IsNullOrEmpty(userName), "Username is invalid or missing");

            bool isSuccess = false;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    int rowcount = this.StrimmDbConnection.Execute("strimm.UpdateActivationEmailSendDateByUserName", new { UserName = userName, ActivationEmailSendDate = DateTime.Now }, null, 30, commandType: CommandType.StoredProcedure);
                    isSuccess = rowcount == 1;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to update activation e-mail send date for user {0}", userName), ex);
            }

            return isSuccess;
        }


        public void UpdateUserLastLoginDateByUserId(int userId)
        {
            Contract.Requires(userId > 0, "UserId should be greater then 0");

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    this.StrimmDbConnection.Execute("strimm.UpdateUserLastLoginDateByUserId", new { UserId = userId }, null, 30, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to set last login date for user with Id = '{0}'", userId), ex);
            }
        }

        public bool UpdateUserProfile(UserProfile user)
        {
            Contract.Requires(user != null, "User profile is invalid");

            bool isSuccess = false;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    int rowcount = this.StrimmDbConnection.Execute("strimm.UpdateUserProfile", new
                    {
                        UserId = user.UserId,
                        UserProfileId = user.UserProfileId,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        BirthDate = user.BirthDate,
                        Address = user.Address,
                        City = user.City,
                        StateOrProvince = user.StateOrProvince,
                        Country = user.Country,
                        ZipCode = user.ZipCode,
                        Gender = user.Gender,
                        UserStory = user.UserStory,
                        Company = user.Company,
                        TermsAndConditionsAcceptanceDate = DateTime.Now,
                        ProfileImageUrl = user.ProfileImageUrl,
                        UserIp = user.UserIp,
                        PhoneNumber = user.PhoneNumber,
                        BoardName = user.BoardName,
                        BackgroundImageUrl = user.BackgroundImageUrl,
                        PrivateVideoModeEnabled = user.PrivateVideoModeEnabled,
                        MatureContentAllowed = user.MatureContentAllowed,
                        StripeCustomerId = user.StripeCustomerId
                    }, null, 30, commandType: CommandType.StoredProcedure);
                    isSuccess = rowcount == 1;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to update user profile for user with Id={0}", user.UserId), ex);
            }

            return isSuccess;
        }

        public int GetUserIdByLoginIdentifierAndPassword(string loginIdentifier, string password)
        {
            Contract.Requires(!String.IsNullOrEmpty(loginIdentifier), "Login identifier is invalid or missing");
            Contract.Requires(!String.IsNullOrEmpty(password), "Password is invalid or missing");

            int userId = 0;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    var dynamicParameter = new DynamicParameters();
                    dynamicParameter.Add("@LoginIdentifier", loginIdentifier);
                    dynamicParameter.Add("@Password", password);
                    dynamicParameter.Add("@UserId", null, DbType.Int32, ParameterDirection.Output);

                    this.StrimmDbConnection.Execute("strimm.GetUserIdByLoginIdentifierAndPassword", dynamicParameter, null, 30, commandType: CommandType.StoredProcedure);
                    var dynamicParameterUserId = dynamicParameter.Get<Int32?>("@UserId");
                    if (dynamicParameterUserId != null)
                    {
                        userId = dynamicParameter.Get<Int32>("@UserId");
                    }
                    
                    
                   
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve user by login idetifier = '{0}' and password = '{1}'", loginIdentifier, password), ex);
            }
            

            return userId;
        }


        public Guid CreateAuthTokenForUserById(int userId)
        {
            Contract.Requires(userId > 0, "UserId should be greater then 0");

            Guid authorizationToken = Guid.Empty;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    var dynamicParameter = new DynamicParameters();
                    dynamicParameter.Add("@UserId", userId);
                    dynamicParameter.Add("@AuthToken", null, DbType.Guid, ParameterDirection.Output);

                    this.StrimmDbConnection.Execute("strimm.GenerateAuthTokenForUserById", dynamicParameter, null, 30, commandType: CommandType.StoredProcedure);

                    authorizationToken = dynamicParameter.Get<Guid>("@AuthToken");
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve authorization token for user with Id = '{0}'", userId), ex);
            }

            return authorizationToken;
        }

        public bool ValidateAuthToken(Guid authToken)
        {
            Contract.Requires(authToken != null, "Invalid authentication token specified");

            bool isValid = false;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    var dynamicParameter = new DynamicParameters();
                    dynamicParameter.Add("@AuthToken", authToken);
                    dynamicParameter.Add("@IsValid", null, DbType.Boolean, ParameterDirection.Output);

                    this.StrimmDbConnection.Execute("strimm.ValidateAuthToken", dynamicParameter, null, 30, commandType: CommandType.StoredProcedure);

                    isValid = dynamicParameter.Get<bool>("@IsValid");
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to validate authorization token '{0}'", authToken), ex);
            }

            return isValid;
        }

        public void SignOut(Guid authToken)
        {
            Contract.Requires(authToken != null, "Invalid authentication token specified");

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    this.StrimmDbConnection.Execute("strimm.SignOutByAuthToken", new { AuthToken = authToken }, null, 30, commandType: CommandType.StoredProcedure);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to signout user with authorization token '{0}'", authToken), ex);
            }
        }

        public List<UserPo> GetUserPoByKeywords(List<string> keywords)
        {
            Logger.Info("Retrieving UserPos by keywords");

            Contract.Requires(keywords != null && keywords.Count > 0, "No keywords specified to perform the search");

            if (keywords == null || keywords.Count == 0)
            {
                throw new ArgumentException("No keywords specified to perform the search");
            }

            var users = new List<UserPo>();
            var builder = new StringBuilder();

            keywords.ForEach(x =>
            {
                builder.Append(x).Append(",");
            });

            string keywordsString = builder.ToString().TrimEnd(',');

            try
            {

                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    users = this.StrimmDbConnection.Query<UserPo>("strimm.GetUserPosByKeywords", new { Keywords = keywordsString }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve UserPo by keywords='{0}'", keywordsString), ex);
            }

            return users;
        }


        public bool UpdateUserProfile(UserPo user)
        {
            Contract.Requires(user != null, "Invalid or missing user specified");
            Contract.Requires(user.UserId > 0, "New user specified. Update aborted");

            bool isSuccess = false;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    int rowcount = this.StrimmDbConnection.Execute("strimm.UpdateUserProfile", new { UserId = user.UserId, UserProfileId=user.UserProfileId, FirstName=user.FirstName,
                                                                                                     LastName = user.LastName,
                                                                                                     BirthDate = user.BirthDate,
                                                                                                     Address = user.Address,
                                                                                                     City=user.City,
                                                                                                     StateOrProvince=user.StateOrProvince,
                                                                                                     Country=user.Country,
                                                                                                     ZipCode=user.ZipCode,
                                                                                                     Gender=user.Gender,
                                                                                                     UserStory=user.UserStory,
                                                                                                     Company = user.Company,
                                                                                                     TermsAndConditionsAcceptanceDate=DateTime.Now,
                                                                                                     ProfileImageUrl = user.ProfileImageUrl,
                                                                                                     UserIp=user.UserIp,
                                                                                                     PhoneNumber=user.PhoneNumber,
                                                                                                     BoardName=user.BoardName,
                                                                                                     BackgroundImageUrl=user.BackgroundImageUrl,
                                                                                                     PrivateVideoModeEnabled = user.PrivateVideoModeEnabled,
                                                                                                     MatureContentAllowed = user.MatureContentAllowed,
                                                                                                     StripeCustomerId = user.StripeCustomerId
                    }, null, 30, commandType: CommandType.StoredProcedure);
                    isSuccess = rowcount == 1;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to update user with username '{0}', UserId = {1}", user.UserName, user.UserId), ex);
            }

            return isSuccess;
        }

        public bool UpdateUserPassword(int userId, DateTime clientDateTime, string newPassword)
        {
            Contract.Requires(userId != 0, "Invalid userId specified");

            bool isSuccess = false;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {


                   int rowcount =  this.StrimmDbConnection.Execute("strimm.UpdateUserPassword", new { UserId = userId, ClientDateTime=clientDateTime, NewPassword=newPassword }, null, 30, commandType: CommandType.StoredProcedure);

                    isSuccess = rowcount == 1;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to update password with userId '{0}'", userId), ex);
            }

            return isSuccess;
        }

        public List<UserPo> GetAllUserPosForAdmin()
        {
            List<UserPo> users = null;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    users = this.StrimmDbConnection.Query<UserPo>("strimm.GetAllUserPosForAdmin", null, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to retrieve all users", ex);
            }

            return users;
        }

        public bool UpdateUserLastKnownLocationByUsername(UserLocation location, string username)
        {
            Logger.Info(String.Format("Updating last know location of user '{0}'", username));

            bool isSuccess = false;
            bool isSignup = false;
            if (location.isSignUp == true)
            {
                isSignup = true;
            }
            try
            {
                if (location != null)
                {
                   
                    if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                    {
                        
                        
                        int rowcount = this.StrimmDbConnection.Execute("strimm.UpdateUserLastKnownLocationByUsername", new
                        {

                            Username = username,
                            UserIp = location.UserIp,
                            Country = location.Country,
                            State = location.State,
                            City = location.City,
                            PostalCode = location.PostalCode,
                            IsSignUp = isSignup
                        }, null, 30, commandType: CommandType.StoredProcedure);

                        isSuccess = rowcount == 1;
                    }
                }
                else
                {
                    Logger.Warn(String.Format("Invalid user location was specified for user with username '{0}'. User location was not updated!", username));
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to update last know location of user '{0}'", username), ex);
            }

            return isSuccess;
        }

        public UserPo EnableProForUserByAccountNumberWithGet(string accountNumber, bool isProEnabled)
        {
            Contract.Requires(!String.IsNullOrEmpty(accountNumber), "Invalid user account number specified");

            UserPo user = null;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    var response = this.StrimmDbConnection.Query<UserPo>("strimm.EnableProForUserByAccountNumberWithGet", new { AccountNumber = accountNumber, IsProEnabled = isProEnabled }, null, false, 30, commandType: CommandType.StoredProcedure);
                    user = response.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to {0} pro for user with account number '{1}'", isProEnabled ? "enable" : "disable", accountNumber), ex);
            }

            return user;
        }

        public UserPo AddSubscriptionToUserByAccountNumberWithGet(string accountNumber, bool isSubscriber)
        {
            Contract.Requires(!String.IsNullOrEmpty(accountNumber), "Invalid user account number specified");

            UserPo user = null;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    var response = this.StrimmDbConnection.Query<UserPo>("strimm.AddSubscriptionToUserByAccountNumberWithGet", new { AccountNumber = accountNumber, IsSubscriber = isSubscriber }, null, false, 30, commandType: CommandType.StoredProcedure);
                    user = response.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to {0} subscription for user with account number '{1}'", isSubscriber ? "add" : "remove", accountNumber), ex);
            }

            return user;
        }

        public List<SubscriberDomain> GetUserDomainsForSubscriberByUserId(int userId)
        {
            Contract.Requires(userId != 0, "Invalid user id specified");

            List<SubscriberDomain> domains = null;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    domains = this.StrimmDbConnection.Query<SubscriberDomain>("strimm.GetUserDomainsForSubscriberByUserId", new { UserId = userId }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve user domains for user with id={0}", userId), ex);
            }

            return domains;
        }

        public List<SubscriberDomain> AddSubscriberDomainToUserByUserId(string userDomain, int userId)
        {
            Contract.Requires(userId != 0, "Invalid user id specified");
            Contract.Requires(!String.IsNullOrEmpty(userDomain), "Invalid or empty user domain specified");

            List<SubscriberDomain> domains = null;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    if (!String.IsNullOrEmpty(userDomain))
                    {
                        domains = this.StrimmDbConnection.Query<SubscriberDomain>("strimm.AddSubscriberDomainToUserByUserId", new { UserId = userId, UserDomain = userDomain }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                    }
                    else
                    {
                        domains = this.GetUserDomainsForSubscriberByUserId(userId);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to add user domain '{0}' to for user with id={1}", userDomain, userId), ex);
            }

            return domains;
        }

        public List<SubscriberDomain> DeleteUserDomainByUserId(string userDomain, int userId)
        {
            Contract.Requires(userId != 0, "Invalid user id specified");
            Contract.Requires(!String.IsNullOrEmpty(userDomain), "Invalid or empty user domain specified");

            List<SubscriberDomain> domains = null;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    if (!String.IsNullOrEmpty(userDomain))
                    {
                        domains = this.StrimmDbConnection.Query<SubscriberDomain>("strimm.DeleteUserDomainByUserId", new { UserId = userId, UserDomain = userDomain }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                    }
                    else
                    {
                        domains = this.GetUserDomainsForSubscriberByUserId(userId);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to delete user domain '{0}' to for user with id={1}", userDomain, userId), ex);
            }

            return domains;
        }


        public List<SubscriberDomain> DeleteUserDomainByIdAndUserId(int domainId, int userId)
        {
            Contract.Requires(userId != 0, "Invalid user id specified");
            Contract.Requires(domainId != 0, "Invlid domain id specified");

            List<SubscriberDomain> domains = null;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    if (domainId > 0)
                    {
                        domains = this.StrimmDbConnection.Query<SubscriberDomain>("strimm.DeleteUserDomainByIdAndUserId", new { UserId = userId, SubscriberDomainId = domainId }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                    }
                    else
                    {
                        domains = this.GetUserDomainsForSubscriberByUserId(userId);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to delete user domain id={0} to for user with id={1}", domainId, userId), ex);
            }

            return domains;
        }
        public bool DeleteUserDomainByChannelIdAndUserId(int channelId, int userId)
        {
            Contract.Requires(userId != 0, "Invalid user id specified");
            Contract.Requires(channelId != 0, "Invlid domain id specified");

           
            bool isSuccess = false;
            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    if (channelId > 0)
                    {
                        
                        int rowCount = this.StrimmDbConnection.Execute("strimm.DeleteUserDomainByChannelIdAndUserId", new { UserId = userId, ChannelTubeId = channelId }, null, 30, commandType: CommandType.StoredProcedure);
                        isSuccess = rowCount > 0;
                    }
                   
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to delete user domain id={0} to for user with id={1}", channelId, userId), ex);
            }

            return isSuccess;
        }

        public List<UserMailingListPo> GetAllUsersNotRegisteredWithMailChimp()
        {
            List<UserMailingListPo> users = null;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    users = this.StrimmDbConnection.Query<UserMailingListPo>("strimm.GetAllUsersNotRegisteredWithMailChimp", null, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error occured while retrieving all users who has not yet being registered with MailChimp", ex);
            }

            return users;
        }

        public List<UserEmailOpoutGroup> GetUserEmailOptoutGroupsByUserId(int userId)
        {
            Contract.Requires(userId != 0, "Invalid user id specified");

            List<UserEmailOpoutGroup> groups = null;

            try 
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    groups = this.StrimmDbConnection.Query<UserEmailOpoutGroup>("strimm.GetUserOptoutGroupsByUserId", new { UserId = userId }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Error occured while retrieving email opt out groups for user with id={0}",userId), ex);
            }

            return groups;
        }

        public bool MarkUserRegisteredWithMailChimp(int userId, string mailchimpId)
        {
            Contract.Requires(userId != 0, "Invalid user id specified");

            bool isSuccess = false;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    int rowcount = this.StrimmDbConnection.Execute("strimm.MarkUserAsRegisteredWithMailChimp", new { UserId = userId, MailChimpEmailId = mailchimpId }, null, 30, commandType: CommandType.StoredProcedure);
                    isSuccess = rowcount > 0;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Error occured while marking user as registered with MailChimp, user id={0}", userId), ex);
            }

            return isSuccess;
        }

        public bool UpdateUserRegistrationWithMailChimp(int userId)
        {
            Contract.Requires(userId != 0, "Invalid user id specified");

            bool isSuccess = false;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    int rowcount = this.StrimmDbConnection.Execute("strimm.UpdateUserRegistrationWithMailChimp", new { UserId = userId }, null, 30, commandType: CommandType.StoredProcedure);
                    isSuccess = rowcount > 0;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Error occured while updating MailChimp user registration record, user id={0}", userId), ex);
            }

            return isSuccess;
        }

        public List<UserMailingListPo> GetUsersRegisteredWithMailChimpForUpdate()
        {           
            List<UserMailingListPo> users = null;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    users = this.StrimmDbConnection.Query<UserMailingListPo>("strimm.GetAllUsersRegisteredWithMailChimpForUpdate", null, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error occured while retrieving users registered with MailChimp for update", ex);
            }
      
            return users;
        }

        public bool UpdateUserOptoutGroupsByUserId(int userId, bool uGreetings, bool uReminders, bool uSocial, bool uNews, bool uMarketing)
        {
            Contract.Requires(userId != 0, "Invalid user id specified");

            bool isSuccess = false;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    int rowcount = this.StrimmDbConnection.Execute("strimm.UpdateUserOptoutGroupsByUserId", new { 
                                                                        UserId = userId,
                                                                        UnsubscribeFromGreetings = !uGreetings,
                                                                        UnsubscribeFromReminders = !uReminders,
                                                                        UnsubscribeFromSocial = !uSocial,
                                                                        UnsubscribeFromNews = !uNews,
                                                                        UnsubscribeFromMarketing = !uMarketing
                                                                    }, 
                                                                    null, 30, commandType: CommandType.StoredProcedure);
                    isSuccess = rowcount > 0;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Error occured while updating user's email communication preferences, user id={0}", userId), ex);
            }

            return isSuccess;
        }

        public bool InsertBusinessContactRequest(Model.Criteria.BusinessContactRequestCriteria request)
        {
            Contract.Requires(request != null, "Invalid contact information specified");

            bool isSuccess = false;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    int rowcount = this.StrimmDbConnection.Execute("strimm.InsertBusinessContactRequest", new
                    {
                        Name = request.Name,
                        Company = request.Company,
                        WebsiteUrl = request.WebsiteUrl,
                        Email = request.Email,
                        PhoneNumber = request.PhoneNumber,
                        PackageType = request.PackageType,
                        Comments = request.Comments
                    },
                    null, 30, commandType: CommandType.StoredProcedure);
                    isSuccess = rowcount > 0;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Error occured while adding business company request for {0}", request.Name), ex);
            }

            return isSuccess;
        }

        public bool AddUserInterests(int userId, string interests)
        {
            Contract.Requires(userId > 0, "Invalid user id specified");
            Contract.Requires(!String.IsNullOrEmpty(interests), "Interests were not specified");

            bool isSuccess = false;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    int rowcount = this.StrimmDbConnection.Execute("strimm.AddUserInterests", new
                    {
                        UserId = userId,
                        UserInterests = interests
                    },
                    null, 30, commandType: CommandType.StoredProcedure);
                    isSuccess = rowcount > 0;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Error occured while adding user interests '{0}' for user with id={1}", interests, userId), ex);
            }

            return isSuccess;
        }
    }
}
