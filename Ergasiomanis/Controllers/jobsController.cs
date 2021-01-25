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
            string minlvl = Request.QueryString["minlvl"];
            string maxlvl = Request.QueryString["maxlvl"];
            string jobDesc = Request.QueryString["jobDesc"];
            string showEmployees = Request.QueryString["showEmployees"];
            if (jobId != null && jobId != "")
            {
                short jobId2 = Convert.ToInt16(jobId);
                list = list.Where(m => m.job_id == jobId2);
                return View(list.ToList());
            }
            if(jobDesc != null && jobDesc.Trim() != "")
            {
                jobDesc = jobDesc.Trim();
                list = list.Where(m => m.job_desc.Contains(jobDesc));
                return View(list.ToList());
            }
            if (minlvl != null && minlvl != "" && (maxlvl == null || maxlvl == ""))
            {
                byte minlvl2 = Convert.ToByte(minlvl);
                list = list.Where(m => m.min_lvl >= minlvl2);
                return View(list.ToList());
            }
            if (maxlvl != null && maxlvl != "" && (minlvl == null || minlvl == ""))
            {
                byte maxlvl2 = Convert.ToByte(maxlvl);
                list = list.Where(m => m.max_lvl <= maxlvl2);
                return View(list.ToList());
            }
            if (maxlvl != null && maxlvl != "" && minlvl != null && minlvl != "")
            {
                byte minlvl2 = Convert.ToByte(minlvl);
                byte maxlvl2 = Convert.ToByte(maxlvl);
                list = list.Where(m => m.min_lvl >= minlvl2);
                list = list.Where(m => m.max_lvl <= maxlvl2);
                return View(list.ToList());
            }
            if(showEmployees == "on")
            {

            }
           
            return View(db.jobs.ToList());

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
