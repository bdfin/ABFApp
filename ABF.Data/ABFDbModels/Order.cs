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
        [Display(Name = "Order ID")]
        public int Id { get; set; }

        [Display(Name = "Order Date")]
        [Required]
        public DateTime Date { get; set; }

        [Display(Name = "Order Time")]
        [Required]
        public DateTime Time { get; set; }

        [Display(Name = "Customer ID")]
        [Required]
        public string CustomerId { get; set; }

        [Display(Name = "Payment ID")]
        [Required]
        public string PaymentId { get; set; }

        [Display(Name = "Delivery Method")]
        [Required]
        public string Delivery { get; set; }

        [Display(Name = "Delivery Name")]
        public string DeliveryName { get; set; }

        [Display(Name = "House No/Name")]
        public string Address1 { get; set; }

        [Display(Name = "Street")]
        public string Address2 { get; set; }

        [Display(Name = "Town/City")]
        public string Address3 { get; set; }

        [Display(Name = "PostCode")]
        [DataType(DataType.PostalCode)]
        public string PostCode { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Phone")]
        public string PhoneNumber { get; set; }

        public virtual Payment Payment { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual IList<Ticket> Tickets { get; set; }
    }
}
