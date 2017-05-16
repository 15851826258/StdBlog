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

        #region api
        static bool isExsist(IEnumerable<m_recblog> lis, int blogid)
        {
            foreach (var t in lis)
            {
                if (t.blogid == blogid) return true;
            }
            return false;
        }

        #endregion

        private m_recblogContext db = new m_recblogContext();


        public ActionResult ShowBlog()
        {
            var rlis = from t in db.m_recblogs.ToList()
                       select m_BlogController.getBlog(t.blogid);
            return View(rlis);
        }

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


    }
}
