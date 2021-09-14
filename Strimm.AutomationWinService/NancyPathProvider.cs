using Nancy;
using Strimm.AutomationWinService.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.AutomationWinService
{
    public class NancyPathProvider : IRootPathProvider
    {
        public string GetRootPath()
        {
            return StartupConfig.IsDevelopment()
              ? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..\..\")
              : AppDomain.CurrentDomain.BaseDirectory;
        }
    }
}