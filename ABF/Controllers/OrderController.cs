using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ABF.Service.Services;
using ABF.ViewModels;

namespace ABF.Controllers
{
    public class OrderController : Controller
    {
        OrderService orderService = new OrderService();
        CustomerService customerService = new CustomerService();
        PaymentService paymentService = new PaymentService();

        // GET: Order
        public ActionResult Index()
        {
            var orderlist = orderService.GetOrders();

            var viewModel = new List<OrderListViewModel>();

            foreach (var order in orderlist)
            {
                var ol = new OrderListViewModel()
                {
                    customerName = customerService.GetCustomer(order.CustomerId).Name,
                    deliveryMethod = order.Delivery,
                    orderDate = order.Date,
                    orderId = order.Id,
                    orderTime = order.Time,
                    paymentMethod = paymentService.GetPayment(order.PaymentId).Method,
                    price = paymentService.GetPayment(order.PaymentId).Amount
                };
                viewModel.Add(ol);
            }
            return View(viewModel);
        }
    }
}