using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Model.Aws
{
    [DataContract]
    public class AmazonJobInputDetailsMessage
    {
        [DataMember]
        public string key { get; set; }
        [DataMember]
        public string frameRate { get; set; }
        [DataMember]
        public string resolution { get; set; }
        [DataMember]
        public string aspectRatio { get; set; }
        [DataMember]
        public string interlaced { get; set; }
        [DataMember]
        public string container { get; set; }
    }
}
