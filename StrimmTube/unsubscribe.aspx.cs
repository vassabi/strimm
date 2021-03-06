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
    public partial class unsubscribe : BasePage
    {
        public int UserId { get; set; }

        public string UserEmail { get; set; }

        public string UserName { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsProd)
            {
                TurnOffCrawling();
            }

            UserPo user = null;

            if (Session["userId"] != null)
            {
                int id = 0;
                if (Int32.TryParse(Session["userId"].ToString(), out id))
                {
                    UserId = id;
                    user = UserManage.GetUserPoByUserId(UserId);

                    if (user != null)
                    {
                        UserEmail = user.Email;
                        UserName = user.UserName;
                    }
                }
            }

            if (user == null && Page.RouteData.Values["UserEmail"] != null)
            {
                UserEmail = Page.RouteData.Values["UserEmail"] == null ? String.Empty : Page.RouteData.Values["UserEmail"].ToString();

                user = UserManage.GetUserByEmail(UserEmail);

                if (user != null)
                {
                    UserId = user.UserId;
                    UserName = user.UserName;

                    Session["UserEmail"] = UserEmail;
                }
                else
                {
                    Response.Redirect("/home", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
            }

        }
    }
}