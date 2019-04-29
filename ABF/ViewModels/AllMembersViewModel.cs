using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ABF.Data.ABFDbModels;

namespace ABF.ViewModels
{
    public class AllMembersViewModel
    {
        [Display(Name = "Customer")]
        public string Name { get; set; }

        [Display(Name = "Membership Type")]
        public string Type { get; set; }

        [Display(Name = "Membership Purchase")]
        public string DateBought { get; set; }

        [Display(Name = "Membership Expires?")]
        public bool Expiry { get; set; }
    }
}