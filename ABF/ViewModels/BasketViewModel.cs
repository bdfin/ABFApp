using ABF.Data.ABFDbModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ABF.ViewModels
{
    public class BasketViewModel
    {
        public Event Event { get; set; }

        [Display(Name = "Location")]
        public string LocationName { get; set; }

        [Display(Name = "Quantity")]
        public int Quantity { get; set; }
    }
}