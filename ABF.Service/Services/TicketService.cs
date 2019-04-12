using ABF.Data.ABFDbModels;
using ABF.Data.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABF.Service.Services
{
    public class TicketService
    {
        private TicketDAO _ticketSalesDAO;

        public TicketService()
        {
            _ticketSalesDAO = new TicketDAO();
        }

        public IList<Ticket> GetAllTicketSales()
        {
            return _ticketSalesDAO.GetAllTicketSales();
        }

        public IList<Ticket> GetTicketSalesForEvent(int id)
        {
            return _ticketSalesDAO.GetTicketSalesForEvent(id);
        }

        public Dictionary<int, int> GetTicketSalesQuantitiesForAllEvents()
        {
          return _ticketSalesDAO.GetTicketSalesQuantitiesForAllEvents();
        }

        public Dictionary<int, int> GetAddOnSalesQuantitiesForAllEvents()
        {
          return _ticketSalesDAO.GetAddOnSalesQuantitiesForAllEvents();
        }

        public int GetTicketSalesQuantityForEvent(int id)
        {
            return _ticketSalesDAO.GetTicketSalesQuantityForEvent(id);
        }

        public int GetAddOnSalesQuantityForEvent(int id)
        {
            return _ticketSalesDAO.GetAddOnSalesQuantityForEvent(id);
        }

        public void CreateTicket(Ticket ticket)
        {
            _ticketSalesDAO.CreateTicket(ticket);
        }
    }

}
