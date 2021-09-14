using System;
using System.Runtime.Serialization;
using Strimm.Shared;

namespace Strimm.Model
{
    [DataContract]
    public class ChannelTube : ModelBase
    {
        private int channelTubeId;
        private String name;
        private String description;
        private int categoryId;
        private String pictureUrl;
        private String url;       
        private bool isFeatured;
        private bool isPrivate;
        private bool isLocked;
        private int userId;
        private bool isDeleted;
        private bool isAutoPilotOn;
        private int languageId;
        private bool isWhiteLabeled;
        private String channelPassword;
        private bool embedEnabled;
        private bool muteOnStartup;
        private String customLabel;
        private bool customPlayerControlsEnabled;
        private bool matureContentEnabled;
        private bool embedOnlyModeEnabled;
        private bool isLogoModeActive;
        private string customLogo;
        private bool playLiveFirst;
        private bool keepGuideOpen;
        private bool isAddedToRoku;

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
        public bool PlayLiveFirst
        {
            get { return this.playLiveFirst; }
            set { this.playLiveFirst = value; }
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
        public String Url
        {
            get
            {
                return this.url;
            }
            set
            {
                this.url = value;
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
        public int UserId
        {
            get
            {
                return this.userId;
            }
            set
            {
                this.userId = value;
            }
        }

        public bool IsDeleted
        {
            get
            {
                return this.isDeleted;
            }
            set
            {
                this.isDeleted = value;
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
                this.isAutoPilotOn= value;
            }
        }

        [DataMember]
        public bool IsWhiteLabeled
        {
            get
            {
                return this.isWhiteLabeled;
            }
            set
            {
                this.isWhiteLabeled = value;
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
        public String ChannelPassword
        {
            get { return this.channelPassword; }
            set { this.channelPassword = value; }
        }

        [DataMember]
        public bool IsLogoModeActive
        {
            get { return this.isLogoModeActive; }
            set { this.isLogoModeActive = value; }
        }
        [DataMember]
        public string CustomLogo
        {
            get { return this.customLogo; }
            set { this.customLogo = value; }
        }
        [DataMember]
        public bool KeepGuideOpen
        {
            get { return this.keepGuideOpen; }
            set { this.keepGuideOpen = value; }
        }
        [DataMember]
        public bool IsAddedToRoku
        {
            get { return this.isAddedToRoku; }
            set { this.isAddedToRoku = value; }
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

        public String CustomLabel
        {
            get { return this.customLabel; }
            set { this.customLabel = value; }
        }

        public bool MatureContentEnabled
        {
            get { return this.matureContentEnabled; }
            set { this.matureContentEnabled = value; }
        }

        public bool EmbedOnlyModeEnabled
        {
            get { return this.embedOnlyModeEnabled; }
            set { this.embedOnlyModeEnabled = value; }
        }

        public bool CustomPlayerControlsEnabled
        {
            get { return this.customPlayerControlsEnabled; }
            set { this.customPlayerControlsEnabled = value; }
        }
    }
}
