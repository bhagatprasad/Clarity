using Clarity.Web.Service.Models;

namespace Clarity.Web.Service.Interfaces
{
    public interface ITutionFeeService
    {
        Task<List<EmployeeTutionFeesModel>> fetchAllTutionFees();

        Task<bool> InsertOrUpdateTutionFee(TutionFee tutionFee);
    }
}
