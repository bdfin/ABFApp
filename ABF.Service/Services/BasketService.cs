using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABF.Data.DAO;

namespace ABF.Service.Services
{
    public class BasketService
    {
        private BasketDAO basketDAO;

        public BasketService()
        {
            basketDAO = new BasketDAO();
        }

        //To add more
    }
}
