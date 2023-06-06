using System;
using System.Threading.Tasks;
using PaymentService.Services;

namespace PaymentService.Services
{
    public class PaymentService : IPaymentService
    {
        public Task<Tuple<bool, string>> DoPaymentAsync(int walletId, int userId, decimal totalAmount)
        {
            return Task.FromResult(new Tuple<bool, string>(true, "Jobs")); 
            //throw new NotImplementedException();
        }
    }
}