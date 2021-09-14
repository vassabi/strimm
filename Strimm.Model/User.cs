using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;

namespace Strimm.Model
{
    [DataContract]
    public class User : ModelBase
    {
        private int userId;
        private String userName;
        private Guid? externalUserId;
        private DateTime lastUpdatedDate;
        private String accountNumber;
        private bool isDeleted;
        private String publicUrl;

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
        public String UserName
        {
            get
            {
                return this.userName;
            }
            set
            {
                this.userName = value;
            }
        }

        [DataMember]
        public String PublicUrl
        {
            get
            {
                return this.publicUrl;
            }
            set
            {
                this.publicUrl = value;
            }
        }

        [DataMember]
        public Guid? ExternalUserId
        {
            get
            {
                return this.externalUserId;
            }
            set
            {
                this.externalUserId = value;
            }
        }

        [DataMember]
        public DateTime LastUpdatedDate
        {
            get
            {
                return this.lastUpdatedDate;
            }
            set
            {
                this.lastUpdatedDate = value;
            }
        }

        [DataMember]
        public String AccountNumber
        {
            get
            {
                return this.accountNumber;
            }
            set
            {
                this.accountNumber = value;
            }
        }

        [DataMember]
        public bool IsDeleted
        {
            get
            {
                return this.isDeleted;
            }
            set
            {
                this.isDeleted = value;
            }
        }
    }
}
