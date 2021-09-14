using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Model.WebModel
{
    [DataContract]
    public class UserCredentials
    {
        [DataMember]
        public string LoginIdentifier { get; set; }

        [DataMember]
        public string Password { get; set; }
    }
}
