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
    public class publishersController : Controller
    {
        private pubsEntities db = new pubsEntities();

        // GET: publishers
        public ActionResult Index()
        {
            // List<publishers> pubList = db.publishers.ToList();
            //ViewBag.publishers = pubList;
            //var publishers = db.publishers.Include(p => p.pub_info);
            IQueryable<publishers> list = db.publishers.Include(p => p.pub_info);
            string publisherName = Request.QueryString["publisherName"];
            string publisherCity = Request.QueryString["publisherCity"];
            string publisherState = Request.QueryString["publisherState"];
            string publisherCountry = Request.QueryString["publisherCountry"];
            string publisherInfo = Request.QueryString["publisherInfo"];
            
            if(publisherName != null && publisherName != "")
            {
                list = list.Where(m => m.pub_name == publisherName);
            }
            if(publisherCity !=null && publisherCity != "")
            {
                list = list.Where(m => m.city == publisherCity);
            }
            if(publisherState != null && publisherState != "")
            {
                list = list.Where(m => m.state == publisherState);
            }
            if(publisherCountry != null && publisherCountry != "")
            {
                list = list.Where(m => m.country == publisherCountry);
            }
            if (publisherInfo != null &&publisherInfo != "")
            {
                list = list.Where(m => m.pub_info.pr_info.Contains(publisherInfo));
            }

            return View(list.ToList());
        }

        // GET: publishers/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            publishers publishers = db.publishers.Find(id);
            if (publishers == null)
            {
                return HttpNotFound();
            }
            return View(publishers);
        }

        // GET: publishers/Create
        public ActionResult Create()
        {
            ViewBag.pub_id = new SelectList(db.pub_info, "pub_id", "pr_info");
            return View();
        }

        public ActionResult logo(string id)
        {
            
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            publishers publisher = db.publishers.Find(id);
            if (publisher == null)
            {
                return HttpNotFound();
            }
            return File(publisher.pub_info.logo, "image/png");
        }

        // POST: publishers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "pub_id,pub_name,city,state,country")] publishers publishers, [Bind(Include = "pub_id,logo,pr_info")] pub_info pub_info)
        {
            if (ModelState.IsValid)
            {
                db.publishers.Add(publishers);
                pub_info.pub_id = publishers.pub_id;
                db.pub_info.Add(pub_info);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.pub_id = new SelectList(db.pub_info, "pub_id", "pr_info", publishers.pub_id);
            return View(publishers);
        }

        // GET: publishers/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            publishers publishers = db.publishers.Find(id);
            if (publishers == null)
            {
                return HttpNotFound();
            }
            ViewBag.pub_id = new SelectList(db.pub_info, "pub_id", "pr_info", publishers.pub_id);
            return View(publishers);
        }

        // POST: publishers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "pub_id,logo,pr_info")] pub_info pub_info, [Bind(Include = "pub_id,pub_name,city,state,country")] publishers publishers )
        {
            if (ModelState.IsValid)
            {
                db.Entry(publishers).State = EntityState.Modified;
                pub_info.pub_id = publishers.pub_id;
                db.Entry(pub_info).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.pub_id = new SelectList(db.pub_info, "pub_id", "pr_info", publishers.pub_id);
            return View(publishers);
        }

        // GET: publishers/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            publishers publishers = db.publishers.Find(id);
            if (publishers == null)
            {
                return HttpNotFound();
            }
            return View(publishers);
        }

        // POST: publishers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            try {
                publishers publishers = db.publishers.Find(id);
                foreach (pub_info egg in db.pub_info.Where(x => x.pub_id == id))
                {
                    db.pub_info.Remove(egg);
                    // db.pub_info.Find
                }
                foreach (titles egg in db.titles.Where(x => x.pub_id == id))
                {
                    foreach (titleauthor egg2 in db.titleauthor.Where(x => x.title_id == egg.title_id))
                    {
                        db.titleauthor.Remove(egg2);
                    }
                    foreach (sales egg3 in db.sales.Where(x => x.title_id == egg.title_id))
                    {
                        foreach (stores egg6 in db.stores.Where(x => x.stor_id == egg3.stor_id))
                        {
                            db.stores.Remove(egg6);
                        }
                        db.sales.Remove(egg3);
                    }
                    db.titles.Remove(egg);
                    foreach (roysched egg4 in db.roysched.Where(x => x.title_id == egg.title_id))
                    {
                        db.roysched.Remove(egg4);
                        return RedirectToAction("Index");
                    }

                }
                foreach (employee egg5 in db.employee.Where(x => x.pub_id == id)) {
                    db.employee.Remove(egg5);
                }
                db.publishers.Remove(publishers);
                db.SaveChanges();
                return RedirectToAction("Index");
            } catch(Exception e)
            {
                return RedirectToAction("Index");
            }
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
