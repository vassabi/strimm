using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using System.IO;
using System.Configuration;
using log4net;
using Strimm.Model;
using Strimm.Model.Aws;
using Strimm.Data.Repositories;
using StrimmBL.Processor;
using Strimm.Shared;

namespace StrimmBL
{
    public class AWSManage
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(AWSManage));

        private static string awsAccessKey;
        private static string awsSecretKey;
        private static string AmazonS3FileUrlDomain;
        private static string AmazonS3Bucket;
        private static string AmazonWebDistribDomain;
        private static string bucketName;
        private static string amazonS3WsDomain;

        static AWSManage()
        {
            try
            {
                awsAccessKey = ConfigurationManager.AppSettings["AmazonAccessKey"];
                awsSecretKey = ConfigurationManager.AppSettings["AmazonSecretKey"];
                AmazonS3FileUrlDomain = ConfigurationManager.AppSettings["AmazonS3FileUrlDomain"];
                AmazonS3Bucket = ConfigurationManager.AppSettings["AmazonS3Bucket"];
                AmazonWebDistribDomain = ConfigurationManager.AppSettings["AmazonWebDistribDomain"];
                bucketName = ConfigurationManager.AppSettings["ImagesBucket"];
                amazonS3WsDomain = ConfigurationManager.AppSettings["AmazonS3WsDomain"];
            }
            catch (Exception ex)
            {
                Logger.Error("Error occured while retrieving access key/secret key for AWS", ex);
            }
        }

        public static bool UploadAvatarToS3(string uploadAsFileName, Stream imgStream, string toWhichBucketName)
        {
            var filePremission = S3CannedACL.PublicRead;
            var storageType = S3StorageClass.Standard;
            bool isUploaded = false;

            try
            {
                using (IAmazonS3 client = Amazon.AWSClientFactory.CreateAmazonS3Client(awsAccessKey, awsSecretKey, RegionEndpoint.USEast1))
                {
                    var request = new PutObjectRequest()
                    {
                        CannedACL = filePremission,
                        StorageClass = storageType
                    };
                    request.InputStream = imgStream;
                    request.BucketName = toWhichBucketName;
                    request.Key = uploadAsFileName;
                    
                    client.PutObject(request);
                    isUploaded = true;
                }
            }
            catch (Amazon.S3.AmazonS3Exception s3ex)
            {
                Logger.Error(String.Format("Error occured while uploading avatar image '{0}' to S3, Status Code='{1}', S3 Message='{2}'", uploadAsFileName, s3ex.StatusCode, s3ex.Message), s3ex);
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Error occured while uploading avatar image to S3, filename = '{0}'", uploadAsFileName));
            }

            return isUploaded;
        }
        
        public static bool CheckIfBucketExists(string key, string bucketName)
        {
            bool exists = false;

            try
            {
                using (IAmazonS3 client = Amazon.AWSClientFactory.CreateAmazonS3Client(awsAccessKey, awsSecretKey, RegionEndpoint.USEast1))
                {
                    var response = client.GetObjectMetadata(new GetObjectMetadataRequest()
                    {
                        BucketName = bucketName,
                        Key = key
                    });

                    // TODO - Should this be really set based on data in the response or
                    // is this always true?
                    exists = true; 
                }
            }
            catch (Amazon.S3.AmazonS3Exception s3ex)
            {
                Logger.Error(String.Format("Error occured while checking if bucket exists using name '{0}' and key '{1}' in S3, Status Code='{2}', S3 Message='{3}'", bucketName, key, s3ex.StatusCode, s3ex.Message), s3ex);
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Error occured while checking if bucket exists using name '{0}' and key '{1}' in S3", bucketName, key), ex);
            }

            return exists;
        }

        public static List<string> GetFileListFromAWS(string bucketName, string folderPath)
        {
            var fileList = new List<string>();

            try
            {
                using (IAmazonS3 client = Amazon.AWSClientFactory.CreateAmazonS3Client(awsAccessKey, awsSecretKey, RegionEndpoint.USEast1))
                {
                    var lor = new ListObjectsRequest()
                    {
                        BucketName = bucketName,
                        Prefix = folderPath,
                        Delimiter = "/"
                    };
                    
                    var responce1 = client.ListObjects(lor);

                    responce1.S3Objects.ForEach(x => fileList.Add(x.Key));
                }
            }
            catch (Amazon.S3.AmazonS3Exception s3ex)
            {
                Logger.Error(String.Format("Error occured retrieving files for backet '{0}' and folder path '{1}' in S3, Status Code='{2}', S3 Message='{3}'", bucketName, folderPath, s3ex.StatusCode, s3ex.Message), s3ex);
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Error occured retrieving files for backet '{0}' and folder path '{1}' in S3", bucketName, folderPath), ex);
            }

            return fileList;
        }

        public static void DeletePrevImage(string objectlocation, string bucketname, string fileName)
        {
            try
            {
                var filesToDelete = GetFileListFromAWS(bucketname, objectlocation);

                if (filesToDelete != null && filesToDelete.Count > 0)
                {
                    using (IAmazonS3 client = Amazon.AWSClientFactory.CreateAmazonS3Client(awsAccessKey, awsSecretKey, RegionEndpoint.USEast1))
                    {
                        var multiple = new DeleteObjectsRequest()
                    {
                            BucketName = bucketname
                        };

                        filesToDelete.ForEach(x => multiple.AddKey(x));

                        client.DeleteObjects(multiple);

                        Logger.Debug(String.Format("Deleted files from Amazon S3 cloud that correspond to '{0}'", fileName));
                    }
                }
                else
                {
                    Logger.Debug(String.Format("File '{0}' does not exist in Amazon S3 cloud. Nothing to delete. Object location '{1}', Bucket name '{2}'", fileName, objectlocation, bucketname));
                }
            }
            catch (Amazon.S3.AmazonS3Exception s3ex)
            {
                Logger.Error(String.Format("Error occured deleteing PrevImage '{0}' from bucket '{1}', object location '{2}' in S3, Status Code='{3}', S3 Message='{4}'", fileName, bucketname, objectlocation, s3ex.StatusCode, s3ex.Message), s3ex);
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Error occured deleteing PrevImage '{0}' from bucket '{1}', object location '{2}' in S3", fileName, bucketname, objectlocation), ex);
            }
        }

        public static string UploadChannelImageToS3(string image, string fileName, int userId, int channelTubeId )
        {
            byte[] bytes = Convert.FromBase64String(image);
           
           if(String.IsNullOrEmpty(fileName))
            {
                fileName = RandomUtils.RandomString(5, true) + ".jpeg";
            }
            
            string s3fileName = String.Format("{0}/channel/{1}/{2}", userId, channelTubeId, fileName.Replace(' ', '_'));
            using (MemoryStream streamAvatr = new MemoryStream(bytes))
            {
                if (channelTubeId > 0)
                {
                    string originalImageS3Filename = String.Format("{0}/channel/{1}", userId, channelTubeId);
                    AWSManage.DeletePrevImage(originalImageS3Filename, bucketName, s3fileName);
                }

                AWSManage.UploadAvatarToS3(s3fileName, streamAvatr, bucketName);

                bool continueIfBucketDoesNotExistsCheck = !AWSManage.CheckIfBucketExists(s3fileName, bucketName);

                if (continueIfBucketDoesNotExistsCheck)
                {
                    DateTime start = DateTime.Now;
                    DateTime end = DateTime.Now;
                    bool hasTimedout = false;

                    while (continueIfBucketDoesNotExistsCheck)
                    {
                        end = DateTime.Now;
                        int elapsedTimeInSec = (end - start).Seconds;

                        if (elapsedTimeInSec < 10)
                        {
                            continueIfBucketDoesNotExistsCheck = !AWSManage.CheckIfBucketExists(s3fileName, bucketName);
                        }
                        else
                        {
                            continueIfBucketDoesNotExistsCheck = false;
                            hasTimedout = true;
                        }
                    }
                }
                return String.Format("/{0}/{1}", bucketName, s3fileName);
                //return String.Format("{0}/{1}", AmazonWebDistribDomain, s3fileName);
            }
        }

        public static string UploadPrivateVideoImageToS3(string image, int userId)
        {
            byte[] bytes = Convert.FromBase64String(image);
            string fileName = String.Empty;
            fileName = RandomUtils.RandomString(5, true) + ".jpeg";
           

            string s3fileName = String.Format("{0}/video/{1}", userId, fileName.Replace(' ', '_'));
            using (MemoryStream streamAvatr = new MemoryStream(bytes))
            {
                AWSManage.UploadAvatarToS3(s3fileName, streamAvatr, bucketName);

                bool continueIfBucketDoesNotExistsCheck = !AWSManage.CheckIfBucketExists(s3fileName, bucketName);

                if (continueIfBucketDoesNotExistsCheck)
                {
                    DateTime start = DateTime.Now;
                    DateTime end = DateTime.Now;
                    bool hasTimedout = false;

                    while (continueIfBucketDoesNotExistsCheck)
                    {
                        end = DateTime.Now;
                        int elapsedTimeInSec = (end - start).Seconds;

                        if (elapsedTimeInSec < 10)
                        {
                            continueIfBucketDoesNotExistsCheck = !AWSManage.CheckIfBucketExists(s3fileName, bucketName);
                        }
                        else
                        {
                            continueIfBucketDoesNotExistsCheck = false;
                            hasTimedout = true;
                        }
                    }
                }
                return String.Format("/{0}/{1}", bucketName, s3fileName);
                //return String.Format("{0}/{1}", AmazonWebDistribDomain, s3fileName);
            }
        }

        public static string UploadUserAvatarToS3(string image, string fileName, int userId)
        {
            byte[] bytes = Convert.FromBase64String(image);
          
            string s3fileName = String.Format("{0}/avatar/{1}", userId, fileName.Replace(' ', '_'));

            using (MemoryStream streamAvatr = new MemoryStream(bytes))
            {
                if (userId > 0)
                {
                    string originalImageS3Filename = String.Format("{0}/avatar", userId);
                    AWSManage.DeletePrevImage(originalImageS3Filename, bucketName, s3fileName);
                }

                AWSManage.UploadAvatarToS3(s3fileName, streamAvatr, bucketName);

                bool continueIfBucketDoesNotExistsCheck = !AWSManage.CheckIfBucketExists(s3fileName, bucketName);

                if (continueIfBucketDoesNotExistsCheck)
                {
                    DateTime start = DateTime.Now;
                    DateTime end = DateTime.Now;
                    bool hasTimedout = false;

                    while (continueIfBucketDoesNotExistsCheck)
                    {
                        end = DateTime.Now;
                        int elapsedTimeInSec = (end - start).Seconds;

                        if (elapsedTimeInSec < 10)
                        {
                            continueIfBucketDoesNotExistsCheck = !AWSManage.CheckIfBucketExists(s3fileName, bucketName);
                        }
                        else
                        {
                            continueIfBucketDoesNotExistsCheck = false;
                            hasTimedout = true;
                        }
                    }
                }
                return String.Format("/{0}/{1}", bucketName, s3fileName);
                //return String.Format("{0}/{1}", AmazonWebDistribDomain, s3fileName);
            }
        }

        public static string UploadUserBackGroundToS3(string image, string fileName, int userId)
        {
            byte[] bytes = Convert.FromBase64String(image);

            string s3fileName = String.Format("{0}/background/{1}", userId, fileName.Replace(' ', '_'));

            using (MemoryStream streamAvatr = new MemoryStream(bytes))
            {
                if (userId > 0)
                {
                    string originalImageS3Filename = String.Format("{0}/background", userId);
                    AWSManage.DeletePrevImage(originalImageS3Filename, bucketName, s3fileName);
                }

                AWSManage.UploadAvatarToS3(s3fileName, streamAvatr, bucketName);

                bool continueIfBucketDoesNotExistsCheck = !AWSManage.CheckIfBucketExists(s3fileName, bucketName);

                if (continueIfBucketDoesNotExistsCheck)
                {
                    DateTime start = DateTime.Now;
                    DateTime end = DateTime.Now;
                    bool hasTimedout = false;

                    while (continueIfBucketDoesNotExistsCheck)
                    {
                        end = DateTime.Now;
                        int elapsedTimeInSec = (end - start).Seconds;

                        if (elapsedTimeInSec < 10)
                        {
                            continueIfBucketDoesNotExistsCheck = !AWSManage.CheckIfBucketExists(s3fileName, bucketName);
                        }
                        else
                        {
                            continueIfBucketDoesNotExistsCheck = false;
                            hasTimedout = true;
                        }
                    }
                }
                return String.Format("/{0}/{1}", bucketName, s3fileName);
                //return String.Format("{0}/{1}", AmazonWebDistribDomain, s3fileName);
            }
        }

        public static bool CopyFile(string srcBucket, string srcKey, string destBucket, string destKey)
        {
            bool isSuccess = false;

            try
            {
                using (IAmazonS3 client = Amazon.AWSClientFactory.CreateAmazonS3Client(awsAccessKey, awsSecretKey, Amazon.RegionEndpoint.USEast1))
                {
                    client.CopyObject(new CopyObjectRequest()
                    {
                        DestinationBucket = destBucket,
                        DestinationKey = destKey,
                        SourceBucket = srcBucket,
                        SourceKey = srcKey
                    });
                }
            }
            catch (Amazon.S3.AmazonS3Exception s3ex)
            {
                Console.WriteLine(String.Format("Error occured while copying file - source key='{0}' and destination key='{1}'", srcKey, destKey), s3ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine(String.Format("Error occured while copying file - source key='{0}' and destination key='{1}'", srcKey, destKey), ex);
            }
            return isSuccess;
        }

        /// <summary>
        /// This method will read image stream and upload it to an existing S3 bucket on Amazon. It will also
        /// return URL of that image on S3
        /// </summary>
        /// <param name="uploadAsFileName">Name of the file being uploaded</param>
        /// <param name="imgStream">Input stream of the file being uploaded</param>
        /// <param name="toWhichBucketName">Target bucket name on S3</param>
        /// <param name="s3FileKey">URL of the uploaded file on S3</param>
        /// <returns>True/False</returns>
        public static bool UploadFileToS3(string uploadAsFileName, Stream imgStream, string toWhichBucketName, out string s3FileKey)
        {
            var filePremission = S3CannedACL.PublicRead;
            var storageType = S3StorageClass.Standard;
            bool isUploaded = false;

            try
            {
                using (IAmazonS3 client = Amazon.AWSClientFactory.CreateAmazonS3Client(awsAccessKey, awsSecretKey, Amazon.RegionEndpoint.USEast1))
                {
                    var request = new PutObjectRequest()
                    {
                        CannedACL = filePremission,
                        StorageClass = storageType,
                        AutoCloseStream = true
                    };
                    request.InputStream = imgStream;
                    request.BucketName = toWhichBucketName;
                    request.Key = uploadAsFileName;

                    client.PutObject(request);

                    var expiryUrlRequest = new GetPreSignedUrlRequest()
                    {
                        BucketName = toWhichBucketName,
                        Key = uploadAsFileName,
                        Expires = DateTime.Now.AddDays(1)
                    };

                    // MAX: At this point, I am deciding to just return the key to the file and 
                    // make it a responsibility of the caller to know what domain to use for the purpose
                    // of retrieving this file. Need to do it because:
                    // 1. Internally to amazon, file can be retrieved as "s3://<mybucket>/key
                    // 2. Externally to amozon, file can be retrieved as "https://cloudfront_domain/key"
                    // 3. Externally to amazon, flowplayer will need the following url "//cloudfront_domain_web_distribution/key" 
                    // 4. Externally to amazon, flash player would need the following url "//cloudfront_domain_rtmp_distribution/key"
                    // 5. Before transcoding completes, the following url is needed "//cloudfront_domain_staging_web_distribution/key"
                    s3FileKey = request.Key;

                    isUploaded = true;
                }
            }
            catch (Amazon.S3.AmazonS3Exception s3ex)
            {
                Console.WriteLine(String.Format("Error occured while uploading avatar image '{0}' to S3, Status Code='{1}', S3 Message='{2}'", uploadAsFileName, s3ex.StatusCode, s3ex.Message), s3ex);
                s3FileKey = String.Empty;
            }
            catch (Exception ex)
            {
                Console.WriteLine(String.Format("Error occured while uploading avatar image to S3, filename = '{0}'", uploadAsFileName));
                s3FileKey = String.Empty;
            }

            return isUploaded;
                    }


        public static void ProcessJobNotification(string cloudfrontDomain, AmazonSnsMessage message)
        {
            if (message == null)
            {
                return;
            }

            Logger.Debug(String.Format("Starting to process SNS message with id={0}, for job id={1}", message.MessageId, message.MessageDetails.jobId));

            var messageId = message.MessageId;
            var jobId = message.MessageDetails.jobId;
            var content = message.MessageText;
            var jobStatus = message.MessageDetails.state;
            var receivedDateTime = message.Timestamp;

            ElasticTranscoderJob existingJob = GetElasticTranscoderJobById(jobId);

            if (existingJob != null) 
            {
                //1. Persist received notification
                PersistElasticTranscoderJobNotification(existingJob.ElasticTranscoderJobId, messageId, content, jobStatus, receivedDateTime);

                using (var repository = new VideoTubeRepository())
                {
                    //2. Retrieve video record associted with the original job
                    var existingVideo = repository.GetVideoTubeById(existingJob.VideoTubeId);

                    //3. Update video status, comments and videokey
                    if (existingVideo != null)
                    {
                        if (message.MessageDetails.outputs != null && message.MessageDetails.outputs.Count() > 0) 
                        {
                            var firstOutput = message.MessageDetails.outputs[0];

                            existingVideo.VideoStatus = firstOutput.status;
                            existingVideo.VideoKey = String.Format("{0}/{1}", cloudfrontDomain, firstOutput.key);

                            if (firstOutput.status == "Progressing")
                            {
                                existingVideo.VideoStatusMessage = "Video is being transcoded.";
                            }
                            else if (firstOutput.status == "Complete")
                            {
                                existingVideo.VideoStatusMessage = "Video transcoding completed successfully";
                            }
                            else if (firstOutput.status == "Error")
                            {
                                existingVideo.VideoStatusMessage = message.MessageDetails.messageDetails;
                            }

                            repository.UpdateVideoTube(existingVideo);
                        }
                        else
                        {
                            Logger.Warn(String.Format("Failed to update existing video id={0}. Job id={1} did not have any outputs", existingVideo.VideoTubeId, existingJob.ElasticTranscoderJobId));
                        }
                    }
                    else
                    {
                        Logger.Warn(String.Format("Failed to retrieve video with id={0} associated with job id={1}", existingJob.VideoTubeId, existingJob.ElasticTranscoderJobId));
                    }
                }

                using (var repository = new ElasticTranscoderJobRepository())
                {
                    //4. Update existing job status
                    existingJob.Status = jobStatus;
                    repository.Update(existingJob);
                }
            }
            else {
                Logger.Warn(String.Format("Received message with id={0}, job id={1} does not correspond to an existing Strimm Transcoding Job. Aborting", messageId, jobId));
            }
        }

        private static void PersistElasticTranscoderJobNotification(int elasticTranscoderJobId, string messageId, string content, string jobStatus, DateTime receivedDateTime)
        {
            using (var repository = new ElasticTranscoderJobRepository())
            {
                if (repository.InsertElasticTranscoderJobNotification(elasticTranscoderJobId, messageId, content, jobStatus, receivedDateTime))
                {
                    Logger.Debug(String.Format("Successfully persisted job notification message id={0} for job id={1}, received on '{2}' with status '{3}'",
                        messageId, elasticTranscoderJobId, receivedDateTime, jobStatus));
                }
                else
                {
                    Logger.Warn(String.Format("Failed to persist job notification message id={0} for job id={1}, received on '{2}' with status '{3}'",
                        messageId, elasticTranscoderJobId, receivedDateTime, jobStatus));
                }
            }
        }

        public static ElasticTranscoderJob GetElasticTranscoderJobById(string jobId)
        {
            ElasticTranscoderJob existingJob = null;

            using (var repository = new ElasticTranscoderJobRepository())
            {
                existingJob = repository.GetJobByExternalId(jobId);
            }

            return existingJob;
        }

        public static string UploadChannelLogoToS3(string customLogoImgData, string fileName, int userId, int channelTubeId)
        {
            byte[] bytes = Convert.FromBase64String(customLogoImgData);

            if (String.IsNullOrEmpty(fileName))
            {
                fileName = RandomUtils.RandomString(5, true) + ".png";
            }

            string s3fileName = String.Format("{0}/channel/logo/{1}/{2}", userId, channelTubeId, fileName.Replace(' ', '_'));
            using (MemoryStream streamAvatr = new MemoryStream(bytes))
            {
                if (channelTubeId > 0)
                {
                    string originalImageS3Filename = String.Format("{0}/channel/logo/{1}", userId, channelTubeId);
                    AWSManage.DeletePrevImage(originalImageS3Filename, bucketName, s3fileName);
                }

                AWSManage.UploadAvatarToS3(s3fileName, streamAvatr, bucketName);

                bool continueIfBucketDoesNotExistsCheck = !AWSManage.CheckIfBucketExists(s3fileName, bucketName);

                if (continueIfBucketDoesNotExistsCheck)
                {
                    DateTime start = DateTime.Now;
                    DateTime end = DateTime.Now;
                    bool hasTimedout = false;

                    while (continueIfBucketDoesNotExistsCheck)
                    {
                        end = DateTime.Now;
                        int elapsedTimeInSec = (end - start).Seconds;

                        if (elapsedTimeInSec < 10)
                        {
                            continueIfBucketDoesNotExistsCheck = !AWSManage.CheckIfBucketExists(s3fileName, bucketName);
                        }
                        else
                        {
                            continueIfBucketDoesNotExistsCheck = false;
                            hasTimedout = true;
                        }
                    }
                }
                return String.Format("{0}/{1}/{2}", amazonS3WsDomain, bucketName, s3fileName);
                //return String.Format("/{0}/{1}", bucketName, s3fileName);
                //return String.Format("{0}/{1}", AmazonWebDistribDomain, s3fileName);
            }
        }
    }
}
