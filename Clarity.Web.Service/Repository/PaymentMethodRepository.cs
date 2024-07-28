using Clarity.Web.Service.DBConfiguration;
using Clarity.Web.Service.Interfaces;
using Clarity.Web.Service.Models;
using Microsoft.EntityFrameworkCore;

namespace Clarity.Web.Service.Repository
{
    public class PaymentMethodRepository: IPaymentMethodService
    {
        private readonly ApplicationDBContext _DBContext;
        public PaymentMethodRepository(ApplicationDBContext DBContext)
        {
            this._DBContext = DBContext;
        }

        public async Task<List<PaymentMethod>> GetAllPaymentMethods()
        {
            try
            {
                return  await _DBContext.paymentMethods.ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
           
            //return await _DBContext.paymentMethods.ToListAsync();
        }

        public async Task<bool> InsertPaymentMethods(PaymentMethod paymentMethod)
        {
            if (paymentMethod == null)
            {
                return false;
            }

            try
            {
                await _DBContext.paymentMethods.AddAsync(paymentMethod);
                await _DBContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {                
                Console.WriteLine($"Error inserting payment method: {ex.Message}");
                return false;
            }
        }
    }

}
