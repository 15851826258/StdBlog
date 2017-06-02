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
        public m_UserController()
        {
            ctrlManager.ctrls.user = this;
        }


        private m_UserContext db = new m_UserContext();
        private m_UserMesContext dbmes = new m_UserMesContext();


        #region apis
        public void mesExcer(int sender, int reciver, string content)
        {
            var mes = new m_UserMes()
            {
                content = content,
                recieverid = reciver,
                time = DateTime.Now,
                senderid = sender
            };
            dbmes.Entry(mes).State = EntityState.Added;
            dbmes.SaveChangesAsync();
        }

        public static int getMescount(int id)
        {
            m_UserMesContext dbmes = new m_UserMesContext();
            int res = 0;
            foreach (var t in dbmes.m_UserMess.ToList())
            {
                if (t.recieverid == id) res++;
            }
            return res;
        }
        public static bool Vertify(string uid, string pw)
        {
            m_UserContext db1 = new m_UserContext();
            string pws = Helper.AESHelper.Encrypt(pw);
            foreach (var t in db1.m_Users)
            {
                if (t.loginid == uid & t.password == pws) return true;
            }
            return false;
        }
        public static string getName(int uid)
        {
            return (new m_UserContext()).m_Users.Find(uid).name;
        }
        public static int getID(string uid)
        {
            m_UserContext db1 = new m_UserContext();
            foreach (var t in db1.m_Users)
                if (t.loginid == uid) return t.ID;
            return -1;
        }
        public static m_User getUserPac(string username)
        {
            m_UserContext db1 = new m_UserContext();
            foreach (var t in db1.m_Users)
                if (t.name == username) return t;
            return null;
        }
        public static m_User getUserPac(int id)
        {
            return (new m_UserContext()).m_Users.Find(id);
        }

        #endregion

        #region ori
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
                Session["id"] = m_User.ID;
                Session["name"] = m_User.name;
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
        #endregion

        public ActionResult mesdel(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var obj = dbmes.m_UserMess.Find((int)id);
            dbmes.Entry(obj).State = EntityState.Deleted;
            dbmes.SaveChanges();
            return RedirectToAction("getmes");
        }

        public ActionResult mesExc(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mesExcer((int)Session["id"], (int)id, Request["ccontent"]);
            return RedirectToAction("Return","Home",new { });
        }

        public ActionResult getmes()
        {
            var lis = from t in dbmes.m_UserMess.ToList()
                      where t.recieverid == (int)Session["id"]
                      select t;
            foreach (var t in lis)
            {
                if (!ViewData.Keys.Contains("s" + t.senderid))
                    ViewData.Add("s" + t.senderid, db.m_Users.Find(t.senderid).name);
            }
            return View(lis);
        }

        public ActionResult sendmes(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewData["reciver"] = db.m_Users.Find((int)id).name;
            return View(db.m_Users.Find((int)id));
        }

        public ActionResult sendmesajax(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewData["reciver"] = db.m_Users.Find((int)id).name;
            return View(db.m_Users.Find((int)id));
        }

        public ActionResult mesExcajax(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            mesExcer((int)Session["id"], (int)id, Request["ccontent"]);
            return RedirectToAction("getmes");
        }

        public ActionResult UserHome()
        {
            return View();
        }

        public ActionResult Search()
        {
            if (Request["sstr"].Length <= 0) return RedirectToAction("Return", "Home");
            return View();
        }
        public ActionResult Searchu()
        {
            var olis = db.m_Users.ToList();
            var lis = from t in olis
                      where t.name.Contains(Request["sstr"])
                      && t.ID != (int)Session["id"]
                      select t;
            return View(lis.ToList());
        }
        public ActionResult Searchb()
        {
            return View(from t in (new m_BlogContext()).m_Blogs.ToList()
                        where t.title.Contains(Request["sstr"])
                        select t);
        }

        public ActionResult UserBinfo(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if ((int)id == (int)Session["id"]) return RedirectToAction("ShowList", "m_Blog");
            m_User m_User = db.m_Users.Find(id);
            if (m_User == null)
            {
                return HttpNotFound();
            }
            return View(m_User);
        }

    }
}
