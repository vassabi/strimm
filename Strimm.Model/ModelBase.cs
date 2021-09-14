using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using Dapper.Contrib.Extensions;

namespace Strimm.Model
{
    [DataContract]
    public abstract class ModelBase : IVersion
    {
        private DateTime? createdDate;
        private String uri;

        [DataMember]
        public String Uri
        {
            get
            {
                return this.uri;
            }
            set
            {
                this.uri = value;
            }
        }

        [DataMember]
        public DateTime? CreatedDate
        {
            get
            {
                return this.createdDate;
            }
            set
            {
                this.createdDate = value;
            }
        }
    }
}
