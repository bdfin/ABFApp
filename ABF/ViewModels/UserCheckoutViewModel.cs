using ABF.Data.ABFDbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ABF.ViewModels
{
    public class UserCheckoutViewModel
    {
        public Customer customer { get; set; }
        public decimal tickettotal { get; set; }
    }
}