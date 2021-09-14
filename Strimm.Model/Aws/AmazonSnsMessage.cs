using Amazon.SimpleNotificationService.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Strimm.Shared.Extensions;

namespace Strimm.Model.Aws
{
    public class AmazonSnsMessage
    {
        public AmazonSnsMessage(Message snsMessage)
        {
            if (snsMessage != null)
            {
                this.IsNotificationType = snsMessage.IsNotificationType;
                this.IsSubscriptionType = snsMessage.IsSubscriptionType;
                this.IsUnsubscriptionType = snsMessage.IsUnsubscriptionType;
                this.MessageId = snsMessage.MessageId;
                this.MessageText = snsMessage.MessageText;
                this.Signature = snsMessage.Signature;
                this.SignatureVersion = snsMessage.SignatureVersion;
                this.SigningCertURL = snsMessage.SigningCertURL;
                this.Subject = snsMessage.Subject;
                this.SubscribeURL = snsMessage.SubscribeURL;
                this.Timestamp = snsMessage.Timestamp;
                this.Token = snsMessage.Token;
                this.TopicArn = snsMessage.TopicArn;
                this.UnsubscribeURL = snsMessage.UnsubscribeURL;
                this.MessageDetails = snsMessage.DeserializeMessageDetails<AmazonSnsMessageDetails>();
            }
        }

        public bool IsNotificationType { get; private set; }
        public bool IsSubscriptionType { get; private set; }
        public bool IsUnsubscriptionType { get; private set; }
        public string MessageId { get; private set; }
        public string MessageText { get; private set; }
        public string Signature { get; private set; }
        public string SignatureVersion { get; private set; }
        public string SigningCertURL { get; private set; }
        public string Subject { get; private set; }
        public string SubscribeURL { get; private set; }
        public DateTime Timestamp { get; private set; }
        public string Token { get; private set; }
        public string TopicArn { get; private set; }
        public string Type { get; private set; }
        public string UnsubscribeURL { get; private set; }
        public AmazonSnsMessageDetails MessageDetails { get; private set; }
    }
}
