using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ABF.Data.ABFDbModels;
using ABF.Service.Services;

namespace ABF.Controllers
{
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

        // GET: Customer/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Customer/Create
        public ActionResult CreateCustomer()
        {
            return View();
        }

        // POST: Customer/Create
        [HttpPost]
        public ActionResult CreateCustomer(Customer NewCustomer)
        {
            try
            {
                // TODO: Add insert logic here
                customerService.CreateCustomer(NewCustomer);
                return RedirectToAction("GetCustomers");
            }
            catch
            {
                return View();
            }
        }

        // GET: Customer/Edit/5
        [Route("Admin/Customer/Edit/{custNo}")]
        public ActionResult Edit(int custNo)
        {
            return View(customerService.GetCustomer(custNo));
        }

        // POST: Customer/Edit/5
        [HttpPost]
        [Route("Customer/Edit/{custNo}")]
        public ActionResult Edit(int custNo, Customer UpdateCustomer)
        {
            try
            {
                // TODO: Add update logic here
                customerService.EditCustomer(UpdateCustomer);
                return RedirectToAction("GetCustomers");
            }
            catch
            {
                return View();
            }
        }

        // GET: Customer/Delete/5
        [Route("Customer/Delete/{custNo}")]
        public ActionResult Delete(int custNo)
        {
            return View(customerService.GetCustomer(custNo));
        }

        // POST: Customer/Delete/5
        [HttpPost]
        [Route("Customer/Delete/{custNo}")]
        public ActionResult Delete(int custNo, Customer RemoveCustomer)
        {
            
                // TODO: Add delete logic here
                Customer  _customer;
                _customer = customerService.GetCustomer(custNo);
                customerService.DeleteCustomer(_customer);
                return RedirectToAction("GetCustomers");
           
        }
    }
}
