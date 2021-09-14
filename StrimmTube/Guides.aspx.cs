using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StrimmTube
{
    public partial class Guides : BasePage
    {
        public string howToPageTutorialVideoId { get; set; }

        public string studioPageTutorialVideoId { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsProd)
            {
                TurnOffCrawling();
            }

            howToPageTutorialVideoId = ConfigurationManager.AppSettings["NetworkPageTutorialVideoId"];
            studioPageTutorialVideoId = ConfigurationManager.AppSettings["StudioPageTutorialVideoId"];
        }

    }
}