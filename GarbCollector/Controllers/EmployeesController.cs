using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using GarbCollector.Models;
using Microsoft.AspNet.Identity;

namespace GarbCollector.Controllers
{
    public class EmployeesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Employees
        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();
            Employee employee = db.Employees.Where(e => e.ApplicationId == userId).SingleOrDefault();
            var today = Convert.ToString(DateTime.Now.DayOfWeek);
            var todayDate = Convert.ToString(DateTime.Now.Date);
            EmployeeHomeViewModel viewModel = new EmployeeHomeViewModel();
            viewModel.Customers = db.Customers.Include(e => e.ApplicationUser).Where(c => c.Zip == employee.zipCode && (c.PickUpDay == today || c.ExtraPickUpDate == todayDate)).ToList();
            viewModel.DaysOfWeek = new SelectList(new List<string>() { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"});
            return View(viewModel);

        }
        [HttpPost]

        public ActionResult Index(EmployeeHomeViewModel employeeView)
        {
            string userId = User.Identity.GetUserId();
            Employee employee = db.Employees.Where(e => e.ApplicationId == userId).SingleOrDefault();
            EmployeeHomeViewModel viewModel = new EmployeeHomeViewModel();
            viewModel.Customers = db.Customers.Include(e => e.ApplicationUser).Where(c => c.Zip == employee.zipCode && c.PickUpDay == employeeView.SelectedDay).ToList();
            viewModel.DaysOfWeek = new SelectList(new List<string>() { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" });
            return View(viewModel);

        }

        public ActionResult GetPickUpDay(string DayOfWeek)
        {
            #region ViewBag
            List<SelectListItem> dayOfWeek = new List<SelectListItem>() {
               new SelectListItem
               {
                   Text = "Sunday", Value = "Sunday"
               },
               new SelectListItem
               {
                   Text= "Monday", Value = "Monday"
               },
               new SelectListItem
               {
                   Text = "Tuesday", Value = "Tuesday"
               },
               new SelectListItem
               {
                    Text = "Wednesday", Value = "Wednesday"
               },
               new SelectListItem
               {
                   Text = "Thursday", Value = "Thursday"
               },
               new SelectListItem
               {
                   Text = "Friday", Value = "Friday"
               },
               new SelectListItem
               {
                   Text = "Saturday", Value = "Saturday"
               },
            };
            ViewBag.DayOfWeek = dayOfWeek;
            #endregion
            return View();
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Employee employee = db.Employees.Find(id);
            //if (employee == null)
            //{
            //    return HttpNotFound();
            //}
            //return View(employee);
            
                Customer customer = null;
                if (id == null)
                {
                    var FoundUserId = User.Identity.GetUserId();
                    customer = db.Customers.Where(c => c.ApplicationId == FoundUserId).FirstOrDefault();
                    return View(customer);
                }

                else
                {
                    customer = db.Customers.Find(id);
                }
                if (customer == null)
                {
                    return HttpNotFound();
                }
                return View(customer);

            
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            ViewBag.ApplicationId = new SelectList(db.Users, "Id", "Email");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ApplicationId,firstName,lastName,zipCode")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                employee.ApplicationId = User.Identity.GetUserId();
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ApplicationId = new SelectList(db.Users, "Id", "Email", employee.ApplicationId);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.ApplicationId = new SelectList(db.Users, "Id", "Email", employee.ApplicationId);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
            [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Zip,Address,PickupStartDate,PickupEndDate,DayOfWeek,ApplicationUserId")] Customer customer)
        {
            //if (customer.PickupConfirmation == true)
            //{
            //    customer.balance = customer.balance + 25;
            //    db.SaveChanges();
            //}
            if (ModelState.IsValid)
            {
                var customerInDb = db.Customers.Where(x => x.Id == customer.Id).Single();

                //db.Entry(customer).State = EntityState.Modified;

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ApplicationUserId = new SelectList(db.Users, "Id", "UserRole", customer.Id);
            return View(customer);
        }



    

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
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
    
       
        
        public ActionResult ConfirmPickUp(int? id)
        {
            var Id = id;
            EmployeeHomeViewModel model = new EmployeeHomeViewModel();
            model.DaysOfWeek = new SelectList(new List<string>() { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" });
            var customerIdFind = db.Customers.Where(x => x.Id == Id).Single();
            if (customerIdFind.PickupConfirmation != true)
            {
                customerIdFind.PickupConfirmation = true;
                customerIdFind.balance += 25;
                db.SaveChanges();
                var customers = db.Customers.Where(c => c.Id == customerIdFind.Id).ToList();
                model.Customers = customers;
                return View("Index", model);
            }

            return RedirectToAction("Index");
        }
    }
}
