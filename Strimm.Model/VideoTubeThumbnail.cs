using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Model
{
    [DataContract]
    public class VideoTubeThumbnail : ModelBase
    {
        private int videoTubeThumbnailId;
        private int videoTubeId;
        private string thumbnailKey;
        private bool isActive;
        private int position;
        private DateTime addedDateTime;

        [DataMember]
        public int VideoTubeThumbnailId
        {
            get
            {
                return this.videoTubeThumbnailId;
            }
            set
            {
                this.videoTubeThumbnailId = value;
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
        public String ThumbnailKey
        {
            get
            {
                return this.thumbnailKey;
            }
            set
            {
                this.thumbnailKey = value;
            }
        }

        [DataMember]
        public bool IsActive
        {
            get
            {
                return this.isActive;
            }
            set
            {
                this.isActive = value;
            }
        }

        [DataMember]
        public int Position
        {
            get
            {
                return this.position;
            }
            set
            {
                this.position = value;
            }
        }

        [DataMember]
        public DateTime AddedDateTime
        {
            get
            {
                return this.addedDateTime;
            }
            set
            {
                this.addedDateTime = value;
            }
        }

    }
}
