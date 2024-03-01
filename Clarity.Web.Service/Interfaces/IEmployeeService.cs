using Clarity.Web.Service.Models;

namespace Clarity.Web.Service.Interfaces
{
    public interface IEmployeeService
    {
        Task<List<AddEditEmployee>> fetchAllEmployeesAsync();
        Task<bool> InsertOrUpdateAsync(AddEditEmployee employee);
        Task<AddEditEmployee> fetchEmployeeAsync(long employeeId);
    }
}
