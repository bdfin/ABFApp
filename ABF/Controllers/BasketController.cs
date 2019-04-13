using ABF.Data.ABFDbModels;
using ABF.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Protocols;
using Microsoft.AspNet.Identity;

namespace ABF.Controllers
{
    public class BasketController : Controller
    {
        private AddOnService addonService;
        private CustomerService customerService;
        private EventService eventService;
        private TicketService ticketService;

        public BasketController()
        {
            addonService = new AddOnService();
            customerService = new CustomerService();
            ticketService = new TicketService();
            eventService = new EventService();
        }

        // GET: Basket
        public ActionResult Index()
        {
            return View("Basket", "Bookings");
        }

        public ActionResult AddToBasket(int eventId, int quantity)
        {
            if (quantity == 0)
            {
                ViewBag.Message = "Can't add 0 tickets to Basket";
                return RedirectToAction("Basket", "Bookings");
            }
            else
            {
                var model = new Dictionary<int, int>();

                if (Session["Tix"] == null)
                {
                    model.Add(eventId, quantity);
                    Session["Tix"] = model;
                }

                else
                {
                    model = (Dictionary<int, int>) Session["Tix"];

                    if (model.ContainsKey(eventId))
                    {
                        model[eventId] += quantity;
                    }
                    else
                    {
                        model.Add(eventId, quantity);
                        Session["Tix"] = model;
                    }
                }

                return RedirectToAction("Basket", "Bookings");
            }

        }

        public ActionResult Membership()
        {
            bool isMember = false;

            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                var customermembershipstate = customerService.GetCustomerByUserId(userId).MembershipTypeId;
                if (customermembershipstate != null)
                {
                    isMember = true;
                }
            }

            return View(isMember);
        }

        public ActionResult AddMembership(int membershipId)
        {
            if (Session["Membership"] != null)
            {
                ViewBag.Message = "You already have a Membership in your Basket. " +
                                  "To change your membership choice you must delete this from the basket first.";
                return View("Error");
            }
            else
            {
                var ms = new MembershipTypeService();
                var membershiptype = ms.GetMembershipType(membershipId);
                Session["Membership"] = membershiptype;
                return RedirectToAction("Basket", "Bookings");
            }
        }

        public ActionResult AddAddOns(int addonId, int quantity)
        {
            // CHECK IF EVENT IS IN BASKET
            var eventsinbasket = (Dictionary<int, int>) Session["Tix"];

            if (Session["Tix"] == null || !eventsinbasket.ContainsKey(addonService.GetAddOn(addonId).EventId))
            {
                ViewBag.Message = "You must have a ticket in the basket for this event before you can buy add-ons.";
                return View("Error");
            }
            else if (quantity == 0)
            {
                ViewBag.Message = "Quantity = 0";
                return View("Error");
            }
            else
            {
                var modelint = new Dictionary<int, int>();

                // if this is the first add-on selected
                if (Session["AddOns"] == null)
                {
                    modelint.Add(addonId, quantity);
                    Session["AddOns"] = modelint;
                }

                // if this is NOT the first add-on selected
                else
                {

                    // if this add-on already exists in the session
                    if (eventsinbasket.ContainsKey(addonId))
                    {
                        modelint[addonId] += quantity;
                    }
                    // if this is a new add on
                    else
                    {
                        modelint = (Dictionary<int, int>) Session["AddOns"];
                        modelint.Add(addonId, quantity);
                        Session["AddOns"] = modelint;
                    }
                }

                return RedirectToAction("Basket", "Bookings");
            }
        }

        public ActionResult ClearBasket()
        {
            Session.Abandon();
            return RedirectToAction("Basket", "Bookings");
        }

        public ActionResult DeleteBasketTix(int id)
        {
            // remove tickets from session  
            var eventdictionary = (Dictionary<int, int>) Session["Tix"];
            eventdictionary.Remove(id);
            Session["Tix"] = eventdictionary;

            //check if any add-ons exist and delete those too
            var addondictionary = (Dictionary<int, int>) Session["AddOns"];
            int needtodelete = 0;

            foreach (KeyValuePair<int, int> addon in addondictionary)
            {
                if (addonService.GetAddOn(addon.Key).EventId == id)
                {
                    needtodelete = addon.Key;
                }
            }

            if (needtodelete != 0)
            {
                addondictionary.Remove(needtodelete);
            }

            Session["AddOns"] = addondictionary;

            return RedirectToAction("Basket", "Bookings");
        }

        public ActionResult DeleteBasketAddOn(int id)
        {
            var addondictionary = (Dictionary<int, int>) Session["AddOns"];
            if (addondictionary.Count > 1)
            {
                addondictionary.Remove(id);
                Session["AddOns"] = addondictionary;
            }
            else
            {
                Session["AddOns"] = null;
            }

            return RedirectToAction("Basket", "Bookings");
        }

        public ActionResult DeleteBasketMembership()
        {
            Session["Membership"] = null;
            return RedirectToAction("Basket", "Bookings");
        }

        public void CheckAvailability()
        {
            if (Session["AddOns"] != null)
            {
                var unavailableAddOns = new Dictionary<int, int>();
                var availableAddOns = new Dictionary<int, int>();

                //get session Tix (and Add-Ons)
                var addOnsBasket = (Dictionary<int, int>) Session["AddOns"];
                //iterate through Add Ons dictionary first
                foreach (KeyValuePair<int, int> addon in addOnsBasket)
                {
                    // get Addon Data:
                    var addoncapacity = addonService.GetAddOn(addon.Key).Quantity;

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

                        // check to see if ANY tickets are available, and keep these in available Basket
                        if (availability - addon.Value > 0)
                        {
                            // keep the number of available tickets in the basket
                            availableAddOns.Add(addon.Key, (availability - addon.Value));
                        }
                    }

                    // if the required number of tickets IS available:
                    else
                    {
                        availableAddOns.Add(addon.Key, addon.Value);
                    }
                }

                Session["AddOns"] = availableAddOns;
                Session["UAddOns"] = unavailableAddOns;
            }

            if (Session["Tix"] != null)
            {
                var unavailableTix = new Dictionary<int, int>();
                var availableTix = new Dictionary<int, int>();

                var tixBasket = (Dictionary<int, int>) Session["Tix"];
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

                        // check to see if ANY tickets are available, and keep these in available Basket
                        if (availability - tix.Value > 0)
                        {
                            // keep the number of available tickets in the basket
                            availableTix.Add(tix.Key, (availability - tix.Value));
                        }
                    }

                    // if the required number of tickets IS available:
                    else
                    {
                        availableTix.Add(tix.Key, tix.Value);
                    }
                }

                Session["Tix"] = availableTix;
                Session["UTix"] = unavailableTix;
            }
        }

        public ActionResult DeleteUnavailableTix(int id)
        {
            // get tickets from session  
            var Ueventdictionary = (Dictionary<int, int>)Session["UTix"];
            Ueventdictionary.Remove(id);
            if (Ueventdictionary.Count > 0)
            {
                Session["UTix"] = Ueventdictionary;
            }
            else
            {
                Session["UTix"] = null;
            }

            return RedirectToAction("Basket", "Bookings");
        }

        public ActionResult DeleteUnavailableAddOn(int id)
        {
            // get tickets from session  
            var Uaddondictionary = (Dictionary<int, int>)Session["UAddOns"];
            Uaddondictionary.Remove(id);
            if (Uaddondictionary.Count > 0)
            {
                Session["UAddOns"] = Uaddondictionary;
            }
            else
            {
                Session["UAddOns"] = null;
            }

            return RedirectToAction("Basket", "Bookings");
        }


    }
}

