using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ABF.Data.ABFDbModels;
using ABF.Service.Services;
using ABF.ViewModels;

namespace ABF.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminCustomersController : Controller
    {
        private MembershipTypeService membershipTypeService;
        private CustomerService customerService;

        public AdminCustomersController()
        {
            customerService = new CustomerService();
            membershipTypeService = new MembershipTypeService();
        }

        [Route("Admin/Customers")]
        public ActionResult Index()
        {
            return View(customerService.GetCustomers());
        }

        [Route("Admin/Customers/New")]
        public ActionResult New()
        {
            var membershipTypes = membershipTypeService.GetMembershipTypes();

            var viewModel = new CustomerFormViewModel
            {
                MembershipTypes = membershipTypes,
                Customer = new Customer()
            };

            return View("CustomerForm", viewModel);
        }

        public ActionResult Save(CustomerFormViewModel viewModel)
        {
            try
            {
                customerService.CreateCustomer(viewModel.Customer);

                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }

        [Route("Admin/Customers/Edit/{id}")]
        public ActionResult Edit(int id)
        {
            return View(customerService.GetCustomer(id));
        }

        [HttpPost]
        public ActionResult Edit(int custNo, Customer customer)
        {
            try
            {              
                customerService.EditCustomer(customer);
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
        public ActionResult DeleteCustomer(int id)
        {     
                Customer  customer;
                customer = customerService.GetCustomer(id);
                customerService.DeleteCustomer(customer);
                return RedirectToAction("Index");
           
        }
    }
}
