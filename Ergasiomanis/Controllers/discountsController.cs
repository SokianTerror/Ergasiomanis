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
    public class discountsController : Controller
    {
        private pubsEntities db = new pubsEntities();

        // GET: discounts
        public ActionResult Index()
        {
            //var discounts = db.discounts.Include(d => d.stores);
            IQueryable<discounts> list = db.discounts.Include(d => d.stores);
            string FromLowQuantity = Request.QueryString["FromLowQuantity"];
            string ToLowQuantity = Request.QueryString["ToLowQuantity"];
            string FromHighQuantity = Request.QueryString["FromHighQuantity"];
            string ToHighQuantity = Request.QueryString["ToHighQuantity"];
            string StoreName = Request.QueryString["StoreName"];
            string FromDiscount = Request.QueryString["FromDiscount"];
            string ToDiscount = Request.QueryString["ToDiscount"];
            string storeId = Request.QueryString["storeId"];
            if(storeId != null && storeId != "")
            {
                storeId = storeId.Trim();
                list = list.Where(m => m.stor_id.Contains(storeId));
            }
            if(FromLowQuantity != null && FromLowQuantity != "")
            {
                short FromLowQuantity2 = Convert.ToInt16(FromLowQuantity);
                list = list.Where(m => m.lowqty >= FromLowQuantity2) ;
            }
            if (ToLowQuantity != null && ToLowQuantity != "")
            {
                short ToLowQuantity2 = Convert.ToInt16(ToLowQuantity);
                list = list.Where(m => m.lowqty <= ToLowQuantity2);
            }
            if (FromHighQuantity != null && FromHighQuantity != "")
            {
                short FromHighQuantity2 = Convert.ToInt16(FromHighQuantity);
                list = list.Where(m => m.lowqty >= FromHighQuantity2);
            }
            if (ToHighQuantity != null && ToHighQuantity != "")
            {
                short ToHighQuantity2 = Convert.ToInt16(ToHighQuantity);
                list = list.Where(m => m.lowqty >= ToHighQuantity2);
            }
            if(StoreName != null && StoreName != "")
            {
                StoreName = StoreName.Trim();
                list = list.Where(m => m.stores.stor_name.Contains(StoreName));
            }
            if(FromDiscount != null && FromDiscount != "")
            {
                decimal FromDiscount2 = Convert.ToDecimal("FromDiscount");
                list = list.Where(m => m.discount >= FromDiscount2);
            }
            if (ToDiscount != null && ToDiscount != "")
            {
                decimal ToDiscount2 = Convert.ToDecimal("ToDiscount");
                list = list.Where(m => m.discount >= ToDiscount2);
            }
            return View(list.ToList());
        }

        // GET: discounts/Details/5
        public ActionResult Details(string id1, decimal id2)
        {
            if (id1 == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            discounts discounts = db.discounts.Find(id1,id2);
            if (discounts == null)
            {
                return HttpNotFound();
            }
            return View(discounts);
        }

        // GET: discounts/Create
        public ActionResult Create()
        {
            ViewBag.stor_id = new SelectList(db.stores, "stor_id", "stor_name");
            return View();
        }

        // POST: discounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "discounttype,stor_id,lowqty,highqty,discount")] discounts discounts)
        {
            if (ModelState.IsValid)
            {
                db.discounts.Add(discounts);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.stor_id = new SelectList(db.stores, "stor_id", "stor_name", discounts.stor_id);
            return View(discounts);
        }

        // GET: discounts/Edit/5
        public ActionResult Edit(string id1 , decimal id2)
        {
            if (id1 == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            discounts discounts = db.discounts.Find(id1,id2);
            if (discounts == null)
            {
                return HttpNotFound();
            }
            ViewBag.stor_id = new SelectList(db.stores, "stor_id", "stor_name", discounts.stor_id);
            return View(discounts);
        }

        // POST: discounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "discounttype,stor_id,lowqty,highqty,discount")] discounts discounts)
        {
            if (ModelState.IsValid)
            {
                db.Entry(discounts).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.stor_id = new SelectList(db.stores, "stor_id", "stor_name", discounts.stor_id);
            return View(discounts);
        }

        // GET: discounts/Delete/5
        public ActionResult Delete(string id1, decimal id2)
        {
            if (id1 == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            discounts discounts = db.discounts.Find(id1,id2);
            if (discounts == null)
            {
                return HttpNotFound();
            }
            return View(discounts);
        }

        // POST: discounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            discounts discounts = db.discounts.Find(id);
            db.discounts.Remove(discounts);
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
