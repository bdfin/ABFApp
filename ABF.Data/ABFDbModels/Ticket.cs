using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABF.Data.ABFDbModels
{
    public class Ticket
    {
        public string Id { get; set; }

        public int? EventId { get; set; }

        public int? AddOnId { get; set; }

        public int OrderId { get; set; }

        public virtual AddOn AddOn { get; set; }
        public virtual Event Event { get; set; }
        public virtual Order Order { get; set; }
    }
}
