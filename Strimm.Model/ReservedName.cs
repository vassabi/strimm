using System;
using System.Runtime.Serialization;

namespace Strimm.Model
{
    [DataContract]
    public class ReservedName
    {
        private int reservedNameId;
        private String name;
        private String description;
        private int? overrideCodeId;

        [DataMember]
        public int ReservedNameId
        {
            get
            {
                return this.reservedNameId;
            }
            set
            {
                this.reservedNameId = value;
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
        public int? OverrideCodeId
        {
            get
            {
                return this.overrideCodeId;
            }
            set
            {
                this.overrideCodeId = value;
            }
        }
    }
}
