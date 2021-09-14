using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Model.Projections
{
    public class UnpublishedChannelScheduleEmailPo
    {
        private int channelScheduleId;
        private int sentEmailCount;
        private DateTime timeSent;
        private string userEmail;

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

        public int SentEmailCount
        {
            get
            {
                return this.sentEmailCount;
            }
            set
            {
                this.sentEmailCount = value;
            }
        }

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

        public string UserEmail
        {
            get
            {
                return this.userEmail;
            }
            set
            {
                this.userEmail = value;
            }
        }
    }
}
