using Amazon.ElasticTranscoder.Model;
using log4net;
using Strimm.Data.Repositories;
using Strimm.Model.Projections;
using Strimm.Model.WebModel;
using StrimmBL.Exeptions;
using StrimmBL.Processor;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StrimmBL
{
    public class VideoUploadManager
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(VideoUploadManager));

        private static float VIDEO_PREVIEW_CLIP_DURATION_IN_SEC;
        private static int VIDEO_NUMBER_OF_DEFAULT_THUMBNAILS_TO_GEN;
        private static string VIDEO_THUMBNAIL_LOCATION_SEQUENCE;
        private static float VIDEO_THUMBNAIL_START_AND_END_OFFSET;
        private static string AMAZON_S3_USERS_VIDEO_FOLDER;
        private static string S3_BUCKET;
        private static string S3_INPUT_VIDEO_BUCKET;
        private static string S3_VIDEO_USER_FOLDER;
        private static string S3_DOMAIN;
        private static string AWS_CLOUD_FRONT_DOMAIN;

        static VideoUploadManager() {
            try {
                if (!float.TryParse(ConfigurationManager.AppSettings["VideoPreviewClipDurationInSec"], out VIDEO_PREVIEW_CLIP_DURATION_IN_SEC)) {
                    VIDEO_PREVIEW_CLIP_DURATION_IN_SEC = 30;
                }

                if (!Int32.TryParse(ConfigurationManager.AppSettings["VideoNumberOfDefaultThumbnailsToGenerate"], out VIDEO_NUMBER_OF_DEFAULT_THUMBNAILS_TO_GEN))
                {
                    VIDEO_NUMBER_OF_DEFAULT_THUMBNAILS_TO_GEN = 3;
                }

                VIDEO_THUMBNAIL_LOCATION_SEQUENCE   = ConfigurationManager.AppSettings["VideoThumbnailLocationSequence"];
                AMAZON_S3_USERS_VIDEO_FOLDER        = ConfigurationManager.AppSettings["AmazonS3UsersVideoFolder"];
                S3_BUCKET                           = ConfigurationManager.AppSettings["ImagesBucket"];
                S3_INPUT_VIDEO_BUCKET               = ConfigurationManager.AppSettings["AmazonS3VideoInputBucket"];
                S3_VIDEO_USER_FOLDER                = ConfigurationManager.AppSettings["AmazonS3VideoUserFolder"];
                S3_DOMAIN                           = ConfigurationManager.AppSettings["AmazonS3FileUrlDomain"];
                AWS_CLOUD_FRONT_DOMAIN              = ConfigurationManager.AppSettings["videoThumbnailPathOnS3"];

                if (!float.TryParse(ConfigurationManager.AppSettings["VideoThumbnailStartEndOffset"], out VIDEO_THUMBNAIL_START_AND_END_OFFSET))
                {
                    VIDEO_THUMBNAIL_START_AND_END_OFFSET = 10;
                }
            }
            catch (Exception ex) {
                Logger.Error("Error occured while retrieving configuration parameters", ex);
            }
        }

        /// <summary>
        /// This method will retrieve video duration of the specified video file
        /// </summary>
        /// <param name="videoFilePath">Local path or URI for a video file</param>
        /// <returns>Video duration</returns>
        public static float GetVideoDuration(string videoFilePath)
        {
            Logger.Info(String.Format("Retrieving video duration for video file: {0}", videoFilePath));

            float duration = 0;

            using (var processor = new VideoProcessor())
            {
                if (Uri.IsWellFormedUriString(videoFilePath, UriKind.Absolute))
                {
                    string vDur = processor.GetVideoDuration(videoFilePath);

                    Logger.Debug(String.Format("Video duration for '{0}' is {1}", videoFilePath, vDur));

                    DateTime date = DateTime.Parse(vDur);

                    int hrs = date.Hour;
                    int min = date.Minute;
                    int sec = date.Second;
                    int ms = date.Millisecond;

                    duration = (hrs * 60 * 60) + (min * 60) + sec + (float)ms / (float)1000;
                }
                else
                {
                    throw new VideoUploadManagerException("Unable to determine video duration. Invalid video file URI specified.");
                }
            }

            return duration;
        }

        /// <summary>
        /// This method will retrieve video thumbnails of an existing video record
        /// </summary>
        /// <param name="videoTubeId">Id of an existing video record</param>
        /// <returns>Collection of thumbnail paths uploaded to AWS</returns>
        public static List<string> GetVideoThumbnails(int videoTubeId)
        {
            Logger.Info(String.Format("Retrieving multiple video thumbnails for video with id={0}", videoTubeId));

            List<string> videoThumbnails = new List<string>();

            var existingVideo = VideoTubeManage.GetVideoTubePoById(videoTubeId);

            if (existingVideo != null && existingVideo.VideoTubeId > 0)
            {
                using (var processor = new VideoProcessor())
                {
                    string videoKeyUrl = String.Format("https:{0}", existingVideo.VideoKey);

                    if (Uri.IsWellFormedUriString(videoKeyUrl, UriKind.Absolute))
                    {
                        var timeOffsets = GetThumbnailImageTimeframeOffsets(existingVideo.Duration);
                        var filename = processor.GetFilenameWithoutPath(videoKeyUrl);

                        float thumbnailOffset = 0;

                        timeOffsets.ForEach(x =>
                        {
                            thumbnailOffset += x;

                            //"https://s3.amazonaws.com/tubestrimmtest/2104/videos/1440707227890-short_video20.mp4"
                            //var imageStream = processor.GetVideoTumbnail("https://d1lcpacmhg64nn.cloudfront.net/2104/videos/1440707227890-short_video20.mp4", thumbnailOffset);
                            var imageStream = processor.GetVideoTumbnail(videoKeyUrl, thumbnailOffset);

                            if (imageStream != null && imageStream.Length > 0)
                            {
                                var awsS3Key = String.Format("{0}/{1}/{2}-{3}-{4}.jpeg", existingVideo.OwnerUserId, AMAZON_S3_USERS_VIDEO_FOLDER, DateTime.Now.ToFileTimeUtc(), filename, thumbnailOffset);

                                string videoThumbnailPathOnS3 = String.Empty;

                                AWSManage.UploadFileToS3(awsS3Key, imageStream, S3_BUCKET, out videoThumbnailPathOnS3);

                                videoThumbnails.Add(videoThumbnailPathOnS3);
                            }
                        });
                    }
                    else
                    {
                        throw new VideoUploadManagerException("Unable to retrieve video thumbnails. Invalid video file URI specified.");
                    }
                }
            }

            return videoThumbnails;
        }

        /// <summary>
        /// This method will retrieve multiple thumbnail images from the
        /// video file provided based on the configuration for a user
        /// </summary>
        /// <param name="userId">User id</param>
        /// <param name="videoFilePath">File path or URI of a video file</param>
        /// <returns>Collection of video thumbnail paths</returns>
        public static List<string> GetVideoThumbnails(int userId, string videoFilePath)
        {
            Logger.Info(String.Format("Retrieving multiple video thumbnails for user id={0} and file '{1}'", userId, videoFilePath));

            List<string> videoThumbnails = new List<string>();

            using (var processor = new VideoProcessor())
            {
                if (Uri.IsWellFormedUriString(videoFilePath, UriKind.Absolute))
                {
                    string durationInSec = processor.GetVideoDuration(videoFilePath);

                    DateTime date = DateTime.Parse(durationInSec);

                    int hrs = date.Hour;
                    int min = date.Minute;
                    int sec = date.Second;
                    int ms = date.Millisecond;

                    float duration = (hrs * 60 * 60) + (min * 60) + sec + (float)ms / (float)1000;

                    var timeOffsets = GetThumbnailImageTimeframeOffsets(duration);
                    var filename = processor.GetFilenameWithoutPath(videoFilePath);

                    float thumbnailOffset = 0;

                    timeOffsets.ForEach(x =>
                    {
                        thumbnailOffset += x;

                        var imageStream = processor.GetVideoTumbnail(videoFilePath, thumbnailOffset);

                        var awsS3Key = String.Format("{0}/{1}/{2}-{3}-{4}.jpeg", userId, AMAZON_S3_USERS_VIDEO_FOLDER, DateTime.Now.ToFileTimeUtc(), filename, thumbnailOffset);

                        string videoThumbnailPathOnS3 = String.Empty;

                        AWSManage.UploadFileToS3(awsS3Key, imageStream, S3_BUCKET, out videoThumbnailPathOnS3);

                        videoThumbnails.Add(videoThumbnailPathOnS3);
                    });
                }
                else
                {
                    throw new VideoUploadManagerException("Unable to retrieve video thumbnails. Invalid video file URI specified.");
                }
            }

            return videoThumbnails;
        }

        /// <summary>
        /// This method will retrieve a single thumbnail image for a user from
        /// a specified video vile and at specified time frame in seconds
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <param name="videoFilePath">File path or URI of a video file</param>
        /// <param name="timeFrameInSec">Time offset from the start of a video where thumbnail should be generated</param>
        /// <returns>Path or URI of a new thumbnail file</returns>
        public static string GetVideoThumbnail(int userId, string videoFilePath, float timeFrameInSec)
        {
            Logger.Info(String.Format("Retrieving video thumbnail for video file {0} at {1} sec", videoFilePath, timeFrameInSec));

            string videoThumbnailPathOnS3 = null;
            string awsS3Key = null;

            using (var processor = new VideoProcessor())
            {
                if (Uri.IsWellFormedUriString(videoFilePath, UriKind.Absolute))
                {
                    var duration = processor.GetVideoDuration(videoFilePath);

                    float vDuration = 0;
                    float.TryParse(duration, out vDuration);

                    if (vDuration >= timeFrameInSec)
                    {
                        var filename = processor.GetFilenameWithoutPath(videoFilePath);
                        var imageStream = processor.GetVideoTumbnail(videoFilePath, timeFrameInSec);

                        awsS3Key = String.Format("{0}/{1}/{2}-{3}.jpeg", userId, AMAZON_S3_USERS_VIDEO_FOLDER, DateTime.Now.ToFileTimeUtc(), filename);

                        AWSManage.UploadFileToS3(awsS3Key, imageStream, S3_BUCKET, out videoThumbnailPathOnS3);
                    }
                    else
                    {
                        throw new VideoUploadManagerException("Failed to retrieve video thumbnail at specified time frame. Video duration exceeded.");
                    }
                }
                else
                {
                    throw new VideoUploadManagerException("Unable to retrieve video thumbnail. Invalid video file URI specified.");
                }
            }

            return videoThumbnailPathOnS3;
        }

        public static string GetVideoThumbnail(int videoTubeId, float timeFrameInSec)
        {
            Logger.Info(String.Format("Retrieving video thumbnail for video id={0} at {1} sec", videoTubeId, timeFrameInSec));

            string videoThumbnailPathOnS3 = null;

            var existingVideo = VideoTubeManage.GetVideoTubePoById(videoTubeId);

            if (existingVideo != null)
            {
                if (existingVideo.Duration > timeFrameInSec)
                {
                    string awsS3Key = null;

                    using (var processor = new VideoProcessor())
                    {
                        string videoKeyUrl = String.Format("https:{0}", existingVideo.VideoKey);

                        if (Uri.IsWellFormedUriString(videoKeyUrl, UriKind.Absolute))
                        {
                            var filename = processor.GetFilenameWithoutPath(videoKeyUrl);
                            var imageStream = processor.GetVideoTumbnail(videoKeyUrl, timeFrameInSec);

                            if (imageStream != null && imageStream.Length > 0)
                            {
                                awsS3Key = String.Format("{0}/{1}/{2}-{3}.jpeg", existingVideo.OwnerUserId, AMAZON_S3_USERS_VIDEO_FOLDER, DateTime.Now.ToFileTimeUtc(), filename);

                                AWSManage.UploadFileToS3(awsS3Key, imageStream, S3_BUCKET, out videoThumbnailPathOnS3);
                            }
                        }
                        else
                        {
                            throw new VideoUploadManagerException("Unable to retrieve video thumbnail. Invalid video file URI specified.");
                        }
                    }
                }
                else
                {
                    throw new VideoUploadManagerException("Video duration is less than the time frame specified. Custom thumbnail was not extracted");
                }
            }
            else
            {
                throw new VideoUploadManagerException("Invalid video specified. Custom thumbnail image cannot be extracted.");
            }

            return videoThumbnailPathOnS3;
        }

        /// <summary>
        /// This method will retrieve video preview clip from an existing video identified by
        /// its record id
        /// </summary>
        /// <param name="videoTubeId">Id of an existing video record</param>
        /// <returns>AWS path to extracted video preview clip</returns>
        public static string GetVideoPreviewClip(int videoTubeId)
        {
            Logger.Info(String.Format("Retrieving video clip for video with id={0}", videoTubeId));

            string videoClipPath = String.Empty;
            string awsS3Key = null;

            var existingVideo = VideoTubeManage.GetVideoTubePoById(videoTubeId);

            if (existingVideo != null)
            {
                using (var processor = new VideoProcessor())
                {
                    string videoClipUrl = String.Format("http:{0}", existingVideo.VideoKey);

                    if (Uri.IsWellFormedUriString(videoClipUrl, UriKind.Absolute))
                    {
                        var filename = processor.GetFilenameWithoutPath(videoClipUrl);

                        if (existingVideo.Duration >= VIDEO_PREVIEW_CLIP_DURATION_IN_SEC)
                        {
                            var videoStream = processor.GetVideoPreviewClip(videoClipUrl, VIDEO_PREVIEW_CLIP_DURATION_IN_SEC);

                            awsS3Key = String.Format("{0}/{1}/{2}-pc.mp4", existingVideo.OwnerUserId, AMAZON_S3_USERS_VIDEO_FOLDER, filename);

                            AWSManage.UploadFileToS3(awsS3Key, videoStream, S3_BUCKET, out videoClipPath);
                        }
                        else
                        {
                            var domainAndBucketUrl = String.Format("{0}{1}", S3_DOMAIN, S3_INPUT_VIDEO_BUCKET);
                            var inputVideoFileKey = existingVideo.VideoKey.Substring(domainAndBucketUrl.Length + 3);
                            
                            string srcBucket = S3_INPUT_VIDEO_BUCKET;
                            string srcKey = inputVideoFileKey;
                            string destKey = String.Format("{0}/{1}/{2}-pc.mp4", existingVideo.OwnerUserId, S3_VIDEO_USER_FOLDER, filename);

                            AWSManage.CopyFile(srcBucket, srcKey, S3_BUCKET, destKey);

                            videoClipPath = destKey;

                            Logger.Warn(String.Format("Video preview clip cannot be extracted for video id={0}. Video duration is less than video preview duration", videoTubeId));
                        }
                    }
                    else
                    {
                        throw new VideoUploadManagerException("Unable to retrieve video preview clip. Invalid video file URI specified.");
                    }
                }
            }
            else
            {
                throw new VideoUploadManagerException("Invalid video specified. Video preview clip cannot be extracted.");
            }
            
            return videoClipPath;
        }

        /// <summary>
        /// This method will generate a preview clip of a certain duration from
        /// a video file specified by the caller
        /// </summary>
        /// <param name="userId">User Id</param>
        /// <param name="videoFilePath">File path or URI of a video file</param>
        /// <returns>Path or URI of a preview clip</returns>
        public static string GetVideoPreviewClip(int userId, string videoFilePath)
        {
            Logger.Info(String.Format("Retrieving video preview clip for video file {0}", videoFilePath));

            string videoPriviewClipPathOnS3 = null;
            string awsS3Key = null;

            using (var processor = new VideoProcessor())
            {
                if (Uri.IsWellFormedUriString(videoFilePath, UriKind.Absolute))
                {
                    var duration = processor.GetVideoDuration(videoFilePath);

                    float vDuration = 0;
                    float.TryParse(duration, out vDuration);

                    if (vDuration >= VIDEO_PREVIEW_CLIP_DURATION_IN_SEC)
                    {
                        var filename = processor.GetFilenameWithoutPath(videoFilePath);
                        var videoStream = processor.GetVideoPreviewClip(videoFilePath, VIDEO_PREVIEW_CLIP_DURATION_IN_SEC);

                        awsS3Key = String.Format("{0}/{1}/{2}-{3}.mp4", userId, AMAZON_S3_USERS_VIDEO_FOLDER, DateTime.Now.ToFileTimeUtc(), filename);

                        AWSManage.UploadFileToS3(awsS3Key, videoStream, S3_BUCKET, out videoPriviewClipPathOnS3);
                    }
                    else
                    {
                        videoPriviewClipPathOnS3 = videoFilePath;
                    }
                }
                else
                {
                    throw new VideoUploadManagerException("Unable to retrieve video preview clip. Invalid video file URI specified.");
                }
            }

            return videoPriviewClipPathOnS3;
        }

        /// <summary>
        /// This method will submit existing video for transcoding to AWS or if the AWS job pipeline is
        /// full it will add it to the internal video transcoding job queue for further processing
        /// </summary>
        /// <param name="videoTubeId">Existing video's id</param>
        public static void SubmitVideoForTranscoding(int videoTubeId)
        {
            Logger.Info(String.Format("Submitting video id='{0}' for transcoding", videoTubeId));

            var transcoder = new ElasticTranscoderProcessor();
            bool hasSubmitted = false;

            VideoTubePo existingVideoRecord = null;

            using (var repository = new VideoTubeRepository())
            {
                existingVideoRecord = repository.GetVideoTubePoById(videoTubeId);
            }

            if (existingVideoRecord != null)
            {
                var pipelines = transcoder.GetAllPipelines();

                if (pipelines != null && pipelines.Count > 0)
                {
                    pipelines.Where(x => x.InputBucket == S3_INPUT_VIDEO_BUCKET).ToList().ForEach(x =>
                    {
                        var jobs = transcoder.GetAllPipelineJobsByPipelineId(x.Id);

                        if (jobs != null && jobs.Where(y => y.Status == "Processing").ToList().Count < 100000)
                        {
                            if (!hasSubmitted)
                            {
                                hasSubmitted = true;

                                var domainAndBucketUrl = String.Format("{0}{1}", S3_DOMAIN, S3_INPUT_VIDEO_BUCKET);
                                var inputVideoFileKey = existingVideoRecord.VideoKey.Substring(domainAndBucketUrl.Length + 3);
                                var filename = inputVideoFileKey.Substring(inputVideoFileKey.LastIndexOf('/') + 1);
                                var outputVideoFileKey = String.Format("{0}/{1}/{2}", existingVideoRecord.OwnerUserId, S3_VIDEO_USER_FOLDER, filename);

                                var job = transcoder.CreateJob(x.Id, inputVideoFileKey, outputVideoFileKey);

                                if (job != null && !String.IsNullOrEmpty(job.Id))
                                {
                                    using (var repository = new ElasticTranscoderJobRepository())
                                    {
                                        var transcoderJob = repository.Insert(existingVideoRecord.OwnerUserId, x.Id, job.Id, job.Arn, job.Input.Key, job.Output.Key, job.Status, job.Outputs[0].PresetId, videoTubeId);
                                        Logger.Debug(String.Format("Elastic job with id={0} in pipeline={1} was successfully added to db for tracking", job.Id, x.Name));
                                    }
                                }
                                else
                                {
                                    Logger.Error(String.Format("Failed to create created a new job for vide file '{0}'", existingVideoRecord.VideoKey));
                                    throw new VideoUploadManagerException("Failed to submit video to be transcoded.");
                                }
                            }
                        }
                        else
                        {
                            using (var repository = new ElasticTranscoderJobRepository())
                            {
                                if (repository.AddVideoToVideoTranscodingQueue(existingVideoRecord.OwnerUserId, videoTubeId))
                                {
                                    Logger.Debug(String.Format("Video with id='{0}' was stored in internal queue for further processing per request of user with id={1}.", videoTubeId, existingVideoRecord.OwnerUserId));
                                }
                            }
                        }
                    });
                }
            }
            else
            {
                throw new VideoUploadManagerException("Invalid video specified. Video was submitted for transcoding.");
            }
        }

        /// <summary>
        /// This method will generate thumbnail offset sequence needed to generate 
        /// a certain number of thumbnails for a video
        /// </summary>
        /// <param name="videoDuration">Video duration of a video file</param>
        /// <returns>Thumbnail extraction time frame sequence</returns>
        private static List<float> GetThumbnailImageTimeframeOffsets(float videoDuration)
        {
            var offsets = new List<float>();

            float firstThumbnailOffset = 0;
            float secondThumbnailOffset = 0;
            float thirdThumbnailOffset = 0;

            try
            {
                var parts = VIDEO_THUMBNAIL_LOCATION_SEQUENCE.Split('|');

                if (parts != null)
                {
                    if (parts.Count() == 3)
                    {
                        Logger.Debug("Setting 3 thumbnail image time offsets per configuration");

                        if (!float.TryParse(parts[0], out firstThumbnailOffset))
                        {
                            firstThumbnailOffset = VIDEO_THUMBNAIL_START_AND_END_OFFSET;
                        }

                        if (!float.TryParse(parts[1], out secondThumbnailOffset))
                        {
                            secondThumbnailOffset = videoDuration / 2;
                        }

                        if (!float.TryParse(parts[2], out thirdThumbnailOffset))
                        {
                            thirdThumbnailOffset = videoDuration - VIDEO_THUMBNAIL_START_AND_END_OFFSET;
                        }

                        var totaltimeframe = firstThumbnailOffset + secondThumbnailOffset + thirdThumbnailOffset;

                        if (totaltimeframe > videoDuration)
                        {
                            firstThumbnailOffset = videoDuration / 4;
                            secondThumbnailOffset = videoDuration / 2;
                            thirdThumbnailOffset = firstThumbnailOffset + secondThumbnailOffset;
                        }

                        offsets.Add(firstThumbnailOffset);
                        offsets.Add(secondThumbnailOffset);
                        offsets.Add(thirdThumbnailOffset);
                    }
                    else if (parts.Count() == 1)
                    {
                        float offset = 0;
                        if (!float.TryParse(parts[0], out offset))
                        {
                            offset = 10;
                        }

                        Logger.DebugFormat("Generating {0} thumbnail time offsets at {1} sec increments", VIDEO_NUMBER_OF_DEFAULT_THUMBNAILS_TO_GEN, offset);

                        for (int i = 0; i < VIDEO_NUMBER_OF_DEFAULT_THUMBNAILS_TO_GEN; i++)
                        {
                            offsets.Add(offset * (i + 1));
                        }
                    }
                    else {
                        Logger.Warn("Failed to retrieve video thumbnail offset sequence instructions. Setting default thumbnail offset at 10 sec");
                        offsets.Add(10);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error occurred while defining video thumbnail offset sequence", ex);
            }

            return offsets;
        }
    }
}
