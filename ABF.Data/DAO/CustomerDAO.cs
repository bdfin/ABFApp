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

        public void CreateCustomer(Customer NewCustomer)
        {

            _context.Customers.Add(NewCustomer);
            _context.SaveChanges();

        }
        
        public void EditCustomer(Customer UpdateCustomer)

        {
            Customer UpCustomer = GetCustomer(UpdateCustomer.Id);

            UpCustomer.Name = UpdateCustomer.Name;
            UpCustomer.Address1 = UpdateCustomer.Address1;
            UpCustomer.Address2 = UpdateCustomer.Address2;
            UpCustomer.Address3 = UpdateCustomer.Address3;
            UpCustomer.PostCode = UpdateCustomer.PostCode;
            UpCustomer.Email = UpdateCustomer.Email;
            UpCustomer.PhoneNumber = UpdateCustomer.PhoneNumber;

            _context.SaveChanges();
        }

        public void DeleteCustomer(Customer RemoveCustomer)
        {
            _context.Customers.Remove(RemoveCustomer);
            _context.SaveChanges();
        }

    }
}
