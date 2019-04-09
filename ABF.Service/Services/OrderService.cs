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

    }
}
