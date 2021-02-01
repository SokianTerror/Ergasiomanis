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
    public class storesController : Controller
    {
        private pubsEntities db = new pubsEntities();

        // GET: stores
        public ActionResult Index()
        {
            IQueryable<stores> list = db.stores;
            string strname = Request.QueryString["stname"];
            string strad = Request.QueryString["stadr"];
            string city = Request.QueryString["city"];
            string state = Request.QueryString["state"];
            string zip = Request.QueryString["zip"];

            if(strname != null && strname != "")
            {
                strname = strname.Trim();
                list = list.Where(p => p.stor_name.Contains(strname));
            }
            if (strad != null && strad != "")
            {
                strad = strad.Trim();
                list = list.Where(p => p.stor_address.Contains(strad));
            }
            if (city != null && city != "")
            { 
                city = city.Trim();
                list = list.Where(p => p.city.Contains(city));
            }
            if (state != null && state != "")
            {
                state = state.Trim();
                list = list.Where(p => p.state.Contains(state));
            }
            if (zip != null && zip != "")
            {
                zip = zip.Trim();
                list = list.Where(p => p.zip.Contains(zip));
            }
            return View(list.ToList());
        }

        // GET: stores/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            stores stores = db.stores.Find(id);
            if (stores == null)
            {
                return HttpNotFound();
            }
            return View(stores);
        }

        // GET: stores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: stores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "stor_id,stor_name,stor_address,city,state,zip")] stores stores)
        {
            if (ModelState.IsValid)
            {
                db.stores.Add(stores);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(stores);
        }

        // GET: stores/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            stores stores = db.stores.Find(id);
            if (stores == null)
            {
                return HttpNotFound();
            }
            return View(stores);
        }

        // POST: stores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "stor_id,stor_name,stor_address,city,state,zip")] stores stores)
        {
            if (ModelState.IsValid)
            {
                db.Entry(stores).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(stores);
        }

        // GET: stores/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            stores stores = db.stores.Find(id);
            if (stores == null)
            {
                return HttpNotFound();
            }
            return View(stores);
        }

        // POST: stores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                stores stores = db.stores.Find(id);
                foreach (sales egg in db.sales.Where(x => x.title_id == id))
                {
                    db.sales.Remove(egg);
                }
                db.stores.Remove(stores);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch(Exception e)
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
