using System;
using System.Collections.Generic;
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
            IQueryable<Customer> _customers;
            _customers = from c
                         in _context.Customers
                         select c;

            return _customers.ToList();
            
        }


        public Customer GetCustomer(int custNo)
        {
            IQueryable<Customer> _customer;
                _customer = from c
                            in _context.Customers
                            where c.Id == custNo
                            select c;

            return _customer.ToList().First();
        }

        public void CreateCustomer(Customer customer)
        {

            _context.Customers.Add(customer);
            _context.SaveChanges();

        }
        
        public void EditCustomer(Customer customer)

        {
            Customer UpCustomer = GetCustomer(customer.Id);

            UpCustomer.Name = customer.Name;
            UpCustomer.Address1 = customer.Address1;
            UpCustomer.Address2 = customer.Address2;
            UpCustomer.Address3 = customer.Address3;
            UpCustomer.PostCode = customer.PostCode;
            UpCustomer.Email = customer.Email;
            UpCustomer.PhoneNumber = customer.PhoneNumber;

            _context.SaveChanges();
        }

        public void DeleteCustomer(Customer customer)
        {
            _context.Customers.Remove(customer);
            _context.SaveChanges();
        }

    }
}
