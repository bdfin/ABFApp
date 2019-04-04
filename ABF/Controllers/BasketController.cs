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
            return View();
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

        public void AddMembership(int membershipId)
        {
            // if user already has membership in basket, can not add another - must delete from basket

            var ms = new MembershipTypeService();
            var membershiptype = ms.GetMembershipType(membershipId);

            Session["Membership"] = membershiptype;

            ViewBag.Message = "membership type " + membershipId + " has been added to basket";
            

        }
 
    }
}