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
    public class employeesController : Controller
    {
        private pubsEntities db = new pubsEntities();

        // GET: employees
        public ActionResult Index()
        {               
            IQueryable<employee> list = db.employee.Include(e => e.jobs).Include(e => e.publishers);
            string employeeLastName = Request.QueryString["employeeLastName"];
            string employeeFirstName = Request.QueryString["employeeFirstName"];
            string employeeMinit = Request.QueryString["employeeMinit"];
            string employeeFromJobLevel = Request.QueryString["employeeFromJobLevel"];
            string employeeToJobLevel = Request.QueryString["employeeToJobLevel"]; 
            string employeeFromHireDate = Request.QueryString["employeeFromHireDate"];
            string employeeToHireDate = Request.QueryString["employeeToHireDate"];
            string employeeJobDesc = Request.QueryString["employeeJobDesc"];
            string employeePublisherName = Request.QueryString["employeePublisherName"];
            string employeeId = Request.QueryString["employeeId"];
            if(employeeId != null && employeeId != "")
            {
                employeeId = employeeId.Trim();
                list = list.Where(m => m.emp_id.Contains(employeeId));
            }
            if(employeeLastName != null && employeeLastName != "")
            {
                employeeLastName = employeeLastName.Trim();
                list = list.Where(m => m.lname.Contains(employeeLastName));
            }
            if(employeeFirstName != null && employeeFirstName != "")
            {
                employeeFirstName = employeeFirstName.Trim();
                list = list.Where(m => m.fname.Contains(employeeFirstName));
            }
            if(employeeMinit !=null && employeeMinit != "")
            {
                employeeMinit = employeeMinit.Trim();
                list = list.Where(m => m.minit == employeeMinit);
            }
            if(employeeFromJobLevel !=null && employeeFromJobLevel !="")
            {
                byte employeeFromJobLevel2 = Convert.ToByte(employeeFromJobLevel);
                list = list.Where(m => m.job_lvl >= employeeFromJobLevel2);
            }
            if(employeeToJobLevel !=null && employeeToJobLevel != "")
            {
                byte employeeToJobLevel2 = Convert.ToByte(employeeToJobLevel);
                list = list.Where(m => m.job_lvl <= employeeToJobLevel2);    
            }
            if(employeeFromHireDate !=null && employeeToHireDate != "")
            {
                DateTime employeeFromHireDate2 = DateTime.Parse(employeeFromHireDate);
                list = list.Where(m => m.hire_date >= employeeFromHireDate2);
            }
            if(employeeToHireDate != null && employeeToHireDate != "")
            {
                DateTime employeeToHireDate2 = DateTime.Parse(employeeToHireDate);
                list = list.Where(m => m.hire_date <= employeeToHireDate2);
            }
            if(employeeJobDesc !=null && employeeJobDesc !="")
            {
                employeeJobDesc = employeeJobDesc.Trim();
                list = list.Where(m => m.jobs.job_desc.Contains(employeeJobDesc));
            }
            if(employeePublisherName != null && employeePublisherName != "")
            {
                employeePublisherName = employeePublisherName.Trim();
                list = list.Where(m => m.publishers.pub_name.Contains(employeePublisherName));
            }
            return View(list.ToList());
        }

        // GET: employees/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            employee employee = db.employee.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: employees/Create
        public ActionResult Create()
        {
            ViewBag.job_id = new SelectList(db.jobs, "job_id", "job_desc");
            ViewBag.pub_id = new SelectList(db.publishers, "pub_id", "pub_name");
            return View();
        }

        // POST: employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "emp_id,fname,minit,lname,job_id,job_lvl,pub_id,hire_date")] employee employee)
        {
            if (ModelState.IsValid)
            {
                db.employee.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.job_id = new SelectList(db.jobs, "job_id", "job_desc", employee.job_id);
            ViewBag.pub_id = new SelectList(db.publishers, "pub_id", "pub_name", employee.pub_id);
            return View(employee);
        }

        // GET: employees/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            employee employee = db.employee.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.job_id = new SelectList(db.jobs, "job_id", "job_desc", employee.job_id);
            ViewBag.pub_id = new SelectList(db.publishers, "pub_id", "pub_name", employee.pub_id);
            return View(employee);
        }

        // POST: employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "emp_id,fname,minit,lname,job_id,job_lvl,pub_id,hire_date")] employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.job_id = new SelectList(db.jobs, "job_id", "job_desc", employee.job_id);
            ViewBag.pub_id = new SelectList(db.publishers, "pub_id", "pub_name", employee.pub_id);
            return View(employee);
        }

        // GET: employees/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            employee employee = db.employee.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            employee employee = db.employee.Find(id);
            db.employee.Remove(employee);
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
