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
                var numid = 0;
                var memid = customer.MembershipTypeId.ToString();
                if(memid != "")
                {
                     numid = Convert.ToInt16(memid);
                }
                else
                {
                    numid = 1;
                }

                if(numid != 1)
                {

                    var viewModel = new AllMembersViewModel()
                    {

                        Name = customer.Name,
                        MembershipTypeId = customer.MembershipTypeId,
                        Type = membershipTypeService.GetMembershipType(numid).Type
                      
                   
                    };
                     ViewModelList.Add(viewModel);
                }

            }

             return View(ViewModelList);
        }
    }
}