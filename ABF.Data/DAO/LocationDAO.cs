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

        public void CreateLocation(Location location)
        {
            _context.Locations.Add(location);
            _context.SaveChanges();
        }

        public void UpdateLocation(Location location)
        {
            Location locationToUpdate = GetLocation(location.Id);

            locationToUpdate.Name = location.Name;
            locationToUpdate.Address1 = location.Address1;
            locationToUpdate.Address2 = location.Address2;
            locationToUpdate.PostCode = location.PostCode;
            locationToUpdate.LatLong = location.LatLong;
            locationToUpdate.DisabledAccess = location.DisabledAccess;
            locationToUpdate.Info = location.Info;
            locationToUpdate.Contact = location.Contact;

            _context.SaveChanges();
        }

        public void DeleteLocation(Location location)
        {
            _context.Locations.Remove(location);
            _context.SaveChanges();
        }
    }
}
