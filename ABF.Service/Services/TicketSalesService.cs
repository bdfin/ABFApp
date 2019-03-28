using ABF.Data.ABFDbModels;
using ABF.Data.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABF.Service.Services
{
    public class TicketSalesService
    {
        private TicketSalesDAO _ticketSalesDAO;

        public TicketSalesService()
        {
            _ticketSalesDAO = new TicketSalesDAO();
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

        public int GetTicketSalesQuantityForEvent(int id)
        {
            return _ticketSalesDAO.GetTicketSalesQuantityForEvent(id);
        }
    }
}
