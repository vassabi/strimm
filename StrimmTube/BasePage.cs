using log4net;
using Strimm.Model;
using StrimmBL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace StrimmTube
{
    public class BasePage : System.Web.UI.Page 
    {       
        public static readonly string DomainName;
        public static readonly string RecaptchaPrivateKey;
        public static readonly string ImagesBucket;
        public static readonly string PromoVideoId;
        public static readonly string AllowProviderSelection;
        public static readonly int MaxUserChannelCount;
        public static readonly int MaxUploadFileSize;
        public static readonly int AmazonS3FileUploadTimoutInSec;
        public static readonly int VideosToAddToFirstChannel;
        public static readonly string MailFrom;
        public static readonly string FeaturedChannelsUser;
        public static readonly string GroupName;
        public static readonly string ChannelsOnLandingPage;
        public static readonly string AdminEmail;
        public static readonly string VideoDevelopmentKey;
        public static readonly string VimeoRegex;
        public static readonly string YouTubeDevelopementKey;
        public static readonly string YouTubeVideoRegex;
        public static readonly int VideoPageSize;

        private static readonly string NetworkPageSocialDescription;
        private static readonly string ChannelPageSocialDescription;
        private static readonly string NetworkPageSocialTitle;
        private static readonly string ChannelPageSocialTitle;
        private static readonly string NetworkPageTwitterTitle;
        private static readonly string ChannelPageTwitterTitle;

        private static string PageDescription = String.Empty;
        private static string PageKeywords = String.Empty;

        private static readonly ILog Logger = LogManager.GetLogger(typeof(BasePage));

        public static readonly string FacebookAppId;

        static BasePage()
        {
            try
            {
                Logger.Debug("Starting to load config parameters");

                DomainName = ConfigurationManager.AppSettings["domainName"] ?? "www.strimm.com";
                PageDescription = ConfigurationManager.AppSettings["PageDescription"];
                PageKeywords = ConfigurationManager.AppSettings["PageKeywords"];
                FacebookAppId = ConfigurationManager.AppSettings["FacebookAppId"];
                RecaptchaPrivateKey = ConfigurationManager.AppSettings["recaptchaPrivateKey"];
                ImagesBucket = ConfigurationManager.AppSettings["ImagesBucket"];
                PromoVideoId = ConfigurationManager.AppSettings["PromoVideoId"];
                AllowProviderSelection = ConfigurationManager.AppSettings["AllowProviderSelection"];
                MailFrom = ConfigurationManager.AppSettings["mailFrom"];
                FeaturedChannelsUser = ConfigurationManager.AppSettings["FeaturedChannelsUser"];
                GroupName = ConfigurationManager.AppSettings["GroupName"];
                ChannelsOnLandingPage = ConfigurationManager.AppSettings["ChannelsOnLandingPage"];
                AdminEmail = ConfigurationManager.AppSettings["adminEmail"];
                VideoDevelopmentKey = ConfigurationManager.AppSettings["vdevelopmentkey"];
                VimeoRegex = ConfigurationManager.AppSettings["VimeoRegex"];
                YouTubeDevelopementKey = ConfigurationManager.AppSettings["ydevelopementkey"];
                YouTubeVideoRegex = ConfigurationManager.AppSettings["YouTubeVideoRegex"];

                NetworkPageSocialDescription = ConfigurationManager.AppSettings["NetworkPageSocialDescription"];
                ChannelPageSocialDescription = ConfigurationManager.AppSettings["ChannelPageSocialDescription"];
                NetworkPageSocialTitle = ConfigurationManager.AppSettings["NetworkPageSocialTitle"];
                ChannelPageSocialTitle = ConfigurationManager.AppSettings["ChannelPageSocialTitle"];
                NetworkPageTwitterTitle = ConfigurationManager.AppSettings["NetworkPageTwitterTitle"];
                ChannelPageTwitterTitle = ConfigurationManager.AppSettings["ChannelPageTwitterTitle"];

                int count = 12;
                Int32.TryParse(ConfigurationManager.AppSettings["maxUserChannelCount"], out count); 
                MaxUserChannelCount = count;

                Int32.TryParse(ConfigurationManager.AppSettings["maxUploadFileSize"], out count);
                MaxUploadFileSize = count;

                Int32.TryParse(ConfigurationManager.AppSettings["AmazonS3FileUploadTimoutInSec"], out count);
                AmazonS3FileUploadTimoutInSec = count;

                Int32.TryParse(ConfigurationManager.AppSettings["VideosToAddToFirstChannel"], out count);
                VideosToAddToFirstChannel = count;

                Int32.TryParse(ConfigurationManager.AppSettings["VideoPageSize"], out count);
                VideoPageSize = count;

                Logger.Debug("Done loading config parameters");
            }
            catch (Exception ex)
            {
                Logger.Error("Failed to read site configuration", ex);
                DomainName = "www.strimm.com";
            }
        }

        public BasePage()
        {
        }

        #region Public Properties

        public string facebookAppId
        {
            get;
            set;
        }

        public bool HideOldFooter
        {
            get;
            set;
        }

        public bool IsProd
        {
            get
            {
                return DomainName != null && (DomainName == "www.strimm.com" || DomainName == "strimm.com");
            }
        }

        #endregion

        #region Public Methods

        public string GetNetworkPageSocialDescription() 
        {
            return NetworkPageSocialDescription;
        }

        public string GetChannelPageSocialDescription()
        {
            return ChannelPageSocialDescription;
        }

        public string GetNetworkPageSocialTitle(string publicName)
        {
            return String.Format(NetworkPageSocialTitle, publicName);
        }

        public string GetChannelPageSocialTitle(string publicName)
        {
            return String.Format(ChannelPageSocialTitle, publicName);
        }

        public string GetNetworkPageTwitterTitle(string publicName, string absoluteUri)
        {
            return String.Format(NetworkPageTwitterTitle, publicName, absoluteUri);
        }

        public string GetChannelPageTwitterTitle(string publicName, string absoluteUri)
        {
            return String.Format(ChannelPageTwitterTitle, publicName, absoluteUri);
        }

        // I've tended to create overloads of this that take just an href and type 
        // for example that allows me to use this to add CSS to a page dynamically
        public void AddHeaderLink(string href,
                                  string rel,
                                  string type,
                                  string media)
        {
            HtmlLink link = new HtmlLink();
            link.Href = String.IsNullOrEmpty(href) ? DomainName : String.Format("https://{0}/{1}", DomainName, href);

            // As I'm working with XHTML, I want to ensure all attributes are lowercase
            // Also, you could replace the length checks with string.IsNullOrEmpty if 
            // you prefer.
            if (0 != type.Length)
            {
                link.Attributes.Add(HtmlTextWriterAttribute.Type.ToString().ToLower(),
                                    type);
            }

            if (0 != rel.Length)
            {
                link.Attributes.Add(HtmlTextWriterAttribute.Rel.ToString().ToLower(), rel);
            }

            if (0 != media.Length)
            {
                link.Attributes.Add("media", media);
            }

            ContentPlaceHolder cph = (ContentPlaceHolder)Master.FindControl("canonicalHolder");
            cph.Controls.Add(link);
        }

        public void AddTitle(string pageName, bool overwrite)
        {
            LiteralControl title = new LiteralControl();
            title.Text = overwrite ? pageName : String.Format("{0} | Strimm TV", pageName);

            Page.Master.FindControl("titleHolder").Controls.Clear();
            Page.Master.FindControl("titleHolder").Controls.Add(title);
        }

        public void UpdateDescription(string newDescription, HttpContext httpContext = null)
        {
            ContentPlaceHolder cph = (ContentPlaceHolder)Master.FindControl("metaHolder");
            LiteralControl literalCtrl = (from ctrls in cph.Controls.OfType<LiteralControl>()
                                     where ctrls.Text.Contains("description")
                                     select ctrls).FirstOrDefault();

            if (literalCtrl != null)
            {
                cph.Controls.Remove(literalCtrl);
            }

            var htmlMetaCtrl = new HtmlMeta();
            htmlMetaCtrl.Name = "description";
            htmlMetaCtrl.Content = newDescription;
            cph.Controls.Add(htmlMetaCtrl);
        }

        public void SetPageMeta(string metaName, string metaContent, bool shouldUpdate, HttpContext httpContext = null)
        {
            if (string.IsNullOrWhiteSpace(metaName))
                return;

            if (metaContent == null)
                throw new Exception("Dynamic Meta tag content can not be null. Pl pass a valid meta tag content");

            if (httpContext == null)
                httpContext = HttpContext.Current;

            Page page = httpContext.Handler as Page;
            if (page != null)
            {
                if (Header != null)
                {
                    var metaCtrls = Header.Controls != null ? (from ctrls in Header.Controls.OfType<HtmlMeta>()
                                                                where ctrls.Name.Equals(metaName, StringComparison.CurrentCultureIgnoreCase)
                                                                select ctrls) : null;

                    HtmlMeta htmlMetaCtrl = metaCtrls != null ? metaCtrls.FirstOrDefault() : null;

                    if (!shouldUpdate || htmlMetaCtrl == null)
                    {
                        if (htmlMetaCtrl != null)
                            Header.Controls.Remove(htmlMetaCtrl);

                        htmlMetaCtrl = new HtmlMeta();
                        htmlMetaCtrl.HttpEquiv = metaName;
                        htmlMetaCtrl.Name = metaName;
                        htmlMetaCtrl.Content = metaContent;
                        Header.Controls.Add(htmlMetaCtrl);
                    }
                    else
                    {
                        htmlMetaCtrl.Content = metaContent;
                    }
                }
            }
            else
            {
                throw new Exception("Web page handler context could not be obtained");
            }
        }

        public void TurnOffCrawling()
        {
            SetPageMeta("robots", "NOFOLLOW, NOINDEX", true);
            SetPageMeta("GOOGLEBOT", "NOFOLLOW, NOINDEX", true);
        }

        public void AddSocialMetaTags(string name, string pictureUrl, string description, string absoluteUri)
        {
            var htmlTitle = Header.Controls.OfType<HtmlTitle>().FirstOrDefault();
            int startIndex = 0;

            if (htmlTitle != null)
            {
                startIndex = Header.Controls.IndexOf(htmlTitle) + 1;
            }

            #region Google+ meta tags

            var metaTitle = new HtmlMeta();
            metaTitle.Attributes.Add("itemprop", "name");
            metaTitle.Attributes.Add("content", String.Format("{0}.\n\r{1}", name, description));
            Header.Controls.AddAt(startIndex++, metaTitle);

            var metaImageUrl = new HtmlMeta();
            metaImageUrl.Attributes.Add("itemprop", "image");
            metaImageUrl.Attributes.Add("content", pictureUrl);
            Header.Controls.AddAt(startIndex++, metaImageUrl);

            var metaDescription = new HtmlMeta();
            metaDescription.Attributes.Add("itemprop", "description");
            metaDescription.Attributes.Add("content", description);
            Header.Controls.AddAt(startIndex++, metaDescription);

            #endregion

            #region Twitter cart meta tags

            var twitterCard = new HtmlMeta();
            twitterCard.Attributes.Add("name", "twitter:card");
            twitterCard.Attributes.Add("content", "summary_large_image");
            Header.Controls.AddAt(startIndex++, twitterCard);

            var twitterSite = new HtmlMeta();
            twitterSite.Attributes.Add("name", "twitter:site");
            twitterSite.Attributes.Add("content", "@StrimmTV");
            Header.Controls.AddAt(startIndex++, twitterSite);

            var twitterTitle = new HtmlMeta();
            twitterTitle.Attributes.Add("name", "twitter:title");
            twitterTitle.Attributes.Add("content", name);
            Header.Controls.AddAt(startIndex++, twitterTitle);

            var twitterDesc = new HtmlMeta();
            twitterDesc.Attributes.Add("name", "twitter:description");
            twitterDesc.Attributes.Add("content", description);
            Header.Controls.AddAt(startIndex++, twitterDesc);

            //var twitterCreator = new HtmlMeta();
            //twitterCreator.Attributes.Add("name", "twitter:creator");
            //twitterCreator.Attributes.Add("content", "@author_handle");
            //Header.Controls.AddAt(startIndex++, twitterCreator);

            var twitterImgSrc = new HtmlMeta();
            twitterImgSrc.Attributes.Add("name", "twitter:image");
            twitterImgSrc.Attributes.Add("content", pictureUrl);
            Header.Controls.AddAt(startIndex++, twitterImgSrc);

            #endregion

            #region Facebook meta tags

            var metaOgTitle = new HtmlMeta();
            metaOgTitle.Attributes.Add("property", "og:title");
            metaOgTitle.Attributes.Add("content", name);
            Header.Controls.AddAt(startIndex++, metaOgTitle);

            var metaOgType = new HtmlMeta();
            metaOgType.Attributes.Add("property", "og:type");
            metaOgType.Attributes.Add("content", "video.tv_show");
            Header.Controls.AddAt(startIndex++, metaOgType);

            var metaOgImage = new HtmlMeta();
            metaOgImage.Attributes.Add("property", "og:image");
            metaOgImage.Attributes.Add("content", pictureUrl);
            Header.Controls.AddAt(startIndex++, metaOgImage);

            var metaOgDescription = new HtmlMeta();
            metaOgDescription.Attributes.Add("property", "og:description");
            metaOgDescription.Attributes.Add("content", description);
            Header.Controls.AddAt(startIndex++, metaOgDescription);

            var metaOgSite = new HtmlMeta();
            metaOgSite.Attributes.Add("property", "og:site_name");
            metaOgSite.Attributes.Add("content", DomainName);
            Header.Controls.AddAt(startIndex++, metaOgSite);

            var metaOgUrl = new HtmlMeta();
            metaOgUrl.Attributes.Add("property", "og:url");
            metaOgUrl.Attributes.Add("content", absoluteUri);
            Header.Controls.AddAt(startIndex++, metaOgUrl);

            DateTime sTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var metaUpdateTime = new HtmlMeta();
            metaUpdateTime.Attributes.Add("property", "og:updated_time");
            metaUpdateTime.Attributes.Add("content", Convert.ToString(Convert.ToInt64((DateTime.Now - sTime).TotalSeconds)));
            Header.Controls.AddAt(startIndex++, metaUpdateTime);


            #endregion

            #region Pinterest

            var domainVerifyPinterest = new HtmlMeta();
            domainVerifyPinterest.Name="p:domain_verify";
            domainVerifyPinterest.Content="1283df95a7aa0e31f24f5cc4402be76a";
            Header.Controls.AddAt(startIndex++, domainVerifyPinterest);

            #endregion

            var metaFbAppId = new HtmlMeta();
            metaFbAppId.Name = "fb:app_id";
            metaFbAppId.Content = FacebookAppId;
            Header.Controls.AddAt(startIndex, metaFbAppId);

        }


        #endregion
    }
}