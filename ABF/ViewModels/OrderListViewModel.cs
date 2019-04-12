using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ABF.ViewModels
{
    public class OrderListViewModel
    {
        public int orderId { get; set; }
        public DateTime orderDate { get; set; }
        public DateTime orderTime { get; set; }
        public string customerName { get; set; }
        public string paymentMethod { get; set; }
        public string deliveryMethod { get; set; }
        public decimal price { get; set; }
    }
}