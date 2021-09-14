using System;
using System.Web;

namespace StrimmBL.Downloadable
{
    public abstract class BaseDownloadableObject : IDownloadableObject
    {
        public string ContentType { get; set; }
        public string FileFormat { get; set; }
        public string FileExtension { get; set; }

        public virtual byte[] GenerateFile(string channelName)
        {
            throw new NotImplementedException();
        }

        public virtual string GenerateLink(string channelName)
        {
            var fileUrl = string.Format("{0}/download/{1}/{2}.{3}/"
                , HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority)
                , FileFormat
                , channelName
                , FileExtension);
            return fileUrl;
        }
    }
}
