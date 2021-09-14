using log4net;
using Strimm.Model.Projections;
using Strimm.Model.WebModel;
using Strimm.Shared;
using StrimmBL;
using StrimmBL.Exeptions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Services;

namespace StrimmTube.WebServices
{
    /// <summary>
    /// Summary description for VideoUploadWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class VideoUploadWebService : System.Web.Services.WebService
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(VideoUploadWebService));

        private static readonly string AWS_CLOUDFRONT_ORIGIN;

        static VideoUploadWebService()
        {
            AWS_CLOUDFRONT_ORIGIN = ConfigurationManager.AppSettings["AmazonCloudFrontDomain"];
        }

        [WebMethod]
        public ResponseMessageModel InitializeCustomVideoTubeUploadForUser(int userId, string filename, string videoTubeStagingKey, string clientDateTime)
        {
            var response = new ResponseMessageModel();

            DateTime clientTime;
            float duration;
            CustomVideoTubeUploadModel videoTube;
            try
            {
                clientTime = DateTimeUtils.GetClientTime(clientDateTime) ?? DateTime.Now;
                duration = VideoUploadManager.GetVideoDuration(String.Format("https://{0}", videoTubeStagingKey));
                videoTube = VideoTubeManage.InitializeVideoTubeUploadForUser(userId, filename, String.Format("//{0}", videoTubeStagingKey), duration, clientTime);


                if (videoTube != null)
                {
                    response.IsSuccess = true;
                    response.Response = videoTube;
                }
                else
                {
                    response.Message = "Unknown error. Failed to initialize custom video tube upload for user";
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Error occured while initializing custom video type upload for video '{0}' and user id={1}", videoTubeStagingKey, userId), ex);

                response.IsSuccess = false;
                response.Message = "Error occured while initializing custom video type upload.";
            }

            return response;
        }

        [WebMethod]
        public ResponseMessageModel GetVideoThumbnails(int videoTubeId)
        {
            Logger.Info(String.Format("Retrieving video thumbnails for video with id='{0}'", videoTubeId));

            var response = new ResponseMessageModel();

            try
            {
                var linksToThumbnails = VideoUploadManager.GetVideoThumbnails(videoTubeId);

                if (linksToThumbnails != null && linksToThumbnails.Count > 0)
                {
                    response.IsSuccess = true;
                    response.Response = linksToThumbnails;
                }
                else
                {
                    response.Message = "Unknown error occured. Unable to retrieve thumbnail images for specified video";
                }
            }
            catch (VideoUploadManagerException vex)
            {
                Logger.Error(String.Format("Error occured while retrieving video thumbnail images for video with id='{0}'", videoTubeId), vex);

                response.IsSuccess = false;
                response.Message = vex.Message;
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Error occured while retrieving thumbnail images for video with id='{0}'", videoTubeId), ex);

                response.IsSuccess = false;
                response.Message = "Failed to retrieve thumbnail images for specified video";
            }

            return response;
        }

        [WebMethod]
        public ResponseMessageModel GetVideoThumbnailsForUser(int userId, string videoFilePath)
        {
            Logger.Info(String.Format("Retrieving video thumbnails for user id={0} and video file '{1}'", userId, videoFilePath));

            var response = new ResponseMessageModel();

            try
            {
                var thumbnails = VideoUploadManager.GetVideoThumbnails(userId, videoFilePath);

                if (thumbnails != null && thumbnails.Count > 0)
                {
                    response.IsSuccess = true;
                    response.Response = thumbnails;
                }
                else
                {
                    response.Message = "Unknown error occured. Unable to retrieve thumbnail images for specified video";
                }
            }
            catch (VideoUploadManagerException vex)
            {
                Logger.Error(String.Format("Error occured while retrieving video thumbnail images for video file '{0}'", videoFilePath), vex);

                response.IsSuccess = false;
                response.Message = vex.Message;
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Error occured while retrieving thumbnail images for video file '{0}'", videoFilePath), ex);

                response.IsSuccess = false;
                response.Message = "Failed to retrieve thumbnail images for specified video";               
            }

            return response;
        }

        [WebMethod]
        public ResponseMessageModel GetCustomVideoThumbnail(int videoTubeId, float timeFrameInSec)
        {
            Logger.Info(String.Format("Retrieving custom video thumbnail from video id='{0}' at custom time frame of {1} sec", videoTubeId, timeFrameInSec));

            var response = new ResponseMessageModel();

            try
            {
                var thumbnail = VideoUploadManager.GetVideoThumbnail(videoTubeId, timeFrameInSec);

                if (!String.IsNullOrEmpty(thumbnail))
                {
                    response.IsSuccess = true;
                    response.Response = thumbnail;
                }
                else
                {
                    response.Message = "Unknown error occured. Unable to retrieve custom thumbnail image for specified video";
                }
            }
            catch (VideoUploadManagerException vex)
            {
                Logger.Error(String.Format("Error occured while retrieving custom thumbnail image for video id='{0}' at {1} sec", videoTubeId, timeFrameInSec), vex);

                response.IsSuccess = false;
                response.Message = vex.Message;
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Error occured while retrieving custom thumbnail image for video id='{0}' at {1} sec", videoTubeId, timeFrameInSec), ex);

                response.IsSuccess = false;
                response.Message = "Failed to retrieve custom video thumbnail for specified video";
            }

            return response;
        }

        [WebMethod]
        public ResponseMessageModel GetCustomVideoThumbnailForUser(int userId, string videoFilePath, float timeFrameInSec)
        {
            Logger.Info(String.Format("Retrieving custom video thumbnail for user id={0}, video '{1}' at custom time frame of {2} sec", userId, videoFilePath, timeFrameInSec));

            var response = new ResponseMessageModel();

            try
            {
                var thumbnail = VideoUploadManager.GetVideoThumbnail(userId, videoFilePath, timeFrameInSec);

                if (!String.IsNullOrEmpty(thumbnail))
                {
                    response.IsSuccess = true;
                    response.Response = thumbnail;
                }
                else
                {
                    response.Message = "Unknown error occured. Unable to retrieve custom thumbnail image for specified video";
                }
            }
            catch (VideoUploadManagerException vex)
            {
                Logger.Error(String.Format("Error occured while retrieving custom thumbnail image for video file '{0}' at {1} sec", videoFilePath, timeFrameInSec), vex);

                response.IsSuccess = false;
                response.Message = vex.Message;
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Error occured while retrieving custom thumbnail image for video file '{0}' at {1} sec", videoFilePath, timeFrameInSec), ex);

                response.IsSuccess = false;
                response.Message = "Failed to retrieve custom video thumbnail for specified video";
            }

            return response;
        }

        [WebMethod]
        public ResponseMessageModel GetVideoDurationForUser(int userId, string videoFilePath)
        {
            Logger.Info(String.Format("Retrieving video duration of the video '{0}' for user id={1}", videoFilePath, userId));

            var response = new ResponseMessageModel();

            try
            {
                float duration = VideoUploadManager.GetVideoDuration(videoFilePath);

                if (duration > 0)
                {
                    response.IsSuccess = true;
                    response.Response = duration;
                }
                else
                {
                    response.Message = "Unknown error occured. Video duration cannot be determine for specified video.";
                }
            }
            catch (VideoUploadManagerException vex)
            {
                Logger.Error(String.Format("Error occured while retrieving video duration of video '{0}' for user id={1}", videoFilePath, userId), vex);

                response.IsSuccess = false;
                response.Message = vex.Message;
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Error occured while retrieving video duration of video '{0}' for user id={1}", videoFilePath, userId), ex);

                response.IsSuccess = false;
                response.Message = "Failed to retrieve video duration of the specified video";
            }

            return response;
        }

        [WebMethod]
        public ResponseMessageModel GetVideoPreviewClip(int videoTubeId)
        {
            Logger.Info(String.Format("Retrieving video preview clip for video id='{0}'", videoTubeId));

            var response = new ResponseMessageModel();

            try
            {
                var previewClip = VideoUploadManager.GetVideoPreviewClip(videoTubeId);

                if (!String.IsNullOrEmpty(previewClip))
                {
                    response.IsSuccess = true;
                    response.Response = previewClip;
                }
                else
                {
                    response.Message = "Unknown error. Failed to retrieve video preview clip for specified video";
                }
            }
            catch (VideoUploadManagerException vex)
            {
                Logger.Error(String.Format("Error occured while retrieving video preview clip of video id='{0}'", videoTubeId), vex);

                response.IsSuccess = false;
                response.Message = vex.Message;
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Error occured while retrieving video preview clicp of video id='{0}'", videoTubeId), ex);

                response.IsSuccess = false;
                response.Message = "Failed to retrieve video preview clip for specified video";
            }

            return response;
        }

        [WebMethod]
        public ResponseMessageModel GetVideoPreviewClipForUser(int userId, string videoFilePath)
        {
            Logger.Info(String.Format("Retrieving video preview clip for user id={0} and video file '{1}'", userId, videoFilePath));

            var response = new ResponseMessageModel();

            try
            {
                var previewClip = VideoUploadManager.GetVideoPreviewClip(userId, videoFilePath);

                if (!String.IsNullOrEmpty(previewClip))
                {
                    response.IsSuccess = true;
                    response.Response = previewClip;
                }
                else
                {
                    response.Message = "Unknown error. Failed to retrieve video preview clip for specified video";
                }
            }
            catch (VideoUploadManagerException vex)
            {
                Logger.Error(String.Format("Error occured while retrieving video preview clip of video '{0}' for user id={1}", videoFilePath, userId), vex);

                response.IsSuccess = false;
                response.Message = vex.Message;
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Error occured while retrieving video preview clicp of video '{0}' for user id={1}", videoFilePath, userId), ex);

                response.IsSuccess = false;
                response.Message = "Failed to retrieve video preview clip for specified video";
            }

            return response;
        }

        [WebMethod]
        public ResponseMessageModel SubmitVideoForTranscoding(int videoTubeId)
        {
            Logger.Info(String.Format("Submitting video id={0} to be transcoded", videoTubeId));

            var response = new ResponseMessageModel();

            try
            {
                VideoUploadManager.SubmitVideoForTranscoding(videoTubeId);

                response.IsSuccess = true;
                response.Response = "Video was successfully submitted for transcoding";
            }
            catch (VideoUploadManagerException vex)
            {
                Logger.Error(String.Format("Error occured while submitting video id='{0}' to be transcoded", videoTubeId), vex);

                response.IsSuccess = false;
                response.Message = vex.Message;
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Error occured while submitting video id='{0}' to be transcoded", videoTubeId), ex);

                response.IsSuccess = false;
                response.Message = "Failed to submit video to be transcoded";
            }

            return response;
        }

        [WebMethod]
        public ResponseMessageModel UpdateUploadedVideo(CustomVideoTubeUploadModel videoModel)
        {
            Logger.Info(String.Format("Updating video id={0} to be transcoded", videoModel.VideoTubeId));

            var response = new ResponseMessageModel();

            try
            {
                VideoTubeManage.UpdateUploadedVideo(videoModel);

                response.IsSuccess = true;
                response.Response = "Video was successfully updated";
            }
            catch (VideoUploadManagerException vex)
            {
                Logger.Error(String.Format("Error occured while updating video id='{0}'", videoModel.VideoTubeId), vex);

                response.IsSuccess = false;
                response.Message = vex.Message;
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Error occured while updating video id='{0}'", videoModel.VideoTubeId), ex);

                response.IsSuccess = false;
                response.Message = "Failed to update video";
            }

            return response;
        }
    }
}
