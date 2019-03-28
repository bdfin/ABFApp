using ABF.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ABF.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public class AdminTicketSalesController : Controller
    {
        private TicketSalesService ticketSalesService;

        public AdminTicketSalesController()
        {
            ticketSalesService = new TicketSalesService();
        }

        // GET: AdminTicketSales
        public ActionResult Index()
        {
            var allticketsales = ticketSalesService.GetAllTicketSales();

            return View(allticketsales);
        }

        public ActionResult EventTickets(int id)
        {
            var ticketsforevent = ticketSalesService.GetTicketSalesForEvent(id);

            return View(ticketsforevent);
        }

        public ActionResult AllTicketQuantities()
        {
            var ticketquantities = ticketSalesService.GetTicketSalesQuantitiesForAllEvents();

            return View(ticketquantities);
        }

        public ActionResult EventQuantity(int id)
        {
            var eventquantity = ticketSalesService.GetTicketSalesQuantityForEvent(id);

            return View(eventquantity);
        }
    }
}