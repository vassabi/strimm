using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StrimmTube.UC
{
    public partial class ScheduleThumbUC : System.Web.UI.UserControl
    {
        public string startDate { get; set; }
        public string endDate { get; set; }
        public int videoScheduleId { get; set; }
        public string videoTitle { get; set; }
        public string videoPath { get; set; }
        public string videoUploadId { get; set; }
        public int tempId { get; set; }
        public string thumbSrc { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}