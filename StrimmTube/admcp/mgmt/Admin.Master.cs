using Strimm.Model.Projections;
using StrimmBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StrimmTube.admcp.mgmt
{
    public partial class Admin : System.Web.UI.MasterPage
    {
        public string adminName { get; set; }

        public int adminUserId { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userId"] != null)
            {
                UserPo user = UserManage.GetUserPoByUserId(int.Parse(Session["userId"].ToString()));
                adminName = user.FirstName + " " + user.LastName;
                adminUserId = user.UserId;
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }

        protected void btnSignOut_Click(object sender, EventArgs e)
        {
            System.Web.Security.FormsAuthentication.SignOut();
            System.Web.Security.FormsAuthentication.RedirectToLoginPage();
        }
    }

     
}