using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StrimmTube.UC
{
    public partial class BoardChannelBox : System.Web.UI.UserControl
    {
        public string channelHref { get; set; }
        public string channelName { get; set; }
        public string channelImage { get; set; }
        public string rating { get; set; }
        public string subscribers { get; set; }
        public string views { get; set; }
        public string channelCategory {get;set; }
        public string redOrGreen { get; set; }
        public string playNotPlay { get; set; }
        public string editChannelUrl { get; set; }
        public bool isMyChannel { get; set; }
        public int channelId { get; set; }
        public string userName { get; set; }

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

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!isMyChannel)
            {
                this.chanBoxEdit.Visible = false;
                this.lblFans.Visible = false;
                this.lblViews.Visible = false;
            }
        }

        protected void ancEdit_Click(object sender, EventArgs e)
        {
            Session["ChannelTubeId"] = channelId;
            Response.Redirect("/"+userName+"/my-studio/" + channelName.Replace(" ",""), false);
            Context.ApplicationInstance.CompleteRequest();
        }
    }
}