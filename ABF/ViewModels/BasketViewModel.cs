using ABF.Data.ABFDbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ABF.ViewModels
{
    public class BasketViewModel
    {
        public Event Event { get; set; }

        public string LocationName { get; set; }

        public int Quantity { get; set; }
    }
}