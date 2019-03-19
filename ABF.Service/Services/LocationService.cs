using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABF.Data.ABFDbModels;
using ABF.Data.DAO;

namespace ABF.Service.Services
{
    public class LocationService
    {
        private LocationDAO locationDAO;

        public LocationService()
        {
            locationDAO = new LocationDAO();
        }

        public IList<Location> GetLocations()
        {
            return locationDAO.GetLocations();
        }

        public Location GetLocation(int id)
        {
            return locationDAO.GetLocation(id);
        }

        public void CreateLocation(Location location)
        {
            locationDAO.CreateLocation(location);
        }

        public void UpdateLocation(Location location)
        {
            locationDAO.UpdateLocation(location);
        }
    }
}
