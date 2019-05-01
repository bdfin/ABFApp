using ABF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ABF.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home/Index
        public ActionResult Index()
        {
            return View();
        }

        // GET: Home/Visit
        public ActionResult Visit()
        {
            return View();
        }

        // GET: Home/About
        public ActionResult About()
        {
            ViewBag.Message = "About Us";

            return View();
        }

        // GET: Home/Contact
        [HttpGet]
        public ActionResult Contact()
        {
            ViewBag.Message = "Contact Us";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Contact(string name, string email, string phone, string message)
        {
            string emailBody = "The following Contact Form has been submitted from the website:\n\n\t" +
                "Name:\t\t" + name + "\n" +
                "\tEmail:\t\t" + email + "\n" +
                "\tPhone:\t\t" + phone + "\n" +
                "\tMessage:\n\t\t\t" + message;

            SMTPEmail.SendEmail("appledoretest@gmail.com", "Contact Form Submission", emailBody);

            return View("ContactSent");
        }

        // GET: Home/FAQs
        public ActionResult FAQs()
        {
            return View();
        }

        // GET: Home/TermsConditions
        public ActionResult TermsConditions()
        {
            return View();
        }

        // GET: Home/Cookies
        public ActionResult Cookies()
        {
            return View();
        }
    }
}