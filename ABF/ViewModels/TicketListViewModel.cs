using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ABF.ViewModels
{
    public class TicketListViewModel
    {
        public string ticketId { get; set; }
        public int orderId { get; set; }
        public DateTime orderDate { get; set; }
        public string customerName { get; set; }
        public string eventName { get; set; }
    }
}