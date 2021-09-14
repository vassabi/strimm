using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Strimm.Model
{
    [DataContract]
    public class UserMembership : ModelBase
    {
        private int userMembershipId;
        private int userId;
        private String password;
        private String email;
        private String recoveryEmail;
        private bool isLockedOut;
        private DateTime? activationEmailSendDate;
        private int activationEmailRetryCount;
        private bool optOutFromEmailActivation;
        private DateTime? emailActivationOptOutDate;
        private DateTime? lastLoginDate;
        private DateTime? lastPasswordChangeDate;
        private int failedPasswordAttemptCount;
        private bool emailVerified;
        private bool isTempUser;
        private bool isExternalUser;
        private bool isAdmin;
        private bool isProEnable;

        [DataMember]
        public int UserMembershipId
        {
            get
            {
                return this.userMembershipId;
            }
            set
            {
                this.userMembershipId = value;
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
        public String Password
        {
            get
            {
                return this.password;
            }
            set
            {
                this.password = value;
            }
        }

        [DataMember]
        public String Email
        {
            get
            {
                return this.email;
            }
            set
            {
                this.email = value;
            }
        }

        [DataMember]
        public String RecoveryEmail
        {
            get
            {
                return this.recoveryEmail;
            }
            set
            {
                this.recoveryEmail = value;
            }
        }

        [DataMember]
        public bool IsLockedOut
        {
            get
            {
                return this.isLockedOut;
            }
            set
            {
                this.isLockedOut = value;
            }
        }

        [DataMember]
        public DateTime? ActivationEmailSendDate
        {
            get
            {
                return this.activationEmailSendDate;
            }
            set
            {
                this.activationEmailSendDate = value;
            }
        }

        [DataMember]
        public int ActivationEmailRetryCount
        {
            get
            {
                return this.activationEmailRetryCount;
            }
            set
            {
                this.activationEmailRetryCount = value;
            }
        }

        [DataMember]
        public bool OptOutFromEmailActivation
        {
            get
            {
                return this.optOutFromEmailActivation;
            }
            set
            {
                this.optOutFromEmailActivation = value;
            }
        }

        [DataMember]
        public DateTime? EmailActivationOptOutDate
        {
            get
            {
                return this.emailActivationOptOutDate;
            }
            set
            {
                this.emailActivationOptOutDate = value;
            }
        }

        [DataMember]
        public DateTime? LastLoginDate
        {
            get
            {
                return this.lastLoginDate;
            }
            set
            {
                this.lastLoginDate = value;
            }
        }

        [DataMember]
        public DateTime? LastPasswordChangeDate
        {
            get
            {
                return this.lastPasswordChangeDate;
            }
            set
            {
                this.lastPasswordChangeDate = value;
            }
        }

        [DataMember]
        public int FailedPasswordAttemptCount
        {
            get
            {
                return this.failedPasswordAttemptCount;
            }
            set
            {
                this.failedPasswordAttemptCount = value;
            }
        }

        [DataMember]
        public bool EmailVerified
        {
            get
            {
                return this.emailVerified;
            }
            set
            {
                this.emailVerified = value;
            }
        }

        [DataMember]
        public bool IsTempUser
        {
            get
            {
                return this.isTempUser;
            }
            set
            {
                this.isTempUser = value;
            }
        }

        [DataMember]
        public bool IsExternalUser
        {
            get
            {
                return this.isExternalUser;
            }
            set
            {
                this.isExternalUser = value;
            }
        }

        [DataMember]
        public bool IsAdmin
        {
            get
            {
                return this.isAdmin;
            }
            set
            {
                this.isAdmin = value;
            }
        }

        [DataMember]
        public bool IsProEnable
        {
            get
            {
                return this.isProEnable;
            }
            set
            {
                this.isProEnable = value;
            }
        }

    }
}
