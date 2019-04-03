using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ABF.Service.Services;
using ABF.ViewModels;
using ABF.Data.ABFDbModels;

namespace ABF.Controllers
{
    public class EventsController : Controller
    {
        private EventService eventService;
        private ImageService imageService;
        private LocationService locationService;
        private AddOnService addOnService;

        public EventsController()
        {
            eventService = new EventService();
            imageService = new ImageService();
            locationService = new LocationService();
            addOnService = new AddOnService();
        }

        public ActionResult Index(DateTime? dateSearch)
        {
            var viewModelList = new List<EventListViewModel>();
            var eventList = eventService.GetEvents();
            var indexViewModel = new EventIndexViewModel();

            foreach (Event e in eventList)
            {
                var location = locationService.GetLocation(e.LocationId);
                var image = imageService.GetImage(e.ImageId);
                var addOns = addOnService.GetEventAddOns(e.Id);

                var viewModel = new EventListViewModel
                {
                    Event = e,
                    Location = location,
                    Image = image,
                    AddOns = addOns
                };

                viewModelList.Add(viewModel);
            }

            if (dateSearch != null)
            {
                viewModelList.RemoveAll(d => d.Event.Date != dateSearch);
            }

            indexViewModel.Events = viewModelList;

            return View(indexViewModel);
        }
    }
}