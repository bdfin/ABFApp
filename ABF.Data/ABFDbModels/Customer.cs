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

        public string IdentityId { get; set; }

        [Required]
        public string Name { get; set; }

        [Display(Name ="House Number/Name")]
        public string Address1 { get; set; }

        [Display(Name="Street Name")]
        public string Address2 { get; set; }

        [Display(Name="Town/City")]
        public string Address3 { get; set; }

        [Display(Name="Post Code")]
        [DataType(DataType.PostalCode)]
        public string PostCode { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]        
        public string PhoneNumber { get; set; }

        [Display(Name = "Membership Type")]
        public int? MembershipTypeId { get; set; }

        public virtual MembershipType MembershipType { get; set; }
        public virtual IList<Ticket> Tickets { get; set; }
        public virtual IList<Order> Orders { get; set; }
    }
}
