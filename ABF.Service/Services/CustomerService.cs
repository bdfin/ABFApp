using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABF.Data.ABFDbModels;
using ABF.Data.DAO;

namespace ABF.Service.Services
{
   public class CustomerService
    {
        private CustomerDAO _customerDAO;

        public CustomerService()

        {
            _customerDAO = new CustomerDAO();
        }

        public IList<Customer> GetCustomers()
        {
            return _customerDAO.GetCustomers();
        }

        public Customer GetCustomer(int id)
        {
            return _customerDAO.GetCustomer(id);
        }

        public void CreateCustomer(Customer customer)
        {
            _customerDAO.CreateCustomer(customer);
        }

        public void EditCustomer(Customer customer)
        {
            _customerDAO.EditCustomer(customer);
        }

        public void DeleteCustomer(Customer customer)
        {
            _customerDAO.DeleteCustomer(customer);
        }
    }
}
