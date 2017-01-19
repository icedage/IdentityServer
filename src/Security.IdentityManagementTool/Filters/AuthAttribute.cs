using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using RestSharp;

namespace Security.IdentityManagementTool.Filters
{
    public class AuthAttribute : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(System.Web.Mvc.AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                // 403 we know who you are, but you haven't been granted access
                filterContext.Result = new HttpStatusCodeResult(System.Net.HttpStatusCode.Forbidden);
            }
            else
            {
                // 401 who are you? go login and then try again
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }

        public override void OnAuthorization(System.Web.Mvc.AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                var user = filterContext.HttpContext.User as ClaimsPrincipal;
                var token = user.FindFirst("access_token").Value;

                var request = new GatewayAPI.HttpRequestWrapper("http://localhost:18115/api/users", Method.GET);
                var response = request.Execute(token);

                if (!((int)response.StatusCode >= 200 && (int)response.StatusCode <= 299))
                {
                    filterContext.Result = new HttpStatusCodeResult(System.Net.HttpStatusCode.Forbidden);
                }
            }
            else
            {
                // 401 who are you? go login and then try again
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }
    }
}