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
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Visit()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "About Us";

            return View();
        }

        [HttpGet]
        public ActionResult Contact()
        {
            ViewBag.Message = "Contact Us";

            return View();
        }

        [HttpPost]
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

        public ActionResult FAQs()
        {
            return View();
        }

        public ActionResult TermsConditions()
        {
            return View();
        }

        public ActionResult Cookies()
        {
            return View();
        }
    }
}