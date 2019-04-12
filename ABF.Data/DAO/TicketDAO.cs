using ABF.Data.ABFDbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ABF.Data.DAO
{
    public class TicketDAO
    {
        private ABFDbContext _context;

        public TicketDAO()
        {
            _context = new ABFDbContext();
        }

        public IList<Ticket> GetAllTicketSales()
        {
            IQueryable<Ticket> tickets;

            tickets = from ticket 
                      in _context.Tickets
                      select ticket;

            return tickets.ToList();
        }

        public IList<Ticket> GetTicketSalesForEvent(int id)
        {
            IQueryable<Ticket> tickets;

            tickets = from ticket
                        in _context.Tickets
                        where ticket.EventId == id
                        select ticket;

            return tickets.ToList();
        }

        public Dictionary<int, int> GetTicketSalesQuantitiesForAllEvents()
        {
            // make a list of all the eventIds which tickets have been sold for
            var alltickets = this.GetAllTicketSales();

            var eventIDlist = new List<int>();

            foreach (var item in alltickets)
            {
                if (item.EventId != null)
                {
                    var conversionvariable = item.EventId.ToString();
                    eventIDlist.Add(Convert.ToInt16(conversionvariable));
                }
            }

            // get the unique list of eventIDs
            var uniqueventIDs = eventIDlist.Distinct();
            
            // get the ticket count for each unique ID
            var event_tickets = new Dictionary<int, int> { };
            foreach (int ID in uniqueventIDs)
            {
                int counter = 0;
                foreach (int x in eventIDlist)
                {
                    if (ID == x)
                        counter++;
                }

                event_tickets.Add(ID, counter);
            }

            return event_tickets;
        }

        public Dictionary<int, int> GetAddOnSalesQuantitiesForAllEvents()
        {
            // make a list of all the addonIds which tickets have been sold for
            var alladdons = this.GetAllTicketSales();

            var addonIDlist = new List<int>();

            foreach (var item in alladdons)
            {
                if (item.AddOnId != null)
                {
                    var conversionvariable = item.AddOnId.ToString();
                    addonIDlist.Add(Convert.ToInt16(conversionvariable));
                }
            }

            // get the unique list of addonIDs
            var uniqueaddonIDs = addonIDlist.Distinct();

            // get the ticket count for each unique ID
            var addon_tickets = new Dictionary<int, int> { };
            foreach (int ID in uniqueaddonIDs)
            {
                int counter = 0;
                foreach (int x in addonIDlist)
                {
                    if (ID == x)
                        counter++;
                }

                addon_tickets.Add(ID, counter);
            }

            return addon_tickets;
        }

        public int GetTicketSalesQuantityForEvent(int id)
        {
            int ticketcount;

            IQueryable<Ticket> tickets;
            tickets = from ticket
                        in _context.Tickets
                      where ticket.EventId == id
                      select ticket;

            ticketcount = tickets.Count();

            return ticketcount;
        }

        public int GetAddOnSalesQuantityForEvent(int id)
        {
            int addoncount;

            IQueryable<Ticket> tickets;
            tickets = from ticket
                        in _context.Tickets
                      where ticket.AddOnId == id
                      select ticket;

            addoncount = tickets.Count();

            return addoncount;
        }

        public void CreateTicket(Ticket ticket)
            {

            _context.Tickets.Add(ticket);
            _context.SaveChanges();

        }

        public IEnumerable<Ticket> GetTicketsForOrder(int id)
        {
            var ticketlist = 
                from ticket 
                in _context.Tickets
                where ticket.OrderId==id
                select ticket;

            return ticketlist;
        }
    }
}
