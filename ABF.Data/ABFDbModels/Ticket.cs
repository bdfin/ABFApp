using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABF.Data.ABFDbModels
{
    public class Ticket
    {
        [Display(Name = "Ticket ID")]
        public string Id { get; set; }

        [Display(Name = "Event ID")]
        public int? EventId { get; set; }

        [Display(Name = "Add-On ID")]
        public int? AddOnId { get; set; }

        [Required]
        [Display(Name = "Order ID")]
        public int OrderId { get; set; }

        public virtual AddOn AddOn { get; set; }
        public virtual Event Event { get; set; }
        public virtual Order Order { get; set; }
    }
}
