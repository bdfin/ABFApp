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
        public string Id { get; set; }

        [Required]
        public string Method { get; set; }

        [Required]
        public decimal Amount {get; set;}
       
        public virtual IList<Order> Orders { get; set; }
    }
}
