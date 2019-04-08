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
        private CustomerDAO customerDAO;

        public CustomerService()

        {
            customerDAO = new CustomerDAO();
        }

        public IList<Customer> GetCustomers()
        {
            return customerDAO.GetCustomers();
        }

        public Customer GetCustomer(int id)
        {
            return customerDAO.GetCustomer(id);
        }

        public Customer GetCustomerByUserId(string id)
        {
            return customerDAO.GetCustomerByUserId(id);
        }

        public void CreateCustomer(Customer customer)
        {
            customerDAO.CreateCustomer(customer);
        }

        public void UpdateCustomer(Customer customer)
        {
            customerDAO.UpdateCustomer(customer);
        }

        public void DeleteCustomer(Customer customer)
        {
            customerDAO.DeleteCustomer(customer);
        }


    }
}
