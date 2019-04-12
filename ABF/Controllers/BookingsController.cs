using ABF.Data.ABFDbModels;
using ABF.Models;
using ABF.Service.Services;
using ABF.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ABF.Controllers.Admin;

namespace ABF.Controllers
{
    public class BookingsController : Controller
    {
        private LocationService locationService;
        private EventService eventService;
        private ImageService imageService;
        private AddOnService addOnService;
        private AdminTicketController adminticketcontroller;

        public BookingsController()
        {
            locationService = new LocationService();
            eventService = new EventService();
            imageService = new ImageService();
            addOnService = new AddOnService();
            adminticketcontroller = new AdminTicketController();
        }

        // GET: Bookings
        public ActionResult Index()
        {
            var locationlist = locationService.GetLocations();
            var eList = eventService.GetEvents();
            var viewModelList = new List<EventListViewModel>();
            var indexview = new EventIndexViewModel();

            // construct a list of all event Ids which have add-ons
            var ao = addOnService.GetAllAddOns();
            var eventswithaddons = new List<int>();
            foreach (var addon in ao)
            {
                eventswithaddons.Add(addon.EventId);
            }

            foreach (Event Singleevent in eList)
            {
                var e = Singleevent;
                var l = locationService.GetLocation(e.LocationId);
                var a = adminticketcontroller.GetAvailability(e.Id);
                var hasao = false;

                if (eventswithaddons.Contains(e.Id))
                {
                    hasao = true;
                }
                
                var viewModel = new EventListViewModel
                {
                    Event = e,
                    Location = l,
                    availability = a,
                    hasAddOn = hasao,  
                };

                viewModelList.Add(viewModel);
            }

            var datelist = eventService.GetUniqueDates();

            indexview.Events = viewModelList;
            indexview.Datelist = datelist.ToList();

            return View(indexview);
        }

        public ActionResult Details(int id)
        {
            var thisevent = eventService.GetEvent(id);
            var eventlocation = locationService.GetLocation(thisevent.LocationId);
            var eventimage = imageService.GetImage(thisevent.ImageId);
            var eventaddons = addOnService.GetEventAddOns(id);

            var viewmodel = new EventDetailsViewModel
            {
                Event = thisevent,
                Location = eventlocation,
                Image = eventimage,
                AddOns = eventaddons.ToList()
            };

            return View(viewmodel);
        }

        public ActionResult UniqueDates()
        {
            var datelist = eventService.GetUniqueDates();
            var stringdatelist = new List<string>();

            foreach (var item in datelist)
            {
                stringdatelist.Add(item.ToString());
            }
            return View("_DateList", stringdatelist);
        }

        public ActionResult Basket()
        {
            if (Session["Tix"] == null && (Session["Membership"] == null))
            {
                return View("BasketEmpty");
            }
            else
            {
                // empty full basket view model
                var fullbasketviewmodel = new FullBasketViewModel();

                // empty list of events ready to be passed to viewmodel
                var basketviewmodel = new List<BasketViewModel>();

                // get Session["Tix"] and store in Dictionary
                var event_ticket = new Dictionary<int, int>();
                event_ticket = (Dictionary<int, int>)Session["Tix"];

                if (event_ticket != null)
                {
                    // populate events viewmodel list
                    foreach (KeyValuePair<int, int> e in event_ticket)
                    {
                        BasketViewModel basketentry = new BasketViewModel();
                        basketentry.Event = eventService.GetEvent(e.Key);
                        basketentry.LocationName = locationService.GetLocation(basketentry.Event.LocationId).Name;
                        basketentry.Quantity = e.Value;
                        basketviewmodel.Add(basketentry);
                    }

                    fullbasketviewmodel.eventtickets = basketviewmodel;
                }

                if (Session["AddOns"] != null)
                {
                    // get Session["AddOns"] and store it in Dictionary
                    var addondictionary = new Dictionary<AddOn, int>();
                    var addonint = (Dictionary<int, int>) Session["AddOns"];

                    foreach (var ao in addonint)
                    {
                        var fulladdon = addOnService.GetAddOn(ao.Key);
                        var aoquantity = ao.Value;

                        addondictionary.Add(fulladdon, aoquantity);
                    }

                    fullbasketviewmodel.addontickets = addondictionary;
                }



                if (Session["Membership"] != null)
                {
                    fullbasketviewmodel.Membership = (MembershipType)Session["Membership"];
                }

                return View(fullbasketviewmodel);
            }
        }
    }
}