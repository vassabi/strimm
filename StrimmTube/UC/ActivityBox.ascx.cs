using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StrimmTube.UC
{
    public partial class ActivityBox : System.Web.UI.UserControl
    {
        public string postTime { get; set; }
        public string postImage { get; set; }
        public string post { get; set; }
        public string userName { get; set; }
        public bool isHasAPost { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (isHasAPost==false)
            {
                divImg.Visible = false;
            }
        }

       
    }
}