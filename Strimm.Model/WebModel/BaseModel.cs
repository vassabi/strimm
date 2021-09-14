using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Model.WebModel
{
    [DataContract]
    public class BaseModel
    {
        private String url;
        private String message;
        private bool isSuccess;

        [DataMember]
        public String Url
        {
            get
            {
                return this.url;
            }
            set
            {
                this.url = value;
            }
        }

        [DataMember]
        public String Message
        {
            get
            {
                return this.message;
            }
            set
            {
                this.message = value;
            }
        }

        [DataMember]
        public bool IsSuccess
        {
            get
            {
                return isSuccess;
            }
            set
            {
                this.isSuccess = value;
            }
        }
    }
}
