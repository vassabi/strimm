using Strimm.Shared;
using StrimmBL;
using Strimm.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Strimm.Model.Projections;
using log4net;

namespace StrimmTube
{
    public partial class Confirmation : BasePage
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(Confirmation));

        public String UserName { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            string etoken    = Request.QueryString["token"];
            string email     = String.Empty;
            //string password = String.Empty;

            if (!IsProd)
            {
                TurnOffCrawling();
            }

            UserPo user = null;

            try
            {
                if (!String.IsNullOrEmpty(etoken.Trim()))
                {
                    string token = MiscUtils.DecodeFrom64(etoken);
                    string[] parts = token.Split(':');
                    email = parts[0];
                    //password = parts[1];

                    Logger.Debug(String.Format("Processing e-mail confirmation request for user with e-mail '{0}'", email));

                    user = UserManage.GetUserPoByEmail(email);// GetUserPoByLoginIdentifierAndPassword(email, password);

                    if (user != null)
                    {
                        this.UserName = user.UserName;

                        if (user.IsTempUser)
                        {
                            UserManage.ConfirmUser(user.UserId);

                            Logger.Debug(String.Format("User with e-mail '{0}' was successfully confirmed", user.Email));

                            //Session["userId"] = user.UserId;
                            //Response.Redirect("welcome-to-strimm", false);
                            //Context.ApplicationInstance.CompleteRequest();
                        }
                        else
                        {
                            Logger.Debug(String.Format("User with e-mail '{0}' was previously confirmed. Redirecting to home page", user.Email));

                            //Session["userId"] = user.UserId;
                            //Response.Redirect("welcome-to-strimm", false);
                            //Context.ApplicationInstance.CompleteRequest();
                            Response.Redirect("/home", false);
                            Context.ApplicationInstance.CompleteRequest();
                        }
                    }
                    else
                    {
                        Logger.Debug(String.Format("Failed to retrieve existing user record for e-mail '{0}'.Redirecting to home page", email));
                        Response.Redirect("/home", false);
                        Context.ApplicationInstance.CompleteRequest();
                    }
                }
                else
                {
                    Logger.Debug("Failed to retrieve user's email in order to confirm it. Redirecting to home page");
                    Response.Redirect("/home", false);
                    Context.ApplicationInstance.CompleteRequest();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(String.Format("Error occured while confirming user with Id={0}. Redirecting to home page", user.UserId), ex);
                Response.Redirect("/home", false);
                Context.ApplicationInstance.CompleteRequest();
            }
        }
    }
}