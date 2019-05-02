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
        TicketService ticketService = new TicketService();
        PaymentService paymentService = new PaymentService();


        [Route("Admin/Orders")]
        // GET: Order/Index
        // Show the list of all orders in the database
        [Authorize(Roles = "Box Office, Admin")]

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

        [Route("Admin/Orders/Details/{id}")]
        // GET Order/Details/1
        // Show the details of a single order in the database
        [Authorize]
        public ActionResult Details(int id)
        {
            var order = orderService.GetOrder(id);
            var payment = paymentService.GetPayment(order.PaymentId);
            var customer = customerService.GetCustomer(order.CustomerId);
            var ticketlist = ticketService.GetTicketsForOrder(id);

            var details = new OrderDetailsViewModel()
            {
                order = order,
                payment = payment,
                customer = customer,
                ticketlist = ticketlist
            };
            return View(details);
        }
    }
}