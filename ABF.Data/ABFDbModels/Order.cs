using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABF.Data.ABFDbModels
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public DateTime Time { get; set; }

        [Required]
        public string CustomerId { get; set; }

        [Required]
        public string PaymentId { get; set; }

        [Required]
        public string Delivery { get; set; }

        public virtual Payment Payment { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual IList<Ticket> Tickets { get; set; }
    }
}
