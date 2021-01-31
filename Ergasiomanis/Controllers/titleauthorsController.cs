using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ergasiomanis;
using Ergasiomanis.Models;

namespace Ergasiomanis.Controllers
{
    public class titleauthorsController : Controller
    {
        private pubsEntities db = new pubsEntities();

        // GET: titleauthors
        public ActionResult Index()
        {
            //var titleauthor = db.titleauthor.Include(t => t.authors).Include(t => t.titles);
            IQueryable<titleauthor> list = db.titleauthor.Include(t => t.authors).Include(t => t.titles);
            string FromAuthorOrder = Request.QueryString["FromAuthorOrder"];
            string ToAuthorOrder = Request.QueryString["ToAuthorOrder"];
            string FromRpB = Request.QueryString["FromRpB"];
            string ToRpB = Request.QueryString["ToRpB"];
            string LastName = Request.QueryString["LastName"];
            string Title = Request.QueryString["Title"];

            if(FromAuthorOrder != null && FromAuthorOrder != "")
            {
                byte FromAuthorOrder2 = Convert.ToByte(FromAuthorOrder);
                list = list.Where(m => m.au_ord >= FromAuthorOrder2);
            }
            if(ToAuthorOrder != null && ToAuthorOrder != "")
            {
                byte ToAuthorOrder2 = Convert.ToByte(ToAuthorOrder);
                list = list.Where(m => m.au_ord <= ToAuthorOrder2);
            }
            if(FromRpB != null && FromRpB!="")
            {
                int FromRpB2 = Convert.ToInt32(FromRpB);
                list = list.Where(m => m.royaltyper >= FromRpB2);
            }
            if (ToRpB != null && ToRpB!="")
            {
                int ToRpB2 = Convert.ToInt32(ToRpB);
                list = list.Where(m => m.royaltyper <= ToRpB2);
            }
            if(LastName != null && LastName != "")
            {
                LastName = LastName.Trim();
                list = list.Where(m => m.authors.au_lname.Contains(LastName));
            }
            if(Title != null && Title !="")
            {
                Title = Title.Trim();
                list = list.Where(m => m.titles.title.Contains(Title));
            }

            return View(list.ToList());
        }

        // GET: titleauthors/Details/5
        public ActionResult Details(string id1 , string id2)
        {
            if (id1 == null || id2 == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            titleauthor titleauthor = db.titleauthor.Find(id1,id2);
            if (titleauthor == null)
            {
                return HttpNotFound();
            }
            return View(titleauthor);
        }

        // GET: titleauthors/Create
        public ActionResult Create()
        {
            ViewBag.au_id = new SelectList(db.authors, "au_id", "au_lname");
            ViewBag.title_id = new SelectList(db.titles, "title_id", "title");
            return View();
        }

        // POST: titleauthors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "au_id,title_id,au_ord,royaltyper")] titleauthor titleauthor)
        {
            if (ModelState.IsValid)
            {
                db.titleauthor.Add(titleauthor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.au_id = new SelectList(db.authors, "au_id", "au_lname", titleauthor.au_id);
            ViewBag.title_id = new SelectList(db.titles, "title_id", "title", titleauthor.title_id);
            return View(titleauthor);
        }

        // GET: titleauthors/Edit/5
        public ActionResult Edit(string id1, string id2)
        {
            if (id1 == null || id2 == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            titleauthor titleauthor = db.titleauthor.Find(id1,id2);
            if (titleauthor == null)
            {
                return HttpNotFound();
            }
            ViewBag.au_id = new SelectList(db.authors, "au_id", "au_lname", titleauthor.au_id);
            ViewBag.title_id = new SelectList(db.titles, "title_id", "title", titleauthor.title_id);
            return View(titleauthor);
        }

        // POST: titleauthors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "au_id,title_id,au_ord,royaltyper")] titleauthor titleauthor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(titleauthor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.au_id = new SelectList(db.authors, "au_id", "au_lname", titleauthor.au_id);
            ViewBag.title_id = new SelectList(db.titles, "title_id", "title", titleauthor.title_id);
            return View(titleauthor);
        }

        // GET: titleauthors/Delete/5
        public ActionResult Delete(string id1 , string id2)
        {
            if (id1 == null || id2 == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            titleauthor titleauthor = db.titleauthor.Find(id1,id2);
            if (titleauthor == null)
            {
                return HttpNotFound();
            }
            return View(titleauthor);
        }

        // POST: titleauthors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id1, string id2)
        {
            titleauthor titleauthor = db.titleauthor.Find(id1,id2);
            db.titleauthor.Remove(titleauthor);
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
