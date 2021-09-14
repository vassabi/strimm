using Strimm.Data.Interfaces;
using Strimm.Model.Projections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using log4net;
using System.Diagnostics.Contracts;
using System.Data;
using Strimm.Model;
using Strimm.Model.Projections;
using Strimm.Model.WebModel;

namespace Strimm.Data.Repositories
{
    public class VideoScheduleRepository : RepositoryBase, IVideoScheduleRepository
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(VideoScheduleRepository));

        public VideoScheduleRepository()
            : base()
        {

        }

        public List<VideoSchedule> GetVideoSchedulesByUserId(int userId)
        {
            Contract.Requires(userId > 0, "UserId should be greater then 0");

            var videoSchedules = new List<VideoSchedule>();

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    videoSchedules = this.StrimmDbConnection.Query<VideoSchedule>("strimm.GetVideoSchedulesByUserId", new { UserId = userId }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve video schedules for user with Id = {0}", userId), ex);
            }

            return videoSchedules;
        }

        public List<VideoSchedule> GetVideoSchedulesByChannelScheduleId(int channelScheduleId)
        {
            Contract.Requires(channelScheduleId > 0, "ChannelScheduleId should be greater then 0");

            var videoSchedules = new List<VideoSchedule>();

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    videoSchedules = this.StrimmDbConnection.Query<VideoSchedule>("strimm.GetVideoSchedulesByChannelScheduleId", new { ChannelScheduleId = channelScheduleId }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve video schedules for channel schedule with Id = {0}", channelScheduleId), ex);
            }

            return videoSchedules;
        }

        public List<VideoSchedule> GetVideoSchedulesByChannelTubeIdAndDate(int channelTubeId, DateTime date)
        {
            Contract.Requires(channelTubeId > 0, "ChannelTubeId should be greater then 0");
            Contract.Requires(date >= DateTime.MinValue && date <= DateTime.MaxValue, "Invalid schedule date specified");

            var videoSchedules = new List<VideoSchedule>();

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    videoSchedules = this.StrimmDbConnection.Query<VideoSchedule>("strimm.GetVideoSchedulesByChannelTubeIdAndDate", new { ChannelTubeId = channelTubeId, ScheduleDate = date }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve video schedules for channel tube with Id = {0} and schedule dated of '{1}'", channelTubeId, date.ToString()), ex);
            }

            return videoSchedules;
        }

        public List<VideoSchedule> GetVideoScheduleByVideoTubeId(int videoTubeId)
        {
            Contract.Requires(videoTubeId > 0, "VideoTubeId should be greater then 0");

            var videoSchedules = new List<VideoSchedule>();

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    videoSchedules = this.StrimmDbConnection.Query<VideoSchedule>("strimm.GetVideoScheduleByVideoTubeId", new { VideoTubeId = videoTubeId }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve video schedules for video tube with Id = {0}", videoTubeId), ex);
            }

            return videoSchedules;
        }

        public List<VideoSchedule> GetVideoScheduleByChannelScheduleIdAndVideoTubeId(int channelScheduleId, int videoTubeId)
        {
            Contract.Requires(channelScheduleId > 0, "ChannelScheduleId should be greater then 0");
            Contract.Requires(videoTubeId > 0, "VideoTubeId should be greater then 0");

            var videoSchedules = new List<VideoSchedule>(); 

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    videoSchedules = this.StrimmDbConnection.Query<VideoSchedule>("strimm.GetVideoScheduleByChannelScheduleIdAndVideoTubeId", new { ChannelScheduleId = channelScheduleId, VideoTubeId = videoTubeId }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve video schedule for channel schedule Id = {0} and videoTubeId = {1}", channelScheduleId, videoTubeId), ex);
            }

            return videoSchedules;
        }

        public List<VideoSchedule> AddVideoTubeToChannelScheduleById(int channelScheduleId, int videoTubeId)
        {
            Contract.Requires(channelScheduleId > 0, "ChannelScheduleId should be greater then 0");
            Contract.Requires(videoTubeId > 0, "Invalid video tube Id specified");

            List<VideoSchedule> videoSchedules = new List<Model.Projections.VideoSchedule>();

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    videoSchedules = this.StrimmDbConnection.Query<VideoSchedule>("strimm.AddVideoTubeToChannelScheduleWithGet", 
                                        new { ChannelScheduleId = channelScheduleId, VideoTubeId = videoTubeId }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to add video with Id={0} to an existing channel schedule with Id={1}", videoTubeId, channelScheduleId), ex);
            }

            return videoSchedules;
        }
        public List<VideoSchedule> AddVideoTubeToChannelScheduleById(int channelScheduleId, int videoTubeId, int orderNumber)
        {
            Contract.Requires(channelScheduleId > 0, "ChannelScheduleId should be greater then 0");
            Contract.Requires(videoTubeId > 0, "Invalid video tube Id specified");

            List<VideoSchedule> videoSchedules = new List<Model.Projections.VideoSchedule>();

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    videoSchedules = this.StrimmDbConnection.Query<VideoSchedule>("strimm.dd_AddVideoTubeToChannelSchedule ",
                                        new { ChannelScheduleId = channelScheduleId, VideoTubeId = videoTubeId, PlaybackOrderNumber = orderNumber }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to add video with Id={0} to an existing channel schedule with Id={1}", videoTubeId, channelScheduleId), ex);
            }

            return videoSchedules;
        }

        public List<VideoSchedule> DeleteVideoTubeFromChannelScheduleById(int channelScheduleId, int videoTubeId, int playbackOrderNumber)
        {
            Contract.Requires(channelScheduleId > 0, "ChannelScheduleId should be greater then 0");
            Contract.Requires(videoTubeId > 0, "Invalid video tube Id specified");
            Contract.Requires(playbackOrderNumber > 0, "Invalid playbackOrderNumber specified");

            List<VideoSchedule> videoSchedules = new List<Model.Projections.VideoSchedule>();

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    videoSchedules = this.StrimmDbConnection.Query<VideoSchedule>("strimm.DeleteVideoTubeFromChannelScheduleByIdWithGet",
                                        new { ChannelScheduleId = channelScheduleId, VideoTubeId = videoTubeId, PlaybackOrderNumber = playbackOrderNumber }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to delete video with Id={0} to an existing channel schedule with Id={1}", videoTubeId, channelScheduleId), ex);
            }

            return videoSchedules;
        }
         public List<VideoSchedule> DeleteVideoTubeFromChannelScheduleByFromOrderNumberIdWithGet(int channelScheduleId,  int playbackOrderNumberFrom)
        {
            Contract.Requires(channelScheduleId > 0, "ChannelScheduleId should be greater then 0");
           
            Contract.Requires(playbackOrderNumberFrom > 0, "Invalid playbackOrderNumber specified");

            List<VideoSchedule> videoSchedules = new List<Model.Projections.VideoSchedule>();

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    videoSchedules = this.StrimmDbConnection.Query<VideoSchedule>("strimm.DeleteVideoTubeFromChannelScheduleByFromOrderNumberIdWithGet",
                                        new { ChannelScheduleId = channelScheduleId,  PlaybackOrderNumber = playbackOrderNumberFrom }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to delete videos to an existing channel schedule with Id={0}",  channelScheduleId), ex);
            }

            return videoSchedules;
        }

        public bool AddVideoTubeToChannelSchedule(int channelScheduleId, VideoTube videoTube, int playbackOrderNumber)
        {
            Contract.Requires(channelScheduleId > 0, "ChannelScheduleId should be greater then 0");
            Contract.Requires(videoTube != null && videoTube.VideoTubeId > 0, "Invalid or missing video tube specified");
            Contract.Requires(playbackOrderNumber >= 0, "Invalid playback order number specifie");

            bool isSuccess = false;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    int rowcount = this.StrimmDbConnection.Execute("strimm.AddVideoTubeToChannelSchedule", new { ChannelScheduleId = channelScheduleId, VideoTubeId = videoTube.VideoTubeId, PlaybackOrderNumber = playbackOrderNumber }, null, 30, commandType: CommandType.StoredProcedure);
                    isSuccess = rowcount == 1;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to add video with Id={0} to an existing channel schedule with Id={1}, at position = {2}", videoTube.VideoTubeId, channelScheduleId, playbackOrderNumber), ex);
            }

            return isSuccess;
        }

        public bool DeleteVideoTubeFromChannelScheduleByChannelScheduleIdAndVideoTubeId(int channelScheduleId, int videoTubeId)
        {
            Contract.Requires(channelScheduleId > 0, "ChannelScheduleId should be greater then 0");
            Contract.Requires(videoTubeId > 0, "VideoTubeId should be greater then 0");

            bool isSuccess = false;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    int rowcount = this.StrimmDbConnection.Execute("strimm.DeleteVidoTubeFromChannelScheduleByChannelScheduleIdAndVideoTubeId", new { ChannelScheduleId = channelScheduleId, VideoTubeId = videoTubeId }, null, 30, commandType: CommandType.StoredProcedure);
                    isSuccess = rowcount == 1;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to delete video with Id={0} from existing channel schedule with Id={1}", videoTubeId, channelScheduleId), ex);
            }

            return isSuccess;
        }

        public List<VideoSchedule> DeleteAllVideoTubesFromChannelScheduleById(int channelScheduleId)
        {
            Contract.Requires(channelScheduleId > 0, "Invalid channel schedule Id specified");

            List<VideoSchedule> videoSchedules = new List<Model.Projections.VideoSchedule>();

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    videoSchedules = this.StrimmDbConnection.Query<VideoSchedule>("strimm.DeleteAllVideosFromChannelScheduleById", new { ChannelScheduleId = channelScheduleId }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to delete all video from existing channel schedule with Id={0}", channelScheduleId), ex);
            }

            return videoSchedules;
        }

        public List<VideoScheduleModel> GetCurrentlyPlayingVideoTubeByKeyword(List<string> keywords, DateTime formattedDateTime)
        {
            Logger.Info("Retrieving video tubes schedule by keywords");

            Contract.Requires(keywords != null && keywords.Count > 0, "No keywords specified to perform the search");

            if (keywords == null || keywords.Count == 0)
            {
                throw new ArgumentException("No keywords specified to perform the search");
            }

            var videoschedules = new List<VideoScheduleModel>();
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
                    videoschedules = this.StrimmDbConnection.Query<VideoScheduleModel>("strimm.GetCurrentlyPlayingVideoTubeByKeyword", new { Keywords = keywordsString, ClientTime = formattedDateTime }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve videotubes by keywords='{0}'", keywordsString), ex);
            }

            return videoschedules;
        }
    }
}
