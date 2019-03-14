using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABF.Data.ABFDbModels;
using ABF.Data.ViewModels;

namespace ABF.Data.DAO
{
    public class EventDAO
    {
        private ABFDbContext _context;

        public EventDAO()
        {
            _context = new ABFDbContext();
        }

        public Event GetEvent(int id)
        {
            IQueryable<Event> _event;

            _event = from e
                     in _context.Events
                     where e.Id == id
                     select e;

            return _event.ToList().First();
        }

        public IList<Event> GetEvents()
        {
            IQueryable<Event> _events;

            _events = from e
                      in _context.Events
                      select e;

            return _events.ToList();
        }

        public void CreateEvent(EventFormViewModel viewModel)
        {
            _context.Events.Add(viewModel.Event);
            _context.SaveChanges();
        }

        public void UpdateEvent(EventFormViewModel viewModel)
        {
            Event _event = GetEvent(viewModel.Event.Id);

            _event.Date = viewModel.Event.Date;
            _event.StartTime = viewModel.Event.StartTime;
            _event.EndTime = viewModel.Event.EndTime;
            _event.Name = viewModel.Event.Name;
            _event.Details = viewModel.Event.Details;
            _event.Description = viewModel.Event.Description;
            _event.Capacity = viewModel.Event.Capacity;
            _event.IsMemberOnly = viewModel.Event.IsMemberOnly;
            _event.LocationId = viewModel.Event.LocationId;
            _event.ImageId = viewModel.Event.ImageId;

            _context.SaveChanges();
        }
    }
}
