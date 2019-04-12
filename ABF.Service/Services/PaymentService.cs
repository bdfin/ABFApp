using ABF.Data.ABFDbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABF.Data.DAO;


namespace ABF.Service.Services
{
    public class PaymentService
    {
        private PaymentDAO paymentDAO;

        public PaymentService()
        {
            paymentDAO = new PaymentDAO();
        }

        public void CreatePayment(Payment payment)
        {
            paymentDAO.createpayment(payment);

        }

        public Payment GetPayment(string Id)
        {
            return paymentDAO.GetPayment(Id);
        }


        public IList<Payment> GetPayments()
        {
            return paymentDAO.GetPayments();
        }
    }
}
