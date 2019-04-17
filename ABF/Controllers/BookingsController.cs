using ABF.Data.ABFDbModels;
using ABF.Models;
using ABF.Service.Services;
using ABF.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Resources;
using System.Web;
using System.Web.Mvc;
using ABF.Controllers.Admin;
using Microsoft.AspNet.Identity;

namespace ABF.Controllers
{
    public class BookingsController : Controller
    {
        private LocationService locationService;
        private EventService eventService;
        private ImageService imageService;
        private AddOnService addOnService;
        private TicketService ticketService;
        private AdminTicketController adminticketcontroller;
        private CustomerService customerService;

        public BookingsController()
        {
            locationService = new LocationService();
            eventService = new EventService();
            imageService = new ImageService();
            addOnService = new AddOnService();
            ticketService = new TicketService();
            customerService = new CustomerService();
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

            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                var customermemberstate = customerService.GetCustomerByUserId(userId).MembershipTypeId;

                if (customermemberstate > 0)
                {
                    indexview.isMember = true;
                }
            }

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
            this.CheckAvailability();

            if (Session["Tix"] == null && Session["Membership"] == null && Session["UTix"] == null 
                && Session["UAddOns"] == null && Session["RAddOns"]==null)
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

                
                // check if there are any Unavailable Tix
                if (Session["UTix"] != null)
                {
                    var UT = (Dictionary<int, int>) Session["UTix"];
                    var UTviewModel = new Dictionary<Event, int>();

                    foreach (KeyValuePair<int, int> e in UT)
                    {
                        UTviewModel.Add(eventService.GetEvent(e.Key), e.Value);
                    }

                    fullbasketviewmodel.UAeventtickets = UTviewModel;
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

                // check if there are any Unavailable AddOns
                if (Session["UAddOns"] != null)
                {
                    var UAO = (Dictionary<int, int>)Session["UAddOns"];
                    var UAOviewModel = new Dictionary<AddOnEventViewModel, int>();

                    foreach (KeyValuePair<int, int> e in UAO)
                    {
                        var AOEviewModel = new AddOnEventViewModel()
                        {
                            addon = addOnService.GetAddOn(e.Key),
                            addonevent = eventService.GetEvent(addOnService.GetAddOn(e.Key).EventId)
                        };
                        UAOviewModel.Add(AOEviewModel, e.Value);
                    }

                    fullbasketviewmodel.UAaddontickets = UAOviewModel;
                }

                if (Session["RAddOns"] != null)
                {
                    var RAO = (Dictionary<int, int>)Session["RAddOns"];
                    var RAOviewModel = new Dictionary<AddOnEventViewModel, int>();

                    foreach (KeyValuePair<int, int> e in RAO)
                    {
                        var ROEviewModel = new AddOnEventViewModel()
                        {
                            addon = addOnService.GetAddOn(e.Key),
                            addonevent = eventService.GetEvent(addOnService.GetAddOn(e.Key).EventId)
                        };
                        RAOviewModel.Add(ROEviewModel, e.Value);
                    }

                    fullbasketviewmodel.RAaddontickets = RAOviewModel;
                }


                if (Session["Membership"] != null)
                {
                    fullbasketviewmodel.Membership = (MembershipType)Session["Membership"];
                }

                return View(fullbasketviewmodel);
            }
        }

        public bool CheckAvailability()
        {
            bool isChanged = false;

            if (Session["AddOns"] != null)
            {
                var unavailableAddOns = new Dictionary<int, int>();
                var availableAddOns = new Dictionary<int, int>();

                //get session Tix (and Add-Ons)
                var addOnsBasket = (Dictionary<int, int>)Session["AddOns"];

                //iterate through Add Ons dictionary first
                foreach (KeyValuePair<int, int> addon in addOnsBasket)
                {
                    // get Addon Data:
                    var addoncapacity = addOnService.GetAddOn(addon.Key).Quantity;

                    // get Sales Quantity for this addon:
                    var addonsSold = ticketService.GetAddOnSalesQuantityForEvent(addon.Key);

                    //get availability
                    var availability = addoncapacity - addonsSold;

                    //check to see if the number of tickets required is more than the available number
                    if (availability < addon.Value)
                    {
                        // calculate number of tickets to move to unavailable and add this to unavailable List
                        var quantityUnavailable = addon.Value - availability;
                        unavailableAddOns.Add(addon.Key, quantityUnavailable);
                        isChanged = true;

                        // check to see if ANY tickets are available, and keep these in available Basket
                        if (addon.Value - quantityUnavailable > 0)
                        {
                            // keep the number of available tickets in the basket
                            availableAddOns.Add(addon.Key, (addon.Value - quantityUnavailable));
                        }
                    }

                    // if the required number of tickets IS available:
                    else
                    {
                        availableAddOns.Add(addon.Key, addon.Value);
                    }
                }

                if (availableAddOns.Count != 0)
                {
                    Session["AddOns"] = availableAddOns;
                }
                else
                {
                    Session["AddOns"] = null;
                }

                if (unavailableAddOns.Count != 0)
                {
                    Session["UAddOns"] = unavailableAddOns;
                }
                else
                {
                    Session["UAddOns"] = null;
                }
            }

            if (Session["Tix"] != null)
            {
                var unavailableTix = new Dictionary<int, int>();
                var availableTix = new Dictionary<int, int>();

                var tixBasket = (Dictionary<int, int>)Session["Tix"];
                // iterate through Tix dictionary
                foreach (KeyValuePair<int, int> tix in tixBasket)
                {
                    // get Event Data:
                    var Eventcapacity = eventService.GetEvent(tix.Key).Capacity;
                    // get Sales Quantity for this Event:
                    var EventSold = ticketService.GetTicketSalesQuantityForEvent(tix.Key);
                    //get availability
                    var availability = Eventcapacity - EventSold;

                    //check to see if the number of tickets required is more than the available number
                    if (availability < tix.Value)
                    {
                        // calculate number of tickets to move to unavailable and add this to unavailable List
                        var quantityUnavailable = tix.Value - availability;
                        unavailableTix.Add(tix.Key, quantityUnavailable);
                        isChanged = true;

                        // check to see if ANY tickets are available, and keep these in available Basket
                        if (tix.Value - quantityUnavailable > 0)
                        {
                            // keep the number of available tickets in the basket
                            availableTix.Add(tix.Key, (tix.Value - quantityUnavailable));
                        }

                        // no tickets are available and all will be removed. Check for and remove any add-ons too.
                        else
                        {
                            if (Session["AddOns"] != null)
                            {
                                var removedaddons = new Dictionary<int, int>();
                                var addons = (Dictionary<int, int>) Session["AddOns"];

                                foreach (KeyValuePair<int, int> addon in addons)
                                {
                                    if (tix.Key == addOnService.GetAddOn(addon.Key).EventId)
                                    {
                                        removedaddons.Add(addon.Key, addon.Value);
                                    }
                                }

                                // update the Session Values
                                if (addons.Count == 0)
                                {
                                    Session["AddOns"] = null;
                                }
                                else
                                {
                                    Session["AddOns"] = addons;
                                }

                                if (removedaddons.Count != 0)
                                {
                                    Session["RAddOns"] = removedaddons;
                                }
                            }
                        }
                    }

                    // if the required number of tickets IS available:
                    else
                    {
                        availableTix.Add(tix.Key, tix.Value);
                    }
                }

                if (availableTix.Count != 0)
                {
                    Session["Tix"] = availableTix;
                }
                else
                {
                    Session["Tix"] = null;
                }

                if (unavailableTix.Count != 0)
                {
                    Session["UTix"] = unavailableTix;
                }
                else
                {
                    Session["UTix"] = null;
                }
            }

            return isChanged;
        }

        public ActionResult BasketNoCheck()
        {
            if (Session["Tix"] == null && Session["Membership"] == null && Session["UTix"] == null
                && Session["UAddOns"] == null && Session["RAddOns"] == null)
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


                // check if there are any Unavailable Tix
                if (Session["UTix"] != null)
                {
                    var UT = (Dictionary<int, int>)Session["UTix"];
                    var UTviewModel = new Dictionary<Event, int>();

                    foreach (KeyValuePair<int, int> e in UT)
                    {
                        UTviewModel.Add(eventService.GetEvent(e.Key), e.Value);
                    }

                    fullbasketviewmodel.UAeventtickets = UTviewModel;
                }


                if (Session["AddOns"] != null)
                {
                    // get Session["AddOns"] and store it in Dictionary
                    var addondictionary = new Dictionary<AddOn, int>();
                    var addonint = (Dictionary<int, int>)Session["AddOns"];

                    foreach (var ao in addonint)
                    {
                        var fulladdon = addOnService.GetAddOn(ao.Key);
                        var aoquantity = ao.Value;

                        addondictionary.Add(fulladdon, aoquantity);
                    }

                    fullbasketviewmodel.addontickets = addondictionary;
                }

                // check if there are any Unavailable AddOns
                if (Session["UAddOns"] != null)
                {
                    var UAO = (Dictionary<int, int>)Session["UAddOns"];
                    var UAOviewModel = new Dictionary<AddOnEventViewModel, int>();

                    foreach (KeyValuePair<int, int> e in UAO)
                    {
                        var AOEviewModel = new AddOnEventViewModel()
                        {
                            addon = addOnService.GetAddOn(e.Key),
                            addonevent = eventService.GetEvent(addOnService.GetAddOn(e.Key).EventId)
                        };
                        UAOviewModel.Add(AOEviewModel, e.Value);
                    }

                    fullbasketviewmodel.UAaddontickets = UAOviewModel;
                }

                if (Session["RAddOns"] != null)
                {
                    var RAO = (Dictionary<int, int>)Session["RAddOns"];
                    var RAOviewModel = new Dictionary<AddOnEventViewModel, int>();

                    foreach (KeyValuePair<int, int> e in RAO)
                    {
                        var ROEviewModel = new AddOnEventViewModel()
                        {
                            addon = addOnService.GetAddOn(e.Key),
                            addonevent = eventService.GetEvent(addOnService.GetAddOn(e.Key).EventId)
                        };
                        RAOviewModel.Add(ROEviewModel, e.Value);
                    }

                    fullbasketviewmodel.RAaddontickets = RAOviewModel;
                }


                if (Session["Membership"] != null)
                {
                    fullbasketviewmodel.Membership = (MembershipType)Session["Membership"];
                }

                return View("Basket", fullbasketviewmodel);
            }
        }

    }
}