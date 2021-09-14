using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StrimmTube
{
    public partial class PaswordRecovery : BasePage
    {
        public string etoken { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsProd)
            {
                TurnOffCrawling();
            }

            if (Request.QueryString["token"] != null)
            {
                etoken = Request.QueryString["token"];
            }           
        }
    }
}