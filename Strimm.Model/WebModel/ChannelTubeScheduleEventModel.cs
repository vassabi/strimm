using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Model.WebModel
{
    [DataContract]
    public class ChannelTubeScheduleEventModel : BaseModel
    {
        /// <summary>
        /// Channel Tube Id
        /// </summary>
        [DataMember]
        public int ChannelTubeId { get; set; }

        /// <summary>
        /// Channel Schedule Id
        /// </summary>
        [DataMember]
        public int ChannelScheduleId { get; set; }

        /// <summary>
        /// Channel Schedule Start Time
        /// </summary>
        [DataMember]
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Boolean flag that marks channel schedule as Active
        /// </summary>
        [DataMember]
        public bool IsActive { get; set; }
    }
}
