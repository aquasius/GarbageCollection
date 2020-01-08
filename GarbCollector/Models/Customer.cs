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
      public string pickUpDay { get; set; }
      public string firstName { get; set; }
      public string lastName { get; set; }
      public int extraPickUpDate { get; set; }
      public string streetAddress { get; set; }
      public string city { get; set; }
      public string state { get; set; }
      public string zip { get; set; } 
      public double balance { get; set; }
      [Display(Name = "Suspended Start Ex: MM/DD/YYYY")]
      public DateTime suspendedStart { get; set; }
      [Display(Name = "Suspended End Ex: MM/DD/YYYY")]
      public DateTime suspendedEnd { get; set; }
      public bool pickupConfirmation { get; set; }

    }
}