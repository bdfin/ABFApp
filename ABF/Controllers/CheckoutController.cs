using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ABF.Data.ABFDbModels;
using ABF.Service.Services;
using Microsoft.AspNet.Identity;

namespace ABF.Controllers
{
    public class CheckoutController : Controller
    {
        // GET: Checkout
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Checkout()
        {
            return View();
        }

        public ActionResult StartCheckoutUser()
        {
            var cs = new CustomerService();

            var customer = cs.GetCustomerByUserId(User.Identity.GetUserId());

            return View("StartCheckoutUser", customer);
        }

        public ActionResult StartCheckoutGuest()
        {
            return View("StartCheckout");
        }


        [HttpPost]
        public ActionResult Submit()
        {

            var payment = new Payment()
            {


            };

            
            
            
            // all the logic goes here
            return View();
        }
    }
}