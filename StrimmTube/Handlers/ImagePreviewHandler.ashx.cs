using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
namespace StrimmTube.Handlers
{
    /// <summary>
    /// Summary description for ImagePreviewHandler
    /// </summary>
    public class ImagePreviewHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            context.Response.Clear();
            if ((context.Request.QueryString.Count != 0))
            {
                var storedImage = context.Session["imgBytes"] as byte[];
                if (storedImage != null)
                {
                    Image image = getImage(storedImage);
                    if (image != null)
                    {
                        context.Response.ContentType = "image/jpeg";
                        image.Save(context.Response.OutputStream, ImageFormat.Jpeg);
                    }
                }
            }
        }
        private Image getImage(byte[] storedImage)
        {
            var stream = new MemoryStream(storedImage);
            return Image.FromStream(stream);
        }
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}