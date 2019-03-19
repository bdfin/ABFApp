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
            IQueryable<Location> locations;

            locations = from location
                         in _context.Locations
                         select location;

            return locations.ToList();
        }

        public Location GetLocation(int id)
        {
            IQueryable<Location> locations;

            locations = from location
                        in _context.Locations
                        where location.Id == id
                        select location;

            return locations.ToList().First();
        }
    }
}
