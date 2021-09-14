using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Model
{
    [DataContract]
    public class UnpublishedChannelScheduleEmail : ModelBase
    {
        private int unpublishedChannelScheduleEmailId;
        private int channelScheduleId;
        private DateTime timeSent;

        [DataMember]
        public int UnpublishedChannelScheduleEmailId
        {
            get
            {
                return this.unpublishedChannelScheduleEmailId;
            }
            set
            {
                this.unpublishedChannelScheduleEmailId = value;
            }
        }

        [DataMember]
        public int ChannelScheduleId
        {
            get
            {
                return this.channelScheduleId;
            }
            set
            {
                this.channelScheduleId = value;
            }
        }

        [DataMember]
        public DateTime TimeSent
        {
            get
            {
                return this.timeSent;
            }
            set
            {
                this.timeSent = value;
            }
        }
    }
}
