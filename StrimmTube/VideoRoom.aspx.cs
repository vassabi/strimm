using log4net;
using StrimmBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StrimmTube
{
    public partial class VideoRoom : BasePage
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(VideoRoom));

        public int UserId { get; set; }

        protected void Page_Init(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetAllowResponseInBrowserHistory(false);
        }

        protected override void OnPreRender(EventArgs e)
        {
            if (!IsPostBack)
            {
                TurnOffCrawling();
            }

            base.OnPreRender(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int uid = 0;

                if (Session["userId"] == null || !Int32.TryParse(Session["userId"].ToString(), out uid))
                {
                    Response.Redirect("/home", false);
                    Context.ApplicationInstance.CompleteRequest();
                    return;
                }

                string userName = Page.RouteData.Values["UserName"].ToString();

                if (!string.IsNullOrEmpty(userName))
                {
                    try
                    {
                        var userPo = UserManage.GetUserByPublicUrl(userName);
                        if (userPo != null)
                        {
                            uid = userPo.UserId;
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(String.Format("Failed to retrieve user by public url '{0}'", userName));

                        Response.Redirect("/home", false);
                        Context.ApplicationInstance.CompleteRequest();
                        return;
                    }
                }

                this.UserId = uid;
            }
        }
    }
}