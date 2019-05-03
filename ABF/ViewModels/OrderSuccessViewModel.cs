using ABF.Data.ABFDbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ABF.ViewModels
{
    public class OrderSuccessViewModel
    {
        public IList<TicketInfoViewModel> tickets { get; set; }

        public Order order { get; set; }

        public bool newmember { get; set; }
    }
}