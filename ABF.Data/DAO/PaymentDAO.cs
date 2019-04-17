using ABF.Data.ABFDbModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ABF.Data.DAO
{
     public class PaymentDAO
    {
        private ABFDbContext _context;

        public PaymentDAO()
        {
            _context = new ABFDbContext();
        }

        public void createpayment(Payment payment)
        {
            _context.Payments.Add(payment);
            _context.SaveChanges();
        }

        public Payment GetPayment(string Id)
        {
            var payment = from pay in _context.Payments
                where pay.Id == Id select pay;
            return payment.First();
        }

        public IList<Payment> GetPayments()
        {
            IQueryable<Payment> payments;
             payments = from pay in _context.Payments
                           select pay;

            return payments.ToList();
        }
    }
}
