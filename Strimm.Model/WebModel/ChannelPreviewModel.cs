using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Model.WebModel
{
    public class ChannelPreviewModel: BaseModel
    {
        private ChannelTubeModel channel;
        private ChannelScheduleModel activeSchedule;
        private List<VideoScheduleModel> playlist;
        private bool isSubscribed;
        private bool isMyChannel;
        private string nextScheduleStartTimeString;
        private DateTime nextScheduleStartDateTime;
        private UserModel user;

        public ChannelTubeModel Channel
        {
            get
            {
                return this.channel;
            }
            set
            {
                this.channel = value;
            }
        }

        public ChannelScheduleModel ActiveSchedule
        {
            get
            {
                return this.activeSchedule;
            }
            set
            {
                this.activeSchedule = value;
            }
        }

        public UserModel User
        {
            get
            {
                return this.user;
            }
            set
            {
                this.user = value;
            }
        }

        public bool IsSubscribed
        {
            get
            {
                return this.isSubscribed;
            }
            set
            {
                this.isSubscribed = value;
            }
        }

        public bool IsMyChannel
        {
            get
            {
                return this.isMyChannel;
            }
            set
            {
                this.isMyChannel = value;
            }
        }

        public List<VideoScheduleModel> Playlist
        {
            get
            {
                return this.playlist;
            }
            set
            {
                this.playlist = value;
            }
        }

        public string NextScheduleStartTimeString
        {
            get
            {
                return this.nextScheduleStartTimeString;
            }
            set
            {
                this.nextScheduleStartTimeString = value;
            }
        }

        public DateTime NextScheduleStartDateTime
        {
            get
            {
                return this.nextScheduleStartDateTime;
            }
            set
            {
                this.nextScheduleStartDateTime = value;
            }
        }

        public bool IsRestricted { get; set; }
    }
}
