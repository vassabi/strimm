using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StrimmTube.UC
{
    public partial class SearchedBoardUC : System.Web.UI.UserControl
    {
        public string boardHref { get; set; }
        public string userName { get; set; }
        public string userImage { get; set; }
        public string boardTitle { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}