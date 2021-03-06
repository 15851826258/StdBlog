﻿using System;
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
    public class m_sensitivekeywordController : Controller
    {
        private m_sensitivekeywordContext db = new m_sensitivekeywordContext();

        // GET: m_sensitivekeyword
        public ActionResult Index()
        {
            return View(db.m_sensitivekeywords.ToList());
        }

        // GET: m_sensitivekeyword/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            m_sensitivekeyword m_sensitivekeyword = db.m_sensitivekeywords.Find(id);
            if (m_sensitivekeyword == null)
            {
                return HttpNotFound();
            }
            return View(m_sensitivekeyword);
        }

        // GET: m_sensitivekeyword/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: m_sensitivekeyword/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,key")] m_sensitivekeyword m_sensitivekeyword)
        {
            if (ModelState.IsValid)
            {
                db.m_sensitivekeywords.Add(m_sensitivekeyword);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(m_sensitivekeyword);
        }

        // GET: m_sensitivekeyword/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            m_sensitivekeyword m_sensitivekeyword = db.m_sensitivekeywords.Find(id);
            if (m_sensitivekeyword == null)
            {
                return HttpNotFound();
            }
            return View(m_sensitivekeyword);
        }

        // POST: m_sensitivekeyword/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,key")] m_sensitivekeyword m_sensitivekeyword)
        {
            if (ModelState.IsValid)
            {
                db.Entry(m_sensitivekeyword).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(m_sensitivekeyword);
        }

        // GET: m_sensitivekeyword/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            m_sensitivekeyword m_sensitivekeyword = db.m_sensitivekeywords.Find(id);
            if (m_sensitivekeyword == null)
            {
                return HttpNotFound();
            }
            return View(m_sensitivekeyword);
        }

        // POST: m_sensitivekeyword/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            m_sensitivekeyword m_sensitivekeyword = db.m_sensitivekeywords.Find(id);
            db.m_sensitivekeywords.Remove(m_sensitivekeyword);
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
