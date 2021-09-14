using Strimm.Model.Projections;
using StrimmBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StrimmTube.admcp
{
    public partial class UserManagement : System.Web.UI.Page
    {
        public string email { get; set; }
        public bool isRedirected { get; set; }

     
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Request.QueryString["id"]!=null)
            {
                isRedirected = true;
                int userId = int.Parse(Request.QueryString["id"].ToString());
                UserPo userPo =UserManage.GetUserPoByUserIdForAdmin(userId);
                email = UserManage.GetUserPoByUserIdForAdmin(int.Parse(Request.QueryString["id"].ToString())).Email;
            }
            else
            {
                isRedirected = false;
            }
            
        }
    }
}