using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StrimmTube.UC
{
    public partial class FeaturedChannelBoxUC : System.Web.UI.UserControl
    {
        public string channelUrl { get; set; }
        public string channelImageSrc { get; set; }
        public string channelName { get; set; }
        public string channelCategory { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}