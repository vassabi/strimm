using log4net;
using Quartz;
using Strimm.Jobs.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.SQS;
using Amazon.SQS.Model;
using StrimmBL.Client;
using Strimm.Model.Aws;
using StrimmBL;
using System.Configuration;

namespace Strimm.Jobs.ElasticTranscoderJobNotificationsListener
{
    [Export(typeof(IStrimmJob))]
    public class ElasticTranscoderJobNotificationsListenerJob : BaseJob
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ElasticTranscoderJobNotificationsListenerJob));

        private static string SQS_ARN;
        private static string SQS_NAME;
        private static string AWS_ACCESS_KEY;
        private static string AWS_SECRET_KEY;
        private static string AWS_CLOUD_FRONT_DOMAIN;

        public ElasticTranscoderJobNotificationsListenerJob()
            : base(typeof(ElasticTranscoderJobNotificationsListenerJob).Name)
        {
            SQS_ARN = this.JobAppSettings.Settings["SQS_ARN"].Value.ToString();
            SQS_NAME = this.JobAppSettings.Settings["SQS_NAME"].Value.ToString();
            AWS_ACCESS_KEY = this.JobAppSettings.Settings["AwsAccessKey"].Value.ToString();
            AWS_SECRET_KEY = this.JobAppSettings.Settings["AwsSecretKey"].Value.ToString();
            AWS_CLOUD_FRONT_DOMAIN = ConfigurationManager.AppSettings["AmazonCloudFrontDomain"];
        }

        public override void Execute(IJobExecutionContext context)
        {
            Logger.Debug(String.Format("Exectuing 'JobsNotificationsListenerJob' job at {0}", DateTime.Now.ToShortTimeString()));

            try
            {
                var sqsClient = new SqsClient(AWS_ACCESS_KEY, AWS_SECRET_KEY, SQS_NAME);
                sqsClient.Subscribe((sqsMessage) =>
                {
                    if (sqsMessage != null)
                    {
                        Amazon.SimpleNotificationService.Util.Message msg = Amazon.SimpleNotificationService.Util.Message.ParseMessage(sqsMessage.Body);
                        AmazonSnsMessage message = new AmazonSnsMessage(msg);

                        AWSManage.ProcessJobNotification(AWS_CLOUD_FRONT_DOMAIN, message);
                    }
                });
            }
            catch (Amazon.S3.AmazonS3Exception s3ex)
            {
                Logger.Error(String.Format("Error occurred while subscribing to topic {0}", SQS_ARN), s3ex);
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Error occurred while subscribing to topic {0}", SQS_ARN), ex);
            }
        }
    }
}
