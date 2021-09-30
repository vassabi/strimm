using log4net;
using Strimm.Data.Repositories;
using Strimm.Model;
using Strimm.Model.WebModel;
using Strimm.VideoImport.Model;
using StrimmBL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.VideoImport
{
    public class Program
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(Program));

        public List<VideoImportData> ReadVideoImportData(FileInfo dataFile)
        {
            var videos = new List<VideoImportData>();

            var content = File.ReadAllLines(dataFile.FullName);
            if (content != null && content.Length > 0)
            {
                content.ToList().ForEach(x =>
                {
                    var parts = x.Split(',');
                    if (parts != null && parts.Length >= 2)
                    {
                        var url = parts[0].Trim();
                        var category = parts[1].ToLower().Trim();

                        if (!videos.Any(y => y.Url == url))
                        {
                            videos.Add(new VideoImportData() { Url = url, CategoryName = category });
                        }
                    }
                });
            }

            return videos;
        }

        public List<VideoTubeModel> GetVideosFromVideoProvider(List<VideoImportData> videos, List<Category> categories)
        {
            var externalVideos = new List<VideoTubeModel>();

            if (videos != null && videos.Count > 0) {
                videos.ForEach(x =>
                {
                    var externalVideo = YouTubeServiceManage.GetVideoByUrl(x.Url);
                    if (externalVideo != null && externalVideo.Title != null)
                    {
                        Logger.Debug(String.Format("Retrieved video = '{0}'", x.Url));

                        var category = categories.FirstOrDefault(y => y.Name == x.CategoryName);
                        if (category == null)
                        {
                            category = categories.FirstOrDefault(y => y.Name == "other");
                        }
                        externalVideo.CategoryId = category.CategoryId;
                        externalVideo.CategoryName = category.Name;

                        externalVideos.Add(externalVideo);
                    }
                    else
                    {
                        Logger.Warn(String.Format("Failed to retrieve video with url = '{0}'", x.Url));
                    }
                });
            }

            return externalVideos;
        }

        public int AddVideoToUsersVideoRoom(List<VideoTubeModel> externalVideos, int videoRoomTubeId)
        {
            int videosAddedCount = 0;

            if (externalVideos != null && externalVideos.Count > 0)
            {
                using (var videoTubeRepository = new VideoTubeRepository())
                {
                    externalVideos.ForEach(x =>
                    {
                        var existing = videoTubeRepository.GetVideoTubeByProviderVideoId(x.ProviderVideoId);

                        if (existing == null)
                        {
                            var videoTube = videoTubeRepository.InsertVideoTubeWithGet(x.Title, x.Description, x.ProviderVideoId, x.Duration, x.CategoryId, x.VideoProviderId, x.IsRRated, false, false, x.Thumbnail);

                            if (videoTube != null && videoTube.VideoTubeId > 0)
                            {
                                videoTubeRepository.AddVideoTubeToVideoRoomTube(videoRoomTubeId, videoTube.VideoTubeId);
                                Logger.Debug(String.Format("Video with URL='{0}' added to Public Library. VideoTubeId = {1}", x.Url, videoTube.VideoTubeId));
                                videosAddedCount++;
                            }
                            else
                            {
                                Logger.Warn(String.Format("Unable to add video '{0}' to video room with Id={1}", x.ProviderVideoId, videoRoomTubeId));
                            }
                        }
                    });
                }
            }

            return videosAddedCount;
        }

        public int AddVideosToPublicLibrary(List<VideoTubeModel> externalVideos)
        {
            int videosAddedCount = 0;

            if (externalVideos != null && externalVideos.Count > 0)
            {
                using (var videoTubeRepository = new VideoTubeRepository())
                {
                    externalVideos.ForEach(x =>
                    {
                        var existing = videoTubeRepository.GetVideoTubeByProviderVideoId(x.ProviderVideoId);

                        if (existing == null)
                        {
                            var videoTube = videoTubeRepository.InsertVideoTubeWithGet(x.Title, x.Description, x.ProviderVideoId, x.Duration, x.CategoryId, x.VideoProviderId, x.IsRRated, true, false, x.Thumbnail);
                            Logger.Debug(String.Format("Video with URL='{0}' added to Public Library. VideoTubeId = {1}", x.Url, videoTube.VideoTubeId));
                            videosAddedCount++;
                        }
                        else
                        {
                            if (!existing.IsInPublicLibrary)
                            {
                                videoTubeRepository.UpdateVideoTube(new VideoTube()
                                {
                                    VideoTubeId = existing.VideoTubeId,
                                    VideoProviderId = existing.VideoProviderId,
                                    Title = x.Title,
                                    Thumbnail = x.Thumbnail,
                                    ProviderVideoId = existing.ProviderVideoId,
                                    IsRRated = existing.IsRRated,
                                    IsRestrictedByProvider = existing.IsRestrictedByProvider,
                                    IsInPublicLibrary = true,
                                    Duration = x.Duration,
                                    Description = x.Description,
                                    CategoryId = x.CategoryId,
                                    CreatedDate = existing.CreatedDate
                                });

                                Logger.Debug(String.Format("Updated existing video record in db for video with URL='{0}'", existing.ProviderVideoId));
                            }
                            else
                            {
                                Logger.Warn(String.Format("Video with ProviderVideoId='{0}' was not added to Public Library. Video record with Id = {1} already exists!", existing.ProviderVideoId, existing.VideoTubeId));
                            }
                        }
                    });
                }
            }

            return videosAddedCount;
        }

        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.Configure();

            var datafilepath = ConfigurationManager.AppSettings["datafilepath"];
            var targetVideoRoomId = ConfigurationManager.AppSettings["destVideoRoomId"];

            int videoRoomId = 0;
            Int32.TryParse(targetVideoRoomId.ToString(), out videoRoomId);

            var datafile = new FileInfo(datafilepath);

            if (datafile.Exists)
            {
                var program = new Program();
                var videos = program.ReadVideoImportData(datafile);

                if (videos != null && videos.Count > 0)
                {
                    var categories = ReferenceDataManage.GetAllCategories();
                    var externalVideos = program.GetVideosFromVideoProvider(videos, categories);
                    var videosAddedCount = 0;

                    if (videoRoomId > 0)
                    {
                        videosAddedCount = program.AddVideoToUsersVideoRoom(externalVideos, videoRoomId);
                    }
                    else if (externalVideos.Count > 0)
                    {
                        videosAddedCount = program.AddVideosToPublicLibrary(externalVideos);
                    }
                    else
                    {
                        Logger.Warn("Failed to resolve specified videos against YouTube");
                    }

                    Logger.Info(String.Format("Number of videos imported: {0}", videos.Count));
                    Logger.Info(String.Format("Number of external videos retrieved from provider: {0}", externalVideos.Count));
                    Logger.Info(String.Format("Number of external videos not found by provider: {0}", (videos.Count - externalVideos.Count)));
                    Logger.Info(String.Format("Number of external videos found in database: {0}", (externalVideos.Count - videosAddedCount)));
                    Logger.Info(String.Format("Number of external videos added to database/public library: {0}", (videosAddedCount)));
                }
                else
                {
                    Logger.Warn("Failed to import any video records. Data file is invalid or does not contain any valid URLs");
                }
            }

            Logger.Debug("Done!");
        }
    }
}
