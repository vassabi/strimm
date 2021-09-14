using Strimm.Model;
using Strimm.Model.Projections;
using Strimm.Model.WebModel;
using StrimmBL;
using StrimmTube.UC;
using StrimmTube.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
namespace StrimmTube
{
    public partial class Strimm : System.Web.UI.MasterPage
    {
        public int userId { get; set; }

        public string userName { get; set; }

        public string channelUrl;

        public string name { get; set; }

        public string imageUrl { get; set; }

        public int year { get; set; }
    
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.AppendHeader("Access-Control-Allow-Origin", "*");
            System.Web.UI.WebControls.Literal lit = new System.Web.UI.WebControls.Literal();
            lit.Text = System.Web.Optimization.Styles.Render("~/bundles/master/css").ToHtmlString();

            Page.Header.Controls.AddAt(5, lit);

            DateTime date = DateTime.Now;
            year = date.Year;

            System.Web.UI.WebControls.Literal lit1 = new System.Web.UI.WebControls.Literal();
            lit1.Text = System.Web.Optimization.Scripts.Render("~/bundles/master/js").ToHtmlString();
            Page.Header.Controls.AddAt(6, lit1);

            if (Page is channelPageNew)
            {
                System.Web.UI.WebControls.Literal lit2 = new System.Web.UI.WebControls.Literal();
                System.Web.UI.WebControls.Literal lit3 = new System.Web.UI.WebControls.Literal();
                bool isFlowplayerEnable = bool.Parse(ConfigurationManager.AppSettings["FlowplayerEnable"].ToString());
                if (isFlowplayerEnable)
                {
                    lit2.Text = System.Web.Optimization.Styles.Render("~/bundles/channelpageNewFP/css").ToHtmlString();
                    lit3.Text = System.Web.Optimization.Scripts.Render("~/bundles/channelPageFP/js").ToHtmlString();
                }
                else
                {
                    lit2.Text = System.Web.Optimization.Styles.Render("~/bundles/channelpageNew/css").ToHtmlString();
                    lit3.Text = System.Web.Optimization.Scripts.Render("~/bundles/channelPage/js").ToHtmlString();
                }
                Page.Header.Controls.AddAt(7, lit2);
                Page.Header.Controls.AddAt(8, lit3);
            }
            if(Page is SignUp)
            {
                form1.Attributes.Add("autocomplete", "off");
            }
          
            Session["ClientTime"] = WebUtils.GetClientTimeFromCookie(Request.Cookies);

            if (Session["userId"] == null)
            {
                Session["userId"] = WebUtils.GetUserIdFromCookie(Request.Cookies);
            }

            int id = 0;

            if (Session["userId"] != null && Int32.TryParse(Session["userId"].ToString(), out id))
            {
                userId = id;
                var userPo = UserManage.GetUserPoByUserIdForAdmin(userId);
                bool isUserDeleted = true;
                var location = WebUtils.GetUserLocationFromCookie();

                if(userPo!=null)
                {
                    isUserDeleted = userPo.IsDeleted;
                    UserManage.UpdateUserLastKnownLocationByUsername(location, userPo.UserName);
                }
    
                if(isUserDeleted)
                {
                    TopMenuBeforeLogin menuBeforeLoginCtrl = (TopMenuBeforeLogin)LoadControl("~/UC/TopMenuBeforeLogin.ascx");
                    topMenu.Controls.Add(menuBeforeLoginCtrl);
                }
                else
                {
                    TopMenuAfterLogin menuAfterLoginCtrl = (TopMenuAfterLogin)LoadControl("~/UC/TopMenuAfterLogin.ascx");
                    topMenu.Controls.Add(menuAfterLoginCtrl);
                }
               
            }
            else
            {
                //load menu before login
                TopMenuBeforeLogin menuBeforeLoginCtrl = (TopMenuBeforeLogin)LoadControl("~/UC/TopMenuBeforeLogin.ascx");
                topMenu.Controls.Add(menuBeforeLoginCtrl);
            }

            //if (Page is channelPageNew)
            //{
            //    int activeCategoryId = 0;

            //    if (Request.QueryString["category"] != null)
            //    {
            //        var allCategories = ReferenceDataManage.GetAllCategories();
            //        var activeCategory = allCategories.FirstOrDefault(x => x.Name.ToLower().Contains(Request.QueryString["category"].ToLower()));
            //        activeCategoryId = (activeCategory != null) ? activeCategory.CategoryId : 9999;
            //    }

            //    if (activeCategoryId == 0 && (Page.RouteData.Values["ChannelOwnerUserName"] == null || Page.RouteData.Values["ChannelURL"] == null))
            //    {
            //        Response.Redirect("/home", false);
            //        Context.ApplicationInstance.CompleteRequest();
            //        return;
            //    }
            //}
            //else
            //{
                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallVisitorFunction", "AddVisitor();", true);
            //}
           
        }
    }
}
