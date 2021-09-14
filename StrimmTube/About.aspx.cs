using log4net;
using System;

namespace StrimmTube
{
    public partial class About : BasePage
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(About));

        protected void Page_Load(object sender, EventArgs e)
        {
            Logger.Info("About page loaded");

            if (!IsProd)
            {
                TurnOffCrawling();
            }

        }

        protected void btnTryNow_Click(object sender, EventArgs e)
        {
            if(Session["userId"] == null)
            {
                Logger.Debug("Redirecting unknow user to sign-up page");
                Response.Redirect("sign-up", false);
                Context.ApplicationInstance.CompleteRequest();
            }
            else
            {
                Logger.Debug(String.Format("Re-directing user with Id={0} to control-panel", Session["userId"].ToString()));
                Response.Redirect("control-panel", false);
                Context.ApplicationInstance.CompleteRequest();
            }
        }
    }
}