using System;
using System.Runtime.Serialization;

namespace Strimm.Model
{
    [DataContract]
    public class ChannelSchedule : ModelBase
    {
        private int channelScheduleId;
        private int channelTubeId;
        private DateTime startTime;
        private bool published;
        private bool isDeleted;

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
        public bool Published
        {
            get
            {
                return this.published;
            }
            set
            {
                this.published = value;
            }
        }

        [DataMember]
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
    }
}
