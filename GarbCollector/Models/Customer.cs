using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GarbCollector.Models
{
    public class Customer
    {
      [Key]
      
      public int Id { get; set; }
      [ForeignKey("ApplicationUser")]
      [Display(Name = "Application Id")]
      public string ApplicationId { get; set; }
      [Display(Name = "Application User")]
      public ApplicationUser ApplicationUser { get; set; }
      [Display(Name = "Pick Up Day")]
      public string PickUpDay { get; set; }
      [Display(Name = "First Name")]
      public string FirstName { get; set; }
      [Display(Name = "Last Name")]
      public string LastName { get; set; }
      [Display(Name = "Extra Pick Up Date")]
      public string ExtraPickUpDate { get; set; }
      [Display(Name = "Street Address")]
      public string StreetAddress { get; set; }
      [Display(Name = "City")]
      public string City { get; set; }

      public string State { get; set; }
      public int Zip { get; set; } 
        [Display(Name = "Balance Due:")]
      public double balance { get; set; }
      [Display(Name = "Suspended Start Ex: MM/DD/YYYY")]
      public DateTime SuspendedStart { get; set; }
      [Display(Name = "Suspended End Ex: MM/DD/YYYY")]
      public DateTime SuspendedEnd { get; set; }
      public bool PickupConfirmation { get; set; }

    }
}