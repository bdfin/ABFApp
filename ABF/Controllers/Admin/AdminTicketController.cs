using ABF.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ABF.Data.ABFDbModels;

namespace ABF.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public class AdminTicketController : Controller
    {
        private TicketService ticketService;
        private EventService eventService;
        private AddOnService addonService;

        public AdminTicketController()
        {
            ticketService = new TicketService();
            eventService = new EventService();
            addonService = new AddOnService();
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
            // get the dictionary of eventids and tickets sold
            var ticketquantities = ticketService.GetTicketSalesQuantitiesForAllEvents();

            // set up new dictionary of events and tickets sold
            var eventandquantitysold = new Dictionary<Event, int>();

            // get all the events
            var allevents = eventService.GetEvents();

            // add each event and the tickets sold to new dictionary
            foreach (Event e in allevents)
            {
                if (ticketquantities.ContainsKey(e.Id))
                {
                    eventandquantitysold.Add(e, ticketquantities[e.Id]);
                }
                else
                {
                    eventandquantitysold.Add(e, 0);
                }
            }

            // pass new dictionary to view
            return View(eventandquantitysold);
        }

        public ActionResult EventQuantity(int id)
        {
            var eventquantity = ticketService.GetTicketSalesQuantityForEvent(id);

            return View(eventquantity);
        }

    }
}