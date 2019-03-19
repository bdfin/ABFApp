using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ABF.Data.ABFDbModels;
using ABF.Service.Services;
using ABF.ViewModels;

namespace ABF.Controllers.Admin
{
    public class AdminEventsController : Controller
    {
        private EventService eventService;
        private ImageService imageService;
        private LocationService locationService;
        private AddOnService addOnService;

        public AdminEventsController()
        {
            eventService = new EventService();
            imageService = new ImageService();
            locationService = new LocationService();
            addOnService = new AddOnService();
        }

        [Route("Admin/Events")]
        public ActionResult Index()
        {
            return View(eventService.GetEvents());
        }

        [Route("Admin/Events/Details/{id}")]
        public ActionResult Details(int id)
        {
            var e = eventService.GetEvent(id);

            var viewModel = new EventDetailsViewModel
            {
                Event = e,
                Image = imageService.GetImage(e.ImageId)
            };

            if (viewModel == null)
            {
                return HttpNotFound();
            }

            return View(viewModel);
        }


        [Route("Admin/Events/New")]
        public ActionResult New()
        {
            var locations = locationService.GetLocations();

            var viewModel = new EventFormViewModel
            {
                Locations = locations,
                Event = new Event(),
                Image = new Image()
            };

            viewModel.Event.Date = DateTime.Now;

            return View("EventForm", viewModel);
        }

        [HttpPost]
        public ActionResult Save(EventFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var newViewModel = new EventFormViewModel
                {
                    Locations = locationService.GetLocations(),
                    Event = viewModel.Event,
                    Image = new Image()
                };

                return View("EventForm", newViewModel);
            }

            if (viewModel.Event.Id == 0)
            {
                if (viewModel.ImageFile != null)
                {
                    viewModel.Image = new Image();

                    int newImageId = imageService.GetNewImageId();

                    viewModel.Image.ImageFile = viewModel.ImageFile;
                    viewModel.Image.Id = newImageId;
                    viewModel.Event.ImageId = newImageId;

                    imageService.SaveImage(viewModel.Image);
                    eventService.CreateEvent(viewModel.Event);
                }
                else
                {
                    eventService.CreateEvent(viewModel.Event);
                }
            }
            else
            {
                eventService.UpdateEvent(viewModel.Event);
            }

            return RedirectToAction("Index", "AdminEvents");
        }

        [Route("Admin/Events/Edit/{id}")]
        public ActionResult Edit(int id)
        {
            var e = eventService.GetEvent(id);
            var image = imageService.GetImage(e.ImageId);
            var locations = locationService.GetLocations();

            if (e == null)
            {
                return HttpNotFound();
            }
                
            var viewModel = new EventFormViewModel
            {
                Event = e,
                Locations = locations,
                Image = image
            };

            return View("EventForm", viewModel);
        }

        [Route("Admin/Events/Delete/{id}")]
        public ActionResult Delete(int id)
        {
            var e = eventService.GetEvent(id);

            if (e == null)
            {
                return HttpNotFound();
            }

            return View(e);
        }

        [HttpPost]
        public ActionResult DeleteEvent(int id)
        {
            var e = eventService.GetEvent(id);
            var image = imageService.GetImage(e.ImageId);

            imageService.DeleteImage(image);
            eventService.DeleteEvent(e);

            return RedirectToAction("Index", "AdminEvents");
        }
    }
}
