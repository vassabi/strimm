using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Model
{
    [DataContract]
    public class UserEmailOpoutGroup
    {
        private int emailOptoutGroupId;
        private int userId;
        private bool unsubscribed;
        private DateTime asOfDate;

        [DataMember]
        public int EmailOptoutGroupId
        {
            get
            {
                return this.emailOptoutGroupId;
            }
            set
            {
                this.emailOptoutGroupId = value;
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
        public bool Unsubscribed
        {
            get
            {
                return this.unsubscribed;
            }
            set
            {
                this.unsubscribed = value;
            }
        }

        [DataMember]
        public DateTime AsOfDate
        {
            get
            {
                return this.asOfDate;
            }
            set
            {
                this.asOfDate = value;
            }
        }
    }
}
