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

        
       // [Display(Name = "Membership Type")]
        // public int? Type { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name ="Membership Id")]
        public int? MembershipTypeId { get; set; }

        public string Type { get; set; }
    }
}