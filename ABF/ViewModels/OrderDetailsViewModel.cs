using ABF.Data.ABFDbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ABF.ViewModels
{
    public class OrderDetailsViewModel
    {
        public Order order { get; set; }
        public Payment payment { get; set; }
        public Customer customer { get; set; }
        public IEnumerable<Ticket> ticketlist { get; set; }
    }
}