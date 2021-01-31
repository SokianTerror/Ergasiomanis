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
    public class jobsController : Controller
    {
        private pubsEntities db = new pubsEntities();

        // GET: jobs
        public ActionResult Index()
        {
            IQueryable<jobs> list = db.jobs;

            string jobId = Request.QueryString["jobId"];
            string FromMinlvl = Request.QueryString["FromMinlvl"];
            string ToMinlvl = Request.QueryString["ToMinlvl"];
            string FromMaxlvl = Request.QueryString["FromMaxlvl"];
            string ToMaxlvl = Request.QueryString["ToMaxlvl"];
            string jobDesc = Request.QueryString["jobDesc"];            
            if (jobId != null && jobId != "")
            {
                short jobId2 = Convert.ToInt16(jobId);
                list = list.Where(m => m.job_id == jobId2);                
            }
            if(jobDesc != null && jobDesc.Trim() != "")
            {
                jobDesc = jobDesc.Trim();
                list = list.Where(m => m.job_desc.Contains(jobDesc));                
            }
            if(FromMinlvl != null && FromMinlvl != "")
            {
                byte FromMinlvl2 = Convert.ToByte(FromMinlvl);
                list = list.Where(m => m.min_lvl >= FromMinlvl2);
            }
            if(ToMinlvl != null && ToMinlvl != "")
            {
                byte ToMinlvl2 = Convert.ToByte(ToMinlvl);
                list = list.Where(m => m.min_lvl <= ToMinlvl2);
            }
            if(FromMaxlvl != null && FromMaxlvl != "")
            {
                byte FromMaxlvl2 = Convert.ToByte(FromMaxlvl);
                list = list.Where(m => m.max_lvl >= FromMaxlvl2);
            }
            if(ToMaxlvl != null && ToMaxlvl !="")
            {
                byte ToMaxlvl2 = Convert.ToByte(ToMaxlvl);
                list = list.Where(m => m.max_lvl <= ToMaxlvl2);
            }      
            return View(list.ToList());

        }

        // GET: jobs/Details/5
        public ActionResult Details(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            jobs jobs = db.jobs.Find(id);
            if (jobs == null)
            {
                return HttpNotFound();
            }
            return View(jobs);
        }

        // GET: jobs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: jobs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "job_id,job_desc,min_lvl,max_lvl")] jobs jobs)
        {
            if (ModelState.IsValid)
            {
                db.jobs.Add(jobs);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(jobs);
        }

        // GET: jobs/Edit/5
        public ActionResult Edit(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            jobs jobs = db.jobs.Find(id);
            if (jobs == null)
            {
                return HttpNotFound();
            }
            return View(jobs);
        }

        // POST: jobs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "job_id,job_desc,min_lvl,max_lvl")] jobs jobs)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jobs).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(jobs);
        }

        // GET: jobs/Delete/5
        public ActionResult Delete(short? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            jobs jobs = db.jobs.Find(id);
            if (jobs == null)
            {
                return HttpNotFound();
            }
            return View(jobs);
        }

        // POST: jobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(short id)
        {
            foreach(employee egg in db.employee.Where(x=> x.job_id == id))
            {
                db.employee.Remove(egg);
            }
            jobs jobs = db.jobs.Find(id);
            db.jobs.Remove(jobs);
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
