using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ABF.ViewModels
{
    public class OrderListViewModel
    {
        [Display(Name = "Order ID")]
        public int orderId { get; set; }

        [Display(Name = "Order Date")]
        public DateTime orderDate { get; set; }

        [Display(Name = "Order Time")]
        public DateTime orderTime { get; set; }

        [Display(Name = "Customer")]
        public string customerName { get; set; }

        [Display(Name = "Payment Method")]
        public string paymentMethod { get; set; }

        [Display(Name = "Delivery Method")]
        public string deliveryMethod { get; set; }

        [Display(Name = "Total")]
        public decimal price { get; set; }
    }
}