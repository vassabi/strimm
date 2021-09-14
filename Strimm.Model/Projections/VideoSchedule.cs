using System;
using System.Runtime.Serialization;

namespace Strimm.Model.Projections
{
    [DataContract]
    public class VideoSchedule
    {
        private int channelScheduleId;
	    private int channelTubeId;
	    private int videoTubeId;
        private String videoTubeTitle;
	    private long duration;
	    private DateTime playbackStartTime;
	    private DateTime playbackEndTime;
	    private int playbackOrderNumber;
        private String description;
        private String providerVideoId;
        private int categoryId;
        private String categoryName;
        private int videoProviderId;
        private String videoProviderName;
        private bool isRRated;
        private bool isRemovedByProvider;
        private bool isInPublicLibrary;
        private bool isPrivate;
        private string thumbnail;
        private string videoKey;
        private DateTime? lastUpdateTimestamp;

        [DataMember]
        public int ChannelScheduleId
        {
            get
            {
                return this.channelScheduleId;
            }
            set
            {
                this.channelScheduleId = value;
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
        public String VideoTubeTitle
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
        public String Description
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
        public String ProviderVideoId
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
        public int CategoryId
        {
            get
            {
                return this.categoryId;
            }
            set
            {
                this.categoryId = value;
            }
        }

        [DataMember]
        public String CategoryName
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
        public int VideoProviderId
        {
            get
            {
                return this.videoProviderId;
            }
            set
            {
                this.videoProviderId = value;
            }
        }

        [DataMember]
        public String VideoProviderName
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
        public DateTime? LastUpdateTimeStamp
        {
            get
            {
                return this.lastUpdateTimestamp;
            }
            set
            {
                this.lastUpdateTimestamp = value;
            }
        }
    }
}
