using Clarity.Web.Service.Models;

namespace Clarity.Web.Service.Interfaces
{
    public interface IMonthlySalaryService
    {
        Task<bool> PublishMonthlySalary(MonthlySalary monthlySalary);
        Task<List<MonthlySalary>> fetchAllMonthlySalaries();
    }
}
