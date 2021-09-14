using System;
using System.Runtime.Serialization;

namespace Strimm.Model
{
    [DataContract]
    public class VideoProvider : ModelBase
    {
        private int videoProviderId;
        private String name;
        private String description;
        private bool isActive;
        private DateTime? prodEffectiveDate;
        private DateTime? qaEffectiveDate;
        private int userId;
        private bool isExternal;
        private string domainValidationRegex;

        [DataMember]
        public int VideoProviderId
        {
            get
            {
                return this.videoProviderId;
            }
            set
            {
                this.videoProviderId = value;
            }
        }

        [DataMember]
        public String Name
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
        public String Description
        {
            get
            {
                return this.description;
            }
            set
            {
                this.description = value;
            }
        }

        [DataMember]
        public bool IsActive
        {
            get
            {
                return this.isActive;
            }
            set
            {
                this.isActive = value;
            }
        }

        [DataMember]
        public DateTime? ProdEffectiveDate
        {
            get
            {
                return this.prodEffectiveDate;
            }
            set
            {
                this.prodEffectiveDate = value;
            }
        }

        [DataMember]
        public DateTime? QaEffectiveDate
        {
            get
            {
                return this.qaEffectiveDate;
            }
            set
            {
                this.qaEffectiveDate = value;
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
        public bool IsExternal
        {
            get
            {
                return this.isExternal;
            }
            set
            {
                this.isExternal = value;
            }
        }

        [DataMember]
        public string DomainValidationRegex
        {
            get
            {
                return this.domainValidationRegex;
            }
            set
            {
                this.domainValidationRegex = value;
            }
        }
    }
}
