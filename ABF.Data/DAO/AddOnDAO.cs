using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABF.Data.ABFDbModels;

namespace ABF.Data.DAO
{
    public class AddOnDAO
    {
        private ABFDbContext _context;

        public AddOnDAO()
        {
            _context = new ABFDbContext();
        }

        public IList<AddOn> GetAllAddOns()
        {
            IQueryable<AddOn> addOns;

            addOns = from addOn
                     in _context.AddOns
                     select addOn;

            return addOns.ToList();
        }

        public IList<AddOn> GetAddOnsForEvent(int id)
        {
            IQueryable<AddOn> addOns;

            addOns = from addOn
                     in _context.AddOns
                     where addOn.EventId == id
                     select addOn;

            return addOns.ToList();
        }

        public void SaveAddOn(AddOn addOn)
        {
            _context.AddOns.Add(addOn);
            _context.SaveChanges();
        }
    }
}
