using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Model
{
    [DataContract]
    public class VideoTranscodingJobQueueItem
    {
        private int videoTranscodingJobQueueItemId;
        private int videoTubeId;
        private int userId;
        private string stagingVideoKey;
        private bool isProcessed;
        private DateTime addedDateTime;

        public int VideoTranscodingJobQueueItemId
        {
            get
            {
                return this.videoTranscodingJobQueueItemId;
            }
            set
            {
                this.videoTranscodingJobQueueItemId = value;
            }
        }

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

        public String StagingVideoKey
        {
            get
            {
                return this.stagingVideoKey;
            }
            set
            {
                this.stagingVideoKey = value;
            }
        }

        public bool IssProcessed
        {
            get
            {
                return this.isProcessed;
            }
            set
            {
                this.isProcessed = value;
            }
        }

        public DateTime AddedDateTime
        {
            get
            {
                return this.addedDateTime;
            }
            set
            {
                this.addedDateTime = value;
            }
        }
    }
}
