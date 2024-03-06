using Clarity.Web.UI.Models;

namespace Clarity.Web.UI.BusinessLogic.Interfaces
{
    public interface IMonthlySalaryService
    {
        Task<bool> PublishMonthlySalary(MonthlySalary monthlySalary);
        Task<List<MonthlySalary>> fetchAllMonthlySalaries();
    }
}
