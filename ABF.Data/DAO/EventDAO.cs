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
