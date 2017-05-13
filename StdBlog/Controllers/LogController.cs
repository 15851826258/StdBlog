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
            //if (Session["id"] != null) return RedirectToAction("UserHome", "m_User");

            if (Request.Cookies["stdblogql"] != null)
            {
                string str = Request.Cookies["stdblogql"].Value;
                if (str.StartsWith("+"))
                {
                    string username = str.Substring(1);
                    var user = m_UserController.getUserPac(username);
                    Session["id"] = user.ID;
                    Session["name"] = user.name;
                    return RedirectToAction("UserHome", "m_User");
                }
            }
            return View();
        }
        public ActionResult UserLogout()
        {
            Response.Cookies["stdblogql"].Value = "";
            Session["id"] = null;
            Session["name"] = null;
            return RedirectToAction("UserLog", "Log");
        }
        public ActionResult regc1()
        {
            return View();
        }
        public ActionResult regc2()
        {
            return RedirectToAction("UserHome", "m_User");
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
            var user = m_UserController.getUserPac(m_UserController.getID(Request["inputEmail"]));
            Session["id"] = user.ID;
            Session["name"] = user.name;
            if (Request["cb"].StartsWith("true")) Response.Cookies.Add(new HttpCookie("stdblogql", "+" + user.name));
            return RedirectToAction("UserHome", "m_User");
        }


        public ActionResult UserReg()
        {
            return View();
        }
    }
}