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
        [Display(Name = "Location ID")]
        public int Id { get; set; }

        [Display(Name = "Location")]
        public string Name { get; set; }

        [Display(Name = "Address 1")]
        public string Address1 { get; set; }

        [Display(Name = "Address 2")]
        public string Address2 { get; set; }

        [Display(Name = "PostCode")]
        [DataType(DataType.PostalCode)]
        public string PostCode { get; set; }

        [Display(Name = "Co-Ordinates")]
        public string LatLong { get; set; }

        [Display(Name = "Disabled Access")]
        public bool DisabledAccess { get; set; }

        [Display(Name = "Venue Info")]
        [DataType(DataType.MultilineText)]
        public string Info { get; set; }

        [Display(Name = "Venue Contact")]
        public string Contact { get; set; }

        public string Test2 { get; set; }

        public virtual IList<Event> Events { get; set; }
    }
}
