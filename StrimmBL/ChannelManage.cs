using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using Strimm.Model.Projections;
using Strimm.Model;
using Strimm.Data.Repositories;
using System.Diagnostics.Contracts;
using System.Net.Http;
using System.Net.Http.Headers;
using Strimm.Shared;
using Strimm.Model.WebModel;
using System.Configuration;
using System.Collections.Concurrent;

namespace StrimmBL
{
    public static class ChannelManage
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ChannelManage));

        private static int MAX_VIDEOS_ON_SCHEDULE = 10;

        static ChannelManage()
        {
            try
            {
                var maxVideos = ConfigurationManager.AppSettings["MaxVideosOnSchedule"] ?? "0";
                if (!Int32.TryParse(maxVideos, out MAX_VIDEOS_ON_SCHEDULE))
                {
                    MAX_VIDEOS_ON_SCHEDULE = 10;
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Unable to retrieve number of videos that can be extracted per single schedule", ex);
            }
        }
        //TODO
        public static List<ChannelTube> GetChannelTubeByUserIdForAdmin(int userId)
        {
            Logger.Info(String.Format("Retrieving all channelTubes for user with Id={0}", userId));

            var channelTubeRepository = new ChannelTubeRepository();
            return  channelTubeRepository.GetChannelTubeByUserIdForAdmin(userId);
        }

        public static ChannelTube GetChannelTubeById(int channelTubeId)
        {
            Logger.Info(String.Format("Retreiving channel Tube using Id={0}", channelTubeId));

            ChannelTube channel = null;

            using (var channelTubeRepository = new ChannelTubeRepository())
            {
                channel = channelTubeRepository.GetChannelTubeById(channelTubeId);
            }

            return channel;
        }

        public static void UpsertChannelRokuSettings(ChannelTubeRokuSettings settings)
        {
            Logger.Info(String.Format("Upserting channel Tube roku settings "));
            using (var channelTubeRepository = new ChannelTubeRepository())
            {
                channelTubeRepository.UpsertChannelRokuSettings(settings);
            }
        }

        public static ChannelTubeRokuSettings GetChannelRokuSettings(int channelTubeId)
        {
            Logger.Info(String.Format("Retreiving channel Tube roku settings " + channelTubeId));
            ChannelTubeRokuSettings settings = null;

            using (var channelTubeRepository = new ChannelTubeRepository())
            {
                settings = channelTubeRepository.GetChannelRokuSettings(channelTubeId);
            }
            return settings;
        }

        public static ChannelTubePo GetChannelTubePoById(int channelTubeId)
        {
            Logger.Info(String.Format("Retreiving channel Tube Po using Id={0}", channelTubeId));

            ChannelTubePo channel = null;

            using (var channelTubeRepository = new ChannelTubeRepository())
            {
                channel = channelTubeRepository.GetChannelTubePoById(channelTubeId);
            }

            return channel;
        }

        public static bool IsChannelNameUnique(string channelTubeName)
        {
            Logger.Info(String.Format("Checking uniqueness of the channel name '{0}'", channelTubeName));

            return ReferenceDataManage.IsReservedName(channelTubeName);
        }

        public static bool CreateChannelTube(int categoryId, int languageId, string name, string description, string pictureUrl, string url, int userId, bool shouldAddPublicVideos, int randomVideoAddCount, bool isWhiteLabeled, string channelPassword, bool embedEnabled,bool muteOnStartup,  string customLabel, string subscriberDomain, bool embedOnlyMode, bool matureContentEnabled, bool showPlayerControls, bool isPrivate, bool isLogoModeActive, string channelLogoUrl, bool playLiveFirst, bool keepGuideOpen, out ChannelTube channelTube)
        {
            Logger.Info(String.Format("Add new ChannelTube named '{0}'", name));

            bool isSuccess = false;

            using (var channelTubeRepository = new ChannelTubeRepository())
            using (var videoTubeRepository = new VideoTubeRepository())
            {
                if (userId > 0)
                {
                    var existing = channelTubeRepository.GetChannelTubeByUrl(url);

                    if (existing == null)
                    {
                        name = TextUtils.ReplaceNonPrintableCharacters(name);
                        description = TextUtils.ReplaceNonPrintableCharacters(description);

                        channelTube = channelTubeRepository.InsertChannelTubeWithGet(categoryId, languageId, name, description, pictureUrl, url, userId, isWhiteLabeled, channelPassword, embedEnabled, muteOnStartup, customLabel, subscriberDomain, embedOnlyMode, matureContentEnabled, showPlayerControls, isPrivate, isLogoModeActive, channelLogoUrl, playLiveFirst,keepGuideOpen);
                       
                        isSuccess = channelTube != null;
                        
                        //if (shouldAddPublicVideos)
                        //{
                        //    var allPublicVideoTubes = videoTubeRepository.GetPublicVideoTubesByCategoryId(categoryId);

                        //    if (allPublicVideoTubes != null && allPublicVideoTubes.Count > 0)
                        //    {
                        //        randomVideoAddCount = (randomVideoAddCount > allPublicVideoTubes.Count) ? allPublicVideoTubes.Count : randomVideoAddCount;

                        //        var arrayOfRandomIndexes = RandomUtils.GenerateArrayOfRandomNumbersInRange(0, allPublicVideoTubes.Count, randomVideoAddCount);
                        //        var listOfRandomVideoIdes = new List<int>();

                        //        arrayOfRandomIndexes.ToList().ForEach(x => listOfRandomVideoIdes.Add(allPublicVideoTubes[x].VideoTubeId));

                        //        videoTubeRepository.BulkInsertVideoTubeListIntoChannelTubeById(channelTube.ChannelTubeId, listOfRandomVideoIdes, false);
                        //    }
                        //}
                    }
                    else
                    {
                        channelTube = null;
                        Logger.Warn(String.Format("Unable to create a new channel. Channel with the same URL='{0}' already existing", url));
                    }
                }
                else
                {
                    channelTube = null;
                    Logger.Warn("Unable to create a new channel. User Id was not specified");
                }
            }

            return isSuccess;
        }

        public static bool UpdateChannelTube(ChannelTube channel)
        {
            if (channel == null)
            {
                Logger.Warn("Unable to update existing channel. ChannelTube is not valid");
                return false;
            }

            Logger.Info(String.Format("Updating channel tube with Id={0}", channel.ChannelTubeId));

            bool isSuccess = false;

            using (var channelTubeRepository = new ChannelTubeRepository())
            {
                isSuccess = channelTubeRepository.UpdateChannelTube(channel);
            }

            return isSuccess;
        }

        /// <summary>
        /// This method will return a currently playing channel by its id
        /// </summary>
        /// <param name="channelId">Channel Tube Id</param>
        /// <param name="userId">Requesting User Id</param>
        /// <returns>Instance of ChannelPreviewModel</returns>
        public static ChannelPreviewModel GetCurrentlyPlayingChannelById(int channelId, int userId, DateTime clientTime)
        {
            ChannelPreviewModel channelModel = null;

            using (var userRepository = new UserRepository())
            using (var channelTubeRespository = new ChannelTubeRepository())
            using (var channelScheduleRepository = new ChannelScheduleRepository())
            {
                var channelPo = channelTubeRespository.GetChannelTubePoWithUserDataByChannelIdAndUserId(channelId, userId);
                //var subscribedChannels = channelTubeRespository.GetChannelTubesSubscribedByUserByUserId(userId);

                if (channelPo != null && !channelPo.IsLocked && !channelPo.IsDeleted && !channelPo.MatureContentEnabled && !channelPo.IsPrivate && !channelPo.EmbedOnlyModeEnabled)
                {
                    var user = userRepository.GetUserPoByUserId(channelPo.UserId);

                    channelModel = new ChannelPreviewModel()
                    {
                        Channel = new ChannelTubeModel()
                        {
                            CategoryName = channelPo.CategoryName,
                            ChannelTubeId = channelPo.ChannelTubeId,
                            ChannelUrl = channelPo.Url,
                            ChannelViewsCount = channelPo.ChannelViewsCount,
                            CurrentScheduleEndTime = channelPo.CurrentScheduleEndTime,
                            CurrentScheduleStartTime = channelPo.CurrentScheduleStartTime,
                            ChannelOwnerUrl = channelPo.ChannelOwnerPublicUrl,
                            ChannelOwnerUserName = channelPo.ChannelOwnerUserName,
                            Description = channelPo.Description,
                            IsAutoPilotOn = channelPo.IsAutoPilotOn,
                            IsFeatured = channelPo.IsFeatured,
                            IsLocked = channelPo.IsLocked,
                            IsPrivate = channelPo.IsPrivate,
                            Name = channelPo.Name,
                            PictureUrl = ImageUtils.GetChannelImageUrl(channelPo.PictureUrl),
                            Rating = channelPo.Rating,
                            SubscriberCount = channelPo.SubscriberCount,
                            Url = channelPo.Url,
                            VideoTubeCount = channelPo.VideoTubeCount,
                            PlaytimeMessage = DateTimeUtils.PrintPlaytimeDuration(channelPo.PlayingVideoTubeStartTime, channelPo.PlayingVideoTubeEndTime),
                            UserChannelRating = channelPo.UserChannelRating,
                            IsLike = channelPo.IsLike,
                            IsSubscribed = channelPo.IsSubscribed,
                            ChannelPassword = channelPo.ChannelPassword,
                            IsWhiteLabel = channelPo.MuteOnStartup,
                            EmbedEnabled = channelPo.EmbedEnabled,
                            CustomLabel=channelPo.CustomLabel,
                            KeepGuideOpen=channelPo.KeepGuideOpen
                        },
                        IsMyChannel = userId == channelPo.UserId,
                        IsSubscribed = channelPo.IsSubscribed, //(subscribedChannels != null && subscribedChannels.Any(x => x.ChannelTubeId == channelPo.ChannelTubeId)),
                        User = new UserModel()
                        {
                            ProfileImageUrl = user.ProfileImageUrl,
                            PublicUrl = user.PublicUrl,
                            UserStory = user.UserStory,
                            UserId = user.UserId,
                            Gender = user.Gender
                        }
                    };
                    
                    var now = clientTime;
                    var schedules = ScheduleManage.GetChannelTubeSchedulesByDate(channelId, now);
                    // if schedules in list  has been created without any videos in it end time doesnt exist, schedule.endtime is null and throw exception

                    if (schedules != null && schedules.Count > 0)
                    {
                        var currentChannelSchedule = schedules.FirstOrDefault(x =>
                        {
                            DateTime startTime = x.StartDateAndTime;
                            DateTime endTime = x.EndDateAndTime ?? startTime;
                            
                            return startTime <= now && endTime > now && x.Published;
                        });

                        if (currentChannelSchedule == null || currentChannelSchedule.VideoSchedules.Count == 0)
                        {
                            ChannelScheduleModel nextSchedule = null;
                            foreach (ChannelScheduleModel schedule in schedules.OrderBy(x => x.EndTime))
                            {
                                DateTime startTime = DateTime.Parse(schedule.StartTime);
                                if (startTime > now)
                                {
                                    nextSchedule = schedule;
                                    break;
                                }
                            }

                            if (nextSchedule != null)
                            {
                                channelModel.NextScheduleStartTimeString = nextSchedule.StartTime;
                                channelModel.NextScheduleStartDateTime = nextSchedule.StartDateAndTime;
                                
                                DateTime startTime = DateTime.Parse(nextSchedule.StartTime);
                                channelModel.Message = String.Format("Channel is off the air right now. It will start playing at {0} today", startTime.ToString("MM/dd/yyyy HH:mm tt"));
                            }
                            else
                            {
                                channelModel.Message = "This channel has no active schedule for today";
                            }
                        }
                        else
                        {
                            channelModel.ActiveSchedule = currentChannelSchedule;

                            var playlist = new List<VideoScheduleModel>();
                            var playSchedule = new List<VideoScheduleModel>();

                            var videosAvailableForPlaylist = new List<VideoTubeModel>();

                            currentChannelSchedule.VideoSchedules.ForEach(v => videosAvailableForPlaylist.Add(new VideoTubeModel(v)));
                            var videoModels = new List<VideoTubeModel>();
                            var vimeoModels = new List<VideoTubeModel>();
                            if (videosAvailableForPlaylist != null && videosAvailableForPlaylist.Count != 0)
                         {
                             videosAvailableForPlaylist.ForEach(video =>
                             {
                                    if (video.VideoProviderName == "vimeo")
                                 {
                                     vimeoModels.Add(video);
                                 }
                                    else if (video.VideoProviderName == "youtube")
                                 {
                                     videoModels.Add(video);
                                 }
                             });
                         }

                           // YouTubeServiceManage.UpdateVideoStatus(videosAvailableForPlaylist);
                         var yVideos = videoModels.ToList();
                         var vVideos = vimeoModels.ToList();

                         //MST: 03152017 - Turning off update all status updates because Vimeo started to throttle us. Th
                         //these updates will be done with JAS by throttling requests to both providers
                         //YouTubeServiceManage.UpdateVideoStatus(yVideos);
                         //VimeoServiceManage.UpdateVideoStatus(vVideos);

                            currentChannelSchedule.VideoSchedules.ForEach(v =>
                            {
                                if (v.PlaybackEndTime > now)
                                {
                                    if (v.PlaybackStartTime <= now)
                                    {
                                        double diff = (now - v.PlaybackStartTime).TotalSeconds;

                                        if (diff < 5)
                                        {
                                            v.PlayerPlaybackStartTimeInSec = 0;
                                        }
                                        if (diff > 5)
                                        {
                                            string fixedDouble = MiscUtils.ToFixed(diff, 2);

                                            v.PlayerPlaybackStartTimeInSec = double.Parse(fixedDouble) - 5;
                                        }
                                    }

                                    var checkedVideo = videosAvailableForPlaylist.FirstOrDefault(x => x.VideoTubeId == v.VideoTubeId);

                                    if (checkedVideo != null && !checkedVideo.IsRemovedByProvider && !checkedVideo.IsRestrictedByProvider)//update video by js there is no .net API for Dmotion
                                    {
                                        if (playlist.Count < MAX_VIDEOS_ON_SCHEDULE)
                                        {
                                            playlist.Add(v);
                                        }
                                    }
                                }
                            });

                            channelModel.Playlist = playlist;
                        }
                    }
                    else
                    {
                        channelModel.Message = "This channel has no active schedule for today.";
                    }
                }
                else if (channelPo != null && (channelPo.MatureContentEnabled || channelPo.IsPrivate || channelPo.EmbedOnlyModeEnabled))
                {
                    channelModel = new ChannelPreviewModel()
                    {
                        Message = "This channel is restricted and can not be viewed on Stream platform",
                        IsRestricted = true
                    };
                }
                else
                {
                    channelModel = new ChannelPreviewModel()
                    {
                        Message = "Unable to retrieve channel information. Invalid channel specified"
                    };
                }
            }

            return channelModel;
        }
        public static ChannelPreviewModel GetCurrentlyPlayingChannelByIdForEmbedding(int channelId, int userId, DateTime clientTime)
        {
            ChannelPreviewModel channelModel = null;

            using (var userRepository = new UserRepository())
            using (var channelTubeRespository = new ChannelTubeRepository())
            using (var channelScheduleRepository = new ChannelScheduleRepository())
            {
                var channelPo = channelTubeRespository.GetChannelTubePoWithUserDataByChannelIdAndUserId(channelId, userId);
                //var subscribedChannels = channelTubeRespository.GetChannelTubesSubscribedByUserByUserId(userId);

                if (channelPo != null && !channelPo.IsLocked && !channelPo.IsDeleted)
                {
                    var user = userRepository.GetUserPoByUserId(channelPo.UserId);

                    channelModel = new ChannelPreviewModel()
                    {
                        Channel = new ChannelTubeModel()
                        {
                            CategoryName = channelPo.CategoryName,
                            ChannelTubeId = channelPo.ChannelTubeId,
                            ChannelUrl = channelPo.Url,
                            ChannelViewsCount = channelPo.ChannelViewsCount,
                            CurrentScheduleEndTime = channelPo.CurrentScheduleEndTime,
                            CurrentScheduleStartTime = channelPo.CurrentScheduleStartTime,
                            ChannelOwnerUrl = channelPo.ChannelOwnerPublicUrl,
                            ChannelOwnerUserName = channelPo.ChannelOwnerUserName,
                            Description = channelPo.Description,
                            IsAutoPilotOn = channelPo.IsAutoPilotOn,
                            IsFeatured = channelPo.IsFeatured,
                            IsLocked = channelPo.IsLocked,
                            IsPrivate = channelPo.IsPrivate,
                            Name = channelPo.Name,
                            PictureUrl = ImageUtils.GetChannelImageUrl(channelPo.PictureUrl),
                            Rating = channelPo.Rating,
                            SubscriberCount = channelPo.SubscriberCount,
                            Url = channelPo.Url,
                            VideoTubeCount = channelPo.VideoTubeCount,
                            PlaytimeMessage = DateTimeUtils.PrintPlaytimeDuration(channelPo.PlayingVideoTubeStartTime, channelPo.PlayingVideoTubeEndTime),
                            UserChannelRating = channelPo.UserChannelRating,
                            IsLike = channelPo.IsLike,
                            IsSubscribed = channelPo.IsSubscribed,
                            ChannelPassword = channelPo.ChannelPassword,
                            IsWhiteLabel = channelPo.MuteOnStartup,
                            EmbedEnabled = channelPo.EmbedEnabled,
                            CustomLabel = channelPo.CustomLabel,
                            CustomLogo= ImageUtils.GetCustomLogoImageUrl(channelPo.CustomLogo),
                            IsLogoModeActive=channelPo.IsLogoModeActive
                        },
                        IsMyChannel = userId == channelPo.UserId,
                        IsSubscribed = channelPo.IsSubscribed, //(subscribedChannels != null && subscribedChannels.Any(x => x.ChannelTubeId == channelPo.ChannelTubeId)),
                        User = new UserModel()
                        {
                            ProfileImageUrl = user.ProfileImageUrl,
                            PublicUrl = user.PublicUrl,
                            UserStory = user.UserStory,
                            UserId = user.UserId,
                            Gender = user.Gender
                        }
                    };

                    var now = clientTime;
                    var schedules = ScheduleManage.GetChannelTubeSchedulesByDate(channelId, now);
                    // if schedules in list  has been created without any videos in it end time doesnt exist, schedule.endtime is null and throw exception

                    if (schedules != null && schedules.Count > 0)
                    {
                        var currentChannelSchedule = schedules.FirstOrDefault(x =>
                        {
                            DateTime startTime = x.StartDateAndTime;
                            DateTime endTime = x.EndDateAndTime ?? startTime;

                            return startTime <= now && endTime > now && x.Published;
                        });

                        if (currentChannelSchedule == null || currentChannelSchedule.VideoSchedules.Count == 0)
                        {
                            ChannelScheduleModel nextSchedule = null;
                            foreach (ChannelScheduleModel schedule in schedules.OrderBy(x => x.EndTime))
                            {
                                DateTime startTime = DateTime.Parse(schedule.StartTime);
                                if (startTime > now)
                                {
                                    nextSchedule = schedule;
                                    break;
                                }
                            }

                            if (nextSchedule != null)
                            {
                                channelModel.NextScheduleStartTimeString = nextSchedule.StartTime;
                                channelModel.NextScheduleStartDateTime = nextSchedule.StartDateAndTime;

                                DateTime startTime = DateTime.Parse(nextSchedule.StartTime);
                                channelModel.Message = String.Format("Channel is off the air right now. It will start playing at {0} today", startTime.ToString("MM/dd/yyyy HH:mm tt"));
                            }
                            else
                            {
                                channelModel.Message = "This channel has no active schedule for today";
                            }
                        }
                        else
                        {
                            channelModel.ActiveSchedule = currentChannelSchedule;

                            var playlist = new List<VideoScheduleModel>();
                            var playSchedule = new List<VideoScheduleModel>();

                            var videosAvailableForPlaylist = new List<VideoTubeModel>();

                            currentChannelSchedule.VideoSchedules.ForEach(v => videosAvailableForPlaylist.Add(new VideoTubeModel(v)));
                            var videoModels = new List<VideoTubeModel>();
                            var vimeoModels = new List<VideoTubeModel>();
                            if (videosAvailableForPlaylist != null && videosAvailableForPlaylist.Count != 0)
                            {
                                videosAvailableForPlaylist.ForEach(video =>
                                {
                                    if (video.VideoProviderName == "vimeo")
                                    {
                                        vimeoModels.Add(video);
                                    }
                                    else if (video.VideoProviderName == "youtube")
                                    {
                                        videoModels.Add(video);
                                    }
                                });
                            }

                            // YouTubeServiceManage.UpdateVideoStatus(videosAvailableForPlaylist);
                            var yVideos = videoModels.ToList();
                            var vVideos = vimeoModels.ToList();

                            //MST: 03152017 - Turning off update all status updates because Vimeo started to throttle us. Th
                            //these updates will be done with JAS by throttling requests to both providers
                            //YouTubeServiceManage.UpdateVideoStatus(yVideos);
                            //VimeoServiceManage.UpdateVideoStatus(vVideos);

                            currentChannelSchedule.VideoSchedules.ForEach(v =>
                            {
                                if (v.PlaybackEndTime > now)
                                {
                                    if (v.PlaybackStartTime <= now)
                                    {
                                        double diff = (now - v.PlaybackStartTime).TotalSeconds;

                                        if (diff < 5)
                                        {
                                            v.PlayerPlaybackStartTimeInSec = 0;
                                        }
                                        if (diff > 5)
                                        {
                                            string fixedDouble = MiscUtils.ToFixed(diff, 2);

                                            v.PlayerPlaybackStartTimeInSec = double.Parse(fixedDouble) - 5;
                                        }
                                    }

                                    var checkedVideo = videosAvailableForPlaylist.FirstOrDefault(x => x.VideoTubeId == v.VideoTubeId);

                                    if (checkedVideo != null && !checkedVideo.IsRemovedByProvider && !checkedVideo.IsRestrictedByProvider)//update video by js there is no .net API for Dmotion
                                    {
                                        if (playlist.Count < MAX_VIDEOS_ON_SCHEDULE)
                                        {
                                            playlist.Add(v);
                                        }
                                    }
                                }
                            });

                            channelModel.Playlist = playlist;
                        }
                    }
                    else
                    {
                        channelModel.Message = "This channel has no active schedule for today.";
                    }
                }
                else if (channelPo != null )
                {
                    channelModel = new ChannelPreviewModel()
                    {
                        Message = "This channel is restricted and can not be viewed on Stream platform",
                        IsRestricted = true
                    };
                }
                else
                {
                    channelModel = new ChannelPreviewModel()
                    {
                        Message = "Unable to retrieve channel information. Invalid channel specified"
                    };
                }
            }

            return channelModel;
        }

        public static ChannelLike GetChannelLikeByChannelIdAndUserId(int channelTubeId, int userId)
        {
            Logger.Info(String.Format("Getting channel like by channel tube with Id={0}", channelTubeId));

            var channelLike = new ChannelLike();

            using (var channelChannelLikeRepository = new ChannelLikeRepository())
            {
                channelLike = channelChannelLikeRepository.GetChannelLikeByChannelTubeIdAndUserId(channelTubeId, userId);
            }
            return channelLike;
        }

        /// <summary>
        /// This method will retrieve video schedules associated with videos that are part of a channel schedule
        /// identified by its channelScheduleId
        /// </summary>
        /// <param name="channelScheduleId">Channel Schedule Id</param>
        /// <returns></returns>
        public static List<VideoSchedule> GetVideoSchedulesByChannelScheduleId(int channelScheduleId)
        {
            Logger.Info(String.Format("Getting video schedules associated with channel schedule with Id={0}", channelScheduleId));
            List<VideoSchedule> videoscheduleList = new List<VideoSchedule>();
            using (var videoScheduleRepository = new VideoScheduleRepository())
            {
                videoscheduleList = videoScheduleRepository.GetVideoSchedulesByChannelScheduleId(channelScheduleId);
            }

            return videoscheduleList;
        }

        /// <summary>
        /// This method will retrieve a single channel schedule by its id
        /// </summary>
        /// <param name="channelScheduleId">Channel Schedule Id</param>
        /// <returns></returns>
        public static ChannelSchedule GetChannelScheduleById(int channelScheduleId)
        {
            Logger.Info(String.Format("Getting channel schedule by Id={0}", channelScheduleId));
            ChannelSchedule schedule = new ChannelSchedule();
            using (var channelScheduleRepository = new ChannelScheduleRepository())
            {
                schedule = channelScheduleRepository.GetChannelScheduleById(channelScheduleId);
            }

            return schedule;
        }

        /// <summary>
        /// This method will retrieve channel schedules for a specific data and specific channel
        /// </summary>
        /// <param name="startTime">Target date</param>
        /// <param name="channelTubeId">Channel Tube Id</param>
        /// <returns></returns>
        public static List<ChannelSchedule> GetChannelSchedulesByDate(DateTime startTime, int channelTubeId)
        {
            Logger.Info(String.Format("Retrieving channel schedules by date '{0}' and channel tube with Id={1}", startTime.ToString(), channelTubeId));
            List<ChannelSchedule> channelScheduleList = new List<ChannelSchedule>();
            using (var channelScheduleRepository = new ChannelScheduleRepository())
            {
                channelScheduleList = channelScheduleRepository.GetChannelSchedulesByDateAndChannelTubeId(startTime, channelTubeId);
            }

            return channelScheduleList;
        }

        /// <summary>
        /// This method will check if channel schedule exists usings its id
        /// </summary>
        /// <param name="channelScheduleId"></param>
        /// <returns></returns>
        public static bool HasChannelSchedule(int channelScheduleId)
        {
            Logger.Info(String.Format("Checking if channel tube schedule exists by Id={0}", channelScheduleId));

            return GetChannelScheduleById(channelScheduleId) != null;
        }

        /// <summary>
        /// This method will remove an existing channel schedule by its Id
        /// </summary>
        /// <param name="channelScheduleId">Channel Schedule Id</param>
        public static void RemoveChannelScheduleById(int channelScheduleId)
        {
            Logger.Info(String.Format("Deleting channel schedule by Id={0}", channelScheduleId));
            using (var channelScheduleRepository = new ChannelScheduleRepository())
            {
                channelScheduleRepository.DeleteChannelScheduleById(channelScheduleId);
            }
        }

        public static List<ChannelTubePo> GetChannelTubesForUser(int userId)
        {
            Logger.Info(String.Format("Retrieving all channel tubes for user with Id={0}", userId));
            List<ChannelTubePo> channelList = new List<ChannelTubePo>();
            using (var channelTubeRepository = new ChannelTubeRepository())
            {
                channelList = channelTubeRepository.GetChannelTubePosByUserId(userId);
            }

            return channelList;
        }

        public static List<ChannelTubePo> GetChannelTubesForAdmin(int userId)
        {
            Logger.Info(String.Format("Retrieving all channel tubes for user with Id={0}", userId));
            List<ChannelTubePo> channelList = new List<ChannelTubePo>();
            using (var channelTubeRepository = new ChannelTubeRepository())
            {
                channelList = channelTubeRepository.GetChannelTubePosByUserIdForAdmin(userId);
            }

            return channelList;
        }

         public static bool HasUserSubscribedToChannelTube(int userId, int channelTubeId)
         {
             Logger.Info(String.Format("Checking if user with Id={0} is subscribed to channel with Id={1}", userId, channelTubeId));
            bool hasSubscribedChannelTube = false;
            using (var channelSubscriptionRepository = new ChannelSubscriptionRepository())
            {

             var subscription = channelSubscriptionRepository.GetChannelSubscriptionByChannelTubeIdAndUserId(channelTubeId, userId);
                hasSubscribedChannelTube = subscription != null;
            }

            return hasSubscribedChannelTube;
         }

         public static bool AddChannelSubscriptionForUser(int userId, int channelTubeId, DateTime clientTime)
         {
             Logger.Info(String.Format("Creating Channel Subscription for user with Id={0} to channel tube Id={1} using timeoffset={2}", userId, channelTubeId, clientTime));
            var success = false;
            using (var channelTubeRespository = new ChannelTubeRepository())
            {
                success = channelTubeRespository.InsertChannelSubscriptionByUserIdAndChannelTubeId(userId, channelTubeId, clientTime);
            }

            return success;
         }

        /// <summary>
        /// This method will unsubscribe user from channel
        /// </summary>
        /// <param name="userId">User id</param>
        /// <param name="channelTubeId">Channel Tube Id</param>
        /// <param name="clientTime">Client request time</param>
        /// <returns></returns>
        public static bool DeleteChannelSubscriptionForUser(int userId, int channelTubeId, DateTime clientTime)
        {
            Logger.Info(String.Format("Deleting Channel Subscription for user with Id={0} to channel tube Id={1} as of {2}", userId, channelTubeId, clientTime.ToString()));

            bool isSuccess = false;

            using (var channelTubeRepository = new ChannelTubeRepository())
            {
                isSuccess = channelTubeRepository.DeleteChannelSubscriptionByUserIdAndChannelTubeId(userId, channelTubeId, clientTime);
            }

            return isSuccess;
        }

         //public static void AddRing(Rings ring)
         //{
         //    StrimmDB context = new StrimmDB();
         //    context.ring.Add(ring);
         //    context.SaveChanges();
         //}

         //public static List<Rings> GetRingsByUserId(int userId)
         //{
         //    StrimmDB context = new StrimmDB();
         //    var rings = (from r in context.ring
         //                 where r.followId == userId
         //                 select r).ToList();
         //    return rings;
         //}

        

         //public static void DeleteOldRings(List<Rings> ringsList, int offset)
         //{
         //    DateTime dateTimeNow = DateTime.Now;
         //    StrimmDB context = new StrimmDB();
         //    foreach (var r in ringsList)
         //    {
         //        if (r.endOfSchedule < dateTimeNow)
         //        {
         //            var ring = (from rs in context.ring
         //                        where rs.ringId == r.ringId
         //                        select rs).SingleOrDefault();
         //            context.ring.Remove(ring);
         //            context.SaveChanges();

         //        }
         //    }
         //}

         //public static List<Rings> GetFollowingRings(int userId)
         //{
         //    StrimmDB context = new StrimmDB();
         //    List<Rings> ringsList = new List<Rings>();
         //    var followings =(from f in context.Follow
         //                     where f.followerUserId==userId
         //                     select f).ToList();
         //    if (followings.Count != 0)
         //    {
         //        foreach (var f in followings)
         //        {
         //            List<Rings> ringsFromFollowing = GetRingsByUserId(f.userId);
         //            if (ringsFromFollowing.Count != 0)
         //            {
         //                ringsList.AddRange(ringsFromFollowing);
         //            }
         //        }
         //    }
         //    return ringsList;
         //}

         public static ChannelTube GetChannelTubeByChannelScheduleId(int channelScheduleId)
         {
             Logger.Info(String.Format("Getting channel tube by channel schedule Id={0}", channelScheduleId));

             ChannelTube channel = null;

             using (var channelTubeRepository = new ChannelTubeRepository())
             {
                 channel = channelTubeRepository.GetChannelTubeByChannelScheduleId(channelScheduleId);
             }

             return channel;
         }

         public static List<ChannelTube> GetFaveoriteChannelsByUserId(int userId)
         {
             Logger.Info(String.Format("Retrieving favorite channels for user with Id={0}", userId));

             var favoriteChannels = new List<ChannelTube>();

             using (var channelTubeRepository = new ChannelTubeRepository())
             {
                 favoriteChannels = channelTubeRepository.GetChannelTubesSubscribedByUserByUserId(userId);
             }

             return favoriteChannels != null ? favoriteChannels.OrderByDescending(x => x.CreatedDate).ToList() : new List<ChannelTube>();
         }

         public static List<ChannelTube> GetChannelTubesByCategoryId(int categoryId)
         {
             Logger.Info(String.Format("Getting channel tubes by category Id={0}", categoryId));

             var channels = new List<ChannelTube>();

             using (var channelTubeRepository = new ChannelTubeRepository())
             {
                 channels = channelTubeRepository.GetChannelTubesByCategoryId(categoryId);
             }

             return channels;
         }


         public static List<ChannelTube> GetTopChannels()
         {
             //StrimmDB context = new StrimmDB();
             //var channelList = (from c in context.ChannelTube
             //                   where c.channelCounter>0
             //                   select c).ToList();
             List<ChannelTube> topChannelList = new List<ChannelTube>();
             //if (channelList.Count > 100)
             //{
             //    topChannelList = channelList.OrderByDescending(x => x.channelCounter).Take(channelList.Count).ToList();
             //}
             //else
             //{
             //    topChannelList = channelList.OrderByDescending(x => x.channelCounter).Take(100).ToList();
             //}
             return topChannelList;
         }

         public static List<VideoSchedule> GetVideoSchedulesForVideoTube(int videoTubeId)
         {
             Logger.Info(String.Format("Getting video schedules for video tube with Id={0}", videoTubeId));
             var schedules = new List<VideoSchedule>();

             using (var videoScheduleRepository = new VideoScheduleRepository())
             {
                 schedules = videoScheduleRepository.GetVideoScheduleByVideoTubeId(videoTubeId);
             }

             return schedules;
         }

         public static bool IsChannelNameExists(string channelName)
         {
             Logger.Info(String.Format("Determining if channel name '{0}' corresponds to an existing channel", channelName));

             bool exists = false;

             using (var channelTubeRepository = new ChannelTubeRepository())
             {
                 var channelTube = channelTubeRepository.GetChannelTubeByName(channelName);
                 string channelUrl = channelName.TrimStart().TrimEnd().Replace(" ", "");
                 var channelTubebyUrl = channelTubeRepository.GetChannelTubeByUrl(channelUrl);
                 if (channelTube != null || channelTubebyUrl != null)
                 {
                     exists = true;
                 }
                 else
                 {
                     exists = false;
                 }
                 //if(channelTube==null)
                 //{
                 //    exists = false;
                 //}
                 //else
                 //{
                 //    exists = true;
                 //}
                
             }

             return exists;
         }

         public static List<ChannelTube> GetAllChannelTubes()
         {
             Logger.Info("Retrieving all channel tubes");

             var channels = new List<ChannelTube>();

             using (var channelTubeRepository = new ChannelTubeRepository())
             {
                 channels = channelTubeRepository.GetAllChannelTubes();
             }

             return channels;
         }

      

         private static List<ChannelSchedule> GetAllChannelSchedules()
         {
             Logger.Info("Retrieving all channel schedules");

             var schedules = new List<ChannelSchedule>();

            using (var channelScheduleRepository = new ChannelScheduleRepository())
             {
                 schedules = channelScheduleRepository.GetAllChannelSchedules();
             }

             return schedules;
         }

         public static List<ChannelTube> GetFeaturedChannels()
         {
             Logger.Info("Retrieving all featured channel tubes");

             var channels = new List<ChannelTube>();

             using (var channelTubeRepository = new ChannelTubeRepository())
             {
                 channels = channelTubeRepository.GetAllFeaturedChannelTubes();
             }

             return channels;
         }

         public static List<ChannelSubscription> GetChannelSubscriptionsByChannelTubeId(int channelTubeId)
         {
             Logger.Info(String.Format("Getting channel subscriptions by channel tube with Id={0}", channelTubeId));

             var channelSubscriptions = new List<ChannelSubscription>();

             using (var channelSubscriptionRepository = new ChannelSubscriptionRepository())
             {
                 channelSubscriptions = channelSubscriptionRepository.GetAllChannelSubscriptionsByChannelTubeId(channelTubeId);
             }
             return channelSubscriptions;
         }

         public static void DeleteChannelSubscriptionById(int channelSubscriptionId)
         {
             Logger.Info(String.Format("Deleting channel subscription with Id={0}", channelSubscriptionId));

             using (var channelSubscriptionRepository = new ChannelSubscriptionRepository())
             {
                 channelSubscriptionRepository.DeleteChannelSubscriptionById(channelSubscriptionId);
             }
         }

         public static bool DeleteChannelTubeById(int channelTubeId)
         {
             Logger.Info(String.Format("Deleting an existing channel Tube with Id={0}", channelTubeId));

             bool isSuccess = false;

             using (var channelTubeRepository = new ChannelTubeRepository())
             {
                 isSuccess = channelTubeRepository.DeleteChannelTubeById(channelTubeId);
                 
             }

             return isSuccess;
         }

         public static ChannelTube GetChannelTubeByName(string channelName)
         {
             Logger.Info(String.Format("Retrieving channel Tube by name '{0}'", channelName));

             ChannelTube channelTube = null;

             using (var channelTubeRepository = new ChannelTubeRepository())
             {
                 channelTube = channelTubeRepository.GetChannelTubeByName(channelName);
             }

             return channelTube;
         }

         public static ChannelTube GetChannelTubeByUrl(string channelUrl)
         {
             Logger.Info(String.Format("Retrieving channel Tube by Url '{0}'", channelUrl));

             ChannelTube channelTube = null;

             using (var channelTubeRepository = new ChannelTubeRepository())
             {
                 channelTube = channelTubeRepository.GetChannelTubeByUrl(channelUrl);
             }

             return channelTube;
         }


         public static List<ChannelTubePo> GetAllChannelsOnAutoPilot()
         {
             Logger.Info("Retrieving all channel tubes that have IsAutoPilot flag set to true");

             List<ChannelTubePo> channels = null;

             using (var channelTubeRepository = new ChannelTubeRepository())
             {
                 channels = channelTubeRepository.GetAllChannelTubePosOnAutoPilot();
             }

             return channels;
         }

         public static List<ChannelTubeModel> GetTopChannelsCurrentlyPlaying(DateTime clientTime)
         {
             Logger.Info("Retrieving top channels that are currently playing");

             List<ChannelTubeModel> topChannels = new List<ChannelTubeModel>();

             using (var channelTubeRepository = new ChannelTubeRepository())
             {
                 var channels = channelTubeRepository.GetCurrentlyPlayingTopChannels(clientTime);

                 if (channels != null && channels.Count > 0)
                 {
                     channels.ForEach(x =>
                     {
                         topChannels.Add(new ChannelTubeModel(x));
                     });
                 }
             }

             return topChannels;
         }

         public static List<ChannelTubeModel> GetFavoriteChannelsOnTheAirForUser(int userId, DateTime clientTime)
         {
             Logger.Info(String.Format("Retrieving user's favorite channels that are currently playing for user with Id={0}", userId));

             List<ChannelTubeModel> favoriteChannels = new List<ChannelTubeModel>();

             using (var channelTubeRepository = new ChannelTubeRepository())
             {
                 var channels = channelTubeRepository.GetCurrentlyPlayingFavoriteChannelsForUserByUserId(userId, clientTime);

                 if (channels != null && channels.Count > 0)
                 {
                     channels.ForEach(x =>
                     {
                         
                         favoriteChannels.Add(new ChannelTubeModel(x));
                     });
                 }
             }

             return favoriteChannels;
         }

        public static List<ChannelTubeModel> GetCurrentlyPlayingChannels(DateTime clientTime)
         {
             Logger.Info(String.Format("Retrieving  channels that are currently playing"));

             List<ChannelTubeModel> playingChannels = new List<ChannelTubeModel>();

             using (var channelTubeRepository = new ChannelTubeRepository())
             {
                 var channels = channelTubeRepository.GetCurrentlyPlayingChannels(clientTime);

                 if (channels != null && channels.Count > 0)
                 {
                     channels.ForEach(x =>
                     {

                         playingChannels.Add(new ChannelTubeModel(x));
                     });
                 }
             }

             return playingChannels;
         }

        public static List<ChannelTubeModel> GetCurrentlyPlayingChannelsByUserId(DateTime clientTime, int userId)
        {
            Logger.Info(String.Format("Retrieving  channels that are currently playing by userId"));

            List<ChannelTubeModel> playingChannels = new List<ChannelTubeModel>();

            using (var channelTubeRepository = new ChannelTubeRepository())
            {
                var channels = channelTubeRepository.GetCurrentlyPlayingChannelsByUserId(clientTime, userId);

                if (channels != null && channels.Count > 0)
                {
                    channels.ForEach(x =>
                    {

                        playingChannels.Add(new ChannelTubeModel(x));
                    });
                }
            }

            return playingChannels;
        }

        public static LandingPageDataModel GetCurrentlyPlayingChannelsForLandingPage(DateTime dateAndTime)
        {
            Logger.Info(String.Format("Retrieving  channels that are currently playing"));

            DateTime ds1 = DateTime.Now;

            var featuredChannelsUser = ConfigurationManager.AppSettings["FeaturedChannelsUser"] ?? "Robert";
            var groupName = ConfigurationManager.AppSettings["GroupName"] ?? "Popular Channels";

            var model = new LandingPageDataModel()
            {
                GroupName = groupName.ToString(),
                ChannelGroup = new List<ChannelTubeModel>()
            };

            TimeSpan t1 = new TimeSpan();
            TimeSpan t2 = new TimeSpan();
            TimeSpan ts3 = new TimeSpan();

            try
            {
                var channelsOnLandingPage = ConfigurationManager.AppSettings["ChannelsOnLandingPage"].ToString();

                t1 = (DateTime.Now - ds1);

                using (var channelTubeRepository = new ChannelTubeRepository())
                {
                    var channels = channelTubeRepository.GetCurrentlyPlayingChannelsForLandingPage(featuredChannelsUser, channelsOnLandingPage, dateAndTime);
                    t2 = (DateTime.Now - ds1);
                   
                    if (channels != null && channels.Count > 0)
                    {
                        channels.ForEach(x => model.ChannelGroup.Add(new ChannelTubeModel(x)));
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error occured while retrieving landing page data", ex);
            }

            ts3 = DateTime.Now - ds1;
            Logger.Debug(String.Format("Time to restrieve {0}", ts3.TotalMilliseconds));

            return model;
        }

        //REMOVE AFTER CREATING CONTROL FROM ADMIN PANEL
        public static LandingPageDataModel GetCurrentlyPlayingChannelsForLandingPageTop(DateTime dateAndTime)
        {
            Logger.Info(String.Format("Retrieving  channels that are currently playing"));

            DateTime ds1 = DateTime.Now;

            var featuredChannelsUser = ConfigurationManager.AppSettings["FeaturedChannelsUser"] ?? "Robert";
            var groupName = ConfigurationManager.AppSettings["GroupName"] ?? "Popular Channels";

            var model = new LandingPageDataModel()
            {
                GroupName = groupName.ToString(),
                ChannelGroup = new List<ChannelTubeModel>()
            };

            TimeSpan t1 = new TimeSpan();
            TimeSpan t2 = new TimeSpan();
            TimeSpan ts3 = new TimeSpan();

            try
            {
                var channelsOnLandingPage = ConfigurationManager.AppSettings["ChannelsOnLandingPageTop"].ToString();

                t1 = (DateTime.Now - ds1);

                using (var channelTubeRepository = new ChannelTubeRepository())
                {
                    var channels = channelTubeRepository.GetCurrentlyPlayingChannelsForLandingPage(featuredChannelsUser, channelsOnLandingPage, dateAndTime);
                    t2 = (DateTime.Now - ds1);

                    if (channels != null && channels.Count > 0)
                    {
                        channels.ForEach(x => model.ChannelGroup.Add(new ChannelTubeModel(x)));
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error occured while retrieving landing page data", ex);
            }

            ts3 = DateTime.Now - ds1;
            Logger.Debug(String.Format("Time to restrieve {0}", ts3.TotalMilliseconds));

            return model;
        }

        public static List<ChannelTubeModel> GetCurrentlyPlayingChannelsByCategoryName(DateTime clientTime, string categoryName)
        {
            Logger.Info(String.Format("Retrieving  channels by channeId that are currently playing"));

            List<ChannelTubeModel> playingChannels = new List<ChannelTubeModel>();

            using (var channelTubeRepository = new ChannelTubeRepository())
            {
                var channels = channelTubeRepository.GetCurrentlyPlayingChannelsByCategoryName(clientTime, categoryName);

                if (channels != null && channels.Count > 0)
                {
                    channels.ForEach(x =>
                    {

                        playingChannels.Add(new ChannelTubeModel(x));
                    });
                }
            }

            return playingChannels;
        }
        public static List<ChannelTubeModel> GetCurrentlyPlayingChannelsByCategoryNameAndLanguageId(DateTime clientTime,  int languageId)
        {
            Logger.Info(String.Format("Retrieving  channels by channeId that are currently playing"));

            List<ChannelTubeModel> playingChannels = new List<ChannelTubeModel>();

            using (var channelTubeRepository = new ChannelTubeRepository())
            {
                var channels = channelTubeRepository.GetCurrentlyPlayingChannelsByCategoryNameAndLanguageId(clientTime, languageId);

                if (channels != null && channels.Count > 0)
                {
                    channels.ForEach(x =>
                    {

                        playingChannels.Add(new ChannelTubeModel(x));
                    });
                }
            }

            return playingChannels;
        }
         public static List<ChannelTubeModel> GetAllFavoriteChannelsForUserByUserIdAndClientTime(int userId, DateTime clientTime)
         {
             Logger.Info(String.Format("Retrieving user's favorite channels that are currently playing for user with Id={0}", userId));

             List<ChannelTubeModel> favoriteChannels = new List<ChannelTubeModel>();

             using (var channelTubeRepository = new ChannelTubeRepository())
             {
                 var channels = channelTubeRepository.GetAllFavoriteChannelsForUserByUserIdAndClientTime(userId, clientTime);

                 if (channels != null && channels.Count > 0)
                 {
                     channels.ForEach(x =>
                     {

                         favoriteChannels.Add(new ChannelTubeModel(x));
                     });
                 }
             }

             return favoriteChannels;
         }


         public static List<ChannelTubeModel> GetChannelsByUserIdAndClientTime(int userId, DateTime clientTime)
         {
             Logger.Info(String.Format("Retrieving user's channels for user with Id={0} at '{1}'", userId, clientTime.ToString()));

             List<ChannelTubeModel> userChannels = new List<ChannelTubeModel>();

             using (var channelTubeRepository = new ChannelTubeRepository())
             {
                 var channels = channelTubeRepository.GetChannelsByUserIdAndClientTime(userId, clientTime);

                 if (channels != null && channels.Count > 0)
                 {
                     channels.ForEach(x =>
                     {
                         userChannels.Add(new ChannelTubeModel(x));
                     });
                 }
             }

             return userChannels;
         }

         public static bool AddChannelViewForUser(int? userId, int channelTubeId, DateTime clientTimeDateTime)
         {
             Logger.Info(String.Format("Adding a channel view for user. UserId={0}, channel tube Id={1}, viewed on '{2}'", userId, channelTubeId, clientTimeDateTime));

             bool isSuccess = false;

             using (var channelTubeRepository = new ChannelTubeRepository())
             {
                 isSuccess = userId > 0 ? channelTubeRepository.InsertUserChannelTubeViewByUserIdAndChannelTubeId(channelTubeId, userId, clientTimeDateTime) : false;
             }

             return isSuccess;
         }

         public static List<ChannelTubeModel> GetAllChannelsForUserByUserIdAndClientTime(int userId, DateTime date)
         {
             Logger.Info(String.Format("Retrieving user's channels that are currently playing for user with Id={0}", userId));

             List<ChannelTubeModel> allUserChannels = new List<ChannelTubeModel>();

             using (var channelTubeRepository = new ChannelTubeRepository())
             {
                 if (userId > 0)
                 {
                     var channels = channelTubeRepository.GetAllChannelsForUserByUserIdAndClientTime(userId, date);

                     if (channels != null && channels.Count > 0)
                     {
                         channels.ForEach(x =>
                         {
                             allUserChannels.Add(new ChannelTubeModel(x));
                         });
                     }
                 }
                 else
                 {
                     Logger.Warn("Failed to retrieve channels for user. User Id is invalid (0)");
                 }
             }

             return allUserChannels;
         }

         public static List<UserChannelTubeView> GetChannelViewByUseridChannelTubeIdAndViewTime(int? userId, int channelTubeId, DateTime clientTimeDateTime)
         {
             Logger.Info(String.Format("retireving a channel view for user. UserId={0}, channel tube Id={1}, viewed on '{2}'", userId, channelTubeId, clientTimeDateTime));

             List<UserChannelTubeView> userChannelsViews = new List<UserChannelTubeView>();

             using (var channelTubeRepository = new ChannelTubeRepository())
             {
                 userChannelsViews = channelTubeRepository.GetChannelViewByUserIdChannelIdAndViewTime(channelTubeId, userId, clientTimeDateTime);
             }

             return userChannelsViews;
         }

        public static bool SetChannelRatingByUserIdAndChannelTubeId(int userId, int channelTubeId, float ratingValue, DateTime enteredDate)
         {
             Logger.Info(String.Format("setting a channel rating. UserId={0}, channel tube Id={1}, ratingValue on '{2}'", userId, channelTubeId, ratingValue));

             bool isSuccess = false;

             using (var channelTubeRepository = new ChannelTubeRepository())
             {
                 isSuccess = channelTubeRepository.SetChannelRatingByUserIdAndChannelTubeId(userId, channelTubeId, ratingValue, enteredDate);
             }

             return isSuccess;
         }

        public static float GetUserRatingByUserIdAndChannelTubeId(int userId, int channelTubeId)
        {
            Logger.Info(String.Format("get user rating of channel. UserId={0}, channel tube Id={1}", userId, channelTubeId));

            float rating = 0;

            using (var channelTubeRepository = new ChannelTubeRepository())
            {
                rating = channelTubeRepository.GetUserRatingByUserIdAndChannelTubeId(userId, channelTubeId);
            }

            return rating;
        }

        public static int AddChannelLike(int userId, int channelTubeId, string clientTime)
        {
            Logger.Info(String.Format("Creating Channel Like for user with Id={0} to channel tube Id={1} using timeoffset={2}", userId, channelTubeId, clientTime));
            ChannelLike like = new ChannelLike();
            int likes = 0;
            using (var channelLikeRepository = new ChannelLikeRepository())
            {
                like.UserId = userId;
                like.ChannelTubeId = channelTubeId;
                like.LikeStartDate = DateTimeUtils.GetClientTime(clientTime) ?? DateTime.Now;
                like.IsLike = true;
                likes = channelLikeRepository.InsertChannelLike(like);
            }

            return likes;
        }

        public static int DeleteChannelLike(int userId, int channelTubeId, string clientTime)
        {
            Logger.Info(String.Format("deleting Channel Like for user with Id={0} to channel tube Id={1} using timeoffset={2}", userId, channelTubeId, clientTime));
            int likes = 0;
            using (var channelLikeRepository = new ChannelLikeRepository())
            {
                DateTime likeEndTime = DateTimeUtils.GetClientTime(clientTime) ?? DateTime.Now;
                likes = channelLikeRepository.DeleteChannelLikeByChannelTubeIdAndUserId(channelTubeId, userId, likeEndTime);
            }

            return likes;
        }

        public static List<ChannelStatistics> GetChannelStatistics(DateTime clientTime)
        {
            Logger.Info(String.Format("Retrieving  channels statistics for admin panel"));

            List<ChannelStatistics> channelStatistics = new List<ChannelStatistics>();
            //List<ChannelTubeModel> currentLyPlayingChannels = GetCurrentlyPlayingChannels(clientTime);
            using (var channelTubeRepository = new ChannelTubeRepository())
            {
                channelStatistics = channelTubeRepository.GetChannelStatistics(clientTime);



            }

            return channelStatistics;

        }
        public static List<EmbeddedChannelPo> GetEmbeddedChannelsByDate(DateTime clientTime)
        {
            Logger.Info(String.Format("Retrieving  channels statistics for admin panel"));

            List<EmbeddedChannelPo> embeddedChannels = new List<EmbeddedChannelPo>();
            //List<ChannelTubeModel> currentLyPlayingChannels = GetCurrentlyPlayingChannels(clientTime);
            using (var embeddedChannelsRepository = new EmbeddedHostChannelLoad())
            {
                embeddedChannels = embeddedChannelsRepository.GetEmbeddedChannelsByDate(clientTime);



            }

            return embeddedChannels;

        }

        public static TvGuideDataModel GetTvGuideTopChannelDataByClientTime(DateTime clientTimeDateTime, int pageIndex, int pageSize)
        {
            Logger.Info(String.Format("Retrieving currently active channels for TV guide by client time"));

            TvGuideDataModel model = null;

            using (var channelTubeRepository = new ChannelTubeRepository())
            {

                model = channelTubeRepository.GetTvGuideDataTopChannelsByClientTime(clientTimeDateTime, pageIndex, pageSize);
                if (model != null)
                {
                    model.PageIndex = pageIndex;
                    model.PageSize = pageSize;
                }

            }

            return model;
        }

        public static TvGuideDataModel GetTvGuideDataByClientTime(DateTime clientTimeDateTime, int pageIndex, int pageSize)
        {
            Logger.Info(String.Format("Retrieving currently active channels for TV guide by client time"));

            TvGuideDataModel model = null;

            using (var channelTubeRepository = new ChannelTubeRepository())
            {

                model = channelTubeRepository.GetTvGuideDataByClientTime(clientTimeDateTime, pageIndex, pageSize);
                if (model != null)
                {
                    model.PageIndex = pageIndex;
                    model.PageSize = pageSize;
                }

            }

            return model;
        }

        public static TvGuideDataModel GetTvGuideFavoriteChannelsForUser(DateTime clientTimeDateTime, int userId, int pageIndex, int pageSize)
        {
            Logger.Info(String.Format("Retrieving currently active favorite channels for TV guide by client time and user with id={0}", userId));

            TvGuideDataModel model = null;

            using (var channelTubeRepository = new ChannelTubeRepository())
            {
                model = channelTubeRepository.GetTvGuideFavoriteChannelsForUserByClientTime(clientTimeDateTime, userId, pageIndex, pageSize);
                model.PageIndex = pageIndex;
                model.PageSize = pageSize;
            }

            return model;
        }

        public static TvGuideDataModel GetTvGuideUserChannelsById(DateTime clientTimeDateTime, int userId, int pageIndex, int pageSize)
        {
            Logger.Info(String.Format("Retrieving currently active user channels for TV guide by client time and user id={0}", userId));

            TvGuideDataModel model = null;

            using (var channelTubeRepository = new ChannelTubeRepository())
            {
                model = channelTubeRepository.GetTvGuideUserChannelsById(clientTimeDateTime, userId, pageIndex, pageSize);
                model.PageIndex = pageIndex;
                model.PageSize = pageSize;
            }

            return model;
        }

        public static TvGuideDataModel GetTvGuideUserChannelsByPublicUrl(DateTime clientTimeDateTime, string publicUrl, int pageIndex, int pageSize)
        {
            Logger.Info(String.Format("Retrieving currently active user channels for TV guide by client time and user '{0}'", publicUrl));

            TvGuideDataModel model = null;

            using (var userRepository = new UserRepository())
            using (var channelTubeRepository = new ChannelTubeRepository())
            {
                User user = userRepository.GetUserByPublicUrl(publicUrl);

                if (user != null)
                {
                    model = channelTubeRepository.GetTvGuideUserChannelsById(clientTimeDateTime, user.UserId, pageIndex, pageSize);
                    model.PageIndex = pageIndex;
                    model.PageSize = pageSize;
                }
            }

            return model;
        }

        public static TvGuideDataModel GetTvGuideChannelsByLanguage(DateTime clientTimeDateTime, int languageId, int pageIndex, int pageSize)
        {
            Logger.Info(String.Format("Retrieving currently active user channels for TV guide by client time and language id={0}", languageId));

            TvGuideDataModel model = null;

            using (var channelTubeRepository = new ChannelTubeRepository())
            {
                model = channelTubeRepository.GetTvGuideChannelsByLanguageId(clientTimeDateTime, languageId, pageIndex, pageSize);
                model.PageIndex = pageIndex;
                model.PageSize = pageSize;
            }

            return model;
        }

        public static TvGuideDataModel GetTvGuideChannelsByCategory(DateTime clientTimeDateTime, int categoryId, int pageIndex, int pageSize)
        {
            Logger.Info(String.Format("Retrieving currently active user channels for TV guide by client time and category id={0}", categoryId));

            TvGuideDataModel model = null;

            using (var channelTubeRepository = new ChannelTubeRepository())
            {
                model = channelTubeRepository.GetTvGuideChannelsByCategoryId(clientTimeDateTime, categoryId, pageIndex, pageSize);
                model.PageIndex = pageIndex;
                model.PageSize = pageSize;
            }

            return model;
        }

        public static TvGuideDataModel GetTvGuideChannelsByKeywords(DateTime clientTimeDateTime, string keywords, int pageIndex, int pageSize)
        {
            Logger.Info(String.Format("Retrieving currently active user channels for TV guide by client time and keywords id=[{0}]", keywords));

            TvGuideDataModel model = null;

            using (var channelTubeRepository = new ChannelTubeRepository())
            {
                model = channelTubeRepository.GetTvGuideChannelsByKeywords(clientTimeDateTime, keywords, pageIndex, pageSize);
                model.PageIndex = pageIndex;
                model.PageSize = pageSize;
            }

            return model;
        }

        public static void InsertEmbeddedHostChannelLoad(int channelTubeId, DateTime clientTimeDateTime, string embeddedHostUrl, string accountNumber, bool isSingleChannelView, bool IsSubscribedDomain)
        {
            Logger.Info(String.Format("insert EmbeddedHostChannelLoad with channelTube id=[{0}]", channelTubeId));



            using (var embeddedHostChannelLoad = new EmbeddedHostChannelLoad())
            {
                bool isSuccess = embeddedHostChannelLoad.InsertEmbeddedHostChannelLoad(channelTubeId, clientTimeDateTime, embeddedHostUrl, accountNumber, isSingleChannelView, IsSubscribedDomain);
            }
        }
        public static int InsertEmbeddedHostChannelLoadWithGet(int channelTubeId, DateTime clientTimeDateTime, string embeddedHostUrl, string accountNumber, bool isSingleChannelView, bool IsSubscribedDomain)
        {
            Logger.Info(String.Format("insert EmbeddedHostChannelLoad with channelTube id=[{0}]", channelTubeId));

            int embeddedChannelLoadId = 0;


            using (var embeddedHostChannelLoad = new EmbeddedHostChannelLoad())
            {
                embeddedChannelLoadId = embeddedHostChannelLoad.InsertEmbeddedHostChannelLoadWithGet(channelTubeId, clientTimeDateTime, embeddedHostUrl, accountNumber, isSingleChannelView, IsSubscribedDomain);
            }
            return embeddedChannelLoadId;
        }

        public static List<EmbeddedChannelPo> GetEmbeddedChannelsInfo()
        {
            List<EmbeddedChannelPo> embeddedChannelList = new List<EmbeddedChannelPo>();

            using (var embeddedHostChannelLoad = new EmbeddedHostChannelLoad())
            {
                embeddedChannelList = embeddedHostChannelLoad.GetAllEmbeddedChannels();
            }
            return embeddedChannelList;
        }

        public static bool UpdateEmbeddedHostChannelLoadById(int embeddedChannelHostLoadId, double visitTime, DateTime loadEndTime)
        {
            bool isSuccess = false;
            using (var embeddedHostChannelLoad = new EmbeddedHostChannelLoad())
            {
                isSuccess = embeddedHostChannelLoad.UpdateEmbeddedHostChannelLoadById(embeddedChannelHostLoadId, visitTime, loadEndTime);
            }
            return isSuccess;
        }

        public static List<EmbeddedChannelPo> GetEmbeddedChannelPoByDate(DateTime date)
        {
            throw new NotImplementedException();
        }

        public static TvGuideDataModel GetTvGuideUserChannelsByUsername(DateTime clientTimeDateTime, string userName, int pageIndex, int pageSize)
        {
            Logger.Info(String.Format("Retrieving currently active user channels for TV guide by client time and user '{0}'", userName));

            TvGuideDataModel model = null;

            using (var userRepository = new UserRepository())
            using (var channelTubeRepository = new ChannelTubeRepository())
            {
                User user = userRepository.GetUserByUserName(userName);

                if (user != null)
                {
                    model = channelTubeRepository.GetTvGuideUserChannelsById(clientTimeDateTime, user.UserId, pageIndex, pageSize);
                    model.PageIndex = pageIndex;
                    model.PageSize = pageSize;
                }
            }

            return model;
        }

        public static ChannelTubePageModel GetCurrentlyPlayingChannelPage(DateTime clientTimeDateTime, string sortBy, int? languageId, int pageIndex, int pageSize)
        {
            Logger.Info(String.Format("Retrieving {0} page of currently playing channels sorted by '{1}' and language id '{2}'", pageIndex, sortBy, languageId));

            ChannelTubePageModel pageModel = null;

            using (var channelTubeRepository = new ChannelTubeRepository())
            {
                pageModel = channelTubeRepository.GetCurrentlyPlayingChannelsSortedByPageIndex(clientTimeDateTime, sortBy, languageId, pageIndex, pageSize);
            }

            return pageModel;
        }

        public static TvGuideDataModel GetEmbeddedTvGuideByUserIdAndPageIndex(DateTime clientTimeDateTime, int userId, string domain, int pageIndex, int pageSize)
        {
            Logger.Info(String.Format("Retrieving {0} page of currently playing embedded channels for user with id={1} on domain='{2}'", pageIndex, userId, domain));

            TvGuideDataModel pageModel = null;

            using (var channelTubeRepository = new ChannelTubeRepository())
            {
                pageModel = channelTubeRepository.GetEmbeddedTvGuideByUserIdAndPageIndex(clientTimeDateTime, userId, domain, pageIndex, pageSize);
                pageModel.PageIndex = pageIndex;
                pageModel.PageSize = pageSize;
            }

            return pageModel;
        }

        public static List<EmbeddedChannelPo> GetListOfEmbededChannelsByChannelId(int channelId)
        {
            List<EmbeddedChannelPo> embeddedChannelList = new List<EmbeddedChannelPo>();

            using (var embeddedHostChannelLoad = new EmbeddedHostChannelLoad())
            {
                embeddedChannelList = embeddedHostChannelLoad.GetListOfEmbededChannelsByChannelId(channelId);
            }
            return embeddedChannelList;
        }

        public static bool ValidateChannelPassword(int channelId, string password)
        {
            bool success = false;
            using (var channelTubeRepository = new ChannelTubeRepository())
            {
                var channel = channelTubeRepository.GetChannelTubePoById(channelId);
                var convertedPassword = CryptoUtils.ConvertToBase64(password);
              //  string encodedPass = CryptoUtils.ConvertToBase64(password);
                if (channel.ChannelPassword == convertedPassword)
                {
                    success = true;
                }
            }

            return success;
        }

        public static UserChannelEntitlements GetUserChannelEntitlementsByUserId(int userId)
        {
            UserChannelEntitlements entitlements = null;

            using (var repository = new ChannelTubeRepository())
            {
                entitlements = repository.GetUserChannelEntitlementsByUser(userId);
            }

            return entitlements;
        }

        public static void InsertChannnelPassword(int channelTubeId, int userId, string channelPassword)
        {
          
            using (var channelTubeRepository = new ChannelTubeRepository())
            {
                var channel = channelTubeRepository.InsertChannnelPassword(userId,channelTubeId,channelPassword);
                
                
            }

           
        }

        public static void InsertSubscriberDomain(int userId, int channelTubeId, string subscriberDomain)
        {
            using (var channelTubeRepository = new ChannelTubeRepository())
            {
                var channel = channelTubeRepository.InsertSubscriberDomain(userId, channelTubeId, subscriberDomain);


            }
        }

        public static void UpdateSubscribtionDomain(int userId, int channelTubeId, string domainName)
        {
            using (var channelTubeRepository = new ChannelTubeRepository())
            {
                var channel = channelTubeRepository.UpdateSubscribtionDomain(userId, channelTubeId, domainName);


            }
        }

        public static void DeleteSubscriberDomain(int userId, int channelTubeId)
        {
            using (var channelTubeRepository = new ChannelTubeRepository())
            {
                var channel = channelTubeRepository.DeleteSubscriberDomain(userId, channelTubeId);


            }
        }

        public static   List<VideoLiveTubePo> GetAllVideoLiveTubePosByChannelIdAndDate(int channelTubeId, DateTime targetDate)
        {
            List<VideoLiveTubePo> videos = new List<VideoLiveTubePo>();
            using (var videoTubeRepository = new VideoTubeRepository())
            {
                videos = videoTubeRepository.GetAllVideoLiveTubePosByChannelIdAndDate(channelTubeId, targetDate);


            }
            return videos;
        }
    }

}
