using ABF.Service.Services;
using ABF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ABF.Controllers.Admin
{
    public class AdminPaymentsController : Controller
    {
        // GET: AdminPayments
        public ActionResult Index()
        {
            var paymentService = new PaymentService();
            var orderService = new OrderService();
            var customerService = new CustomerService();

            var allPayments = paymentService.GetPayments();

            var viewModelList = new List<PaymentIndexViewModel>();

            foreach(var payment in allPayments)
            {
                var viewModel = new PaymentIndexViewModel()
                {
                    PaymentId = payment.Id,
                    PaymentMethod = payment.Method,
                    amount = payment.Amount,
                    OrderId = orderService.getOrderFromPaymentId(payment.Id).Id,
                    OrderDate=orderService.getOrderFromPaymentId(payment.Id).Date,
                    deliveryMethod=orderService.getOrderFromPaymentId(payment.Id).Delivery,
                    CustName=customerService.GetCustomer(orderService.getOrderFromPaymentId(payment.Id).CustomerId).Name,

                };
                viewModelList.Add(viewModel);

            }

            return View(viewModelList);
        }
    }
}