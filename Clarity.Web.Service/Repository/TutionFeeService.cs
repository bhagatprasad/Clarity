using Clarity.Web.Service.DBConfiguration;
using Clarity.Web.Service.Interfaces;
using Clarity.Web.Service.Models;
using Microsoft.EntityFrameworkCore;

namespace Clarity.Web.Service.Repository
{
    public class TutionFeeService : ITutionFeeService
    {
        private readonly ApplicationDBContext _dbContext;
        public TutionFeeService(ApplicationDBContext applicationDBContext)
        {
            this._dbContext = applicationDBContext;
        }

        public async Task<List<EmployeeTutionFeesModel>> fetchAllTutionFees()
        {

            var tutionFees = (from tutionFee in _dbContext.tutionFees
                              join emplopyee in _dbContext.employees on tutionFee.EmployeeId equals emplopyee.EmployeeId into employeeTutionFees
                              from employeeTutionFeeJoin in employeeTutionFees.DefaultIfEmpty()
                              select new EmployeeTutionFeesModel
                              {
                                  Id= tutionFee.Id,
                                  EmployeeId= tutionFee.EmployeeId,
                                  EmployeeFullName = employeeTutionFeeJoin != null ? employeeTutionFeeJoin.FirstName + "" + employeeTutionFeeJoin.LastName : string.Empty,
                                  ActualFee = tutionFee.ActualFee,
                                  FinalFee = tutionFee.FinalFee,
                                  RemaingFee = tutionFee.RemaingFee,
                                  PaidFee = tutionFee.PaidFee,
                                  CreatedOn = tutionFee.CreatedOn,

                              }).ToList();

            return tutionFees;
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
