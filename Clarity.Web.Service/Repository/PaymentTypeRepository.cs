using Clarity.Web.Service.DBConfiguration;
using Clarity.Web.Service.Interfaces;
using Clarity.Web.Service.Models;
using Microsoft.EntityFrameworkCore;

namespace Clarity.Web.Service.Repository
{
    public class PaymentTypeRepository : IPaymentTypeService
    {
        private readonly ApplicationDBContext _dbContext;
        public PaymentTypeRepository(ApplicationDBContext dBContext)
        {
            this._dbContext = dBContext;
        }
        public async Task<List<PaymentType>> GetAllPaymentTypes()
        {
            var data = await _dbContext.paymentTypes.Where(x => x.Id > 0).ToListAsync();
            return data;
        }
        public async Task<bool> InsertPaymentTypes(PaymentType paymentType)
        {
            var data = await _dbContext.AddAsync(paymentType);
            await _dbContext.SaveChangesAsync();
            return true;

        }
    }
}
