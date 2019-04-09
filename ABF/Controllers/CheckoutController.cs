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
        public ActionResult Submit(string name, string address1, string address2, string address3, string postcode, string email, string phone)
        {
             ABFDbContext db;
             db = new ABFDbContext();

            //Make a new payment
            PaymentService ps;
            ps = new PaymentService();

            string paymentid = Guid.NewGuid().ToString();
            var payment = new Payment()
            {
                Id = paymentid, 
                Method = "card",
                Amount = 20

            };

            ps.CreatePayment(payment);
            db.SaveChanges();

            //Create Customer Class


            CustomerService cs;
            cs = new CustomerService();

            string customerid = Guid.NewGuid().ToString();

            var customer = new Customer()
            {
                Id = customerid,
                Name = name,
                Address1 = address1,
                Address2 = address2,
                Address3 = address3,
                PostCode = postcode,
                Email = email,
                PhoneNumber = phone,
            };

            cs.CreateCustomer(customer);
            db.SaveChanges();


            // all the logic goes here
            return View();
        }
    }
}