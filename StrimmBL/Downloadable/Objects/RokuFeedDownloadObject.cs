using Newtonsoft.Json;
using Strimm.Model;
using Strimm.Model.WebModel;
using StrimmBL.Roku;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StrimmBL.Downloadable
{
    public class RokuFeedDownloadObject : BaseDownloadableObject
    {
        
        public RokuFeedDownloadObject()
        {
            ContentType = "application/json";
            FileFormat = "rokufeed";
            FileExtension = "json";
        }

        public override byte[] GenerateFile(string channelName)
        {
            ChannelTube channelTube = ChannelManage.GetChannelTubeByName(channelName);

            //var sched = ScheduleManage.GetChannelTubeSchedulesByDate(channelTube.ChannelTubeId, DateTime.Now)
            //    .Where(x => x.StartDateAndTime > DateTime.Now || (x.StartDateAndTime < DateTime.Now && x.EndDateAndTime > DateTime.Now))
            //    .OrderBy(x => x.StartDateAndTime)
            //    .ToList();

            //var hls_videos = (sched != null && sched.Count > 0) ? sched[0].VideoSchedules
            //    .Where(x => x.VideoProviderName == "custom" && x.ProviderVideoId.ToLower().Contains(".m3u8")
            //        && (x.PlaybackStartTime > DateTime.Now || (x.PlaybackStartTime < DateTime.Now && x.PlaybackEndTime > DateTime.Now)))
            //    .ToList() : new List<VideoScheduleModel>();

            string channelHlsLink = DownloadableFactory.Create("hls").GenerateLink(channelTube.Name);

            FeedBuilder builder = new FeedBuilder();
            //builder.Build(hls_videos);
            builder.Build(channelTube, channelHlsLink);

            var json = JsonConvert.SerializeObject(builder.GetRokuFeed());
            return Encoding.ASCII.GetBytes(json);
        }
    }
}
