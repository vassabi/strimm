using Strimm.Model;
using Strimm.Model.Projections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Data.Interfaces
{
    public interface IChannelScheduleRepository
    {
        List<ChannelSchedule> GetAllChannelSchedules();

        List<ChannelSchedule> GetChannelSchedulesByDateAndChannelTubeId(DateTime startTime, int channelTubeId);

        ChannelSchedule GetChannelScheduleById(int channelScheduleId);

        List<ChannelSchedule> GetChannelSchedulesByChannelTubeId(int channelTubeId);

        bool DeleteChannelScheduleById(int channelScheduleId);

        bool InsertChannelSchedule(ChannelSchedule channelSchedule);

        bool UpdateChannelSchedule(ChannelSchedule channelSchedule);

        ChannelSchedule UpdateChannelScheduleStartDateAndTimeById(int channelScheduleId, DateTime startDateAndTime);

        bool UpdatePublishFlagForChannelScheduleById(int channelScheduleId, bool shouldPublish);

        List<ChannelTubeScheduleCalendarEvent> GetChannelTubeScheduleCalendarEvents(int month, int year, int channelTubeId);

        List<ChannelTubeScheduleCalendarEvent> GetChannelTubeScheduleCalendarEvents(int day, int month, int year, int channelTubeId);

        List<UnpublishedChannelSchedulePo> GetAllUnpublishedFutureSchedules();

        ChannelSchedulePo GetChannelSchedulePoById(int channelScheduleId);

        List<UnpublishedChannelScheduleEmailPo> GetUnpublishedChannelScheduleEmailPoForAllUnpublishedChannelSchedules();

        ChannelSchedule RepeatChannelScheduleByChannelScheduleIdAndTargetDateWithGet(int channelScheduleId, DateTime targetDate);

        void DeleteEmptySchedulesByChannelIdOnOrBeforeDate(int channelTubeId, DateTime scheduleDate);

        void DeleteAllSchedulesByChannelId(int channelTubelId);

        bool ReorderSchedule(int channelScheduleId, int playbackOrderNumber, int targetPlaybackOrderNumber, int videoTubeId);

         bool DeleteVideoFromScheduleOnDrop(int channelScheduleId, int videoTubeId, int playbackOrderNumber);
    }
}
