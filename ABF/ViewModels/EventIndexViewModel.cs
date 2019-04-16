using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ABF.ViewModels
{
    public class EventIndexViewModel
    {
        public List<EventListViewModel> Events { get; set; }

        public List<DateTime> Datelist { get; set; }

        public bool isMember { get; set; }
    }
}