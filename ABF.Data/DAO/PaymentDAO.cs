using ABF.Data.ABFDbModels;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
