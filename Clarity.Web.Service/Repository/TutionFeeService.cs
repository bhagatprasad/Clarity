using Clarity.Web.Service.DBConfiguration;
using Clarity.Web.Service.Interfaces;
using Clarity.Web.Service.Models;
using Microsoft.EntityFrameworkCore;

namespace Clarity.Web.Service.Repository
{
    public class TutionFeeService: ITutionFeeService
    {
        private readonly ApplicationDBContext _dbContext;
        public TutionFeeService(ApplicationDBContext applicationDBContext)
        {
            this._dbContext = applicationDBContext;    
        }

        public async Task<List<TutionFee>> fetchAllTutionFees()
        {
            return await _dbContext.tutionFees.ToListAsync();
        }

        public async Task<bool> InsertOrUpdateTutionFee(TutionFee tutionFee)
        {
            if (tutionFee.Id > 0)
            {
                var data = await _dbContext.tutionFees.FindAsync(tutionFee.Id);
                if (data != null)
                {
                    data.PaidFee = tutionFee.PaidFee;
                }
            }
            else
            {
                await _dbContext.tutionFees.AddAsync(tutionFee);
            }
            var responce = await _dbContext.SaveChangesAsync();

            return responce == 1 ? true : false;
        }
    }
}
