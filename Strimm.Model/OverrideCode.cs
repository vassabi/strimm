using System;
using System.Runtime.Serialization;

namespace Strimm.Model
{
    [DataContract]
    public class OverrideCode : ModelBase
    {
        private int overrideCodeId;
        private String code;
        private int userId;
        private String comments;

        [DataMember]
        public int OverrideCodeId
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

        [DataMember]
        public String Code
        {
            get
            {
                return this.code;
            }
            set
            {
                this.code = value;
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
        public String Comments
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
    }
}
