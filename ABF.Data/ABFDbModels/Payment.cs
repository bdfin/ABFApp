using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABF.Data.ABFDbModels
{
    public class Payment
    {
        public int Id { get; set; }

        public string Method { get; set; }

        public virtual IList<Order> Orders { get; set; }
    }
}
