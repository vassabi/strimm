using StrimmBL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StrimmTube
{
    public partial class ProfessionalPackage : System.Web.UI.Page
    {
        public string userId { get; set; }

        public string subscriptionId { get; set; }

        public string payPalServiceUrl { get; set; }

        public bool isAnnual { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int userId = 0;
                int priorOrderCount = 0;

                if (Session["UserId"] != null)
                {
                    userId = Convert.ToInt32(Session["UserId"].ToString());

                    if (userId > 0)
                    {
                        priorOrderCount = OrderManager.GetPriorOrderCountByUserId(userId);
                    }

                    if (Request.QueryString["isAnnual"] != null)
                    {
                        isAnnual = Boolean.Parse(Request.QueryString["isAnnual"]);
                    }

                    //subscriptionId = ConfigurationManager.AppSettings["BasicSubscriptionButtonId"].ToString();

                    payPalServiceUrl = ConfigurationManager.AppSettings["PayPalUrl"].ToString();
                }
            }
        }
    }
}