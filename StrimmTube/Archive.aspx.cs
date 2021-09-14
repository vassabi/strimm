using StrimmBL;
using StrimmTube.UC;
using StrimmTube.WebServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Strimm.Model;
using Strimm.Model.Projections;
using log4net;
using Strimm.Shared;
using Strimm.Model.WebModel;

namespace StrimmTube
{
    public partial class Archive : BasePage
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(Archive));

        public int UserId { get; set; }

        private int ArchiveId { get; set; }

        public string userName { get; set; }

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
            Logger.Info("Archive page loaded");

            int userId = 0;

            if (!IsProd)
            {
                TurnOffCrawling();
            }

            if (Session["userId"] != null)
            {
                Int32.TryParse(Session["userId"].ToString(), out userId);

                userName = Page.RouteData.Values["UserName"].ToString();
                UserPo user = UserManage.GetUserByPublicUrl(userName);
                if (userId != user.UserId)
                {
                    Response.Redirect("/home", false);
                    Context.ApplicationInstance.CompleteRequest();
                    return;
                }
                if (userId > 0)
                {
                    AddTitle(String.Format("{0} Video Archive", user.UserName), false);
                    Logger.Debug(String.Format("Retrieved user Id={0}", userId));
                    UserId = int.Parse(Session["userId"].ToString());
                }
                else
                {
                    Logger.Debug("Redirecting unknown user to the home page");
                    Response.Redirect("/home", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
            }
            else
            {
                Logger.Debug("Redirecting unknown user to the home page");
                Response.Redirect("/home", false);
                Context.ApplicationInstance.CompleteRequest();
            }
        }

    }
}