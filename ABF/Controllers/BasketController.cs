using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ABF.Controllers
{
    public class BasketController : Controller
    {
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
    }
}