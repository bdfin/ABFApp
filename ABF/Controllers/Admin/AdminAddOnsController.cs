using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ABF.Service.Services;
using ABF.Data.ABFDbModels;
using ABF.ViewModels;

namespace ABF.Controllers.Admin
{
    public class AdminAddOnsController : Controller
    {
        private AddOnService addOnService;
        private EventService eventService;

        public AdminAddOnsController()
        {
            addOnService = new AddOnService();
            eventService = new EventService();
        }

        [Route("Admin/Addons")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SaveAddOn(EventFormViewModel viewModel)
        {
            viewModel.AddOn.EventId = viewModel.Event.Id;

            addOnService.SaveAddOn(viewModel.AddOn);

            return Redirect(Request.UrlReferrer.ToString());
        }

        [ChildActionOnly]
        public ActionResult GetEventAddOns(int id)
        {
            return PartialView("_AddOnsPartial", addOnService.GetAddOnsForEvent(id));
        }

    }
}
