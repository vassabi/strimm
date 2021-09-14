using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Strimm.Model;
using Strimm.Model.Projections;
using Strimm.Model.WebModel;
using Strimm.Model.Criteria;

namespace Strimm.Data.Interfaces
{
  public interface IVideoTubeRepository
    {
      bool InsertVideoTube(string title, string description, string providerVideoId, long duration, int categoryId, int videoProviderId, bool isRRated, bool isInPublicLibrary, bool isPrivate);

      VideoTube InsertVideoTubeWithGet(string title, string description, string providerVideoId, long duration, int categoryId, int videoProviderId, bool isRRated,
            bool isInPublicLibrary, bool isPrivate, string thumbnail, bool PrivateVideoModeEnabled);

      bool BulkInsertVideoTubeListIntoChannelTubeById(int channelTubeId, List<int> videoTubeIds, bool addToVideoRoom);

      bool BulkInsertVideoTubeListIntoVideoRoomTubeById(int videoRoomTubeId, List<int> videoTubeIds);

      bool InsertUserVideoTubeViewByUserIdAndVideoTubeId(int videoTubeId, int? userId, DateTime? viewStartTime, DateTime? viewEndTime);

      VideoTube GetVideoTubeById(int videoTubeId);

      bool DeleteVideoTubeById(int videoTubeId);

      bool RemoveVideoTubeFromPublicLib(int videoTubeId);

      bool UpdateVideoTube(VideoTube tube);

      List<VideoTubePo> GetAllPublicVideoTubes();

      VideoTube GetVideoTubeByProviderVideoId(string providerVideoId);

      List<VideoTubePo> GetPublicVideoTubesByVideoProviderId(int videoProviderId);

      List<VideoTubePo> GetPublicVideoTubesByVideoProviderName(string videoProviderName);

      List<VideoTubePo> GetVideoTubesByTitleKeywords(List<String> keywordList);

      List<VideoTubePo> GetVideoTubesByVideoRoomTubeId(int videoRoomTubeId);

      List<VideoTubePo> GetVideoTubesByVideoRoomTubeIdAndCategoryId(int videoRoomTubeId, int categoryId);

      List<VideoTubePo> GetAllVideoTubeByChannelTubeId(int channelTubeId);

      bool AddVideoTubeToUserArchive(int userId, int videoTubeId, DateTime clientTime);

      VideoTube GetVideoTubeFromArchiveByUserIdAndVideoTubeId(int userId, int videoTubeId);

      List<VideoTubePo> GetArchivedUserVideoTubesByUserId(int userId);

      bool AddVideoTubeToChannelTube(int channelTubeId, int videoTubeId);

      bool DeleteVideoTubeFromChannelTubeById(int channelTubeId, int videoTubeId);

      bool DeleteAllVideoTubesFromChannelTubeByChannelTubeId(int channelTubeId);

      bool DeleteAllVideoTubesFromVideoRoomTubeByVideoRoomTubeId(int videoRoomTubeId);

      List<VideoTubeCounterEntity> GetVideoTubeCountsByCategory();

      List<VideoTubeCounterEntity> GetVideoTubeCountsByCategoryAndVideoRoomTubeId(int videoRoomTubeId);

      List<VideoTubeCounterEntity> GetVideoTubeCountsByCategoryAndChannelTubeId(int channelTubeId);

      List<VideoTubePo> GetAllVideoTubeByProviderName(string providerName);

      VideoTube GetVideoTubeByProviderVideoIdAndVideoRoomTubeId(string providerVideoId, int videoRoomTubeId);

      VideoTube GetVideoTubeByIdAndVideoRoomTubeId(int videoTubeId, int videoRoomTubeId);

      bool DeleteVideoTubeFromUserArchiveByVideoTubeIdAndUserId(int videoTubeId, int userId);

      bool AddVideoTubeToVideoRoomTube(int videoRoomTubeId, int videoTubeId);

      bool DeleteVideoTubeFromVideoRoomByVideoTubeIdAndUserId(int videoTubeId, int userId, bool isAdmin = false);

      List<VideoTubePo> GetAllVideoTubeByVideoRoomTubeId(int videoRoomTubeId);

        List<VideoTubePo> GetPublicVideoTubesByPageIndex(int pageIndex, string keywords, out int pageCount, int pageSize = 10);

      VideoTubePo GetVideoTubePoById(int videoTubeId);

      List<VideoTubePo> GetVideoTubePosFromVideoRoomTubeByUserId(int userId);

        List<VideoTubeModel> GetVideoTubePoByChannelTubeIdAndCategoryIdAndPageIndex(int channelTubeId, int categoryId, int pageIndex, string keywords, out int pageCount, int pageSize = 10);

        List<VideoTubeModel> GetVideoTubePoByChannelTubeIdAndPageIndex(int channelTubeId, int pageIndex, string keywords, out int pageCount, int pageSize = 10);

        List<VideoTubePo> GetPublicVideoTubesByCategoryIdAndPageIndex(int categoryId, int pageIndex, string keywords, out int pageCount, int pageSize = 10);

        List<VideoTubePo> GetVideoTubePosFromVideoRoomTubeByUserIdAndCategoryIdAndPageIndex(int userId, int categoryId, int pageIndex, bool retrieveMyVideos, bool retrieveLicensedVideos, bool retrieveExternalVideos, string keywords, out int pageCount, int pageSize = 0);

        List<VideoTubePo> GetVideoTubePosFromVideoRoomTubeByUserIdAndPageIndex(int userId, int pageIndex, bool retrieveMyVideos, bool retrieveLicensedVideos, bool retrieveExternalVideos, string keywords, out int pageCount, int pageSize = 0);

      bool DeleteRestrictedOrRemovedVideosFromChannelTubeByChannelTubeId(int channelId, List<int> videoIds);

      bool RemoveAllVideosFromChannelTubeByChannelTubeId(int channelId);

      List<VideoSchedule> AddVideoTubeToChannelScheduleById(int channelScheduleId, int videoTubeId);

      List<VideoSchedule> DeleteVideoTubeFromChannelScheduleById(int channelScheduleId, int videoTubeId);

      int MoveUserPrivateVideosToPublicLibraryByUsername(string userName);

      int DeleteMultipleVideosFromPublicLibrary(List<int> videoTubeIds);

      bool DeleteArchivedVideoByVideotubeIdAndUserId(int userId, int videoTubeId);

        List<VideoTubeModel> GetVideoTubeByKeywordAndChannelId(List<string> keywords, int channelTubeId);

        List<VideoTubeModel> GetVideoTubeByKeywordForPublicLibrary(List<string> keywords);

        bool UpdateVideoTubeLastScheduleDateTimeByChannelScheduleId(int channelScheduleId);

      CustomVideoTubeUploadPo InitializeVideoTubeUploadForUser(int userId, string fileName, string videoTubeStagingKey, float duration, DateTime clientTime);

      CustomVideoTubeUploadPo GetCustomVideoTubeById(int videoTubeId);

      List<VideoTubePo> GetUserUploadedVideoTubesByCategoryIdAndPageIndex(VideoStoreVideoSearchCriteria searchCriteria, out int pageCount, int pageSize);

      List<VideoTubePo> GetUserUploadedVideoTubesByPageIndex(VideoStoreVideoSearchCriteria searchCriteria, out int pageCount, int pageSize);

      List<VideoTubeModel> GetVideoTubesByUserId(int userId);
              
      List<VideoTubeModel> RetrievePageOfVideosToUpdate();

      bool UpdateVideoTubeStatusById(int videoTubeId, bool isPrivate, bool isRestrictedByProvider, bool isRemovedByProvider, bool isRRated);

      VideoTubeModel GetPrivateVideoByProviderIdAndChannelId(string providerId, int channelTubeId);
    }
}
