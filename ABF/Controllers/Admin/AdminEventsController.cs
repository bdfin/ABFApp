using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ABF.Data.ABFDbModels;
using ABF.Service.Services;
using ABF.Data.ViewModels;

namespace ABF.Controllers.Admin
{
    public class AdminEventsController : Controller
    {
        private EventService eventService;
        private LocationService locationService;

        public AdminEventsController()
        {
            eventService = new EventService();
            locationService = new LocationService();
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

            if (e == null)
            {
                return HttpNotFound();
            }

            return View(e);
        }

        [Route("Admin/Events/New")]
        public ActionResult New()
        {
            var locations = locationService.GetLocations();

            var viewModel = new EventFormViewModel
            {
                Locations = locations

            };

            return View("EventForm", viewModel);
        }

        [HttpPost]
        public ActionResult Save(EventFormViewModel viewModel)
        {
            try
            {
                if (viewModel.Event.Id == 0)
                {
                    eventService.CreateEvent(viewModel);
                }
                else
                {
                    eventService.UpdateEvent(viewModel);
                }

                return RedirectToAction("Index", "AdminEvents");
            }
            catch
            {

                return View();
            }
        }

        [Route("Admin/Events/Edit/{id}")]
        public ActionResult Edit(int id)
        {
            var e = eventService.GetEvent(id);
            var locations = locationService.GetLocations();

            if (e == null)
            {
                return HttpNotFound();
            }
                
            var viewModel = new EventFormViewModel
            {
                Event = e,
                Locations = locations
            };

            return View("EventForm", viewModel);
        }

        // GET: Events/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Events/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
