using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using StdBlog.Models;

namespace StdBlog.Controllers
{
    public class m_UserController : Controller
    {
        static private m_UserContext db1 = new m_UserContext();
        public static bool Vertify(string uid, string pw)
        {
            string pws = Helper.AESHelper.Encrypt(pw);
            foreach (var t in db1.m_Users)
            {
                if (t.loginid == uid & t.password == pws) return true;
            }
            return false;
        }
        public static int getID(string uid)
        {
            foreach (var t in db1.m_Users)
                if (t.loginid == uid) return t.ID;
            return -1;
        }
        public static m_User getUserPac(int id)
        {
            if (id == -1) return null;
            foreach (var t in db1.m_Users)
                if (t.ID == id) return t;
            return null;
        }
        private m_UserContext db = new m_UserContext();



        // GET: m_User
        public ActionResult Index()
        {
            return View(db.m_Users.ToList());
        }

        // GET: m_User/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            m_User m_User = db.m_Users.Find(id);
            if (m_User == null)
            {
                return HttpNotFound();
            }
            return View(m_User);
        }

        // GET: m_User/Create
        public ActionResult Create()
        {
            return View();
        }



        // POST: m_User/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,loginid,password,name,gender,follows")] m_User m_User)
        {
            if (ModelState.IsValid)
            {
                m_User.password = Helper.AESHelper.Encrypt(m_User.password);
                db.m_Users.Add(m_User);
                db.SaveChanges();
                return RedirectToAction("UserLog", "Log");
            }

            return View(m_User);
        }

        // GET: m_User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            m_User m_User = db.m_Users.Find(id);
            if (m_User == null)
            {
                return HttpNotFound();
            }
            return View(m_User);
        }

        // POST: m_User/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,loginid,password,name,gender,follows")] m_User m_User)
        {
            if (ModelState.IsValid)
            {
                db.Entry(m_User).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(m_User);
        }

        // GET: m_User/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            m_User m_User = db.m_Users.Find(id);
            if (m_User == null)
            {
                return HttpNotFound();
            }
            return View(m_User);
        }

        // POST: m_User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            m_User m_User = db.m_Users.Find(id);
            db.m_Users.Remove(m_User);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
