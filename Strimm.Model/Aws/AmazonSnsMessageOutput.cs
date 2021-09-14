using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Model.Aws
{
    [DataContract]
    public class AmazonSnsMessageOutput
    {
        [DataMember]
        public string id { get; set; }
        [DataMember]
        public string presetId { get; set; }
        [DataMember]
        public string key { get; set; }
        [DataMember]
        public string thumbnailPattern { get; set; }
        [DataMember]
        public string rotate { get; set; }
        [DataMember]
        public string status { get; set; }
        [DataMember]
        public string duration { get; set; }
        [DataMember]
        public string width { get; set; }
        [DataMember]
        public string height { get; set; }
    }
}
