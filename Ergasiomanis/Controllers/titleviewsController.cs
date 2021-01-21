using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ergasiomanis;

namespace Ergasiomanis.Controllers
{
    public class titleviewsController : Controller
    {
        private pubsEntities db = new pubsEntities();

        // GET: titleviews
        public ActionResult Index()
        {
            return View(db.titleview.ToList());
        }

        // GET: titleviews/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            titleview titleview = db.titleview.Find(id);
            if (titleview == null)
            {
                return HttpNotFound();
            }
            return View(titleview);
        }

        // GET: titleviews/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: titleviews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "title,au_ord,au_lname,price,ytd_sales,pub_id")] titleview titleview)
        {
            if (ModelState.IsValid)
            {
                db.titleview.Add(titleview);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(titleview);
        }

        // GET: titleviews/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            titleview titleview = db.titleview.Find(id);
            if (titleview == null)
            {
                return HttpNotFound();
            }
            return View(titleview);
        }

        // POST: titleviews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "title,au_ord,au_lname,price,ytd_sales,pub_id")] titleview titleview)
        {
            if (ModelState.IsValid)
            {
                db.Entry(titleview).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(titleview);
        }

        // GET: titleviews/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            titleview titleview = db.titleview.Find(id);
            if (titleview == null)
            {
                return HttpNotFound();
            }
            return View(titleview);
        }

        // POST: titleviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            titleview titleview = db.titleview.Find(id);
            db.titleview.Remove(titleview);
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
