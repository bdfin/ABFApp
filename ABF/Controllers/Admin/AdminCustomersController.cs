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

        [ValidateAntiForgeryToken]
        public ActionResult Save(CustomerFormViewModel viewModel)
        {
            if (viewModel.Customer.Id == "0" || viewModel.Customer.Id == null)
            {
                viewModel.Customer.MembershipTypeId = 1;
                customerService.CreateCustomer(viewModel.Customer); 

                return RedirectToAction("Index");
            }
            else
            {
                customerService.UpdateCustomer(viewModel.Customer);

                return RedirectToAction("Index");
            }
        }

        [Route("Admin/Customers/Edit/{id}")]
        public ActionResult Edit(string id)
        {
            var membershipTypes = membershipTypeService.GetMembershipTypes();
            var customer = customerService.GetCustomer(id);

            var viewModel = new CustomerFormViewModel
            {
                MembershipTypes = membershipTypes,
                Customer = customer
            };

            return View("CustomerForm", viewModel);
        }

        public ActionResult Details(string id)
        {
            return View(customerService.GetCustomer(id));
        }

        [Route("Admin/Customers/Delete/{id}")]
        public ActionResult Delete(string id)
        {
            return View(customerService.GetCustomer(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCustomer(string id)
        {
            var customer = customerService.GetCustomer(id);
            customerService.DeleteCustomer(customer);
            
            return RedirectToAction("Index");
           
        }

        public ActionResult RenewAllMemberships()
        {
            var allusers = customerService.GetCustomers();

            foreach (var user in allusers)
            {
                var needreset = membershipTypeService.GetMembershipType(user.MembershipTypeId).Expiry;

                if (needreset)
                {
                    user.MembershipTypeId = 1;
                    user.DateJoined = null;
                    customerService.UpdateCustomer(user);
                }
            }
            return View("Index", customerService.GetCustomers());
        }
    }
}
