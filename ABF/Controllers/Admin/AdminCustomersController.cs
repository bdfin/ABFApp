using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ABF.Data.ABFDbModels;
using ABF.Service.Services;

namespace ABF.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminCustomersController : Controller
    {

        private CustomerService customerService;

        public AdminCustomersController()
        {
            customerService = new CustomerService();
        }

        [Route("Admin/Customers")]
        public ActionResult Index()
        {
            return View(customerService.GetCustomers());
        }

        [HttpPost]
        public ActionResult CreateCustomer(Customer NewCustomer)
        {
            try
            {
                // TODO: Add insert logic here
                customerService.CreateCustomer(NewCustomer);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [Route("Admin/Customers/Edit/{id}")]
        public ActionResult Edit(int id)
        {
            return View(customerService.GetCustomer(id));
        }

        [HttpPost]
        public ActionResult Edit(int custNo, Customer UpdateCustomer)
        {
            try
            {              
                customerService.EditCustomer(UpdateCustomer);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [Route("Admin/Customers/Delete/{id}")]
        public ActionResult Delete(int id)
        {
            return View(customerService.GetCustomer(id));
        }


        [HttpPost]
        public ActionResult Delete(int custNo, Customer RemoveCustomer)
        {     
                Customer  _customer;
                _customer = customerService.GetCustomer(custNo);
                customerService.DeleteCustomer(_customer);
                return RedirectToAction("Index");
           
        }
    }
}
