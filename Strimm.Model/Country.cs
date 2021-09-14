using System;
using System.Runtime.Serialization;

namespace Strimm.Model
{
    [DataContract]
    public class Country
    {
        private int countryId;
        private String name;
        private String code_3;
        private String code_2;

        [DataMember]
        public int CountryId
        {
            get
            {
                return this.countryId;
            }
            set
            {
                this.countryId = value;
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
        public String Code_3
        {
            get
            {
                return this.code_3;
            }
            set
            {
                this.code_3 = value;
            }
        }

        [DataMember]
        public String Code_2
        {
            get
            {
                return this.code_2;
            }
            set
            {
                this.code_2 = value;
            }
        }
    }
}
