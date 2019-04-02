using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ABF.Data.ABFDbModels;

namespace ABF.ViewModels
{
    public class CustomerFormViewModel
    {
        public Customer Customer { get; set; }
        public IEnumerable<MembershipType> MembershipTypes { get; set; }
    }
}