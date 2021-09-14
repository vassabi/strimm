using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Model
{
    [DataContract]
    public class SubscriberDomain
    {
        private int subscriberDomainId;
        private int userId;
        private string userDomain;
        private DateTime addedDate;

        public int SubscriberDomainId
        {
            get
            {
                return this.subscriberDomainId;
            }
            set
            {
                this.subscriberDomainId = value;
            }
        }

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

        public string UserDomain
        {
            get
            {
                return this.userDomain;
            }
            set
            {
                this.userDomain = value;
            }
        }

        public DateTime AddedDate
        {
            get
            {
                return this.addedDate;
            }
            set
            {
                this.addedDate = value;
            }
        }
    }
}
