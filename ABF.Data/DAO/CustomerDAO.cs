using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABF.Data.ABFDbModels;

namespace ABF.Data.DAO
{
    public class CustomerDAO
    {
        private ABFDbContext _context;

        public CustomerDAO()
        {
            _context = new ABFDbContext();
        }


        public IList<Customer> GetCustomers()
        {
            IQueryable<Customer> customers;

            customers = from c
                         in _context.Customers
                         select c;

            return customers.ToList();
            
        }


        public Customer GetCustomer(string id)
        {
            IQueryable<Customer> customer;

            customer = from c
                        in _context.Customers
                        where c.Id == id
                        select c;
            return customer.ToList().First();
        }

        public Customer GetCustomerByUserId(string id)
        {
            IQueryable<Customer> customer;

            customer = from c
                       in _context.Customers
                       where c.UserId == id
                       select c;
            return customer.ToList().First();
        }

        public void CreateCustomer(Customer customer)
        {
            if (customer.Id == null)
            {
                customer.Id = Guid.NewGuid().ToString();
            }
            _context.Customers.Add(customer);
            _context.SaveChanges();
        }
        
        public void UpdateCustomer(Customer customer)
        {
            Customer UpCustomer = GetCustomer(customer.Id);
            UpCustomer.Name = customer.Name;
            UpCustomer.Address1 = customer.Address1;
            UpCustomer.Address2 = customer.Address2;
            UpCustomer.Address3 = customer.Address3;
            UpCustomer.PostCode = customer.PostCode;
            UpCustomer.Email = customer.Email;
            UpCustomer.PhoneNumber = customer.PhoneNumber;
            UpCustomer.MembershipTypeId = customer.MembershipTypeId;

            _context.SaveChanges();
        }

        public void DeleteCustomer(Customer customer)
        {
            _context.Customers.Remove(customer);
            _context.SaveChanges();
        }

    }
}
