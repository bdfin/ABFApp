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
        private BasketService basketService;
        private AddOnService addonService;
        private CustomerService customerService;
        public BasketController()
        {
            basketService = new BasketService();
            addonService = new AddOnService();
            customerService = new CustomerService();
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
                    model = (Dictionary<int, int>)Session["Tix"];

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
                ViewBag.Message ="You already have a Membership in your Basket. " +
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
            var eventsinbasket = (Dictionary<int, int>)Session["Tix"];      

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
            var addondictionary = (Dictionary<int, int>)Session["AddOns"];
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

    }
}