using ABF.Data.ABFDbModels;
using ABF.Models;
using ABF.Service.Services;
using ABF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ABF.Controllers
{
    public class BookingsController : Controller
    {
        private LocationService locationService;
        private EventService eventService;
        private ImageService imageService;

        public BookingsController()
        {
            locationService = new LocationService();
            eventService = new EventService();
            imageService = new ImageService();
        }

        // GET: Bookings
        public ActionResult Index()
        {
            var locationlist = locationService.GetLocations();
            var eList = eventService.GetEvents();
            var viewModelList = new List<EventListViewModel>();

            foreach (Event Singleevent in eList)
            {
                var e = Singleevent;
                var l = locationService.GetLocation(e.LocationId);

                var viewModel = new EventListViewModel
                {
                    thisevent = e,
                    thislocation = l
                };

                viewModelList.Add(viewModel);
            }

            return View(viewModelList);
        }

        public ActionResult Details(int id)
        {
            var thisevent = eventService.GetEvent(id);
            var eventlocation = locationService.GetLocation(thisevent.LocationId);
            var eventimage = imageService.GetImage(thisevent.ImageId);

            var viewmodel = new EventDetailsViewModel
            {
                Event = thisevent,
                Location = eventlocation,
                Image = eventimage
            };

            return View(viewmodel);
        }
    }
}