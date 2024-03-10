using Clarity.Web.Service.Models;

namespace Clarity.Web.Service.Interfaces
{
    public interface IEmployeeSalaryStructureService
    {
        Task<List<EmployeeSalaryStructure>> fetchEmployeeSalaryStructuresAsync();
        Task<EmployeeSalaryStructure> fetchEmployeeSalaryStructure(long employeeId);
    }
}
