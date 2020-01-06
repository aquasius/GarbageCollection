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
      public string ApplicationId { get; set; }
      public ApplicationUser ApplicationUser { get; set; }
      public string pickUpDay { get; set; }
      public string firstName { get; set; }
      public string lastName { get; set; }
      public int extraPickUpDate { get; set; }
      public string streetAddress { get; set; }
      public string city { get; set; }
      public string state { get; set; }
      public int zip { get; set; } 
      public double balance { get; set; }
      public DateTime suspendedStart { get; set; }
      public DateTime suspendedEnd { get; set; }
      public string pickupConfirmation { get; set; }



    }
}