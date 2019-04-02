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
        public int Id { get; set; }

        public string Type { get; set; }

        public bool Expiry { get; set; }

        public virtual IList<Customer> Customers { get; set; }
    }
}
