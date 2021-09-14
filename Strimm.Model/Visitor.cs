using System;
using System.Runtime.Serialization;

namespace Strimm.Model
{
    [DataContract]
    public class Visitor : ModelBase
    {
        private int visitorId;
        private int? userId;
        private String ipAddress;
        private DateTime visitDate;
        private float visitDuration;
        private int? channelTubeId;
        private String destination;
        private int? visitorUserId;

        [DataMember]
        public int VisitorId
        {
            get
            {
                return this.visitorId;
            }
            set
            {
                this.visitorId = value;
            }
        }

        [DataMember]
        public int? UserId
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
        public String IpAddress
        {
            get
            {
                return this.ipAddress;
            }
            set
            {
                this.ipAddress = value;
            }
        }

        [DataMember]
        public DateTime VisitDate
        {
            get
            {
                return this.visitDate;
            }
            set
            {
                this.visitDate = value;
            }
        }

        [DataMember]
        public float VisitDuration
        {
            get
            {
                return this.visitDuration;
            }
            set
            {
                this.visitDuration = value;
            }
        }

        [DataMember]
        public int? ChannelTubeId
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
        public String Destination
        {
            get
            {
                return this.destination;
            }
            set
            {
                this.destination = value;
            }
        }
        [DataMember]
      
        public int? VisitorUserId
        {
            get
            {
                return this.visitorUserId;
            }
            set
            {
                this.visitorUserId = value;
            }
        }
    }
}
