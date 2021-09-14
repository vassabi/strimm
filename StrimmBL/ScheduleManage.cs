using log4net;
using Strimm.Data.Repositories;
using Strimm.Model;
using Strimm.Model.Projections;
using Strimm.Model.WebModel;
using Strimm.Shared;
using Strimm.Shared.Extensions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace StrimmBL
{
    public static class ScheduleManage
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ScheduleManage));

        private static string AWS_CLOUD_FRONT_WEB_DIST_DOMAIN;

        static ScheduleManage()
        {
            AWS_CLOUD_FRONT_WEB_DIST_DOMAIN = ConfigurationManager.AppSettings["AmazonWebDistribDomain"];
        }

        public static void RegisterVideoTubeViewingOccuranceByUser(int videoTubeId, int userId)
        {
            Logger.Info(String.Format("Registering viewing occurance for a videoTube with Id={0} for user with Id={1}", videoTubeId, userId));

            var videoTubeRepository = new VideoTubeRepository();
            videoTubeRepository.InsertUserVideoTubeViewByUserIdAndVideoTubeId(videoTubeId, userId, DateTime.Now, DateTime.Now);
        }

        public static void RemoveOldVideoSchedulesAsOfDate(DateTime asOfDate)
        {
            Logger.Debug(String.Format("Removing old video schedules for all users as of: {0}", asOfDate.ToString("MM/dd/yyyy")));

            using (var repository = new ChannelScheduleRepository())
            {
                repository.RemoveOldVideoSchedulesAsOfDate(asOfDate);
            }
        }

        public static List<ChannelTubeScheduleCalendarEvent> GetChannelTubeScheduleCalendarEvents(int month, int year, int channelTubeId)
        {
            Logger.Info(String.Format("Retrieving channel schedule events for month={0}, year={1} using channel Tube Id={2}", month, year, channelTubeId));

            var events = new List<ChannelTubeScheduleCalendarEvent>();

            using (var channelScheduleRepository = new ChannelScheduleRepository())
            {
                events = channelScheduleRepository.GetChannelTubeScheduleCalendarEvents(month, year, channelTubeId);
            }

            return events;
        }

        public static List<ChannelTubeScheduleCalendarEvent> GetChannelTubeScheduleCalendarEvents(int day, int month, int year, int channelTubeId)
        {
            Logger.Info(String.Format("Retrieving channel schedule events for day={0}, month={1}, year={2} using channel Tube Id={3}", day, month, year, channelTubeId));

            var events = new List<ChannelTubeScheduleCalendarEvent>();

            using (var channelScheduleRepository = new ChannelScheduleRepository())
            {
                events = channelScheduleRepository.GetChannelTubeScheduleCalendarEvents(day, month, year, channelTubeId);
            }

            return events;
        }

        public static VideoTubePageModel GetVideoTubePoByChannelTubeIdAndCategoryIdAndPageIndex(int channelTubeId, int categoryId, int pageIndex, string keywords, int pageSize = 10)
        {
            Logger.Info(String.Format("Retrieving page {0} of videos for channel Id={1} and category Id={2}, {3} videos per page", pageIndex, channelTubeId, categoryId, pageSize));

            int configPageSize = 0;

            if (Int32.TryParse(ConfigurationManager.AppSettings["VideoPageSize"].ToString(), out configPageSize))
            {
                pageSize = configPageSize;
            }

            string originDomain = ConfigurationManager.AppSettings["AmazonWebDistribDomain"];

            var videoPage = new VideoTubePageModel();
            int pageCount = 0;

            using (var videoTubeRepository = new VideoTubeRepository())
            {

                
                var videos = videoTubeRepository.GetVideoTubePoByChannelTubeIdAndCategoryIdAndPageIndex(channelTubeId, categoryId, pageIndex, keywords, out pageCount, pageSize);

                videoPage.PageIndex = pageIndex;
                videoPage.PageSize = pageSize;
                videoPage.PrevPageIndex = pageIndex <= 1 ? 1 : pageIndex - 1;
                videoPage.NextPageIndex = pageIndex >= pageCount ? pageCount : pageIndex + 1;
                videoPage.PageCount = pageCount;
                videoPage.VideoTubeModels = videos; //new List<VideoTubeModel>();

                videos.ForEach(v =>
                {
                    //videoPage.VideoTubeModels.Add(new VideoTubeModel(originDomain, v));
                    v.OriginDomain = originDomain;
                    v.DurationString = DateTimeUtils.PrintTimeSpan(v.Duration);
                });

                var videoModels = new List<VideoTubeModel>();
                var vimeoModels = new List<VideoTubeModel>();

                if (videoPage.VideoTubeModels != null && videoPage.VideoTubeModels.Count > 0)
                {
                    videoPage.VideoTubeModels.ForEach(video =>
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
                //YouTubeServiceManage.UpdateVideoStatus(videoModels);
                //VimeoServiceManage.UpdateVideoStatus(vimeoModels);
            }

            return videoPage;
        }

        public static VideoTubePageModel GetVideoTubePoByChannelTubeIdAndPageIndex(int channelTubeId, int pageIndex, string keywords, int pageSize = 10)
        {
            Logger.Info(String.Format("Retrieving page {0} of videos for channel Id={1}, {2} videos per page", pageIndex, channelTubeId, pageSize));

            int configPageSize = 0;

            if (Int32.TryParse(ConfigurationManager.AppSettings["VideoPageSize"].ToString(), out configPageSize))
            {
                pageSize = configPageSize;
            }

            string originDomain = ConfigurationManager.AppSettings["AmazonWebDistribDomain"];

            var videoPage = new VideoTubePageModel();
            int pageCount = 0;

            using (var videoTubeRepository = new VideoTubeRepository())
            {
                var videos = videoTubeRepository.GetVideoTubePoByChannelTubeIdAndPageIndex(channelTubeId, pageIndex, keywords, out pageCount, pageSize);

                videoPage.PageIndex = pageIndex;
                videoPage.PageSize = pageSize;
                videoPage.PrevPageIndex = pageIndex <= 1 ? 1 : pageIndex - 1;
                videoPage.NextPageIndex = pageIndex >= pageCount ? pageCount : pageIndex + 1;
                videoPage.PageCount = pageCount;
                videoPage.VideoTubeModels = videos; // new List<VideoTubeModel>();

                videos.ForEach(v =>
                {
                   // videoPage.VideoTubeModels.Add(new VideoTubeModel(originDomain, v));
                    v.OriginDomain = originDomain;
                    
                        v.DurationString = DateTimeUtils.PrintTimeSpan(v.Duration);
                    
                    
                });

                var videoModels = new List<VideoTubeModel>();
                var vimeoModels = new List<VideoTubeModel>();

                if (videoPage.VideoTubeModels != null && videoPage.VideoTubeModels.Count!=0)
                {
                    videoPage.VideoTubeModels.ForEach(video =>
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
                //YouTubeServiceManage.UpdateVideoStatus(videoModels);
                //VimeoServiceManage.UpdateVideoStatus(vimeoModels);
            }

            return videoPage;
        }

        public static ChannelScheduleModel CreateChannelScheduleOnAutoPilot(int channelTubeId, DateTime startDateAndTime)
        {
            Logger.Info(String.Format("Creating schedule for channel tube with Id={0} as of date='{1}'", channelTubeId, startDateAndTime.ToLongTimeString()));

            ChannelScheduleModel channelScheduleModel = null;

            using (var channelScheduleRepository = new ChannelScheduleRepository())
            {
                var calendarEventForTargetDate = ScheduleManage.GetChannelTubeScheduleCalendarEvents(startDateAndTime.Day, startDateAndTime.Month, startDateAndTime.Year, channelTubeId);

                if (calendarEventForTargetDate == null || calendarEventForTargetDate.Count == 0)
                {
                    var channelSchedule = channelScheduleRepository.InsertChannelSchedule(channelTubeId, startDateAndTime);

                    if (channelSchedule != null)
                    {
                        channelScheduleModel = new ChannelScheduleModel(channelSchedule)
                        {
                            AllowRepeat = false,
                            AllowEdit = true,
                            AllowDelete = true
                        };
                    }
                    else
                    {
                        channelScheduleModel = new ChannelScheduleModel()
                        {
                            Message = "Failed to create new schedule."
                        };
                    }
                }
                else
                {
                    channelScheduleModel = new ChannelScheduleModel()
                    {
                        Message = "Failed to create new schedule, schedule already exists for the same date and time."
                    };
                }
            }

            return channelScheduleModel;
        }

        public static int CompareScheduleTimeToDate(DateTime scheduleTime, DateTime startTime)
        {
            var schedule = scheduleTime.AddSeconds(-scheduleTime.Second).AddMilliseconds(-scheduleTime.Millisecond);
            var start = startTime.AddSeconds(-startTime.Second).AddMilliseconds(-startTime.Millisecond);

            return schedule.CompareTo(start);
        }

        public static ChannelScheduleModel CreateChannelSchedule(int channelTubeId, DateTime startDateAndTime)
        {
            Logger.Info(String.Format("Creating schedule for channel tube with Id={0} as of date='{1}'", channelTubeId, startDateAndTime.ToLongTimeString()));

            ChannelScheduleModel channelScheduleModel = null;

            using (var channelScheduleRepository = new ChannelScheduleRepository())
            {
                var existingSchedulesForTargetDate = ScheduleManage.GetChannelTubeSchedulesByDate(channelTubeId, startDateAndTime);

                if (existingSchedulesForTargetDate != null &&
                    !existingSchedulesForTargetDate.Any(x => ((CompareScheduleTimeToDate(x.StartDateAndTime,startDateAndTime) == -1 || CompareScheduleTimeToDate(x.StartDateAndTime,startDateAndTime) == 0) && 
                                                              x.VideoSchedules.Count() > 0 && 
                                                              x.VideoSchedules.Last().PlaybackEndTime >= startDateAndTime) ||
                                                             (CompareScheduleTimeToDate(x.StartDateAndTime,startDateAndTime) == 0 &&
                                                              x.VideoSchedules.Count() == 0)))
                {
                    var channelSchedule = channelScheduleRepository.InsertChannelSchedule(channelTubeId, startDateAndTime);

                    if (channelSchedule != null)
                    {
                        channelScheduleModel = new ChannelScheduleModel(channelSchedule)
                        {
                            AllowRepeat = false,
                            AllowEdit = true,
                            AllowDelete = true
                        };
                    }
                    else
                    {
                        channelScheduleModel = new ChannelScheduleModel()
                        {
                            Message = "Failed to create new schedule."
                        };
                    }
                }
                else
                {
                    channelScheduleModel = new ChannelScheduleModel()
                    {
                        Message = "Failed to create new schedule, schedule already exists for the same date and time."
                    };
                }
            }

            return channelScheduleModel;
        }

        public static ChannelScheduleModel CreateInstantSchedule(int channelTubeId, DateTime startDateAndTime)
        {
            Logger.Info(String.Format("Creating instant schedule for channel tube with Id={0}", channelTubeId));

            // Create channel schedule as of now
            var channelSchedule = ScheduleManage.CreateChannelSchedule(channelTubeId, startDateAndTime);

            Logger.Debug(String.Format("Created channel schedule? {0}", channelSchedule != null && channelSchedule.ChannelScheduleId > 0));

            if (channelSchedule != null && channelSchedule.ChannelScheduleId > 0)
            {
                // Add random videos to new schedule
                var videoTubesForChannel = VideoTubeManage.GetVideoTubesForChannel(channelTubeId);

                Logger.Debug(String.Format("Retrieved {0} video tubes for channel id={1}", videoTubesForChannel != null ? videoTubesForChannel.Count : 0, channelTubeId));

                if (videoTubesForChannel != null && videoTubesForChannel.Count > 0)
                {
                    var videoModels = new List<VideoTubeModel>();
                    var sortedVideosByLastScheduled  = videoTubesForChannel.OrderBy(x => x.LastScheduleDateTime).ToList();

                    sortedVideosByLastScheduled.ForEach(x => {
                        videoModels.Add(new VideoTubeModel(AWS_CLOUD_FRONT_WEB_DIST_DOMAIN, x) { IsInChannel = true });
                    });

                    Logger.Debug(String.Format("Converted {0} video tubes to {0} video models. Checking their video status", sortedVideosByLastScheduled.Count, videoModels.Count));

                    // Build channel schedule model
                    var youtubeModels = new List<VideoTubeModel>();
                    var vimeoModels = new List<VideoTubeModel>();

                    if (videoModels != null && videoModels.Count > 0)
                    {
                        videoModels.ForEach(video =>
                        {
                            if (video.VideoProviderName == "vimeo")
                            {
                                vimeoModels.Add(video);
                            }
                            else if(video.VideoProviderName=="youtube")
                            {
                                youtubeModels.Add(video);
                            }
                        });
                    }

                    //MST: 03152017 - Turning off update all status updates because Vimeo started to throttle us. Th
                    //these updates will be done with JAS by throttling requests to both providers
                    //YouTubeServiceManage.UpdateVideoStatus(youtubeModels);
                    //VimeoServiceManage.UpdateVideoStatus(vimeoModels);
              
                    var videosAvailableToBeScheduled = videoModels.Where(x => x != null && !x.IsRemovedByProvider && !x.IsRestrictedByProvider);

                    Logger.Debug(String.Format("Out of {0} videos, only {1} can be scheduled", videoModels.Count, videosAvailableToBeScheduled.ToList().Count));

                    if (videosAvailableToBeScheduled.Count() >= 3)
                    {
                        Logger.Debug("There are more than 3 videos available for scheduling. Adding videos to channel schedule");

                        var existingSchedules = ScheduleManage.GetChannelTubeSchedulesByDate(channelTubeId, startDateAndTime);

                        // Find next schedule and get its start time
                        var nextSchedule = existingSchedules.OrderBy(x => x.StartDateAndTime).FirstOrDefault(x => x.StartDateAndTime > startDateAndTime && x.ChannelScheduleId != channelSchedule.ChannelScheduleId);

                        var videosToAddToSchedule = new List<VideoTubeModel>();

                        DateTime endOfTheDay = startDateAndTime.AddHours(-startDateAndTime.Hour)
                                                               .AddMinutes(-(startDateAndTime.Minute + 1))
                                                               .AddSeconds(-startDateAndTime.Second)
                                                               .AddDays(1);

                        if (nextSchedule != null && endOfTheDay > nextSchedule.StartDateAndTime)
                        {
                            endOfTheDay = nextSchedule.StartDateAndTime;
                        }

                        double totalTimeAvailableForSchedulingInSec = (endOfTheDay - startDateAndTime).TotalSeconds;

                        // Assuming that recently used videos constitute videos that were used in the schedule
                        // during the last 2 days
                        var recentlyUsedVideos = (from video in videosAvailableToBeScheduled
                                                  where video.LastScheduleDateTime != null && (DateTime.Now - video.LastScheduleDateTime.Value).TotalDays <= 2 
                                                  select video).ToList();

                        Logger.Debug(String.Format("There are {0} recently used videos out of {1} in the channel with id={2}", recentlyUsedVideos.Count, videosAvailableToBeScheduled.ToList().Count, channelTubeId));

                        List<VideoTubeModel> videosToUseForScheduling = new List<VideoTubeModel>();

                        if (recentlyUsedVideos != null && (recentlyUsedVideos.Count == videosAvailableToBeScheduled.Count() || recentlyUsedVideos.Count == 0)) {
                            videosToUseForScheduling = videosAvailableToBeScheduled.ToList();
                        }
                        else {
                            videosToUseForScheduling = videosAvailableToBeScheduled.Except<VideoTubeModel>(recentlyUsedVideos).ToList();
                                                        //(from video in videosAvailableToBeScheduled
                                                        //where video.LastScheduleDateTime == null || (video.LastScheduleDateTime != null && (DateTime.Now - video.LastScheduleDateTime.Value).TotalDays > 2)
                                                        //select video).ToList();
                        }

                        var firstVideo = videosAvailableToBeScheduled.FirstOrDefault();
                        var lastVideo = videosAvailableToBeScheduled.LastOrDefault();

                        var firstTime = firstVideo != null ? firstVideo.LastScheduleDateTime.ToString() : "NOT SET";
                        var lastTime = lastVideo != null ? lastVideo.LastScheduleDateTime.ToString() : "NOT SET";

                        Logger.Debug(String.Format("Last Schedule time for the 1st video is '{0}' and for the last video in the collection is '{1}'", firstTime, lastTime));

                        //videosToUseForScheduling = videosAvailableToBeScheduled.OrderBy(x => x.LastScheduleDateTime).ToList();

                        // First process videos that were not recently used in the channel schedule, meaning their LastScheduleDateTime is null
                        //
                        videosToUseForScheduling = videosToUseForScheduling.Shuffle<VideoTubeModel>().ToList();
                        recentlyUsedVideos = recentlyUsedVideos.Shuffle<VideoTubeModel>().ToList();
                        videosToUseForScheduling.AddRange(recentlyUsedVideos);
                        
                        do
                        {
                            totalTimeAvailableForSchedulingInSec = AddVideosToSchedule(ref videosToUseForScheduling, videosToAddToSchedule, totalTimeAvailableForSchedulingInSec);
                        }
                        while (totalTimeAvailableForSchedulingInSec > 0 && videosToUseForScheduling.Any(x => x.Duration < totalTimeAvailableForSchedulingInSec));

                        int newVideoScheduledCount = videosToAddToSchedule.Distinct().Count();

                        Logger.Debug(String.Format("Number of videos that were not recently used scheduled is {0} from {1} available to be scheduled", newVideoScheduledCount, videosToUseForScheduling.Count));


                        if (videosToAddToSchedule.Count() > 0)
                        {
                            var videoSchedules = VideoTubeManage.AddMultipleVideosToChannelSchedule(videosToAddToSchedule.ToList(), channelSchedule.ChannelScheduleId);

                            if (videoSchedules != null && videoSchedules.Count > 0)
                            {
                                Logger.Debug(String.Format("Created {0} video schedules for channel id={1} and channel schedule id={2}", videoSchedules.Count, channelTubeId, channelSchedule.ChannelScheduleId));

                                channelSchedule.VideoSchedules = new List<VideoScheduleModel>();
                                
                                videoSchedules.OrderBy(x => x.PlaybackOrderNumber).ToList().ForEach(v =>
                                {
                                    channelSchedule.VideoSchedules.Add(new VideoScheduleModel(AWS_CLOUD_FRONT_WEB_DIST_DOMAIN, v)
                                    {
                                        AllowDeleted = true
                                    });
                                });

                                channelSchedule.EndDateAndTime = videoSchedules.Count > 0 ? videoSchedules.Last().PlaybackEndTime : new Nullable<DateTime>();
                            }
                        }
                        else
                        {
                            channelSchedule.Message = "Unable to add selected videos to the schedule, not enough time available for the programming.";
                        }
                    }
                    else
                    {
                        channelSchedule.Message = "Please select at least 3 unrestricted viodeos.";
                    }
                }
            }

            return channelSchedule;
        }
        public static void RemoveVideoFromChannelScheduleOnDrop(int channelScheduleId, int videoTubeId, int playbackOrderNumber)
        {
            using (var channelScheduleRepository = new ChannelScheduleRepository())
            {
                channelScheduleRepository.DeleteVideoFromScheduleOnDrop(channelScheduleId, videoTubeId, playbackOrderNumber);
            }
        }
        
        public static void ReorderSchedule(int channelScheduleId, int targetPlaybackOrderNumber, int playbackOrderNumber, int videoTubeId)
        {
            using (var channelScheduleRepository = new ChannelScheduleRepository())
            {
                channelScheduleRepository.ReorderSchedule(channelScheduleId, targetPlaybackOrderNumber, playbackOrderNumber, videoTubeId);
            }
        }

        private static double AddVideosToSchedule(ref List<VideoTubeModel> videosAvailableForScheduling, List<VideoTubeModel> videosInSchedule, double timeAvailableForProgramming)
        {
            double timeLeft = timeAvailableForProgramming;
            VideoTubeModel lastVideoAdded = videosInSchedule.Count > 0 ? videosInSchedule.Last() : null;

            int countOfVideosAdded = 0;

            videosAvailableForScheduling.ForEach(v =>
            {
                if (v.Duration <= timeLeft && (lastVideoAdded == null || lastVideoAdded.VideoTubeId != v.VideoTubeId) && (v.Duration != 0))
                {
                    videosInSchedule.Add(v);
                    timeLeft -= v.Duration;
                    countOfVideosAdded++;
                }
            });

            if (countOfVideosAdded == 0)
            {
                timeLeft = 0;
            }

            return timeLeft;
        }

        public static ChannelScheduleModel AddVideoToChannelSchedule(int channelScheduleId, int videoTubeId)
        {
            Logger.Info(String.Format("Adding video tube with Id={0} to channel schedule with Id={1}", videoTubeId, channelScheduleId));

            ChannelScheduleModel channelScheduleModel;

            using (var videoTubeRepository = new VideoTubeRepository())
            using (var channelScheduleRepository = new ChannelScheduleRepository())
            using (var videoScheduleRepository = new VideoScheduleRepository())
            {
                bool isNextDay = false;
                bool isLastVideoOfTheDay = false;

                var channelSchedule = channelScheduleRepository.GetChannelSchedulePoById(channelScheduleId);
                var video = videoTubeRepository.GetVideoTubeById(videoTubeId);

                DateTime newEndDate = channelSchedule.EndTime.AddSeconds(video.Duration);

                var lastEndTime_tt = channelSchedule.EndTime.ToString("tt", System.Globalization.CultureInfo.InvariantCulture);
                var newEndTime_tt = newEndDate.ToString("tt", System.Globalization.CultureInfo.InvariantCulture);

                isNextDay = (lastEndTime_tt != newEndTime_tt && newEndTime_tt == "AM");

                DateTime endOfTheDay = channelSchedule.EndTime.AddHours(-channelSchedule.EndTime.Hour)
                                       .AddMinutes(-(channelSchedule.EndTime.Minute))
                                       .AddSeconds(-channelSchedule.EndTime.Second)
                                       .AddHours(24);

                if ((!isNextDay && channelSchedule.StartTime.Day == newEndDate.Day) || (newEndDate.Minute == 0 && isNextDay))
                {
                    var existingSchedules = ScheduleManage.GetChannelTubeSchedulesByDate(channelSchedule.ChannelTubeId, channelSchedule.StartTime);
                    var currentEndTimeOfTargetSchedule = channelSchedule.EndTime;
                    var newEndTime = currentEndTimeOfTargetSchedule.AddSeconds(video.Duration);

                    if (isLastVideoOfTheDay)
                    {
                        newEndTime.AddDays(1);
                    }

                    if (!existingSchedules.Any(x => (CompareScheduleTimeToDate(x.StartDateAndTime, currentEndTimeOfTargetSchedule) == 1 &&
                                                     CompareScheduleTimeToDate(x.StartDateAndTime, newEndTime) == -1) ||
                                                    (CompareScheduleTimeToDate(x.StartDateAndTime, currentEndTimeOfTargetSchedule) == 1 &&
                                                     CompareScheduleTimeToDate(x.StartDateAndTime, newEndTime) == 0)))
                    {
                        if (channelSchedule != null)
                        {
                            channelScheduleModel = new ChannelScheduleModel(channelSchedule)
                            {
                                AllowDelete = !channelSchedule.CreatedDate.Value.IsPriorTo(DateTime.Now),
                                AllowEdit = !channelSchedule.CreatedDate.Value.IsPriorTo(DateTime.Now),
                                AllowRepeat = true
                            };

                            var videoSchedules = videoScheduleRepository.AddVideoTubeToChannelScheduleById(channelScheduleId, videoTubeId);

                            if (videoSchedules != null && videoSchedules.Count > 0)
                            {
                                videoSchedules.ForEach(v =>
                                {
                                    channelScheduleModel.VideoSchedules.Add(new VideoScheduleModel(AWS_CLOUD_FRONT_WEB_DIST_DOMAIN, v)
                                    {
                                        AllowDeleted = true
                                    });
                                });
                            }

                            channelScheduleModel.EndDateAndTime = channelScheduleModel.VideoSchedules.Count > 0 ? channelScheduleModel.VideoSchedules.Last().PlaybackEndTime : new Nullable<DateTime>();
                        }
                        else
                        {
                            channelScheduleModel = new ChannelScheduleModel()
                            {
                                Message = "Error occured while adding selected video to the schedule. Please try again."
                            };
                        }
                    }
                    else
                    {
                        channelScheduleModel = new ChannelScheduleModel()
                        {
                            Message = "Unable to add the video, video duration is too long. Video overlaps with the start of the next schedule. Please choose a different video."
                        };
                    }
                }
                else
                {
                    channelScheduleModel = new ChannelScheduleModel()
                    {
                        Message = "Unable to add the video, video duration is too long. Schedule can't go past midnight. Please choose a different video."
                    };
                }
            }

            return channelScheduleModel;
        }

        public static ChannelScheduleModel AddVideoToChannelScheduleOnDrop(int channelScheduleId, int videoTubeId, int orderNumber)
        {
            Logger.Info(String.Format("Adding video tube with Id={0} to channel schedule with Id={1}", videoTubeId, channelScheduleId));

            ChannelScheduleModel channelScheduleModel;

            using (var videoTubeRepository = new VideoTubeRepository())
            using (var channelScheduleRepository = new ChannelScheduleRepository())
            using (var videoScheduleRepository = new VideoScheduleRepository())
            {
                bool isNextDay = false;
                bool isLastVideoOfTheDay = false;

                var channelSchedule = channelScheduleRepository.GetChannelSchedulePoById(channelScheduleId);
                var video = videoTubeRepository.GetVideoTubeById(videoTubeId);

                DateTime newEndDate = channelSchedule.EndTime.AddSeconds(video.Duration);

                var lastEndTime_tt = channelSchedule.EndTime.ToString("tt", System.Globalization.CultureInfo.InvariantCulture);
                var newEndTime_tt = newEndDate.ToString("tt", System.Globalization.CultureInfo.InvariantCulture);

                isNextDay = (lastEndTime_tt != newEndTime_tt && newEndTime_tt == "AM");

                DateTime endOfTheDay = channelSchedule.EndTime.AddHours(-channelSchedule.EndTime.Hour)
                                       .AddMinutes(-(channelSchedule.EndTime.Minute))
                                       .AddSeconds(-channelSchedule.EndTime.Second)
                                       .AddHours(24);

                if ((!isNextDay && channelSchedule.StartTime.Day == newEndDate.Day) || (newEndDate.Minute == 0 && isNextDay))
                {
                    var existingSchedules = ScheduleManage.GetChannelTubeSchedulesByDate(channelSchedule.ChannelTubeId, channelSchedule.StartTime);
                    var currentEndTimeOfTargetSchedule = channelSchedule.EndTime;
                    var newEndTime = currentEndTimeOfTargetSchedule.AddSeconds(video.Duration);

                    if (isLastVideoOfTheDay)
                    {
                        newEndTime.AddDays(1);
                    }

                    if (!existingSchedules.Any(x => (CompareScheduleTimeToDate(x.StartDateAndTime, currentEndTimeOfTargetSchedule) == 1 &&
                                                     CompareScheduleTimeToDate(x.StartDateAndTime, newEndTime) == -1) ||
                                                    (CompareScheduleTimeToDate(x.StartDateAndTime, currentEndTimeOfTargetSchedule) == 1 &&
                                                     CompareScheduleTimeToDate(x.StartDateAndTime, newEndTime) == 0)))
                    {
                        if (channelSchedule != null)
                        {
                            channelScheduleModel = new ChannelScheduleModel(channelSchedule)
                            {
                                AllowDelete = !channelSchedule.CreatedDate.Value.IsPriorTo(DateTime.Now),
                                AllowEdit = !channelSchedule.CreatedDate.Value.IsPriorTo(DateTime.Now),
                                AllowRepeat = true
                            };

                            var videoSchedules = videoScheduleRepository.AddVideoTubeToChannelScheduleById(channelScheduleId, videoTubeId, orderNumber);

                            if (videoSchedules != null && videoSchedules.Count > 0)
                            {
                                videoSchedules.ForEach(v =>
                                {
                                    channelScheduleModel.VideoSchedules.Add(new VideoScheduleModel(AWS_CLOUD_FRONT_WEB_DIST_DOMAIN, v)
                                    {
                                        AllowDeleted = true
                                    });
                                });
                            }

                            channelScheduleModel.EndDateAndTime = channelScheduleModel.VideoSchedules.Count > 0 ? channelScheduleModel.VideoSchedules.Last().PlaybackEndTime : new Nullable<DateTime>();
                        }
                        else
                        {
                            channelScheduleModel = new ChannelScheduleModel()
                            {
                                Message = "Error occured while adding selected video to the schedule. Please try again."
                            };
                        }
                    }
                    else
                    {
                        channelScheduleModel = new ChannelScheduleModel()
                        {
                            Message = "Unable to add the video, video duration is too long. Video overlaps with the start of the next schedule. Please choose a different video."
                        };
                    }
                }
                else
                {
                    channelScheduleModel = new ChannelScheduleModel()
                    {
                        Message = "Unable to add the video, video duration is too long. Schedule can't go past midnight. Please choose a different video."
                    };
                }
            }

            return channelScheduleModel;
        }


        public static ChannelScheduleModel RemoveVideoFromChannelSchedule(int channelScheduleId, int videoTubeId, int playbackOrderNumber)
        {
            Logger.Info(String.Format("Removing video tube with Id={0} from channel schedule with Id={1} and playback order number of {2}", videoTubeId, channelScheduleId, playbackOrderNumber));

            ChannelScheduleModel channelScheduleModel;

            using (var channelScheduleRepository = new ChannelScheduleRepository())
            using (var videoScheduleRepository = new VideoScheduleRepository())
            {
                var channelSchedule = channelScheduleRepository.GetChannelScheduleById(channelScheduleId);

                if (channelSchedule != null)
                {
                    channelScheduleModel = new ChannelScheduleModel(channelSchedule)
                    {
                        AllowDelete = !channelSchedule.CreatedDate.Value.IsPriorTo(DateTime.Now),
                        AllowEdit = !channelSchedule.CreatedDate.Value.IsPriorTo(DateTime.Now),
                        AllowRepeat = true
                    };

                    var videoSchedules = videoScheduleRepository.DeleteVideoTubeFromChannelScheduleById(channelScheduleId, videoTubeId, playbackOrderNumber);

                    if (videoSchedules != null && videoSchedules.Count > 0)
                    {
                        videoSchedules.ForEach(v =>
                        {
                            channelScheduleModel.VideoSchedules.Add(new VideoScheduleModel(AWS_CLOUD_FRONT_WEB_DIST_DOMAIN, v)
                            {
                                AllowDeleted = !v.PlaybackStartTime.IsPriorTo(DateTime.Now)
                            });
                        });

                        channelScheduleModel.EndDateAndTime = channelScheduleModel.VideoSchedules.Count > 0 ? channelScheduleModel.VideoSchedules.Last().PlaybackEndTime : new Nullable<DateTime>();
                    }
                }
                else
                {
                    channelScheduleModel = new ChannelScheduleModel()
                    {
                        Message = "Error occurred, video was not deleted from channel's schedule."
                    };
                }
            }

            return channelScheduleModel;
        }

        public static ChannelScheduleModel RemoveVideosFromChannelScheduleFromOrderNumber(int channelScheduleId, int playbackOrderNumber)
        {
            Logger.Info(String.Format("Removing videos  from channel schedule with Id={0} and playback order number of {1}", channelScheduleId, playbackOrderNumber));

            ChannelScheduleModel channelScheduleModel;

            using (var channelScheduleRepository = new ChannelScheduleRepository())
            using (var videoScheduleRepository = new VideoScheduleRepository())
            {
                var channelSchedule = channelScheduleRepository.GetChannelScheduleById(channelScheduleId);

                if (channelSchedule != null)
                {
                    channelScheduleModel = new ChannelScheduleModel(channelSchedule)
                    {
                        AllowDelete = !channelSchedule.CreatedDate.Value.IsPriorTo(DateTime.Now),
                        AllowEdit = !channelSchedule.CreatedDate.Value.IsPriorTo(DateTime.Now),
                        AllowRepeat = true
                    };

                    var videoSchedules = videoScheduleRepository.DeleteVideoTubeFromChannelScheduleByFromOrderNumberIdWithGet(channelScheduleId, playbackOrderNumber);

                    if (videoSchedules != null && videoSchedules.Count > 0)
                    {
                        videoSchedules.ForEach(v =>
                        {
                            channelScheduleModel.VideoSchedules.Add(new VideoScheduleModel(AWS_CLOUD_FRONT_WEB_DIST_DOMAIN, v)
                            {
                                AllowDeleted = !v.PlaybackStartTime.IsPriorTo(DateTime.Now)
                            });
                        });

                        channelScheduleModel.EndDateAndTime = channelScheduleModel.VideoSchedules.Count > 0 ? channelScheduleModel.VideoSchedules.Last().PlaybackEndTime : new Nullable<DateTime>();
                    }
                }
                else
                {
                    channelScheduleModel = new ChannelScheduleModel()
                    {
                        Message = "Error occurred, video was not deleted from channel's schedule."
                    };
                }
            }

            return channelScheduleModel;
        }

        public static ChannelScheduleModel ClearChannelScheduleById(int channelScheduleId)
        {
            Logger.Info(String.Format("Removing video tube from channel schedule with Id={0}", channelScheduleId));

            ChannelScheduleModel channelScheduleModel;

            using (var channelScheduleRepository = new ChannelScheduleRepository())
            using (var videoScheduleRepository = new VideoScheduleRepository())
            {
                var channelSchedule = channelScheduleRepository.GetChannelScheduleById(channelScheduleId);

                if (channelSchedule != null)
                {
                    channelScheduleModel = new ChannelScheduleModel(channelSchedule)
                    {
                        AllowDelete = true,
                        AllowEdit = !channelSchedule.CreatedDate.Value.IsPriorTo(DateTime.Now),
                        AllowRepeat = true
                    };

                    var videoSchedules = videoScheduleRepository.DeleteAllVideoTubesFromChannelScheduleById(channelScheduleId);

                    if (videoSchedules != null && videoSchedules.Count > 0)
                    {
                        videoSchedules.ForEach(v =>
                        {
                            channelScheduleModel.VideoSchedules.Add(new VideoScheduleModel(AWS_CLOUD_FRONT_WEB_DIST_DOMAIN, v)
                            {
                                AllowDeleted = !v.PlaybackStartTime.IsPriorTo(DateTime.Now)
                            });
                        });
                    }
                }
                else
                {
                    channelScheduleModel = new ChannelScheduleModel()
                    {
                        Message = "Error occured while deleting all video from channel's schedule"
                    };
                }
            }

            return channelScheduleModel;
        }

        public static void DeleteEmptySchedulesForChannelOnOrBeforeDate(int channelTubeId, DateTime scheduleDate)
        {
            using (var channelScheduleRepository = new ChannelScheduleRepository())
            {
                channelScheduleRepository.DeleteEmptySchedulesByChannelIdOnOrBeforeDate(channelTubeId, scheduleDate);
            }
        }

        public static List<ChannelScheduleModel> GetChannelTubeSchedulesByDate(int channelTubeId, DateTime scheduleDate)
        {
            Logger.Info(String.Format("Retrieving full channel schedules for date='{0}' and channelId={1}", scheduleDate, channelTubeId));

            var scheduleModels = new List<ChannelScheduleModel>();

            using (var channelScheduleRepository = new ChannelScheduleRepository())
            using (var videoScheduleRepository = new VideoScheduleRepository())
            {
                var schedules = channelScheduleRepository.GetChannelSchedulesByDateAndChannelTubeId(scheduleDate, channelTubeId);

                schedules.ForEach(x =>
                {
                    var scheduleModel = new ChannelScheduleModel(x)
                    {
                        AllowDelete = !x.CreatedDate.Value.IsPriorTo(DateTime.Now),
                        AllowEdit = !x.CreatedDate.Value.IsPriorTo(DateTime.Now),
                        AllowRepeat = true
                    };

                    var videoSchedules = videoScheduleRepository.GetVideoSchedulesByChannelScheduleId(x.ChannelScheduleId);
                    int playbackOrderNumberOfNextPlayingVideo = -1;

                    videoSchedules.ForEach(v =>
                    {
                        bool isPlayingNow = v.PlaybackStartTime <= scheduleDate && v.PlaybackEndTime > scheduleDate;

                        if (isPlayingNow)
                        {
                            playbackOrderNumberOfNextPlayingVideo = v.PlaybackOrderNumber + 1;
                        }

                        var videoScheduleModel = new VideoScheduleModel(AWS_CLOUD_FRONT_WEB_DIST_DOMAIN, v)
                        {
                            AllowDeleted = !v.PlaybackStartTime.IsPriorTo(DateTime.Now),
                            PlaytimeMessage = DateTimeUtils.PrintPlaytimeDuration(v.PlaybackStartTime, v.PlaybackEndTime),
                            IsPlayingNow = isPlayingNow,
                            IsPlayingNext = v.PlaybackOrderNumber == playbackOrderNumberOfNextPlayingVideo
                        };
                        scheduleModel.VideoSchedules.Add(videoScheduleModel);
                    });

                    scheduleModel.EndDateAndTime = scheduleModel.VideoSchedules.Count > 0 ? scheduleModel.VideoSchedules.Last().PlaybackEndTime : new Nullable<DateTime>();

                    scheduleModels.Add(scheduleModel);
                });
            }

            return scheduleModels;
        }

        public static ChannelSchedule GetChannelScheduleByChannelScheduleById(int channelScheduleId)
        {
            Logger.Info(String.Format("Retrieving channel schedule by channelscheduleId={0}", channelScheduleId));
             using (var channelScheduleRepository = new ChannelScheduleRepository())
             {
               return channelScheduleRepository.GetChannelScheduleById(channelScheduleId);
             }
        }

        public static List<VideoScheduleModel> GetVideoSchedulesForChannelScheduleById(int channelScheduleId)
        {
            Logger.Info(String.Format("Retrieving video schedules for channel schedule with Id={0}", channelScheduleId));

            var videoSchedules = new List<VideoScheduleModel>();

            var schedules = ChannelManage.GetVideoSchedulesByChannelScheduleId(channelScheduleId);

            if (schedules != null && schedules.Count > 0)
            {
                schedules.ForEach(v =>
                {
                    videoSchedules.Add(new VideoScheduleModel(AWS_CLOUD_FRONT_WEB_DIST_DOMAIN, v)
                    {
                        AllowDeleted = !v.PlaybackStartTime.IsPriorTo(DateTime.Now)
                    });
                });
            }            

            return videoSchedules;
        }

        public static bool RemoveChannelSchedule(int channelScheduleId)
        {
            Logger.Info(String.Format("Deleting channel schedule with Id={0}", channelScheduleId));

            bool isSuccessfull = false;

            using (var channelScheduleRepository = new ChannelScheduleRepository())
            {
                isSuccessfull = channelScheduleRepository.DeleteChannelScheduleById(channelScheduleId);
            }

            return isSuccessfull;
        }

        public static bool PublishOrUnpublishChannelSchedule(int channelScheduleId, bool shouldPublish)
        {
            Logger.Info(String.Format("Updating channel schedule by publishing it? {0}", shouldPublish));

            bool isSuccessful = false;

            using (var channelScheduleRepository = new ChannelScheduleRepository())
            {
                isSuccessful = channelScheduleRepository.UpdatePublishFlagForChannelScheduleById(channelScheduleId, shouldPublish);

                if (shouldPublish)
                {
                    VideoTubeManage.UpdateVideoTubeLastScheduleDateTimeByScheduleId(channelScheduleId);
            }
            }

            return isSuccessful;
        }

        public static ChannelScheduleModel UpdateChannelScheduleStartDateAndTime(int channelScheduleId, DateTime startDateAndTime)
        {
            Logger.Info(String.Format("Updating start date and time for channel schedule with Id={0} to '{1}'", channelScheduleId, startDateAndTime));

            ChannelScheduleModel channelSchedule = null;

            using (var videoScheduleRepository = new VideoScheduleRepository())
            using (var channelScheduleRepository = new ChannelScheduleRepository())
            {
                var schedule = channelScheduleRepository.UpdateChannelScheduleStartDateAndTimeById(channelScheduleId, startDateAndTime);

                if (schedule != null)
                {
                    channelSchedule = new ChannelScheduleModel(schedule)
                    {
                        AllowDelete = !schedule.CreatedDate.Value.IsPriorTo(DateTime.Now),
                        AllowEdit = !schedule.CreatedDate.Value.IsPriorTo(DateTime.Now),
                        AllowRepeat = true
                    };

                    var videoSchedules = videoScheduleRepository.GetVideoSchedulesByChannelScheduleId(schedule.ChannelScheduleId);

                    videoSchedules.ForEach(v =>
                    {
                        var videoScheduleModel = new VideoScheduleModel(AWS_CLOUD_FRONT_WEB_DIST_DOMAIN, v)
                        {
                            AllowDeleted = !v.PlaybackStartTime.IsPriorTo(DateTime.Now),
                            PlaytimeMessage = DateTimeUtils.PrintPlaytimeDuration(v.PlaybackStartTime, v.PlaybackEndTime)
                        };

                        channelSchedule.VideoSchedules.Add(videoScheduleModel);
                    });

                }
            }

            return channelSchedule;
        }

        public static List<UnpublishedChannelSchedulePo> GetAllUnpublishedFutureSchedules()
        {
            Logger.Info("Retrieving all future channel schedules that were not published yet");

            List<UnpublishedChannelSchedulePo> schedules = new List<UnpublishedChannelSchedulePo>();

            using (var channelScheduleRepository = new ChannelScheduleRepository())
            {
                schedules = channelScheduleRepository.GetAllUnpublishedFutureSchedules();
            }

            return schedules;
        }

        public static List<UnpublishedChannelScheduleEmailPo> GetUnpublishedSchedulesWithSentEmailCounts()
        {
            Logger.Info("Retrieving all unpublished channel schedules with number of emails sent for each channel");

            List<UnpublishedChannelScheduleEmailPo> schedulesWithEmailCounts = new List<UnpublishedChannelScheduleEmailPo>();

            using (var channelScheduleRepository = new ChannelScheduleRepository())
            {
                schedulesWithEmailCounts = channelScheduleRepository.GetUnpublishedChannelScheduleEmailPoForAllUnpublishedChannelSchedules();
            }

            return schedulesWithEmailCounts;
        }

        public static ChannelScheduleModel RepeatChannelSchedule(int channelScheduleId, int channelTubeId, DateTime targetDate)
        {
            Logger.Info(String.Format("Repeating existing schedule with Id={0} for channel Tube with Id={1} on '{2}", channelScheduleId, channelTubeId, targetDate.ToString("MM/dd/yyyy")));

            ChannelScheduleModel channelSchedule = null;

            using (var channelTubeRepository = new ChannelTubeRepository())
            using (var videoScheduleRepository = new VideoScheduleRepository())
            using (var channelScheduleRepository = new ChannelScheduleRepository())
            {
                var schedule = channelScheduleRepository.GetChannelScheduleById(channelScheduleId);
                var channel = channelTubeRepository.GetChannelTubeById(channelTubeId);

                if (schedule != null)
                {
                    if (schedule.ChannelTubeId == channelTubeId)
                    {
                        if (channel != null)
                        {
                            //if (!channel.IsAutoPilotOn)
                            //{
                            var targetDateScheduleEvents = GetChannelTubeSchedulesByDate(channelTubeId, targetDate);

                                targetDate = targetDate.AddHours(-targetDate.Hour).AddHours(schedule.StartTime.Hour)
                                                       .AddMinutes(-targetDate.Minute).AddMinutes(schedule.StartTime.Minute)
                                                       .AddSeconds(-targetDate.Second).AddSeconds(schedule.StartTime.Second);

                             if (targetDateScheduleEvents == null || targetDateScheduleEvents.Count == 0 ||
                                !targetDateScheduleEvents.Any(x => x.StartDateAndTime <= targetDate && x.EndDateAndTime > targetDate))
                             {
                                 schedule = channelScheduleRepository.RepeatChannelScheduleByChannelScheduleIdAndTargetDateWithGet(channelScheduleId, targetDate);

                                 if (schedule != null)
                                 {
                                     channelSchedule = new ChannelScheduleModel()
                                     {
                                         AllowDelete = !schedule.CreatedDate.Value.IsPriorTo(DateTime.Now),
                                         AllowEdit = !schedule.CreatedDate.Value.IsPriorTo(DateTime.Now),
                                         AllowRepeat = true,
                                         ChannelScheduleId = schedule.ChannelScheduleId,
                                         ChannelTubeId = schedule.ChannelTubeId,
                                         VideoSchedules = new List<VideoScheduleModel>()
                                     };

                                     var videoSchedules = videoScheduleRepository.GetVideoSchedulesByChannelScheduleId(schedule.ChannelScheduleId);

                                     videoSchedules.ForEach(v =>
                                     {
                                         var videoScheduleModel = new VideoScheduleModel(AWS_CLOUD_FRONT_WEB_DIST_DOMAIN, v)
                                         {
                                             AllowDeleted = !v.PlaybackStartTime.IsPriorTo(DateTime.Now),
                                             PlaytimeMessage = DateTimeUtils.PrintPlaytimeDuration(v.PlaybackStartTime, v.PlaybackEndTime)
                                         };

                                         channelSchedule.VideoSchedules.Add(videoScheduleModel);
                                     });
                                 }
                                 else
                                 {
                                     channelSchedule = new ChannelScheduleModel()
                                     {
                                         Message = "Error occurred, channel schedule was not repeated."
                                     };
                                 }
                             }
                             else
                             {
                                 channelSchedule = new ChannelScheduleModel()
                                 {
                                     Message = "A schedule already exists for this date and time. Please make another selection."
                                 };
                             }
                            //}
                            //else
                            //{
                            //    channelSchedule = new ChannelScheduleModel()
                            //    {
                            //        Message = "Error occured while handling request. Channel is on Auto-Pilot."
                            //    };
                            //}
                        }
                        else
                        {
                            channelSchedule = new ChannelScheduleModel()
                            {
                                Message = "Error occured while handling request. Invalid channel information specified."
                            };
                        }
                    }
                    else
                    {
                        channelSchedule = new ChannelScheduleModel() {
                            Message = "Error occurred, schedule does not belong to specified channel."
                        };
                    }
                }
                else
                {
                    channelSchedule = new ChannelScheduleModel() {
                        Message = "Error occured while handling request. Source schedule was not found."
                    };
                }
            }

            return channelSchedule;

        }

        public static bool SetAutoPilotForChannelTube(int channelTubeId, bool isAutoPilot)
        {
            Logger.Info(String.Format("Setting isAutoPilot flag for channel with Id={0} to: {1}", channelTubeId, isAutoPilot));

            bool isSuccess = false;

            using (var channelTubeRepository = new ChannelTubeRepository())
            {
                var channel = channelTubeRepository.GetChannelTubeById(channelTubeId);

                if (channel != null)
                {
                    channel.IsAutoPilotOn = isAutoPilot;
                    isSuccess = channelTubeRepository.UpdateChannelTube(channel);
                }
                else
                {
                    Logger.Warn(String.Format("Failed to set isAutoPilot flag on Channel Tube with Id={0} to: {1}. Channel Tube with Id={0} was not found", channelTubeId, isAutoPilot));
                }
            }

            return isSuccess;
        }

        public static void DeleteAllSchedulesByChannelId(int channelId)
        {
            using (var channelScheduleRepository = new ChannelScheduleRepository())
            {
               
                channelScheduleRepository.DeleteAllSchedulesByChannelId(channelId);
            }
        }

        public static void DeleteChannelSchedulesWithMatureContent(int userId)
        {
            using (var channelScheduleRepository = new ChannelScheduleRepository())
            {
                channelScheduleRepository.DeleteFutureChannelSchedulesWithMatureContentByUserId(userId);
            }
        }

        public static void ArchiveOldChannelSchedules(DateTime priorToDate)
        {
            using (var channelScheduleRepository = new ChannelScheduleRepository())
            {
                channelScheduleRepository.ArchiveOldChannelSchedulesPriorToDate(priorToDate);
            }
        }
    }
}
