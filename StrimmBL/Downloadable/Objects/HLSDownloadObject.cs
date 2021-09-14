using Strimm.Model;
using Strimm.Model.WebModel;
using StrimmBL.HLS;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StrimmBL.Downloadable
{
    public class HLSDownloadObject : BaseDownloadableObject
    {
        public HLSDownloadObject()
        {
            ContentType = "application/x-mpegURL";
            FileFormat = "hls";
            FileExtension = "m3u8";
        }

        public override byte[] GenerateFile(string channelName)
        {
            ChannelTube channelTube = ChannelManage.GetChannelTubeByName(channelName);

            //var videoTube = ScheduleManage.GetVideoTubePoByChannelTubeIdAndPageIndex(channelTube.ChannelTubeId, 1, "");
            var sched = ScheduleManage.GetChannelTubeSchedulesByDate(channelTube.ChannelTubeId, DateTime.Now)
                .Where(x => x.StartDateAndTime > DateTime.Now || (x.StartDateAndTime < DateTime.Now && x.EndDateAndTime > DateTime.Now))
                .OrderBy(x => x.StartDateAndTime)
                .ToList();

            var hls_videos = (sched != null && sched.Count > 0) ? sched[0].VideoSchedules
                .Where(x => x.VideoProviderName == "custom" && x.ProviderVideoId.ToLower().Contains(".m3u8")
                    && (x.PlaybackStartTime > DateTime.Now || (x.PlaybackStartTime < DateTime.Now && x.PlaybackEndTime > DateTime.Now)))
                .ToList() : new List<VideoScheduleModel>();

            var hlsService = new HLSVideoService();
            var fileBytes = hlsService.GenerateM3U8File(hls_videos);

            return fileBytes;
        }
    }
}
