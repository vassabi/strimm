using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Net.Http;
using System.Text;
using System.Security.Principal;
using System.Threading;
using StrimmBL;
using Strimm.Shared;
using Strimm.Model.Projections;

namespace Strimm.WebApi.Filters
{
    public class StrimmAuthorizeAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (Thread.CurrentPrincipal.Identity.IsAuthenticated)
            {
                return;
            }

            var authHeader = actionContext.Request.Headers.Authorization;
            if (authHeader != null)
            {
                if (authHeader.Scheme.Equals("basic", StringComparison.OrdinalIgnoreCase) &&
                    !string.IsNullOrWhiteSpace(authHeader.Parameter))
                {
                    var rawCredentials = authHeader.Parameter; // base64 encoded string
                    var encoding = Encoding.GetEncoding("iso-8859-1");
                    var credentials = encoding.GetString(Convert.FromBase64String(rawCredentials));
                    var split = credentials.Split(':');
                    var username = split[0];
                    var password = split[1];

                    UserPo user = null;

                    if (UserManage.Login(username, password, out user))
                    {
                        var principal = new GenericPrincipal(new GenericIdentity(username), null); // don't need to specify list of roles for right now
                        Thread.CurrentPrincipal = principal;

                        return;
                    }
                }
            }

            return;

            //HandleUnathorized(actionContext);
        }

        private void HandleUnathorized(HttpActionContext actionContext)
        {
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            actionContext.Response.Headers.Add("WWW-Authenticate", "Basic Scheme='Strimm' location='http://localhost:4002/account/login'"); //compute based on request uri
        }
    }
}