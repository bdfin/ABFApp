using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ABF.Data.ABFDbModels;

namespace ABF.ViewModels
{
    public class PaymentIndexViewModel
    {
        [Display (Name = "Payment Id")]
        public string PaymentId { get; set; }

        [Display (Name = "Order Id")]
        public int OrderId { get; set; }

        [Display (Name ="Customer Name")]
        public string CustName { get; set; }

        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; }

        [Display (Name = "Delivery Method")]
        public string deliveryMethod { get; set; }

        [Display(Name = "Payment Method")]
        public string PaymentMethod { get; set; }

        [Display(Name ="Amount")]
        public decimal amount { get; set; }







    }
}





