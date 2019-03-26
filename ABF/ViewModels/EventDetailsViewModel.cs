using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABF.Data.ABFDbModels;

namespace ABF.ViewModels
{
    public class EventDetailsViewModel
    {
        public Location Location { get; set; }
        public Event Event { get; set; }
        public Image Image { get; set; }
    }
}
