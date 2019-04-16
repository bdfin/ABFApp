using ABF.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ABF.Data.ABFDbModels;
using ABF.ViewModels;

namespace ABF.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public class AdminTicketController : Controller
    {
        private TicketService ticketService;
        private EventService eventService;
        private AddOnService addonService;
        private OrderService orderService;
        private CustomerService customerService;

        public AdminTicketController()
        {
            ticketService = new TicketService();
            eventService = new EventService();
            addonService = new AddOnService();
            orderService = new OrderService();
            customerService = new CustomerService();
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
            var viewModel = new List<TicketListViewModel>();
            var eventName = eventService.GetEvent(id).Name;

            foreach (var ticket in ticketsforevent)
            {
                var modelentry = new TicketListViewModel()
                {
                    ticketId = ticket.Id,
                    orderId = ticket.OrderId,
                    orderDate = orderService.GetOrder(ticket.OrderId).Date,
                    customerName = customerService.GetCustomer(orderService.GetOrder(ticket.OrderId).CustomerId).Name,
                    eventName = eventName
                };
                viewModel.Add(modelentry);
            }

            return View(viewModel);
        }

        public ActionResult AllTicketQuantities()
        {
            // get the dictionary of eventids and tickets sold
            var ticketquantities = ticketService.GetTicketSalesQuantitiesForAllEvents();
            var addonquantities = ticketService.GetAddOnSalesQuantitiesForAllEvents();

            // set up new dictionary of events and tickets sold
            var eventandquantitysold = new Dictionary<Event, int>();
            var addonandquantitysold = new Dictionary<AddOn, int>();

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

            // get all the addons
            var alladdons = addonService.GetAllAddOns();

            // add each addon and the tickets sold to new dictionary
            foreach (AddOn a in alladdons)
            {
                if (addonquantities.ContainsKey(a.Id))
                {
                    addonandquantitysold.Add(a, addonquantities[a.Id]);
                }
                else
                {
                    addonandquantitysold.Add(a, 0);
                }
            }

            var salesdata = new StockQuantities()
            {
                events = eventandquantitysold,
                addons = addonandquantitysold
            };

            // pass new dictionary to view
            return View(salesdata);
        }

        public ActionResult GetAllAvailabilities()
        {
            // get the tickets and add-on sales numbers (Dictionary<Id,quantity>)
            var ticketquantities = ticketService.GetTicketSalesQuantitiesForAllEvents();
            var addonquantities = ticketService.GetAddOnSalesQuantitiesForAllEvents();

            // set up new dictionary to store the available number of tickets or addons per event
            var ticketavailabilities = new Dictionary<int, int>();
            var addonavailabilities = new Dictionary<int, int>();

            // get all the events
            var allevents = eventService.GetEvents();

            // iterate through events, calculating availability
            foreach (var e in allevents)
            {
                if (ticketquantities.ContainsKey(e.Id))
                {
                    ticketavailabilities.Add(e.Id, (e.Capacity - ticketquantities[e.Id]));
                }
                else
                {
                    ticketavailabilities.Add(e.Id, e.Capacity);
                }
            }

            // get all the addons
            var alladdons = addonService.GetAllAddOns();

            // iterate through addons, calculating availability
            foreach (var a in alladdons)
            {
                if (addonquantities.ContainsKey(a.Id))
                {
                    addonavailabilities.Add(a.Id, (a.Quantity - addonquantities[a.Id]));
                }
                else
                {
                    addonavailabilities.Add(a.Id, a.Quantity);
                }
            }

            return View(ticketavailabilities);
        }

        public int GetAvailability(int id)
        {
            var ticketssold = ticketService.GetTicketSalesQuantityForEvent(id);
            var availability = eventService.GetEvent(id).Capacity - ticketssold;
            return availability;
        }

        public ActionResult EventQuantity(int id)
        {
            var eventquantity = ticketService.GetTicketSalesQuantityForEvent(id);

            return View(eventquantity);
        }

    }
}