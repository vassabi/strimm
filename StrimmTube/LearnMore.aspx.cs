using StrimmTube.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StrimmTube
{
    public partial class LearnMore : BasePage
    {
        public bool isUserLogedIn { get; set; }
        public string promoVideoUrl { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsProd)
            {
                TurnOffCrawling();
            }
           
            int id = 0;

            if (Session["userId"] != null && Int32.TryParse(Session["userId"].ToString(), out id))
            {
                isUserLogedIn = true;
            }
            else
            {
                isUserLogedIn = false;
            }
            if (!Page.IsPostBack)
            {
                promoVideoUrl = ConfigurationManager.AppSettings["PromoVideoId"];
            }
        }
    }
}