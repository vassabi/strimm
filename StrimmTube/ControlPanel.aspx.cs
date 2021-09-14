using StrimmBL;
using Strimm.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace StrimmTube
{
    public partial class ControlPanel : BasePage
    {
        public string boardUrl { get; set; }
        public int userId { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsProd)
            {
                TurnOffCrawling();
            }
            
            if (Session["userId"] != null)
            {
                User user = StrimmBL.UserManage.GetUserPoByUserId(int.Parse(Session["userId"].ToString()));
                if (String.IsNullOrEmpty(user.UserName))
                {
                    boardUrl = "profile";

                }
                else
                {
                    boardUrl = "board/" + user.UserName.Trim(' ');
                }

                userId = int.Parse(Session["userId"].ToString());
            }
        }
    }
}