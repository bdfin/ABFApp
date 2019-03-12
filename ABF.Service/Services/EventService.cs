using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABF.Data.ABFDbModels;
using ABF.Data.DAO;
using ABF.Data.ViewModels;

namespace ABF.Service.Services
{
    public class EventService
    {
        private EventDAO _eventDAO;

        public EventService()
        {
            _eventDAO = new EventDAO();
        }

        public Event GetEvent(int id)
        {
            return _eventDAO.GetEvent(id);
        }

        public IList<Event> GetEvents()
        {
            return _eventDAO.GetEvents();
        } 

        public void CreateEvent(CreateEventViewModel viewModel)
        {
            _eventDAO.CreateEvent(viewModel);
        }
    }
}
