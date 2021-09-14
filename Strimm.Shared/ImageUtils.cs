using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using log4net;

namespace Strimm.Shared
{
    public static class ImageUtils
    {
        private static ILog Logger = LogManager.GetLogger(typeof(ImageUtils));

        public static readonly string DomainName;
        public static readonly string DefaultChannelPictureUrl;
        public static readonly string AmazonS3WsDomain;
        public static readonly string DefaultAvatarMale;
        public static readonly string DefaultAvatarFemale;
        public static readonly string DefaultStrimmIcon;
        public static readonly string DefaultImageDomain;
        public static readonly string DefaultPicture;
        public static readonly string ImagesBucket;

        static ImageUtils()
        {
            try
            {
                DomainName = ConfigurationManager.AppSettings["domainName"] ?? "www.strimm.com";
                DefaultPicture = ConfigurationManager.AppSettings["DefaultChannelPictureUrl"];
                ImagesBucket = ConfigurationManager.AppSettings["ImagesBucket"];
                DefaultChannelPictureUrl = ConfigurationManager.AppSettings["DefaultChannelPictureUrl"];
                AmazonS3WsDomain = ConfigurationManager.AppSettings["AmazonS3WsDomain"];
                DefaultAvatarMale = ConfigurationManager.AppSettings["DefaultAvatarMale"];
                DefaultAvatarFemale = ConfigurationManager.AppSettings["DefaultAvatarFemale"];
                DefaultStrimmIcon = ConfigurationManager.AppSettings["DefaultStrimmIcon"];
                DefaultPicture = ConfigurationManager.AppSettings["DefaultChannelPictureUrl"];

                DefaultImageDomain = DomainName.StartsWith("localhost") 
                    ? "http://s3.amazonaws.com/" 
                    : AmazonS3WsDomain;
            }
            catch (Exception ex)
            {
                DomainName = "www.strimm.com";
            }
        }

        public static string DefaultChannelImageAbsoluteUrl
        {
            get
            {
                return String.Format("{0}/{1}{2}", AmazonS3WsDomain, ImagesBucket, DefaultChannelPictureUrl);
            }
        }

        public static string DefaultAvatarImageAbsoluteUrl
        {
            get
            {
                return String.Format("{0}{1}{2}", AmazonS3WsDomain, ImagesBucket, DefaultChannelPictureUrl);
            }
        }

        public static string DefaultAvatarMaleImageAbsoluteUrl
        {
            get
            {
                return String.Format("{0}{1}{2}", DefaultImageDomain, ImagesBucket, DefaultAvatarMale);
            }
        }

        public static string DefaultAvatarFemaleImageAbsoluteUrl
        {
            get
            {
                return String.Format("{0}{1}{2}", DefaultImageDomain, ImagesBucket, DefaultAvatarFemale);
            }
        }

        public static string DefaultStrimmIconAbsoluteUrl
        {
            get
            {
                return String.Format("{0}{1}{2}", DefaultImageDomain, ImagesBucket, DefaultStrimmIcon);
            }
        }

        public static string GetChannelImageUrl(string url)
        {
            string pictureUrl = url;

            if (!String.IsNullOrEmpty(url) && url.StartsWith("/"))
            {
                pictureUrl = String.Format("{0}{1}", AmazonS3WsDomain, url);
            }
            else
            {
                pictureUrl = DefaultChannelImageAbsoluteUrl;
            }

            Logger.Debug(String.Format("Channel: Original Url '{0}' | Final Url '{1}'", url, pictureUrl));

            return pictureUrl;
        }

        public static string GetCustomLogoImageUrl(string url)
        {
            string pictureUrl = url;

            if (!String.IsNullOrEmpty(url) && url.StartsWith("/"))
            {
                pictureUrl = String.Format("{0}{1}", AmazonS3WsDomain, url);
            }

            Logger.Debug(String.Format("Custom Logo: Original Url '{0}' | Final Url '{1}'", url, pictureUrl));

            return pictureUrl;
        }

        public static string GetProfileImageUrl(string url)
        {
            string pictureUrl = url;

            if (!String.IsNullOrEmpty(url))
            {
                if (url.StartsWith("/"))
                {
                    pictureUrl = String.Format("{0}/{1}", AmazonS3WsDomain, url.Substring(1));
                }
            }
            else
            {
                pictureUrl = DefaultChannelImageAbsoluteUrl;
            }

            Logger.Debug(String.Format("Profile: Original Url '{0}' | Final Url '{1}'", url, pictureUrl));

            return pictureUrl;
        }

        public static string GetBackgroundImageUrl(string url)
        {
            string pictureUrl = url;

            if (!String.IsNullOrEmpty(url) && url.StartsWith("/"))
            {
                pictureUrl = String.Format("{0}/{1}", AmazonS3WsDomain, url.Substring(1));
            }

            Logger.Debug(String.Format("Background: Original Url '{0}' | Final Url '{1}'", url, pictureUrl));

            return pictureUrl;
        }
    }
}
