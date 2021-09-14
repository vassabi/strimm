using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace StrimmTube
{
    public partial class business_features : System.Web.UI.Page
    {
        public string DomainName { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.DomainName = ConfigurationManager.AppSettings["domainName"];

                
            }
        }
    }
}