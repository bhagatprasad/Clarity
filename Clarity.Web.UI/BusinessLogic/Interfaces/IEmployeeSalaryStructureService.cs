using Clarity.Web.UI.Models;

namespace Clarity.Web.UI.BusinessLogic.Interfaces
{
    public interface IEmployeeSalaryStructureService
    {
        Task<List<EmployeeSalaryStructure>> fetchEmployeeSalaryStructuresAsync();
        Task<EmployeeSalaryStructure> fetchEmployeeSalaryStructure(long employeeId);
    }
}
