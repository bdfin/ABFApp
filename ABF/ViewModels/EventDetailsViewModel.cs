using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Display(Name = "Availability")]
        public int EventAvailability { get; set; }

        public Image Image { get; set; }

        public Dictionary<AddOn, int> AddOnsAndAvailabilities { get; set; }

        public bool IsMember { get; set; }
    }
}
