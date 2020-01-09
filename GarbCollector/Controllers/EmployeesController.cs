﻿using System;
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
            //var employees = db.Employees.Include(e => e.ApplicationUser);
            //return View(employees.ToList());
            var currentUserId = User.Identity.GetUserId();
            var day = DateTime.Today.DayOfWeek;
            string stringDay = day.ToString();
            var employees = db.Employees.Include(e => e.ApplicationUser);
            return View();
        }

        public ActionResult Customers()
        {
            return RedirectToAction("Index", "Customers");
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
            if (customer.PickupConfirmation == true)
            {
                customer.balance = customer.balance + 25;
                db.SaveChanges();
            }
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
    
        public void FindZipCodes(int zip)
        {
            var customer = db.Customers.Find(zip);
            //db.Customers.;
        }
        
        public void ConfirmPickUp(bool confirmPickUp)
        {
            var pickUpConfirmed = db.Customers.Find(confirmPickUp);
            if (confirmPickUp == true)
            {
                
            }
        }
    }
}
