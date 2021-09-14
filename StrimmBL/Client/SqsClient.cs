using Amazon.Runtime;
using Amazon.SQS;
using Amazon.SQS.Model;
using Amazon.SQS.Util;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StrimmBL.Client
{
    public class SqsClient
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(SqsClient));

        private AmazonSQSClient _sqsClient;

        public string QueueName { get; private set; }

        internal string QueueUrl { get; private set; }

        internal string QueueArn { get; private set; }

        private Action<Message> _receiveAction;
        private CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();

        public SqsClient(string awsAccessKey, string awsSecretKey, string queueName)
        {
            _sqsClient = new AmazonSQSClient(new BasicAWSCredentials(awsAccessKey, awsSecretKey), Amazon.RegionEndpoint.USEast1);

            QueueName = queueName;
            Ensure();
        }

        public void Ensure()
        {
            if (!Exists())
            {
                var request = new CreateQueueRequest();
                request.QueueName = QueueName;
                var response = _sqsClient.CreateQueue(request);
                QueueUrl = response.QueueUrl;
            }
        }

        public bool Exists()
        {
            var exists = false;
            var queues = _sqsClient.ListQueues(new ListQueuesRequest());
            var matchString = string.Format("/{0}", QueueName);
            var matches = queues.QueueUrls.Where(x => x.EndsWith(QueueName));
            if (matches.Count() == 1)
            {
                exists = true;
                QueueUrl = matches.ElementAt(0);
            }

            return exists;
        }

        public void Unsubscribe()
        {
            _cancellationTokenSource.Cancel();
        }

        private async void Subscribe()
        {
            if (!_cancellationTokenSource.IsCancellationRequested)
            {
                var request = new ReceiveMessageRequest { MaxNumberOfMessages = 10 };
                request.QueueUrl = QueueUrl;
                // MAX: Trust Error is thrown in the line below. Need to handle it. Look at the inner exception
                // may need to try to manage retry tokens
                var result = await _sqsClient.ReceiveMessageAsync(request, _cancellationTokenSource.Token);
                if (result.Messages.Count > 0)
                {
                    foreach (var message in result.Messages)
                    {
                        if (_receiveAction != null && message != null)
                        {
                            _receiveAction(message);
                            DeleteMessage(message.ReceiptHandle);
                        }
                    }
                }
            }
            if (!_cancellationTokenSource.IsCancellationRequested)
            {
                Subscribe();
            }
        }

        private DeleteMessageResponse DeleteMessage(string receiptHandle)
        {
            var request = new DeleteMessageRequest();
            request.QueueUrl = QueueUrl;
            request.ReceiptHandle = receiptHandle;
            return _sqsClient.DeleteMessage(request);
        }

        public void Subscribe(Action<Message> receiveAction)
        {
            _receiveAction = receiveAction;
            _cancellationTokenSource = new CancellationTokenSource();

            try
            {
                Subscribe();
            }
            catch (Amazon.SQS.AmazonSQSException sqsex)
            {
                Logger.Error("Error occured while subscribing to SQS queue for all Elastic Jobs notifications", sqsex);
                Subscribe(_receiveAction);
            }
            catch (Exception ex)
            {
                Logger.Error("Error occured while subscribing to SQS queue for all Elastic Jobs notifications", ex);
                Subscribe(_receiveAction);
            }
        }

        public void Send(Message message)
        {
            var request = new SendMessageRequest();
            request.QueueUrl = QueueUrl;
            request.MessageBody = message.Body;
            _sqsClient.SendMessage(request);
        }

        public bool HasMessages()
        {
            var request = new GetQueueAttributesRequest
            {
                QueueUrl = QueueUrl,
                AttributeNames = new List<string>(new string[] { SQSConstants.ATTRIBUTE_APPROXIMATE_NUMBER_OF_MESSAGES })
            };
            var response = _sqsClient.GetQueueAttributes(request);
            return response.ApproximateNumberOfMessages > 0;
        }

        public bool IsListening()
        {
            return !_cancellationTokenSource.IsCancellationRequested;
        }
    }
}

