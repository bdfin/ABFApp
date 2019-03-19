using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABF.Data.ABFDbModels;
using ABF.Data.DAO;

namespace ABF.Service.Services
{
    public class EventService
    {
        private EventDAO eventDAO;

        public EventService()
        {
            eventDAO = new EventDAO();
        }

        public Event GetEvent(int id)
        {
            return eventDAO.GetEvent(id);
        }

        public IList<Event> GetEvents()
        {
            return eventDAO.GetEvents();
        } 

        public void CreateEvent(Event e)
        {
            eventDAO.CreateEvent(e);
        }

        public void UpdateEvent(Event e)
        {
            eventDAO.UpdateEvent(e);
        }

        public void DeleteEvent(Event e)
        {
            eventDAO.DeleteEvent(e);
        }

        public int GetNewEventId()
        {
            return eventDAO.GetNewEventId();
        }
    }
}
