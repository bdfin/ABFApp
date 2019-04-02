using ABF.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ABF.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public class AdminTicketController : Controller
    {
        private TicketService ticketService;

        public AdminTicketController()
        {
            ticketService = new TicketService();
        }

        // GET: AdminTicketSales
        public ActionResult Index()
        {
            var allticketsales = ticketService.GetAllTicketSales();

            return View(allticketsales);
        }

        public ActionResult EventTickets(int id)
        {
            var ticketsforevent = ticketService.GetTicketSalesForEvent(id);

            return View(ticketsforevent);
        }

        public ActionResult AllTicketQuantities()
        {
            var ticketquantities = ticketService.GetTicketSalesQuantitiesForAllEvents();

            return View(ticketquantities);
        }

        public ActionResult EventQuantity(int id)
        {
            var eventquantity = ticketService.GetTicketSalesQuantityForEvent(id);

            return View(eventquantity);
        }
    }
}