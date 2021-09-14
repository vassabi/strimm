using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StrimmTube.UC
{
    public partial class FollowerUC : System.Web.UI.UserControl
    {
        public string followerBoardUrl { get; set; }
        public string imSrc { get; set; }
        public string followerTitle { get; set; }
        public string followerId { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}