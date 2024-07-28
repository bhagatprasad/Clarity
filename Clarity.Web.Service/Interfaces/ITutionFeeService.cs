using Clarity.Web.Service.Models;

namespace Clarity.Web.Service.Interfaces
{
    public interface ITutionFeeService
    {
        Task<List<TutionFee>> fetchAllTutionFees();

        Task<bool> InsertOrUpdateTutionFee(TutionFee tutionFee);
    }
}
