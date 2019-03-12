using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABF.Data.ABFDbModels
{
    public class Ticket
    {
        public int Id { get; set; }

        public double Price { get; set; }

        public int EventId { get; set; }

        public bool IsChild { get; set; }

        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual Event Event { get; set; }
    }
}
