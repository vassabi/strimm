using System;
using System.Runtime.Serialization;

namespace Strimm.Model
{
    [DataContract]
    public class ChannelSubscription : ModelBase
    {
        private int channelSubscriptionId;
        private int userId;
        private int channelTubeId;
        private DateTime subscriptionStartDate;
        private DateTime? subscriptionEndDate;

        [DataMember]
        public int ChannelSubscriptionId
        {
            get
            {
                return this.channelSubscriptionId;
            }
            set
            {
                this.channelSubscriptionId = value;
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
        public DateTime SubscriptionStartDate
        {
            get
            {
                return this.subscriptionStartDate;
            }
            set
            {
                this.subscriptionStartDate = value;
            }
        }

        [DataMember]
        public DateTime? SubscriptionEndDate
        {
            get
            {
                return this.subscriptionEndDate;
            }
            set
            {
                this.subscriptionEndDate = value;
            }
        }
    }
}
