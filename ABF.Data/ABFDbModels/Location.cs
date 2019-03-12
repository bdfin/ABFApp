using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABF.Data.ABFDbModels
{
    public class Location
    {
        public int Id { get; set; }

        [Display(Name = "Location")]
        public string Name { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string PostCode { get; set; }

        public bool DisabledAccess { get; set; }

        public string Info { get; set; }

        public string Contact { get; set; }

        public virtual IList<Event> Events { get; set; }
    }
}
