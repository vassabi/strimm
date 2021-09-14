using log4net;
using StrimmTube.App_Start;
using System;
using System.Configuration;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;

namespace StrimmTube
{
    public class Global : System.Web.HttpApplication
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(Global));

        protected void Application_Start(object sender, EventArgs e)
        {
            RouteConfig.RegisterRoutes(RouteTable.Routes);
              
            //var log4NetPath = Server.MapPath("~/log4net.config");
            log4net.Config.XmlConfigurator.Configure();// AndWatch(new System.IO.FileInfo(log4NetPath));

            log4net.ThreadContext.Properties["userId"] = 1;
            log4net.ThreadContext.Properties["applicationComponent"] = "strimm";

            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Session_Start(object sender, EventArgs e)
        {
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            bool prevenFormsRedirect = Boolean.Parse(ConfigurationManager.AppSettings["prevent-forms-redirect"]);

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest" && prevenFormsRedirect)
            {
                Response.SuppressFormsAuthenticationRedirect = true;
            }
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
        }

        protected void Application_Error(object sender, EventArgs e)
        {
             string url = HttpContext.Current.Request.Url.ToString();
            string error = Server.GetLastError().ToString();
            string userIp = getUserIP();           

            Logger.Error(String.Format("Application level error occured. UserId={0}, Request URL='{1}'. Error details: {2}", userIp, url, error));
        }

        protected void Session_End(object sender, EventArgs e)
        {
        }

        protected void Application_End(object sender, EventArgs e)
        {
            
        }

        public string getUserIP()
        {
            string clientIP = (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] == null) ?
                              HttpContext.Current.Request.UserHostAddress : HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            return clientIP;
            //Dns.GetHostAddresses(Dns.GetHostName())
        }
    }
}