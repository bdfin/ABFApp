using ABF.Data.ABFDbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ABF.ViewModels
{
    public class OrderSuccessViewModel
    {
        public IList<Ticket> tickets { get; set; }
        public Order order { get; set; }
    }
}