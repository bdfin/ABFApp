using ABF.Data.ABFDbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ABF.Data.DAO
{
   public class OrderDAO
   {
        private ABFDbContext _context;

        public OrderDAO()
        {
            _context = new ABFDbContext();
        }

        public void CreateOrder(Order order)
        {

            _context.Orders.Add(order);
            _context.SaveChanges();

        }

        public int GetOrderId(string PaymentId, string CustomerId)
        {
            var orderId = from order in _context.Orders
                          where order.PaymentId == PaymentId
                          && order.CustomerId == CustomerId
                          select order.Id;

            return orderId.First();
        }

    }
}
