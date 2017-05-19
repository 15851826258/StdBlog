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
            ViewData.Add(nameof(c), c);
            return View();
        }
        public ActionResult tevexc(int? id)
        {
            if (id == 0) c = !c;
            if (c)
                return Content("<button class=\"btn btn-default\" type=\"submit\"><span class=\"glyphicon glyphicon-heart\" aria-hidden=\"true\"></span></button>");
            else
                return Content("<button class=\"btn btn-default\" type=\"submit\"><span class=\"glyphicon glyphicon-heart-empty\" aria-hidden=\"true\"></span></button>");
        }
    }
}