using Microsoft.Owin;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Newtonsoft.Json.Serialization;
using Owin;
using System;
using System.Configuration;
using System.Net.Http.Formatting;
using System.Web.Http;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;

namespace Strimm.AutomationWinService.Config
{
    public class StartupConfig
    {
        // add an extra parameter of type IKernel
        public void Configuration(IAppBuilder appBuilder)
        {
            UseFileServer(appBuilder);
            UseWebApi(appBuilder);

            appBuilder.UseNancy(options => options.Bootstrapper = new NancyBootstrapper());
        }

        private static void UseFileServer(IAppBuilder application)
        {
            application.UseFileServer(new FileServerOptions
            {
                FileSystem = new PhysicalFileSystem(IsDevelopment() ? "../../static" : "static"),
                RequestPath = new PathString("/static")
            });
        }

        private void UseWebApi(IAppBuilder application)
        {
            var config = new HttpConfiguration();

            config.MapHttpAttributeRoutes();
            config.MapDefinedRoutes();

            config.Formatters.Clear();
            config.Formatters.Add(new JsonMediaTypeFormatter());

            var json = config.Formatters.JsonFormatter;

            json.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Utc;
            json.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
            json.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            json.SerializerSettings.DateFormatHandling = Newtonsoft.Json.DateFormatHandling.MicrosoftDateFormat;

            application.UseNinjectWebApi(config);
        }

        public static bool IsDevelopment()
        {
            return ConfigurationManager.AppSettings["DeployedInDev"] != null 
                        ? Boolean.Parse(ConfigurationManager.AppSettings["DeployedInDev"].ToString()) 
                        : false;
        }
    }
}
