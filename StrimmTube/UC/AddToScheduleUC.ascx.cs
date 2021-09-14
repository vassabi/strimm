using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace StrimmTube.UC
{
    public partial class AddToScheduleUC : System.Web.UI.UserControl
    {
        public string containerId { get; set; }
        public string thumbnailSrc { get; set; }
        public string title { get; set; }
        public string playId { get; set; }
        public double duration { get; set; }
        public string showDuration { get; set; }
        public string views { get; set; }
        public string addToScheduleId { get; set; }
        public string provider { get; set; }
       public int schduleListId { get; set; }
       public DateTime startTime { get; set; }
       public DateTime endTime { get; set; }
       public string r_rated { get; set; }
       public bool isVideoRemovedByYoutube { get; set; }
       public bool isRestrictedVideo { get; set; }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            double d = Convert.ToDouble(duration);
            endTime = startTime.AddMinutes(d);
            if(isVideoRemovedByYoutube==true)
            {
                addVideo.Visible = false;
                removedDiv.Visible = true;
            }
            if(isRestrictedVideo==true)
            {
                addVideo.Visible = false;
                restrictedDiv.Visible = true;
            }
        }
    }
}