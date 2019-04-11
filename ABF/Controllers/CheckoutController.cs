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
        // GET: Checkout
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Checkout()
        {
            return View();
        }

        public ActionResult StartCheckoutUser()
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

            var tickettotal = this.calculategrandtotal() + (decimal) 1.50;
            Session["GrandTotal"] = tickettotal;

            var usercheckoutviewmodel = new UserCheckoutViewModel()
            {
                customer = cust,
                tickettotal = tickettotal
            };

            return View("StartCheckoutUser", usercheckoutviewmodel);
        }

        public ActionResult StartCheckoutGuest()
        {
            var tickettotal = this.calculategrandtotal() + (decimal) 1.50;
            Session["GrandTotal"] = tickettotal;
            return View("StartCheckout", tickettotal);
        }

        protected decimal calculategrandtotal()
        {
            EventService es = new EventService();
            AddOnService aos = new AddOnService();
            MembershipTypeService mts = new MembershipTypeService();
            decimal grandtotal = 0;


            if (Session["Tix"] != null)
            {
                var alltix = (Dictionary<int, int>) Session["Tix"];
                foreach (KeyValuePair<int, int> singletix in alltix)
                {
                    var price = es.GetEvent(singletix.Key).TicketPrice;
                    var subtotal = price * singletix.Value;
                    grandtotal += subtotal;
                }

            }

            if (Session["AddOns"] != null)
            {
                var alladdons = (Dictionary<int, int>) Session["AddOns"];
                foreach (KeyValuePair<int, int> singleaddon in alladdons)
                {
                    var price = aos.GetAddOn(singleaddon.Key).Price;
                    var subtotal = price * singleaddon.Value;
                    grandtotal += subtotal;
                }
            }

            if (Session["Membership"] != null)
            {
                var membershipprice = ((MembershipType) Session["Membership"]).Price;
                grandtotal += membershipprice;
            }

            return grandtotal;
        }

        [HttpPost]
        public ActionResult Submit(string name, string address1, string address2, string address3, string postcode,
            string email, string phone, string paymentmethod)
        {
            ABFDbContext db = new ABFDbContext();
            OrderService orderService = new OrderService();
            TicketService ticketService = new TicketService();

            #region //----------------- Make a new payment

            PaymentService ps;
            ps = new PaymentService();

            string paymentid = Guid.NewGuid().ToString();
            var payment = new Payment()
            {
                Id = paymentid,
                Method = paymentmethod,
                Amount = 20

            };

            ps.CreatePayment(payment);
            db.SaveChanges();

            #endregion

            #region //-------------- Create Customer Class

            CustomerService cs = new CustomerService();
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

            cs.CreateCustomer(customer);
            db.SaveChanges();

            #endregion

            #region//---------------- create a new order

            OrderService os = new OrderService();
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
            os.CreateOrder(order);
            db.SaveChanges();

            #endregion

            #region //------------ Create tickets for each item

            var TicketList = new List<Ticket>();

            var orderId = orderService.GetOrderId(paymentid, customerid);

            if (Session["Tix"] != null)
            {
                var alltix = (Dictionary<int, int>) Session["Tix"];
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
                        db.SaveChanges();
                        TicketList.Add(ticket);
                    }
                }

            }

            ;

            if (Session["AddOns"] != null)
            {
                var alladdons = (Dictionary<int, int>) Session["AddOns"];
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
                        db.SaveChanges();
                        TicketList.Add(ticket);
                    }
                }

            }

            ;

            #endregion

            // clear all tickets from the basket!
            Session.Abandon();

            // all the logic goes here
            return View("OrderSuccess", TicketList);
        }


        [HttpPost]
        public ActionResult SubmitUser(string name, string address1, string address2, string address3, string postcode,
            string email, string phone, string paymentmethod, string updatedetails)
        {
            ABFDbContext db = new ABFDbContext();
            OrderService orderService = new OrderService();
            TicketService ticketService = new TicketService();

            #region //----------------- Make a new payment

            PaymentService ps;
            ps = new PaymentService();

            string paymentid = Guid.NewGuid().ToString();
            var payment = new Payment()
            {
                Id = paymentid,
                Method = paymentmethod,
                Amount = 20

            };

            ps.CreatePayment(payment);
            db.SaveChanges();

            #endregion

            #region //-------------- Update Customer Details if appropriate

            CustomerService cs = new CustomerService();
            var userId = User.Identity.GetUserId();
            var custId = "";

            // check they have a customer account linked, if not create one
            try
            {
                // see if a customer account can be found
                custId = cs.GetCustomerByUserId(userId).Id;
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
                cs.CreateCustomer(newcustomer);
            }

            // if customer checked the update details box, update details
            if (updatedetails == "update")
            {
                custId = cs.GetCustomerByUserId(userId).Id;
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
                cs.UpdateCustomer(customerupdated);
            }

            #endregion

            #region//---------------- create a new order

            OrderService os = new OrderService();
            custId = cs.GetCustomerByUserId(userId).Id;

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
            os.CreateOrder(order);
            db.SaveChanges();

            #endregion

            #region //------------ Create tickets for each item

            var TicketList = new List<Ticket>();

            var orderId = orderService.GetOrderId(paymentid, custId);

            if (Session["Tix"] != null)
            {
                var alltix = (Dictionary<int, int>) Session["Tix"];
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
                        db.SaveChanges();
                        TicketList.Add(ticket);
                    }
                }

            }

            ;

            if (Session["AddOns"] != null)
            {
                var alladdons = (Dictionary<int, int>) Session["AddOns"];
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
                        db.SaveChanges();
                        TicketList.Add(ticket);
                    }
                }

            }

            ;

            #endregion

            #region //----------- turn user into member, if Membership was put in basket

            if (Session["Membership"] != null)
            {
                
                // add user to role 'member'
            }
            #endregion

            // clear all tickets from the basket!
            Session.Abandon();

            // all the logic goes here
            return View("OrderSuccess", TicketList);
        }
    }
}
