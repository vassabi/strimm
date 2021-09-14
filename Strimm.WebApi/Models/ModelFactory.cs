using Strimm.Model;
using Strimm.Model.Projections;
using Strimm.Model.WebModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Routing;

namespace Strimm.WebApi.Models
{
    public class ModelFactory
    {
        private UrlHelper urlHelper;

        public ModelFactory(HttpRequestMessage request)
        {
            this.urlHelper = new UrlHelper(request);
        }

        public ChannelTubeScheduleEventModel CreateChannelTubeScheduleEventModel(ChannelSchedule schedule)
        {
            string url = this.urlHelper.Link("ScheduleEvent", new { ChannelId = schedule.ChannelTubeId, ScheduleDate = schedule.StartTime.ToString("yyyy-MM-dd") });

            return new ChannelTubeScheduleEventModel()
            {
                Url = url,
                ChannelTubeId = schedule.ChannelTubeId,
                StartTime = schedule.StartTime,
                IsActive = schedule.Published
            };
        }

        public IEnumerable<ChannelScheduleModel> CreateChannelScheduleModels(List<VideoSchedule> videoTubeSchedules)
        {
            if (videoTubeSchedules == null)
            {
                return null;
            }

            var channelScheduleModels = new List<ChannelScheduleModel>();

            var videoSchedulesGroupsByScheduleId = from videoSchedule in videoTubeSchedules
                                                        group videoSchedule by videoSchedule.ChannelScheduleId;

            foreach (var videoScheduleGroup in videoSchedulesGroupsByScheduleId)
            {
                var firstVideoSchedule = videoScheduleGroup.ToList().First(x => x.PlaybackOrderNumber == 0 && x.ChannelScheduleId == videoScheduleGroup.Key);
                var channelScheduleModel = new ChannelScheduleModel()
                {
                    Url = this.urlHelper.Link("ChannelSchedule", new { ScheduleId = firstVideoSchedule.ChannelScheduleId }),
                    Message = String.Format("Programming Starts at {0}", firstVideoSchedule.PlaybackStartTime.ToShortTimeString()),
                    ChannelTubeId = firstVideoSchedule.ChannelTubeId,
                    ChannelScheduleId = firstVideoSchedule.ChannelScheduleId,
                    VideoSchedules = new List<VideoScheduleModel>()
                };

                foreach (var videoSchedule in videoScheduleGroup)
                {
                    channelScheduleModel.VideoSchedules.Add(this.CreateVideoScheduleModel(videoSchedule));
                }

                channelScheduleModels.Add(channelScheduleModel);
            }
            
            return channelScheduleModels;
        }

        internal VideoScheduleModel CreateVideoScheduleModel(VideoSchedule videoSchedule)
        {
            var videoScheduleModel = new VideoScheduleModel()
            {
                Url = this.urlHelper.Link("VideoSchedule", new { ScheduleId = videoSchedule.ChannelScheduleId, VideoId = videoSchedule.VideoTubeId, OrderNumber = videoSchedule.PlaybackOrderNumber }),
                CategoryName = videoSchedule.CategoryName,
                VideoTubeId = videoSchedule.VideoTubeId,
                VideoTubeTitle = videoSchedule.VideoTubeTitle,
                VideoProviderName = videoSchedule.VideoProviderName,
                ProviderVideoId = videoSchedule.ProviderVideoId,
                PlaybackOrderNumber = videoSchedule.PlaybackOrderNumber,
                PlaybackStartTime = videoSchedule.PlaybackStartTime,
                PlaybackEndTime = videoSchedule.PlaybackEndTime,
                IsInPublicLibrary = videoSchedule.IsInPublicLibrary,
                IsPrivate = videoSchedule.IsPrivate,
                IsRemovedByProvider = videoSchedule.IsRemovedByProvider,
                IsRRated = videoSchedule.IsRRated,
                Duration = videoSchedule.Duration,
                Description = videoSchedule.Description
            };

            return videoScheduleModel;
        }

        internal IEnumerable<ChannelTubeModel> CreateChannelTubeModels(List<ChannelTubePo> channels)
        {
            var channelTubeModels = new List<ChannelTubeModel>();
            channels.ForEach(x => channelTubeModels.Add(CreateChannelTubeModel(x)));

            return channelTubeModels;
        }

        internal ChannelTubeModel CreateChannelTubeModel(ChannelTubePo channelTubePo)
        {
            return new ChannelTubeModel()
                {
                    Url = this.urlHelper.Link("UserChannel", new { ChannelTubeId = channelTubePo.ChannelTubeId }),
                    CategoryName = channelTubePo.CategoryName,
                    ChannelTubeId = channelTubePo.ChannelTubeId,
                    Description = channelTubePo.Description,
                    IsFeatured = channelTubePo.IsFeatured,
                    IsLocked = channelTubePo.IsLocked,
                    IsPrivate = channelTubePo.IsPrivate,
                    Name = channelTubePo.Name,
                    PictureUrl = channelTubePo.PictureUrl,
                    Rating = channelTubePo.Rating,
                    ChannelUrl = channelTubePo.Url,
                    VideoTubeCount = channelTubePo.VideoTubeCount,
                    SubscriberCount = channelTubePo.SubscriberCount,
                    ChannelViewsCount = channelTubePo.ChannelViewsCount
                };
        }

        internal UserAuthModel CreateUserAuthModel(UserPo user, Guid authToken)
        {
            return new UserAuthModel()
            {
                Url = this.urlHelper.Link("Login", null),
                UserId = user.UserId,
                Email = user.Email,
                Username = user.UserName,
                AuthorizationToken = authToken
            };
        }

        internal VideoTubePageModel CreateVideoPageModel(int pageIndex, int pageCount, int pageSize, List<VideoTubePo> pageOfPublicVideos)
        {
            var videoTubePage = new VideoTubePageModel()
            {
                Url = this.urlHelper.Link("PublicVideoPage", new { PageIndex = pageIndex, PageSize = pageSize }),
                NextPageIndex = pageIndex == pageCount ? pageIndex : pageIndex + 1,
                PageCount = pageCount,
                PageIndex = pageIndex,
                PageSize = pageOfPublicVideos.Count(),
                PrevPageIndex = pageIndex == 1 ? pageIndex : pageIndex - 1,
                VideoTubeModels = new List<VideoTubeModel>()
            };

            pageOfPublicVideos.ForEach(v => videoTubePage.VideoTubeModels.Add(this.CreateVideoTubeModel(v)));

            return videoTubePage;
        }

        internal VideoTubeModel CreateVideoTubeModel(VideoTubePo videoTubePo)
        {
            if (videoTubePo == null)
            {
                return null;
            }

            return new VideoTubeModel()
            {
                Url = this.urlHelper.Link("VideoTube", new { videoId = videoTubePo.VideoTubeId }), 
                CategoryId = videoTubePo.CategoryId,
                CategoryName = videoTubePo.CategoryName,
                Description = videoTubePo.Description,
                Duration = videoTubePo.Duration,
                IsInPublicLibrary = videoTubePo.IsInPublicLibrary,
                IsPrivate = videoTubePo.IsPrivate,
                IsRemovedByProvider = videoTubePo.IsRemovedByProvider,
                IsRestrictedByProvider = videoTubePo.IsRestrictedByProvider,
                IsRRated = videoTubePo.IsRRated,
                IsScheduled = videoTubePo.IsScheduled,
                ProviderVideoId = videoTubePo.ProviderVideoId,
                Thumbnail = videoTubePo.Thumbnail,
                Title = videoTubePo.Title,
                UseCounter = videoTubePo.UseCounter,
                VideoProviderId = videoTubePo.VideoProviderId,
                VideoProviderName = videoTubePo.VideoProviderName,
                VideoTubeId = videoTubePo.VideoTubeId,
                ViewCounter = videoTubePo.ViewCounter
            };
        }
    }
}