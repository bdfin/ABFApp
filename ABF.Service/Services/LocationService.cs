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
        private LocationDAO _locationDAO;

        public LocationService()
        {
            _locationDAO = new LocationDAO();
        }

        public IList<Location> GetLocations()
        {
            return _locationDAO.GetLocations();
        }
    }
}
