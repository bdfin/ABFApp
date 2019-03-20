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

        public Customer GetCustomer(int custNo)
        {
            return _customerDAO.GetCustomer(custNo);
        }

        public void CreateCustomer(Customer NewCustomer)
        {
            _customerDAO.CreateCustomer(NewCustomer);
        }

        public void EditCustomer(Customer UpdateCustomer)
        {
            _customerDAO.EditCustomer(UpdateCustomer);
        }

        public void DeleteCustomer(Customer RemoveCustomer)
        {
            _customerDAO.DeleteCustomer(RemoveCustomer);
        }
    }
}
