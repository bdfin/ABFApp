using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABF.Data.ABFDbModels;

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
    }
}
