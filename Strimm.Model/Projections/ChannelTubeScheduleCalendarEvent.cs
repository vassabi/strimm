using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Model.Projections
{
    [DataContract]
    public class ChannelTubeScheduleCalendarEvent
    {
        private string message;
        private int channelTubeId;
        private int channelScheduleId;
        private string startTime;
        private string endTime;
        private bool isActive;

        [DataMember]
        public string Message
        {
            get
            {
                return this.message;
            }
            set
            {
                this.message = value;
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

        public string StartTime
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

        public string EndTime
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
    }
}
