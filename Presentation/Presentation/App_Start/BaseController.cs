using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using Application.Common;
using Application.Presentation;

namespace Presentation
{
    public abstract class BaseController : Controller
    {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            SetUserModulePermissions();
            SetUserFunctionPermissions();
            base.OnActionExecuting(filterContext);
        }

        protected void SetUserModulePermissions()
        {
            /////////Module Authorization by User////////
            DataSet theDS = (DataSet)Session["User"];
            if (theDS != null)
            {
                ViewBag.Administration = AccessManager.HasModuleAcces(ModuleAccess.Administration, theDS.Tables[3]);
                ViewBag.Security = AccessManager.HasModuleAcces(ModuleAccess.Security, theDS.Tables[3]);
            }
            if (Session["User"] == null)
            {
                //throw new Exception("Session Expire. Please Login Again");
            }
            else
            {
                theDS.Dispose();
            }
        }

        protected void SetUserFunctionPermissions()
        {
            ////////Function Authorization by User////////
            DataSet theDS = (DataSet)Session["User"];
            ViewBag.Organization = AccessManager.HasFunctionAccess(appAccess.Organization, theDS.Tables[2]);
            ViewBag.Location = AccessManager.HasFunctionAccess(appAccess.Location, theDS.Tables[2]);
            ViewBag.UserGroup = AccessManager.HasFunctionAccess(appAccess.UserGroup, theDS.Tables[2]);
            ViewBag.UserMaster = AccessManager.HasFunctionAccess(appAccess.UserMaster, theDS.Tables[2]);
            ViewBag.ProfileMaster = AccessManager.HasFunctionAccess(appAccess.ProfileMaster, theDS.Tables[2]);
            
            theDS.Dispose();
            if (Session["UserId"].ToString() == "1")
                ViewBag.RefreshCache = true;
            else
                ViewBag.RefreshCache = false;
        }

    }
}