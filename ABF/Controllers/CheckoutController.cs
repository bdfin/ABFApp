﻿using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
        private MembershipTypeService memberService;

        public CheckoutController()
        {
            eventService = new EventService();
            ticketService = new TicketService();
            addOnService = new AddOnService();
            orderService = new OrderService();
            paymentService = new PaymentService();
            customerService = new CustomerService();
            memberService = new MembershipTypeService();
        }

        // GET: /Checkout/StartCheckoutUser
        // Shows checkout page to users (linked to from basket)
        [Authorize]
        public ActionResult StartCheckoutUser()
        {
            // check the availability, and return to basket if anything needs changing
            if (this.CheckAvailability())
            {
                return RedirectToAction("BasketNoCheck", "Bookings");
            }
            else
            {
                var customer = customerService.GetCustomerByUserId(User.Identity.GetUserId());
                var tickettotal = this.calculategrandtotal();
                Session["GrandTotal"] = tickettotal;
                var vm = new SubmitViewModel
                {
                    name = customer.Name,
                    address1 = customer.Address1,
                    address2 = customer.Address2,
                    address3 = customer.Address3,
                    postcode = customer.PostCode,
                    email = customer.Email,
                    phone = customer.PhoneNumber,
                    total = tickettotal,
                    ismember = true,
                    updateneeded = false
                };
                return View("StartCheckout", vm);
            }
        }

        // GET: /Checkout/StartCheckoutGuest
        // Shows checkout page to guests (linked to from basket)
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
                var vm = new SubmitViewModel
                {
                    total = tickettotal,
                    ismember = false,
                    updateneeded = false
                };
                return View("StartCheckout", vm);
            }
        }

        // POST: /Checkout/Submit
        // Submits the customers/users details to make a new order
        [HttpPost]
        [ValidateAntiForgeryToken()]
        public ActionResult Submit(SubmitViewModel submitViewModel)
        {
            string customerid = "";
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
                switch (submitViewModel.paymentmethod)
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
                else if (paymentmethod == "cardDownload")
                {
                    deliverymethod = "download";
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

                foreach (var ticket in TicketList) 
                {
                    var pdfTicket = ticketService.GenerateTicket(ticket);
                }

                #endregion

                // clear all tickets from the basket!
                Session.Abandon();

                // all the logic goes here
                return View("OrderSuccess", viewModel);
            }
        }
                #region //-------------- Create/Update Customer Details

                if (submitViewModel.ismember && submitViewModel.updateneeded)               // member needs details updating
                {
                    var customer = customerService.GetCustomerByUserId(User.Identity.GetUserId());
                    customer.Name = submitViewModel.name;
                    customer.Address1 = submitViewModel.address1;
                    customer.Address2 = submitViewModel.address2;
                    customer.Address3 = submitViewModel.address3;
                    customer.Email = submitViewModel.email;
                    customer.PhoneNumber = submitViewModel.phone;
                    customer.PostCode = submitViewModel.phone;

                    customerService.UpdateCustomer(customer);
                    customerid = customer.Id;
                }
                else if (!submitViewModel.ismember)                                         // new customer
                {
                    customerid = Guid.NewGuid().ToString();
                    var customer = new Customer()
                    {
                        Id = customerid,
                        Name = submitViewModel.name,
                        Address1 = submitViewModel.address1,
                        Address2 = submitViewModel.address2,
                        Address3 = submitViewModel.address3,
                        PostCode = submitViewModel.postcode,
                        Email = submitViewModel.email,
                        PhoneNumber = submitViewModel.phone,
                        MembershipTypeId = 1
                    };
                    customerService.CreateCustomer(customer);
                }

                #endregion

                #region//---------------- create a new order
                var deliverymethod = "";
                if (submitViewModel.paymentmethod == "cheque" || submitViewModel.paymentmethod == "cardpost")
                {
                    deliverymethod = "post";
                }
                else if (submitViewModel.paymentmethod == "collect" || submitViewModel.paymentmethod == "cardcollect")
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
                    Delivery = deliverymethod,
                    DeliveryName = submitViewModel.name,
                    Address1 = submitViewModel.address1,
                    Address2 = submitViewModel.address2,
                    Address3 = submitViewModel.address3,
                    PostCode = submitViewModel.postcode,
                    Email = submitViewModel.email,
                    PhoneNumber = submitViewModel.phone
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

                return View("OrderSuccess", viewModel);
            }
        }


        public ActionResult DownloadTicket(string id)
        {
            MemoryStream stream = new MemoryStream();

            var ticket = ticketService.GetTicket(id);
            var pdfTicket = ticketService.GenerateTicket(ticket);

            pdfTicket.Save(stream, false);

            return File(stream, "application/pdf");
        }

        // Checks the availability of all tickets and addons in the basket
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

        // calculates the total of all the items in the basket
        protected decimal calculategrandtotal()
        {
            decimal grandtotal = 0;

            if (Session["Tix"] != null)
            {
                var alltix = (Dictionary<int, int>)Session["Tix"];
                foreach (KeyValuePair<int, int> singletix in alltix)
                {
                    var price = eventService.GetEvent(singletix.Key).TicketPrice;
                    var subtotal = price * singletix.Value;
                    grandtotal += subtotal;
                }
            }

            if (Session["AddOns"] != null)
            {
                var alladdons = (Dictionary<int, int>)Session["AddOns"];
                foreach (KeyValuePair<int, int> singleaddon in alladdons)
                {
                    var price = addOnService.GetAddOn(singleaddon.Key).Price;
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

    }
}
