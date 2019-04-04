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
        private AddOnService addOnService;

        public BookingsController()
        {
            locationService = new LocationService();
            eventService = new EventService();
            imageService = new ImageService();
            addOnService = new AddOnService();
        }

        // GET: Bookings
        public ActionResult Index()
        {
            var locationlist = locationService.GetLocations();
            var eList = eventService.GetEvents();
            var viewModelList = new List<EventListViewModel>();
            var indexview = new EventIndexViewModel();

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

            var datelist = eventService.GetUniqueDates();

            indexview.events = viewModelList;
            indexview.datelist = datelist.ToList();

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
            if (Session["Tix"] == null)
            {
                return View("BasketEmpty");
            }
            else
            {
                // empty list ready to be passed to view
                var basketviewmodel = new List<BasketViewModel>();

                // get Session["tix"] and store in Dictionary
                Dictionary<int, int> event_ticket = new Dictionary<int, int>();
                event_ticket = (Dictionary<int, int>)Session["Tix"];

                if (event_ticket.Count == 0)
                {
                    return View("BasketEmpty");
                }
                else
                {
                    // populate viewmodel list for view
                    foreach (KeyValuePair<int, int> e in event_ticket)
                    {
                        BasketViewModel basketentry = new BasketViewModel();
                        basketentry.Event = eventService.GetEvent(e.Key);
                        basketentry.LocationName = locationService.GetLocation(basketentry.Event.LocationId).Name;
                        basketentry.Quantity = e.Value;
                        basketviewmodel.Add(basketentry);
                    }

                    return View(basketviewmodel);
                }
            }
        }

        public ActionResult BasketwithAddOns()
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
                    var addondictionary = new Dictionary<int, int>();
                    addondictionary = (Dictionary<int, int>)Session["AddOns"];

                    // populate dictionary of addons
                    var fulladdons = new Dictionary<AddOn, int>();
                    foreach (KeyValuePair<int, int> a in addondictionary)
                    {
                        var thisaddon = addOnService.GetAddOn(a.Key);
                        fulladdons.Add(thisaddon, a.Value);
                    }

                    fullbasketviewmodel.addontickets = fulladdons;
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