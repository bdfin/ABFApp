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
            return View(eventService.GetEvent(id));
        }

        [Route("Admin/Events/New")]
        public ActionResult New()
        {
            var locations = locationService.GetLocations();

            var viewModel = new CreateEventViewModel
            {
                Locations = locations,
                Event = new Event()
                
            };

            return View(viewModel);
        }

        // POST: Events/Create
        [HttpPost]
        public ActionResult Create(CreateEventViewModel viewModel)
        {
            try
            {
              eventService.CreateEvent(viewModel);

              return RedirectToAction("Index", "AdminEvents");
            }
            catch
            {
                return View();
            }
        }

        // GET: Events/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Events/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
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
