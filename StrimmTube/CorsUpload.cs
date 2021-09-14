using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace StrimmTube
{
    public class CorsUpload
    {
        private static ILog Logger = LogManager.GetLogger(typeof(CorsUpload));

        private static string AWS_ACCESS_KEY;
        private static string AWS_SECRET_KEY;
        private static string S3_BUCKET;
        private static string S3_VIDEO_INPUT_BUCKET;
        private static string S3_VIDEO_USER_FOLDER;
        private static string S3_DOMAIN;
        private static string S3_STAGING_FOLDER;
        private static string S3_CLOUDFRONT_WEB_URL;
        private static string S3_CLOUDFRONT_RTMP_URL;
        private static string S3_CLOUDFRONT_STAGING_URL;
        private static string DM_API_KEY;
        private static string YDEV_CL_ID;
        private static string YDEV_API_KEY;

        static CorsUpload()
        {
            try
            {
                AWS_ACCESS_KEY = ConfigurationManager.AppSettings["AmazonAccessKey"];
                AWS_SECRET_KEY = ConfigurationManager.AppSettings["AmazonSecretKey"];
                S3_BUCKET = ConfigurationManager.AppSettings["ImagesBucket"];
                S3_VIDEO_INPUT_BUCKET = ConfigurationManager.AppSettings["AmazonS3VideoInputBucket"];
                S3_VIDEO_USER_FOLDER = ConfigurationManager.AppSettings["AmazonS3VideoUserFolder"];
                S3_DOMAIN = ConfigurationManager.AppSettings["AmazonS3FileUrlDomain"];
                S3_STAGING_FOLDER = ConfigurationManager.AppSettings["AmazonS3VideoStagingFolder"];
                S3_CLOUDFRONT_WEB_URL = ConfigurationManager.AppSettings["AmazonWebDistribDomain"];
                S3_CLOUDFRONT_RTMP_URL = ConfigurationManager.AppSettings["AmazonRtmpDistribDomain"];
                S3_CLOUDFRONT_STAGING_URL = ConfigurationManager.AppSettings["AmazonWebStagingDistribDomain"];
                DM_API_KEY = ConfigurationManager.AppSettings["DailyMotionApiKey"];
                YDEV_CL_ID = ConfigurationManager.AppSettings["ydev_oauth2_client_id"];
                YDEV_API_KEY = ConfigurationManager.AppSettings["ydevelopementkey"];
            }
            catch (Exception ex)
            {
                Logger.Error("Error occured while reading configuration", ex);
            }
        }

        public string S3UploadUrl { get { return String.Format("{0}{1}/", S3_DOMAIN, S3_VIDEO_INPUT_BUCKET); } }

        public string S3OriginDomain { get { return S3_CLOUDFRONT_WEB_URL; } }

        public string StagingFolder { get { return S3_STAGING_FOLDER; } }

        public string UserVideoFolder { get { return S3_VIDEO_USER_FOLDER; } }

        public string Bucket { get { return S3_BUCKET; } }

        public string VideoInputBucket { get { return S3_VIDEO_INPUT_BUCKET; } }

        private string AwsAccessKeyID { get { return AWS_ACCESS_KEY; } }

        private string AwsSecretKey { get { return AWS_SECRET_KEY; } }

        private string Policy { get { return Convert.ToBase64String(Encoding.ASCII.GetBytes((MyPolicy))); } }

        private string Signature { get { return GetS3Signature(MyPolicy); } }

        private string DmApiKey { get { return DM_API_KEY; } }

        private string MyPolicy
        {
            get
            {
                return "{\"expiration\": \"2020-01-01T00:00:00Z\", " +
                    "\"conditions\": [ " +
                    "{\"bucket\": \"" + S3_VIDEO_INPUT_BUCKET + "\"}, " +
                    "[\"starts-with\", \"$key\", \"" + S3_STAGING_FOLDER + "/\"]," +
                    "{\"acl\": \"public-read\"}," +
                    "[\"starts-with\", \"$Content-Type\", \"\"]" +
                    "]}";
            }
        }

        private string GetS3Signature(string policyStr)
        {
            var b64Policy = Convert.ToBase64String(Encoding.ASCII.GetBytes(policyStr));
            var b64Key = Encoding.ASCII.GetBytes(AwsSecretKey);
            var hmacSha1 = new HMACSHA1(b64Key);

            return Convert.ToBase64String(hmacSha1.ComputeHash(Encoding.ASCII.GetBytes(b64Policy)));
        }

        public new string ToString()
        {
            var build = new StringBuilder();
            build.Append("<script type='text/javascript'>");
            build.AppendFormat("var access_key = '{0}';", AwsAccessKeyID);
            build.AppendFormat("var policy = '{0}';", Policy);
            build.AppendFormat("var signature = '{0}';", Signature);
            build.AppendFormat("var folder = '{0}';", UserVideoFolder);
            build.AppendFormat("var stagingFolder = '{0}';", StagingFolder);
            build.AppendFormat("var bucket = '{0}';", Bucket);
            build.AppendFormat("var videoBucket = '{0}';", VideoInputBucket);
            build.AppendFormat("var s3UploadUrl = '{0}';", S3UploadUrl);
            build.AppendFormat("var s3Domain = '{0}';", S3_DOMAIN);
            build.AppendFormat("var cfWebOrigin = '{0}';", S3_CLOUDFRONT_WEB_URL);
            build.AppendFormat("var cfRtmpOrigin = '{0}';", S3_CLOUDFRONT_RTMP_URL);
            build.AppendFormat("var cfStagingOrigin = '{0}';", S3_CLOUDFRONT_STAGING_URL);
            build.AppendFormat("var dmApiKey = '{0}';", DM_API_KEY);
            build.AppendFormat("var ydevApiKey = '{0}';", YDEV_API_KEY);
            build.AppendFormat("var ydevClientId = '{0}';", YDEV_CL_ID);

            build.Append("</script>");

            return build.ToString();
        }
    }
}