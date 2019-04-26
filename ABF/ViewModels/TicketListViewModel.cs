using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ABF.ViewModels
{
    public class TicketListViewModel
    {
        [Display(Name = "Ticket ID")]
        public string ticketId { get; set; }

        [Display(Name = "Order ID")]
        public int orderId { get; set; }

        [Display(Name = "Order Date")]
        public DateTime orderDate { get; set; }

        [Display(Name = "Customer")]
        public string customerName { get; set; }

        [Display(Name = "Event Name")]
        public string eventName { get; set; }
    }
}