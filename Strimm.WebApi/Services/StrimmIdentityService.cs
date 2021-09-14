using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace Strimm.WebApi.Services
{
    public class StrimmIdentityService : Strimm.WebApi.Services.IStrimmIdentityService
    {
        public string CurrentUser
        {
            get
            {
                return Thread.CurrentPrincipal.Identity.Name;
            }
        }
    }
}