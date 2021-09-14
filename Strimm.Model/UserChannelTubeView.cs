using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Model
{
    [DataContract]
   public class UserChannelTubeView
    {
        private int userId;
        private int channelTubeId;
        private DateTime viewTime;

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
        public DateTime ViewTime
        {
            get
            {
                return this.viewTime;
            }
            set
            {
                this.viewTime=value;
            }
        }
    }
}
