using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BusinessProcess.Administration;
//using BusinessProcess.Chat;
using BusinessProcess.Profile;
using BusinessProcess.Security;
using Interface.Administration;
//using Interface.Chat;
using Interface.Profile;
using Interface.Security;
using System.Web.Http;
using Unity;
using WebAPI.Models;

namespace WebAPI.Configuration
{
    public static class BProcessAPI
    {
        public static void Register(HttpConfiguration theConfig)
        {

            var theContainer = new UnityContainer();
            ////////////Security///////////////
            theContainer.RegisterType<ILogin, BLogin>();
            theContainer.RegisterType<IUser, BUser>();

            ////////////Administration/////////
            theContainer.RegisterType<ISystemUtility, BSystemUtility>();
            theContainer.RegisterType<IOrganization, BOrganization>();
            theContainer.RegisterType<ILocation, BLocation>();
            theContainer.RegisterType<IProfile, BProfile>();
            //theContainer.RegisterType<IChat, BChat>();

            theConfig.Filters.Add(BasicAuthenticationAttribute.Instance);
            theConfig.DependencyResolver = new UnityResolver(theContainer);
            theConfig.MapHttpAttributeRoutes();
            theConfig.Routes.MapHttpRoute(
                name: "BProcessAPI",   ///Name of API///
                routeTemplate: "BProcessApi/{path}/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional });

        }
    }
}