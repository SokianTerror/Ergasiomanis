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
    public class salesController : Controller
    {
        private pubsEntities db = new pubsEntities();

        // GET: sales
        public ActionResult Index()
        {
            IQueryable<sales> list = db.sales.Include(s => s.stores).Include(s => s.titles);
            string salesFromOrderDate = Request.QueryString["salesFromOrderDate"];
            string salesToOrderDate = Request.QueryString["salesToOrderDate"];
            string salesFromQuantity = Request.QueryString["salesFromQuantity"];
            string salesToQuantity = Request.QueryString["salesToQuantity"];
            string salesPaymentTerms = Request.QueryString["salesPaymentTerms"];
            string salesStoreName = Request.QueryString["salesStoreName"];
            string salesTitle = Request.QueryString["salesTitle"];
            string salesOrdNum = Request.QueryString["salesOrderNumber"];
            if(salesOrdNum != null && salesOrdNum !="")
            {
                salesOrdNum = salesOrdNum.Trim();
                list = list.Where(m => m.ord_num.Contains(salesOrdNum));
            }
            if(salesFromOrderDate !=null && salesFromOrderDate != "")
            {
                DateTime salesFromOrderDate2 = DateTime.Parse(salesFromOrderDate);
                list = list.Where(m => m.ord_date >= salesFromOrderDate2);
            }
            if(salesToOrderDate != null && salesToOrderDate != "")
            {
                DateTime salesToOrderDate2 = DateTime.Parse(salesToOrderDate);
                list = list.Where(m => m.ord_date <= salesToOrderDate2);
            }
            if(salesFromQuantity != null && salesFromQuantity != "")
            {
                short salesFromQuantity2 = Convert.ToInt16(salesFromQuantity);
                list = list.Where(m => m.qty >= salesFromQuantity2);
            }
            if(salesToQuantity != null && salesToQuantity != "")
            {
                short salesToQuantity2 = Convert.ToInt16(salesToQuantity);
                list = list.Where(m => m.qty <= salesToQuantity2);
            }
            if(salesPaymentTerms != null && salesPaymentTerms != "")
            {
                salesPaymentTerms = salesPaymentTerms.Trim();
                list = list.Where(m => m.payterms.Contains(salesPaymentTerms));
            }
            if(salesStoreName != null && salesStoreName != "")
            {
                salesStoreName = salesStoreName.Trim();
                list = list.Where(m => m.stores.stor_name.Contains(salesStoreName));
            }
            if(salesTitle != null && salesTitle != "")
            {
                salesTitle = salesTitle.Trim();
                list = list.Where(m => m.titles.title.Contains(salesTitle));
            }
            return View(list.ToList());
        }

        // GET: sales/Details/5
        public ActionResult Details(string id1,string id2,string id3)
        {
            if (id1 == null || id2 == null || id3 == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sales sales = db.sales.Find(id1,id2,id3);
            if (sales == null)
            {
                return HttpNotFound();
            }
            return View(sales);
        }

        // GET: sales/Create
        public ActionResult Create()
        {
            ViewBag.stor_id = new SelectList(db.stores, "stor_id", "stor_name");
            ViewBag.title_id = new SelectList(db.titles, "title_id", "title");
            ViewBag.ord_num = new SelectList(db.titles, "ord_num", "order_number");
            return View();
        }

        // POST: sales/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "stor_id,ord_num,ord_date,qty,payterms,title_id")] sales sales)
        {
            if (ModelState.IsValid)
            {
                db.sales.Add(sales);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.stor_id = new SelectList(db.stores, "stor_id", "stor_name", sales.stor_id);
            ViewBag.title_id = new SelectList(db.titles, "title_id", "title", sales.title_id);
            ViewBag.ord_num = new SelectList(db.titles, "ord_num", "order_number", sales.ord_num);
            return View(sales);
        }

        // GET: sales/Edit/5
        public ActionResult Edit(string id1, string id2, string id3)
        {
            if (id1 == null  || id2 == null || id3 == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sales sales = db.sales.Find(id1,id2,id3);
            if (sales == null)
            {
                return HttpNotFound();
            }
            ViewBag.stor_id = new SelectList(db.stores, "stor_id", "stor_name", sales.stor_id);
            ViewBag.title_id = new SelectList(db.titles, "title_id", "title", sales.title_id);
            return View(sales);
        }

        // POST: sales/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "stor_id,ord_num,ord_date,qty,payterms,title_id")] sales sales)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sales).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.stor_id = new SelectList(db.stores, "stor_id", "stor_name", sales.stor_id);
            ViewBag.title_id = new SelectList(db.titles, "title_id", "title", sales.title_id);
            ViewBag.ord_num  = new SelectList(db.titles, "ord_num", "order_number", sales.ord_num);
            return View(sales);
        }

        // GET: sales/Delete/5
        public ActionResult Delete(string id1, string id2, string id3)
        {
            if (id1 == null || id2 == null || id3 == null )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            sales sales = db.sales.Find(id1,id2,id3);
            if (sales == null)
            {
                return HttpNotFound();
            }
            return View(sales);
        }

        // POST: sales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id1, string id2, string id3)
        {

            sales sales = db.sales.Find(id1,id2,id3);
            db.sales.Remove(sales);
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
