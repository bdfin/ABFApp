using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABF.Data.ABFDbModels
{
    public class Payment
    {
        [Display(Name = "Payment ID")]
        public string Id { get; set; }

        [Display(Name = "Payment Method")]
        [Required]
        public string Method { get; set; }

        [Display(Name = "Payment Amount")]
        [Required]
        public decimal Amount {get; set;}
    
        public virtual IList<Order> Orders { get; set; }
    }
}
