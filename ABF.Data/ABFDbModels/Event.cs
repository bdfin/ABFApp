using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABF.Data.ABFDbModels
{
    public class Event
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public DateTime Time { get; set; }

        public string Name { get; set; }

        public string Details { get; set; }

        public string Description { get; set; }

        public int Capacity { get; set; }

        public bool IsMemberOnly { get; set; }

        public int LocationId { get; set; }

        public int? ImageId { get; set; }

        public virtual Image Image { get; set; }
        public virtual Location Location { get; set; }
        public virtual IList<AddOn> AddOns { get; set; }
        public virtual IList<Ticket> Tickets { get; set; }
    }
}
