using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ABF.Service.Services;
using ABF.Data.ABFDbModels;

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

        [Route("Admin/Locations/New")]
        public ActionResult New()
        {
            var location = new Location();

            return View("LocationForm", location);
        }

        [HttpPost]
        public ActionResult Save(Location location)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new Location();

                return View("LocationForm", viewModel);
            }

            if (location.Id == 0)
            {
                locationService.CreateLocation(location);
            }
            else
            {
                locationService.UpdateLocation(location);
            }

            return RedirectToAction("Index", "AdminLocations");
        }

        [Route("Admin/Locations/Edit/{id}")]
        public ActionResult Edit(int id)
        {
            var location = locationService.GetLocation(id);

            if (location == null)
            {
                return HttpNotFound();
            }

            return View("LocationForm", location);
        }
    }
}
