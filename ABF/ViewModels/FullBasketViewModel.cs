using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ABF.Data.ABFDbModels;

namespace ABF.ViewModels
{
    public class FullBasketViewModel
    {
        public List<BasketViewModel> eventtickets { get; set; }

        public Dictionary<AddOn, int> addontickets { get; set; }

        public Dictionary<Event, int> UAeventtickets { get; set; }

        public Dictionary<AddOnEventViewModel, int> UAaddontickets { get; set; }

        public Dictionary<AddOnEventViewModel, int> RAaddontickets { get; set; }

        public MembershipType Membership { get; set; }
    }
}