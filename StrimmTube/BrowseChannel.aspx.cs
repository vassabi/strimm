using StrimmBL;
using Strimm.Model;
using StrimmTube.UC;
using StrimmTube.WebServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StrimmTube
{
    public partial class BrowseChannel : BasePage
    {
        public string categoryName { get; set; }
        public int channelsCount { get; set; }
        public int userId { get; set; }
        public int channelCategoryId { get; set; }
        List<ChannelTube> channelList { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsProd)
            {
                TurnOffCrawling();
            }

            if (Session["userId"] != null)
            {
                userId = int.Parse(Session["userId"].ToString());
            }

            if (Request.QueryString["id"] != null)
            {
                channelCategoryId = int.Parse(Request.QueryString["id"].ToString());
            }
        }
    }
}
