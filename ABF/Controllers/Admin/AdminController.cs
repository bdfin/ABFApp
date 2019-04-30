using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ABF.Service.Services;

namespace ABF.Controllers.Admin
{
    [Authorize(Roles = "Admin, Box Office")]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }     
    }
}
