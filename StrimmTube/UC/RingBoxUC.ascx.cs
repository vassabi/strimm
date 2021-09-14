using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StrimmTube.UC
{
    public partial class RingBoxUC : System.Web.UI.UserControl
    {
        public string channelUrl { get; set; }
        public string dateTimeOfRing { get; set; }
        public string userNameRingSender { get; set; }
        public string channelImage { get; set; }
        public string channelName { get; set; }
        public string ringId { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}