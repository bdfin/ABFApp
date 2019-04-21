using ABF.Data.ABFDbModels;
using ABF.Data.DAO;
using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PdfSharp.Pdf;
using PdfSharp.Drawing;

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

        public IEnumerable<Ticket> GetTicketsForOrder(int id)
        {
            return _ticketSalesDAO.GetTicketsForOrder(id);
        }

        public PdfDocument GenerateTicket(Ticket ticket)
        {
            int eventId = 0;

            if (ticket.EventId.HasValue)
            {
                eventId = ticket.EventId.Value;   
            }

            EventService eventService = new EventService();
            var ticketEvent = eventService.GetEvent(eventId);

            PdfDocument document = new PdfDocument();
            PdfPage page = document.AddPage();

            XGraphics gfx = XGraphics.FromPdfPage(page);
            XFont font = new XFont("Sans-Serif", 20);

            document.Info.Title = ticketEvent.Name + ticket.Id.ToString();

            gfx.DrawString(ticketEvent.Name, font, XBrushes.Black,
                new XRect(0, 0, page.Width, page.Height), XStringFormats.Center);

            return document;
        }     
    }

}
