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

        [Display(Name = "House No/Name")]
        public string address1 { get; set; }

        [Display(Name = "Street Name")]
        public string address2 { get; set; }

        [Display(Name = "Town/City")]
        public string address3 { get; set; }

        [Display(Name = "Postcode")]
        public string postcode { get; set; }

        [Display(Name = "Email Address")]
        public string email { get; set; }

        [Display(Name = "Phone Number")]
        public string phone { get; set; }

        public string paymentmethod { get; set; }

        public decimal total { get; set; }

        public bool ismember { get; set; }
        public bool updateneeded { get; set; }
    }

}