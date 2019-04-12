using ABF.Data.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABF.Data.ABFDbModels;

namespace ABF.Service.Services
{
    public class OrderService
    {
        private OrderDAO orderDAO;

        public OrderService()
        {
            orderDAO = new OrderDAO();
        }

        public void CreateOrder(Order order)
        {
            orderDAO.CreateOrder(order);
        }

        public int GetOrderId(string PaymentId, string CustomerId)
        {
           return orderDAO.GetOrderId(PaymentId, CustomerId);
        }

        public Order GetOrder(int Id)
        {
            return orderDAO.getOrder(Id);
        }

        public Order getOrderFromPaymentId(string Id)
        {
            return orderDAO.getOrderFromPaymentId(Id);
        }


        public IEnumerable<Order> GetOrders()
        {
            return orderDAO.getOrders();
        }

    }
}
