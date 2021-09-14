using log4net;
using Strimm.Data.Repositories;
using Strimm.Model;
using Strimm.Model.Projections;
using Strimm.Model.Criteria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Strimm.Model.WebModel;
using Strimm.Shared;

namespace StrimmBL
{
   public static class SearchManage
    {
       internal class SearchedVideoScheduleData:VideoScheduleModel
       {
           string channelOwnerUserName, channelName;
       }
       private static readonly ILog Logger = LogManager.GetLogger(typeof(SearchManage));

       public static List<VideoTubePo> GetSearchedVideos(List<string> keywords)
       {
           Logger.Info("Searching videoTubes by keywords that appear in their titles");
           var list = new List<VideoTubePo>();
           using (var videTubeRepository = new VideoTubeRepository())
           {
               list = videTubeRepository.GetVideoTubesByTitleKeywords(keywords);
           }

           return list;
       }

       public static List<ChannelTube> GetSearchedChannelList(List<string> keywords)
       {
           Logger.Info("Searching for channel tubes using keywords that appear in their names");
           var list = new List<ChannelTube>();
           using ( var channelRepository = new ChannelTubeRepository())
           {
               list = channelRepository.GetChannelTubesByTitleKeywords(keywords);
           }
          
           return list;
       }

       public static List<UserPo> GetUsersByUserName(List<string> keywords)
       {
           Logger.Info("Searching for users based on keywords that appear in their username");

           var userRepository = new UserRepository();
           return userRepository.GetUsersByUserNameKeywords(keywords);
       }

       public static List<UserPo> GetUsersByBoardTitle(List<string> keywords)
       {
           Logger.Info("Searching for users based on keyswords that appear in their board's titla name"); 
           var list = new List<UserPo> ();
           using ( var userRepository = new UserRepository())
           {
               list = userRepository.GetUsersByBoardTitleKeywords(keywords);
           }

           return list;
       }

       public static List<UserModel> GetUsersByKeywords(List<string> keywords)
       {
           Logger.Info("Retrieving users by keywords in their username, first name and last name");

           var userModels = new List<UserModel>();
          
           try
           {
               using (var userRepository = new UserRepository())
               {
                   var users = userRepository.GetUserPoByKeywords(keywords);
                   
                   users.ForEach(x =>
                   {
                       List<ChannelTubePo> channelList = new List<ChannelTubePo>();
                       channelList = ChannelManage.GetChannelTubesForUser(x.UserId);
                       userModels.Add(new UserModel()
                       {
                           AccountNumber = x.AccountNumber,
                           Address = x.Address,
                           BirthDate = x.BirthDate,
                           City = x.City,
                           Company = x.Company,
                           Country = x.Country,
                           Email = x.Email,
                           FirstName = x.FirstName,
                           Gender = x.Gender,
                           IsDeleted = x.IsDeleted,
                           LastName = x.LastName,
                           PhoneNumber = x.PhoneNumber,
                           ProfileImageUrl = x.ProfileImageUrl,
                           StateOrProvince = x.StateOrProvince,
                           UserId = x.UserId,
                           UserName = x.UserName,
                           UserStory = x.UserStory,
                           ZipCode = x.ZipCode,
                           ChannelList =channelList,
                           PublicUrl = x.PublicUrl

                       });
                   });
               }
           }
           catch (Exception ex)
           {
               Logger.Error("Failed to retrieve user models by keywords", ex);
           }

           return userModels;
       }



       public static List<ChannelTubeModel> GetChannelsByKeywords(List<string> keywords, DateTime clientTime)
       {
           Logger.Info("Retrieving channels by keywords");

           var channelModels = new List<ChannelTubeModel>();

           try
           {
               using (var channelTubeRepository = new ChannelTubeRepository())
               {
                   var channels = channelTubeRepository.GetChannelTubePoByKeywords(keywords, clientTime);

                   channels.ForEach(x =>
                   {
                       channelModels.Add(new ChannelTubeModel(x));
                   });
               }
           }
           catch (Exception ex)
           {
               Logger.Error("Failed to retrieve channel tube models by keywords", ex);
           }

           return channelModels;
       }

        public static List<VideoTubeModel> GetVideoTubeByKeywordAndChannelId(List<string> keywords, int channelTubeId)
        {
            Logger.Info("Retrieving videos by keywords");

            var videoModels = new List<VideoTubeModel>();

            try
            {
                using (var videoTubeRepository = new VideoTubeRepository())
                {
                    var videos = videoTubeRepository.GetVideoTubeByKeywordAndChannelId(keywords, channelTubeId);

                    videos.ForEach(x =>
                    {
                        //videoModels.Add(new VideoTubeModel(x));
                        videoModels.Add(x);
                    });
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to retrieve video tube models by keywords", ex);
            }

            return videoModels;
        } 

        public static List<VideoTubeModel> GetVideoTubeByKeywordForPublicLibrary(List<string> keywords, int channelTubeId)
        {
            Logger.Info("Retrieving videos by keywords for public library");

            var videoModels = new List<VideoTubeModel>();

            try
            {
                using (var videoTubeRepository = new VideoTubeRepository())
                {
                    var videosInChannel = videoTubeRepository.GetAllVideoTubeByChannelTubeId(channelTubeId);
                    var videos = videoTubeRepository.GetVideoTubeByKeywordForPublicLibrary(keywords);

                    videos.ForEach(v =>
                    {
                        v.IsInChannel = videosInChannel.Any(x => x.VideoTubeId == v.VideoTubeId);
                        videoModels.Add(v);
                       
                    });
                }

                var videoTubeModels = new List<VideoTubeModel>();
                var vimeoModels = new List<VideoTubeModel>();

                if (videoModels != null && videoModels.Count > 0)
                {
                    videoModels.ForEach(video =>
                    {
                        if (video.VideoProviderId == 2)
                        {
                            vimeoModels.Add(video);
                        }
                        else if (video.VideoProviderId == 1)
                        {
                            videoModels.Add(video);
                        }
                    });
                }
                //MST: 03152017 - Turning off update all status updates because Vimeo started to throttle us. Th
                //these updates will be done with JAS by throttling requests to both providers
                //YouTubeServiceManage.UpdateVideoStatus(videoModels.ToList());
                //VimeoServiceManage.UpdateVideoStatus(vimeoModels.ToList());
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to retrieve video tube models by keywords", ex);
            }
           
            return videoModels;
        }

        public static List<VideoScheduleModel> GetCurrentlyPlayingVideoTubeByKeyword(List<string> keywords, DateTime formattedDateTime)
        {
            Logger.Info("Retrieving videos by keywords for public library");

            var videoScheduleModels = new List<VideoScheduleModel>();

            try
            {
                using (var videoScheduleRepository = new VideoScheduleRepository())
                {

                    var videoSchedules = videoScheduleRepository.GetCurrentlyPlayingVideoTubeByKeyword(keywords, formattedDateTime);

                    videoSchedules.ForEach(v =>
                    {
                        var channel = ChannelManage.GetChannelTubeById(v.ChannelTubeId);                        
                        v.ChannelUrl = channel.Url;
                        v.UserPublicUrl = UserManage.GetUserByChannelName(channel.Name).PublicUrl;
                        v.PlaytimeMessage = DateTimeUtils.PrintPlaytimeDuration(v.PlayingVideoTubeStartTime, v.PlayingVideoTubeEndTime);
                        v.ChannelName = channel.Name;
                        videoScheduleModels.Add(v);
                    });
                }

                var videoModels = new List<VideoTubeModel>();
                var vimeoModels = new List<VideoTubeModel>();

                if (videoScheduleModels != null && videoScheduleModels.Count > 0)
                {
                    videoModels.ForEach(video =>
                    {
                        if (video.VideoProviderId == 2)
                        {
                            vimeoModels.Add(video);
                        }
                        else if (video.VideoProviderId == 1)
                        {
                            videoModels.Add(video);
                        }
                    });
                }
                //MST: 03152017 - Turning off update all status updates because Vimeo started to throttle us. Th
                //these updates will be done with JAS by throttling requests to both providers
                //YouTubeServiceManage.UpdateVideoStatus(videoModels.ToList());
                //VimeoServiceManage.UpdateVideoStatus(vimeoModels.ToList());
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to retrieve video schedule models by keywords", ex);
            }

            return videoScheduleModels;
        }


    }
}
