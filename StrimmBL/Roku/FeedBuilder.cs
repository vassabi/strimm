using Strimm.Model;
using Strimm.Model.Roku;
using Strimm.Model.WebModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StrimmBL.Roku
{
    public class FeedBuilder
    {
        private RokuFeedModel _rokuFeedModel;
        private List<VideoScheduleModel> _videoSchedules;
        private ChannelTube _channelTube;
        private string _channelStreamLink;

        public FeedBuilder()
        {
            Reset();
        }

        private void Reset()
        {
            _rokuFeedModel = new RokuFeedModel();
        }

        public void Build(List<VideoScheduleModel> videoSchedules)
        {
            _videoSchedules = videoSchedules;
            _rokuFeedModel.providerName = "Strimm Developers";
            _rokuFeedModel.language = "en-US";
            _rokuFeedModel.lastUpdated = DateTime.Now.ToString("yyyy-MM-ddThh:mm:sszz");

            FillShortFormVideos();
            //FillMovies();
            //FillLiveFeed();
            FillPlaylists();
            FillCategories();
        }
        public void Build(ChannelTube channelTube, string channelStreamLink)
        {
            _channelTube = channelTube;
            _channelStreamLink = channelStreamLink;
            _rokuFeedModel.providerName = "Strimm Developers";
            _rokuFeedModel.language = "en-US";
            _rokuFeedModel.lastUpdated = DateTime.Now.ToString("yyyy-MM-ddThh:mm:sszz");

            FillLiveFeed();
            FillPlaylists();
            FillCategories();
        }

        public RokuFeedModel GetRokuFeed()
        {
            var result = _rokuFeedModel;
            Reset();
            return result;
        }

        private void FillCategories()
        {
            var category = new FeedCategoryModel
            {
                name = "Miscellaneous",
                //playlistName = "Entertainment",
                order = "manual",
                query = "movie"
            };

            _rokuFeedModel.categories.Add(category);
        }

        private void FillPlaylists()
        {
            var playlist = new PlaylistModel { name = "Entertainment", itemIds = new List<string>() };

            playlist.itemIds.AddRange(_rokuFeedModel.shortFormVideos.Select(x => x.id).ToList());
            playlist.itemIds.AddRange(_rokuFeedModel.movies.Select(x => x.id).ToList());
            playlist.itemIds.AddRange(_rokuFeedModel.liveFeeds.Select(x => x.id).ToList());


            _rokuFeedModel.playlists.Add(playlist);
        }

        private void FillShortFormVideos()
        {
            var list = new List<ShortFormVideoModel>();

            foreach (var video in _videoSchedules)
            {
                list.Add(new ShortFormVideoModel()
                {
                    id = Guid.NewGuid().ToString(),
                    title = video.VideoTubeTitle,
                    shortDescription = video.Description,
                    thumbnail = video.ThumbnailUrl,
                    genres = new List<string>() { "sports" },
                    tags = new List<string>() { "broadcasts", "live", "technology" },
                    releaseDate = new DateTime(2019, 06, 11).ToString("yyyy-MM-dd"),
                    content = new VideoContentModel
                    {
                        dateAdded = DateTime.Now.ToString("yyyy-MM-ddThh:mm:sszz"),
                        captions = new List<VideoContentCaptionModel>(),
                        duration = video.Duration,
                        adBreaks = null, // new List<TimeSpan>(),
                        videos = new List<VideoContentVideoModel> {
                            new VideoContentVideoModel{
                                url = "https:" + video.ProviderVideoId,
                                quality = "HD",
                                videoType = "HLS"
                            }
                        }
                    }
                });
            }

            _rokuFeedModel.shortFormVideos = list;
        }

        private void FillMovies()
        {
            var list = new List<MoviesModel>();

            foreach (var video in _videoSchedules)
            {
                list.Add(new MoviesModel()
                {
                    id = Guid.NewGuid().ToString(),
                    title = video.VideoTubeTitle,
                    shortDescription = string.IsNullOrEmpty(video.Description) ? "Short description of the movie" : video.Description,
                    longDescription = "Long description of the video",
                    thumbnail = "https://d6hm6c1vbpfva.cloudfront.net/shortform-4be9d883d51ca3161c780d261c3e1ef480e7e56d_2/images/VoiceFeatures.jpg", // video.ThumbnailUrl,
                    genres = new List<string>() { "sports" },
                    tags = new List<string>() { "broadcasts", "live", "technology" },
                    releaseDate = video.VideoTubeLastUpdateTime.HasValue ? video.VideoTubeLastUpdateTime.Value.ToString("yyyy-MM-dd") : new DateTime(2019, 06, 11).ToString("yyyy-MM-dd"),
                    content = new VideoContentModel
                    {
                        dateAdded = DateTime.Now.ToString("yyyy-MM-ddThh:mm:sszz"),
                        captions = new List<VideoContentCaptionModel>(),
                        duration = video.Duration,
                        adBreaks = null, //new List<TimeSpan>(),
                        videos = new List<VideoContentVideoModel> {
                            new VideoContentVideoModel{
                                url = "https:" + video.ProviderVideoId,
                                quality = "HD",
                                videoType = "HLS"
                            }
                        }
                    }
                });
            }

            _rokuFeedModel.movies = list;
        }

        private void FillLiveFeed()
        {
            _rokuFeedModel.liveFeeds = new List<LiveFeedModel>();

            _rokuFeedModel.liveFeeds.Add(new LiveFeedModel()
            {
                id = Guid.NewGuid().ToString(),
                title = _channelTube.Name,
                thumbnail = string.Format("{0}{1}", HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority), _channelTube.PictureUrl), //?? "https://d6hm6c1vbpfva.cloudfront.net/shortform-4be9d883d51ca3161c780d261c3e1ef480e7e56d_2/images/VoiceFeatures.jpg", // video.ThumbnailUrl,
                brandedThumbnail = string.Format("{0}{1}", HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority), _channelTube.PictureUrl), //?? "https://d6hm6c1vbpfva.cloudfront.net/shortform-4be9d883d51ca3161c780d261c3e1ef480e7e56d_2/images/VoiceFeatures.jpg",
                shortDescription = _channelTube.Description ?? "Short description of the movie",
                longDescription = _channelTube.Description ?? "Long Description of the video",
                genres = new List<string>() { "sports" },
                tags = new List<string>() { "dramas" }, // "linear", "news", "celebrity"
                content = new VideoContentModel
                {
                    dateAdded = DateTime.Now.ToString("yyyy-MM-ddThh:mm:sszz"),
                    duration = 24 * 60 * 60,
                    adBreaks = new List<TimeSpan>() { TimeSpan.FromMinutes(1) },
                    language = "en",
                    videos = new List<VideoContentVideoModel>() {
                            new VideoContentVideoModel() {
                                url = _channelStreamLink, //"https:" + video.ProviderVideoId,
                                quality = "FHD",
                                videoType = "HLS"
                            }
                        },
                    captions = new List<VideoContentCaptionModel>()
                        {
                            new VideoContentCaptionModel() {
                                url = string.Format("{0}/download/srt/hls-channel.srt/", HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority)),
                                language = "en",
                                captionType = "CLOSED_CAPTION"
                            }
                        }
                }
            });

        }
    }
}
