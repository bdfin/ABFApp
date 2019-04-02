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
    }
}