using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StdBlog.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("userlog","log");
        }

        public ActionResult Return()
        {
            return View();
        }

        public ActionResult Pg_wb()
        {
            return View();
        }

        public ActionResult Pg_dnc()
        {
            return View();
        }
    }
}