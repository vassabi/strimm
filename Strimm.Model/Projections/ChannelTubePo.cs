using Strimm.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Model.Projections
{
    public class ChannelTubePo : ChannelTube
    {
        private string categoryName;
        private int videoTubeCount;
        private int subscriberCount;
        private int channelViewsCount;
        private bool isSubscribed;
        private int playingVideoTubeId;
        private string playingVideoTubeTitle;
        private DateTime? playingVideoTubeStartTime;
        private DateTime? playingVideoTubeEndTime;
        private string playingVideoTubeThumbnail;
        private DateTime? currentScheduleStartTime;
        private DateTime? currentScheduleEndTime;
        private string channelOwnerUserName;
        private string channelOwnerPublicUrl;
        private float rating;
        private int likeCount;
        private int languageId;
        private int? userChannelRating;
        private bool isLike;
        private bool isWhiteLabeled;
        private bool embedEnabled;
        private bool muteOnStartup;
        private string channelPassword;
        private string customLabel;
        private string userDomain;
        private bool hasMatureContent;
        private bool customPlayerControlsEnabled;
        private bool isLogoModeActive;
        private string customLogo;

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

        public string ChannelOwnerPublicUrl
        {
            get
            {
                return this.channelOwnerPublicUrl;
            }
            set
            {
                this.channelOwnerPublicUrl = value;
            }
        }

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

        public int SubscriberCount
        {
            get
            {
                return this.subscriberCount;
            }
            set
            {
                this.subscriberCount = value;

                this.IsSubscribed = (this.subscriberCount > 0);
            }
        }

        public int ChannelViewsCount
        {
            get
            {
                return this.channelViewsCount;
            }
            set
            {
                this.channelViewsCount = value;
            }
        }

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

        public int LikeCount
        {
            get
            {
                return this.likeCount;
            }
            set
            {
                this.likeCount = value;
            }
        }
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

        public bool IsWhiteLabeled
        {
            get { return this.isWhiteLabeled; }
            set { this.isWhiteLabeled = value; }
        }

       
        public bool EmbedEnabled
        {
            get { return this.embedEnabled; }
            set { this.embedEnabled = value; }
        }

        public bool MuteOnStartup
        {
            get { return this.muteOnStartup; }
            set { this.muteOnStartup = value; }
        }
        public string ChannelPassword
        {
            get { return this.channelPassword; }
            set { this.channelPassword = value; }
        }

        public string CustomLabel
        {
            get { return this.customLabel; }
            set { this.customLabel = value;  }
        }
        public string UserDomain
        {
            get { return this.userDomain; }
            set { this.userDomain = value; }
        }

        public bool HasMatureContent
        {
            get { return this.hasMatureContent; }
            set { this.hasMatureContent = value; }
        }

        public bool CustomPlayerControlsEnabled
        {
            get { return this.customPlayerControlsEnabled; }
            set { this.customPlayerControlsEnabled = value; }
        }

        public  bool IsLogoModeActive
    {
        get { return this.isLogoModeActive; }
        set { this.isLogoModeActive = value; }
    }
        public string CustomLogo
        {
            get { return this.customLogo; }
            set { this.customLogo = value; }
        }


        public bool PlayLiveFirst { get; set; }
    }
}
