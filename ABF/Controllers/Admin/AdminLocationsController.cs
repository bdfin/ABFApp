using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ABF.Service.Services;

namespace ABF.Controllers.Admin
{
    public class AdminLocationsController : Controller
    {
        private LocationService locationService;

        public AdminLocationsController()
        {
            locationService = new LocationService();
        }

        [Route("Admin/Locations")]
        public ActionResult Index()
        {
            return View(locationService.GetLocations());
        }

       [Route("Admin/Locations/Details/{id}")]
       public ActionResult Details(int id)
        {
            var location = locationService.GetLocation(id);

            if (location == null)
            {
                return HttpNotFound();
            }

            return View(location);
        }
    }
}
