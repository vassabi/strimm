using Strimm.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Model.WebModel
{
    [DataContract]
    public class ChannelScheduleModel : BaseModel
    {
        private string message;
        private int channelTubeId;
        private int channelScheduleId;
        private List<VideoScheduleModel> videoSchedules;
        private bool allowEdit;
        private bool allowRepeat;
        private bool allowDelete;
        private string startTime;
        private string endTime;
        private bool published;
        private DateTime startDateAndTime;
        private DateTime? endDateAndTime;
        private bool expandVideoSchedulesList;

        public ChannelScheduleModel()
        {

        }

        public ChannelScheduleModel(ChannelSchedule schedule)
        {
            if (schedule != null)
            {
                ChannelScheduleId = schedule.ChannelScheduleId;
                ChannelTubeId = schedule.ChannelTubeId;
                VideoSchedules = new List<VideoScheduleModel>();
                Published = schedule.Published;
                StartDateAndTime = schedule.StartTime;
            }
        }

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
        public List<VideoScheduleModel> VideoSchedules
        {
            get
            {
                return this.videoSchedules;
            }
            set
            {
                this.videoSchedules = value;
            }
        }

        [DataMember]
        public bool AllowEdit
        {
            get
            {
                return this.allowEdit;
            }
            set
            {
                this.allowEdit = value;
            }
        }

        [DataMember]
        public bool AllowRepeat
        {
            get
            {
                return this.allowRepeat;
            }
            set
            {
                this.allowRepeat = value;
            }
        }

        [DataMember]
        public bool AllowDelete
        {
            get
            {
                return this.allowDelete;
            }
            set
            {
                this.allowDelete = value;
            }
        }

        [DataMember]
        public string StartTime
        {
            get
            {
                return this.startDateAndTime != null 
                    ? DateTimeUtils.PrintAirTime(this.startDateAndTime)
                    : String.Empty;
            }
        }

        [DataMember]
        public string EndTime
        {
            get
            {
                return this.endDateAndTime != null && this.endDateAndTime.HasValue
                   ? DateTimeUtils.PrintAirTime(this.endDateAndTime.Value)
                   : String.Empty;
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
        public DateTime StartDateAndTime
        {
            get
            {
                return this.startDateAndTime;
            }
            set
            {
                this.startDateAndTime = value;
            }
        }

        [DataMember]
        public DateTime? EndDateAndTime
        {
            get
            {
                return this.endDateAndTime;
            }
            set
            {
                this.endDateAndTime = value;
            }
        }

        [DataMember]
        public bool ExpandVideoSchedulesList
        {
            get
            {
                return this.expandVideoSchedulesList;
            }
            set
            {
                this.expandVideoSchedulesList = value;
            }
        }
    }
}
