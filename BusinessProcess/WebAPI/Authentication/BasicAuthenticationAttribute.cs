using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;
using System.Web.Http.Controllers;
using System.Threading;
using System.Threading.Tasks;
using System.Security.Principal;
using System.Text;
using BusinessProcess.Security;


namespace WebAPI
{
    public sealed class BasicAuthenticationAttribute:AuthorizationFilterAttribute
    {
        static BasicAuthenticationAttribute _instance;

        public static BasicAuthenticationAttribute Instance
        {
            get { return _instance ?? (_instance = new BasicAuthenticationAttribute()); }
        }

        private BasicAuthenticationAttribute()
        { }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if(actionContext.Request.Headers.Authorization!=null)
            {
                var theAuthToken = actionContext.Request.Headers.Authorization.Parameter;
                var theToken = Encoding.UTF8.GetString(Convert.FromBase64String(theAuthToken));
                var theUserCredentials = theToken.Split(':');
                if(IsAuthorized(theUserCredentials[0],theUserCredentials[1],theUserCredentials[2])==true)
                {
                    Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(theUserCredentials[0]), null);
                }
                else
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                }
            }
            else
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
        }

        public static bool IsAuthorized(string theUserId, string thePassword, string theController)
        {
            BLogin theAuthenticationManager = new BLogin();
            if (theAuthenticationManager.isAPIUserHasAccess(theUserId, thePassword, theController) == true)
                return true;
            else
                return false;
        }
    }
}