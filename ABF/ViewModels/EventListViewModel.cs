using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ABF.Data.ABFDbModels;

namespace ABF.ViewModels
{
    public class EventListViewModel
    {
        public Event Event { get; set; }
        public Location Location { get; set; }
        public Image Image { get; set; }
        public IList<AddOn> AddOns { get; set; }

    }
}