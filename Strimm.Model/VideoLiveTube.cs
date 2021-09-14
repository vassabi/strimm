using System;
using System.Runtime.Serialization;

namespace Strimm.Model
{
    [DataContract]
    public class VideoLiveTube : ModelBase
    {
        private int videoLiveTubeId;
        private String title;
        private String description;
        private int categoryId;
        private int videoProviderId;
        private bool isRRated;
        private bool isRemovedByProvider;
        private bool isRestrictedByProvider;
        private bool isPrivate;
        private String providerVideoId;
        private String thumbnail;
        private DateTime startTime;
        private DateTime? endTime;
       

        [DataMember]
        public int VideoLiveTubeId
        {
            get
            {
                return this.videoLiveTubeId;
            }
            set
            {
                this.videoLiveTubeId = value;
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
        public DateTime StartTime
        {
            get
            {
                return this.startTime;
            }
            set
            {
                this.startTime = value;
            }
        }

        [DataMember]
        public DateTime? EndTime
        {
            get
            {
                return this.endTime;
            }
            set
            {
                this.endTime = value;
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
    }
}
