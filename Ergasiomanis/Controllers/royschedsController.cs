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
    public class royschedsController : Controller
    {
        private pubsEntities db = new pubsEntities();

        // GET: royscheds
        public ActionResult Index()
            
        {
            string qr = "select * from roysched";
            IEnumerable<roysched> roysched = db.Database.SqlQuery<roysched>(qr);            
            string FromLowRange = Request.QueryString["FromLowRange"];
            string ToLowRange = Request.QueryString["ToLowRange"];
            string FromHighRange = Request.QueryString["FromHighRange"];
            string ToHighRange = Request.QueryString["ToHighRange"];
            string FromRoyalty = Request.QueryString["FromRoyalty"];
            string ToRoyalty = Request.QueryString["ToRoyalty"];
            string titleId = Request.QueryString["titleId"];
            if(titleId != null && titleId !="")
            {
                titleId = titleId.Trim();
                roysched = roysched.Where(m => m.title_id.Contains(titleId));
            }
            if(FromLowRange!=null && FromLowRange != "")
            {
                int FromLowRange2 = Convert.ToInt32(FromLowRange);
                roysched = roysched.Where(m => m.lorange >= FromLowRange2);
            }
            if(ToLowRange != null && ToLowRange != "")
            {
                int ToLowRange2 = Convert.ToInt32(ToLowRange);
                roysched = roysched.Where(m => m.lorange <= ToLowRange2);
            }
            if(FromHighRange != null && FromHighRange != "")
            {
                int FromHighRange2 = Convert.ToInt32(FromHighRange);
                roysched = roysched.Where(m => m.hirange >= FromHighRange2);
            }
            if(ToHighRange != null && ToHighRange != "")
            {
                int ToHighRange2 = Convert.ToInt32(ToHighRange);
                roysched = roysched.Where(m => m.hirange <= ToHighRange2);
            }
            if(FromRoyalty != null && FromRoyalty !="")
            {
                int FromRoyalty2 = Convert.ToInt32(FromRoyalty);
                roysched = roysched.Where(m => m.royalty >= FromRoyalty2);
            }
            if (ToRoyalty != null && ToRoyalty !="")
            {
                int ToRoyalty2 = Convert.ToInt32(ToRoyalty);
                roysched = roysched.Where(m => m.royalty <= ToRoyalty2);
            }            
            return View(roysched.ToList());

        }

        // GET: royscheds/Details/5
        public ActionResult Details(string id,int id2,int id3,int id4)
        {
            string qr = "select * from roysched where title_id=@p0 and lorange=@p1 and hirange=@p2 and royalty=@p3";
            roysched roysched = db.roysched.SqlQuery(qr,  id, id2, id3, id4 ).SingleOrDefault();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (roysched == null)
            {
                return HttpNotFound();
            }
            return View(roysched);
        }

        // GET: royscheds/Create
        public ActionResult Create()
        {
            ViewBag.title_id = new SelectList(db.titles, "title_id", "title");
            return View();
        }

        // POST: royscheds/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "title_id,lorange,hirange,royalty")] roysched roysched)
        {
            //string qr = "insert into roysched values (@p0,@p1,@p2,@p3) ";
            //IEnumerable<roysched> roysched = db.Database.SqlQuery<roysched>(qr, id, new int[] { id2, id3, id4 });

            if (ModelState.IsValid)
            {
                db.roysched.Add(roysched);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.title_id = new SelectList(db.titles, "title_id", "title", roysched.title_id);
            return View(roysched);
        }

        // GET: royscheds/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            roysched roysched = db.roysched.Find(id);
            if (roysched == null)
            {
                return HttpNotFound();
            }
            ViewBag.title_id = new SelectList(db.titles, "title_id", "title", roysched.title_id);
            return View(roysched);
        }

        // POST: royscheds/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "title_id,lorange,hirange,royalty")] roysched roysched)
        {
            if (ModelState.IsValid)
            {
                db.Entry(roysched).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.title_id = new SelectList(db.titles, "title_id", "title", roysched.title_id);
            return View(roysched);
        }

        // GET: royscheds/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            roysched roysched = db.roysched.Find(id);
            if (roysched == null)
            {
                return HttpNotFound();
            }
            return View(roysched);
        }

        // POST: royscheds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            roysched roysched = db.roysched.Find(id);
            db.roysched.Remove(roysched);
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
