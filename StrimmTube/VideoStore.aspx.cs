using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StrimmTube
{
    public partial class VideoStore : BasePage
    {
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

                this.UserId = uid;
            }
        }
    }
}