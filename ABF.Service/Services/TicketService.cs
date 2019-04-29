using ABF.Data.ABFDbModels;
using ABF.Data.DAO;
using System.Collections.Generic;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System.IO;
using QRCoder;
using PdfSharp;
using PdfSharp.Drawing.Layout;

namespace ABF.Service.Services
{
    public class TicketService
    {
        private TicketDAO ticketDAO;

        public TicketService()
        {
            ticketDAO = new TicketDAO();
        }

        public IList<Ticket> GetAllTicketSales()
        {
            return ticketDAO.GetAllTicketSales();
        }

        public IList<Ticket> GetTicketSalesForEvent(int id)
        {
            return ticketDAO.GetTicketSalesForEvent(id);
        }

        public Dictionary<int, int> GetTicketSalesQuantitiesForAllEvents()
        {
          return ticketDAO.GetTicketSalesQuantitiesForAllEvents();
        }

        public Dictionary<int, int> GetAddOnSalesQuantitiesForAllEvents()
        {
          return ticketDAO.GetAddOnSalesQuantitiesForAllEvents();
        }

        public int GetTicketSalesQuantityForEvent(int id)
        {
            return ticketDAO.GetTicketSalesQuantityForEvent(id);
        }

        public int GetAddOnSalesQuantityForEvent(int id)
        {
            return ticketDAO.GetAddOnSalesQuantityForEvent(id);
        }

        public void CreateTicket(Ticket ticket)
        {
            ticketDAO.CreateTicket(ticket);        
        }

        public IEnumerable<Ticket> GetTicketsForOrder(int id)
        {
            return ticketDAO.GetTicketsForOrder(id);
        }

        public Ticket GetTicket(string id)
        {
            return ticketDAO.GetTicket(id);
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

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(ticketEvent.Id.ToString(), QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            System.Drawing.Image qrCodeImage = qrCode.GetGraphic(15);

            MemoryStream imageStream = new MemoryStream();

            qrCodeImage.Save(imageStream, System.Drawing.Imaging.ImageFormat.Png);

            PdfDocument document = new PdfDocument();

            PdfPage page = document.AddPage();
            page.Orientation = PageOrientation.Landscape; 

            XGraphics gfx = XGraphics.FromPdfPage(page);
            XFont font = new XFont("Sans-Serif", 18);
            XTextFormatter tf = new XTextFormatter(gfx);

            document.Info.Title = ticketEvent.Name + " " + ticket.Id.ToString();

            gfx.DrawImage(XImage.FromStream(imageStream), 0, 0);

            imageStream.Close();

            string eventInfo =
                ticketEvent.Name + "\n\n" +
                ticketEvent.Author + "\n\n" +
                ticketEvent.Date.ToShortDateString() + "\n\n" +
                ticketEvent.StartTime.ToShortTimeString() + " - " + ticketEvent.EndTime.ToShortTimeString();

            var rect = new XRect(40, 300, 255, 255);
            gfx.DrawRectangle(XBrushes.White, rect);
            tf.Alignment = XParagraphAlignment.Justify;
            tf.DrawString(eventInfo, font, XBrushes.Black, rect, XStringFormats.TopLeft);

            return document;
        }     
    }

}
