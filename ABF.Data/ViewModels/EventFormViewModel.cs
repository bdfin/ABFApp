using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ABF.Data.ABFDbModels;


namespace ABF.Data.ViewModels
{
    public class EventFormViewModel
    {
        public IEnumerable<Location> Locations { get; set; }
        public Event Event { get; set; }
        public Image Image { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
    }
}