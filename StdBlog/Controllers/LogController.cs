using StdBlog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace StdBlog.Controllers
{
    public class LogController : Controller
    {

        public ActionResult AdminLog()
        {
            return View();
        }

        public ActionResult UserLog()
        {
            return View();
        }

        public ActionResult UserLogA()
        {
            return View();
        }

        public ActionResult ALC()
        {
            return View();
        }

        public ActionResult ULC()
        {
            var bol = m_UserController.Vertify(Request["inputEmail"], Request["inputPassword"]);
            if (!bol) return RedirectToAction("UserLogA", "Log");
            Session["id"] = m_UserController.getID(Request["inputEmail"]);
            return View();
        }


        public ActionResult UserReg()
        {
            return View();
        }
    }
}