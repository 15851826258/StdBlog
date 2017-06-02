using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StdBlog.Controllers
{
    public class UmeditorController : Controller
    {
        static bool c = false;
        public ActionResult Editor()
        {
            return View();
        }

        public ActionResult Exc()
        {
            m_BlogController.createBlog(Request.Unvalidated.Form["info"], (int)Session["id"], Request["title"]);
            return RedirectToAction("UserHome", "m_User");
        }

        public ActionResult tev()
        {
            return View();
        }
        public ActionResult tevexc()
        {
            return View();
        }
    }
}