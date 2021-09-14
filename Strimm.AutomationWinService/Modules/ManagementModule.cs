using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strimm.AutomationWinService.Modules
{
    public class ManagementModule : NancyModule
    {
        public ManagementModule()
        {
            Get["/"] = parameters => { return View["views/index"]; };
        }
    }
}
