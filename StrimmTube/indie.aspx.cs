using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StrimmTube
{
    public partial class indie : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["userId"] != null)
                {
                    ancCreateOrSignUp.Attributes.Add("onclick", "CreateChannel.RedirectToCreateChannel()");
                    ancCreateOrSignUp.InnerText = "Create Channel";
                }
                else
                {
                    ancCreateOrSignUp.HRef = "/sign-up";
                    ancCreateOrSignUp.InnerText = "Sign Up. It's Free.";
                }
            }
        }
    }
}