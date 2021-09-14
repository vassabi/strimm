﻿using System;
using Ninject.Modules;
using Quartz;
using Topshelf;
using Topshelf.Ninject;
using Topshelf.Quartz;
using Topshelf.Quartz.Ninject;
using System.Configuration;
using Strimm.AutomationWinService.Modules.IoC;
using log4net;

namespace Strimm.AutomationWinService
{
    public static class Program
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(Program));

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        private static int Main()
        {
            string log4netConfig        = ConfigurationManager.AppSettings["log4netConfig"];
            string serviceName          = ConfigurationManager.AppSettings["ServiceName"];
            string serviceDisplayName   = ConfigurationManager.AppSettings["ServiceDisplayName"];
            string serviceDescription   = ConfigurationManager.AppSettings["ServiceDescription"];

            log4net.Config.XmlConfigurator.Configure();// ConfigureAndWatch(new System.IO.FileInfo(log4netConfig));

            Logger.Info("Running HostFactory");

            TopshelfExitCode exitCode = TopshelfExitCode.AbnormalExit;

            try
            {
                exitCode = HostFactory.Run(host =>
                {
                    host.UseLog4Net();//log4netConfig);
                    Logger.Info("Using log4net...");

                    host.EnablePauseAndContinue();
                    Logger.Info("Enabled Pause and Continue...");

                    host.EnableShutdown();
                    Logger.Info("Enabled Shutdown...");

                    var configurator = host.UseNinject(new JobServiceModule());
                    Logger.Info("Created configurator...");

                    host.Service<JobsService>(service =>
                    {
                        service.ConstructUsingNinject();
                        Logger.Debug("Constructed service using Ninject");

                        service.UseQuartzNinject();
                        Logger.Debug("Using Quartz for job scheduling");

                        service.WhenStarted((s, control) => {
                            Logger.Debug("Starting service....");
                            return s.Start();
                        });

                        service.WhenStopped((s, control) => s.Stop());
                        Logger.Debug("Done configuring JobService");
                    });

                    host.SetDescription(serviceDescription);
                    Logger.Info("Set description...");

                    host.SetDisplayName(serviceDisplayName);
                    Logger.Info("Set display name...");

                    host.SetServiceName(serviceName);
                    Logger.Info("Set service name...");

                    host.RunAsNetworkService();
                    Logger.Info("Running as network service...");
                });
            }
            catch (Exception ex)
            {
                Logger.Error("Error occured while starting JAS service", ex);
            }

            Logger.Debug(String.Format("Topshelf exit code: {0}", exitCode));

            return (int)exitCode;
        }
    }
}
