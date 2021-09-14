using Strimm.Model;
using Strimm.Model.Projections;
using Strimm.Model.WebModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Data.Interfaces
{
    public interface IChannelTubeRepository
    {
        List<ChannelTube> GetAllChannelTubes();

        List<ChannelTube> GetAllFeaturedChannelTubes();

        List<ChannelTubePo> GetChannelTubePosByUserId(int userId);
        List<ChannelTube> GetChannelTubeByUserIdForAdmin(int userId);

        ChannelTubePo GetChannelTubePoById(int channelTubeId);

        List<ChannelTube> GetChannelTubesByTitleKeywords(List<String> keywords);

        ChannelTube GetChannelTubeById(int channelTubeId);

        ChannelTube GetChannelTubeByName(string name);

        ChannelTube GetChannelTubeByUrl(string url);

        ChannelTube GetChannelTubeByChannelScheduleId(int channelScheduleId);

        List<ChannelTube> GetChannelTubesSubscribedByUserByUserId(int userId);

        List<ChannelTube> GetChannelTubesByCategoryId(int categoryId);

        List<ChannelTube> GetHighlySubscribedChannelTubes(int rowCount);

        //bool InsertChannelTube(ChannelTube channelTube);

        bool InsertChannelTube(int categoryId, string name, string description, string pictureUrl, string url, int userId);

        ChannelTube InsertChannelTubeWithGet(int categoryId, int languageId, string name, string description, string pictureUrl, string url, int userId, bool isWhiteLabeled, string channelPassword, bool embedEnabled, bool muteOnStartup, string customLabel, string subscriberDomain, bool embedOnlyMode, bool matureContentEnabled, bool showPlayerControls, bool isPrivate, bool isLogoModeActive, string channelLogoUrl, bool playLiveFirst, bool keepGuideOpen=false);
        bool InsertChannnelPassword(int userId, int channelTubeId, string channelPassword);

        bool InsertSubscriberDomain(int userId, int channelTubeId, string domainName);

        bool UpdateSubscribtionDomain(int userId, int channelTubeId, string userDomain);

        bool UpdateChannelTube(ChannelTube channelTube);

        bool InsertChannelSubscriptionByUserIdAndChannelTubeId(int userId, int channelTubeId, DateTime subscriptionStartDate);

        bool DeleteChannelTubeById(int channelTubeId);

        bool DeleteChannelSubscriptionById(int channelSubscriptionId);

        List<ChannelTubePo> GetChannelTubePoByKeywords(List<string> keywords, DateTime clientTime);

        List<ChannelTubePo> GetAllChannelTubePosOnAutoPilot();

        List<ChannelTubePo> GetCurrentlyPlayingTopChannels(DateTime clientTime);

        List<ChannelTubePo> GetCurrentlyPlayingFavoriteChannelsForUserByUserId(int userId, DateTime clientTime);

        bool InsertUserChannelTubeViewByUserIdAndChannelTubeId(int channelTubeId, int? userId, DateTime viewTime);

        bool DeleteChannelSubscriptionByUserIdAndChannelTubeId(int userId, int channelTubeId, DateTime clientTime);

        List<ChannelTubePo> GetAllFavoriteChannelsForUserByUserIdAndClientTime(int userId, DateTime clientTime);

        List<ChannelTubePo> GetAllChannelsForUserByUserIdAndClientTime(int userId, DateTime date);

        List<UserChannelTubeView> GetChannelViewByUserIdChannelIdAndViewTime(int channelTubeId, int? userId, DateTime viewTime);

        List<ChannelTubePo> GetCurrentlyPlayingChannelsByCategoryName(DateTime clientTime, string categoryName);

        List<ChannelTubePo> GetCurrentlyPlayingChannelsByCategoryNameAndLanguageId(DateTime clientTime, int languageId);

        List<ChannelTubePo> GetChannelsByUserIdAndClientTime(int userId, DateTime clientTime);

        List<ChannelTubePo> GetCurrentlyPlayingChannels(DateTime clientTime);

        bool SetChannelRatingByUserIdAndChannelTubeId(int userId, int channelTubeId, float ratingValue, DateTime enteredDate);

        float GetUserRatingByUserIdAndChannelTubeId(int userId, int channelTubeId);

        List<ChannelTubePo> GetCurrentlyPlayingChannelsByUserNameAndClientTime(string userName, DateTime dateAndTime);

        List<ChannelTubePo> GetCurrentlyPlayingChannelsByPublicUrlAndClientTime(string publicUrl, DateTime dateAndTime);

        List<ChannelTubePo> GetCurrentlyPlayingChannelsForLandingPage(string userName, string channelUrls, DateTime dateAndTime);

        List<ChannelTubePo> GetChannelTubePosByUserIdForAdmin(int userId);

        List<ChannelTubePo> GetCurrentlyPlayingChannelsByUserId(DateTime clientTime, int userId);

        ChannelTubePageModel GetCurrentlyPlayingChannelsSortedByPageIndex(DateTime clientTime, string sortBy, int? languageId, int pageIndex, int pageSize);

        UserChannelEntitlements GetUserChannelEntitlementsByUser(int userId);
    }
}
