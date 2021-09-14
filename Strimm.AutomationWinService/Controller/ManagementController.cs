using Strimm.AutomationWinService.Jobs;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Strimm.AutomationWinService.Controller
{
    public class ManagementController : ApiController
    {
        private IJobManager jobManager;

        public ManagementController(IJobManager jobManager)
        {
            this.jobManager = jobManager;
        }

        [Route("api/manager/status")]
        public string GetStatus()
        {
            return "OK";
        }
    }
}
