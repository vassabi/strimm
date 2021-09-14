using Strimm.Data.Interfaces;
using Strimm.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using log4net;
using System.Data;

namespace Strimm.Data.Repositories
{
    public class VideoRoomTubeRepository : RepositoryBase, IVideoRoomTubeRepository
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(VideoRoomTube));

        public VideoRoomTubeRepository()
            : base()
        {

        }

        public VideoRoomTube GetVideoRoomTubeById(int videoRoomTubeId)
        {
            Contract.Requires(videoRoomTubeId > 0, "VideoRoomTubeId should be greater then 0");

            VideoRoomTube videoRoomTube = null;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    var results = this.StrimmDbConnection.Query<VideoRoomTube>("strimm.GetVideoRoomTubeById", new { VideoRoomTubeId = videoRoomTubeId }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                    videoRoomTube = results.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve video room tube by id = {0}", videoRoomTubeId), ex);
            }

            return videoRoomTube;
        }

        public VideoRoomTube GetVideoRoomTubeByUserId(int userId)
        {
            Contract.Requires(userId > 0, "UserId should be greater then 0");

            VideoRoomTube videoRoomTube = null;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    var results = this.StrimmDbConnection.Query<VideoRoomTube>("strimm.GetVideoRoomTubeByUserId", new { UserId = userId }, null, false, 30, commandType: CommandType.StoredProcedure).ToList();
                    videoRoomTube = results.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to retrieve video room tube by UserId = {0}", userId), ex);
            }

            return videoRoomTube;
        }

        public bool InsertVideoRoomTube(int userId, bool isPrivate = false)
        {
            Contract.Requires(userId > 0, "Existing user specified. Insert aborted");

            bool isSuccess = false;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    int rowcount = this.StrimmDbConnection.Execute("strimm.InsertVideoRoomTube", new { UserId = userId, IsPrivate = isPrivate }, null, 30, commandType: CommandType.StoredProcedure);
                    isSuccess = rowcount == 1;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to insert new video room tube for user Id '{0}'", userId), ex);
            }

            return isSuccess;
        }

        public VideoRoomTube InsertVideoRoomTubeWithGet(int userId, bool isPrivate = false)
        {
            Contract.Requires(userId > 0, "Existing user specified. Insert aborted");

            VideoRoomTube videoRoomTube = null;

            try
            {
                if (this.StrimmDbConnection != null && this.StrimmDbConnection.State == System.Data.ConnectionState.Open)
                {
                    var results = this.StrimmDbConnection.Query<VideoRoomTube>("strimm.InsertVideoRoomTubeWithGet", new { UserId = userId, IsPrivate = isPrivate }, null, false, 30, commandType: CommandType.StoredProcedure);
                    videoRoomTube = results.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Failed to insert new video room tube for user Id '{0}'", userId), ex);
            }

            return videoRoomTube;
        }

    }
}
