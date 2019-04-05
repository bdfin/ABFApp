using ABF.Data.ABFDbModels;
using ABF.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ABF.Controllers
{
    public class BasketController : Controller
    {
        private BasketService basketService;
        public BasketController()
        {
            basketService = new BasketService();

        }

        // GET: Basket
        public ActionResult Index()
        {
            return View("Basketwithaddons", "Bookings");
        }

        public ActionResult AddToBasket(int eventId, int quantity)
        {
            if (quantity == 0)
            {
                ViewBag.Message = "Quantity = 0";
                // do nothing
            }
            else
            {
                var model = new Dictionary<int, int>();

                if (Session["Tix"] == null)
                {
                    model.Add(eventId, quantity);
                    Session["Tix"] = model;
                    ViewBag.Message = "New Basket created and tickets added";
                }

                else
                {
                    model = (Dictionary<int, int>)Session["Tix"];

                    if (model.ContainsKey(eventId))
                    {
                        model[eventId] += quantity;
                        ViewBag.Message = "Event aready in basket, quantity updated";
                    }
                    else
                    {
                        model.Add(eventId, quantity);
                        Session["Tix"] = model;
                        ViewBag.Message = "New event and quantity of tickets added";
                    }
                }
            }

            return View("AddtoBasket");
        }

        public ActionResult Membership()
        {
            return View();
        }

        public ActionResult AddMembership(int membershipId)
        {
            // if user already has membership in basket, can not add another - must delete from basket

            var ms = new MembershipTypeService();
            var membershiptype = ms.GetMembershipType(membershipId);

            Session["Membership"] = membershiptype;

            ViewBag.Message = "membership type " + membershipId + " has been added to basket";

            return View ("AddtoBasket");

        }

        public ActionResult AddAddOns(int addonId, int quantity)
        {
            if (quantity == 0)
            {
                ViewBag.Message = "Quantity = 0";
                // do nothing
            }
            else
            {
                var modeldict = new Dictionary<AddOn, int>();
                var modelint = new Dictionary<int, int>();
                var aos = new AddOnService();

                // if this is the first add-on selected
                if (Session["AddOns"] == null)
                {
                    modeldict.Add(aos.GetAddOn(addonId), quantity);
                    Session["AddOns"] = modeldict;
                    ViewBag.Message = "New Basket created and add ons added";
                }

                // if this is NOT the first add-on selected
                else
                {
                    modeldict = (Dictionary<AddOn, int>)Session["AddOns"];

                    // if this add-on already exists in the session
                    var thisaddon = aos.GetAddOn(addonId);

                    if (modeldict.ContainsKey(thisaddon))
                    {
                        modeldict[thisaddon] += quantity;
                        ViewBag.Message = "Add on already in basket, quantity updated";
                    }
                    // if this is a new add on
                    else
                    {
                        modeldict.Add(thisaddon, quantity);
                        Session["AddOns"] = modeldict;
                        ViewBag.Message = "New add on and quantity added";
                    }
                }
            }

            return View("AddtoBasket");

        }
 
    }
}