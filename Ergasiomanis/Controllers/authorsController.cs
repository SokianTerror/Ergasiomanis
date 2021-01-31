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
    public class authorsController : Controller
    {
        private pubsEntities db = new pubsEntities();

        // GET: authors
        public ActionResult Index()
        {
            IQueryable<authors> list = db.authors;
            string authorLastName = Request.QueryString["authorLastName"];
            string authorFirstName = Request.QueryString["authorFirstName"];
            string authorPhone = Request.QueryString["authorPhone"];
            string authorAddress = Request.QueryString["authorAddress"];
            string authorCity = Request.QueryString["authorCity"];
            string authorState = Request.QueryString["authorState"];
            string authorZip = Request.QueryString["authorZip"];
            string authorContract = Request.QueryString["authorContract"];
            string authorId = Request.QueryString["authorId"];
            if(authorId != null && authorId !="")
            {
                authorId = authorId.Trim();
                list = list.Where(m => m.au_id.Contains(authorId));
            }
            if(authorLastName != null && authorLastName != "")
            {
                authorLastName = authorLastName.Trim();
                list = list.Where(m => m.au_lname.Contains(authorLastName));
            }
            if(authorFirstName != null && authorFirstName != "")
            {
                authorFirstName = authorFirstName.Trim();
                list = list.Where(m => m.au_fname.Contains(authorFirstName));
            }
            if(authorPhone != null && authorPhone != "")
            {
                authorPhone = authorPhone.Trim();
                list = list.Where(m => m.phone.Contains(authorPhone));
            }
            if(authorAddress != null && authorAddress != "")
            {
                authorAddress = authorAddress.Trim();
                list = list.Where(m => m.address.Contains(authorAddress));
            }
            if(authorCity != null && authorCity != "")
            {
                authorCity = authorCity.Trim();
                list = list.Where(m => m.city.Contains(authorCity));
            }
            if(authorState != null && authorState != "")
            {
                authorState = authorState.Trim();
                list = list.Where(m => m.state.Contains(authorState));
            }
            if(authorZip != null && authorZip != "")
            {
                authorZip = authorZip.Trim();
                list = list.Where(m => m.zip.Contains(authorZip));
            }
            if(authorContract == "on")
            {
                list = list.Where(m => m.contract == true);
            }            

            return View(list.ToList());;
        }

        // GET: authors/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            authors authors = db.authors.Find(id);
            if (authors == null)
            {
                return HttpNotFound();
            }
            return View(authors);
        }

        // GET: authors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: authors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "au_id,au_lname,au_fname,phone,address,city,state,zip,contract")] authors authors)
        {
            if (ModelState.IsValid)
            {
                db.authors.Add(authors);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(authors);
        }

        // GET: authors/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            authors authors = db.authors.Find(id);
            if (authors == null)
            {
                return HttpNotFound();
            }
            return View(authors);
        }

        // POST: authors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "au_id,au_lname,au_fname,phone,address,city,state,zip,contract")] authors authors)
        {
            if (ModelState.IsValid)
            {
                db.Entry(authors).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(authors);
        }

        // GET: authors/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            authors authors = db.authors.Find(id);
            if (authors == null)
            {
                return HttpNotFound();
            }
            return View(authors);
        }

        // POST: authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            authors authors = db.authors.Find(id);
            foreach(titleauthor eggrafi in db.titleauthor.Where(x=> x.au_id == id))
            {
                db.titleauthor.Remove(eggrafi);
            }
            db.authors.Remove(authors);
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
