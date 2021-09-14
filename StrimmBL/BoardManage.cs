using log4net;
using Strimm.Data.Repositories;
using Strimm.Model.Projections;
using Strimm.Shared;
using System;

namespace StrimmBL
{
    public static class BoardManage
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(BoardManage));

        public static bool IsMyBoard(int userId, string userUrl)
        {
            Logger.Debug(String.Format("Checking if board belongs to a user using UserId={0} and UserUrl={1}", userId, userUrl));

            bool isMyBoard = false;

            using (var repository = new UserRepository())
            {
                var user = repository.GetUserById(userId);
                isMyBoard = user != null && user.UserName == userUrl.ToString();
            }

            return isMyBoard;
        }

        public static bool IsUserBoardExist(string userName)
        {
            Logger.Debug(String.Format("Checking if the user board exists for user = '{0}'", userName));

            bool exists = false;

            using (var repository = new UserRepository())
            {
                var user = repository.GetUserByPublicUrl(PublicNameUtils.GetUrl(userName));
                exists = user != null && user.UserId > 0;
            }

            return exists;
        }

        public static int GetCountOfBoardVisitorsByUserId(int userId)
        {
            Logger.Debug(String.Format("Retrieving count of board visitors for user Id = '{0}'", userId));

            int count = 0;

            using (var repository = new UserRepository())
            {
                count = repository.GetCountOfBoardVisitorsByUserId(userId);
            }

            return count;
        }

        public static UserBoard GetUserBoardDataByUserName(String userName, DateTime clientTime)
        {
            Logger.Debug(String.Format("Retrieving Board view data for user with name={0} at '{1}'", userName, clientTime));

            UserBoard userBoard = null;

            using (var boardRepository = new BoardRepository())
            {
                userBoard = boardRepository.GetUserBoardDataByUserName(userName, clientTime);
                userBoard.ProfileImageUrl = ImageUtils.GetProfileImageUrl(userBoard.ProfileImageUrl);
                userBoard.BackgroundImageUrl = ImageUtils.GetBackgroundImageUrl(userBoard.BackgroundImageUrl);
            }

            return userBoard;
        }

        public static UserBoard GetUserBoardDataByPublicUrl(String publicUrl, DateTime clientTime)
        {
            Logger.Debug(String.Format("Retrieving Board view data for user with public url={0} at '{1}'", publicUrl, clientTime));

            UserBoard userBoard = null;

            using (var boardRepository = new BoardRepository())
            {
                userBoard = boardRepository.GetUserBoardDataByPublicUrl(publicUrl, clientTime);
                if(userBoard != null)
                {
                    userBoard.ProfileImageUrl = ImageUtils.GetProfileImageUrl(userBoard.ProfileImageUrl);
                    userBoard.BackgroundImageUrl = ImageUtils.GetBackgroundImageUrl(userBoard.BackgroundImageUrl);
                }
            }

            return userBoard;
        }

        public static UserBoard UpdateUserBoard(int userId, string boardName, string profileImageUrl, string backgroundImageUrl, string userStory)
        {
            Logger.Info(String.Format("Updating userboard for user with Id={0}", userId));

            UserBoard userBoard = null;

            using (var boardRepository = new BoardRepository())
            using (var userRepository = new UserRepository())
            {

                var userProfile = userRepository.GetUserProfileByUserId(userId);

                if (userProfile != null)
                {
                    userProfile.BoardName = boardName;
                    userProfile.BackgroundImageUrl = backgroundImageUrl;
                    userProfile.ProfileImageUrl = profileImageUrl;
                    userProfile.UserStory = userStory;

                    if (userRepository.UpdateUserProfile(userProfile))
                    {
                        Logger.Debug(String.Format("Successfully update user profile for user with Id={0}", userId));
                    }
                    else
                    {
                        Logger.Debug(String.Format("Failed to update user profile for user with Id={0}", userId));
                    }

                    userBoard = boardRepository.GetUserBoardDataByUserId(userId);
                    userBoard.ProfileImageUrl = ImageUtils.GetProfileImageUrl(userBoard.ProfileImageUrl);
                    userBoard.BackgroundImageUrl = ImageUtils.GetBackgroundImageUrl(userBoard.BackgroundImageUrl);
                }
            }

            return userBoard;
        }


    }
}
