using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Presentation.Areas.Administration.Controllers
{
    public class AdminHomeController : BaseController
    {
        // GET: Administration/AdminHome
        public ActionResult frmAdminHome()
        {
            return View();
        }
    }
}