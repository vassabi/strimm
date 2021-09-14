using StrimmBL;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StrimmTube
{
    public partial class HLS : System.Web.UI.Page
    {
        protected async void Page_Load(object sender, EventArgs e)
        {
            string ext = string.Empty;
            if (Request.RawUrl.EndsWith(".m3u8"))
                ext = ".m3u8";
            if (Request.RawUrl.EndsWith(".json"))
                ext = ".json";

            string channelId = Request.RawUrl.Replace("/hls/", string.Empty).Replace(ext, string.Empty);
            int id = 0;
            if(int.TryParse(channelId, out id))
            {
                if (ext == ".m3u8")
                    M3U8(id);
                if (ext == ".json")
                    JSON(id);
            }
        }

        private async void M3U8(int id)
        {
            HLSManager hls = new HLSManager();
            await hls.GenerateHLSLinkForChannel(id);

            string data = hls.generatedData;

            if (data != null)
            {
                MemoryStream ms = new MemoryStream();
                TextWriter tw = new StreamWriter(ms);
                tw.Write(hls.generatedData);
                tw.Flush();
                byte[] bytes = ms.ToArray();
                ms.Close();
                Response.Clear();
                Response.ContentType = "application/force-download";
                Response.AddHeader("content-disposition", "attachment;    filename=" + id + ".m3u8");
                Response.BinaryWrite(bytes);
                HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
                HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
                HttpContext.Current.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the EndRequest event.
            }
        }
        private void JSON(int id)
        {
            HLSManager hls = new HLSManager();
            hls.GenerateJSONLinkForChannel(id);
            string data = hls.generatedData;
            if (data != null)
            {
                MemoryStream ms = new MemoryStream();
                TextWriter tw = new StreamWriter(ms);
                tw.Write(data);
                tw.Flush();
                byte[] bytes = ms.ToArray();
                ms.Close();
                Response.Clear();
                Response.ContentType = "application/force-download";
                Response.AddHeader("content-disposition", "attachment;    filename=" + id + ".json");
                Response.BinaryWrite(bytes);
                HttpContext.Current.Response.Flush(); // Sends all currently buffered output to the client.
                HttpContext.Current.Response.SuppressContent = true;  // Gets or sets a value indicating whether to send HTTP content to the client.
                HttpContext.Current.ApplicationInstance.CompleteRequest(); // Causes ASP.NET to bypass all events and filtering in the HTTP pipeline chain of execution and directly execute the EndRequest event.
            }
        }
    }
}