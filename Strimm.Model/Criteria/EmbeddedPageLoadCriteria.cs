using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Model.Criteria
{
    [DataContract]
    public class EmbeddedPageLoadCriteria
    {

        [DataMember]
        public int channelTubeId { get; set; }
        [DataMember]
        public string clientTime { get; set; }
        [DataMember]
        public string embeddedHostUrl { get; set; }
        [DataMember]
        public string accountNumber { get; set; }
        [DataMember]
        public bool isSingleChannelView { get; set; }
        [DataMember]
        public bool IsSubscribedDomain { get; set; }

       


    }
}
