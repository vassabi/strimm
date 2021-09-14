using System;
using System.Runtime.Serialization;

namespace Strimm.Model
{
    [DataContract]
    public class VideoTube : ModelBase
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
        private String keywords;
        private String videoStatus;
        private String videoStatusMessage;
        private String videoKey;
        private String thumbnail;
        private bool privateVideoModeEnabled;
       

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

        [DataMember]
        public String VideoStatus
        {
            get
            {
                return this.videoStatus;
            }
            set
            {
                this.videoStatus = value;
            }
        }

        [DataMember]
        public String VideoStatusMessage
        {
            get
            {
                return this.videoStatusMessage;
            }
            set
            {
                this.videoStatusMessage = value;
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

       

    }
}
