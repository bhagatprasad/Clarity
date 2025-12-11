using Clarity.Web.UI.Models;

namespace Clarity.Web.UI.BusinessLogic.Interfaces
{
    public interface ITutionFeeService
    {
        Task<List<EmployeeTutionFeesModel>> fetchAllTutionFees();

        Task<bool> InsertOrUpdateTutionFee(TutionFee tutionFee);
    }
}
