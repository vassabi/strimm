using StrimmBL.Downloadable;
using System;

namespace StrimmTube
{
    public partial class DownloadLiveStream : System.Web.UI.Page
    {
        private string VideoFormat { get; set; }
        private string FileName { get; set; }
        private string ChannelName
        {
            get { return FileName.Substring(0, FileName.LastIndexOf(".")); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var format = RouteData.Values["format"];
            var file = RouteData.Values["file"];

            if (format == null || file == null)
            {
                txterrorMessage.InnerText = "Format is wrong";
                return;
            }

            VideoFormat = format.ToString().ToLower();
            FileName = file.ToString();


            FindVideoByFormat();
        }

        private void FindVideoByFormat()
        {
            try
            {
                var downloadObj = DownloadableFactory.Create(VideoFormat);
                if (downloadObj == null)
                {
                    txterrorMessage.InnerText = "Format is wrong";
                    return;
                }

                ReturnFileStream(downloadObj);
            }
            catch (Exception ex)
            {
                // Logger
            }
        }
        private void ReturnFileStream(IDownloadableObject downloadObject)
        {
            var fileBytes = downloadObject.GenerateFile(ChannelName);

            Response.ContentType = downloadObject.ContentType;
            //Response.AppendHeader("Content-Disposition", "attachment; filename=" + FileName);
            Response.OutputStream.Write(fileBytes, 0, fileBytes.Length);
            Response.End();
        }
    }
}