using ABF.Data.ABFDbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABF.Data.DAO
{
    public class TicketSalesDAO
    {
        private ABFDbContext _context;

        public TicketSalesDAO()
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
            IQueryable<int> eventIDs;
            eventIDs = from ticket
                      in _context.Tickets
                      select ticket.EventId;

            // get the unique list of eventIDs
            IList<int> uniqueventIDs = new List<int> { };
            foreach (var item in eventIDs)
            {
                if (!uniqueventIDs.Contains(item))
                {
                    uniqueventIDs.Add(item);
                }
            }

            // get the ticket count for each unique ID
            var event_tickets = new Dictionary<int, int> { };
            foreach (int ID in uniqueventIDs)
            {
                int counter = 0;
                foreach (int x in eventIDs)
                {
                    if (ID == x)
                        counter++;
                }

                event_tickets.Add(ID, counter);
            }

            return event_tickets;
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

    }
}
