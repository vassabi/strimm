using System;
using System.Runtime.Serialization;

namespace Strimm.Model
{
    [DataContract]
    public class ChannelLike :ModelBase
    {
        private int channelLikeId;
        private int userId;
        private int channelTubeId;
        private DateTime? likeStartDate;
        private DateTime? likeEndDate;
        private bool isLike;
        private bool isNotLike;

        [DataMember]
        public int ChannelLikeId
        {
            get
            {
                return this.channelLikeId;
            }
            set
            {
                this.channelLikeId = value;
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
        public DateTime? LikeStartDate
        {
            get
            {
                return this.likeStartDate;
            }

            set
            {
                this.likeStartDate = value;
            }
        }

        [DataMember]
        public DateTime? LikeEndDate
        {
            get
            {
                return this.likeEndDate;
            }

            set
            {
                this.likeEndDate = value;
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
        public bool IsNotLike
        {
            get
            {
                return this.isNotLike;
            }

            set
            {
                this.isNotLike = value;
            }
        }
    }
}
