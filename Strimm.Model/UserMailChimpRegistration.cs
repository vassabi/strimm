using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Model
{
    [DataContract]
    public class UserMailChimpRegistration : ModelBase
    {
        private int userMailChimpRegistrationId;
        private int userId;
        private DateTime? registrationDate;

        [DataMember]
        public int UserMailChimpRegistrationId
        {
            get
            {
                return this.userMailChimpRegistrationId;
            }
            set
            {
                this.userMailChimpRegistrationId = value;
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
        public DateTime? RegistrationDate
        {
            get
            {
                return this.registrationDate;
            }
            set
            {
                this.registrationDate = value;
            }
        }
    }
}
