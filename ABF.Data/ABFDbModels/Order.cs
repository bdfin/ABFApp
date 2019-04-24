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

        public string DeliveryName { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string PostCode { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public virtual Payment Payment { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual IList<Ticket> Tickets { get; set; }
    }
}
