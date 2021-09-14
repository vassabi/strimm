using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StrimmTube.UC
{
    public partial class VideoScheduleUC : System.Web.UI.UserControl
    {
        public string title { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
        public string description { get; set; }
        public int id { get; set; }
        public bool isVideoRemoved { get; set; }
        public bool isVideoRestrictred { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(description))
            {
                description = "no description for this video";
            }
            if((isVideoRemoved==true)||(isVideoRestrictred==true))
            {
                removedVideo.Visible = true;
            }
        }
    }
}