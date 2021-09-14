using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StrimmTube
{
    public partial class Copyrights : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Page.Header.Title = "Strimm - Copyright";

            if (!IsProd)
            {
                TurnOffCrawling();
            }
        }
    }
}