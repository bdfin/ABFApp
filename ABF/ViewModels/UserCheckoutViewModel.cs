using ABF.Data.ABFDbModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ABF.ViewModels
{
    public class UserCheckoutViewModel
    {
        public Customer customer { get; set; }

        [Display(Name = "Order Total")]
        public decimal tickettotal { get; set; }
    }
}