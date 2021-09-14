using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Model.Projections
{
    [DataContract]
    public class VideoTubeCounterEntity
    {
        private int entityId;
        private String entityName;
        private int videoTubeCounter;

        [DataMember]
        public int EntityId
        {
            get
            {
                return this.entityId;
            }
            set
            {
                this.entityId = value;
            }
        }

        [DataMember]
        public String EntityName
        {
            get
            {
                return this.entityName;
            }
            set
            {
                this.entityName = value;
            }
        }

        [DataMember]
        public int VideoTubeCounter
        {
            get
            {
                return this.videoTubeCounter;
            }
            set
            {
                this.videoTubeCounter = value;
            }
        }
    }
}
