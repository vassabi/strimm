using System;
using System.Runtime.Serialization;

namespace Strimm.Model
{
    [DataContract]
    public class Language
    {
        private int languageId;
        private String name;


        [DataMember]
        public int LanguageId
        {
            get
            {
                return this.languageId;
            }
            set
            {
                this.languageId = value;
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


    }
}

