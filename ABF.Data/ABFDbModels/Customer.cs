using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABF.Data.ABFDbModels
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string Address3 { get; set; }

        public string PostCode { get; set; }

        public string Email { get; set; }

        public int PhoneNumber { get; set; }

        public int? MembershipId { get; set; }

        public virtual Membership Membership { get; set; }
        public virtual IList<Ticket> Tickets { get; set; }
        public virtual IList<Order> Orders { get; set; }
    }
}
