using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Model.Projections
{
    [DataContract]
    public class VideoLiveTubePo : VideoLiveTube
    {
        private string videoProviderName;
        private string categoryName;
        private int ownerUserId;
        private string ownerUserName;
        private string ownerPublicUrl;
        private String thumbnail;
        private int viewCounter;

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
        public double Duration { get; set; }
    }
}
