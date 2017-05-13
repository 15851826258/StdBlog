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
    public class m_BlogController : Controller
    {
        private m_BlogContext db = new m_BlogContext();


        #region APIS
        static IEnumerable<m_sensitivekeyword> skys = (new m_sensitivekeywordContext()).m_sensitivekeywords.ToList();
        private static string filter(string content)
        {
            foreach (var t in skys)
                content.Replace(t.key, getcs(t.key.Length));
            return content;
        }
        private static string getcs(int count)
        {
            string res = "";
            while (count-- > 0) res += "*";
            return res;
        }
        public static void createBlog(string BlogContent, int OwnerID, string Title)
        {
            m_BlogContext db1 = new m_BlogContext();
            db1.m_Blogs.Add(new m_Blog()
            {
                ownerid = OwnerID,
                title = Title,
                content = filter(BlogContent),
                classify = "",
                last_modify_time = DateTime.Now,
                visit_count = 0
            });
            db1.SaveChanges();
        }
        public static string getBlogContent(int BlogID)
        {
            m_BlogContext db1 = new m_BlogContext();
            foreach (var t in db1.m_Blogs)
            {
                if (t.ID == BlogID) return t.content;
            }
            return "";
        }
        public static m_Blog getBlog(int BlogID)
        {
            m_BlogContext db1 = new m_BlogContext();
            foreach (var t in db1.m_Blogs)
            {
                if (t.ID == BlogID) return t;
            }
            return null;
        }

        #endregion

        #region Ctrl funcs

        #region ori
        // GET: m_Blog
        public ActionResult Index()
        {
            return View(db.m_Blogs.ToList());
        }

        // GET: m_Blog/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            m_Blog m_Blog = db.m_Blogs.Find(id);
            if (m_Blog == null)
            {
                return HttpNotFound();
            }
            return View(m_Blog);
        }
        // GET: m_Blog/Create
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ownerid,title,content,visit_count,last_modify_time,classify")] m_Blog m_Blog)
        {
            if (ModelState.IsValid)
            {
                db.m_Blogs.Add(m_Blog);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(m_Blog);
        }

        // GET: m_Blog/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            m_Blog m_Blog = db.m_Blogs.Find(id);
            if (m_Blog == null)
            {
                return HttpNotFound();
            }
            return View(m_Blog);
        }

        // POST: m_Blog/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ownerid,content,visit_count,last_modify_time,classify")] m_Blog m_Blog)
        {
            if (ModelState.IsValid)
            {
                db.Entry(m_Blog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(m_Blog);
        }

        // GET: m_Blog/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            m_Blog m_Blog = db.m_Blogs.Find(id);
            if (m_Blog == null)
            {
                return HttpNotFound();
            }
            return View(m_Blog);
        }

        // POST: m_Blog/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            m_Blog m_Blog = db.m_Blogs.Find(id);
            db.m_Blogs.Remove(m_Blog);
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
        public ActionResult CreateBlog()
        {
            return View();
        }

        public ActionResult Show(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            m_Blog m_Blog = db.m_Blogs.Find(id);
            if (m_Blog == null)
            {
                return HttpNotFound();
            }
            return View(m_Blog);
        }

        public ActionResult Modify(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            m_Blog m_Blog = db.m_Blogs.Find(id);
            if (m_Blog == null)
            {
                return HttpNotFound();
            }
            return View(m_Blog);
        }

        [HttpPost]
        public ActionResult Modify([Bind(Include = "ID,ownerid,title,content,visit_count,last_modify_time,classify")] m_Blog m_Blog)
        {
            if (ModelState.IsValid)
            {
                m_Blog.last_modify_time = DateTime.Now;
                m_Blog.content = filter(Request.Unvalidated.Form["fc"]);
                m_Blog.ownerid = (int)Session["id"];
                db.Entry(m_Blog).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("UserHome", "m_User");
            }
            return View(m_Blog);
        }



        public ActionResult ShowList()
        {
            var lis = from t in db.m_Blogs.ToList()
                      where t.ownerid == (int)Session["id"]
                      select t;
            return View(lis);
        }
        #endregion
    }
}
