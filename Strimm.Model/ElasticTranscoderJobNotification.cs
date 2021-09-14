using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.Model
{
    [DataContract]
    public class ElasticTranscoderJobNotification
    {
        private int elasticTranscoderJobNotificationId;
        private int elasticTranscoderJobId;
        private int messageId;
        private string content;
        private string status;
        private DateTime receivedDateTime;

        [DataMember]
        public int ElasticTranscoderJobNotificationId
        {
            get
            {
                return this.elasticTranscoderJobNotificationId;
            }
            set
            {
                this.elasticTranscoderJobNotificationId = value;
            }
        }

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
        public int MessageId
        {
            get
            {
                return this.messageId;
            }
            set
            {
                this.messageId = value;
            }
        }

        [DataMember]
        public string Content
        {
            get
            {
                return this.content;
            }
            set
            {
                this.content = value;
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
        public DateTime ReceivedDateTime
        {
            get
            {
                return this.receivedDateTime;
            }
            set
            {
                this.receivedDateTime = value;
            }
        }
    }
}
