using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace StrimmTube
{
    public partial class businessPage : BasePage
    {
        public int userId { get; set; }
        public string StudioTutorialVideoId { get; set; }

        public string BulkImportTutorialVideoId { get; set; }

        public string DomainName { get; set; }

        public string btnStartFreeTrial10Url { get;  set; }

        public string btnStartFreeTrial70Url { get; set; }

        public string btnStartFreeTrialCustomUrl { get; set; }

        

        protected void Page_Init(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetAllowResponseInBrowserHistory(false);
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            //// This is needed by PayPal
            //// <form action="https://www.paypal.com/cgi-bin/webscr" method="post" target="_top">
            //HtmlForm myForm = Page.Master.FindControl("form1") as HtmlForm;
            //if (myForm != null)
            //{
            //    myForm.Target = "_top"; // _parent, _self or _top
            //    myForm.Action = "https://www.paypal.com/cgi-bin/webscr";
            //}
            if (Session["userId"] != null)
            {
                btnStartFreeTrial10Url = "location='basic-package'";
                btnStartFreeTrial70Url = "location='standard-package'";
                btnStartFreeTrialCustomUrl = "location='professional-package'";

            }
            else
            {
                btnStartFreeTrial10Url = "loginModal('business_solutions')";
                btnStartFreeTrial70Url = "loginModal('business_solutions')";
                btnStartFreeTrialCustomUrl = "loginModal('business_solutions')";
              
            }
            if (!IsPostBack)
            {
                this.StudioTutorialVideoId = ConfigurationManager.AppSettings["StudioPageTutorialVideoId"];
                this.BulkImportTutorialVideoId = ConfigurationManager.AppSettings["BuilkImportOfVideoTutorialVideoId"];
                this.DomainName = ConfigurationManager.AppSettings["domainName"];

                if (!IsProd)
                {
                    TurnOffCrawling();
                }
            }
        }
    }
}