using ABF.Data.ABFDbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
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

        public Order getOrder(int Id)
        {
            var order = from orders in _context.Orders
                where orders.Id == Id 
                select orders;

            return order.First();
        }
        public Order getOrderFromPaymentId(string Id)
        {
            var order = from orders in _context.Orders
                where orders.PaymentId == Id 
                select orders;

            return order.First();
        }

        public IEnumerable<Order> getOrders()
        {
            var orders = from order 
                        in _context.Orders
                        select order;

            return orders;
        }

    }
}
