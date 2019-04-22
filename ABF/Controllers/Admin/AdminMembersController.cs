using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ABF.Data.ABFDbModels;
using ABF.Service.Services;
using ABF.ViewModels;

namespace ABF.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public class AdminMembersController : Controller
    {

        private MembershipTypeService membershipTypeService;
        private CustomerService customerService;
        // GET: AdminMembers
        public ActionResult Index()
        {
            var customerService = new CustomerService();
            var membershipTypeService = new MembershipTypeService();

            var allMembers = customerService.GetCustomers();


            var ViewModelList = new List<AllMembersViewModel>();

            

            foreach( var customer in allMembers)
            {
                if(customer.MembershipTypeId != null)
                {

                    var viewModel = new AllMembersViewModel()
                    {

                        Name = customer.Name,
                        MembershipTypeId = customer.MembershipTypeId,
                      
                   
                    };
                     ViewModelList.Add(viewModel);
                }

            }

             return View(ViewModelList);
        }
    }
}