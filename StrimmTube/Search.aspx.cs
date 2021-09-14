using Strimm.Model.Criteria;
using StrimmBL;
using Strimm.Model;
using StrimmTube.UC;
using StrimmTube.WebServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StrimmTube
{
    public partial class Search : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsProd)
            {
                TurnOffCrawling();
            }
        }
    }
}