using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NDFriends_New1.Controllers
{
    public class HomeController : Controller
    {
        //[Route("Index")]
        public ActionResult Index()
        {
            return View();
        }

        [Route("about-the-creators")]
        [ActionName("about-the-creators")]
        public ActionResult aboutTheCreators()
        {
            return View();
        }

        [Route("my-profile")]
        [ActionName("my-profile")]
        public ActionResult myProfile()
        {
            return View();
        }

        [Route("chat")]
        [ActionName("chat")]
        public ActionResult chat()
        {
            return View();
        }

        [Route("edit-profile")]
        [ActionName("edit-profile")]
        public ActionResult editProfile()
        {
            return View();
        }

        [Route("my-matches")]
        [ActionName("my-matches")]
        public ActionResult myMatches()
        {
            return View();
        }

    }
}