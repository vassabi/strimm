using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StrimmTube.UC
{
    public partial class EmbeddedPageChannelBox : System.Web.UI.UserControl
    {
        public string channelHref { get; set; }
        public string channelName { get; set; }
        public string channelImage { get; set; }
        public int channelId { get; set; }
        public int channelNumber { get; set; }

        public string channelNumberString
        {
            get
            {
                return channelNumber < 10 ? String.Format("0{0}", channelNumber) : Convert.ToString(channelNumber);
            }
        }

        private bool isOnAir = false;
        public bool IsOnAir
        {
            get
            {
                return this.isOnAir;
            }
            set
            {
                this.isOnAir = value;
                channelTiming.Visible = value;
            }
        }

        private bool isLink;
        public bool IsLink
        {
            get
            {
                return this.isLink;
            }
            set
            {
                this.isLink = value;
                seeMoreLink.Visible = this.isLink;
                channelBoxEmbedded.Visible = !this.isLink;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
        }
    }
}