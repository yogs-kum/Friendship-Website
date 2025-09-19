using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Presentation
{
    public sealed class ExceptionMgr : HandleErrorAttribute
    {
        static ExceptionMgr _Instance;

        public static ExceptionMgr Instance
        {
            get { return _Instance ?? (_Instance = new ExceptionMgr()); }
        }

        private ExceptionMgr()
        { }

        public override void OnException(ExceptionContext filterContext)
        {
            var controllerName = filterContext.RouteData.Values["controller"];
            var actionName = filterContext.RouteData.Values["action"];

            if (filterContext.HttpContext.Session["User"] == null || filterContext.HttpContext.Session["UserId"] == null || filterContext.HttpContext.Session["UserId"].ToString() == "" || filterContext.HttpContext.Session["UserId"].ToString() == "0")
            {
                filterContext.HttpContext.Response.Clear();
                filterContext.HttpContext.Response.Redirect("~/Login/frmLogin?error=Your session expired. Please Login Again!");
            }
            else
            {
                Exception err = filterContext.Exception;
                filterContext.ExceptionHandled = true;
                var theResult = new ViewResult();
                theResult.ViewName = "frmError";
                Logger.LogError(string.Format("Error Description:{0}", err.Message + err.StackTrace));
                theResult.ViewBag.Error = "Message:" + err.Message + "\r\n" + err.InnerException + "\r\n" + err.StackTrace;
                filterContext.Result = theResult;
            }
        }
    }
}