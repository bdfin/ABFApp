using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABF.Data.ABFDbModels
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public DateTime Time { get; set; }

        public int CustomerId { get; set; }

        public int PaymentId { get; set; }

        public string Delivery { get; set; }

        public virtual Payment Payment { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual IList<Ticket> Tickets { get; set; }
    }
}
