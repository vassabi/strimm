using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StrimmTube.admcp.mgmt.UC
{
    public partial class ChannelBoxUC : System.Web.UI.UserControl
    {
        public int channelId { get; set; }
        public string channelName { get; set; }
        public string channelUrl { get; set; }

        public bool isDeleted { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
    }
}