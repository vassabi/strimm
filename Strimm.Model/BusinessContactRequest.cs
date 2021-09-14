using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Model
{
    [DataContract]
    public class BusinessContactRequest
    {
        private int businessContactRequestId;
        private string name;
        private string company;
        private string websiteUrl;
        private string email;
        private string phoneNumber;
        private string packageType;
        private string comments;
        private DateTime addedDate;

        [DataMember]
        public int BusinessContactRequestId
        {
            get
            {
                return this.businessContactRequestId;
            }
            set
            {
                this.businessContactRequestId = value;
            }
        }
        [DataMember]
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }
        [DataMember]
        public string Company
        {
            get
            {
                return this.company;
            }
            set
            {
                this.company = value;
            }
        }
        [DataMember]
        public string WebsiteUrl
        {
            get
            {
                return this.websiteUrl;
            }
            set
            {
                this.websiteUrl = value;
            }
        }
        [DataMember]
        public string Email
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
        public string PhoneNumber
        {
            get
            {
                return this.phoneNumber;
            }
            set
            {
                this.phoneNumber = value;
            }
        }
        [DataMember]
        public string PackageType
        {
            get
            {
                return this.packageType;
            }
            set
            {
                this.packageType = value;
            }
        }
        [DataMember]
        public string Comments
        {
            get
            {
                return this.comments;
            }
            set
            {
                this.comments = value;
            }
        }
        [DataMember]
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
