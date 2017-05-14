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
    public class m_AdminController : Controller
    {
        #region api
        static m_AdminController()
        {
            m_AdminContext db = new m_AdminContext();
            if (db.m_Admin.Count() == 0)
            {
                var obj = new m_Admin();
                obj.loginid = "admin@stdblog.com";
                obj.password = Helper.AESHelper.Encrypt("admin");
                obj.name = "Tsual Gwenix";
                db.m_Admin.Add(obj);
                db.SaveChanges();
            }
        }
        public static bool Vertify(string uid, string pw)
        {
            m_AdminContext db = new m_AdminContext();
            string pws = Helper.AESHelper.Encrypt(pw);
            foreach (var t in db.m_Admin)
            {
                if (t.loginid == uid & t.password == pws) return true;
            }
            return false;
        }
        public static m_Admin getAdminPac(int id)
        {
            m_AdminContext db = new m_AdminContext();
            foreach (var t in db.m_Admin)
                if (t.ID == id) return t;
            return null;
        }
        public static m_Admin getAdminPac(string loginid)
        {
            m_AdminContext db = new m_AdminContext();
            foreach (var t in db.m_Admin)
                if (t.loginid == loginid)
                    return t;
            return null;
        }
        #endregion
        private m_AdminContext db = new m_AdminContext();

        public ActionResult AdminHome()
        {
            ViewData.Add("usercount", "" + (new m_UserContext()).m_Users.ToList().Count());
            ViewData.Add("blogcount", "" + (new m_BlogContext()).m_Blogs.ToList().Count());
            return View();
        }



        #region ori
        // GET: m_Admin
        public ActionResult Index()
        {
            return View(db.m_Admin.ToList());
        }

        // GET: m_Admin/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            m_Admin m_Admin = db.m_Admin.Find(id);
            if (m_Admin == null)
            {
                return HttpNotFound();
            }
            return View(m_Admin);
        }

        // GET: m_Admin/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: m_Admin/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,loginid,password,name")] m_Admin m_Admin)
        {
            if (ModelState.IsValid)
            {
                db.m_Admin.Add(m_Admin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(m_Admin);
        }

        // GET: m_Admin/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            m_Admin m_Admin = db.m_Admin.Find(id);
            if (m_Admin == null)
            {
                return HttpNotFound();
            }
            return View(m_Admin);
        }

        // POST: m_Admin/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,loginid,password,name")] m_Admin m_Admin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(m_Admin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(m_Admin);
        }

        // GET: m_Admin/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            m_Admin m_Admin = db.m_Admin.Find(id);
            if (m_Admin == null)
            {
                return HttpNotFound();
            }
            return View(m_Admin);
        }

        // POST: m_Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            m_Admin m_Admin = db.m_Admin.Find(id);
            db.m_Admin.Remove(m_Admin);
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
        #endregion
    }
}
