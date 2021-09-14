using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Model.Projections
{
    public class UnpublishedChannelSchedulePo : ChannelSchedule
    {
        private string userFirstName;
        private string userEmail;
        private string channelName;

        public string UserFirstName
        {
            get
            {
                return this.userFirstName;
            }
            set
            {
                this.userFirstName = value;
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

        public string ChannelName
        {
            get
            {
                return this.channelName;
            }
            set
            {
                this.channelName = value;
            }
        }
    }
}
