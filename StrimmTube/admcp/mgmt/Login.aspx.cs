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
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
        {
            e.Authenticated = false;
            string password = Login1.Password.ToString();
            string user = Login1.UserName.ToString();
            UserPo userPo = new UserPo();

            if(Session["userId"]!=null)
            {
                userPo = UserManage.GetUserPoByUserId(int.Parse(Session["userId"].ToString()));
                if(user!=null&&userPo.IsAdmin)
                {
                    e.Authenticated = true;
                }
                else
                {
                    Response.Write("you don't have permission to access admin panel");
                    Session["userId"] = null;
                    return;
                }
            }
            else
            {
                if (UserManage.Login(user, password, out userPo))
                {
                    HttpContext.Current.Session["userId"] = userPo.UserId;

                    if (userPo.IsAdmin)
                    {
                        e.Authenticated = true;
                    }
                    else
                    {
                        Response.Write("you don't have permission to access admin panel");
                        Session["userId"] = null;
                        return;
                    }
                }
            }
           
        }
    }
}