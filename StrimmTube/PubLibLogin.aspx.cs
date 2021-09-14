using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StrimmTube
{
    public partial class PubLibLogin : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsProd)
            {
                TurnOffCrawling();
            }
        }

        protected void ancLogin_Click(object sender, EventArgs e)
        {
            string username = txtUserName.Text;
            string password = txtPassword.Text;

            if (String.IsNullOrEmpty(txtUserName.Text) || String.IsNullOrEmpty(txtPassword.Text))
            {
                spanMessage.Text = "please enter username and password";
            }
            else
            {
                if (username == "admin" && password == "Bilbup0625!")
                {
                    Session["publib"] = true;
                    Response.Redirect("PublicLIbraryAdmin.aspx", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
                else
                {
                    spanMessage.Text = "username or password not correct";
                }
            }
            
        }
    }
}