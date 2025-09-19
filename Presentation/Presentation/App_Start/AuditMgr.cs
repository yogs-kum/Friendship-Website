using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Net.Http;
using Application.Presentation;

namespace Presentation
{
    public sealed class AuditMgr : ActionFilterAttribute
    {
        static AuditMgr _instance;

        public static AuditMgr Instance
        {
            get { return _instance ?? (_instance = new AuditMgr()); }
        }

        private AuditMgr()
        { }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (ConfigurationManager.AppSettings["AuditEvents"].ToString() != "True")
            { return; }
            string theUserId = "0";
            string theUserName = "";
            string theLogOutTime = "1900-01-01";

            if (filterContext.HttpContext.Session["UserId"] != null)
            { theUserId = filterContext.HttpContext.Session["UserId"].ToString(); }

            if (filterContext.HttpContext.Session["UserName"] != null)
            { theUserName = filterContext.HttpContext.Session["UserName"].ToString(); }

            if (filterContext.ActionDescriptor.ActionName == "Logout")
                theLogOutTime = DateTime.UtcNow.ToString();

            //string theMessage = string.Format("UserId:{0}, UserName:{1}, SessionId:{2}, IPAddress:{3}, HostName:{4}, Url:{5}, Controller:{6}, Action:{7}", theUserId, theUserName, HttpContext.Current.Session.SessionID,
            //    filterContext.HttpContext.Request.UserHostAddress, filterContext.HttpContext.Request.UserHostName, filterContext.HttpContext.Request.Url, filterContext.ActionDescriptor.ControllerDescriptor.ControllerName,
            //    filterContext.ActionDescriptor.ActionName);

            //Logger.LogInfo(theMessage);

            var theParameter = new FormUrlEncodedContent(new[]{
                new KeyValuePair<string,string>("theUserId", theUserId),
                new KeyValuePair<string,string>("theUserName",theUserName),
                new KeyValuePair<string,string>("theSessionId", HttpContext.Current.Session.SessionID),
                new KeyValuePair<string,string>("theIPAddress",filterContext.HttpContext.Request.UserHostAddress),
                new KeyValuePair<string,string>("theComputerName",filterContext.HttpContext.Request.UserHostName),
                new KeyValuePair<string,string>("thePageAccessed",filterContext.HttpContext.Request.Url.ToString()),
                new KeyValuePair<string,string>("theControllerName",filterContext.ActionDescriptor.ControllerDescriptor.ControllerName),
                new KeyValuePair<string,string>("theActionName",filterContext.ActionDescriptor.ActionName),
                new KeyValuePair<string,string>("theActionParameter",""),
                new KeyValuePair<string,string>("theLogOutTime",theLogOutTime) });

            var theResult = ObjectFactory.PostOnServerAsync("Administration/SystemUtility/SaveApplicationLog", theParameter, "SystemUtilityController", filterContext.HttpContext);


        }

    }
}