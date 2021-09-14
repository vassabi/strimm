using Strimm.Model;
using Strimm.Model.Projections;
using StrimmBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StrimmTube
{
    public partial class ChannelPassword : System.Web.UI.Page
    {

        public string channelPassword { get; set; }
        public string channelUrl { get; set; }
        public string channelOwnerUserName { get; set; }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["channelName"]!=null)
            {
                channelUrl = Request.QueryString["channelName"].ToString();
            }
            else
            {
                Response.Redirect("/home", false);
                Context.ApplicationInstance.CompleteRequest();
                return;
            }
            

            if (!String.IsNullOrEmpty(channelUrl))
            {
                ChannelTube channelTube = ChannelManage.GetChannelTubeByUrl(channelUrl);
                channelPassword = channelTube.ChannelPassword;
                channelOwnerUserName = UserManage.GetUserByChannelName(channelTube.Name).UserName;
            }
            else
            {
                Response.Redirect("/home", false);
                Context.ApplicationInstance.CompleteRequest();
                return;
            }
        }
    }
}