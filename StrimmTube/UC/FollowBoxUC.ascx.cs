using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StrimmTube.UC
{
    public partial class FollowBoxUC : System.Web.UI.UserControl
    {
        public string userBoardUrl { get; set; }
        public string userName { get; set; }
        public string avatarUrl { get; set; }
        public bool isMyFollowers { get; set; }
        public int followerId { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (isMyFollowers == true)
            {
                divActions.Visible = true;
            }
            else
            {
                divActions.Visible = false;
            }
        }
    }
}