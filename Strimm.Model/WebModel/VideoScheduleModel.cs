using Strimm.Model.Projections;
using Strimm.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Model.WebModel
{
    [DataContract]
    public class VideoScheduleModel : BaseModel
    {
        private string categoryName;
        private int videoTubeId;
        private string videoTubeTitle;
        private string videoProviderName;
        private string providerVideoId;
        private int playbackOrderNumber;
        private DateTime playbackStartTime;
        private DateTime playbackEndTime;
        private DateTime playingVideoTubeStartTime;
        private DateTime playingVideoTubeEndTime;
        private bool isInPublicLibrary;
        private bool isPrivate;
        private bool isRemovedByProvider;
        private bool isRRated;
        private long duration;
        private String description;
        private string thumbnail;
        private string playtimeMessage;
        private bool allowDeleted;
        private double playerPlaybackStartTimeInSec;
        private bool isPlayingNow;
        private bool isPlayingNext;
        private int channelTubeId;
        private string channelUrl;
        private string userPublicUrl;
        private string channelName;
        private string originDomain;
        private string videoKey;
        private DateTime? videoTubeLastUpdateTime;
        private string playingVideoTubeTitle;
        private string playingVideoTubeThumbnail;
        

        public VideoScheduleModel()
        {

        }

        public VideoScheduleModel(string domainOrigin, VideoSchedule schedule)
        {
            if (schedule != null)
            {
                CategoryName = schedule.CategoryName;
                Description = schedule.Description;
                Duration = schedule.Duration;
                IsInPublicLibrary = schedule.IsInPublicLibrary;
                IsPrivate = schedule.IsPrivate;
                IsRemovedByProvider = schedule.IsRemovedByProvider;
                IsRRated = schedule.IsRRated;
                PlaybackEndTime = schedule.PlaybackEndTime;
                PlaybackOrderNumber = schedule.PlaybackOrderNumber;
                PlaybackStartTime = schedule.PlaybackStartTime;
                ProviderVideoId = schedule.ProviderVideoId;
                Thumbnail = schedule.Thumbnail;
                VideoProviderName = schedule.VideoProviderName;
                VideoTubeTitle = schedule.VideoTubeTitle;
                VideoTubeId = schedule.VideoTubeId;
                ChannelTubeId = schedule.ChannelTubeId;           
                PlaytimeMessage = DateTimeUtils.PrintPlaytimeDuration(schedule.PlaybackStartTime, schedule.PlaybackEndTime);
                VideoKey = schedule.VideoKey;
                VideoTubeLastUpdateTime = schedule.LastUpdateTimeStamp;
            }

            this.originDomain = domainOrigin;
        }
        [DataMember]
        public string ChannelName
        {
            get {
                return this.channelName;
            }
            set
            {
                this.channelName = value;
            }
        }

        [DataMember]
        public string ChannelUrl
        {
            get
            {
                return this.channelUrl;
            }
            set
            {
                this.channelUrl = value;
            }
        }
        [DataMember]
        public string UserPublicUrl
        {
            get
            {
                return this.userPublicUrl;
            }
            set
            {
                this.userPublicUrl = value;
            }
        }
        [DataMember]
        public int ChannelTubeId
        {
            get
            {
                return this.channelTubeId;
            }
            set
            {
                this.channelTubeId = value;
             }
        }

        [DataMember]
        public string VideoKey
        {
            get
            {
                return this.videoKey;
            }
            set
            {
                this.videoKey = value;
            }
        }

        [DataMember]
        public string CategoryName
        {
            get
            {
                return this.categoryName;
            }
            set
            {
                this.categoryName = value;
            }
        }

        [DataMember]
        public int VideoTubeId
        {
            get
            {
                return this.videoTubeId;
            }
            set
            {
                this.videoTubeId = value;
            }
        }

        [DataMember]
        public string VideoTubeTitle
        {
            get
            {
                return this.videoTubeTitle;
            }
            set
            {
                this.videoTubeTitle = value;
            }
        }

        [DataMember]
        public string VideoProviderName
        {
            get
            {
                return this.videoProviderName;
            }
            set
            {
                this.videoProviderName = value;
            }
        }

        [DataMember]
        public string ProviderVideoId
        {
            get
            {
                return this.providerVideoId;
            }
            set
            {
                this.providerVideoId = value;
            }
        }

        [DataMember]
        public int PlaybackOrderNumber
        {
            get
            {
                return this.playbackOrderNumber;
            }
            set
            {
                this.playbackOrderNumber = value;
            }
        }

        [DataMember]
        public DateTime PlaybackStartTime
        {
            get
            {
                return this.playbackStartTime;
            }
            set
            {
                this.playbackStartTime = value;
            }
        }

        [DataMember]
        public string PlaybackStartTimeString
        {
            get
            {
                return (playbackStartTime != null) ? playbackStartTime.ToString("MM/dd/yyyy HH:mm") : String.Empty;
            }
        }

        [DataMember]
        public string PlaybackEndTimeString
        {
            get
            {
                return (playbackEndTime != null) ? playbackEndTime.ToString("MM/dd/yyyy HH:mm") : String.Empty;
            }
        }
        [DataMember]
        public DateTime PlayingVideoTubeStartTime
        {
            get { return this.playingVideoTubeStartTime; }
            set { this.playingVideoTubeStartTime = value; }
        }
        [DataMember]
        public DateTime PlayingVideoTubeEndTime
        {
            get { return this.playingVideoTubeEndTime; }
            set { this.playingVideoTubeEndTime = value; }
        }
        [DataMember]
        public DateTime PlaybackEndTime
        {
            get
            {
                return this.playbackEndTime;
            }
            set
            {
                this.playbackEndTime = value;
            }
        }

        [DataMember]
        public bool IsInPublicLibrary
        {
            get
            {
                return this.isInPublicLibrary;
            }
            set
            {
                this.isInPublicLibrary = value;
            }
        }

        [DataMember]
        public bool IsPrivate
        {
            get
            {
                return this.isPrivate;
            }
            set
            {
                this.isPrivate = value;
            }
        }

        [DataMember]
        public bool IsRemovedByProvider
        {
            get
            {
                return this.isRemovedByProvider;
            }
            set
            {
                this.isRemovedByProvider = value;
            }
        }

        [DataMember]
        public bool IsRRated
        {
            get
            {
                return this.isRRated;
            }
            set
            {
                this.isRRated = value;
            }
        }

        [DataMember]
        public long Duration
        {
            get
            {
                return this.duration;
            }
            set
            {
                this.duration = value;
            }
        }

        [DataMember]
        public string Description
        {
            get
            {
                return this.description;
            }
            set
            {
                this.description = value;
            }
        }

        [DataMember]
        public string Thumbnail
        {
            get
            {
                return this.thumbnail;
            }
            set
            {
                this.thumbnail = value;
            }
        }

        [DataMember]
        public string ThumbnailUrl
        {
            get
            {
                return this.thumbnail == null || this.thumbnail.StartsWith("http")
                        ? thumbnail
                        : String.Format("https://{0}/{1}", this.originDomain, this.thumbnail);
            }
        }
        [DataMember]
        public string PlaytimeMessage
        {
            get
            {
                return this.playtimeMessage;
            }
            set
            {
                this.playtimeMessage = value;
            }
        }

        [DataMember]
        public bool AllowDeleted
        {
            get
            {
                return this.allowDeleted;
            }
            set
            {
                this.allowDeleted = value;
            }
        }

        [DataMember]
        public double PlayerPlaybackStartTimeInSec
        {
            get
            {
                return this.playerPlaybackStartTimeInSec;
            }
            set
            {
                this.playerPlaybackStartTimeInSec = value;
            }
        }

        [DataMember]
        public bool IsPlayingNow
        {
            get
            {
                return this.isPlayingNow;
            }
            set
            {
                this.isPlayingNow = value;
            }
        }

        [DataMember]
        public bool IsPlayingNext
        {
            get
            {
                return this.isPlayingNext;
            }
            set
            {
                this.isPlayingNext = value;
            }
        }

        [DataMember]
        public DateTime? VideoTubeLastUpdateTime
        {
            get
            {
                return this.videoTubeLastUpdateTime;
            }
            set
            {
                this.videoTubeLastUpdateTime = value;
            }
        }

        [DataMember]
        public string PlayingVideoTubeTitle
        {
            get { return this.playingVideoTubeTitle; }
            set {this.playingVideoTubeTitle = value; }
        }

        [DataMember]
        public string PlayingVideoTubeThumbnail
        {
            get { return this.playingVideoTubeThumbnail; }
            set { this.playingVideoTubeThumbnail = value; }
        }
    }
}
