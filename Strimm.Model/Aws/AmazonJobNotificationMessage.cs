using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Model.Aws
{
    [DataContract]
    public class AmazonJobNotificationMessage
    {
        [DataMember]
        public string state { get; set; }
        [DataMember]
        public string errorCode { get; set; }
        [DataMember]
        public string messageDetails { get; set; }
        [DataMember]
        public string version { get; set; }
        [DataMember]
        public string jobId { get; set; }
        [DataMember]
        public string pipelineId { get; set; }
        [DataMember]
        public string outputKeyPrefix { get; set; }
        [DataMember]
        public AmazonJobInputDetailsMessage input { get; set; }
        [DataMember]
        public AmazonJobOutputDetailsMessage[] outputs { get; set; }
        [DataMember]
        public AmazonJobMetaDataDetailsMessage userMetadata { get; set; }

    }
}
