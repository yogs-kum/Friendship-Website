using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using Application.Presentation;

namespace Presentation.Controllers
{
    public class AppHomeController : BaseController
    {
        // GET: AppHome
        public ActionResult frmAppHome()
        {
            Init_Form();
            return View();
        }

        private void Init_Form()
        {
            clsBindFunctions theBindManager = new clsBindFunctions();
            DataSet theDS = (DataSet)Session["User"];
            ViewData["UserLocation"] = theBindManager.DropDownBindingList(theDS.Tables[1]);
        }

        [HttpPost]
        public ActionResult Form_Post(FormCollection theColl)
        {
            string theLoc = theColl["theLocation"].ToString();
            Session.Add("LocationId", theLoc);
            //Init_Form();
            return RedirectToAction("frmAppHome","AppHome");
        }
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("frmLogin", "Login");
        }

        public ActionResult RefreshSystemCache()
        {
            CacheMgr.ClearCache();
            return View("frmAppHome");
        }
    }
}