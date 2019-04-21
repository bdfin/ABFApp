using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ABF.Data.ABFDbModels;
using ABF.Service.Services;
using ABF.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace ABF.Controllers
{
    public class CheckoutController : Controller
    {
        private EventService eventService;
        private TicketService ticketService;
        private AddOnService addOnService;
        private OrderService orderService;
        private PaymentService paymentService;
        private CustomerService customerService;

        public CheckoutController()
        {
            eventService = new EventService();
            ticketService = new TicketService();
            addOnService = new AddOnService();
            orderService = new OrderService();
            paymentService = new PaymentService();
            customerService = new CustomerService();
        }


        // GET: Checkout
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult StartCheckoutUser()
        {
            // check the availability, and return to basket if anything needs changing
            if (this.CheckAvailability())
            {
                return RedirectToAction("BasketNoCheck", "Bookings");
            }
            else
            {
                var cust = new Customer();
                try
                {
                    var cs = new CustomerService();
                    cust = cs.GetCustomerByUserId(User.Identity.GetUserId());
                }
                catch
                {
                    // do nothing
                }

                var tickettotal = this.calculategrandtotal();
                Session["GrandTotal"] = tickettotal;

                var usercheckoutviewmodel = new UserCheckoutViewModel()
                {
                    customer = cust,
                    tickettotal = tickettotal
                };

                return View("StartCheckoutUser", usercheckoutviewmodel);
            }
        }

        public ActionResult StartCheckoutGuest()
        {
            if (this.CheckAvailability())
            {
                return RedirectToAction("BasketNoCheck", "Bookings");
            }
            else
            {
                var tickettotal = this.calculategrandtotal();
                Session["GrandTotal"] = tickettotal;
                return View("StartCheckout", tickettotal);
            }
        }

        protected decimal calculategrandtotal()
        {
            EventService es = new EventService();
            AddOnService aos = new AddOnService();
            MembershipTypeService mts = new MembershipTypeService();
            decimal grandtotal = 0;

            if (Session["Tix"] != null)
            {
                var alltix = (Dictionary<int, int>)Session["Tix"];
                foreach (KeyValuePair<int, int> singletix in alltix)
                {
                    var price = es.GetEvent(singletix.Key).TicketPrice;
                    var subtotal = price * singletix.Value;
                    grandtotal += subtotal;
                }
            }

            if (Session["AddOns"] != null)
            {
                var alladdons = (Dictionary<int, int>)Session["AddOns"];
                foreach (KeyValuePair<int, int> singleaddon in alladdons)
                {
                    var price = aos.GetAddOn(singleaddon.Key).Price;
                    var subtotal = price * singleaddon.Value;
                    grandtotal += subtotal;
                }
            }

            if (Session["Membership"] != null)
            {
                var membershipprice = ((MembershipType)Session["Membership"]).Price;
                grandtotal += membershipprice;
            }

            // Add the p&p fee
            grandtotal += (decimal)1.50;

            return grandtotal;
        }

        [HttpPost]
        public ActionResult Submit(string name, string address1, string address2, string address3, string postcode,
            string email, string phone, string paymentmethod)
        {
            // check the availability, and return to basket if anything needs changing
            if (this.CheckAvailability())
            {
                return RedirectToAction("BasketNoCheck", "Bookings");
            }
            else
            {
                var viewModel = new OrderSuccessViewModel();

                #region //----------------- Make a new payment
                string paymentid = Guid.NewGuid().ToString();
                var pmethod = "";
                switch (paymentmethod)
                {
                    case "cardcollect":
                    case "cardpost":
                    case "cardemail":
                        pmethod = "card";
                        break;
                    case "collectlater":
                        pmethod = "on collection";
                        break;
                    case "cheque":
                        pmethod = "cheque";
                        break;
                }

                var payment = new Payment()
                {
                    Id = paymentid,
                    Method = pmethod,
                    Amount = this.calculategrandtotal()
                };
                paymentService.CreatePayment(payment);
                #endregion

                #region //-------------- Create a new Customer
                string customerid = Guid.NewGuid().ToString();
                var customer = new Customer()
                {
                    Id = customerid,
                    Name = name,
                    Address1 = address1,
                    Address2 = address2,
                    Address3 = address3,
                    PostCode = postcode,
                    Email = email,
                    PhoneNumber = phone,
                };
                customerService.CreateCustomer(customer);
                #endregion

                #region//---------------- create a new order
                var deliverymethod = "";
                if (paymentmethod == "cheque" || paymentmethod == "cardpost")
                {
                    deliverymethod = "post";
                }
                else if (paymentmethod == "collect" || paymentmethod == "cardcollect")
                {
                    deliverymethod = "collect";
                }
                else
                {
                    deliverymethod = "email";
                }

                var order = new Order()
                {
                    Date = DateTime.Today,
                    Time = DateTime.Now,
                    CustomerId = customerid,
                    PaymentId = paymentid,
                    Delivery = deliverymethod
                };
                orderService.CreateOrder(order);
                viewModel.order = order;
                #endregion

                #region //------------ Create tickets for each item

                var TicketList = new List<Ticket>();

                var orderId = orderService.GetOrderId(paymentid, customerid);

                if (Session["Tix"] != null)
                {
                    var alltix = (Dictionary<int, int>)Session["Tix"];
                    foreach (KeyValuePair<int, int> singletix in alltix)
                    {
                        for (int i = 0; i < singletix.Value; i++)
                        {
                            var ticketId = Guid.NewGuid().ToString();
                            var ticket = new Ticket()
                            {
                                Id = ticketId,
                                EventId = singletix.Key,
                                OrderId = orderId,

                            };

                            ticketService.CreateTicket(ticket);
                            TicketList.Add(ticket);
                        }
                    }
                }

                if (Session["AddOns"] != null)
                {
                    var alladdons = (Dictionary<int, int>)Session["AddOns"];
                    foreach (KeyValuePair<int, int> singleaddon in alladdons)
                    {
                        for (int i = 0; i < singleaddon.Value; i++)
                        {
                            var ticketId = Guid.NewGuid().ToString();
                            var ticket = new Ticket()
                            {
                                Id = ticketId,
                                AddOnId = singleaddon.Key,
                                OrderId = orderId,

                            };

                            ticketService.CreateTicket(ticket);
                            TicketList.Add(ticket);
                        }
                    }

                }

                viewModel.tickets = TicketList;

                #endregion

                // clear all tickets from the basket!
                Session.Abandon();

                // all the logic goes here
                return View("OrderSuccess", viewModel);
            }
        }

        [HttpPost]
        public ActionResult SubmitUser(string name, string address1, string address2, string address3, string postcode,
            string email, string phone, string paymentmethod, string updatedetails)
        {
            // check the availability, and return to basket if anything needs changing
            if (this.CheckAvailability())
            {
                return RedirectToAction("BasketNoCheck", "Bookings");
            }
            else
            {
                var viewModel = new OrderSuccessViewModel() { newmember = false };

                #region //----------------- Make a new payment
                string paymentid = Guid.NewGuid().ToString();
                var pmethod = "";
                switch (paymentmethod)
                {
                    case "cardcollect":
                    case "cardpost":
                    case "cardemail":
                        pmethod = "card";
                        break;
                    case "collectlater":
                        pmethod = "on collection";
                        break;
                    case "cheque":
                        pmethod = "cheque";
                        break;
                }

                var payment = new Payment()
                {
                    Id = paymentid,
                    Method = pmethod,
                    Amount = this.calculategrandtotal()
                };

                paymentService.CreatePayment(payment);

                #endregion

                #region //-------------- Update Customer Details if appropriate
                var userId = User.Identity.GetUserId();
                var custId = "";
                var updatedetails1 = updatedetails;
                // check they have a customer account linked, if not create one
                try
                {
                    // see if a customer account can be found
                    custId = customerService.GetCustomerByUserId(userId).Id;
                }
                catch
                {
                    // if customer account does not exist:
                    var newcustomer = new Customer()
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserId = userId,
                        Name = name,
                        Address1 = address1,
                        Address2 = address2,
                        Address3 = address3,
                        PostCode = postcode,
                        Email = email,
                        PhoneNumber = phone,
                    };
                    customerService.CreateCustomer(newcustomer);
                    updatedetails1 = "update";
                }

                // if customer checked the update details box, update details
                if (updatedetails1 == "update")
                {
                    custId = customerService.GetCustomerByUserId(userId).Id;
                    var customerupdated = new Customer()
                    {
                        Id = custId,
                        Name = name,
                        Address1 = address1,
                        Address2 = address2,
                        Address3 = address3,
                        PostCode = postcode,
                        Email = email,
                        PhoneNumber = phone,
                    };
                    customerService.UpdateCustomer(customerupdated);
                }
                #endregion

                #region//---------------- create a new order
                custId = customerService.GetCustomerByUserId(userId).Id;

                var deliverymethod = "";
                if (paymentmethod == "cheque" || paymentmethod == "cardpost")
                {
                    deliverymethod = "post";
                }
                else if (paymentmethod == "collect" || paymentmethod == "cardcollect")
                {
                    deliverymethod = "collect";
                }
                else
                {
                    deliverymethod = "email";
                }

                var order = new Order()
                {
                    Date = DateTime.Today,
                    Time = DateTime.Now,
                    CustomerId = custId,
                    PaymentId = paymentid,
                    Delivery = deliverymethod
                };
                orderService.CreateOrder(order);
                viewModel.order = order;
                #endregion

                #region //------------ Create tickets for each item
                var TicketList = new List<Ticket>();
                var orderId = orderService.GetOrderId(paymentid, custId);
                if (Session["Tix"] != null)
                {
                    var alltix = (Dictionary<int, int>)Session["Tix"];
                    foreach (KeyValuePair<int, int> singletix in alltix)
                    {
                        for (int i = 0; i < singletix.Value; i++)
                        {
                            var ticketId = Guid.NewGuid().ToString();
                            var ticket = new Ticket()
                            {
                                Id = ticketId,
                                EventId = singletix.Key,
                                OrderId = orderId,

                            };
                            ticketService.CreateTicket(ticket);
                            TicketList.Add(ticket);
                        }
                    }
                }

                if (Session["AddOns"] != null)
                {
                    var alladdons = (Dictionary<int, int>)Session["AddOns"];
                    foreach (KeyValuePair<int, int> singleaddon in alladdons)
                    {
                        for (int i = 0; i < singleaddon.Value; i++)
                        {
                            var ticketId = Guid.NewGuid().ToString();
                            var ticket = new Ticket()
                            {
                                Id = ticketId,
                                AddOnId = singleaddon.Key,
                                OrderId = orderId,
                            };
                            ticketService.CreateTicket(ticket);
                            TicketList.Add(ticket);
                        }
                    }

                }

                viewModel.tickets = TicketList;
                #endregion

                #region //----------- turn user into member, if Membership was put in basket

                if (Session["Membership"] != null)
                {
                    var newmember = customerService.GetCustomer(custId);
                    var requestedmembershiptype = (MembershipType)Session["Membership"];
                    newmember.MembershipTypeId = requestedmembershiptype.Id;
                    newmember.DateJoined = DateTime.Now;
                    customerService.UpdateCustomer(newmember);
                    viewModel.newmember = true;
                }
                #endregion

                // clear all tickets from the basket!
                Session.Abandon();

                // all the logic goes here
                return View("OrderSuccess", viewModel);
            }
        }

        public bool CheckAvailability()
        {
            bool isChanged = false;

            if (Session["AddOns"] != null)
            {
                var unavailableAddOns = new Dictionary<int, int>();
                var availableAddOns = new Dictionary<int, int>();

                //get session Tix (and Add-Ons)
                var addOnsBasket = (Dictionary<int, int>)Session["AddOns"];

                //iterate through Add Ons dictionary first
                foreach (KeyValuePair<int, int> addon in addOnsBasket)
                {
                    // get Addon Data:
                    var addoncapacity = addOnService.GetAddOn(addon.Key).Quantity;

                    // get Sales Quantity for this addon:
                    var addonsSold = ticketService.GetAddOnSalesQuantityForEvent(addon.Key);

                    //get availability
                    var availability = addoncapacity - addonsSold;

                    //check to see if the number of tickets required is more than the available number
                    if (availability < addon.Value)
                    {
                        // calculate number of tickets to move to unavailable and add this to unavailable List
                        var quantityUnavailable = addon.Value - availability;
                        unavailableAddOns.Add(addon.Key, quantityUnavailable);
                        isChanged = true;

                        // check to see if ANY tickets are available, and keep these in available Basket
                        if (addon.Value - quantityUnavailable > 0)
                        {
                            // keep the number of available tickets in the basket
                            availableAddOns.Add(addon.Key, (addon.Value - quantityUnavailable));
                        }
                    }

                    // if the required number of tickets IS available:
                    else
                    {
                        availableAddOns.Add(addon.Key, addon.Value);
                    }
                }

                if (availableAddOns.Count != 0)
                {
                    Session["AddOns"] = availableAddOns;
                }
                else
                {
                    Session["AddOns"] = null;
                }

                if (unavailableAddOns.Count != 0)
                {
                    Session["UAddOns"] = unavailableAddOns;
                }
                else
                {
                    Session["UAddOns"] = null;
                }
            }

            if (Session["Tix"] != null)
            {
                var unavailableTix = new Dictionary<int, int>();
                var availableTix = new Dictionary<int, int>();

                var tixBasket = (Dictionary<int, int>)Session["Tix"];
                // iterate through Tix dictionary
                foreach (KeyValuePair<int, int> tix in tixBasket)
                {
                    // get Event Data:
                    var Eventcapacity = eventService.GetEvent(tix.Key).Capacity;
                    // get Sales Quantity for this Event:
                    var EventSold = ticketService.GetTicketSalesQuantityForEvent(tix.Key);
                    //get availability
                    var availability = Eventcapacity - EventSold;

                    //check to see if the number of tickets required is more than the available number
                    if (availability < tix.Value)
                    {
                        // calculate number of tickets to move to unavailable and add this to unavailable List
                        var quantityUnavailable = tix.Value - availability;
                        unavailableTix.Add(tix.Key, quantityUnavailable);
                        isChanged = true;

                        // check to see if ANY tickets are available, and keep these in available Basket
                        if (tix.Value - quantityUnavailable > 0)
                        {
                            // keep the number of available tickets in the basket
                            availableTix.Add(tix.Key, (tix.Value - quantityUnavailable));
                        }

                        // no tickets are available and all will be removed. Check for and remove any add-ons too.
                        else
                        {
                            if (Session["AddOns"] != null)
                            {
                                var removedaddons = new Dictionary<int, int>();
                                var addons = (Dictionary<int, int>)Session["AddOns"];

                                foreach (KeyValuePair<int, int> addon in addons)
                                {
                                    if (tix.Key == addOnService.GetAddOn(addon.Key).EventId)
                                    {
                                        removedaddons.Add(addon.Key, addon.Value);
                                    }
                                }

                                // update the Session Values
                                if (addons.Count == 0)
                                {
                                    Session["AddOns"] = null;
                                }
                                else
                                {
                                    Session["AddOns"] = addons;
                                }

                                if (removedaddons.Count != 0)
                                {
                                    Session["RAddOns"] = removedaddons;
                                }
                            }
                        }
                    }

                    // if the required number of tickets IS available:
                    else
                    {
                        availableTix.Add(tix.Key, tix.Value);
                    }
                }

                if (availableTix.Count != 0)
                {
                    Session["Tix"] = availableTix;
                }
                else
                {
                    Session["Tix"] = null;
                }

                if (unavailableTix.Count != 0)
                {
                    Session["UTix"] = unavailableTix;
                }
                else
                {
                    Session["UTix"] = null;
                }
            }

            return isChanged;
        }
    }
}
