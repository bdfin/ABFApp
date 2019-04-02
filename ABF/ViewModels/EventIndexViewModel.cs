using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ABF.ViewModels
{
    public class EventIndexViewModel
    {
        public List<EventListViewModel> events { get; set; }

        public List<DateTime> datelist { get; set; }
    }
}