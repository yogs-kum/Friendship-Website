using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Presentation.Areas.Security
{
    public class SecurityHomeController : BaseController
    {
        // GET: Security/SecurityHome
        public ActionResult frmSecurityHome()
        {
            return View();
        }
    }
}