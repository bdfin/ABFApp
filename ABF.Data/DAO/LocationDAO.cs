using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABF.Data.ABFDbModels;

namespace ABF.Data.DAO
{
    public class LocationDAO
    {
        private ABFDbContext _context;

        public LocationDAO()
        {
            _context = new ABFDbContext();
        }

        public IList<Location> GetLocations()
        {
            IQueryable<Location> _locations;

            _locations = from location
                         in _context.Locations
                         select location;

            return _locations.ToList();
        }
    }
}
