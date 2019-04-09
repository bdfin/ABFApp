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
             ABFDbContext db;
             db = new ABFDbContext();

            PaymentService ps;
            ps = new PaymentService();


            var payment = new Payment()
            {
                Method = "card",
                Amount = 20

            };

            ps.CreatePayment(payment);
            db.SaveChanges();  
            
            
            // all the logic goes here
            return View();
        }
    }
}