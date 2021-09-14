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
    public partial class EmbeddedChannelPassword : System.Web.UI.Page
    {
        public string channelPassword { get; set; }

        public string channelUrl { get; set; }
        public string channelOwnerUserName { get; set; }
        public string accountNumber { get; set; }
        public string embedPasswordUrl { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.AppendHeader("Access-Control-Allow-Origin", "*");

            System.Web.UI.WebControls.Literal lit = new System.Web.UI.WebControls.Literal();
            System.Web.UI.WebControls.Literal lit1 = new System.Web.UI.WebControls.Literal();

            lit.Text = System.Web.Optimization.Styles.Render("~/bundles/embedded/css").ToHtmlString();
            lit1.Text = System.Web.Optimization.Scripts.Render("~/bundles/embeddedPass/js").ToHtmlString();
            Page.Header.Controls.AddAt(1, lit);
            Page.Header.Controls.AddAt(2, lit1);
            if (Request.QueryString["channelName"] != null)
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
                UserPo user = UserManage.GetUserByChannelName(channelTube.Name);
                channelPassword = channelTube.ChannelPassword;
                channelOwnerUserName = user.UserName;
                accountNumber = user.AccountNumber;

                
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