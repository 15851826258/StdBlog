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
    public class m_recblogController : Controller
    {
        static bool isExsist(IEnumerable<m_recblog> lis, int blogid)
        {
            foreach (var t in lis)
            {
                if (t.blogid == blogid) return true;
            }
            return false;
        }

        private m_recblogContext db = new m_recblogContext();

        public ActionResult Recommend(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            m_recblog obj = new m_recblog();
            obj.blogid = (int)id;
            obj.pushtime = DateTime.Now;
            obj.deletetime = DateTime.Now.AddDays(7);

            db.m_recblogs.Add(obj);
            db.SaveChanges();


            return RedirectToAction("Home");
        }

        public ActionResult Delrec(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var obj = db.m_recblogs.Find(id);
            db.Entry(obj).State = EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Home");
        }

        public ActionResult Home()
        {
            var rec = db.m_recblogs.ToList();
            var blogs = from t in (new m_BlogContext()).m_Blogs.ToList()
                        where !isExsist(rec, t.ID)
                        select t;
            var recs = from t in rec
                       select m_BlogController.getBlog(t.blogid);
            return View(new m_recblog_blog() { blogs = blogs, recs = recs });
        }

        #region ori
        // GET: m_recblog
        public ActionResult Index()
        {
            return View(db.m_recblogs.ToList());
        }

        // GET: m_recblog/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            m_recblog m_recblog = db.m_recblogs.Find(id);
            if (m_recblog == null)
            {
                return HttpNotFound();
            }
            return View(m_recblog);
        }

        // GET: m_recblog/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: m_recblog/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,pushtime,deletetime,blogid")] m_recblog m_recblog)
        {
            if (ModelState.IsValid)
            {
                db.m_recblogs.Add(m_recblog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(m_recblog);
        }

        // GET: m_recblog/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            m_recblog m_recblog = db.m_recblogs.Find(id);
            if (m_recblog == null)
            {
                return HttpNotFound();
            }
            return View(m_recblog);
        }

        // POST: m_recblog/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,pushtime,deletetime,blogid")] m_recblog m_recblog)
        {
            if (ModelState.IsValid)
            {
                db.Entry(m_recblog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(m_recblog);
        }

        // GET: m_recblog/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            m_recblog m_recblog = db.m_recblogs.Find(id);
            if (m_recblog == null)
            {
                return HttpNotFound();
            }
            return View(m_recblog);
        }

        // POST: m_recblog/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            m_recblog m_recblog = db.m_recblogs.Find(id);
            db.m_recblogs.Remove(m_recblog);
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
