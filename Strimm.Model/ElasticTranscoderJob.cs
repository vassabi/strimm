using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Model
{
    [DataContract]
    public class ElasticTranscoderJob : ModelBase
    {
        private int elasticTranscoderJobId;
        private string jobId;
        private string pipelineId;
        private string jobArn;
        private string inputKey;
        private string outputKey;
        private string presetId;
        private string status;
        private int userId;
        private int videoTubeId;
        private DateTime createdDateTime;
        private DateTime modifiedDateTime;

        [DataMember]
        public int ElasticTranscoderJobId
        {
            get
            {
                return this.elasticTranscoderJobId;
            }
            set
            {
                this.elasticTranscoderJobId = value;
            }
        }

        [DataMember]
        public string JobId
        {
            get
            {
                return this.jobId;
            }
            set
            {
                this.jobId = value;
            }
        }

        [DataMember]
        public string PipelineId
        {
            get
            {
                return this.pipelineId;
            }
            set
            {
                this.pipelineId = value;
            }
        }

        [DataMember]
        public string JobArn
        {
            get
            {
                return this.jobArn;
            }
            set
            {
                this.jobArn = value;
            }
        }

        [DataMember]
        public string InputKey
        {
            get
            {
                return this.inputKey;
            }
            set
            {
                this.inputKey = value;
            }
        }

        [DataMember]
        public string OutputKey
        {
            get
            {
                return this.outputKey;
            }
            set
            {
                this.outputKey = value;
            }
        }

        [DataMember]
        public string PresetId
        {
            get
            {
                return this.presetId;
            }
            set
            {
                this.presetId = value;
            }
        }

        [DataMember]
        public string Status
        {
            get
            {
                return this.status;
            }
            set
            {
                this.status = value;
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
        public int VideoTubeId
        {
            get
            {
                return this.videoTubeId;
            }
            set
            {
                this.videoTubeId = value;
            }
        }

        [DataMember]
        public DateTime CreatedDateTime
        {
            get
            {
                return this.createdDateTime;
            }
            set
            {
                this.createdDateTime = value;
            }
        }

        [DataMember]
        public DateTime ModifiedDateTime
        {
            get
            {
                return this.modifiedDateTime;
            }
            set
            {
                this.modifiedDateTime = value;
            }
        }
    }
}
