using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Model.Projections
{
    [DataContract]
    public class VideoTubePo : VideoTube
    {
        private string videoProviderName;
        private string categoryName;
        private bool isScheduled;
        private int useCounter;
        private int viewCounter;
        private DateTime dateAdded;
        private int ownerUserId;
        private string ownerUserName;
        private string ownerPublicUrl;
        private String videoPreviewKey;
        private String thumbnail;
        private bool isProcessing;
        private DateTime? lastScheduleDateTime;

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
        public int ViewCounter
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
        public DateTime DateAdded
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
    }
}
