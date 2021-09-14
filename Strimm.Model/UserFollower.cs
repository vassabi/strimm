using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Strimm.Model
{
    [DataContract]
    public class UserFollower : ModelBase
    {
        private int userFollowerId;
        private int userId;
        private int follewerUserId;
        private DateTime startedFollowDate;
        private DateTime? stoppedFollowDate;

        [DataMember]
        public int UserFollowerId
        {
            get
            {
                return this.userFollowerId;
            }
            set
            {
                this.userFollowerId = value;
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
        public int FollewerUserId
        {
            get
            {
                return this.follewerUserId;
            }
            set
            {
                this.follewerUserId = value;
            }
        }

        [DataMember]
        public DateTime StartedFollowDate
        {
            get
            {
                return this.startedFollowDate;
            }
            set
            {
                this.startedFollowDate = value;
            }
        }

        [DataMember]
        public DateTime? StoppedFollowDate
        {
            get
            {
                return this.stoppedFollowDate;
            }
            set
            {
                this.stoppedFollowDate = value;
            }
        }
    }
}
