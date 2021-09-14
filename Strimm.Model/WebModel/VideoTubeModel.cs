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
    public class VideoTubeModel : BaseModel
    {
        private int videoTubeId;
        private String title;
        private String description;
        private long duration;
        private int categoryId;
        private int videoProviderId;
        private bool isRRated;
        private bool isRemovedByProvider;
        private bool isRestrictedByProvider;
        private bool isInPublicLibrary;
        private bool isPrivate;
        private String providerVideoId;
        private String thumbnail;
        private string videoProviderName;
        private string categoryName;
        private bool isScheduled;
        private int useCounter;
        private double viewCounter;
       
        private string durationString;
        private bool isInChannel;
        private string message;
        private string dateAdded;
        private DateTime? lastScheduleDateTime;
        private String keywords;

        private bool isProcessing;
        private String videoKey;
        private String videoPreviewKey;
        private int ownerUserId;
        private string ownerUserName;
        private string ownerPublicUrl;
        private string originDomain;
        private DateTime? lastUpdateTimestamp;

        private bool privateVideoModeEnabled;
        private bool matureContentAllowed;

        private string liveStartTime;
        private string liveEndTime;
        private bool isLive;

        public VideoTubeModel()
        {

        }

        public VideoTubeModel(VideoLiveTubePo videoLiveTube)
        {
            if (videoLiveTube != null)
            {
                CategoryId = videoLiveTube.CategoryId;
                CategoryName = videoLiveTube.CategoryName;
                Description = videoLiveTube.Description;
                IsPrivate = videoLiveTube.IsPrivate;
                IsRemovedByProvider = videoLiveTube.IsRemovedByProvider;
                IsRestrictedByProvider = videoLiveTube.IsRestrictedByProvider;
                ProviderVideoId = videoLiveTube.ProviderVideoId;
                Thumbnail = videoLiveTube.Thumbnail;
                Title = videoLiveTube.Title;
                VideoProviderId = videoLiveTube.VideoProviderId;
                VideoProviderName = videoLiveTube.VideoProviderName;
                VideoTubeId = videoLiveTube.VideoLiveTubeId;
                ViewCounter = videoLiveTube.ViewCounter;
                OwnerUserId = videoLiveTube.OwnerUserId;
                OwnerUserName = videoLiveTube.OwnerUserName;
                OwnerPublicUrl = videoLiveTube.OwnerPublicUrl;
                MatureContentAllowed = videoLiveTube.IsRRated;
                LiveStartTime = videoLiveTube.StartTime.ToString();
                LiveEndTime = videoLiveTube.EndTime.ToString();
                DurationString = DateTimeUtils.PrintTimeSpan(videoLiveTube.Duration);
                IsLive = true;
            }
        }

        public VideoTubeModel(VideoTubePo videoTube)
        {
            if (videoTube != null)
            {
                CategoryId = videoTube.CategoryId;
                CategoryName = videoTube.CategoryName;
                Description = videoTube.Description;
                Duration = videoTube.Duration;
                DurationString = DateTimeUtils.PrintTimeSpan(videoTube.Duration);
                IsInPublicLibrary = videoTube.IsInPublicLibrary;
                IsPrivate = videoTube.IsPrivate;
                IsRemovedByProvider = videoTube.IsRemovedByProvider;
                IsRestrictedByProvider = videoTube.IsRestrictedByProvider;
                IsScheduled = videoTube.IsScheduled;
                ProviderVideoId = videoTube.ProviderVideoId;
                Thumbnail = videoTube.Thumbnail;
                Title = videoTube.Title;
                IsRRated = videoTube.IsRRated;
                UseCounter = videoTube.UseCounter;
                VideoProviderId = videoTube.VideoProviderId;
                VideoProviderName = videoTube.VideoProviderName;
                VideoTubeId = videoTube.VideoTubeId;
                ViewCounter = videoTube.ViewCounter;               
                DateAdded = videoTube.DateAdded.ToString();
                Keywords = videoTube.Keywords;
                IsProcessing = videoTube.IsProcessing;
                VideoKey = videoTube.VideoKey;
                OwnerUserId = videoTube.OwnerUserId;
                OwnerUserName = videoTube.OwnerUserName;
                OwnerPublicUrl = videoTube.OwnerPublicUrl;
                VideoPreviewKey = videoTube.VideoPreviewKey;
                LastScheduleDateTime = videoTube.LastScheduleDateTime;
                PrivateVideoModeEnabled = videoTube.PrivateVideoModeEnabled;
                MatureContentAllowed = videoTube.IsRRated;
            }
        }

        public VideoTubeModel(VideoScheduleModel model)
        {
            if (model != null)
            {
                CategoryName = model.CategoryName;
                Description = model.Description;
                Duration = model.Duration;
                DurationString = DateTimeUtils.PrintTimeSpan(model.Duration);
                IsInPublicLibrary = model.IsInPublicLibrary;
                IsPrivate = model.IsPrivate;
                IsRemovedByProvider = model.IsRemovedByProvider;
                IsRestrictedByProvider = model.IsRemovedByProvider;
                ProviderVideoId = model.ProviderVideoId;
                Thumbnail = model.Thumbnail;
                Title = model.VideoTubeTitle;
                VideoProviderName = model.VideoProviderName;
                VideoTubeId = model.VideoTubeId;
                LastUpdateTimestamp = model.VideoTubeLastUpdateTime;
            }
        }

        public VideoTubeModel(string originDomain, VideoTubePo videoTube)
            : this(videoTube)
        {
            OriginDomain = originDomain;
        }

        public VideoTubeModel(string originDomain, VideoLiveTubePo videoLiveTube)
            : this(videoLiveTube)
        {
            OriginDomain = originDomain;
        }

        [DataMember]
        public DateTime? LastScheduleDateTime
        {
            get
            {
                return this.lastScheduleDateTime;
            }
            set
            {
                this.lastScheduleDateTime = value;
            }
        }

        [DataMember]
        public string OriginDomain
        {
            get
            {
                return this.originDomain;
            }
            set
            {
                this.originDomain = value;
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
        public String Title
        {
            get
            {
                return this.title;
            }
            set
            {
                this.title = value;
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
        public bool IsRestrictedByProvider
        {
            get
            {
                return this.isRestrictedByProvider;
            }
            set
            {
                this.isRestrictedByProvider = value;
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
        public String Thumbnail
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

        public String ThumbnailUrl
        {
            get
            {
                return (thumbnail == null || thumbnail.StartsWith("http")) ? thumbnail : String.Format("https://{0}/{1}", originDomain, thumbnail);
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
        public bool IsScheduled
        {
            get
            {
                return this.isScheduled;
            }
            set
            {
                this.isScheduled = value;
            }
        }

        [DataMember]
        public int UseCounter
        {
            get
            {
                return this.useCounter;
            }
            set
            {
                this.useCounter = value;
            }
        }

        [DataMember]
        public double ViewCounter
        {
            get
            {
                return this.viewCounter;
            }
            set
            {
                this.viewCounter = value;
            }
        }

        [DataMember]
        public string DurationString
        {
            get
            {
                return this.durationString;
            }
            set
            {
                this.durationString = value;
            }
        }

        [DataMember]
        public bool IsInChannel
        {
            get
            {
                return this.isInChannel;
            }
            set
            {
                this.isInChannel = value;
            }
        }

        [DataMember]
        public string Message
        {
            get
            {
                return this.message;
            }
            set
            {
                this.message = value;
            }
        }

        [DataMember]
        public string DateAdded
        {
            get
            {
                return this.dateAdded;
            }
            set
            {
                this.dateAdded = value;
            }
        }

        [DataMember]
        public String Keywords
        {
            get
            {
                return this.keywords;
            }
            set
            {
                this.keywords = value;
            }
        }

        [DataMember]
        public bool IsProcessing
        {
            get
            {
                return this.isProcessing;
            }
            set
            {
                this.isProcessing = value;
            }
        }

        [DataMember]
        public String VideoKey
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
        public String VideoPreviewKey
        {
            get
            {
                return this.videoPreviewKey;
            }
            set
            {
                this.videoPreviewKey = value;
            }
        }
        
        [DataMember]
        public int OwnerUserId
        {
            get
            {
                return this.ownerUserId;
            }
            set
            {
                this.ownerUserId = value;
            }
        }

        [DataMember]
        public String OwnerUserName
        {
            get
            {
                return this.ownerUserName;
            }
            set
            {
                this.ownerUserName = value;
            }
        }

        [DataMember]
        public String OwnerPublicUrl
        {
            get
            {
                return this.ownerPublicUrl;
            }
            set
            {
                this.ownerPublicUrl = value;
            }
        }

        [DataMember]
        public DateTime? LastUpdateTimestamp
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

        [DataMember]
        public bool PrivateVideoModeEnabled
        {
            get
            {
                return this.privateVideoModeEnabled;
            }
            set
            {
                this.privateVideoModeEnabled = value;
            }
        }

        [DataMember]
        public bool MatureContentAllowed
        {
            get
            {
                return this.matureContentAllowed;
            }
            set
            {
                this.matureContentAllowed = value;
            }
        }


        [DataMember]
        public string LiveStartTime
        {
            get
            {
                return this.liveStartTime;
            }
            set
            {
                this.liveStartTime = value;
            }
        }

        [DataMember]
        public string LiveEndTime
        {
            get
            {
                return this.liveEndTime;
            }
            set
            {
                this.liveEndTime = value;
            }
        }

        [DataMember]
        public bool IsLive
        {
            get
            {
                return this.isLive;
            }
            set
            {
                this.isLive = value;
            }
        }

    }
}
