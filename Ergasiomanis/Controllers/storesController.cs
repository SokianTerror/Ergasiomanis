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
            string StoreName = Request.QueryString["StoreName"];
            string StoreAddress = Request.QueryString["StoreAddress"];
            string StoreCity = Request.QueryString["StoreCity"];
            string StoreState = Request.QueryString["StoreState"];
            string StoreZip = Request.QueryString["StoreZip"];
            string StoreId = Request.QueryString["StoreId"];
            if(StoreId != null && StoreId != "")
            {
                StoreId = StoreId.Trim();
                list = list.Where(m => m.stor_id.Contains(StoreId));
            }
            if(StoreName != null && StoreName != "")
            {
                StoreName = StoreName.Trim();
                list = list.Where(m => m.stor_name.Contains(StoreName));
            }
            if(StoreAddress != null && StoreAddress != "")
            {
                StoreAddress = StoreAddress.Trim();
                list = list.Where(m => m.stor_address.Contains(StoreAddress));
            }
            if(StoreCity != null && StoreCity != "")
            {
                StoreCity = StoreAddress.Trim();
                list = list.Where(m => m.city.Contains(StoreCity));
            }
            if(StoreState != null && StoreState != "")
            {
                StoreState = StoreState.Trim();
                list = list.Where(m => m.state.Contains(StoreState));
            }
            if (StoreZip != null && StoreZip != "")
            {
                StoreZip = StoreZip.Trim();
                list = list.Where(m => m.zip.Contains(StoreZip));
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
