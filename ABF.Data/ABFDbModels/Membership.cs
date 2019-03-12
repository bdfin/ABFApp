using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABF.Data.ABFDbModels
{
    public class Membership
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public DateTime DatePurchased { get; set; }

        public DateTime Expiry { get; set; }

        [Required]
        public virtual Customer Customer { get; set; }
    }
}
