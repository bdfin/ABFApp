using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ABF.Data.ABFDbModels;


namespace ABF.Data.ViewModels
{
    public class CreateEventViewModel
    {
        public IEnumerable<Location> Locations { get; set; }
        public Event Event { get; set; }
    }
}