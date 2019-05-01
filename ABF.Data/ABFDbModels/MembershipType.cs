using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABF.Data.ABFDbModels
{
    public class MembershipType
    {
        [Display(Name = "Membership Type ID")]
        public int Id { get; set; }

        [Display(Name = "Membership Type")]
        public string Type { get; set; }

        [Display(Name = "Membership Expires?")]
        public bool Expiry { get; set; }

        [Display(Name = "Membership Price")]
        public decimal Price { get; set; }

        public virtual IList<Customer> Customers { get; set; }
    }
}
