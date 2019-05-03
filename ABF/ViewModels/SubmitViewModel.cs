using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ABF.Data.ABFDbModels;

namespace ABF.ViewModels
{
    public class SubmitViewModel
    {
        [Required]
        [Display(Name = "Full Name")]
        public string name { get; set; }

        [Required]
        [Display(Name = "House No/Name")]
        public string address1 { get; set; }

        [Required]
        [Display(Name = "Street Name")]
        public string address2 { get; set; }

        [Required]
        [Display(Name = "Town/City")]
        public string address3 { get; set; }

        [Required]
        [Display(Name = "Postcode")]
        public string postcode { get; set; }

        [Required]
        [Display(Name = "Email Address")]
        public string email { get; set; }

        [Required]
        [Display(Name = "Phone Number")]
        public string phone { get; set; }

        [Required]
        public string paymentmethod { get; set; }

        public decimal total { get; set; }

        public bool isuser { get; set; }
        public bool updateneeded { get; set; }
    }

}