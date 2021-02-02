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
    public static class titlegl
    {
        public static string mes { get; set; }
        public static bool qu { get; set; }
    }

    public class titlesController : Controller
    {
        private pubsEntities db = new pubsEntities();

        // GET: titles
        public ActionResult Index()
        {
            ViewBag.Message = titlegl.mes;
            ViewBag.ar = titlegl.qu;
            titlegl.qu = false;            
            IQueryable<titles> list = db.titles.Include(t => t.publishers).Include(t => t.roysched);
            string titlesTitle = Request.QueryString["titlesTitle"];
            string titlesType = Request.QueryString["titlesType"];
            string titlesFromPrice = Request.QueryString["titlesFromPrice"];
            string titlesToPrice = Request.QueryString["titlesToPrice"];
            string titlesFromAdvance = Request.QueryString["titlesFromAdvance"];
            string titlesToAdvance = Request.QueryString["titlesToAdvance"];
            string titlesFromRoyalty = Request.QueryString["titlesFromRoyalty"];
            string titlesToRoyalty = Request.QueryString["titlesToRoyalty"];
            string titlesFromTimesSold = Request.QueryString["titlesFromTimesSold"];
            string titlesToTimesSold = Request.QueryString["titlesToTimesSold"];
            string titlesNotes = Request.QueryString["titlesNotes"];
            string titlesFromReleaseDate = Request.QueryString["titlesFromReleaseDate"];
            string titlesToReleaseDate = Request.QueryString["titlesToReleaseDate"];
            string titlesPublisherName = Request.QueryString["titlesPublisherName"];
            string titlesTitleId = Request.QueryString["titlesTitleId"];

            if(titlesTitle != null && titlesTitle !="")
            {
                titlesTitle = titlesTitle.Trim();
                list = list.Where(m => m.title.Contains(titlesTitle));
            }
            if (titlesType != null && titlesType != "")
            {
                titlesType = titlesType.Trim();
                list = list.Where(m => m.type.Contains(titlesType));
            }
            if(titlesFromPrice != null && titlesFromPrice !="")
            {
                decimal titlesFromPrice2 = Convert.ToDecimal(titlesFromPrice);
                list = list.Where(m => m.price >= titlesFromPrice2);
            }
            if(titlesToPrice != null && titlesToPrice != "")
            {
                decimal titlesToPrice2 = Convert.ToDecimal(titlesToPrice);
                list = list.Where(m => m.price <= titlesToPrice2);
            }
            if(titlesFromAdvance != null && titlesToAdvance != "")
            {
                decimal titlesFromAdvance2 = Convert.ToDecimal(titlesFromAdvance);
                list = list.Where(m => m.advance >= titlesFromAdvance2);
            }
            if(titlesToAdvance != null && titlesToAdvance != "")
            {
                decimal titlesToAdvance2 = Convert.ToDecimal(titlesToAdvance);
                list = list.Where(m => m.advance <= titlesToAdvance2);
            }
            if(titlesFromRoyalty != null && titlesFromRoyalty != "")
            {
                int titlesFromRoyalty2 = Convert.ToInt32(titlesFromRoyalty);
                list = list.Where(m => m.royalty >= titlesFromRoyalty2);
            }
            if (titlesToRoyalty != null && titlesToRoyalty != "")
            {
                int titlesToRoyalty2 = Convert.ToInt32(titlesToRoyalty);
                list = list.Where(m => m.royalty <= titlesToRoyalty2);
            }
            if(titlesFromTimesSold != null && titlesFromTimesSold != "")
            {
                int titlesFromTimesSold2 = Convert.ToInt32(titlesFromTimesSold);
                list = list.Where(m => m.ytd_sales >= titlesFromTimesSold2);
            }
            if(titlesToTimesSold != null && titlesToTimesSold != "")
            {
                int titlesToTimesSold2 = Convert.ToInt32(titlesToTimesSold);
                list = list.Where(m => m.ytd_sales <= titlesToTimesSold2);
            }
            if(titlesNotes != null && titlesNotes != "")
            {
                titlesNotes = titlesNotes.Trim();
                list = list.Where(m => m.notes.Contains(titlesNotes));
            }
            if(titlesFromReleaseDate != null && titlesFromReleaseDate !="")
            {
                DateTime titlesFromReleaseDate2 = DateTime.Parse(titlesFromReleaseDate);
                list = list.Where(m => m.pubdate >= titlesFromReleaseDate2);
            }
            if(titlesToReleaseDate != null && titlesToReleaseDate != "")
            {
                DateTime titlesToReleaseDate2 = DateTime.Parse(titlesToReleaseDate);
                list = list.Where(m => m.pubdate <= titlesToReleaseDate2);
            }
            if(titlesPublisherName != null && titlesPublisherName != "")
            {
                titlesPublisherName = titlesPublisherName.Trim();
                list = list.Where(m => m.publishers.pub_name.Contains(titlesPublisherName));
            }
            if(titlesTitleId != null && titlesTitleId != "")
            {
                titlesTitleId = titlesTitleId.Trim();
                list = list.Where(m => m.title_id.Contains(titlesTitleId));
            }
            return View(list.ToList());
        }

        // GET: titles/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            titles titles = db.titles.Find(id);
            if (titles == null)
            {
                return HttpNotFound();
            }
            return View(titles);
        }

        // GET: titles/Create
        public ActionResult Create()
        {
            ViewBag.pub_id = new SelectList(db.publishers, "pub_id", "pub_name");
            ViewBag.title_id = new SelectList(db.roysched, "title_id", "title_id");
            return View();
        }

        // POST: titles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "title_id,title,type,pub_id,price,advance,royalty,ytd_sales,notes,pubdate")] titles titles)
        {
            if (ModelState.IsValid)
            {
                db.titles.Add(titles);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.pub_id = new SelectList(db.publishers, "pub_id", "pub_name", titles.pub_id);
            ViewBag.title_id = new SelectList(db.roysched, "title_id", "title_id", titles.title_id);
            return View(titles);
        }

        // GET: titles/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            titles titles = db.titles.Find(id);
            if (titles == null)
            {
                return HttpNotFound();
            }
            ViewBag.pub_id = new SelectList(db.publishers, "pub_id", "pub_name", titles.pub_id);
            ViewBag.title_id = new SelectList(db.roysched, "title_id", "title_id", titles.title_id);
            return View(titles);
        }

        // POST: titles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "title_id,title,type,pub_id,price,advance,royalty,ytd_sales,notes,pubdate")] titles titles)
        {
            if (ModelState.IsValid)
            {
                db.Entry(titles).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.pub_id = new SelectList(db.publishers, "pub_id", "pub_name", titles.pub_id);
            ViewBag.title_id = new SelectList(db.roysched, "title_id", "title_id", titles.title_id);
            return View(titles);
        }

        // GET: titles/Delete/5
        public ActionResult Delete(string id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            titles titles = db.titles.Find(id);
            if (titles == null)
            {
                return HttpNotFound();
            }
            return View(titles);
        }

        // POST: titles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            try
            {
                titlegl.mes = "Η διαγραφη ολοκληρωθηκε";
                titlegl.qu = true;
                ViewBag.message = "k";
                titles titles = db.titles.Find(id);
                foreach (titleauthor egg in db.titleauthor.Where(x => x.title_id == id))
                {
                    db.titleauthor.Remove(egg);
                }
                foreach (sales egg2 in db.sales.Where(x => x.title_id == id))
                {
                    foreach (stores egg3 in db.stores.Where(x => x.stor_id == egg2.stor_id))
                    {
                        db.stores.Remove(egg3);
                    }
                    db.sales.Remove(egg2);
                }
                foreach (roysched egg4 in db.roysched.Where(x => x.title_id == id))
                {
                    db.roysched.Remove(egg4);
                }
                db.titles.Remove(titles);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                titlegl.qu = true;
                titlegl.mes = "Η διαγραφη δεν ολοκληρωθηκε.";
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
