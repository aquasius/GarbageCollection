using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GarbCollector.Models
{
    public class EmployeeHomeViewModel
    {
        public List<Customer> Customers { get; set; }
        public SelectList DaysOfWeek { get; set; }
        public string SelectedDay { get; set; }

        public bool PickUpConfirmed { get; set; }
    }
}