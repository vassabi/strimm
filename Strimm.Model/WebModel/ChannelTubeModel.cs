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
    public class ChannelTubeModel: BaseModel
    {
        private String categoryName;
        private int channelTubeId;
        private String description;
        private bool isFeatured;
        private bool isLocked;
        private bool isPrivate; 
        private String name;
        private String pictureUrl;
        private float rating;
        private String channelUrl;
        private int videoTubeCount;
        private int subscriberCount;
        private int channelViewCount;
        private bool isAutoPilotOn;
        private int playingVideoTubeId;
        private string playingVideoTubeTitle;
        private DateTime? playingVideoTubeStartTime;
        private DateTime? playingVideoTubeEndTime;
        private string playingVideoTubeThumbnail;
        private DateTime? currentScheduleStartTime;
        private DateTime? currentScheduleEndTime;
        private bool isSubscribed;
        private string playTimeMessage;
        private string channelOwnerUserName;
        private string createdDate;
        private string channelOwnerUrl;
        private int languageId;
        private int? userChannelRating;
        private bool isLike;
        private string channelPassword;
        private bool embedEnabled;
        private bool isWhiteLabel;
        private string customLabel;
        private bool isMuted;
        private string customLogo;
        private bool isLogoModeActive;


        public ChannelTubeModel()
        {

        }

        public ChannelTubeModel(ChannelTubePo channel)
        {
            if (channel != null)
            {
                CategoryName = channel.CategoryName;
                ChannelTubeId = channel.ChannelTubeId;
                ChannelUrl = channel.Url;
                ChannelViewsCount = channel.ChannelViewsCount;
                CurrentScheduleEndTime = channel.CurrentScheduleEndTime;
                CurrentScheduleStartTime = channel.CurrentScheduleStartTime;
                Description = channel.Description;
                IsAutoPilotOn = channel.IsAutoPilotOn;
                IsFeatured = channel.IsFeatured;
                IsLocked = channel.IsLocked;
                IsPrivate = channel.IsPrivate;
                IsSubscribed = channel.IsSubscribed;
                Name = channel.Name;
                PictureUrl = channel.PictureUrl;
                PlayingVideoTubeEndTime = channel.PlayingVideoTubeEndTime;
                PlayingVideoTubeId = channel.PlayingVideoTubeId;
                PlayingVideoTubeStartTime = channel.PlayingVideoTubeStartTime;
                PlayingVideoTubeThumbnail = channel.PlayingVideoTubeThumbnail;
                PlayingVideoTubeTitle = channel.PlayingVideoTubeTitle;
                Rating = channel.Rating;
                SubscriberCount = channel.SubscriberCount;
                VideoTubeCount = channel.VideoTubeCount;
                ChannelOwnerUserName = channel.ChannelOwnerUserName;
                PlaytimeMessage = DateTimeUtils.PrintPlaytimeDuration(channel.PlayingVideoTubeStartTime, channel.PlayingVideoTubeEndTime);
                CreatedDate = channel.CreatedDate.ToString();
                ChannelOwnerUrl = channel.ChannelOwnerPublicUrl;
                LanguageId = channel.LanguageId;
                ChannelPassword = channel.ChannelPassword;
                EmbedEnabled=channel.EmbedEnabled;
                IsWhiteLabel=channel.IsWhiteLabeled;
                CustomLabel=channel.CustomLabel;
                IsMuted = channel.MuteOnStartup;
                CustomLogo = channel.CustomLogo;
                IsLogoModeActive = channel.IsLogoModeActive;                  
            }
        }
  
        [DataMember]
        public string ChannelOwnerUserName
        {
            get
            {
                return this.channelOwnerUserName;
            }
            set
            {
                this.channelOwnerUserName = value;
            }
        }

        [DataMember]
        public string ChannelOwnerUrl
        {
            get
            {
                return this.channelOwnerUrl;
            }
            set
            {
                this.channelOwnerUrl = value;
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
        public bool IsFeatured 
        {
            get 
            {
                return this.isFeatured;
            }
            set 
            {
                this.isFeatured = value;
            }
        }

        [DataMember]
        public bool IsLocked 
        {
            get 
            {
                return this.isLocked;
            }
            set 
            {
                this.isLocked = value;
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
        public String Name 
        {
            get 
            {
                return this.name;
            }
            set 
            {
                this.name = value;
            }
        }

        [DataMember]
        public String PictureUrl 
        {
            get 
            {
                return this.pictureUrl;
            }
            set 
            {
                this.pictureUrl = value;
            }
        }

        [DataMember]
        public float Rating 
        {
            get 
            {
                return this.rating;
            }
            set 
            {
                this.rating = value;
            }
        }

        [DataMember]
        public String ChannelUrl 
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
        public int VideoTubeCount 
        {
            get 
            {
                return this.videoTubeCount;
            }
            set 
            {
                this.videoTubeCount = value;
            }
        }

        [DataMember]
        public int SubscriberCount 
        {
            get 
            {
                return this.subscriberCount;
            }
            set 
            {
                this.subscriberCount = value;
            }
        }

        [DataMember]
        public int ChannelViewsCount 
        {
            get 
            {
                return this.channelViewCount;
            }
            set 
            {
                this.channelViewCount = value;
            }
        }

        [DataMember]
        public bool IsAutoPilotOn
        {
            get
            {
                return this.isAutoPilotOn;
            }
            set
            {
                this.isAutoPilotOn = value;
            }
        }

        [DataMember]
        public int PlayingVideoTubeId
        {
            get
            {
                return this.playingVideoTubeId;
            }
            set
            {
                this.playingVideoTubeId = value;
            }
        }

        [DataMember]
        public string PlayingVideoTubeTitle
        {
            get
            {
                return this.playingVideoTubeTitle;
            }
            set
            {
                this.playingVideoTubeTitle = value;
            }
        }

        [DataMember]
        public DateTime? PlayingVideoTubeStartTime
        {
            get
            {
                return this.playingVideoTubeStartTime;
            }
            set
            {
                this.playingVideoTubeStartTime = value;
            }
        }

        [DataMember]
        public DateTime? PlayingVideoTubeEndTime
        {
            get
            {
                return this.playingVideoTubeEndTime;
            }
            set
            {
                this.playingVideoTubeEndTime = value;
            }
        }

        [DataMember]
        public string PlayingVideoTubeThumbnail
        {
            get
            {
                return this.playingVideoTubeThumbnail;
            }
            set
            {
                this.playingVideoTubeThumbnail = value;
            }
        }

        [DataMember]
        public DateTime? CurrentScheduleStartTime
        {
            get
            {
                return this.currentScheduleStartTime;
            }
            set
            {
                this.currentScheduleStartTime = value;
            }
        }

        [DataMember]
        public DateTime? CurrentScheduleEndTime
        {
            get
            {
                return this.currentScheduleEndTime;
            }
            set
            {
                this.currentScheduleEndTime = value;
            }
        }

        [DataMember]
        public string CurrentScheduleEndTimeString
        {
            get
            {
                return (this.currentScheduleEndTime != null) ? this.currentScheduleEndTime.Value.ToString("MM/dd/yyyy HH:mm") : String.Empty;
            }
        }

        [DataMember]
        public string CurrentScheduleStartTimeString
        {
            get
            {
                return (this.currentScheduleStartTime != null) ? this.currentScheduleStartTime.Value.ToString("MM/dd/yyyy HH:mm") : String.Empty;
            }
        }


        [DataMember]
        public bool IsSubscribed
        {
            get
            {
                return this.isSubscribed;
            }
            set
            {
                this.isSubscribed = value;
            }
        }

        [DataMember]
        public string PlaytimeMessage
        {
            get
            {
                return this.playTimeMessage;
            }
            set
            {
                this.playTimeMessage = value;
            }
        }

        [DataMember]
        public string CreatedDate
        {
            get
            {
                return this.createdDate;
            }
            set
            {
                this.createdDate = value;
            }
        }

        [DataMember]
        public int LanguageId
        {
            get
            {
                return this.languageId;
            }
            set
            {
                this.languageId = value;
            }
        }

        [DataMember]
        public int? UserChannelRating
        {
            get
            {
                return this.userChannelRating;
            }
            set
            {
                this.userChannelRating = value;
            }
        }

        [DataMember]
        public bool IsLike
        {
            get
            {
                return this.isLike;
            }
            set
            {
                this.isLike = value;
            }
        }
        [DataMember]
        public string ChannelPassword
        {
            get
            {
                return this.channelPassword;
            }
            set
            {
                this.channelPassword = value;
            }
        }

        [DataMember]
        public string CustomLabel
        {
            get
            {
                return this.customLabel;
            }
            set
            {
                this.customLabel = value;
            }
        }
        [DataMember]
        public bool EmbedEnabled
        {
            get
            {
                return this.embedEnabled;
            }
            set
            {
                this.embedEnabled = value;
            }
        }
        [DataMember]
        public bool IsWhiteLabel
        {
            get
            {
                return this.isWhiteLabel;
            }
            set
            {
                this.isWhiteLabel = value;
            }
        }
        [DataMember]
        public bool IsMuted
        {
            get
            {
                return this.isMuted;
            }
            set
            {
                this.isMuted = value;
            }
        }
        [DataMember]
        public string CustomLogo
        {
            get
            {
                return this.customLogo;
            }
            set
            {
                this.customLogo = value;
            }
        }
        public bool IsLogoModeActive
        {
            get
            {
                return this.isLogoModeActive;
            }
            set
            {
                this.isLogoModeActive = value;
            }
        }

        public bool KeepGuideOpen { get; set; }
    }
}
