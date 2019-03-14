using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABF.Data.ABFDbModels
{
    public class Event
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [DataType(DataType.Time)]
        public DateTime Time { get; set; }

        public string Name { get; set; }

        [DataType(DataType.Text)]
        public string Details { get; set; }

        [DataType(DataType.Text)]
        public string Description { get; set; }

        public int Capacity { get; set; }

        [Display(Name = "Member Only")]
        public bool IsMemberOnly { get; set; }

        [Display(Name = "Location")]
        public int LocationId { get; set; }

        public int? ImageId { get; set; }

        public virtual Image Image { get; set; }
        public virtual Location Location { get; set; }
        public virtual IList<AddOn> AddOns { get; set; }
        public virtual IList<Ticket> Tickets { get; set; }
    }
}
