using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Strimm.Model;
using Strimm.Model.Projections;
using log4net;
using System.Diagnostics.Contracts;
using Strimm.Data.Repositories;
using StrimmBL.Exeptions;

namespace StrimmBL
{
    public static class VideoRoomTubeManage
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(VideoRoomTubeManage));

        public static bool HasVideoRoomTube(int userId)
        {
            bool hasVideoRoom = false;

            using (var videoRoomTubeRepository = new VideoRoomTubeRepository())
            {
                var videoRoomTube = videoRoomTubeRepository.GetVideoRoomTubeByUserId(userId);
                hasVideoRoom = videoRoomTube != null;
            }

            return hasVideoRoom;
        }

        public static bool CreateVideoRoom(int userId)
        {
            Contract.Requires(userId > 0, "Invalid user Id specified. VideoRoomTube cannot be created");

            Logger.Info(String.Format("Creating new videoRoomTube for user with Id={0}", userId));

            bool isSuccess = false;

            using (var videoRoomTubeRepository = new VideoRoomTubeRepository())
            {
                isSuccess = videoRoomTubeRepository.InsertVideoRoomTube(userId);
            }

            return isSuccess;
        }

        public static VideoRoomTube GetVideoRoomTubeByUserId(int userId)
        {
            Contract.Requires(userId > 0, "Invalid user Id specified. VideoRoomTube cannot be retrieved");

            Logger.Info(String.Format("Retrieving videoRoomTube for user with Id={0}", userId));

            VideoRoomTube videoRoom = null;

            using (var videoRoomTubeRepository = new VideoRoomTubeRepository())
            {
                videoRoom = videoRoomTubeRepository.GetVideoRoomTubeByUserId(userId);
            }

            return videoRoom;
        }

        public static int GetVideoRoomTubeIdByUserId(int userId)
        {
            Contract.Requires(userId > 0, "Invalid user Id specified. VideoRoomTube cannot be retrieved");

            Logger.Info(String.Format("Retrieving videoRoomTube for user with Id={0}", userId));

            var videoRoomTube = GetVideoRoomTubeByUserId(userId);

            return videoRoomTube != null ? videoRoomTube.VideoRoomTubeId : 0;
        }



        public static bool DeleteVideoTubeFromVideoRoomById(int userId, int videoTubeId)
        {
            Logger.Info(String.Format("Deleting video with id={0} from video room for user with id={1}", videoTubeId, userId));

            bool isSuccess = false;

            using (var vrRepository = new VideoRoomTubeRepository())
            using (var videoRepository = new VideoTubeRepository()) 
            {
                var existingVideo = videoRepository.GetVideoTubePoById(videoTubeId);
                if (existingVideo != null)
                {
                    isSuccess = videoRepository.DeleteVideoTubeFromVideoRoomByVideoTubeIdAndUserId(videoTubeId, userId);
                }
                else
                {
                    throw new VideoRoomTubeManagerException("Error occured while deleting a video. Video was not found");
                }
            }

            return isSuccess;
        }
    }
}
