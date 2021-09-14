using Strimm.Model;
using Strimm.Model.Projections;
using Strimm.Model.WebModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Data.Interfaces
{
    public interface IVideoScheduleRepository
    {
        List<VideoSchedule> GetVideoSchedulesByUserId(int userId);

        List<VideoSchedule> GetVideoSchedulesByChannelScheduleId(int channelScheduleId);

        List<VideoSchedule> GetVideoSchedulesByChannelTubeIdAndDate(int channelTubeId, DateTime date);

        List<VideoSchedule> GetVideoScheduleByVideoTubeId(int videoTubeId);

        List<VideoSchedule> GetVideoScheduleByChannelScheduleIdAndVideoTubeId(int channelScheduleId, int videoTubeId);

        bool AddVideoTubeToChannelSchedule(int channelScheduleId, VideoTube videoTube, int playbackOrderNumber);

        bool DeleteVideoTubeFromChannelScheduleByChannelScheduleIdAndVideoTubeId(int channelScheduleId, int videoTubeId);

        List<VideoSchedule> AddVideoTubeToChannelScheduleById(int channelScheduleId, int videoTubeId);

        List<VideoSchedule> DeleteVideoTubeFromChannelScheduleById(int channelScheduleId, int videoTubeId, int playbackOrderNumber);

        List<VideoSchedule> DeleteAllVideoTubesFromChannelScheduleById(int channelScheduleId);

        List<VideoScheduleModel> GetCurrentlyPlayingVideoTubeByKeyword(List<string> keywords, DateTime formattedDateTime);
    }
}
