using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StrimmTube.UC
{
    public partial class SearchedVideosUC : System.Web.UI.UserControl
    {
        public string videoTitle { get; set; }
        public string videoDescription { get; set; }
        public int channelId { get; set; }
        public double duration { get; set; }
        public long views { get; set; }
        public string channelName { get; set; }
        public string imgUrl { get; set; }
        public int videoUploadId { get; set; }
        public DateTime startTime { get; set; }
        public DateTime clientTime { get; set; }
        public string channelurl { get; set; }
        public int videoScheduleId { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}