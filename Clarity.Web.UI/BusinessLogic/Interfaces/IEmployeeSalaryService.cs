using Clarity.Web.UI.Models;

namespace Clarity.Web.UI.BusinessLogic.Interfaces
{
    public interface IEmployeeSalaryService
    {
        Task<List<EmployeeSalaryModel>> FetchAllEmployeeSalaries(string all = "", string employeeCode ="",string month ="",string year= "", long employeeId = 0);
     
        Task<EmployeeSalaryModel> FetchEmployeeSalary(long employeeSalaryId);
        Task<List<EmployeeSalaryModel>> FetchEmployeeSalaryAsync(long employeeId);

        Task<EmployeeSalary> InsertOrUpdateEmployeeSalaryAsync(EmployeeSalary employeeSalary);
    }
}
