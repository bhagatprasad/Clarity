using Clarity.Web.Service.Models;

namespace Clarity.Web.Service.Interfaces
{
    public interface IEmployeeSalaryService
    {
        Task<List<EmployeeSalaryModel>> FetchAllEmployeeSalarys();
        Task<List<EmployeeSalaryModel>> FetchAllEmployeeSalarys(string employeeCode);
        Task<List<EmployeeSalaryModel>> FetchAllEmployeeSalarys(long employeeId);
        Task<List<EmployeeSalaryModel>> FetchAllEmployeeSalarys(string month ="",string year ="");
        Task<EmployeeSalaryModel> FetchEmployeeSalary(long employeeSalaryId);
        Task<EmployeeSalary> InsertOrUpdateEmployeeSalaryAsync(EmployeeSalary employeeSalary);
    }
}
